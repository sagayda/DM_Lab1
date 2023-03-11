using System.Numerics;

namespace DM_Labs
{
    public static class SLAE
    {
        public static void Solve(double[,] mainMatrix, double[] vector)
        {
            Console.WriteLine("\t--Solution by Gaussian method");
            double[] result = GaussianMethodStraightCourse(mainMatrix, vector);

            MatrixHandler.PrintMatrix(result, "\t--Result:");
            MatrixHandler.PrintMatrix(DiscontinuityVector(mainMatrix, vector, result), "\t--DiscontinuityVector");
        }
        private static double[] GaussianMethodStraightCourse(double[,] mainMatrix, double[] vector)
        {
            if (mainMatrix.GetLength(1) != vector.Length)
            {
                Console.WriteLine("\t--Matrix and vector have different size");
                return null;
            }
             
            for (int i = 0; i < mainMatrix.GetLength(0); i++)
            {
                int maxCoefficientIndex = i;
                double maxCoefficient = mainMatrix[i, i];

                for (int x = i; x < mainMatrix.GetLength(0); x++)
                {
                    if (Math.Abs(mainMatrix[x, i]) > Math.Abs(maxCoefficient) && vector[x] != 0)
                    {
                        maxCoefficientIndex = x;
                        maxCoefficient = mainMatrix[x, i];
                    }
                }

                if (maxCoefficientIndex != i)
                {
                    mainMatrix = MatrixHandler.RearrangeRows(mainMatrix, i, maxCoefficientIndex);
                    vector = MatrixHandler.RearrangeRows(vector, i, maxCoefficientIndex);
                    MatrixHandler.PrintMatrix(mainMatrix, vector, $"\t-- {i + 1} row rearranged to {maxCoefficientIndex + 1}");
                }

                GaussianMethodStep(mainMatrix, vector, i);
            }

            MainDiagonalToOne(mainMatrix, vector);
            MatrixHandler.PrintMatrix(mainMatrix, vector, "\t--Main diagonal to 1");

            return GaussianMethodReverseCourse(mainMatrix, vector);
        }
        private static void GaussianMethodStep(double[,] matrix, double[] vector, int i)
        {
            if (i + 1 >= matrix.GetLength(1) || i + 1 >= matrix.GetLength(0))
            {
                MatrixHandler.PrintMatrix(matrix, vector, $"\t--Final step");
                return;
            }

            double upRowCoeff = matrix[i, i];
            for (int x = i + 1; x < matrix.GetLength(0); x++)
            {
                double rowCoeff = matrix[x, i];
                for (int y = i; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] -= rowCoeff / upRowCoeff * matrix[i, y];
                }
                vector[x] -= rowCoeff / upRowCoeff * vector[i];
            }

            MatrixHandler.PrintMatrix(matrix, vector, $"\t--Step {i + 1}");
            return;
        }
        private static void MainDiagonalToOne(double[,] matrix, double[] vector)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                double mainCoeff = matrix[x, x];
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] /= mainCoeff;
                }
                vector[x] /= mainCoeff;
            }
        }
        private static double[] GaussianMethodReverseCourse(double[,] mainMatrix, double[] vector)
        {
            Console.WriteLine("\t--Reverse Course");

            double[] result = new double[vector.Length];

            for (int x = vector.Length - 1; x >= 0; x--)
            {
                double mainCoeff = mainMatrix[x, x];
                result[x] = vector[x];


                if (x == vector.Length - 1)
                {
                    result[x] = vector[x] / mainMatrix[x, x];
                    result[x] = Math.Round(result[x], 13);
                    Console.Write($"\nX{x + 1} = {result[x]}");
                    continue;
                }

                Console.Write($"\nX{x + 1} = {result[x]}");

                for (int y = x + 1; y < mainMatrix.GetLength(1); y++)
                {
                    Console.Write($" - ({mainMatrix[x, y]} * {result[y]})");
                    result[x] -= mainMatrix[x, y] * result[y];
                }

                result[x] = Math.Round(result[x], 13);
            }

            Console.WriteLine("\n###############################");
            return result;
        }
        private static double[] DiscontinuityVector(double[,] matrix, double[] vector, double[] result) 
        {
            return MatrixHandler.VectorSubtraction(vector, MatrixHandler.MultiplyMatrix(matrix, result));
        }
    }
}
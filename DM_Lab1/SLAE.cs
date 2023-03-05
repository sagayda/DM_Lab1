namespace DM_Labs
{
    public static class SLAE
    {
        public static void Solve(double[,] mainMatrix, double[] vector)
        {
            Console.WriteLine("\t--Solution by Gaussian method");
            GaussianMethodStraightCourse(mainMatrix, vector);
        }
        private static void GaussianMethodStraightCourse(double[,] mainMatrix, double[] vector)
        {
            if (mainMatrix.GetLength(1) != vector.Length)
            {
                Console.WriteLine("\t--Matrix and vector have different size");
                return;
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

            double[] res = GaussianMethodReverseCourse(mainMatrix, vector);
            MatrixHandler.PrintMatrix(res, "\t--Result:");
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
                    matrix[x, y] = Math.Round(matrix[x, y], 12);
                }
                vector[x] -= rowCoeff / upRowCoeff * vector[i];
                vector[x] = Math.Round(vector[x], 12);
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
            double[] result = new double[vector.Length];

            for (int x = vector.Length - 1; x >= 0; x--)
            {
                double mainCoeff = mainMatrix[x, x];
                result[x] = vector[x];

                if (x == vector.Length - 1)
                {
                    result[x] = vector[x] / mainMatrix[x, x];
                    result[x] = Math.Round(result[x], 12);
                    continue;
                }

                for (int y = x + 1; y < mainMatrix.GetLength(1); y++)
                {
                    result[x] -= mainMatrix[x, y] * result[y];
                }

                result[x] = Math.Round(result[x], 12);
            }
            return result;
        }
    }
}

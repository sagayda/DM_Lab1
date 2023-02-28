namespace DM_Labs
{
    public static class SLAE
    {
        public static void Start(double[,] mainMatrix, double[] vector)
        {
            if (MatrixHandler.IsSymmetric(mainMatrix))
            {
                Console.WriteLine("\t--Solution by the square root method");
                SquareRootMethod(mainMatrix, vector);
            }
            else
            {
                Console.WriteLine("\t--Solution by Gaussian method");
                GaussianMethod(mainMatrix, vector);
            }
        }
        public static double[,] Factorization(double[,] inputMatrix)
        {
            int xLenght = inputMatrix.GetLength(0);
            int yLenght = inputMatrix.GetLength(1);

            double[,] tMatrix = new double[xLenght, yLenght];
            tMatrix[0, 0] = Math.Sqrt(inputMatrix[0, 0]);

            for (int y = 0; y < xLenght; y++)
            {
                for (int x = 0; x < yLenght; x++)
                {
                    if (y == 0 && x == 0)
                        continue;

                    if (y > 0) //set zero elems
                    {
                        if (x < y)
                        {
                            tMatrix[x, y] = 0;
                            continue;
                        }
                    }

                    if (y == 0) //set firs row
                    {
                        tMatrix[x, y] = inputMatrix[x, y] / tMatrix[0, 0];
                        continue;
                    }

                    if (y == x) //set main diagonal
                    {
                        double sigmaRes = MatrixHandler.DegreeSigma(y, tMatrix.GetLength(1) - 1, tMatrix);
                        Console.WriteLine(sigmaRes);
                        if (inputMatrix[x, y] - sigmaRes >= 0)
                        {
                            tMatrix[x, y] = Math.Sqrt(inputMatrix[x, y] - sigmaRes);
                        }
                        else
                        {
                            tMatrix[x, y] = Math.Sqrt(Math.Abs(inputMatrix[x, y] - sigmaRes)) * -1;
                        }
                        continue;
                    }

                    if (y < x)
                    {
                        tMatrix[x, y] = (inputMatrix[x, y] - MatrixHandler.MultiplySigma(y - 1, 0, tMatrix, x)) / tMatrix[y, y];
                        continue;
                    }
                }
            }

            return tMatrix;
        }
        private static void Step(double[,] matrix, double[] vector, int i)
        {
            double firsElementInRowCoeff = matrix[i, i];
            for (int x = i; x < matrix.GetLength(0); x++)
            {
                matrix[x, i] /= firsElementInRowCoeff;
            }
            vector[i] /= firsElementInRowCoeff;

            if (i + 1 >= matrix.GetLength(1) || i + 1 >= matrix.GetLength(0))
            {
                MatrixHandler.PrintMatrix(matrix, vector, $"\t--Final step");
                return;
            }

            double upRowCoeff = matrix[i, i];
            for (int y = i + 1; y < matrix.GetLength(1); y++)
            {
                double rowCoeff = matrix[i, y];
                for (int x = i; x < matrix.GetLength(0); x++)
                {
                    matrix[x, y] -= rowCoeff / upRowCoeff * matrix[x, i];
                    if (x == i && matrix[x, y] < 0.0000000000001)
                        matrix[x, y] = 0;
                }
                vector[y] -= rowCoeff / upRowCoeff * vector[i];

            }

            MatrixHandler.PrintMatrix(matrix, vector, $"\t--Step {i + 1}");
            return;
        }
        private static void GaussianMethod(double[,] mainMatrix, double[] vector)
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

                for (int y = i; y < mainMatrix.GetLength(1); y++)
                {
                    if (Math.Abs(mainMatrix[i, y]) > Math.Abs(maxCoefficient))
                    {
                        maxCoefficientIndex = y;
                        maxCoefficient = mainMatrix[i, y];
                    }
                }

                if (maxCoefficientIndex != i)
                {
                    mainMatrix = MatrixHandler.RearrangeRows(mainMatrix, i, maxCoefficientIndex);
                    vector = MatrixHandler.RearrangeRows(vector, i, maxCoefficientIndex);
                    MatrixHandler.PrintMatrix(mainMatrix, vector, $"\t-- {i + 1} row rearranged to {maxCoefficientIndex + 1}");
                }

                Step(mainMatrix, vector, i);
            }

            double[] res = GMReverseCourseUp(mainMatrix, vector);
            MatrixHandler.PrintMatrix(res, "\t--Result:");
        }
        private static double[] GMReverseCourseUp(double[,] mainMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int y = vector.Length - 1; y >= 0; y--)
            {
                if (y == vector.Length - 1)
                {
                    result[y] = vector[y] / mainMatrix[y, mainMatrix.GetLength(1) - 1];
                    continue;
                }

                result[y] = vector[y];
                for (int x = y + 1; x < mainMatrix.GetLength(0); x++)
                {
                    result[y] -= mainMatrix[x, y] * result[x];
                }
                result[y] = result[y] / mainMatrix[y, y];
            }

            return result;

        }
        private static double[] GMReverseCourseDown(double[,] mainMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                if (i == 0)
                {
                    result[i] = vector[i] / mainMatrix[i, 0];
                    continue;
                }

                result[i] = vector[i];
                for (int j = 0; j < i; j++)
                {
                    result[i] = result[i] - mainMatrix[i, j] * result[j];
                }
                result[i] = result[i] / mainMatrix[i, i];
            }

            return result;

        }
        private static void SquareRootMethod(double[,] mainMatrix, double[] vector)
        {
            QEStraightCourse(mainMatrix, vector);
        }
        private static void QEStraightCourse(double[,] mainMatrix, double[] vector)
        {
            double[,] tMatrix = Factorization(mainMatrix);
            double[,] transTMatrix = MatrixHandler.Transponate(tMatrix);

            MatrixHandler.PrintMatrix(tMatrix, "\tT matrix");
            MatrixHandler.PrintMatrix(transTMatrix, "\t--T Transponated matrix");

            double[] yVector = GMReverseCourseDown(transTMatrix, vector);
            MatrixHandler.PrintMatrix(yVector, "\t--Y vector");
            double[] xVector = GMReverseCourseUp(tMatrix, yVector);
            MatrixHandler.PrintMatrix(xVector, "\tResult");
        }
    }
}

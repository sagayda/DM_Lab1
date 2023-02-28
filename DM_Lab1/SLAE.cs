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

            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLenght; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (i > 0) //set zero elems
                    {
                        if (j < i)
                        {
                            tMatrix[i, j] = 0;
                            continue;
                        }
                    }

                    if (i == 0) //set firs row
                    {
                        tMatrix[i, j] = inputMatrix[i, j] / tMatrix[0, 0];
                        continue;
                    }

                    if (i == j) //set main diagonal
                    {
                        double sigmaRes = MatrixHandler.DegreeSigma(0, i - 1, tMatrix);
                        if (inputMatrix[i, j] - sigmaRes >= 0)
                        {
                            tMatrix[i, j] = Math.Sqrt(inputMatrix[i, j] - sigmaRes);
                        }
                        else
                        {
                            tMatrix[i, j] = Math.Sqrt(Math.Abs(inputMatrix[i, j] - sigmaRes)) * -1;
                        }
                        continue;
                    }

                    if (i < j)
                    {
                        tMatrix[i, j] = (inputMatrix[i, j] - MatrixHandler.MultiplySigma(i - 1, 0, tMatrix, j)) / tMatrix[i, i];
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

            double[] yVector = GMReverseCourseDown(transTMatrix, vector);
            double[] xVector = GMReverseCourseUp(tMatrix, yVector);

            MatrixHandler.PrintMatrix(xVector);
        }
    }
}

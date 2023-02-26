namespace DM_Labs
{
    public static class SLAE
    {
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

        public static void Start(double[,] mainMatrix, double[] vector)
        {
            if (mainMatrix.GetLength(0) == mainMatrix.GetLength(1))
            {
                QuadraticEquationsSolve(mainMatrix, vector);
                return;
            }
            else
            {

                return;
            }
        }

        private static void GaussianMethod(double[,] mainMatrix, double[] vector)
        {
            if (mainMatrix.GetLength(1) != vector.Length)
            {
                Console.WriteLine("Cant solve by Gaussian method");
                return;
            }



        }

        private static double[] GMReverseCourseUp(double[,] mainMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int i = vector.Length - 1; i >= 0; i--)
            {
                if (i == vector.Length - 1)
                {
                    result[i] = vector[i] / mainMatrix[i, mainMatrix.GetLength(1) - 1];
                    continue;
                }

                result[i] = vector[i];
                for (int j = i + 1; j < mainMatrix.GetLength(0); j++)
                {
                    result[i] = result[i] - mainMatrix[i, j] * result[j];
                }
                result[i] = result[i] / mainMatrix[i, i];
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

        private static void QuadraticEquationsSolve(double[,] mainMatrix, double[] vector)
        {

            QEStraightCourse(mainMatrix, vector);

        }

        private static void QEStraightCourse(double[,] mainMatrix, double[] vector)
        {
            double[,] tMatrix = Factorization(mainMatrix);
            double[,] transTMatrix = MatrixHandler.Transponate(tMatrix);

            MatrixHandler.PrintMatrix(tMatrix);
            Console.WriteLine("___________________________");
            MatrixHandler.PrintMatrix(vector);
            Console.WriteLine("___________________________");


            double[] yVector = GMReverseCourseUp(tMatrix, vector);
            MatrixHandler.PrintMatrix(yVector);
        }


    }
}

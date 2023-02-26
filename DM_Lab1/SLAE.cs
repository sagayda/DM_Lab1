namespace DM_Labs
{
    public static class SLAE
    {
        public static void Solve(double[,] mainMatrix, double[] vector)
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

        private static void QuadraticEquationsSolve(double[,] mainMatrix, double[] vector)
        {

            StraightCourse(mainMatrix);

        }

        private static void StraightCourse(double[,] mainMatrix)
        {
            int xLenght = mainMatrix.GetLength(0);
            int yLenght = mainMatrix.GetLength(1);

            double[,] tMatrix = new double[xLenght, yLenght];
            tMatrix[0, 0] = Math.Sqrt(mainMatrix[0, 0]);

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
                        tMatrix[i, j] = mainMatrix[i, j] / tMatrix[0, 0];
                        continue;
                    }

                    if (i == j) //set main diagonal
                    {
                        double sigmaRes = MatrixHandler.DegreeSigma(0, i - 1, tMatrix);
                        if (mainMatrix[i, j] - sigmaRes >= 0)
                        {
                            tMatrix[i, j] = Math.Sqrt(mainMatrix[i, j] - sigmaRes);
                        }
                        else
                        {
                            tMatrix[i, j] = Math.Sqrt(Math.Abs(mainMatrix[i, j] - sigmaRes)) * -1;
                        }
                        continue;
                    }

                    if (i < j)
                    {
                        tMatrix[i, j] = (mainMatrix[i, j] - MatrixHandler.MultiplySigma(i - 1, 0, tMatrix, j)) / tMatrix[i, i];
                        continue;
                    }

                }

            }
            MatrixHandler.PrintMatrix(tMatrix);
        }
    }
}

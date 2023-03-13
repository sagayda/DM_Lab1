namespace DM_lab_3
{
    internal class SeidelMethod
    {
        private static bool CanStopIterations(double[] vector, double[] previousVector, double precision, double norm)
        {
            double temp = 0;
            for (int j = 0; j < vector.Length; j++)
            {
                temp = Math.Abs(vector[j] - previousVector[j]);
                if ((norm / (1 - norm)) * temp > precision)
                {
                    Console.WriteLine($"{(norm / (1 - norm)) * temp} !< {precision}, iterations continued");
                    return false;
                }
            }
            Console.WriteLine($"{(norm / (1 - norm)) * temp} < {precision}, iterations stopped");
            return true;
        }
        public static double[] StartIterations(double[,] matrix, double[] vector, double precision, double norm)
        {
            double[] result = new double[vector.Length];
            double[] previousResul = new double[vector.Length];

            //iterations
            for (int i = 1;; i++)
            {
                previousResul = (double[])result.Clone();
                //for each x
                for (int j = 0; j < result.Length; j++)
                {
                    double tempRes = vector[j];
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        if (j == k)
                            continue;
                        tempRes -= matrix[j, k] * result[k];
                    }
                    result[j] = tempRes;
                }

                MatrixHandler.PrintMatrix(result, $"\t--Iteration {i}");

                bool tempBool = true;
                int a = 1;

                if (CanStopIterations(result, previousResul, precision, norm))
                    return result;
            }
        }
        public static double[] Solve(double[,] introducedMatrix, double[] introducedVector, double precision)
        {
            double[] result = new double[introducedVector.Length];

            //main diagonal to 1
            for (int i = 0; i < introducedMatrix.GetLongLength(0); i++)
            {
                double divider = introducedMatrix[i, i];
                for (int j = 0; j < introducedMatrix.GetLongLength(1); j++)
                {
                    introducedMatrix[i, j] /= divider;
                }
                introducedVector[i] /= divider;
            }
            MatrixHandler.PrintMatrix(introducedMatrix, introducedVector, "\t--Main diagonal to 1");

            //convergence condition
            bool[] diagonalDominationByRows = new bool[introducedVector.Length];

            double norm = 0;
            for (int i = 0; i < introducedMatrix.GetLongLength(0); i++)
            {
                double elem = 0;
                for (int j = 0; j < introducedMatrix.GetLongLength(1); j++)
                {
                    if (i == j)
                        continue;

                    elem += Math.Abs(introducedMatrix[i, j]);
                }
                diagonalDominationByRows[i] = Math.Abs(introducedMatrix[i, i]) - elem > 0;
                if(norm < elem)
                    norm = elem;
            }

            bool diagonalDomination = true;
            for (int i = 0; i < diagonalDominationByRows.Length; i++)
            {
                if (!diagonalDominationByRows[i])
                {
                    Console.WriteLine($"Row {i + 1} is not diagonally dominant");
                    diagonalDomination = false;
                }
            }

            if (!diagonalDomination)
            {
                Console.WriteLine("Matrix dont have convergence condition");
                return result;
            }

            //get Beta matrix
            //double[,] betaMatrix = (double[,])introducedMatrix.Clone();
            //for (int i = 0; i < betaMatrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < betaMatrix.GetLength(1); j++)
            //    {
            //        if (i == j)
            //        {
            //            betaMatrix[i, j] = 0;
            //            continue;
            //        }

            //        betaMatrix[i, j] = betaMatrix[i, j] * -1;
            //    }
            //}
            //MatrixHandler.PrintMatrix(betaMatrix, introducedVector, "\t--Beta matrix");

            return StartIterations(introducedMatrix, introducedVector, precision, norm);
           
        }

    }
}

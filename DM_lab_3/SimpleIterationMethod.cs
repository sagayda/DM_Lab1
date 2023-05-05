namespace DM_lab_3
{
    public class SimpleIterationMethod
    {
        private static Matrix StartIterations(Matrix vector, Matrix betaMatrix, double norm, double precision, Matrix matrix)
        {
            Matrix result = vector.Clone();
            Matrix previousResult = vector.Clone();

            result.PrintUnrounded($"================================\n\t--Iteration {1}\nResult:");
            (vector - betaMatrix * result).PrintUnrounded($"Vector of unconnectedness:");

            for (int i = 2; ; i++)
            {
                previousResult = result.Clone();
                result = betaMatrix * result + vector;
                result.PrintUnrounded($"================================\n\t--Iteration {i}\nResult:");
                (vector - matrix * result).PrintUnrounded($"Vector of unconnectedness:");


                if (CanStopIterations(result, previousResult, norm, precision))
                    break;
            }
            return result;
        }
        private static bool CanStopIterations(Matrix vector, Matrix previousVector, double norm, double precision)
        {
            Console.WriteLine("Check for stopping iterations:");
            bool res = true;
            double temp = 0;
            for (int j = 0; j < vector.XLength; j++)
            {
                temp = Math.Abs(vector[j] - previousVector[j]);
                if ((norm / (1 - norm)) * temp > precision)
                {
                    Console.WriteLine($"{(norm / (1 - norm)) * temp} !< {precision}");
                    res = false;
                }
                else
                {
                    Console.WriteLine($"{(norm / (1 - norm)) * temp} < {precision}");
                }
            }

            if (res)
            {
                Console.WriteLine("\tIterations stopped");
                return true;
            }

            Console.WriteLine("\tIterations contunied");
            return false;
        }
        private static bool IsHaveConvergenceConditionByRow(Matrix matrix, out double norm)
        {
            norm = 0;
            bool[] convergenceVector = new bool[matrix.XLength];
            for (int i = 0; i < matrix.XLength; i++)
            {
                double temp = 0;
                for (int j = 0; j < matrix.YLength; j++)
                {
                    temp += Math.Abs(matrix[i, j]);
                }

                if (temp > norm)
                    norm = temp;

                convergenceVector[i] = temp < 1;

            }

            foreach (var item in convergenceVector)
                if (!item)
                    return false;

            return true;

        }
        private static bool IsHaveConvergenceConditionByColumn(Matrix matrix, out double norm)
        {
            norm = 0;
            bool[] convergenceVector = new bool[matrix.YLength];
            for (int j = 0; j < matrix.YLength; j++)
            {
                double temp = 0;
                for (int i = 0; i < matrix.XLength; i++)
                {
                    temp += Math.Abs(matrix[i, j]);
                }

                if (temp > norm)
                    norm = temp;

                convergenceVector[j] = temp < 1;
            }

            foreach (var item in convergenceVector)
                if (!item)
                    return false;

            return true;
        }
        private static bool IsHaveConvergenceConditionByEuclid(Matrix matrix, out double norm)
        {
            norm = 0;
            double res = 0;
            for (int i = 0; i < matrix.XLength; i++)
            {
                for (int j = 0; j < matrix.YLength; j++)
                {
                    res += matrix[i, j] * matrix[i, j];
                }
            }
            norm = res;
            return res < 1;
        }
        public static Matrix Solve(Matrix introducedMatrix, Matrix introducedVector, double precision)
        {
            Console.WriteLine("\n\t\tSimple iteration method\n\n================================");

            //main diagonal to 1
            for (int i = 0; i < introducedMatrix.XLength; i++)
            {
                double divider = introducedMatrix[i, i];
                for (int j = 0; j < introducedMatrix.YLength; j++)
                {
                    introducedMatrix[i, j] /= divider;
                }
                introducedVector[i] /= divider;
            }
            introducedMatrix.Print(introducedVector, "\t--Main diagonal to 1");

            //get Beta matrix
            Matrix betaMatrix = introducedMatrix.Clone();
            for (int i = 0; i < betaMatrix.XLength; i++)
            {
                for (int j = 0; j < betaMatrix.YLength; j++)
                {
                    if (i == j)
                    {
                        betaMatrix[i, j] = 0;
                        continue;
                    }

                    betaMatrix[i, j] = betaMatrix[i, j] * -1;
                }
            }
            betaMatrix.Print("Beta matrix");

            //convergence condition
            double norm;
            Matrix result = new Matrix(introducedVector.XLength);

            if (IsHaveConvergenceConditionByRow(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by row: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision, introducedMatrix);
                return result;
            }

            if (IsHaveConvergenceConditionByColumn(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by Column: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision, introducedMatrix);
                return result;
            }

            if (IsHaveConvergenceConditionByEuclid(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by Euclid: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision, introducedMatrix);
                return result;
            }

            if (norm > 1)
                Console.WriteLine("\n\tNO NORM IS VALID");

            return result;
        }
    }

}

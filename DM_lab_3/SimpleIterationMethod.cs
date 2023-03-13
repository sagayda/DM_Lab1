namespace DM_lab_3
{
    public class SimpleIterationMethod
    {
        private static double[] StartIterations(double[] vector, double[,] betaMatrix, double norm, double precision)
        {
            double[] result = (double[])vector.Clone();
            double[] previousResult = (double[])vector.Clone();

            MatrixHandler.PrintMatrix(result, $"\t--Interation {1}");
            for (int i = 2; ; i++)
            {
                previousResult = (double[])result.Clone();
                result = MatrixHandler.SumVector(MatrixHandler.MultiplyMatrix(betaMatrix, result), vector);
                MatrixHandler.PrintMatrix(result, $"\t--Interation {i}");

                if (CanStopIterations(result, previousResult, norm, precision))
                    break;
            }
            return result;
        }
        private static bool CanStopIterations(double[] vector, double[] previousVector, double norm, double precision)
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
        private static bool IsHaveConvergenceConditionByRow(double[,] matrix, out double norm)
        {
            norm = 0;
            bool[] convergenceVector = new bool[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double temp = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
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
        private static bool IsHaveConvergenceConditionByColumn(double[,] matrix, out double norm)
        {
            norm = 0;
            bool[] convergenceVector = new bool[matrix.GetLength(1)];
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                double temp = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
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
        private static bool IsHaveConvergenceConditionByEuclid(double[,] matrix, out double norm)
        {
            norm = 0;
            double res = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    res += matrix[i, j] * matrix[i, j];
                }
            }
            norm = res;
            return res < 1;
        }
        public static double[] Solve(double[,] introducedMatrix, double[] introducedVector, double precision)
        {
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

            //get Beta matrix
            double[,] betaMatrix = (double[,])introducedMatrix.Clone();
            for (int i = 0; i < betaMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < betaMatrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        betaMatrix[i, j] = 0;
                        continue;
                    }

                    betaMatrix[i, j] = betaMatrix[i, j] * -1;
                }
            }
            MatrixHandler.PrintMatrix(betaMatrix, introducedVector, "\t--Beta matrix");

            //convergence condition
            double norm;
            double[] result = new double[introducedVector.Length];

            if (IsHaveConvergenceConditionByRow(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by row: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision);
                return result;
            }

            if (IsHaveConvergenceConditionByColumn(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by Column: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision);
                return result;
            }

            if (IsHaveConvergenceConditionByEuclid(betaMatrix, out norm))
            {
                Console.WriteLine($"Got norm by Euclid: {norm}");
                result = StartIterations(introducedVector, betaMatrix, norm, precision);
                return result;
            }

            if(norm > 1)
                Console.WriteLine("\n\tNO NORM IS VALID");

            return result;
        }
    }
}

namespace DM_lab_3
{
    internal class SeidelMethod
    {
        private static bool CanStopIterations(Matrix vector, Matrix previousVector, double precision, double norm)
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

            if(res)
            {
                Console.WriteLine("\tIterations stopped");
                return true;
            }

            Console.WriteLine("\tIterations contunied");
            return false;
        }
        public static Matrix StartIterations(Matrix matrix, Matrix vector, double precision, double norm)
        {
            Matrix result = new Matrix(vector.XLength);
            Matrix previousResul = new Matrix(vector.XLength);

            //iterations
            for (int i = 1; ; i++)
            {
                previousResul = result.Clone();
                //for each x
                for (int j = 0; j < result.XLength; j++)
                {
                    double tempRes = vector[j];
                    for (int k = 0; k < matrix.YLength; k++)
                    {
                        if (j == k)
                            continue;
                        tempRes -= matrix[j, k] * result[k];
                    }
                    result[j] = tempRes;
                }

                result.PrintUnrounded($"================================\n\t--Iteration {i}\nResult:");
                (vector - matrix * result).PrintUnrounded($"Vector of unconnectedness:");
                if (CanStopIterations(result, previousResul, precision, norm))
                    return result;
            }
        }
        public static Matrix Solve(Matrix introducedMatrix, Matrix introducedVector, double precision)
        {
            Console.WriteLine("\t\tSeidel method");

            Matrix result = new Matrix(introducedVector.XLength);

            //main diagonal to 1
            for (int i = 0; i < introducedMatrix.XLength; i++)
            {
                double divider = introducedMatrix[i, i];
                for (int j = 0; j < introducedMatrix.XLength; j++)
                {
                    introducedMatrix[i, j] /= divider;
                }
                introducedVector[i] /= divider;
            }

            //convergence condition
            bool[] diagonalDominationByRows = new bool[introducedVector.XLength];

            double norm = 0;
            for (int i = 0; i < introducedMatrix.XLength; i++)
            {
                double elem = 0;
                for (int j = 0; j < introducedMatrix.YLength; j++)
                {
                    if (i == j)
                        continue;

                    elem += Math.Abs(introducedMatrix[i, j]);
                }
                diagonalDominationByRows[i] = Math.Abs(introducedMatrix[i, i]) - elem > 0;
                if (norm < elem)
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

            return StartIterations(introducedMatrix, introducedVector, precision, norm);

        }

    }

}

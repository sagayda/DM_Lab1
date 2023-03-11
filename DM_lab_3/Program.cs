namespace DM_lab_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] introducedMatrix;
            double[] introducedVector;
            while (true)
            {
                while (!MatrixHandler.GetMatrixFromFile(out introducedMatrix));
                while (!MatrixHandler.GetVectorFromFIle(out introducedVector));
                if (introducedMatrix?.GetLength(0) == introducedVector?.Length)
                    break;
                Console.WriteLine("Matrix size != vector size");
            }

            MatrixHandler.PrintMatrix(introducedMatrix, "Got matrix:");
            MatrixHandler.PrintMatrix(introducedVector, "Got vector:");

            double[,] ass;
            if (MatrixHandler.TryGetDiagonalAdvantage(introducedMatrix, out ass))
            {
                MatrixHandler.PrintMatrix(ass, "result: ");
            }
        }
    }
}
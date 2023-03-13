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
                while (!MatrixHandler.GetMatrixFromFile(out introducedMatrix)) ;
                while (!MatrixHandler.GetVectorFromFIle(out introducedVector)) ;
                if (introducedMatrix?.GetLength(0) == introducedVector?.Length)
                    break;
                Console.WriteLine("Matrix size != vector size");
            }

            Console.WriteLine("Enter desired precision:");
            double precision = double.Parse(Console.ReadLine());

            MatrixHandler.PrintMatrix(introducedMatrix, "Got matrix:");
            MatrixHandler.PrintMatrix(introducedVector, "Got vector:");

            MatrixHandler.PrintMatrix(SimpleIterationMethod.Solve(introducedMatrix, introducedVector, precision), "\tRESULT");
        }
    }
}
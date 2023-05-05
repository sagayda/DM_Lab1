namespace DM_Labs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set path to folder with matrices: (matrix.txt and vector.txt)");

            string path = Console.ReadLine();
            string mainMatrixPath = path + @"\matrix.txt";
            string vectorPath = path + @"\vector.txt";

            double[,] mainMatrix = MatrixHandler.GetMatrixFromFile(mainMatrixPath);
            double[] vector = MatrixHandler.GetVectorFromFIle(vectorPath);

            var res = SLAE.Solve(mainMatrix, vector);

            MatrixHandler.PrintMatrix(res,"res");
        }
    }
}
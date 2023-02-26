namespace DM_Labs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set path to folder with matrices: (system_matrix.txt and right_vector.txt)");

            string path = Console.ReadLine();
            string mainMatrixPath = path + @"\system_matrix.txt";
            string vectorPath = path + @"\right_vector.txt";

            double[,] mainMatrix = MatrixHandler.GetMatrixFromFile(mainMatrixPath);
            double[] vector = MatrixHandler.GetVectorFromFIle(vectorPath);

            SLAE.Start(mainMatrix, vector);
        }
    }
}
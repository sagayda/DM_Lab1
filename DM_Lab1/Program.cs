﻿namespace DM_Labs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set path to folder with matrices: (system_matrix.txt and right_vector.txt)");

            string path = Console.ReadLine();
            string mainMatrixPath = path + @"\matrix.txt";
            string vectorPath = path + @"\vector.txt";

            double[,] mainMatrix = MatrixHandler.GetMatrixFromFile(mainMatrixPath);
            double[] vector = MatrixHandler.GetVectorFromFIle(vectorPath);

            SLAE.Solve(mainMatrix, vector);
        }
    }
}
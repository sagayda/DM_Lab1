﻿namespace DM_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set path to folder with matrices: (system_matrix.txt and right_vector.txt)");

            string path = Console.ReadLine();
            string mainMatrixPath = path + @"\system_matrix.txt";
            string vectorPath = path + @"\right_vector.txt";

            float[,] mainMatrix = MatrixHandler.GetMatrixFromFile(mainMatrixPath);

            MatrixHandler.PrintMatrix(mainMatrix);

        }
    }
}
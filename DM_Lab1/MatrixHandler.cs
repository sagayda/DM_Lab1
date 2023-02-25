using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_Lab2
{
    public static class MatrixHandler
    {
        public static void PrintMatrix(float[,] matrix)
        {
            Console.WriteLine($"Size: {matrix.GetLength(0)} * {matrix.GetLength(1)}\n");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                Console.WriteLine();
            }
        }
        public static void PrintMatrix(float[] matrix)
        {
            Console.WriteLine($"Size: {matrix.Length}\n");

            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write($"{matrix[i]}, \n");
            }
        }
        
        public static float[,] GetMatrixFromFile(string path) 
        {
            List<string> tempList = new List<string>();

            tempList = File.ReadAllLines(path).ToList();

            int yLenght = tempList[0].Split(" ").Length;
            int xLength = tempList.Count;

            float[,] matrix = new float[xLength, yLenght];

            for (int i = 0; i < xLength; i++)
            {
                var temp = tempList[i].Split(" ");
                for (int j = 0; j < yLenght; j++)
                {
                    if (temp.Length > yLenght)
                    {
                        Console.WriteLine("Bad matrix");
                        break;
                    }
                    matrix[i, j] = float.Parse(temp[j]);
                }
            }

            return matrix;
        }
        public static float[] GetVectorFromFIle(string path)
        {
            List<string> tempList = new List<string>();
            tempList = File.ReadAllLines(path).ToList();
            float[] vector = new float[tempList.Count];

            for (int i = 0; i < tempList.Count; i++)
            {
                vector[i] = float.Parse(tempList[i]);
            }

            return vector;
        }

    }
}

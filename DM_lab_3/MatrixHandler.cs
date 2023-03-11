using System.Data.Common;
using System.Net.Security;

namespace DM_lab_3
{
    public static class MatrixHandler
    {
        public static double[] MultiplyMatrix(double[,] matrix, double[] vector)
        {
            if (matrix.GetLength(1) != vector.Length)
                return null;
            
            double[] result = new double[vector.Length];

            for (int i = 0; i < result.Length; i++)
            {
                double temp = 0;
                for (int k = 0; k < vector.Length; k++)
                {
                    temp += matrix[i, k] * vector[k];
                }
                result[i] = temp;
            }
            return result;
        }
        public static double[] VectorSubtraction(double[] vectorA, double[] VectorB)
        {
            if (vectorA.Length != VectorB.Length)
                return null;

            double[] result = new double[vectorA.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = vectorA[i] - VectorB[i];
            }
            return result;
        }
        public static double[,] RearrangeRows(double[,] matrix, int fromRowIndex, int toRowIndex)
        {
            double[] row = new double[matrix.GetLength(0)];

            for (int y = 0; y < row.Length; y++)
                row[y] = matrix[toRowIndex, y];

            for (int y = 0; y < matrix.GetLength(0); y++)
                matrix[toRowIndex, y] = matrix[fromRowIndex, y];

            for (int y = 0; y < matrix.GetLength(0); y++)
                matrix[fromRowIndex, y] = row[y];

            return matrix;
        }
        public static double[] RearrangeRows(double[] vector, int fromRowIndex, int toRowIndex)
        {
            double temp = vector[toRowIndex];
            vector[toRowIndex] = vector[fromRowIndex];
            vector[fromRowIndex] = temp;

            return vector;
        }
        public static void PrintMatrix(double[,] matrix, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Size: {matrix.GetLength(0)} * {matrix.GetLength(1)}");
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write($"{Math.Round(matrix[x, y], 3)}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("###############################");
        }
        public static void PrintMatrix(double[] vector, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Size: {vector.Length}");

            for (int y = 0; y < vector.Length; y++)
                Console.Write($"{vector[y]}, \n");

            Console.WriteLine("###############################");
        }
        public static void PrintMatrix(double[,] matrix, double[] vector, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Size: {matrix.GetLength(0)} * {matrix.GetLength(1)}");
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write($"{Math.Round(matrix[x, y], 3)}, ");
                }
                Console.Write($"\t| {Math.Round(vector[x], 3)}, ");
                Console.WriteLine();
            }
            Console.WriteLine("###############################");
        }
        public static bool GetMatrixFromFile(out double[,] matrix)
        {
            Console.WriteLine("Set path to matrix:");
            string path = Console.ReadLine();
            matrix = null;

            if (!File.Exists(path))
            {
                Console.WriteLine("Matrix file doesnt exist");
                return false;
            }

            List<string> tempList = new List<string>();
            tempList = File.ReadAllLines(path).ToList();
            int xCount = tempList.Count;
            if (xCount <= 0)
            {
                Console.WriteLine("Invalid matrix!");
                return false;
            }

            int yCount = tempList[0].Split(" ").Length;
            if (yCount <= 0)
            {
                Console.WriteLine("Invalid matrix!");
                return false;
            }

            if (xCount != yCount)
            {
                Console.WriteLine("Мatrix is not square!");
                return false;
            }

            double[,] array = new double[xCount, yCount];

            try
            {
                for (int i = 0; i < xCount; i++)
                {
                    var temp = tempList[i].Split(" ");

                    if (temp.Length != yCount)
                    {
                        Console.WriteLine("Мatrix is not square!");
                        return false;
                    }

                    for (int j = 0; j < temp.Length; j++)
                    {
                        array[i, j] = double.Parse(temp[j]);
                    }
                }
                matrix = array;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool GetVectorFromFIle(out double[] vector)
        {
            Console.WriteLine("Set path to vector:");
            string path = Console.ReadLine();
            vector = null;

            if (!File.Exists(path))
            {
                Console.WriteLine("Matrix file doesnt exist");
                return false;
            }

            List<string> tempList = new List<string>();
            tempList = File.ReadAllLines(path).ToList();
            int length = tempList.Count;

            if (length <= 0)
            {
                Console.WriteLine("Invalid matrix!");
                return false;
            }

            double[] array = new double[tempList.Count];

            try
            {
                for (int x = 0; x < tempList.Count; x++)
                    array[x] = double.Parse(tempList[x]);

                vector = array;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

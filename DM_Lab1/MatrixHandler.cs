namespace DM_Labs
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
        public static double[,] GetMatrixFromFile(string path)
        {
            List<string> tempList = new List<string>();

            tempList = File.ReadAllLines(path).ToList();

            int yLenght = tempList[0].Split(" ").Length;
            int xLength = tempList.Count;

            double[,] matrix = new double[xLength, yLenght];

            for (int x = 0; x < xLength; x++)
            {
                var temp = tempList[x].Split(" ");
                for (int y = 0; y < yLenght; y++)
                {
                    if (temp.Length > yLenght)
                    {
                        Console.WriteLine("Bad matrix");
                        break;
                    }
                    matrix[x, y] = double.Parse(temp[y]);
                }
            }
            PrintMatrix(matrix, "\t--Got matrix: ");
            return matrix;
        }
        public static double[] GetVectorFromFIle(string path)
        {
            List<string> tempList = new List<string>();
            tempList = File.ReadAllLines(path).ToList();
            double[] vector = new double[tempList.Count];

            for (int x = 0; x < tempList.Count; x++)
                vector[x] = double.Parse(tempList[x]);

            PrintMatrix(vector, "\t--Got vector: ");
            return vector;
        }
    }
}

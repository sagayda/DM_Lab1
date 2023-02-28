namespace DM_Labs
{
    public static class MatrixHandler
    {
        public static double[,] RearrangeRows(double[,] matrix, int fromRowIndex, int toRowIndex)
        {
            double[] row = new double[matrix.GetLength(0)];

            for (int x = 0; x < row.Length; x++)
                row[x] = matrix[x, toRowIndex];

            for (int x = 0; x < matrix.GetLength(0); x++)
                matrix[x, toRowIndex] = matrix[x, fromRowIndex];

            for (int x = 0; x < matrix.GetLength(0); x++)
                matrix[x, fromRowIndex] = row[x];

            return matrix;
        }
        public static double[] RearrangeRows(double[] vector, int fromRowIndex, int toRowIndex)
        {
            double temp = vector[toRowIndex];
            vector[toRowIndex] = vector[fromRowIndex];
            vector[fromRowIndex] = temp;

            return vector;
        }
        public static double[,] Transponate(double[,] matrix)
        {
            double[,] resMatrix = new double[matrix.GetLength(1), matrix.GetLength(0)];

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    resMatrix[x, y] = matrix[y, x];
                }
            }

            return resMatrix;
        }
        public static bool IsIdentical(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
                return false;

            for (int y = 0; y < matrix1.GetLength(0); y++)
            {
                for (int x = 0; x < matrix1.GetLength(1); x++)
                {
                    if (matrix1[x,y] != matrix2[x,y])
                        return false;
                }
            }

            return true;
        }
        public static bool IsSymmetric(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;

            for (int y = 0; y < matrix.GetLength(0); y++)
                for (int x = 0; x < matrix.GetLength(1); x++)
                    if (matrix[y, x] != matrix[x, y])
                        return false;

            return true;
        }
        public static double DegreeSigma(int i, int k, double[,] matrix)
        {
            double sum = 0;

            for (; i <= k; i++)
                sum += Math.Pow(matrix[i, k], 2);

            return sum;
        }
        public static double MultiplySigma(int i, int k, double[,] matrix, int j)
        {
            double sum = 0;

            for (; i <= k; i++)
                sum += matrix[i, k + 1] * matrix[i, j];

            return sum;
        }
        public static void PrintMatrix(double[,] matrix)
        {
            Console.WriteLine($"Size: {matrix.GetLength(0)} * {matrix.GetLength(1)}");
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    Console.Write($"{matrix[x, y]}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("###############################");
        }
        public static void PrintMatrix(double[,] matrix, string message)
        {
            Console.WriteLine(message);
            PrintMatrix(matrix);
        }
        public static void PrintMatrix(double[,] matrix, double[] vector, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Size: {matrix.GetLength(0)} * {matrix.GetLength(1)}");
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    Console.Write($"{matrix[x, y]}, ");
                }
                Console.Write($"\t| {vector[y]}, ");
                Console.WriteLine();
            }
            Console.WriteLine("###############################");
        }
        public static void PrintMatrix(double[] vector)
        {
            Console.WriteLine($"Size: {vector.Length}");

            for (int x = 0; x < vector.Length; x++)
                Console.Write($"{vector[x]}, \n");

            Console.WriteLine("###############################");
        }
        public static void PrintMatrix(double[] matrix, string message)
        {
            Console.WriteLine(message);
            PrintMatrix(matrix);
        }
        public static double[,] GetMatrixFromFile(string path)
        {
            List<string> tempList = new List<string>();

            tempList = File.ReadAllLines(path).ToList();

            int yLenght = tempList[0].Split(" ").Length;
            int xLength = tempList.Count;

            double[,] matrix = new double[xLength, yLenght];

            for (int y = 0; y < xLength; y++)
            {
                var temp = tempList[y].Split(" ");
                for (int x = 0; x < yLenght; x++)
                {
                    if (temp.Length > yLenght)
                    {
                        Console.WriteLine("Bad matrix");
                        break;
                    }
                    matrix[x, y] = double.Parse(temp[x]);
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

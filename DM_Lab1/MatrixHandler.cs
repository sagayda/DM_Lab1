namespace DM_Labs
{
    public static class MatrixHandler
    {
        public static double[,] RearrangeRows(double[,] matrix, int fromRowIndex, int toRowIndex)
        {
            double[] row = new double[matrix.GetLength(0)];

            for (int i = 0; i < row.Length; i++)
                row[i] = matrix[i, toRowIndex];

            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[i, toRowIndex] = matrix[i, fromRowIndex];

            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[i, fromRowIndex] = row[i];

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

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    resMatrix[j, i] = matrix[i, j];
                }
            }

            return resMatrix;
        }
        public static bool IsIdentical(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
                return false;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i,j] != matrix2[i,j])
                        return false;
                }
            }

            return true;
        }
        public static bool IsSymmetric(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] != matrix[j, i])
                        return false;

            return true;
        }
        public static double DegreeSigma(int i, int k, double[,] matrix)
        {
            double sum = 0;

            for (; i <= k; i++)
                sum += Math.Pow(matrix[i, k + 1], 2);

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
        public static void PrintMatrix(double[] matrix)
        {
            Console.WriteLine($"Size: {matrix.Length}");

            for (int i = 0; i < matrix.Length; i++)
                Console.Write($"{matrix[i]}, \n");

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

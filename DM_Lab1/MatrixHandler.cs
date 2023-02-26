namespace DM_Labs
{
    public static class MatrixHandler
    {
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
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------");
        }
        public static void PrintMatrix(double[] matrix)
        {
            Console.WriteLine($"Size: {matrix.Length}");

            for (int i = 0; i < matrix.Length; i++)
                Console.Write($"{matrix[i]}, \n");

            Console.WriteLine("----------------------------------");
        }

        public static double[,] GetMatrixFromFile(string path)
        {
            List<string> tempList = new List<string>();

            tempList = File.ReadAllLines(path).ToList();

            int yLenght = tempList[0].Split(" ").Length;
            int xLength = tempList.Count;

            double[,] matrix = new double[xLength, yLenght];

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
                    matrix[i, j] = double.Parse(temp[j]);
                }
            }

            return matrix;
        }
        public static double[] GetVectorFromFIle(string path)
        {
            List<string> tempList = new List<string>();
            tempList = File.ReadAllLines(path).ToList();
            double[] vector = new double[tempList.Count];

            for (int i = 0; i < tempList.Count; i++)
                vector[i] = double.Parse(tempList[i]);

            return vector;
        }

    }
}

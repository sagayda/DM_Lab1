namespace DM_Labs
{
    public static class MatrixHandler
    {
        public static double DegreeSigma(int i, int k, double[,] matrix)
        {
            double sum = 0;

            for (; i <= k; i++)
            {
                sum += Math.Pow(matrix[i, k + 1], 2);
            }

            return sum;
        }
        public static double MultiplySigma(int i, int k, double[,] matrix, int j)
        {
            double sum = 0;

            for (; i <= k; i++)
            {
                sum += matrix[i, k + 1] * matrix[i, j];
            }

            return sum;
        }
        public static void PrintMatrix(double[,] matrix)
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
        public static void PrintMatrix(double[] matrix)
        {
            Console.WriteLine($"Size: {matrix.Length}\n");

            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write($"{matrix[i]}, \n");
            }
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
            {
                vector[i] = double.Parse(tempList[i]);
            }

            return vector;
        }

    }
}

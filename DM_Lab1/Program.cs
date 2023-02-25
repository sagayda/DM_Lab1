namespace DM_Lab1
{
    internal class Program
    {
        public static void PrintMatrix(float[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Set path to folder with matrices: (system_matrix.txt and right_vector.txt)");

            string path = Console.ReadLine();
            string mainMatrixPath = path + @"\system_matrix.txt";
            string vectorPath = path + @"\right_vector.txt";

            List<string> tempList = new List<string>();

            tempList = File.ReadAllLines(mainMatrixPath).ToList();

            int xLenght = tempList[0].Split(" ").Length;
            int yLength = tempList.Count;

            float[,] mainMatrix = new float[yLength,xLenght];

            Console.WriteLine($"Matrix size: {xLenght} * {yLength}\n");

            for (int i = 0; i < yLength; i++)
            {
                var temp = tempList[i].Split(" ");
                for (int j = 0; j < xLenght; j++)
                {
                    if (temp.Length > xLenght)
                    {
                        Console.WriteLine("Bad matrix");
                        break;
                    }
                    mainMatrix[i,j] = float.Parse(temp[j]);
                }
            }
        }
    }
}
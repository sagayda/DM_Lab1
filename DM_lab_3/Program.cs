using System.Security.Cryptography.X509Certificates;

namespace DM_lab_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix introducedMatrix;
            Matrix introducedVector;
            while (true)
            {
                while (!Matrix.GetMatrixFromFile(out introducedMatrix)) ;
                while (!Matrix.GetMatrixFromFile(out introducedVector)) ;
                if (introducedMatrix?.XLength == introducedVector?.XLength)
                    break;
                Console.WriteLine("Matrix size != vector size");
            }

            Console.WriteLine("Enter desired precision:");
            double precision = double.Parse(Console.ReadLine());

            Console.WriteLine("\nAd hoc:");
            Console.WriteLine("\t\nRow 1 + row 2");
            Console.WriteLine("Full Gaussian straight course\n");
            Console.WriteLine("================================");
            for (int i = 0; i < introducedMatrix.XLength; i++)
            {
                introducedMatrix[1, i] += introducedMatrix[2, i];
            }
            introducedVector[1] += introducedVector[2];

            for (int i = 0; i < introducedMatrix.XLength-1; i++)
            {
                double upperRowCoeff = introducedMatrix[i, i];
                for (int x = i+1; x < introducedMatrix.XLength; x++)
                {
                    double thisRowCoeff = introducedMatrix[x, i];
                    for (int y = 0; y < introducedMatrix.YLength; y++)
                    {
                        introducedMatrix[x, y] -= thisRowCoeff / upperRowCoeff * introducedMatrix[i, y];
                    }
                    introducedVector[x] -= thisRowCoeff / upperRowCoeff * introducedVector[i];
                }
            }

            introducedMatrix.Print(introducedVector,"Got: ");

            SeidelMethod.Solve(introducedMatrix, introducedVector, precision).PrintUnrounded("\t\nRESULT OF SEIDEL METHOD:");
            SimpleIterationMethod.Solve(introducedMatrix, introducedVector, precision).PrintUnrounded("\t\nRESULT OF SIMPLE ITERATIONS METHOD:");
        }
    }
}
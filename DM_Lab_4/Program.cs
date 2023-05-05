namespace DM_Lab_4
{
    public class Matrix
    {
        private double[,] matrix;
        public int XLength => matrix.GetLength(0);
        public int YLength => matrix.GetLength(1);
        public bool isVector => XLength == 1 || YLength == 1;
        public double this[int x, int y]
        {
            get => matrix[x, y];
            set => matrix[x, y] = value;
        }
        public double this[int x]
        {
            get => XLength == 1 ? matrix[0, x] : matrix[x, 0];
            set
            {
                if (XLength == 1)
                {
                    matrix[0, x] = value;
                    return;
                }
                matrix[x, 0] = value;
            }
        }
        public Matrix()
        {
            matrix = new double[3, 3];
        }
        public Matrix(int x)
        {
            if (x <= 0)
                matrix = new double[3, 1];

            matrix = new double[x, 1];
        }
        public Matrix(int x, int y)
        {
            if (x <= 0 || y <= 0)
                matrix = new double[3, 3];

            matrix = new double[x, y];
        }
        public Matrix(double[,] array)
        {
            matrix = array;
        }
        public Matrix(double[] array)
        {
            matrix = new double[array.Length, 1];
            for (int i = 0; i < array.Length; i++)
            {
                matrix[i, 0] = array[i];
            }
        }
        public Matrix Clone()
        {
            return new Matrix((double[,])matrix.Clone());
        }
        public void Print()
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write($"{matrix[x, y]:0.00}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("================================");
        }
        public void Print(string message)
        {
            Console.WriteLine($"{message}");
            Print();
        }
        public void Print(Matrix vector, string message)
        {
            Console.WriteLine($"{message}");
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write($"{matrix[x, y]:0.00}  ");
                }
                Console.Write($"  |  {vector[x]:0.00}");
                Console.WriteLine();
            }
            Console.WriteLine("================================");
        }
        public void PrintUnrounded(string message)
        {
            Console.WriteLine($"{message}");
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write($"{matrix[x, y]:0.00000}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("================================");
        }
        public void RearrangeRow(int index1, int index2)
        {
            double[] row = new double[XLength];

            for (int y = 0; y < row.Length; y++)
                row[y] = matrix[index2, y];

            for (int y = 0; y < XLength; y++)
                matrix[index2, y] = matrix[index1, y];

            for (int y = 0; y < XLength; y++)
                matrix[index1, y] = row[y];
        }
        public static Matrix GetIdentityMatrix(int length)
        {
            Matrix Rmatrix = new Matrix(length, length);
            for (int i = 0; i < length; i++)
                Rmatrix[i, i] = 1;

            return Rmatrix;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.YLength != matrix2.XLength)
                return new Matrix();

            double[,] result = new double[matrix1.XLength, matrix2.YLength];

            for (int x = 0; x < matrix1.XLength; x++)
            {
                for (int y = 0; y < matrix2.YLength; y++)
                {
                    double sum = 0;
                    for (int i = 0; i < matrix2.XLength; i++)
                    {
                        sum += matrix1[x, i] * matrix2[i, y];
                    }
                    result[x, y] = sum;
                }
            }

            return new Matrix(result);
        }
        public static Matrix operator *(double num, Matrix matrix)
        {
            double[,] result = new double[matrix.XLength, matrix.YLength];

            for (int i = 0; i < matrix.XLength; i++)
            {
                for (int j = 0; j < matrix.YLength; j++)
                {
                    result[i, j] = matrix[i,j] * num;
                }
            }

            return new Matrix(result);
        }
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.YLength != matrix2.YLength || matrix1.XLength != matrix2.XLength)
                return new Matrix();

            double[,] result = new double[matrix1.XLength, matrix1.YLength];
            for (int i = 0; i < matrix1.XLength; i++)
            {
                for (int j = 0; j < matrix1.YLength; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return new Matrix(result);
        }
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.YLength != matrix2.YLength || matrix1.XLength != matrix2.XLength)
                return new Matrix();

            double[,] result = new double[matrix1.XLength, matrix1.YLength];
            for (int i = 0; i < matrix1.XLength; i++)
            {
                for (int j = 0; j < matrix1.YLength; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return new Matrix(result);
        }
        public static bool GetMatrixFromFile(out Matrix matrix)
        {
            {
                Console.WriteLine("Set path to matrix:");
                string path = Console.ReadLine();
                matrix = null;

                if (path == "m")
                {
                    path = @"c:matrix\DM_Lab4\matrix.txt";
                }

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

                double[,] array = new double[xCount, yCount];

                try
                {
                    for (int i = 0; i < xCount; i++)
                    {
                        var temp = tempList[i].Split(" ");

                        for (int j = 0; j < temp.Length; j++)
                        {
                            array[i, j] = double.Parse(temp[j]);
                        }
                    }
                    matrix = new Matrix(array);
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


    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix;
            Matrix.GetMatrixFromFile(out matrix);
            var res = FrobeniusNormalForm.StartReduction(matrix);
        }
    }

    public static class FrobeniusNormalForm
    {
        public static Matrix StartReduction(Matrix matrix)
        {
            int length = matrix.XLength;
            Matrix matrixA = matrix.Clone();
            List<Matrix> mMatices = new List<Matrix>();

            for (int k = length - 1; k > 0; k--)
            {
                Matrix matrixM = Matrix.GetIdentityMatrix(length);
                Matrix matrixRM = Matrix.GetIdentityMatrix(length);

                for (int j = 0; j < length; j++)
                {
                    if (j == k - 1)
                        matrixM[k - 1, j] = 1 / matrixA[k, k - 1];
                    else
                        matrixM[k - 1, j] = matrixA[k, j] / -matrixA[k, k - 1];

                    matrixRM[k - 1, j] = matrixA[k, j];
                }
                matrixA = matrixRM * matrixA * matrixM;
                matrixM.Print($"\t--M {k}");
                mMatices.Add( matrixM);
                matrixRM.Print($"\t--MR {k}");

                if(k == 1)
                    matrixA.PrintUnrounded($"\t--A {length - k}");
                else
                    matrixA.Print($"\t--A {length - k}");

            }
            Console.WriteLine($"\nGot equation:\ny^4 + y^3 * {matrixA[0, 0]:0.00000} + y^2 * {matrixA[0, 1]:0.00000} + y * {matrixA[0, 2]:0.00000} + {matrixA[0, 3]:0.00000}");

            var properNumbers = SolveEquation(matrixA);
            properNumbers.Print("Proper numbers derived from MathCad");
            var properVectorsP = GetPropperVectors(properNumbers);

            Console.WriteLine("\t--Proper vectors P");
            for (int i = 0; i < properVectorsP.Length; i++)
            {
                properVectorsP[i].PrintUnrounded($"\t\ty{i + 1}");
            }

            Matrix matrixS = mMatices[0] * mMatices[1] * mMatices[2];
            matrixS.Print("\t--Matrix S");
            Console.WriteLine("\t--Proper vectors A");
            for (int i = 0; i < properVectorsP.Length; i++)
            {
                (matrixS * properVectorsP[i]).PrintUnrounded($"\t\tx{i+1}");
            }

            Console.WriteLine("\nValidation\n");

            for (int i = 0; i < 4; i++)
            {
                (matrix * (matrixS * properVectorsP[i])).PrintUnrounded($"A * x{i+1}:");
                (properNumbers[i] * (matrixS * properVectorsP[i])).PrintUnrounded($"Y{i+1} * x{i+1}:");
                Console.WriteLine();
            }

            return matrixA;
        }
        public static Matrix[] GetPropperVectors(Matrix matrix)
        {
            List<Matrix> vectors = new List<Matrix>();

            for (int i = 0; i < matrix.XLength; i++)
            {
                double temp = matrix[i];

                Matrix vector = new Matrix(4);
                vector[3] = 1;
                for (int j = 0; j < 3; j++)
                {
                    vector[j] = Math.Pow(matrix[i], 3 - j);
                }
                vectors.Add(vector);
            }

            return vectors.ToArray();
        }
        public static Matrix SolveEquation(Matrix matrix)
        {
            Matrix propperNumbers = new Matrix(4);
            propperNumbers[0] = 2.58652;
            propperNumbers[1] = 4.15777;
            propperNumbers[2] = 5.82107;
            propperNumbers[3] = 9.39464;
            return propperNumbers;
        }
    }
}
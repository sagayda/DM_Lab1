﻿namespace DM_lab_3
{
    public class Matrix
    {
        private double[,] matrix;
        public int XLength => matrix.GetLength(0);
        public int YLength => matrix.GetLength(1);
        public bool isVector => XLength == 1 || YLength == 1;
        public double this[int x, int y] 
        { 
            get => matrix [x, y]; 
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
                matrix[x,0] = value;
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
                matrix[i,0] = array[i];
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
                    Console.Write($"{matrix[x, y]:0.000000000000000}  ");
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
                    path = @"c:matrix\DM_Lab3\matrix.txt";
                }

                if (path == "v")
                {
                    path = @"c:matrix\DM_Lab3\vector.txt";
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

                //if (xCount != yCount)
                //{
                //    Console.WriteLine("Мatrix is not square!");
                //    return false;
                //}

                double[,] array = new double[xCount, yCount];

                try
                {
                    for (int i = 0; i < xCount; i++)
                    {
                        var temp = tempList[i].Split(" ");

                        //if (temp.Length != yCount)
                        //{
                        //    Console.WriteLine("Мatrix is not square!");
                        //    return false;
                        //}

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

}

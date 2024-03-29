﻿using System.Numerics;

namespace DM_Labs
{
    public static class SLAE
    {
        public static double[] Solve(double[,] mainMatrix, double[] vector)
        {
            var res = GaussianMethodStraightCourse(mainMatrix, vector);

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Math.Round(res[i], 6);
            }

            return res;
        }
        private static double[] GaussianMethodStraightCourse(double[,] mainMatrix, double[] vector)
        {
            if (mainMatrix.GetLength(1) != vector.Length)
            {
                return null;
            }
             
            for (int i = 0; i < mainMatrix.GetLength(0); i++)
            {
                int maxCoefficientIndex = i;
                double maxCoefficient = mainMatrix[i, i];

                for (int x = i; x < mainMatrix.GetLength(0); x++)
                {
                    if (Math.Abs(mainMatrix[x, i]) > Math.Abs(maxCoefficient) && vector[x] != 0)
                    {
                        maxCoefficientIndex = x;
                        maxCoefficient = mainMatrix[x, i];
                    }
                }

                if (maxCoefficientIndex != i)
                {
                    mainMatrix = MatrixHandler.RearrangeRows(mainMatrix, i, maxCoefficientIndex);
                    vector = MatrixHandler.RearrangeRows(vector, i, maxCoefficientIndex);
                }

                GaussianMethodStep(mainMatrix, vector, i);
            }

            MainDiagonalToOne(mainMatrix, vector);

            return GaussianMethodReverseCourse(mainMatrix, vector);
        }
        private static void GaussianMethodStep(double[,] matrix, double[] vector, int i)
        {
            if (i + 1 >= matrix.GetLength(1) || i + 1 >= matrix.GetLength(0))
            {
                return;
            }

            double upRowCoeff = matrix[i, i];
            for (int x = i + 1; x < matrix.GetLength(0); x++)
            {
                double rowCoeff = matrix[x, i];
                if (rowCoeff == 0)
                    continue;

                for (int y = i; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] -= rowCoeff / upRowCoeff * matrix[i, y];
                }
                vector[x] -= rowCoeff / upRowCoeff * vector[i];
            }

            return;
        }
        private static void MainDiagonalToOne(double[,] matrix, double[] vector)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                double mainCoeff = matrix[x, x];
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] /= mainCoeff;
                }
                vector[x] /= mainCoeff;
            }
        }
        private static double[] GaussianMethodReverseCourse(double[,] mainMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int x = vector.Length - 1; x >= 0; x--)
            {
                double mainCoeff = mainMatrix[x, x];
                result[x] = vector[x];


                if (x == vector.Length - 1)
                {
                    result[x] = vector[x] / mainMatrix[x, x];
                    result[x] = Math.Round(result[x], 13);
                    continue;
                }

                for (int y = x + 1; y < mainMatrix.GetLength(1); y++)
                {
                    result[x] -= mainMatrix[x, y] * result[y];
                }

                result[x] = Math.Round(result[x], 13);
            }

            return result;
        }
        private static double[] DiscontinuityVector(double[,] matrix, double[] vector, double[] result) 
        {
            return MatrixHandler.VectorSubtraction(vector, MatrixHandler.MultiplyMatrix(matrix, result));
        }
    }
}
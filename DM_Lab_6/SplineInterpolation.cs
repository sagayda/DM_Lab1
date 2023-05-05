using DM_Labs;

namespace DM_Lab_6
{
    public class SplineInterpolation
    {
        public string[] Formulas { get; private set; }

        public Func<double, double>[] Funcs { get; private set; }

        public List<double> xValues { get; }

        public List<double> yValues { get; }

        public bool IsBuilded { get; private set; } = false;

        public bool[] IsFormulasBuilded { get; private set; }

        public double[] bCoefs => coefs.AsSpan(bStartIndx, equationCount).ToArray();
        public double[] cCoefs => coefs.AsSpan(cStartIndx, equationCount).ToArray();
        public double[] dCoefs => coefs.AsSpan(dStartIndx, equationCount).ToArray(); 

        private double[] coefs;
        private readonly int equationCount;
        private readonly int bStartIndx;
        private readonly int cStartIndx;
        private readonly int dStartIndx;


        public SplineInterpolation(List<double> xList, List<double> yList)
        {
            if (xList == null || yList == null)
                throw new ArgumentNullException();

            if (xList.Count < 2 || yList.Count < 2)
                throw new ArgumentException();

            if (xList.Count != yList.Count)
                throw new ArgumentException();

            xValues = xList;
            yValues = yList;

            equationCount = xValues.Count - 1;

            IsFormulasBuilded = new bool[equationCount];
            Formulas = new string[equationCount];

            bStartIndx = 0;
            cStartIndx = bStartIndx + equationCount;
            dStartIndx = cStartIndx + equationCount;
        }

        public void Start()
        {
            //create h
            double[] h = new double[equationCount];
            for (int i = 0; i < equationCount; i++)
            {
                h[i] = xValues[i + 1] - xValues[i];
            }

            //create vector for SLAE solution
            double[] vector = new double[3 * equationCount];
            for (int i = 0; i < vector.Length; i++)
            {
                if(i < equationCount)
                {
                    vector[i] = yValues[i+1] - yValues[i];
                }
            }

            //creation equation matrix
            double[,] matrix = new double[3 * equationCount, 3 * equationCount];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i < equationCount)
                    {
                        if (j >= bStartIndx && j < cStartIndx)
                        {
                            if (j - bStartIndx == i)
                            {
                                matrix[i, j] = h[i];
                                continue;
                            }
                        }

                        if (j >= cStartIndx && j <= dStartIndx)
                        {
                            if (j - cStartIndx == i)
                            {
                                matrix[i, j] = Math.Pow(h[i], 2);
                                continue;
                            }
                        }

                        if (j >= dStartIndx)
                        {
                            if (j - dStartIndx == i)
                            {
                                matrix[i, j] = Math.Pow(h[i], 3);
                                continue;
                            }
                        }
                    }

                    if (i < equationCount + (equationCount - 1) && i >= equationCount)
                    {
                        if (j >= bStartIndx && j < cStartIndx)
                        {
                            if (j - bStartIndx == i - equationCount)
                            {
                                matrix[i, j] = -1;
                                continue;
                            }

                            if (j - bStartIndx == i - equationCount + 1)
                            {
                                matrix[i, j] = 1;
                                continue;
                            }
                        }

                        if (j >= cStartIndx && j <= dStartIndx)
                        {
                            if (j - cStartIndx == i - equationCount)
                            {
                                matrix[i, j] = -2 * h[i - equationCount];
                                continue;
                            }
                        }

                        if (j >= dStartIndx)
                        {
                            if (j - dStartIndx == i - equationCount)
                            {
                                matrix[i, j] = -3 * Math.Pow(h[i - equationCount], 2);
                                continue;
                            }
                        }
                    }

                    if (i < equationCount + (equationCount - 1) * 2 && i >= equationCount + (equationCount - 1))
                    {
                        if (j >= cStartIndx && j < dStartIndx)
                        {
                            if (j - cStartIndx == i - equationCount - (equationCount - 1))
                            {
                                matrix[i, j] = -1;
                                continue;
                            }

                            if (j - cStartIndx == i - equationCount - (equationCount - 1) + 1)
                            {
                                matrix[i, j] = 1;
                                continue;
                            }
                        }

                        if (j >= dStartIndx)
                        {
                            if (j - dStartIndx == i - equationCount - (equationCount - 1))
                            {
                                matrix[i, j] = -3 * h[i - equationCount - (equationCount - 1)];
                                continue;
                            }
                        }
                    }

                    if (i == matrix.GetLength(0) - 2)
                    {
                        matrix[i, cStartIndx] = 1;
                        continue;
                    }

                    if (i == matrix.GetLength(1) - 1)
                    {
                        matrix[i, dStartIndx - 1] = 1;
                        matrix[i, matrix.GetLength(1) - 1] = 3 * h[cStartIndx - 1];
                        continue;
                    }

                    matrix[i, j] = 0;
                }
            }

            //solving equations
            coefs = SLAE.Solve(matrix, vector);

            //creation funcs for equations
            //Func<double, double>[] funcs = new Func<double, double>[equationCount];
            Funcs = new Func<double, double>[equationCount];
            for (int i = 0; i < Funcs.Length; i++)
            {
                int k = i;
                double func(double x)
                {
                    if (!IsFormulasBuilded[k])
                        Formulas[k] = $"{Math.Round(yValues[k], 6)} + {Math.Round(coefs[bStartIndx + k], 6)}(x - {Math.Round(xValues[k], 6)}) + {Math.Round(coefs[cStartIndx + k], 6)}(x - {Math.Round(xValues[k], 6)})^2 + {Math.Round(coefs[dStartIndx + k], 6)}(x - {Math.Round(xValues[k], 6)})^3";

                    return yValues[k] + coefs[bStartIndx + k] * (x - xValues[k]) + coefs[cStartIndx + k] * Math.Pow((x - xValues[k]), 2) + coefs[dStartIndx + k] * Math.Pow((x - xValues[k]), 3);
                }
                Funcs[i] = func;
            }

            IsBuilded = true;
        }

    }
}

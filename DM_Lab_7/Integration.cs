using MathNet.Numerics;

namespace DM_Lab_7
{
    public class Integration
    {
        public Func<double, double> Func { get; private set; }

        public double A { get; private set; }

        public double B { get; private set; }

        public double LastIntegrationE { get; private set; } = -1;

        public Integration(Func<double, double> func)
        {
            Func = func;
        }

        public void SetFunc(Func<double, double> func)
        {
            Func = func;
        }

        public void SetIntegrationBorders(double a, double b)
        {
            A = a;
            B = b;
        }

        public double IntegrateViaTrapeziumMethod(double e)
        {
            if (A >= B)
            {
                throw new ArgumentException(null, nameof(A));
            }

            if (Func == null)
            {
                throw new ArgumentNullException(nameof(Func));
            }

            if (e <= 0)
            {
                throw new ArgumentException(null, nameof(e));
            }

            double step = GetStep(e) * 2;
            double[] x;
            double[] y;
            double n;
            bool isAccuracyAchieved = false;
            List<double> I = new();

            do
            {
                step /= 2;

                x = GetXValues(step);
                y = GetYValues(x);
                n = x.Length - 1;

                double sum = (y.First() + y.Last()) / 2;
                for (int i = 1; i < y.Length - 1; i++)
                    sum += y[i];

                I.Add((B - A) / n * sum);

                if (I.Count >= 2)
                {
                    double calculatedE = Math.Abs(I[^1] - I[^2]) / (Math.Pow(2, 2) - 1);
                    LastIntegrationE = calculatedE;
                    isAccuracyAchieved = e >= calculatedE;
                }

            } while (I.Count < 2 || !isAccuracyAchieved);

            Console.WriteLine($"[IntegrateViaTrapeziumMethod] Accuracy achieved with step = {step} (n = {n})");

            return I[^1] + (I[^1] - I[^2]) / (Math.Pow(2, 2) - 1);

            double GetStep(double e)
            {
                List<double> DerivativeValues = new();
                double step;
                int n = 0;
                double[] xValues;
                bool IsStepFound;

                Func<double, double> DerivativeFunc = Differentiate.DerivativeFunc(Func, 2);

                do
                {
                    n++;
                    step = Math.Round((B - A) / (n + 1), 6);
                    xValues = GetXValues(step);

                    DerivativeValues.Clear();
                    foreach (var value in xValues)
                        DerivativeValues.Add(Math.Abs(DerivativeFunc(value)));

                    double calculatedE = Math.Pow(B - A, 3) / (12 * Math.Pow(xValues.Length - 1, 2)) * DerivativeValues.Max();
                    IsStepFound = e >= calculatedE;
                } while (!IsStepFound);


                Console.WriteLine($"[IntegrateViaTrapeziumMethod] Start step = {step} (n = {n+1})");
                return step;
            }
        }

        public double IntegrateViaQuadratureFormula(double e)
        {
            if (A >= B)
            {
                throw new ArgumentException(null, nameof(A));
            }

            if (Func == null)
            {
                throw new ArgumentNullException(nameof(Func));
            }

            if (e <= 0)
            {
                throw new ArgumentException(null, nameof(e));
            }

            int n = GetN(e);
            Func<double, double> ReplacedFunc;

            if (A != -1 || B != 1)
            {
                double xFunc(double x) => ((A + B) / 2) + ((B - A) / 2) * x;
                double tFunc(double t) => (-A - B + 2 * t) / (B - A);

                ReplacedFunc = t => Func(xFunc(t)) * Differentiate.DerivativeFunc(xFunc, 1)(t);
            }
            else
            {
                ReplacedFunc = Func;
            }

            double res = 0;
            double[]? tValues = GetNodes(n);
            double[]? aValues = GetCoefficient(n);

            if (tValues == null || aValues == null)
            {
                throw new ArgumentException("The required accuracy cannot be achieved", nameof(e));
            }

            for (int i = 0; i < n; i++)
            {
                res += aValues[i] * ReplacedFunc(tValues[i]);
            }

            Console.WriteLine($"[IntegrateViaQuadratureFormula] Accuracy achieved with n = {n}");

            return res;

            double[]? GetCoefficient(int n)
            {
                switch (n)
                {
                    case 1:
                        return new double[] { 2 };
                    case 2:
                        return new double[] { 1, 1 };
                    case 3:
                        return new double[] { 0.555555, 0.888889, 0.555555 };
                    case 4:
                        return new double[] { 0.347855, 0.652145, 0.652145, 0.347855 };
                    case 5:
                        return new double[] { 0.236927, 0.478629, 0.568889, 0.478629, 0.236927 };
                    case 6:
                        return new double[] { 0.171324, 0.360761, 0.467914, 0.467914, 0.360761, 0.171324 };
                    case 7:
                        return new double[] { 0.129485, 0.279705, 0.381830, 0.417960, 0.381830, 0.279705, 0.129485 };
                    case 8:
                        return new double[] { 0.101228, 0.222381, 0.313707, 0.362684, 0.362684, 0.313707, 0.222381, 0.101228 };

                    default:
                        return null;
                }
            }

            double[]? GetNodes(int n)
            {
                switch (n)
                {
                    case 1:
                        return new double[] { 0.5 };
                    case 2:
                        return new double[] { -0.57735, 0.57735 };
                    case 3:
                        return new double[] { -0.774997, 0, 0.774997 };
                    case 4:
                        return new double[] { -0.861136, -0.339981, 0.339981, 0.861136 };
                    case 5:
                        return new double[] { -0.906180, -0.538470, 0, 0.538470, 0.906180 };
                    case 6:
                        return new double[] { -0.932470, -0.661210, -0.238620, 0.238620, 0.661210, 0.932470 };
                    case 7:
                        return new double[] { -0.949108, -0.741531, -0.405845, 0, 0.405845, 0.741531, 0.949108 };
                    case 8:
                        return new double[] { -0.960290, -0.796666, -0.525532, -0.183434, 0.183434, 0.525532, 0.796666, 0.960290 };

                    default:
                        return null;
                }
            }

            int GetN(double e)
            {
                int n = 0;
                List<double> DerivativeValues = new();
                double step;
                double[] xValues;
                bool IsNFound;

                do
                {
                    n++;
                    step = (B - A) / (n + 1);
                    xValues = GetXValues(step);

                    var DerivativeFunc = Differentiate.DerivativeFunc(Func, 2 * n);

                    DerivativeValues.Clear();
                    foreach (var value in xValues)
                        DerivativeValues.Add(Math.Abs(DerivativeFunc(value)));

                    double calculatedE = Math.Pow(Factorial(n), 4) * Math.Pow(B - A, 2 * n + 1) / ((2 * n + 1) * Math.Pow(Factorial(2 * n), 3)) * DerivativeValues.Max();
                    LastIntegrationE = calculatedE;
                    IsNFound = calculatedE <= e;
                } while (!IsNFound);

                return n;

                double Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (p, item) => p * item);
            }
        }

        private double[] GetXValues(double step)
        {
            List<double> result = new();

            double x = A;
            while (x < B)
            {
                result.Add(x);
                x = Math.Round(x + step, 6);
            }

            result.Add(B);

            return result.ToArray();
        }

        private double[] GetYValues(double[] xValues)
        {
            double[] result = new double[xValues.Length];

            for (int i = 0; i < xValues.Length; i++)
            {
                result[i] = Func(xValues[i]);
            }

            return result;
        }
    }
}

using MathNet.Numerics;

namespace DM_Lab_5
{
    public static class Polynomial
    {
        public static double BisectionMethod(Func<double, double> f, double A, double B, double e)
        {
            Console.WriteLine("Bisection method:");

            double a = A;
            double b = B;

            int k = 0;

            double x;
            bool isFuncNegative = f(A) * f(B) < 0;

            while (Math.Abs(b - a) >= e)
            {
                k++;
                x = (a + b) / 2;

                if (f(x) == 0)
                    return x;

                if (isFuncNegative)
                {
                    if (f(a) * f(x) < 0)
                        b = x;
                    else
                        a = x;
                }
                else
                {
                    if (f(a) * f(x) > 0)
                        b = x;
                    else
                        a = x;
                }


                Console.WriteLine($"\t Iteration {k}:");
                Console.WriteLine($"\t\tx{k} = {x:0.000000}");
            }

            Console.WriteLine($"\tIterations stopped {Math.Abs(b - a):0.000000} < {e:0.00000}");
            return (a + b) / 2;
        }

        public static double ChordMethod(Func<double, double> f, double A, double B, double e)
        {
            Console.WriteLine("Chord method:");

            var f1 = Differentiate.DerivativeFunc(f, 1);
            var f2 = Differentiate.DerivativeFunc(f, 2);

            int k = 0;

            double x0;
            double x;
            double xPrev = 0;

            if (f1(A) * f2(A) < 0)
            {
                x0 = A;
                x = B;
            }
            else
            {
                x0 = B;
                x = A;
            }

            while (Math.Abs(x - xPrev) > e)
            {
                k++;
                xPrev = x;
                x -= (f(x) / (f(x) - f(x0))) * (x - x0);

                Console.WriteLine($"\t Iteration {k}:");
                Console.WriteLine($"\t\tx{k} = {x:0.000000}");
            }

            Console.WriteLine($"\tIterations stopped {(x - xPrev):0.000000} < {e:0.00000}");
            return x;
        }

        public static double NewtonsMethod(Func<double, double> f, double A, double B, double e)
        {
            Console.WriteLine("Newtons method:");

            var f1 = Differentiate.DerivativeFunc(f, 1);
            var f2 = Differentiate.DerivativeFunc(f, 2);

            int k = 0;

            double x;
            double xPrev = 0;

            if (f1(A) * f2(A) < 0)
            {
                x = A;
            }
            else
            {
                x = B;
            }

            while (Math.Abs(x - xPrev) > e)
            {
                k++;
                xPrev = x;
                x -= f(x) / f1(x);

                Console.WriteLine($"\t Iteration {k}:");
                Console.WriteLine($"\t\tx{k} = {x:0.000000}");
            }

            Console.WriteLine($"\tIterations stopped {Math.Abs(x - xPrev):0.000000} < {e:0.00000}");
            return x;
        }
    }
}

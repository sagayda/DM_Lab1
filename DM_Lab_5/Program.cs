using MathNet.Numerics.Differentiation;
using MathNet.Numerics;

namespace DM_Lab_5
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Func<double, double> f = x => 2 * Math.Pow(x, 3) - 4 * Math.Pow(x, 2) - x + 3;

            Console.WriteLine($"\t-1 < x1 < -0.5\n");
            double a = -1;
            double b = -0.5;

            Polynomial.BisectionMethod(f, a, b, 0.00001f);
            Console.WriteLine("=============================");
            Polynomial.ChordMethod(f, a, b, 0.00001f);
            Console.WriteLine("=============================");
            Polynomial.NewtonsMethod(f, a, b, 0.00001f);
            Console.WriteLine("=============================");


            Console.WriteLine($"\n\n\t1.5 < x3 < 2\n");
            a = 1.5;
            b = 2;

            Polynomial.BisectionMethod(f, a, b, 0.00001f);
            Console.WriteLine("=============================");
            Polynomial.ChordMethod(f, a, b, 0.00001f);
            Console.WriteLine("=============================");
            Polynomial.NewtonsMethod(f, a, b, 0.00001f);
        }
    }
}
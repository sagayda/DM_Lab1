namespace DM_Lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> func = (double x) =>
            {
                double res = Math.Sin((3f / 2f) * x) * Math.Pow(3f * x, 1f / 3f);
                return Math.Round(res,6);
            };

            List<double> x = new List<double>() { 0, 2, 4, 6, 8 };
            List<double> y = new List<double>() { 0, 0.256432, -0.639702, 1.080056, -1.547744 };

            InterpolationByNewtons newtonsInterpolation = new InterpolationByNewtons(x, y);
            newtonsInterpolation.Start();
            newtonsInterpolation.Func(0);
            Console.WriteLine("Interpolation By Newtons");
            Console.WriteLine($"\tP(x) = {newtonsInterpolation.Formula}");
            
            Console.WriteLine();
            Console.WriteLine($"\tf(0) = {func(0f)}\n\tP(0) = {newtonsInterpolation.Func(0f)}");
            Console.WriteLine($"\tf(2) = {func(2f)}\n\tP(2) = {newtonsInterpolation.Func(2f)}");
            Console.WriteLine($"\tf(4) = {func(4f)}\n\tP(4) = {newtonsInterpolation.Func(4f)}");
            Console.WriteLine($"\tf(6) = {func(6f)}\n\tP(6) = {newtonsInterpolation.Func(6f)}");
            Console.WriteLine($"\tf(8) = {func(8f)}\n\tP(8) = {newtonsInterpolation.Func(8f)}");


            SplineInterpolation splineInterpolation = new SplineInterpolation(x, y);
            splineInterpolation.Start();

            foreach (var tempFunc in splineInterpolation.Funcs)
            {
                tempFunc(0);
            }

            Console.WriteLine("\n\nSpline interpolation");
            for (int i = 0; i < splineInterpolation.Formulas.Length; i++)
            {
                Console.WriteLine($"\tS{i+1}(x) = {splineInterpolation.Formulas[i]}");
            }


            Console.WriteLine();
            Console.Write("\t");
            for (int i = 0; i < splineInterpolation.bCoefs.Length; i++)
            {
                Console.Write($"b{i+1}={splineInterpolation.bCoefs[i]}\t");
            }
            Console.Write("\n\t");
            for (int i = 0; i < splineInterpolation.cCoefs.Length; i++)
            {
                Console.Write($"c{i+1}={splineInterpolation.cCoefs[i]}\t");
            }
            Console.Write("\n\t");
            for (int i = 0; i < splineInterpolation.dCoefs.Length; i++)
            {
                Console.Write($"d{i + 1}={splineInterpolation.dCoefs[i]}\t");
            }


            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < splineInterpolation.Funcs.Length; i++)
            {
                Console.WriteLine($"\tf({x[i]}) = {func(x[i])}");
                Console.WriteLine($"\tS{i + 1}({x[i]}) = {splineInterpolation.Funcs[i](x[i])}");
                Console.WriteLine($"\tf({x[i+1]}) = {func(x[i+1])}");
                Console.WriteLine($"\tS{i + 1}({x[i+1]}) = {splineInterpolation.Funcs[i](x[i+1])}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Похибка");
            List<double> e = new List<double>();
            double k = 0;
            while (k<=8)
            {
                Console.WriteLine($"P({k}) = {newtonsInterpolation.Func(k)}");
                Console.WriteLine($"\te = {Math.Abs(newtonsInterpolation.Func(k) - func(k))}");
                e.Add(Math.Abs(newtonsInterpolation.Func(k) - func(k)));
                k += 0.25;
            }

            Console.WriteLine($"\neMax = {e.Max()}");
        }
    }
}
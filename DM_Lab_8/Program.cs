namespace DM_Lab_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double, double> func = (x, y) => 0.25 * Math.Pow(y, 2) + Math.Pow(x, 2);
            double a = 0;
            double b = 0.5;
            double h = 0.1;
            double yAtZero = -1;

            //Func<double, double, double> func = (x, y) => (x - y) / 2;
            //double a = 0;
            //double b = 3;
            //double h = 0.25;
            //double yAtZero = 1;

            //Func<double, double, double> func = (x, y) => 1 + 2 * y - Math.Pow(x, 2);
            //double a = 0;
            //double b = 1;
            //double h = 0.1;
            //double yAtZero = 1;

            CauchyProblemSolution cauchyProblemSolution = new(func, a, b);
            cauchyProblemSolution.RungeKuttaMethod(h, yAtZero);
        }
    }
}
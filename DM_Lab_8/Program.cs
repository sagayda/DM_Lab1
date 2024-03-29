﻿namespace DM_Lab_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double, double> func = (x, y) => Math.Pow(Math.E, -1.4 * x) * (Math.Pow(y, 2) + 1.4);
            double a = 0;
            double b = 1;
            double e = 0.01;
            double yAtZero = 0;

            CauchyProblemSolution cauchyProblemSolution = new(func, a, b);
            cauchyProblemSolution.PrintCalculationsRungeKuttaMethod(yAtZero, e);
            cauchyProblemSolution.PrintCalculationsAdamsMethod(yAtZero, e);
        }
    }
}
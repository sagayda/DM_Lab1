namespace DM_Lab_8
{
    public class CauchyProblemSolution
    {
        public Func<double, double, double> Equation { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public CauchyProblemSolution(Func<double, double, double> equation, double a, double b)
        {
            if (equation == null)
                throw new ArgumentNullException(nameof(equation));

            Equation = equation;
            A = a;
            B = b;
        }

        public void RungeKuttaMethod(double startStep, double yAtZero)
        {
            List<Dictionary<string, List<double>>> table = new();


            double h = startStep;

            //steps cycle
            int i = 0;
            while (i <= 4)
            {
                table.Add(GetTableRow());
                var row = table[i];

                //setting 4 rows for table row
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            row["y"].Add(yAtZero);
                            row["x"].Add(0);
                            row["y'"].Add(Equation(row["x"][j], row["y"][j]));
                            row["k"].Add(row["y'"][j]);
                            continue;
                        }

                        row["x"].Add(i * h);
                        row["y"].Add(table[i - 1]["y"][0] + table[i - 1]["deltaY"][0]);
                        row["y'"].Add(Equation(row["x"][j], row["y"][j]));
                        row["k"].Add(row["y'"][j]);
                        continue;
                    }

                    //setting x
                    if (j == 3)
                    {
                        row["x"].Add(row["x"][0] + h);
                    }
                    else
                    {
                        row["x"].Add(row["x"][0] + h / 2);
                    }

                    //setting y
                    if(j == 3)
                    {
                        row["y"].Add(row["y"][0] + h * row["k"][j - 1]);
                    }
                    else
                    {
                        row["y"].Add(row["y"][0] + h * row["k"][j - 1] / 2);
                    }


                    row["y'"].Add(Equation(row["x"][j], row["y"][j]));
                    row["k"].Add(row["y'"][j]);

                    if (j == 3)
                    {
                        row["deltaY"].Add(h / 6f * (row["k"][0] + 2 * row["k"][1] + 2 * row["k"][2] + row["k"][3]));
                    }

                }

                i++;
            }


            double[] xValues = SplitSpan(A, B, startStep);
            double temp = Equation(0, yAtZero);
            double k0 = h * temp;
        }

        private double[] SplitSpan(double start, double end, double step)
        {
            List<double> result = new();

            for (double i = start; i <= end; i += step)
            {
                result.Add(i);
            }

            if (result.Last() != end)
            {
                result.Add(end);
            }

            return result.ToArray();
        }

        private Dictionary<string, List<double>> GetTableRow()
        {
            Dictionary<string, List<double>> result = new()
            {
                { "x", new() },
                { "y", new() },
                { "y'", new() },
                { "k", new() },
                { "deltaY", new() }
            };

            return result;
        }
    }
}

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

        public double RungeKuttaMethod(double startStep, double yAtZero, out List<Dictionary<string, List<double>>> calculationsTable)
        {
            calculationsTable = new();

            double h = startStep;

            //steps cycle
            int i = 0;
            while (i < (B-A) / h)
            {
                calculationsTable.Add(CreateNewTableRow());
                var row = calculationsTable[i];

                //setting 4 rows for table row
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            row["y"].Add(yAtZero);
                            row["x"].Add(0);
                            row["k"].Add(Equation(row["x"][j], row["y"][j]));
                            continue;
                        }

                        row["x"].Add(i * h);
                        row["y"].Add(calculationsTable[i - 1]["y"][0] + calculationsTable[i - 1]["deltaY"][0]);
                        row["k"].Add(Equation(row["x"][j], row["y"][j]));
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
                    if (j == 3)
                    {
                        row["y"].Add(row["y"][0] + h * row["k"][j - 1]);
                    }
                    else
                    {
                        row["y"].Add(row["y"][0] + h * row["k"][j - 1] / 2);
                    }

                    row["k"].Add(Equation(row["x"][j], row["y"][j]));

                    if (j == 3)
                    {
                        row["deltaY"].Add(h / 6f * (row["k"][0] + 2 * row["k"][1] + 2 * row["k"][2] + row["k"][3]));
                        row["yi"].Add(row["y"][0] + row["deltaY"][0]);
                        row["e"].Add(Math.Abs((row["k"][1] - row["k"][2]) / (row["k"][0] - row["k"][1])));
                    }

                }

                i++;
            }

            var lastRow = calculationsTable.Last();
            return lastRow["y"][0] + lastRow["deltaY"][0];
        }

        public void PrintCalculations(double startStep, double yAtZero)
        {
            double result = RungeKuttaMethod(startStep, yAtZero, out List<Dictionary<string, List<double>>> calculationsTable);

            int j = 0;
            foreach (var tableRow in calculationsTable)
            {
                //each row in tableRow
                Console.Write($"\ni = {j}\n");
                Console.Write($"|x\t|      y      |      k     |\n");
                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"|{Math.Round(tableRow["x"][i], 10)}\t|  {tableRow["y"][i]:0.000000}  |  {tableRow["k"][i]:0.000000}  |");
                    Console.Write("\n");
                }
                Console.Write($"|DeltaY = {tableRow["deltaY"][0]}\t   |\n");
                Console.Write($"|Y{j} = {tableRow["yi"][0]}\t   |\n");
                Console.Write($"|E = {tableRow["e"][0]}\t   |\n");
                j++;
            }
        }

        private Dictionary<string, List<double>> CreateNewTableRow()
        {
            Dictionary<string, List<double>> result = new()
            {
                { "x", new() },
                { "y", new() },
                { "k", new() },
                { "deltaY", new() },
                {"yi", new() },
                { "e", new() },
            };

            return result;
        }
    }
}

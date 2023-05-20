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

        public List<Dictionary<string, double[]>> RungeKuttaMethod(double startStep, double yAtZero)
        {
            List<Dictionary<string, double[]>> calculationsTable = new();

            double h = startStep;

            //steps cycle
            int i = 0;
            while (i < (B - A) / h)
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
                            row["y"][j] = yAtZero;
                            row["x"][j] = 0;
                            row["k"][j] = Equation(row["x"][j], row["y"][j]);
                            continue;
                        }

                        row["x"][j] = (i * h);
                        row["y"][j] = (calculationsTable[i - 1]["y"][0] + calculationsTable[i - 1]["deltaY"][0]);
                        row["k"][j] = (Equation(row["x"][j], row["y"][j]));
                        continue;
                    }

                    //setting x
                    if (j == 3)
                    {
                        row["x"][j] = (row["x"][0] + h);
                    }
                    else
                    {
                        row["x"][j] = (row["x"][0] + h / 2);
                    }

                    //setting y
                    if (j == 3)
                    {
                        row["y"][j] = (row["y"][0] + h * row["k"][j - 1]);
                    }
                    else
                    {
                        row["y"][j] = (row["y"][0] + h * row["k"][j - 1] / 2);
                    }

                    row["k"][j] = (Equation(row["x"][j], row["y"][j]));

                    if (j == 3)
                    {
                        row["deltaY"][0] = (h / 6f * (row["k"][0] + 2 * row["k"][1] + 2 * row["k"][2] + row["k"][3]));
                        row["yi"][0] = (row["y"][0] + row["deltaY"][0]);
                        row["teta"][0] = (Math.Abs((row["k"][1] - row["k"][2]) / (row["k"][0] - row["k"][1])));
                    }
                }

                i++;
            }

            return calculationsTable;

            Dictionary<string, double[]> CreateNewTableRow()
            {
                Dictionary<string, double[]> result = new()
                {
                    { "x", new double[4] },
                    { "y", new double[4] },
                    { "k", new double[4] },
                    { "deltaY", new double[1] },
                    {"yi", new double[1] },
                    { "teta", new double[1] },
                };

                return result;
            }
        }

        public List<Dictionary<string, double[]>> RungeKuttaMethod(double startStep, double yAtZero, int rowsToReturn)
        {
            List<Dictionary<string, double[]>> calculationsTable = new();

            double h = startStep;

            //steps cycle
            int i = 0;
            while (i <= rowsToReturn)
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
                            row["y"][j] = yAtZero;
                            row["x"][j] = 0;
                            row["k"][j] = Equation(row["x"][j], row["y"][j]);
                            continue;
                        }

                        row["x"][j] = (i * h);
                        row["y"][j] = (calculationsTable[i - 1]["y"][0] + calculationsTable[i - 1]["deltaY"][0]);
                        row["k"][j] = (Equation(row["x"][j], row["y"][j]));
                        continue;
                    }

                    //setting x
                    if (j == 3)
                    {
                        row["x"][j] = (row["x"][0] + h);
                    }
                    else
                    {
                        row["x"][j] = (row["x"][0] + h / 2);
                    }

                    //setting y
                    if (j == 3)
                    {
                        row["y"][j] = (row["y"][0] + h * row["k"][j - 1]);
                    }
                    else
                    {
                        row["y"][j] = (row["y"][0] + h * row["k"][j - 1] / 2);
                    }

                    row["k"][j] = (Equation(row["x"][j], row["y"][j]));

                    if (j == 3)
                    {
                        row["deltaY"][0] = (h / 6f * (row["k"][0] + 2 * row["k"][1] + 2 * row["k"][2] + row["k"][3]));
                        row["yi"][0] = (row["y"][0] + row["deltaY"][0]);
                        row["teta"][0] = (Math.Abs((row["k"][1] - row["k"][2]) / (row["k"][0] - row["k"][1])));
                    }
                }

                i++;
            }

            return calculationsTable;

            Dictionary<string, double[]> CreateNewTableRow()
            {
                Dictionary<string, double[]> result = new()
                {
                    { "x", new double[4] },
                    { "y", new double[4] },
                    { "k", new double[4] },
                    { "deltaY", new double[1] },
                    {"yi", new double[1] },
                    { "teta", new double[1] },
                };

                return result;
            }
        }

        public void PrintCalculationsRungeKuttaMethod(double startStep, double yAtZero)
        {
            List<Dictionary<string, double[]>> calculationsTable = RungeKuttaMethod(startStep, yAtZero);
            Console.WriteLine("\n>>>\tRunge Kutta method\n");

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
                Console.Write("------------------------------------\n");
                Console.Write($"|DeltaY = {tableRow["deltaY"][0]}\t   |\n");
                Console.Write($"|y{j} = {tableRow["yi"][0]}\t   |\n");
                Console.Write($"|Teta = {tableRow["teta"][0]}\t   |\n");
                j++;
            }
        }

        public List<Dictionary<string, double>> AdamsMethod(double startStep, double yAtZero)
        {
            double h = startStep;

            var RungeKuttaMethodResult = RungeKuttaMethod(h, yAtZero, 3);
            List<Dictionary<string, double>> tableA = new();

            foreach (var tableRow in RungeKuttaMethodResult)
            {
                tableA.Add(CreateNewTableRow());
                var tableARow = tableA.Last();

                tableARow["x"] = tableRow["x"][0];
                tableARow["y"] = tableRow["y"][0];
                tableARow["y'"] = tableRow["k"][0];
            }

            int i = 3;
            while (i <= (B - A) / h)
            {
                if (tableA.Count <= i)
                    tableA.Add(CreateNewTableRow());

                var tableRow = tableA[i];

                if(i == 3) 
                {
                    tableRow["ha/24"] = h * (55f * tableA[i]["y'"] - 59f * tableA[i - 1]["y'"] + 37f * tableA[i - 2]["y'"] - 9f * tableA[i - 3]["y'"]) / 24f;
                }

                if (i > 3)
                {
                    tableRow["x"] = i * h;
                    tableRow["y"] = tableA[i - 1]["y"] + tableA[i - 1]["ha/24"];
                    tableRow["y'"] = Equation(tableRow["x"], tableRow["y"]);

                    tableRow["ha/24"] = h * (55f * tableA[i]["y'"] - 59f * tableA[i - 1]["y'"] + 37f * tableA[i - 2]["y'"] - 9f * tableA[i - 3]["y'"]) / 24f;

                    tableA[i - 1]["hb/24"] = h * (9f * tableA[i]["y'"] + 19f * tableA[i-1]["y'"] - 5f * tableA[i - 2]["y'"] + tableA[i - 3]["y'"] ) / 24f;
                    tableRow["ySpec"] = tableA[i - 1]["y"] + tableA[i - 1]["hb/24"];
                }

                i++;
            }

            return tableA;

            static Dictionary<string, double> CreateNewTableRow()
            {
                Dictionary<string, double> result = new()
                {
                    { "x", new() },
                    { "y", new() },
                    { "ySpec", new() },
                    { "y'", new() },
                    { "ha/24", new() },
                    { "hb/24", new() },
                };

                return result;
            }
        }

        public void PrintCalculationsAdamsMethod(double startStep, double yAtZero)
        {
            var calculationsTable = AdamsMethod(startStep, yAtZero);

            int j = 0;
            Console.WriteLine("\n>>>\tAdams method\n");
            Console.Write($"|x\t|      y'     |      y     |    ySpec   |   h*a/24   |    h*b/24  |\n");

            foreach (var tableRow in calculationsTable)
            {
                //each row in tableRow
                Console.Write($"i = {j}\n");
                if(j >= 3)
                {
                    Console.Write($"|{Math.Round(tableRow["x"], 10)}\t|  {tableRow["y'"]:0.000000}  |  {tableRow["y"]:0.000000}  |  {tableRow["ySpec"]:0.000000}  |  {tableRow["ha/24"]:0.000000}  |  {tableRow["hb/24"]:0.000000}  |");
                }
                else
                {
                    Console.Write($"|{Math.Round(tableRow["x"], 10)}\t|  {tableRow["y'"]:0.000000}  |  {tableRow["y"]:0.000000}  |            |            |            |");
                }

                Console.Write("\n");

                j++;
            }
        }
    }
}

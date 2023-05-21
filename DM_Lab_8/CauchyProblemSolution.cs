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

        /// <summary>
        /// Returns a calculations table that satisfies the specified error        
        /// </summary>
        public List<Dictionary<string, double[]>> RungeKuttaMethod(double yAtZero, double e)
        {
            double h = 0.1;
            List<List<Dictionary<string, double[]>>> tablesList = new();

            bool canStop = false;
            while (tablesList.Count < 1 || !canStop)
            {
                if (tablesList.Count != 0)
                {
                    h /= 2;
                }

                tablesList.Add(RungeKuttaMethodCalculate(h, yAtZero));

                if (tablesList.Count > 1)
                {
                    canStop = true;
                    for (int i = 0; i < tablesList[^2].Count; i++)
                    {
                        tablesList[^2][i]["e"][0] = Math.Abs(tablesList[^1][i * 2]["yi"][0] - tablesList[^2][i]["yi"][0]) / 15f;

                        if (tablesList[^2][i]["e"][0] > e)
                            canStop = false;
                    }
                }
            }

            return tablesList[^2];
        }

        /// <summary>
        /// Returns a calculations table with specified step
        /// </summary>
        private List<Dictionary<string, double[]>> RungeKuttaMethodCalculate(double h, double yAtZero)
        {
            List<Dictionary<string, double[]>> calculationsTable = new();

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
                        row["y"][j] = calculationsTable[i - 1]["yi"][0];
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

                    //setting k
                    row["k"][j] = (Equation(row["x"][j], row["y"][j]));

                    //setting other
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

            static Dictionary<string, double[]> CreateNewTableRow()
            {
                Dictionary<string, double[]> result = new()
                {
                    { "x", new double[4] },
                    { "y", new double[4] },
                    { "k", new double[4] },
                    { "deltaY", new double[1] },
                    {"yi", new double[1] },
                    { "teta", new double[1] },
                    { "e", new double[1] },
                };

                return result;
            }
        }

        /// <summary>
        /// Returns first [rowsToReturn] rows of calculations table with specified step 
        /// </summary>
        private List<Dictionary<string, double[]>> RungeKuttaMethodCalculate(double h, double yAtZero, int rowsToReturn, out double step)
        {
            List<Dictionary<string, double[]>> calculationsTable = new();

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

            step = h;
            return calculationsTable;

            static Dictionary<string, double[]> CreateNewTableRow()
            {
                Dictionary<string, double[]> result = new()
                {
                    { "x", new double[4] },
                    { "y", new double[4] },
                    { "k", new double[4] },
                    { "deltaY", new double[1] },
                    {"yi", new double[1] },
                    { "teta", new double[1] },
                    { "e", new double[1] },
                };

                return result;
            }
        }

        /// <summary>
        /// Prints calculations table that satisfies the specified error 
        /// </summary>
        public void PrintCalculationsRungeKuttaMethod(double yAtZero, double e)
        {
            List<Dictionary<string, double[]>> calculationsTable = RungeKuttaMethod(yAtZero, e);
            Console.WriteLine($"\n>>>\tRunge Kutta method\te = {e}\n");

            int j = 0;
            foreach (var tableRow in calculationsTable)
            {
                //each row in tableRow
                Console.Write($"\ni = {j}\n");
                Console.Write($"|    x     |     y      |      k     |\n");
                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"|{Math.Round(tableRow["x"][i], 10),-10}|  {tableRow["y"][i]:0.000000}  |  {tableRow["k"][i]:0.000000}  |");
                    Console.Write("\n");
                }
                Console.Write("------------------------------------\n");
                Console.Write($"|DeltaY = {tableRow["deltaY"][0]}\t     |\n");
                Console.Write($"|y{j} = {tableRow["yi"][0]}\t     |\n");
                Console.Write($"|Teta = {tableRow["teta"][0]}\t     |\n");
                Console.Write($"|E = {tableRow["e"][0]}\t     |\n");

                j++;
            }
        }

        /// <summary>
        /// Returns a calculations table that satisfies the specified error
        /// </summary>
        public List<Dictionary<string, double>> AdamsMethod(double yAtZero, double e)
        {
            double h = 0.1;
            List<List<Dictionary<string, double>>> tablesList = new();

            bool canStop = false;
            while (!canStop)
            {
                if (tablesList.Count != 0)
                {
                    h /= 2;
                }

                tablesList.Add(AdamsMethodCalculate(h, yAtZero));

                canStop = true;
                for (int i = 0; i < tablesList[^1].Count; i++)
                    if (tablesList[^1][i]["e"] > e)
                        canStop = false;
            }

            return tablesList[^1];
        }

        /// <summary>
        /// Returns a calculations table with specified step
        /// </summary>
        private List<Dictionary<string, double>> AdamsMethodCalculate(double h, double yAtZero)
        {
            var RungeKuttaMethodResult = RungeKuttaMethodCalculate(h, yAtZero, 3, out h);
            List<Dictionary<string, double>> table = new();

            foreach (var tableRow in RungeKuttaMethodResult)
            {
                table.Add(CreateNewTableRow());
                var tableARow = table.Last();

                tableARow["x"] = tableRow["x"][0];
                tableARow["y"] = tableRow["y"][0];
                tableARow["y'"] = tableRow["k"][0];
            }

            int i = 3;
            while (i <= (B - A) / h)
            {
                if (table.Count <= i)
                    table.Add(CreateNewTableRow());

                var tableRow = table[i];

                if (i == 3)
                {
                    tableRow["ha/24"] = h * (55f * table[i]["y'"] - 59f * table[i - 1]["y'"] + 37f * table[i - 2]["y'"] - 9f * table[i - 3]["y'"]) / 24f;
                }
                else
                {
                    tableRow["x"] = i * h;
                    tableRow["y"] = table[i - 1]["y"] + table[i - 1]["ha/24"];
                    tableRow["y'"] = Equation(tableRow["x"], tableRow["y"]);

                    tableRow["ha/24"] = h * (55f * table[i]["y'"] - 59f * table[i - 1]["y'"] + 37f * table[i - 2]["y'"] - 9f * table[i - 3]["y'"]) / 24f;

                    table[i - 1]["hb/24"] = h * (9f * table[i]["y'"] + 19f * table[i - 1]["y'"] - 5f * table[i - 2]["y'"] + table[i - 3]["y'"]) / 24f;
                    tableRow["ySpec"] = table[i - 1]["y"] + table[i - 1]["hb/24"];
                    tableRow["e"] = Math.Abs(tableRow["ySpec"] - tableRow["y"]);
                }

                i++;
            }

            return table;

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
                    { "e", new() },
                };

                return result;
            }
        }

        /// <summary>
        /// Prints calculations table that satisfies the specified error 
        /// </summary>
        public void PrintCalculationsAdamsMethod(double yAtZero, double e)
        {
            var calculationsTable = AdamsMethod(yAtZero, e);

            int j = 0;
            Console.WriteLine($"\n>>>\tAdams method\te = {e}\n");
            Console.Write($"|x\t|      y'     |      y     |    ySpec   |   h*a/24   |    h*b/24  |      e     |\n");

            foreach (var tableRow in calculationsTable)
            {
                //each row in tableRow
                Console.Write($"i = {j}\n");
                if (j >= 3)
                {
                    Console.Write($"|{Math.Round(tableRow["x"], 10)}\t|  {tableRow["y'"]:0.000000}  |  {tableRow["y"]:0.000000}  |  {tableRow["ySpec"]:0.000000}  |  {tableRow["ha/24"]:0.000000}  |  {tableRow["hb/24"]:0.000000}  |  {tableRow["e"]:0.000000}  |");
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

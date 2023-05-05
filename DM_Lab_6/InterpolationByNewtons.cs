namespace DM_Lab_6
{
    public class InterpolationByNewtons
    {
        public string Formula { get; private set; }

        public List<double> xValues { get; }

        public List<double> yValues { get; }

        public Func<double, double> Func { get; private set; }

        public bool IsFormulaBuilded { get; private set; } = false;

        public bool IsBuilded { get; private set; } = false;

        public InterpolationByNewtons(List<double> xList, List<double> yList)
        {
            if (xList == null || yList == null)
                throw new ArgumentNullException();

            if (xList.Count < 2 || yList.Count < 2)
                throw new ArgumentException();

            if (xList.Count != yList.Count)
                throw new ArgumentException();

            xValues = xList;
            yValues = yList;
        }

        public void Start()
        {
            Func = (double x) =>
            {
                if (!IsFormulaBuilded)
                    Formula = "";

                double multiplyFunc(int i)
                {
                    if (!IsFormulaBuilded)
                        Formula += $"x - {xValues[i]}";

                    return x - xValues[i];
                }

                double sumFunc(int i)
                {
                    if (!IsFormulaBuilded)
                        Formula += $"{Math.Round(GetDelta(yValues.GetRange(0, i + 1), xValues.GetRange(0, i + 1)), 6)}";

                    return GetDelta(yValues.GetRange(0, i + 1), xValues.GetRange(0, i + 1)) * Multiply(0, i - 1, multiplyFunc);
                }

                if (!IsFormulaBuilded)
                    Formula += $"{yValues.First()} + ";

                return yValues.First() + Sum(1, xValues.Count - 1, sumFunc);
            };

            IsBuilded = true;
        }

        public double GetDelta(List<double> y, List<double> x)
        {
            double xFirst;
            double xSecond;

            if (x.Count < 2)
            {
                throw new ArgumentException();
            }

            if (y.Count != x.Count)
            {
                throw new ArgumentException();
            }

            if (x.Count > 2)
            {
                List<double> xWithoutFirst = x.GetRange(1, x.Count - 1);
                List<double> xWithoutLast = x.GetRange(0, x.Count - 1);

                List<double> yWithoutFirst = y.GetRange(1, y.Count - 1);
                List<double> yWithoutLast = y.GetRange(0, y.Count - 1);

                xFirst = GetDelta(yWithoutLast, xWithoutLast);
                xSecond = GetDelta(yWithoutFirst, xWithoutFirst);

                return (xFirst - xSecond) / (x.First() - x.Last());
            }
            else
            {
                xFirst = x[0];
                xSecond = x[1];

                return (y[0] - y[1]) / (xFirst - xSecond);
            }
        }

        private double Sum(int i, int j, Func<int, double> func)
        {
            double res = 0;

            while (i <= j)
            {
                res += func(i);

                if (!IsFormulaBuilded && i + 1 <= j)
                    Formula += " + ";

                i++;
            }

            IsFormulaBuilded = true;
            return res;
        }

        private double Multiply(int i, int j, Func<int, double> func)
        {
            double res = 1;

            while (i <= j)
            {
                if (!IsFormulaBuilded)
                    Formula += "(";

                res *= func(i);
                i++;

                if (!IsFormulaBuilded)
                    Formula += ")";
            }

            return res;
        }
    }
}

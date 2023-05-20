namespace DM_Lab_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> func = x => Math.Cos(x) / (x + 1f);
            //Func<double, double> func = x => Math.Pow(x + 2f, -1);
            //Func<double, double> func = x => 1 / (1 + Math.Pow(x, 2));

            Integration integrate = new(func);
            integrate.SetIntegrationBorders(0.5, 1.4);
            Console.WriteLine($"\t[IntegrateViaQuadratureFormula] Integration result = {integrate.IntegrateViaQuadratureFormula(0.0001)}");
            Console.WriteLine($"\t[IntegrateViaQuadratureFormula] E = {integrate.LastIntegrationE:0.000000000000000}");
            Console.WriteLine("\n");
            Console.WriteLine($"\t[IntegrateViaTrapeziumMethod] Integration result = {integrate.IntegrateViaTrapeziumMethod(0.0001)}");
            Console.WriteLine($"\t[IntegrateViaTrapeziumMethod] E = {integrate.LastIntegrationE:0.000000000000000}");
        }
    }
}
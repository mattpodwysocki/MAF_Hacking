using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ExtensibleApplication
{
    class Program
    {
        private CompositionContainer _container;

        [Import]
        ICalculator Calculator { get; set; }

        private Program()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        static void Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine($"3.0 / 4.0: {p.Calculator.CalculateInput("3.0/4.0")}");
            Console.WriteLine($"3.0 + 4.0: {p.Calculator.CalculateInput("3.0+4.0")}");
            Console.WriteLine($"3.0 - 4.0: {p.Calculator.CalculateInput("3.0-4.0")}");
            Console.WriteLine($"3.0 * 4.0: {p.Calculator.CalculateInput("3.0*4.0")}");
            Console.WriteLine($"3.0 % 4.0: {p.Calculator.CalculateInput("3.0%4.0")}");
        }
    }
}

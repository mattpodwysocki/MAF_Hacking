using System;
using System.ComponentModel.Composition;

namespace ExtensibleApplication
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '^')]
    public class Pow : IOperation
    {
        public double Operate(double left, double right)
        {
            return Math.Pow(left, right);
        }
    }
}

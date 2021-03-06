﻿using System.ComponentModel.Composition;

namespace ExtensibleApplication
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '/')]
    public class Divide : IOperation
    {
        public double Operate(double left, double right)
        {
            return left / right;
        }
    }
}

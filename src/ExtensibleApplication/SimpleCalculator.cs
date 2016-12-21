using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ExtensibleApplication
{
    [Export(typeof(ICalculator))]
    public class SimpleCalculator : ICalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> Operations { get; set; }

        public string CalculateInput(string input)
        {
            double left, right;
            char operation;

            int op = FindOperator(input);
            if (op < 0) { return "Invalid Input"; }

            if (!double.TryParse(input.Substring(0, op), out left) ||
                !double.TryParse(input.Substring(op + 1), out right))
            {
                return "Invalid input";
            }

            operation = input[op];
            foreach(var i in Operations)
            {
                if (i.Metadata.Symbol == operation) { return i.Value.Operate(left, right).ToString();  }
            }

            return "Invalid operation";
        }

        private int FindOperator(string str)
        {
            var ops = (from i in Operations select i.Metadata.Symbol).ToArray();
            return str.IndexOfAny(ops);
        }
    }
}

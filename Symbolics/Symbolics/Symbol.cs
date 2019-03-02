using System;
using System.Linq;
using System.Globalization;

namespace Symbolics
{
    public class Symbol : Expression
    {
        public string symbol { get; protected set; } = string.Empty;

        public double? Value = null;


        public override bool CanParse(string equation)
        {
            return equation.Trim().All(c => char.IsLetterOrDigit(c) || c == '.');
        }

        public override Expression Parse(string equation)
        {
            string eq = equation.Trim();
            if (double.TryParse(eq, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
            {
                return new Symbol
                {
                    symbol = eq,
                    Value = val
                };
            }
            else
            {
                return new Symbol
                {
                    symbol = eq
                };
            }
        }

        public override string ToString()
        {
            return symbol;
        }

        public override bool CanCalculate()
        {
            return (Value != null)
                && (Value.HasValue)
                && !double.IsNaN(Value.Value);
        }

        public override double Calculate()
        {
            if (!CanCalculate())
                throw new Exception("But I cannot calculate this symbols value! : " + ToString());

            return Value.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Symbolics
{
    public class Subtraction : Operation
    {
        public Subtraction()
        {
            Operator = "-";
            ParseOrder = ParseOrders.Subtraction;
        }

        public override double Calculate()
        {
            return LeftHandSide.Calculate() - RightHandSide.Calculate();
        }

        public override Expression Parse(string equation)
        {
            string[] sides = equation.Split(Operator.ToCharArray());

            var LeftSide = sides.Reverse().Skip(1).Reverse();
            var RightSide = sides.Last();

            return new Subtraction
            {
                LeftHandSide = Expression.Create(String.Join(Operator, LeftSide)),
                RightHandSide = Expression.Create(RightSide)
            };
        }
    }
}

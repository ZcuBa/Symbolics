using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Symbolics
{
    public class Addition : Operation
    {
        public Addition()
        {
            Operator = "+";
        }

        public override double Calculate()
        {
            return LeftHandSide.Calculate() + RightHandSide.Calculate();
        }

        public override Expression Parse(string equation)
        {
            string[] sides = equation.Split(Operator.ToCharArray());

            var LeftSide = sides.First();
            var RightSide = sides.Skip(1);

            return new Addition
            {
                LeftHandSide = Expression.Create(LeftSide),
                RightHandSide = Expression.Create(String.Join(Operator, RightSide))
            };
        }
    }
}

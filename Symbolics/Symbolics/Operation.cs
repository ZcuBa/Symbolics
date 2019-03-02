using System;
using System.Collections.Generic;
using System.Text;

namespace Symbolics
{
    public abstract class Operation : Expression
    {
        protected string Operator;

        public override bool CanCalculate()
        {
            return LeftHandSide.CanCalculate() && RightHandSide.CanCalculate();
        }

        public override bool CanParse(string equation)
        {
            return equation.Contains(Operator);
        }

        public override string ToString()
        {
            return LeftHandSide.ToString() 
                + Operator
                + RightHandSide.ToString();
        }

        /// <summary>
        /// Child left of operator
        /// </summary>
        public Expression LeftHandSide {
            get
            {
                return _leftHandSide;
            }
            set
            {
                _leftHandSide = value;
                _leftHandSide.Parent = this;
            }
        }
        private Expression _leftHandSide;

        /// <summary>
        /// Child right of operator
        /// </summary>
        public Expression RightHandSide {
            get
            {
                return _rightHandSide;
            }
            set
            {
                _rightHandSide = value;
                _rightHandSide.Parent = this;
            }
        }
        private Expression _rightHandSide;


    }
}

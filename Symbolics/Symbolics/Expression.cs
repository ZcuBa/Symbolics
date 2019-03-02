using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Symbolics
{
    /// <summary>
    /// A node in an expression hiearchy is some sort of expression.
    /// This abstract class also implements an expression node factory
    /// </summary>
    public abstract class Expression
    {
        public Expression Parent { get; set; }

        public abstract Expression Parse(string equation);

        public virtual bool CanCalculate()
        {
            return false;
        }

        public virtual double Calculate()
        {
            throw new NotImplementedException(this.GetType() + "does not implement a Calculate method!");
        }

        public virtual bool CanParse(string equation)
        {
            return false;
        }

        private static List<Expression> Expressions = new List<Expression>();

        public static Expression Create(string equation)
        {
            if(Expressions.Count == 0)
            { 
                var asm = Assembly.GetExecutingAssembly();

                var ExpressionsTypes =  asm.GetTypes().Where(t => typeof(Expression).IsAssignableFrom(t));

                foreach (Type expressionType in ExpressionsTypes)
                {
                    try
                    {
                        Expressions.Add((Expression)Activator.CreateInstance(expressionType));
                    }
                    catch { }
                }
            }

            Expression expression = null; 

            try
            {
                expression = Expressions.Single(e => e.CanParse(equation));
            }
            catch
            {
                throw new Exception("I was unable to determine which expression node to create from this string: " + equation);
            }

            try
            {
                return expression.Parse(equation);
            }
            catch (Exception ex)
            {
                throw new Exception("(" + expression.GetType() + ") I was unable to parse this string: " + equation + ", see inner exception for details.", ex);
            }
        }
    }
}

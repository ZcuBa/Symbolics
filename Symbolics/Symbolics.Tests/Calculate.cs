using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Symbolics.Tests
{
    [TestFixture]
    public class Calculate
    {

        [TestCase("1.234 + 2.345", 3.579, 0.001)]
        [TestCase("1.234 + 2.345 + 3.456", 7.035, 0.001)]
        public void CalculatesValue(string equation, double expectedvalue, double tolerance)
        {
            Expression SUT = Expression.Create(equation);

            Assert.That(SUT.CanCalculate(), equation + " is not recognized as something we can calculate.");

            Assert.AreEqual(expectedvalue, SUT.Calculate(), tolerance, "calcualted value is not what we expected");
        }

    }
}

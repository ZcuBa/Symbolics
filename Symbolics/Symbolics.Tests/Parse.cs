using System;
using NUnit.Framework;
using System.Globalization;


namespace Symbolics.Tests
{
    [TestFixture]
    public class Parse
    {
        [TestCase("a", true)]
        [TestCase("2.345", true)]
        [TestCase("a+b", false)]
        [TestCase("a + b", false)]
        [TestCase("1.2+2.3", false)]
        [TestCase("1.2 + 2.3", false)]
        [TestCase("a=b", false)]
        public void ParseSymbol(string equation, bool IsSymbol)
        {
            Expression SUT = Expression.Create(equation);

            bool IsActuallySymbol = SUT.GetType() == typeof(Symbol);

            Assert.That(IsActuallySymbol == IsSymbol);
        }

        [TestCase("a", false)]
        [TestCase("2.345", false)]
        [TestCase("a+b", true)]
        [TestCase("a + b", true)]
        [TestCase("1.2+2.3", true)]
        [TestCase("1.2 + 2.3", true)]
        [TestCase("a=b", false)]
        public void ParseAddition(string equation, bool IsAddition)
        {
            Expression SUT = Expression.Create(equation);

            bool IsActuallyAddition = SUT.GetType() == typeof(Addition);

            Assert.That(IsActuallyAddition == IsAddition);
        }

        [TestCase("a", false)]
        [TestCase("2.345", true)]
        public void ParseSymbolValue(string equation, bool IsSymbolValue)
        {
            Expression SUT = Expression.Create(equation);

            bool IsActuallySymbol = SUT.GetType() == typeof(Symbol);
            Assert.That(IsActuallySymbol, equation + " is not a Symbol");

            Assert.That(IsSymbolValue == SUT.CanCalculate());
            if (IsSymbolValue)
                Assert.AreEqual(double.Parse(equation, CultureInfo.InvariantCulture), SUT.Calculate(), double.Epsilon * 2.0, "symbol value is somehow calcualted wrong!");
        }
    }
}

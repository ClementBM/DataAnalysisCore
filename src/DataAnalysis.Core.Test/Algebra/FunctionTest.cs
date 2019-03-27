using DataAnalysis.Core.Algebra.Formula;
using DataAnalysis.Core.Algebra.Derivative;
using NUnit.Framework;

namespace DataAnalysis.Test.Algebra
{
    [TestFixture]
    public class FunctionTest
    {
        [Test]
        [TestCase(2, 1, 3, 2, 0.01)]
        [TestCase(2, 1, 3, 2, 0.1)]
        [TestCase(10, 0, 10, 2, 0.01)]
        [TestCase(10, 0, 10, 2, 0.1)]
        [TestCase(10, 1, -10, 2, 0.01)]
        public void DerivativeTest(double a, double b, double c, double x, double h)
        {
            var secondOrderPolynomial = new SecondOrderPolynomial(a, b, c);

            var derivedPolynomialNum = new NumericDerivative(secondOrderPolynomial, h);
            var actualResult = derivedPolynomialNum.GetValue(x);

            var expectedValue = secondOrderPolynomial.GetDerivativeValue(x);

            Assert.AreEqual(expectedValue, actualResult, 1E-12);
        }

        [Test]
        [TestCase(2, 1, 3, 2)]
        public void DerivativeTest2(double a, double b, double c, double x)
        {
            var secondOrderPolynomial = new SecondOrderPolynomial(a, b, c);
            var actualResult = secondOrderPolynomial.GetNumericalDerivativeValue(x);

            var expectedValue = secondOrderPolynomial.GetDerivativeValue(x);

            Assert.AreEqual(expectedValue, actualResult, 1E-12);
        }
    }
}

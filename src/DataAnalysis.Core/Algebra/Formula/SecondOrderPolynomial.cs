using System;

namespace DataAnalysis.Core.Algebra.Formula
{
    public class SecondOrderPolynomial : SimpleFunctionBase
    {
        public double a { get; set; }

        public double b { get; set; }

        public double c { get; set; }

        public SecondOrderPolynomial(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            FunctionFormula = (double x) => a * Math.Pow(x, 2) + b * x + c;
        }

        public double GetDerivativeValue(double x)
        {
            return 2 * a * x + b;
        }
    }
}

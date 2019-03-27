using DataAnalysis.Core.Algebra.Formula;

namespace DataAnalysis.Core.Algebra.Derivative
{
    public class NumericDerivative : SimpleFunctionBase
    {
        public SimpleFunctionBase function { get; protected set; }

        public double h { get; protected set; }

        public NumericDerivative(SimpleFunctionBase function, double h)
        {
            this.function = function;
            this.h = h;
            FunctionFormula = (double x) => (function.FunctionFormula(x + h) - function.FunctionFormula(x - h)) / (2 * h);
        }

        public static readonly double hDefault = 0.01;
    }
}

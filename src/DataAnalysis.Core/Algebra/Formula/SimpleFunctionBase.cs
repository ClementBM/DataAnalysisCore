using DataAnalysis.Core.Algebra.Derivative;

namespace DataAnalysis.Core.Algebra.Formula
{
    public abstract class SimpleFunctionBase
    {
        public Formula FunctionFormula { get; protected set; }

        private Formula _numericalDerivedFunctionFormula;

        public Formula NumericalDerivedFunctionFormula
        {
            get
            {
                if (_numericalDerivedFunctionFormula == null)
                {
                    _numericalDerivedFunctionFormula = new NumericDerivative(this, NumericDerivative.hDefault).FunctionFormula;
                }
                return _numericalDerivedFunctionFormula;
            }
        }

        public delegate double Formula(double point);

        public double GetValue(double x)
        {
            return FunctionFormula(x);
        }

        public double GetNumericalDerivativeValue(double x)
        {
            return NumericalDerivedFunctionFormula(x);
        }
    }
}

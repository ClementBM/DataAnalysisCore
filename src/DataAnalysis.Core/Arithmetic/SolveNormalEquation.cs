using System;
using DataAnalysis.Core.Algebra;

namespace DataAnalysis.Core.Arithmetic
{
    public class SolveNormalEquation : Algorithm<SolveNormalEquationParameter, Matrix>
    {
        protected override bool CheckInputs(SolveNormalEquationParameter parameters)
        {
            throw new NotImplementedException();
        }

        protected override Matrix DoCompute(SolveNormalEquationParameter parameters)
        {
            throw new NotImplementedException();
        }
    }

    public class SolveNormalEquationParameter
    {
        public Matrix A { get; set; }
        public Matrix b { get; set; }
    }
}

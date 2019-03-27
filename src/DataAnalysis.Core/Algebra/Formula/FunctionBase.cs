using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace DataAnalysis.Core.Algebra.Formula
{
    public abstract class MultivariateFunctionBase<Tvariable, Tparameter>
        where Tparameter : FunctionParameter<Tparameter>, new()
        where Tvariable : FunctionVariable<Tvariable>, new()
    {
        public Formula FunctionFormula { get; protected set; }

        Formula _numericalDerivedFunctionFormula;

        public delegate double Formula(Tvariable x, Tparameter p);

        public double GetValue(Tvariable x, Tparameter p)
        {
            return FunctionFormula(x, p);
        }
    }

    public abstract class MultivariateDerivative<Tvariable, Tparameter>
         where Tparameter : FunctionParameter<Tparameter>, new()
         where Tvariable : FunctionVariable<Tvariable>, new()
    {
        public double H { get; protected set; }

        public MultivariateFunctionBase<Tvariable, Tparameter> Function { get; protected set; }

        public MultivariateDerivative(MultivariateFunctionBase<Tvariable, Tparameter> function, double h)
        {
            H = h;
            Function = function;
        }

        public double DerivativeByVariable(Tvariable x, Tparameter p, int partialOrder)
        {
            throw new Exception();
        }

        public double DerivativeByParameter(Tvariable x, Tparameter p, int partialOrder)
        {
            Contract.Assert(partialOrder < p.Count());
            var pBefore = p.Set(partialOrder, p[partialOrder] + H);
            var pAfter = p.Set(partialOrder, p[partialOrder] - H);
            return (Function.GetValue(x, pBefore) - Function.GetValue(x, pAfter)) / (2 * H);
        }
        public double SecondDerivativeByParameter(Tvariable x, Tparameter p, int partialOrder)
        {
            Contract.Assert(partialOrder < p.Count());
            var pBefore = p.Set(partialOrder, p[partialOrder] + H);
            var pAfter = p.Set(partialOrder, p[partialOrder] - H);
            return
                (Function.GetValue(x, pBefore) - (2 * Function.GetValue(x, p)) + Function.GetValue(x, pAfter))
                / Math.Pow(H, 2);
        }

        public Matrix Jacobian(List<Tvariable> variables, Tparameter parameter)
        {
            int m = variables.Count;
            int n = parameter.Count();
            Contract.Assert(m > 0);
            Contract.Assert(variables[0].Count() > 0);
            Contract.Assert(n > 0);

            var jacobian = new Matrix(m, n);
            return MatrixUtils.Fill(jacobian, (i, j) => DerivativeByParameter(variables[i], parameter, j));
        }

        public Matrix Hessian(List<Tvariable> variables, Tparameter parameter)
        {
            int m = variables.Count;
            int n = parameter.Count();
            Contract.Assert(m > 0);
            Contract.Assert(variables[0].Count() > 0);
            Contract.Assert(n > 0);

            var hessian = new Matrix(m, n);
            return MatrixUtils.Fill(hessian, (i, j) => SecondDerivativeByParameter(variables[i], parameter, j));
        }
    }

    public class MultivariateSecondOrderPolynomial : MultivariateFunctionBase<SecondOrderVariable, SecondOrderParameter>
    {
        public MultivariateSecondOrderPolynomial()
        {
            FunctionFormula = (SecondOrderVariable variable, SecondOrderParameter parameter) =>
                parameter.A * Math.Pow(variable.X, 2)
                + parameter.B * variable.X
                + parameter.C;
        }
    }

    public class SecondOrderVariable : FunctionVariable<SecondOrderVariable>
    {
        public double X { get; set; }
        public override SecondOrderVariable MakeAInstance()
        {
            return new SecondOrderVariable()
            {
                X = this.X,
            };
        }
    }

    public class SecondOrderParameter : FunctionParameter<SecondOrderParameter>
    {
        [FunctionParameter(Order = 1)]
        public double A { get; set; }
        [FunctionParameter(Order = 2)]
        public double B { get; set; }
        [FunctionParameter(Order = 3)]
        public double C { get; set; }

        public override SecondOrderParameter MakeAInstance()
        {
            return new SecondOrderParameter()
            {
                A = this.A,
                B = this.B,
                C = this.C
            };
        }
    }
}
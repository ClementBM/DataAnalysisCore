using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Arithmetic;
using System;
using System.Collections.Generic;

namespace DataAnalysis.Core.Regression
{
    public class LogisticRegression : Algorithm<LogisticRegressionParameters, LogisticRegressionResult>
    {
        protected override bool CheckInputs(LogisticRegressionParameters parameters)
        {
            bool isValid = true;
            isValid &= MatrixUtils.AreRowCountEquals(parameters.Y, parameters.X);
            isValid &= parameters.Y.ColumnCount == 1;
            isValid &= parameters.X.ColumnCount + 1 == parameters.ThetaInit.RowCount;
            return isValid;
        }

        protected override LogisticRegressionResult DoCompute(LogisticRegressionParameters parameters)
        {
            Func<LogisticRegressionParameters, Matrix, Matrix, Matrix> computeTheta;
            if (parameters.Lambda == 0)
            {
                computeTheta = ComputeTheta;
            }
            else
            {
                computeTheta = ComputeThetaRegularized;
            }

            var theta = parameters.ThetaInit;
            var costs = new List<double>();
            var x = parameters.X;
            var result = new LogisticRegressionResult();

            x = MatrixUtils.Concatenate(MatrixUtils.Ones(parameters.M, 1), x);

            costs.Add(ComputeCost(x, parameters.Y, theta, parameters.M));

            for (int i = 0; i < parameters.IterationCount; i++)
            {
                theta = computeTheta(parameters, x, theta);
                costs.Add(ComputeCost(x, parameters.Y, theta, parameters.M));
            }

            result.Costs = costs;
            result.Theta = theta;

            return result;
        }

        Matrix ComputeTheta(LogisticRegressionParameters parameters, Matrix x, Matrix theta)
        {
            return ComputeTetha(x, parameters.Y, theta);
        }


        Matrix ComputeTetha(Matrix x, Matrix y, Matrix theta)
        {
            var z = x * theta;
            var h = MatrixUtils.Sigmoid(z);

            var gradient = x.Transpose() * (h - y);

            var hessian = new Matrix(x.ColumnCount, x.ColumnCount);
            for (int i = 0; i < x.RowCount; i++)
            {
                hessian += h[i, 0] * (1 - h[i, 0]) * x.GetRow(i) * x.GetRow(i).Transpose();
            }

            var solver = new SolveEquationByGauss();

            var dTheta = solver.Compute(new SolveEquationByGaussParameter { A = hessian, b = -1 * gradient, EPSILON = 10e-12 });

            return dTheta + theta;
        }

        Matrix ComputeThetaRegularized(LogisticRegressionParameters parameters, Matrix x, Matrix theta)
        {
            return ComputeThetaRegularized(x, parameters.Y, theta, parameters.Lambda, parameters.N);
        }

        Matrix ComputeThetaRegularized(Matrix x, Matrix y, Matrix theta, double lambda, int n)
        {
            var z = x * theta;
            var h = MatrixUtils.Sigmoid(z);
            var l = MatrixUtils.Identity(n).Set(0, 0, 0);

            var gradient = x.Transpose() * (h - y) + (lambda * l * theta);

            var hessian = new Matrix(x.ColumnCount, x.ColumnCount);
            for (int i = 0; i < x.RowCount; i++)
            {
                hessian += h[i, 0] * (1 - h[i, 0]) * x.GetRow(i) * x.GetRow(i).Transpose();
            }

            hessian += hessian + (lambda * l);

            var solver = new SolveEquationByGauss();

            var dTheta = solver.Compute(new SolveEquationByGaussParameter { A = hessian, b = -1 * gradient, EPSILON = 10e-12 });

            return dTheta + theta;
        }

        double ComputeCost(Matrix x, Matrix y, Matrix theta, int m)
        {
            var z = x * theta;
            var h = MatrixUtils.Sigmoid(z);
            var cost =
                (y.Transpose() * MatrixUtils.Log(h)
                + ((1.0 - y).Transpose() * MatrixUtils.Log(1.0 - h)));

            cost = -(1.0 / m) * cost;

            if (!MatrixUtils.IsScalar(cost))
            {
                throw new Exception("Cost matrix must be scalar.");
            }

            return cost[0, 0];
        }
    }

    public class LogisticRegressionParameters
    {
        public Matrix X { get; set; }
        public Matrix Y { get; set; }
        public Matrix ThetaInit { get; set; }
        public int M { get { return X.RowCount; } }
        public int N { get { return X.ColumnCount + 1; } }
        public int IterationCount { get; set; }
        public double Lambda { get; set; } = 0;
    }

    public class LogisticRegressionResult
    {
        public Matrix Theta { get; set; }
        public List<double> Costs { get; set; }
    }
}
using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Arithmetic;
using System;
using System.Collections.Generic;

namespace DataAnalysis.Core.Regression
{
    public class LinearRegression : Algorithm<LinearRegressionParameters, LinearRegressionResult>
    {
        protected override bool CheckInputs(LinearRegressionParameters parameters)
        {
            bool isValid = true;
            isValid &= MatrixUtils.AreRowCountEquals(parameters.Y, parameters.X);
            isValid &= parameters.Y.ColumnCount == 1;
            isValid &= parameters.X.ColumnCount + 1 == parameters.ThetaInit.RowCount;
            return isValid;
        }

        protected override LinearRegressionResult DoCompute(LinearRegressionParameters parameters)
        {
            Func<LinearRegressionParameters, Matrix, Matrix, Matrix> computeTheta;
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
            var result = new LinearRegressionResult();

            if (parameters.Normalized)
            {
                result.Mean = MatrixUtils.Mean(parameters.X);
                result.StandardDeviation = MatrixUtils.StandardDeviation(parameters.X);
                x = MatrixUtils.Normalize(parameters.X, result.Mean, result.StandardDeviation, parameters.M);
            }

            x = MatrixUtils.Concatenate(MatrixUtils.Ones(parameters.M, 1), x);

            for (int i = 0; i < parameters.IterationCount; i++)
            {
                theta = computeTheta(parameters, x, theta);
                costs.Add(ComputeCost(x, parameters.Y, theta, parameters.M));
            }

            result.Costs = costs;
            result.Theta = theta;

            return result;
        }

        Matrix ComputeTheta(LinearRegressionParameters parameters, Matrix x, Matrix theta)
        {
            return ComputeTetha(x, parameters.Y, theta, parameters.Alpha, parameters.M);
        }

        Matrix ComputeTetha(Matrix x, Matrix y, Matrix theta, double alpha, int m)
        {
            var sum = ((x * theta) - y).Transpose() * x / m;
            return theta - (alpha * sum.Transpose());
        }

        Matrix ComputeThetaRegularized(LinearRegressionParameters parameters, Matrix x, Matrix theta)
        {
            return ComputeThetaRegularized(x, parameters.Y, theta, parameters.Alpha, parameters.M, parameters.Lambda);
        }

        Matrix ComputeThetaRegularized(Matrix x, Matrix y, Matrix theta, double alpha, int m, double lambda)
        {
            var sum = ((x * theta) - y).Transpose() * x / m;
            var thetaReg = new Matrix(theta.ToMatrixDouble()).Set(0, 0, 0);
            var sumReg = lambda * thetaReg / m;
            sum = sum.Transpose() + sumReg;
            return theta - (alpha * sum);
        }

        double ComputeCost(Matrix x, Matrix y, Matrix theta, int m)
        {
            var cost = (x * theta - y).Transpose() * (x * theta - y) / (2 * m);

            if (!MatrixUtils.IsScalar(cost))
            {
                throw new Exception("Cost matrix must be scalar.");
            }

            return cost[0, 0];
        }
    }

    public class LinearRegressionParameters
    {
        public Matrix X { get; set; }
        public Matrix Y { get; set; }
        public Matrix ThetaInit { get; set; }
        public double Alpha { get; set; }
        public int IterationCount { get; set; }
        public int M { get { return X.RowCount; } }
        public bool Normalized { get; set; } = false;
        public double Lambda { get; set; } = 0;
    }

    public class LinearRegressionResult
    {
        public Matrix Theta { get; set; }
        public List<double> Costs { get; set; }
        public Matrix Mean { get; set; }
        public Matrix StandardDeviation { get; set; }
    }
}
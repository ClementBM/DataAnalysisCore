using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Arithmetic;
using System;
using System.Collections.Generic;
using mu = DataAnalysis.Core.Algebra.MatrixUtils;

namespace DataAnalysis.Core.NeuralNetwork
{
    public class SimpleNeuralNetwork : Algorithm<SimpleNeuralNetworkParameters, SimpleNeuralNetworkResult>
    {
        protected override bool CheckInputs(SimpleNeuralNetworkParameters parameters)
        {
            bool isValid = true;
            isValid &= mu.AreRowCountEquals(parameters.Y, parameters.X);
            isValid &= parameters.Y.ColumnCount == parameters.LabelCount;
            return isValid;
        }

        protected override SimpleNeuralNetworkResult DoCompute(SimpleNeuralNetworkParameters parameters)
        {
            Matrix theta1;
            Matrix theta2;
            if (parameters.Theta1Init != null)
            {
                theta1 = parameters.Theta1Init;
            }
            else
            {
                theta1 = mu.Random(parameters.HiddenLayerSize, parameters.InputLayerSize + 1);
            }
            if (parameters.Theta2Init != null)
            {
                theta2 = parameters.Theta2Init;
            }
            else
            {
                theta2 = mu.Random(parameters.LabelCount, parameters.HiddenLayerSize + 1);
            }

            var costs = new List<double>();

            for (int i = 0; i < parameters.IterationCount; i++)
            {
                var result = Propagation(theta1, theta2, parameters.X, parameters.Y, parameters.M, parameters.Lambda, parameters.LabelCount, parameters.HiddenLayerSize);
                costs.Add(result.Cost);

                theta1 -= parameters.Alpha * result.Theta1Grad;
                theta2 -= parameters.Alpha * result.Theta2Grad;
            }

            return new SimpleNeuralNetworkResult
            {
                Theta1 = theta1,
                Theta2 = theta2,
                Costs = costs
            };
        }

        /// <param name="theta1">DIM(HiddenLayerSize X InputLayerSize + 1)</param>
        /// <param name="theta2">DIM(LabelCount X HiddenLayerSize + 1)</param>
        /// <param name="x">DIM(InputLayerSize X 1)</param>
        /// <returns></returns>
        PropagationResult Propagation(Matrix theta1, Matrix theta2, Matrix x, Matrix y, int m, double lambda, int labelCount, int hiddenLayerCount)
        {
            var z2 = theta1 * mu.Concatenate(mu.Ones(1, m), x.Transpose(), 2);
            var a2 = mu.Sigmoid(z2);

            var z3 = theta2 * mu.Concatenate(mu.Ones(1, m), a2, 2);
            var h = mu.Sigmoid(z3);

            var cost = ComputeCost(y, h, m, lambda, theta1, theta2);

            var delta3 = h.Transpose() - y;
            var theta2Grad = delta3.Transpose() * mu.Concatenate(mu.Ones(m, 1), a2.Transpose()) / m;
            theta2Grad += mu.Concatenate(mu.Zeros(labelCount, 1), lambda * theta2.GetColumns(1, theta2.ColumnCount - 1) / m);

            var delta2 = mu.ElementWiseMultiply(delta3 * theta2, mu.Concatenate(mu.Ones(m, 1), mu.SigmoidGradient(z2.Transpose())));
            var theta1Grad = delta2.GetColumns(1, delta2.ColumnCount - 1).Transpose() * mu.Concatenate(mu.Ones(m, 1), x) / m;
            theta1Grad += mu.Concatenate(mu.Zeros(hiddenLayerCount, 1), lambda * theta1.GetColumns(1, theta1.ColumnCount - 1) / m);

            return new PropagationResult
            {
                Cost = cost,
                Theta1Grad = theta1Grad,
                Theta2Grad = theta2Grad
            };
        }

        double ComputeCost(Matrix y, Matrix h, int m, double lambda, Matrix theta1, Matrix theta2)
        {
            var yUnrool = y.Unroll(2);
            var cost = -1 * yUnrool.Transpose() * mu.Log(h).Unroll() - (1 - yUnrool).Transpose() * mu.Log(1 - h).Unroll();

            // Régularisation
            cost /= m;

            var theta1Unrolled = theta1.GetColumns(1, theta1.ColumnCount - 1).Unroll();
            var theta2Unrolled = theta2.GetColumns(1, theta2.ColumnCount - 1).Unroll();

            cost += lambda * (theta1Unrolled.Transpose() * theta1Unrolled + theta2Unrolled.Transpose() * theta2Unrolled) / (2 * m);

            if (!mu.IsScalar(cost))
            {
                throw new Exception("Cost matrix must be scalar.");
            }

            return cost[0, 0];
        }

        public IEnumerable<MatrixCell> Predict(Matrix theta1, Matrix theta2, Matrix x, int m)
        {
            var h1 = mu.Sigmoid(mu.Concatenate(mu.Ones(m, 1), x) * theta1.Transpose());
            var h2 = mu.Sigmoid(mu.Concatenate(mu.Ones(m, 1), h1) * theta2.Transpose());

            return h2.Max(2);
        }
    }

    public class PropagationResult
    {
        public Matrix Theta1Grad { get; set; }
        public Matrix Theta2Grad { get; set; }
        public double Cost { get; set; }
    }

    public class SimpleNeuralNetworkParameters
    {
        /// <summary>
        /// Training set.
        /// </summary>
        public Matrix X { get; set; }
        public Matrix Y { get; set; }
        public int M { get { return X.RowCount; } }
        public double Lambda { get; set; }
        public int InputLayerSize { get; set; }
        public int HiddenLayerSize { get; set; }
        public int LabelCount { get; set; }
        public Matrix Theta1Init { get; set; }
        public Matrix Theta2Init { get; set; }

        public int IterationCount { get; set; }
        public double Alpha { get; set; }
    }

    public class SimpleNeuralNetworkResult
    {
        public Matrix Theta1 { get; set; }
        public Matrix Theta2 { get; set; }

        public List<double> Costs { get; set; }
    }
}
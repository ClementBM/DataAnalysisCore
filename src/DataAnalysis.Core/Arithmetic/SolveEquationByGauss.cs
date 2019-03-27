using System;
using DataAnalysis.Core.Algebra;

namespace DataAnalysis.Core.Arithmetic
{
    /// <summary>
    /// From https://www.codeproject.com/Tips/388179/Linear-Equation-Solver-Gaussian-Elimination-Csharp
    /// </summary>
    public class SolveEquationByGauss : Algorithm<SolveEquationByGaussParameter, Matrix>
    {
        protected override bool CheckInputs(SolveEquationByGaussParameter parameters)
        {
            bool isValid = true;
            isValid &= MatrixUtils.AreRowCountEquals(parameters.A, parameters.b);
            isValid &= parameters.b.ColumnCount == 1;
            isValid &= parameters.b.RowCount > 1;
            isValid &= parameters.A.RowCount > 1;
            return isValid;
        }

        protected override Matrix DoCompute(SolveEquationByGaussParameter parameters)
        {
            var ab = MatrixUtils.Concatenate(parameters.A, parameters.b);
            var M = ab.ToMatrixDouble();

            // pivoting
            for (int col = 0; col + 1 < parameters.m; col++)
            {
                if (Math.Abs(M[col, col]) < parameters.EPSILON) // check for zero coefficients
                {
                    // find non-zero coefficient
                    int swapRow = col + 1;
                    for (; swapRow < parameters.m; swapRow++)
                    {
                        if (Math.Abs(M[swapRow, col]) > parameters.EPSILON)
                        {
                            break;
                        }
                    }

                    if (Math.Abs(M[swapRow, col]) > parameters.EPSILON) // found a non-zero coefficient?
                    {
                        // yes, then swap it with the above
                        double[] tmp = new double[parameters.m + 1];
                        for (int i = 0; i < parameters.m + 1; i++)
                        {
                            tmp[i] = M[swapRow, i]; M[swapRow, i] = M[col, i]; M[col, i] = tmp[i];
                        }
                    }
                    else
                    {
                        throw new Exception("The matrix has no unique solution");
                    }
                }
            }

            // elimination
            for (int sourceRow = 0; sourceRow + 1 < parameters.m; sourceRow++)
            {
                for (int destRow = sourceRow + 1; destRow < parameters.m; destRow++)
                {
                    double df = M[sourceRow, sourceRow];
                    double sf = M[destRow, sourceRow];
                    for (int i = 0; i < parameters.m + 1; i++)
                    {
                        M[destRow, i] = M[destRow, i] * df - M[sourceRow, i] * sf;
                    }
                }
            }

            // back-insertion
            for (int row = parameters.m - 1; row >= 0; row--)
            {
                double f = M[row, row];
                if (Math.Abs(f) < parameters.EPSILON)
                {
                    throw new Exception("");
                }

                for (int i = 0; i < parameters.m + 1; i++)
                {
                    M[row, i] /= f;
                }
                for (int destRow = 0; destRow < row; destRow++)
                {
                    M[destRow, parameters.m] -= M[destRow, row] * M[row, parameters.m];
                    M[destRow, row] = 0;
                }
            }
            return new Matrix(M).GetColumn(parameters.n - 1);
        }
    }
    public class SolveEquationByGaussParameter
    {
        public int m { get { return A.RowCount; } }
        public int n { get { return A.ColumnCount + 1; } }
        public Matrix A { get; set; }
        public Matrix b { get; set; }
        public double EPSILON { get; set; } = 10e-10;
    }
}
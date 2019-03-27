using DataAnalysis.Core.Algebra.MathFunc;
using DataAnalysis.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Algebra
{
    public static class MatrixUtils
    {
        public static bool AreColumnCountEquals(Matrix a, Matrix b)
        {
            return a.ColumnCount == b.ColumnCount;
        }
        public static bool AreRowCountEquals(Matrix a, Matrix b)
        {
            return a.RowCount == b.RowCount;
        }

        public static bool AreDimensionEquals(Matrix a, Matrix b)
        {
            return AreColumnCountEquals(a, b) && AreRowCountEquals(a, b);
        }

        public static Matrix Zeros(int rowCount, int columnCount)
        {
            return new Matrix(rowCount, columnCount);
        }
        public static Matrix Random(int rowCount, int columnCount)
        {
            var random = new Random();
            var randomMatrix = new Matrix(rowCount, columnCount);
            return ApplyFunction(randomMatrix, (x) => (2 * random.NextDouble() - 1) * 0.12);
        }

        public static Matrix Ones(int rowCount, int columnCount)
        {
            var values = new ArrayList(rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                values.Add(ArrayListExtensions.Ones(columnCount));
            }

            return new Matrix(values);
        }

        public static Matrix Fill(Matrix matrix, Func<int, int, double> function)
        {
            return new Matrix(matrix.ToMatrixDouble().Fill(function));
        }


        public static bool IsScalar(Matrix matrix)
        {
            return matrix.RowCount == 1 && matrix.ColumnCount == 1;
        }

        public static bool RowVector(Matrix matrix)
        {
            return matrix.ColumnCount == 1;
        }

        public static Matrix Concatenate(Matrix mA, Matrix mB, int direction = 1)
        {
            if (direction != 1 && direction != 2)
            {
                throw new Exception("Direction must be 1 (by row) or 2 (by column)");
            }
            if (direction == 1)
            {
                if (!AreRowCountEquals(mA, mB))
                {
                    throw new Exception("Matrices must have the same rows count");
                }
                var result = new Matrix(mA.RowCount, mA.ColumnCount + mB.ColumnCount);
                return ApplyFunction(
                    mA,
                    mB,
                    new MatrixIteratorByRow(result),
                    new MatrixIteratorByColumn(result),
                    (i, j, ma, mb) =>
                    {
                        if (i < ma.RowCount && j < ma.ColumnCount)
                        {
                            return ma[i, j];
                        }

                        return mb[i, j - ma.ColumnCount];
                    });
            }
            if (direction == 2)
            {
                if (!AreColumnCountEquals(mA, mB))
                {
                    throw new Exception("Matrices must have the same columns count");
                }
                var result = new Matrix(mA.RowCount + mB.RowCount, mA.ColumnCount);
                return ApplyFunction(
                    mA,
                    mB,
                    new MatrixIteratorByRow(result),
                    new MatrixIteratorByColumn(result),
                    (i, j, ma, mb) =>
                    {
                        if (i < ma.RowCount && j < ma.ColumnCount)
                        {
                            return ma[i, j];
                        }

                        return mb[i - ma.RowCount, j];
                    });
            }

            throw new Exception();
        }

        public static Matrix ElementWiseDivide(Matrix a, Matrix b)
        {
            if (!AreDimensionEquals(a, b))
            {
                throw new Exception("Matrices must have the same dimension");
            }

            return ApplyFunction(
                   a,
                   b,
                   new MatrixIteratorByRow(a),
                   new MatrixIteratorByColumn(b),
                   (i, j, ma, mb) =>
                   {
                       return ma[i, j] / mb[i, j];
                   });
        }

        public static Matrix ElementWiseMultiply(Matrix a, Matrix b)
        {
            if (!AreDimensionEquals(a, b))
            {
                throw new Exception("Matrices must have the same dimension");
            }

            return ApplyFunction(
                   a,
                   b,
                   new MatrixIteratorByRow(a),
                   new MatrixIteratorByColumn(b),
                   (i, j, ma, mb) =>
                   {
                       return ma[i, j] * mb[i, j];
                   });
        }

        public static Matrix Normalize(Matrix x, Matrix mean, Matrix standardDeviation, int m)
        {
            return ElementWiseDivide(x - Ones(m, 1) * mean, Ones(m, 1) * standardDeviation);
        }

        public static Matrix ApplyFunction(
            Matrix a,
            Matrix b,
            MatrixIteratorByRow matrixIteratorByRow,
            MatrixIteratorByColumn matrixIteratorByColumn,
            Func<int, int, Matrix, Matrix, double> function)
        {
            Matrix c = new Matrix(matrixIteratorByRow.Matrix.RowCount, matrixIteratorByColumn.Matrix.ColumnCount);
            for (int i = 0; i < matrixIteratorByRow.Matrix.RowCount; i++)
            {
                for (int j = 0; j < matrixIteratorByColumn.Matrix.ColumnCount; j++)
                {
                    c[i, j] = function(i, j, a, b);
                }
            }
            return c;
        }

        public static Matrix ApplyFunction(
            Matrix a,
            Func<double, double> function)
        {
            Matrix c = new Matrix(a.RowCount, a.ColumnCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    c[i, j] = function(a[i, j]);
                }
            }
            return c;
        }

        public static Matrix VectorToBinaryMatrix(int[] x, int labelCount)
        {
            var y = Zeros(x.Count(), labelCount);

            for (int i = 0; i < x.Count(); i++)
            {
                y[i, x[i] - 1] = 1;
            }
            return y;
        }

        public static Matrix VectorToBinaryMatrix(int[] x)
        {
            var count = x.GroupBy(k => k).Count();
            return VectorToBinaryMatrix(x,count);
        }

        public static Matrix Aggregate(Matrix matrix, Func<Matrix, int, int, double, double> function, int direction = 1)
        {
            if (direction != 1 && direction != 2)
            {
                throw new Exception("Direction must be 1 (by row) or 2 (by column)");
            }
            if (direction == 1)
            {
                Matrix result = new Matrix(1, matrix.ColumnCount);
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    for (int i = 0; i < matrix.RowCount; i++)
                    {
                        result[0, j] = function(matrix, i, j, result[0, j]);
                    }
                }
                return result;
            }
            else if (direction == 2)
            {
                Matrix result = new Matrix(matrix.RowCount, 1);
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnCount; j++)
                    {
                        result[i, 0] = function(matrix, i, j, result[i, 0]);
                    }
                }
                return result;
            }
            throw new Exception();
        }

        public static Matrix Mean(Matrix matrix, int direction = 1)
        {
            if (matrix.RowCount < 1)
            {
                throw new Exception("Matrix must not be empty");
            }

            if (direction == 1)
            {
                return new Matrix(matrix.Columns.Mean(), 2);
            }
            else if (direction == 2)
            {
                return new Matrix(matrix.Rows.Mean(), 1);
            }

            throw new Exception();
        }

        public static Matrix StandardDeviation(Matrix matrix, int direction = 1)
        {
            if (matrix.RowCount < 1)
            {
                throw new Exception("Matrix must not be empty");
            }

            if (direction == 1)
            {
                return new Matrix(matrix.Columns.StandardDeviation(), 2);
            }
            else if (direction == 2)
            {
                return new Matrix(matrix.Rows.StandardDeviation(), 1);
            }

            throw new Exception();
        }

        public static Matrix Sigmoid(Matrix x)
        {
            return ApplyFunction(x, MathUtils.Sigmoid);
        }

        public static Matrix SigmoidGradient(Matrix x)
        {
            var sigmoidX = Sigmoid(x);
            return ElementWiseMultiply(sigmoidX, (1 - sigmoidX));
        }


        public static Matrix Log(Matrix x)
        {
            return ApplyFunction(x, Math.Log);
        }

        public static Matrix Diag(double[] diagonalVector)
        {
            int dim = diagonalVector.Count();

            if (dim == 0)
            {
                throw new ArgumentException("diagonalVector.Count() must be greater than 1");
            }

            Matrix M = new Matrix(dim, dim);

            for (int i = 1; i <= dim; i++)
            {
                M[i, i] = diagonalVector[i];
            }

            return M;
        }

        public static Matrix Diag(int dim, double diagonalValue)
        {
            Matrix M = new Matrix(dim, dim);

            for (int i = 0; i < dim; i++)
            {
                M[i, i] = diagonalValue;
            }

            return M;
        }

        public static Matrix Identity(int n)
        {
            return Diag(n, 1);
        }
    }
}

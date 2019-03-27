namespace DataAnalysis.Core.Algebra
{
    public partial class Matrix
    {
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (MatrixUtils.AreDimensionEquals(A, B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[i, j] - mb[i, j]);
            }
            else if (MatrixUtils.IsScalar(A) && !MatrixUtils.IsScalar(B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(B), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[0, 0] - mb[i, j]);
            }
            else if (!MatrixUtils.IsScalar(A) && MatrixUtils.IsScalar(B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(A), (i, j, ma, mb) => ma[i, j] - mb[0, 0]);
            }
            else if (MatrixUtils.RowVector(A) && A.RowCount == B.RowCount)
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(B), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[i, 0] - mb[i, j]);
            }
            else if (MatrixUtils.RowVector(B) && A.RowCount == B.RowCount)
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(A), (i, j, ma, mb) => ma[i, j] - mb[i, 0]);
            }
            return null;
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (MatrixUtils.AreDimensionEquals(A, B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[i, j] + mb[i, j]);
            }
            else if (MatrixUtils.IsScalar(A) && !MatrixUtils.IsScalar(B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(B), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[0, 0] + mb[i, j]);
            }
            else if (!MatrixUtils.IsScalar(A) && MatrixUtils.IsScalar(B))
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(A), (i, j, ma, mb) => ma[i, j] + mb[0, 0]);
            }
            else if (MatrixUtils.RowVector(A) && A.RowCount == B.RowCount)
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(B), new MatrixIteratorByColumn(B), (i, j, ma, mb) => ma[i, 0] + mb[i, j]);
            }
            else if (MatrixUtils.RowVector(B) && A.RowCount == B.RowCount)
            {
                return MatrixUtils.ApplyFunction(A, B, new MatrixIteratorByRow(A), new MatrixIteratorByColumn(A), (i, j, ma, mb) => ma[i, j] + mb[i, 0]);
            }
            return null;
        }

        public static Matrix operator *([MatrixIterator(Iterators.ByColumn)]Matrix A, [MatrixIterator(Iterators.ByRow)]Matrix B)
        {
            var matrixIteratorByRow = new MatrixIteratorByRow(A);
            var matrixIteratorByColumn = new MatrixIteratorByColumn(B);
            return MatrixUtils.ApplyFunction(A, B, matrixIteratorByRow, matrixIteratorByColumn, (i, j, ma, mb) => MatrixElement.Dot(ma.Row(i), mb.Column(j)));
        }

        public static Matrix operator /(Matrix matrix, int scalar)
        {
            var scalarMatrix = new Matrix(new double[1, 1] { { scalar } });
            return MatrixUtils.ApplyFunction(
                matrix,
                scalarMatrix,
                new MatrixIteratorByRow(matrix),
                new MatrixIteratorByColumn(matrix),
                (i, j, ma, mb) => ma[i, j] / mb[0, 0]);
        }
        public static Matrix operator *(double scalar, Matrix matrix)
        {
            var scalarMatrix = new Matrix(new double[1, 1] { { scalar } });
            return MatrixUtils.ApplyFunction(
                matrix,
                scalarMatrix,
                new MatrixIteratorByRow(matrix),
                new MatrixIteratorByColumn(matrix),
                (i, j, ma, mb) => ma[i, j] * mb[0, 0]);
        }

        public static Matrix operator -(double scalar, Matrix matrix)
        {
            return MatrixUtils.ApplyFunction(
                matrix,
                (x) => scalar - x);
        }

        public static Matrix operator +(double scalar, Matrix matrix)
        {
            return MatrixUtils.ApplyFunction(
                            matrix,
                            (x) => scalar + x);
        }
        public static Matrix operator -(Matrix matrix, double scalar)
        {
            return MatrixUtils.ApplyFunction(
                matrix,
                (x) => x - scalar);
        }

        public static Matrix operator +(Matrix matrix, double scalar)
        {
            return MatrixUtils.ApplyFunction(
                            matrix,
                            (x) => x + scalar);
        }
    }
}
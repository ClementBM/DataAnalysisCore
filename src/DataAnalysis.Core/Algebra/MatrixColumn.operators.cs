using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Algebra
{
    public partial class MatrixColumn
    {
        public static MatrixColumn operator +(MatrixColumn x, MatrixColumn y)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentOutOfRangeException($"{nameof(x)} and {nameof(y)}", "Matrix columns does not have the same dimension");
            }

            double[] sumedColumn = new double[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                sumedColumn[i] = x[i] + y[i];
            }
            return sumedColumn;
        }

        public static MatrixColumn operator -(MatrixColumn x, MatrixColumn y)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentOutOfRangeException($"{nameof(x)} and {nameof(y)}", "Matrix columns does not have the same dimension");
            }

            double[] subtractedColumn = new double[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                subtractedColumn[i] = x[i] - y[i];
            }
            return subtractedColumn;
        }

        public static MatrixColumn operator *(double scalar, MatrixColumn matrixColumn)
        {
            var scalarMatrix = new Matrix(new double[1, 1] { { scalar } });
            return MatrixUtils.ApplyFunction(
                matrixColumn.ToMatrix(),
                scalarMatrix,
                new MatrixIteratorByRow(matrixColumn.ToMatrix()),
                new MatrixIteratorByColumn(matrixColumn.ToMatrix()),
                (i, j, ma, mb) => ma[i, j] * mb[0, 0]).Column(0);
        }
    }
}

using System.Collections;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public partial class MatrixRow
    {
        public static implicit operator double[] (MatrixRow x)
        {
            return x.Values.Cast<double>().ToArray();
        }

        public static implicit operator MatrixRow(double[] x)
        {
            return new MatrixRow(x);
        }

        public static implicit operator Matrix(MatrixRow x)
        {
            return new Matrix(x.Values);
        }
    }
}

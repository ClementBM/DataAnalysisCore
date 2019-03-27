using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Algebra
{
    public partial class MatrixColumn
    {
        public static implicit operator double[] (MatrixColumn x)
        {
            return x.Values.Cast<double>().ToArray();
        }

        public static implicit operator MatrixColumn(double[] x)
        {
            return new MatrixColumn(x);
        }

        public static implicit operator MatrixColumn(List<double> x)
        {
            return new MatrixColumn(x.ToArray<double>());
        }

        public static implicit operator Matrix(MatrixColumn x)
        {
            return new Matrix(x.Values);
        }
    }
}

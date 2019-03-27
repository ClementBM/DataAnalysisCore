using DataAnalysis.Core.Algebra;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class MatrixColumnExtensions
    {
        public static double[] Mean(this IEnumerable<MatrixColumn> matrixColumns)
        {
            return matrixColumns.Select(x => x.Mean).ToArray();
        }

        public static double[] StandardDeviation(this IEnumerable<MatrixColumn> matrixColumns)
        {
            return matrixColumns.Select(x => x.StandardDeviation).ToArray();
        }
    }
}

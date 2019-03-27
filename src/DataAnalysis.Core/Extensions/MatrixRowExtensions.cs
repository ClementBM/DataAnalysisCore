using DataAnalysis.Core.Algebra;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class MatrixRowExtensions
    {
        public static double[] Mean(this IEnumerable<MatrixRow> matrixRows)
        {
            return matrixRows.Select(x => x.Mean).ToArray();
        }

        public static double[] StandardDeviation(this IEnumerable<MatrixRow> matrixRows)
        {
            return matrixRows.Select(x => x.StandardDeviation).ToArray();
        }
    }
}

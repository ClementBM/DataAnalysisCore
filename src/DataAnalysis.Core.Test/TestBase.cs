using DataAnalysis.Core.Algebra;
using System.Collections.Generic;

namespace DataAnalysis.Test
{
    public class TestBase
    {
        protected const double Epsilon = 10e-6;

        protected void AssertMatrixAreEqual(Matrix expected, Matrix actual)
        {
            AssertExtensions.MatrixAreEqual(expected, actual, Epsilon);
        }

        protected void AssertMatrixAreEqual(double[,] expected, Matrix actual)
        {
            AssertExtensions.MatrixAreEqual(expected, actual, Epsilon);
        }
        protected void AssertMatrixAreEqual(double[,] expected, double[,] actual)
        {
            AssertExtensions.MatrixAreEqual(expected, actual, Epsilon);
        }

        protected void AssertArrayAreEqual(double[] expected, double[] actual)
        {
            AssertExtensions.ArrayAreEqual(expected, actual, Epsilon);
        }

        protected void AssertMatrixAreEqual(double[,] expected, List<double[]> actual)
        {
            AssertExtensions.ArrayAreEqual(expected, actual, Epsilon);
        }
    }
}

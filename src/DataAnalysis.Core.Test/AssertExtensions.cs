using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Test
{
    public static class AssertExtensions
    {
        public static void AreAboutEqual(double[,] expected, double[,] actual, double epsilon, string message)
        {
            int mExp = expected.GetLength(0); // nombre de points, nombre de lignes
            int nEx = expected.GetLength(1); // nombre de dimensions, nombre de colonnes

            int mAct = expected.GetLength(0); // nombre de points, nombre de lignes
            int nAct = expected.GetLength(1); // nombre de dimensions, nombre de colonnes

            if (mExp != mAct && nEx != nAct)
            {
                throw new AssertionException($"{message}. Lines count and columns count are not equal.");
            }

            if (mExp != mAct)
            {
                throw new AssertionException($"{message}. Lines count are not equal.");
            }

            if (nEx != nAct)
            {
                throw new AssertionException($"{message}. Columns count are not equal.");
            }

            for (int i = 0; i < mExp; i++)
            {
                var actualVect = actual.Row(i);
                var expectedVect = expected.Row(i);
                AreAboutEqual(expectedVect, actualVect, epsilon);
            }
        }

        public static void AreAboutEqual(double[] expected, double[] actual, double epsilon, string message = null)
        {
            int nExp = expected.Length; // 
            int nAct = actual.Length; // 

            if (nExp != nAct)
            {
                throw new AssertionException($"{message} Array lengths are not equal.");
            }

            int i = 0;
            for (i = 0; i < nExp; i++)
            {
                if (!expected[i].AboutEqual(actual[i], epsilon))
                {
                    break;
                }
            }

            if (i == nExp)
            {
                return;
            }

            // Compare avec le vecteur opposé
            for (i = 0; i < nExp; i++)
            {
                if (!expected[i].AboutEqual(-actual[i], epsilon))
                {
                    break;
                }
            }

            if (i == nExp)
            {
                return;
            }

            // Uniformisation des vecteurs
            expected = expected.Scale();
            actual = actual.Scale();

            for (i = 0; i < nExp; i++)
            {
                if (!expected[i].AboutEqual(actual[i], epsilon))
                {
                    break;
                }
            }

            if (i == nExp)
            {
                return;
            }

            for (i = 0; i < nExp; i++)
            {
                if (!expected[i].AboutEqual(actual[i], epsilon))
                {
                    throw new AssertionException($"{message} Value at index {i} are not equals, [{expected[i]}]!=[{actual[i]}], with epsilon value at {epsilon}.");
                }
            }
        }

        public static void ArrayAreEqual(double[,] expected, List<double[]> actual, double epsilon)
        {
            Assert.AreEqual(expected.RowCount(), actual.Count, "Row count are not equal");
            Assert.AreEqual(expected.ColumnCount(), actual.First().Count(), "Column count are not equal");

            for (int i = 0; i < expected.RowCount(); i++)
            {
                for (int j = 0; j < expected.ColumnCount(); j++)
                {
                    Assert.AreEqual(expected[i, j], actual.ElementAt(i)[j], epsilon, $"For element at [{i},{j}]");
                }
            }
        }

        public static void MatrixAreEqual(Matrix expected, Matrix actual, double epsilon)
        {
            Assert.AreEqual(expected.RowCount, actual.RowCount, "Row count are not equal");
            Assert.AreEqual(expected.ColumnCount, actual.ColumnCount, "Column count are not equal");

            for (int i = 0; i < actual.RowCount; i++)
            {
                for (int j = 0; j < actual.ColumnCount; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j], epsilon, $"For element at [{i},{j}]");
                }
            }
        }

        public static void MatrixAreEqual(double[,] expected, Matrix actual, double epsilon)
        {
            Assert.AreEqual(expected.RowCount(), actual.RowCount, "Row count are not equal");
            Assert.AreEqual(expected.ColumnCount(), actual.ColumnCount, "Column count are not equal");

            for (int i = 0; i < actual.RowCount; i++)
            {
                for (int j = 0; j < actual.ColumnCount; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j], epsilon, $"For element at [{i},{j}]");
                }
            }
        }

        public static void MatrixAreEqual(double[,] expected, double[,] actual, double epsilon)
        {
            Assert.AreEqual(expected.RowCount(), actual.RowCount(), "Row count are not equal");
            Assert.AreEqual(expected.ColumnCount(), actual.ColumnCount(), "Column count are not equal");

            for (int i = 0; i < actual.RowCount(); i++)
            {
                for (int j = 0; j < actual.ColumnCount(); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j], epsilon, $"For element at [{i},{j}]");
                }
            }
        }
        public static void ArrayAreEqual(double[] expected, double[] actual, double epsilon)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Length are not equal");

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], epsilon, $"For element at [{i}]");
            }
        }
    }
}

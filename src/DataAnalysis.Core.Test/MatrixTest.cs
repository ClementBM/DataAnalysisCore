using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Extensions;
using DataAnalysis.Test.Data;
using NUnit.Framework;
using System.Linq;

namespace DataAnalysis.Test
{
    [TestFixture]
    public class MatrixTest : TestBase
    {
        //[Test]
        //public void ConstructorFromVectorListTest()
        //{
        //    var datas = GetTestDataByName(Engine153Test8Data);

        //    var matrix = new ChemicalAnalysisMatrix(datas.Select(x => x.Chemical), datas.Select(x => x.Km));

        //    AssertMatrixAreEqual(Engine153TestMatrix, matrix);
        //}

        //[Test]
        //public void ConstructorFromRowListTest()
        //{
        //    var datas = GetTestDataByName(Engine153Test8Data);
        //    var matrixFrom = new ChemicalAnalysisMatrix(datas.Select(x => x.Chemical), datas.Select(x => x.Km));
        //    var rows = matrixFrom.ToRowCollection();
        //    var matrix = new Matrix(rows.ToList());

        //    AssertMatrixAreEqual(Engine153TestMatrix, matrix);
        //}

        [Test]
        public void MatrixScaleTest()
        {
            var matrix = new Matrix(StatisticDatas.MatrixToScale);
            var matrixScaledActual = matrix.Scale();

            var matrixScaledExpected = StatisticDatas.MatrixScaled.ReplaceElementIf((double d) => double.IsNaN(d), double.MaxValue);
            matrixScaledActual = matrixScaledActual.ReplaceElementIf((double d) => double.IsNaN(d), double.MaxValue);

            AssertMatrixAreEqual(matrixScaledExpected, matrixScaledActual);
        }

        [Test]
        public void MatrixCovarianceTest()
        {
            var matrixToCov = new Matrix(StatisticDatas.MatrixToCovariance.Transpose());
            var actualCovarianceMatrix = matrixToCov.Covariance();

            AssertMatrixAreEqual(StatisticDatas.CovarianceMatrix, actualCovarianceMatrix);
        }

        [Test]
        public void MatrixUpperTriTest()
        {
            var upperTri = StatisticDatas.SquareMatrix44.UpperTri();

            double[] expected = { 4, 5, 16, 6, 88, 83 };

            AssertArrayAreEqual(expected.OrderBy(x => x).ToArray(), upperTri.OrderBy(x => x).ToArray());
        }

        [Test]
        public void MatrixLowerTriTest()
        {
            var lowerTri = StatisticDatas.SquareMatrix44.LowerTri();

            double[] expected = { 10, 55, 37, 64, 59, 36 };

            AssertArrayAreEqual(expected.OrderBy(x => x).ToArray(), lowerTri.OrderBy(x => x).ToArray());
        }

        [Test]
        public void MatrixProductTest()
        {
            var mA = new Matrix(StatisticDatas.SquareMatrix44);
            var mB = new Matrix(StatisticDatas.RectangleMatrix46);

            var result = mA * mB;
            var expectedResult = new double[,] { { 538, 728, 71, 96, 83, 74 }, { 4276, 6405, 461, 728, 370, 534 }, { 9101, 12047, 1406, 1616, 1646, 1300 }, { 3125, 3971, 573, 727, 959, 747 } };

            AssertMatrixAreEqual(expectedResult, result);
        }

        [Test]
        public void MatrixSubtractTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixB = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixResult = matrixA - matrixB;

            var matrixExpected = MatrixUtils.Zeros(4, 4);
            AssertMatrixAreEqual(matrixExpected, matrixResult);
        }

        [Test]
        public void MatrixSubtractRowVectorTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixB = new Matrix(StatisticDatas.SquareMatrix41);
            var matrixResult = matrixA - matrixB;

            double[,] matrixExpectedArray = {
                { 0 ,3 ,4 , 5  },
                { 0 ,3 ,6 , 78 },
                { 0 ,9 ,42, 28 },
                { 0 ,22,-1,-23 }
            };

            var matrixExpected = new Matrix(matrixExpectedArray);
            AssertMatrixAreEqual(matrixExpected, matrixResult);
        }

        [Test]
        public void RowVectorSubtractMatrixTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixB = new Matrix(StatisticDatas.SquareMatrix41);
            var matrixResult = matrixB - matrixA;

            double[,] matrixExpectedArray = {
                { 0 ,-3 ,-4 , -5 },
                { 0 ,-3 ,-6 ,-78 },
                { 0 ,-9 ,-42,-28 },
                { 0 ,-22,  1, 23 }
            };

            var matrixExpected = new Matrix(matrixExpectedArray);
            AssertMatrixAreEqual(matrixExpected, matrixResult);
        }

        [Test]
        public void ScalarSubtractMatrixTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixB = new Matrix(new double[1, 1] { { 3 } });
            var matrixResult = matrixB - matrixA;

            double[,] matrixExpectedArray = {
                {  2, -1, -2, -3 },
                { -7,-10,-13,-85 },
                {-52,-61,-94,-80 },
                {-34,-56,-33,-11 }
            };

            var matrixExpected = new Matrix(matrixExpectedArray);
            AssertMatrixAreEqual(matrixExpected, matrixResult);
        }

        [Test]
        public void MatrixSubtractScalarTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var matrixB = new Matrix(new double[1, 1] { { 3 } });
            var matrixResult = matrixA - matrixB;

            double[,] matrixExpectedArray = {
                { -2, 1, 2,3  },
                {  7,10,13,85 },
                { 52,61,94,80 },
                { 34,56,33,11 }
            };

            var matrixExpected = new Matrix(matrixExpectedArray);
            AssertMatrixAreEqual(matrixExpected, matrixResult);
        }

        [Test]
        public void MatrixMeanTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);
            var mean = MatrixUtils.Mean(matrixA);

            var meanExpected = new Matrix(new double[,] { { 25.7500, 35.0000, 38.5000, 47.7500 } });
            AssertMatrixAreEqual(meanExpected, mean);
        }

        [Test]
        public void MatrixNormTest()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);

            var resultExpected = new double[,]
            {
                { -0.998625428903524, -1.00366205776917 , -0.815941377926610 , -0.954072856467937 },
                { -0.635488909302243, -0.712276299061994, -0.548020328458171 ,  0.919794789768490 },
                { 1.18019368870416  ,  0.938909666945355, 1.42485285399124   ,  0.805534567437000 },
                { 0.453920649501602 ,  0.777028689885811, -0.0608911476064634, -0.771256500737554 }
            };

            var mean = MatrixUtils.Mean(matrixA);
            var standardDeviation = MatrixUtils.StandardDeviation(matrixA);

            var result = MatrixUtils.Normalize(matrixA, mean, standardDeviation, matrixA.RowCount);

            AssertMatrixAreEqual(resultExpected, result);
        }

        [Test]
        public void MatrixUnroll2Test()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);

            var resultExpected = new double[,]
            {
                { 1 ,4 ,5 ,6, 10,13,16,88, 55,64,97,83, 37,59,36,14 }
            };

            var unrolled = matrixA.Unroll(2);

            AssertMatrixAreEqual(resultExpected.Transpose(), unrolled);
        }

        [Test]
        public void MatrixUnroll1Test()
        {
            var matrixA = new Matrix(StatisticDatas.SquareMatrix44);

            var resultExpected = new double[,]
            { { 1  },
              { 10 },
              { 55 },
              { 37 },
              { 4  } ,
              { 13 },
              { 64 },
              { 59 },
              { 5  },
              { 16 },
              { 97 },
              { 36 },
              { 6  },
              { 88 },
              { 83 },
              { 14 }};

            var unrolled = matrixA.Unroll(1);

            AssertMatrixAreEqual(resultExpected, unrolled);
        }
    }
}
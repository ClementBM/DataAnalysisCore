using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.Regression;
using DataAnalysis.DataBinding;
using DataAnalysis.Test.DataBindingTests;
using NUnit.Framework;
using System.Linq;

namespace DataAnalysis.Test
{
    [TestFixture]
    public class LogisticRegressionTest : TestBase
    {
        [Test]
        public void OneVariableLogisticRegressionTest()
        {
            var revenuePerPopulationFilePath = FileManagerTest.GetTestDataByName("ResultExamsAdmission.txt");
            var reader = new CsvReader<ResultExamsAdmission>();
            var revenuePerPopulationRows = reader.GetData(revenuePerPopulationFilePath, ",");

            var dataMatrix = new Matrix(revenuePerPopulationRows.Select(x => new ResultExamsAdmissionRow(x)));
            var dataY = dataMatrix.Columns[0].ToMatrix();
            var dataX = dataMatrix.ToMatrix(new int[2] { 1, 2 });

            var logisticRegression = new LogisticRegression();
            var logisticRegressionParameters = new LogisticRegressionParameters
            {
                IterationCount = 100,
                ThetaInit = new Matrix(new double[3, 1] { { 0 }, { 0 }, { 0 } }),
                X = dataX,
                Y = dataY
            };

            var output = logisticRegression.Compute(logisticRegressionParameters);

            var expectedOutput = new Matrix(new double[3, 1] { { -25.161333566639530 }, { 0.206231713293983 }, { 0.201471600441963 } });

            AssertMatrixAreEqual(expectedOutput, output.Theta);
        }

        [Test]
        [TestCase(1)]
        public void OneVariableLogisticRegressionRegularizedTest(double lambda)
        {
            var revenuePerPopulationFilePath = FileManagerTest.GetTestDataByName("ResultExamsAdmission.txt");
            var reader = new CsvReader<ResultExamsAdmission>();
            var revenuePerPopulationRows = reader.GetData(revenuePerPopulationFilePath, ",");

            var dataMatrix = new Matrix(revenuePerPopulationRows.Select(x => new ResultExamsAdmissionRow(x)));
            var dataY = dataMatrix.Columns[0].ToMatrix();
            var dataX = dataMatrix.ToMatrix(new int[2] { 1, 2 });

            var logisticRegression = new LogisticRegression();
            var logisticRegressionParameters = new LogisticRegressionParameters
            {
                IterationCount = 100,
                ThetaInit = new Matrix(new double[3, 1] { { 0 }, { 0 }, { 0 } }),
                X = dataX,
                Y = dataY,
                Lambda = lambda
            };

            var output = logisticRegression.Compute(logisticRegressionParameters);

            var expectedOutput = new Matrix(new double[3, 1] { { -25.052148050018360 }, { 0.205354461994741 }, { 0.200583555605940 } });

            AssertMatrixAreEqual(expectedOutput, output.Theta);
        }
    }
}

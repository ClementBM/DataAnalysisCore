using DataAnalysis.Core.Algebra;
using DataAnalysis.Core.NeuralNetwork;
using DataAnalysis.DataBinding;
using NUnit.Framework;
using System.Linq;

namespace DataAnalysis.Test
{
    [TestFixture]
    public class NeuralNetworkTest : TestBase
    {
        [Test]
        [Ignore("Too expensive")]
        public void SimpleNeuralNetworkTest()
        {
            var takeN = 100;

            var xPath = FileManagerTest.GetTestDataByName("HandwrittenDigit", "dataX.csv");
            var doubleReader = new CsvDoubleReader();
            var xArray = doubleReader.GetData(xPath, ",");
            var x = new Matrix(xArray.Take(takeN));

            var yPath = FileManagerTest.GetTestDataByName("HandwrittenDigit", "datay.csv");
            var intReader = new CsvIntReader();
            var yArray = intReader.GetData(yPath, ",").Take(takeN).ToArray();
            var y = MatrixUtils.VectorToBinaryMatrix(yArray.Select(z => z[0]).ToArray(), 10);

            var neuralNetworkParameters = new SimpleNeuralNetworkParameters()
            {
                Alpha = 1,
                HiddenLayerSize = 25,
                InputLayerSize = 400,
                IterationCount = 10,
                LabelCount = 10,
                Lambda = 1,
                X = x,
                Y = y,
            };

            var neuralNetwork = new SimpleNeuralNetwork();
            var neuralNetworkResults = neuralNetwork.Compute(neuralNetworkParameters);

            var prediction = neuralNetwork.Predict(neuralNetworkResults.Theta1, neuralNetworkResults.Theta2, x, x.RowCount);
        }

        [Test]
        [Ignore("Too expensive")]
        public void SimpleNeuralNetworkTest2()
        {
            var x = new double[,]
            {
                { 0.1683,-0.1923},
                { 0.1819,-0.1502},
                { 0.0282, 0.0300},
                {-0.1514, 0.1826},
                {-0.1918, 0.1673},
                {-0.0559,-0.0018},
                { 0.1314,-0.1692},
                { 0.1979,-0.1811},
                { 0.0824,-0.0265},
                {-0.1088, 0.1525},
                {-0.2000, 0.1913},
                {-0.1073, 0.0542},
                { 0.0840,-0.1327},
                { 0.1981,-0.1976},
                { 0.1301,-0.0808},
                {-0.0576, 0.1103}
            };

            var y = new int[] { 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1 };

            var yMat = MatrixUtils.VectorToBinaryMatrix(y);

            var theta1 = new double[,]
            {
                { 0.0181  ,  0.0771  ,  0.0358 },
                {-0.1057  , -0.1163  ,  0.0556 },
                {-0.0637  , -0.1097  ,  0.0355 },
                { -0.0352 ,  -0.0794 ,  -0.0118}
            };

            var theta2 = new double[,]
            {
               { 0.0113  ,  0.0448  ,  0.0673  , -0.0032  ,  0.0020 },
               {-0.0489  , -0.0760  , -0.1005  , -0.0154  ,  0.0026 },
               { 0.0587  , -0.0316  ,  0.1031  , -0.0128  ,  0.0762 },
               { -0.0747 ,   0.0301 ,   0.0662 ,  -0.0465 ,   0.0708}
            };

            var neuralNetworkParameters = new SimpleNeuralNetworkParameters()
            {
                Alpha = 1,
                HiddenLayerSize = 4,
                InputLayerSize = 2,
                IterationCount = 50,
                LabelCount = 4,
                Lambda = 1,
                X = new Matrix(x),
                Y = yMat,
                Theta1Init = new Matrix(theta1),
                Theta2Init = new Matrix(theta2)
            };

            var neuralNetwork = new SimpleNeuralNetwork();
            var neuralNetworkResults = neuralNetwork.Compute(neuralNetworkParameters);
        }
    }
}
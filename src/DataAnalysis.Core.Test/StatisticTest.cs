using DataAnalysis.Core.Extensions;
using DataAnalysis.Test.Data;
using NUnit.Framework;

namespace DataAnalysis.Test
{
    [TestFixture]
    public class StatisticTest : TestBase
    {
        [Test]
        public void BoxPlotEvenTest()
        {
            var boxPlot = StatisticDatas.BoxPlotDataTest50.BoxPlot(1.5);

            double lowerWhiskerExpected = -1.6474959569274;
            double firstQuartileExpected = -0.422772113564895;
            double medianExpected = 0.0909932084547631;
            double thirdQuartileExpected = 0.554583963268494;
            double upperWhiskerExpected = 1.83853968405904;

            Assert.AreEqual(lowerWhiskerExpected, boxPlot.LowerWhisker, Epsilon, "Lower Whisker");
            Assert.AreEqual(firstQuartileExpected, boxPlot.FirstQuartile, Epsilon, "First Quartile");
            Assert.AreEqual(medianExpected, boxPlot.Median, Epsilon, "Median");
            Assert.AreEqual(thirdQuartileExpected, boxPlot.ThirdQuartile, Epsilon, "Third Quartile");
            Assert.AreEqual(upperWhiskerExpected, boxPlot.UpperWhisker, Epsilon, "Upper Whisker");
        }

        [Test]
        public void BoxPlotOddTest()
        {
            var boxPlot = StatisticDatas.BoxPlotDataTest39.BoxPlot(1.5);

            double lowerWhiskerExpected = -1.81820169182727;
            double firstQuartileExpected = -0.823852833916682;
            double medianExpected = -0.307836681136551;
            double thirdQuartileExpected = 0.246168868457265;
            double upperWhiskerExpected = 1.5530250043316;

            Assert.AreEqual(lowerWhiskerExpected, boxPlot.LowerWhisker, Epsilon, "Lower Whisker");
            Assert.AreEqual(firstQuartileExpected, boxPlot.FirstQuartile, Epsilon, "First Quartile");
            Assert.AreEqual(medianExpected, boxPlot.Median, Epsilon, "Median");
            Assert.AreEqual(thirdQuartileExpected, boxPlot.ThirdQuartile, Epsilon, "Third Quartile");
            Assert.AreEqual(upperWhiskerExpected, boxPlot.UpperWhisker, Epsilon, "Upper Whisker");
        }

        [Test]
        public void BoxPlot129Test()
        {
            var boxPlot = StatisticDatas.BoxPlotDataTest129.BoxPlot(1.5);

            double lowerWhiskerExpected = -2.21093372973404;
            double firstQuartileExpected = -0.782932842155588;
            double medianExpected = 0.0458877321572357;
            double thirdQuartileExpected = 0.651524671771454;
            double upperWhiskerExpected = 2.17314725214995;

            Assert.AreEqual(lowerWhiskerExpected, boxPlot.LowerWhisker, Epsilon, "Lower Whisker");
            Assert.AreEqual(firstQuartileExpected, boxPlot.FirstQuartile, Epsilon, "First Quartile");
            Assert.AreEqual(medianExpected, boxPlot.Median, Epsilon, "Median");
            Assert.AreEqual(thirdQuartileExpected, boxPlot.ThirdQuartile, Epsilon, "Third Quartile");
            Assert.AreEqual(upperWhiskerExpected, boxPlot.UpperWhisker, Epsilon, "Upper Whisker");
        }
    }
}

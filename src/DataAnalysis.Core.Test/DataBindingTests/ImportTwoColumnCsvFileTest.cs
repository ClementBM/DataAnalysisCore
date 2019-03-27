using DataAnalysis.DataBinding;
using NUnit.Framework;
using System.Linq;

namespace DataAnalysis.Test.DataBindingTests
{
    [TestFixture]
    public class ImportTwoColumnCsvFileTest
    {
        [Test]
        public void ImportRevenuePerPopulationTest()
        {
            var revenuePerPopulationFilePath = FileManagerTest.GetTestDataByName("RevenuePerPopulation.txt");
            var reader = new CsvReader<RevenuePerPopulation>();
            var revenuePerPopulationRows = reader.GetData(revenuePerPopulationFilePath, ",");

            Assert.IsNotNull(revenuePerPopulationRows);
            Assert.IsTrue(revenuePerPopulationRows.Count() == 97);
        }
    }
}

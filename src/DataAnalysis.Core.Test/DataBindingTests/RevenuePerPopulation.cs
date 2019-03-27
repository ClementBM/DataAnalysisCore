using DataAnalysis.Core.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Test.DataBindingTests
{
    public class RevenuePerPopulation
    {
        public double Population { get; set; }
        public double Revenue { get; set; }
    }

    public class RevenuePerPopulationRow : RevenuePerPopulation, IRowVector
    {
        public int Count => 2;

        public object Values
        {
            get
            {
                return new double[2] { Population, Revenue };
            }
        }

        public RevenuePerPopulationRow(RevenuePerPopulation revenuePerPopulation)
        {
            Population = revenuePerPopulation.Population;
            Revenue = revenuePerPopulation.Revenue; 
        }
    }
}

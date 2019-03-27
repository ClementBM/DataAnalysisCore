using DataAnalysis.Core.Extensions;
using System.Linq;

namespace DataAnalysis.Core.Statistic
{
    public struct BoxPlot
    {
        public double[] Numbers;

        public double FirstQuartile
        {
            get
            {
                return Numbers.FirstQuartile();
            }
        }

        public double Median
        {
            get
            {
                return Numbers.Median();
            }
        }

        public double ThirdQuartile
        {
            get
            {
                return Numbers.ThirdQuartile();
            }
        }

        public double InterQuartile
        {
            get
            {
                return ThirdQuartile - FirstQuartile;
            }
        }

        public double K;

        public BoxPlot(double[] numbers, double k)
        {
            Numbers = numbers;
            K = k;
        }

        public double LowerWhiskerLimit
        {
            get
            {
                return FirstQuartile - (K * InterQuartile);
            }
        }

        public double LowerWhisker
        {
            get
            {
                double lowerDifference = double.MaxValue;
                double lowerWhisker = Numbers.Min();
                for (int i = 0; i < Numbers.Count(); i++)
                {
                    if (Numbers[i] > LowerWhiskerLimit)
                    {
                        if (lowerDifference > Numbers[i] - LowerWhiskerLimit)
                        {
                            lowerDifference = Numbers[i] - LowerWhiskerLimit;
                            lowerWhisker = Numbers[i];
                        }
                    }
                }
                return lowerWhisker;
            }
        }

        public double UpperWhiskerLimit
        {
            get
            {
                return ThirdQuartile + (K * InterQuartile);
            }
        }

        public double UpperWhisker
        {
            get
            {
                double upperDifference = double.MaxValue;
                double upperWhisker = Numbers.Max();

                for (int i = 0; i < Numbers.Count(); i++)
                {
                    if (Numbers[i] < UpperWhiskerLimit)
                    {
                        if (upperDifference > UpperWhiskerLimit - Numbers[i])
                        {
                            upperDifference = UpperWhiskerLimit - Numbers[i];
                            upperWhisker = Numbers[i];
                        }
                    }
                }
                return upperWhisker;
            }
        }
    }
}

using DataAnalysis.Core.Statistic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class DoubleArrayExtensions
    {
        public static double[] Scale(this double[] vector)
        {
            int n = vector.Length;

            var length = vector.SquaredSum().Sqrt();
            for (int i = 0; i < n; i++)
            {
                vector[i] = vector[i] / length;
            }
            return vector;
        }

        public static bool Equals(double[] xVector, double[] yVector)
        {
            return xVector.Length == yVector.Length;
        }
        public static double[] Add(double[] xVector, double[] yVector)
        {
            if (!Equals(xVector, yVector))
            {
                throw new Exception();
            }
            double[] result = new double[xVector.Length];
            for (int i = 0; i < xVector.Length; i++)
            {
                result[i] = xVector[i] + yVector[i];
            }
            return result;
        }

        public static double[] Contrary(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = -vector[i];
            }
            return vector;
        }

        public static double[] Subtract(this double[] xVector, double[] yVector)
        {
            return Add(xVector, Contrary(yVector));
        }

        public static double SquaredSum(this double[] vector)
        {
            double squaredSum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                squaredSum += vector[i] * vector[i];
            }
            return squaredSum;
        }

        public static double Mean(this IEnumerable<double> values)
        {
            return values.Count() == 0 ? 0 : values.Mean(0, values.Count());
        }

        public static double Mean(this IEnumerable<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values.ElementAt(i);
            }

            return s / (end - start);
        }

        public static double Mean(this double[] values)
        {
            return values.Count() == 0 ? 0 : values.Mean(0, values.Count());
        }

        public static double Median(this double[] values)
        {
            int nthOrder = (values.Length - 1) / 2;
            double median1 = values.NthOrderStatistic(nthOrder);
            if ((values.Length - 1) % 2 == 0)
            {
                return median1;
            }
            double median2 = values.NthOrderStatistic(nthOrder + 1);
            return (median1 + median2) / 2;
        }


        public static double Mean(this double[] values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values.ElementAt(i);
            }

            return s / (end - start);
        }

        public static double Covariance(double[] xVector, double[] yVector)
        {
            if (xVector.Length != yVector.Length)
            {
                throw new Exception();
            }
            double covariance = 0;
            double xMean = xVector.Mean();
            double yMean = yVector.Mean();
            for (int i = 0; i < yVector.Length; i++)
            {
                covariance = (xVector[i] - xMean) * (yVector[i] - yMean);
            }
            return covariance / (xVector.Length - 1);
        }

        public static double Variance(this List<double> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow(values[i] - mean, 2);
            }

            int n = end - start;

            return variance / (n - 1);
        }

        public static double StandardDeviation(this double[] values)
        {
            return values.Count() == 0 ? 0 : values.ToList().StandardDeviation(0, values.Count());
        }

        public static double StandardDeviation(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }

        public static double StandardDeviationUncrooked(this List<double> values)
        {
            if (values.Count == 0)
            {
                return 0;
                throw new InvalidDataException("La liste est vide");
            }
            if (values.Count == 1)
            {
                return values.StandardDeviation(0, values.Count);
            }

            return values.StandardDeviation(0, values.Count) * values.Count / (values.Count - 1);
        }

        public static double StandardDeviation(this List<double> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }

        public static double[,] ToMatrix(this IEnumerable<double[]> list)
        {
            if (list.Distinct().Count() != 1)
            {
                throw new Exception();
            }

            int rowCount = list.Count();
            int columnCount = list.First().Count();

            double[,] matrix = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = list.ElementAt(i)[j];
                }
            }
            return matrix;
        }

        public static double[,] ToMatrix(this IList<double[]> list)
        {
            if (list.Select(x => x.Count()).Distinct().Count() != 1)
            {
                throw new Exception();
            }

            int rowCount = list.Count();
            int columnCount = list.First().Count();

            double[,] matrix = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = list.ElementAt(i)[j];
                }
            }
            return matrix;
        }


        /// <summary>
        /// Partitions the given list around a pivot element such that all elements on left of pivot are <= pivot
        /// and the ones at the right are > pivot. This method can be used for sorting, N-order statistics such as
        /// as median finding algorithms.
        /// Pivot is selected ranodmly if random number generator is supplied else its selected as last element in the list.
        /// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 171
        /// </summary>
        private static int Partition<T>(this IList<T> list, int start, int end, Random rnd = null) where T : IComparable<T>
        {
            if (rnd != null)
                list.Swap(end, rnd.Next(start, end + 1));

            var pivot = list[end];
            var lastLow = start - 1;
            for (var i = start; i < end; i++)
            {
                if (list[i].CompareTo(pivot) <= 0)
                    list.Swap(i, ++lastLow);
            }
            list.Swap(end, ++lastLow);
            return lastLow;
        }

        /// <summary>
        /// Returns Nth smallest element from the list. Here n starts from 0 so that n=0 returns minimum, n=1 returns 2nd smallest element etc.
        /// Note: specified list would be mutated in the process.
        /// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 216
        /// </summary>
        private static T NthOrderStatistic<T>(this IList<T> list, int n, Random rnd = null) where T : IComparable<T>
        {
            return NthOrderStatistic(list, n, 0, list.Count - 1, rnd);
        }
        private static T NthOrderStatistic<T>(this IList<T> list, int n, int start, int end, Random rnd) where T : IComparable<T>
        {
            while (true)
            {
                var pivotIndex = list.Partition(start, end, rnd);
                if (pivotIndex == n)
                {
                    return list[pivotIndex];
                }

                if (n < pivotIndex)
                {
                    end = pivotIndex - 1;
                }
                else
                {
                    start = pivotIndex + 1;
                }
            }
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            if (i == j)   //This check is not required but Partition function may make many calls so its for perf reason
                return;
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        /// <summary>
        /// Note: specified list would be mutated in the process.
        /// </summary>
        public static T Median<T>(this IList<T> list) where T : IComparable<T>
        {
            //return list.NthOrderStatistic((list.Count - 1) / 2);
            throw new NotImplementedException();
            return list.NthOrderStatistic((list.Count) / 2);
        }

        public static T FirstQuartile<T>(this IList<T> list) where T : IComparable<T>
        {
            throw new NotImplementedException();
            return list.NthOrderStatistic((list.Count - 1) / 4);
        }

        public static double FirstQuartile(this double[] values)
        {
            int nthOrder = (values.Length - 1) / 4;
            double firstQuartile1 = values.NthOrderStatistic(nthOrder);
            if (values.Length + 1 % 2 != 0 && ((values.Length + 1) / 2) % 2 != 0)
            {
                return firstQuartile1;
            }
            double firstQuartile2 = values.NthOrderStatistic(nthOrder + 1);
            return (firstQuartile1 + firstQuartile2) / 2;
        }

        public static T ThirdQuartile<T>(this IList<T> list) where T : IComparable<T>
        {
            throw new NotImplementedException();
            //return list.NthOrderStatistic(3 * (list.Count - 1) / 4);
            return list.NthOrderStatistic(3 * (list.Count) / 4);
        }
        public static double ThirdQuartile(this double[] values)
        {
            int nthOrder = 3 * values.Length / 4;
            double thirdQuartile1 = values.NthOrderStatistic(nthOrder);
            if (values.Length + 1 % 2 != 0 && ((values.Length + 1) / 2) % 2 != 0)
            {
                return thirdQuartile1;
            }
            double thirdQuartile2 = values.NthOrderStatistic(nthOrder - 1);
            return (thirdQuartile1 + thirdQuartile2) / 2;
        }

        public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
        {
            var list = sequence.Select(getValue).ToList();
            var mid = (list.Count - 1) / 2;
            throw new NotImplementedException();
            return list.NthOrderStatistic(mid);
        }

        public static BoxPlot BoxPlot(this double[] vector, double k = 1.5)
        {
            return new BoxPlot(vector, k);
        }
    }
}
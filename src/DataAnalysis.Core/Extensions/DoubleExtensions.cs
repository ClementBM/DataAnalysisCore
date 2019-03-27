using DataAnalysis.Core.Algebra.MathFunc;
using System;

namespace DataAnalysis.Core.Extensions
{
    public static class DoubleExtensions
    {

        public static double Subtract(Tuple<double, double> xy)
        {
            return xy.Item1 - xy.Item2;
        }

        public static double Add(Tuple<double, double> xy)
        {
            return xy.Item1 + xy.Item2;
        }

        public static double Divide(Tuple<double, double> xy)
        {
            return xy.Item1 / xy.Item2;
        }

        public static double Multiply(Tuple<double, double> xy)
        {
            return xy.Item1 * xy.Item2;
        }

        public static double Sqrt(this double scalar)
        {
            return Math.Sqrt(scalar);
        }


        public static bool AboutEqual(this double x, double y, double epsilon)
        {
            return Math.Abs(x - y) <= epsilon;
        }
        public static double Sigmoid(this double x)
        {
            return MathUtils.Sigmoid(x);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class BooleanArrayExtensions
    {
        public static IEnumerable<bool> LogicalOr(bool[] x, bool[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < x.Length; i++)
            {
                yield return x[i] || y[i];
            }
        }

        public static bool[] BooleanArray(int length, bool defaultValue = true)
        {
            var values = new bool[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = defaultValue;
            }
            return values;
        }

        public static IList<int> Indexes(this bool[] array, bool value = true)
        {
            var indexes =
                      array
                          .Select((p, i) => new { Item = p, Index = i })
                          .Where(p => p.Item == value)
                          .Select(p => p.Index);
            return indexes.ToList();
        }
    }
}
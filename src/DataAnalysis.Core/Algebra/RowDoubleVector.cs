using DataAnalysis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAnalysis.Core.Algebra
{
    public interface IRowVector
    {
        object Values { get; }

        int Count { get; }
    }

    public abstract class RowDoubleVector<T>
        where T : new()
    {
        private static List<PropertyInfo> _fieldInfos;

        public Type Type
        {
            get { return typeof(T); }
        }

        public RowDoubleVector()
        {
            _fieldInfos = new List<PropertyInfo>();
            foreach (var field in typeof(T).GetProperties().Where(z => z.PropertyType == typeof(double)))
            {
                _fieldInfos.Add(field);
            }
        }

        public static double[] ToArray(T x)
        {
            var fields = _fieldInfos
                .OrderBy(y => y.GetCustomAttribute<ColumnDefinitionAttribute>().Order);

            return fields.Select(y => (double)typeof(T).GetProperty(y.Name).GetValue(x)).ToArray();
        }

        protected static T SetValues(double[] values)
        {
            var fields = _fieldInfos
                .OrderBy(y => y.GetCustomAttribute<ColumnDefinitionAttribute>().Order);

            T obj = new T();
            for (int i = 0; i < fields.Count(); i++)
            {
                typeof(T).GetProperty(fields.ElementAt(i).Name).SetValue(obj, values[i]);
            }
            return obj;
        }

        public int Count
        {
            get
            {
                return _fieldInfos.Count();
            }
        }

        public static double Sum(T x)
        {
            double sum = 0;
            foreach (var field in _fieldInfos)
            {
                sum += (double)typeof(T).GetProperty(field.Name).GetValue(x);
            }
            return sum;
        }

        public static T Divide(T x, T y)
        {
            return ArithmeticOperation(x, y, DoubleExtensions.Divide);
        }

        public static T Multiply(T x, T y)
        {
            return ArithmeticOperation(x, y, DoubleExtensions.Multiply);
        }

        public static T Subtract(T x, T y)
        {
            return ArithmeticOperation(x, y, DoubleExtensions.Subtract);
        }

        public static T Add(T x, T y)
        {
            return ArithmeticOperation(x, y, DoubleExtensions.Add);
        }

        public static string[] ColumnNames()
        {
            var fields = _fieldInfos.OrderBy(x => x.GetCustomAttribute<ColumnDefinitionAttribute>().Order);
            return fields.Select(x => x.Name).ToArray();
        }

        public static bool Equals(T x, T y)
        {
            foreach (var field in _fieldInfos)
            {
                double xFieldValue = (double)typeof(T).GetProperty(field.Name).GetValue(x);
                double yFieldValue = (double)typeof(T).GetProperty(field.Name).GetValue(y);
                if (xFieldValue != yFieldValue)
                {
                    return false;
                }
            }
            return true;
        }

        private static T ArithmeticOperation(T x, T y, Func<Tuple<double, double>, double> func)
        {
            T returnRow = new T();

            foreach (var field in _fieldInfos)
            {
                double xFieldValue = (double)typeof(T).GetProperty(field.Name).GetValue(x);
                double yFieldValue = (double)typeof(T).GetProperty(field.Name).GetValue(y);
                typeof(T).GetProperty(field.Name).SetValue(returnRow, func(new Tuple<double, double>(xFieldValue, yFieldValue)));
            }
            return returnRow;
        }
    }
}
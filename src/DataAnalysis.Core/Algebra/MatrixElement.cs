using DataAnalysis.Core.Extensions;
using DataAnalysis.Core.Statistic;
using System;
using System.Collections;
using System.Linq;
using System.Diagnostics.Contracts;

namespace DataAnalysis.Core.Algebra
{
    public abstract class MatrixElement
    {
        protected ArrayList _values;

        public MatrixElement()
        {
            _values = new ArrayList();
        }

        public MatrixElement(int dimension)
        {
            _values = new ArrayList(dimension);
        }

        public MatrixElement(double[] x)
        {
            _values = new ArrayList(x.Length);
            for (int i = 0; i < x.Length; i++)
            {
                _values.Add(x[i]);
            }
        }
        public MatrixElement(ArrayList x)
        {
            _values = new ArrayList(x);
        }

        public double this[int i]
        {
            get
            {
                return (double)_values[i];
            }
            set { _values[i] = value; }
        }

        public ArrayList Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public int Order { get; internal set; }


        public int Count
        {
            get
            {
                return _values.Count();
            }
        }

        public double Mean
        {
            get
            {
                return _values.Mean();
            }
        }

        public double StandardDeviation
        {
            get
            {
                return _values.StandardDeviation();
            }
        }

        public double Median
        {
            get
            {
                return _values.Median();
            }
        }

        public double Maximum
        {
            get
            {
                return _values.Maximum();
            }
        }
        public double Sum
        {
            get
            {
                return _values.Sum();
            }
        }

        public double SquareSum()
        {
            return _values.Cast<double>().ToArray().SquaredSum();
        }

        public MatrixColumn ElementWiseSquare()
        {
            Contract.Ensures(Contract.Result<MatrixColumn>() != null);

            var array = _values.Cast<double>().ToList();
            array.ForEach(x => Math.Pow(x, 2));
            return array;
        }

        public double SquareSumSqrt()
        {
            return SquareSum().Sqrt();
        }

        public override string ToString()
        {
            return string.Join(" | ", _values);
        }

        public static double Dot(MatrixElement A, MatrixElement B)
        {
            if (A.Count != B.Count)
            {
                throw new ArgumentException("Inner matrix dimensions must agree.");
            }

            double result = new double();

            for (int i = 0; i < A.Count; i++)
            {
                result += A[i] * B[i];
            }

            return result;
        }

        public virtual Matrix ToMatrix()
        {
            return new Matrix(_values);
        }

        public BoxPlot BoxPlot(double k = 1.5)
        {
            return _values.BoxPlot(k);
        }
    }
}

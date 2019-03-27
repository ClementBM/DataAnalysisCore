using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Algebra
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class MatrixIteratorAttribute : Attribute
    {
        public Iterators Iterator { get; set; }

        public MatrixIteratorAttribute(Iterators iterator)
        {
            Iterator = iterator;
        }
    }

    public class MatrixIterator
    {
        public MatrixIterator(Matrix matrix)
        {
            Matrix = matrix;
        }
        public Matrix Matrix { get; set; }
    }
    public class MatrixIteratorByColumn : MatrixIterator
    {
        public MatrixIteratorByColumn(Matrix matrix) : base(matrix)
        {
        }
    }

    public class MatrixIteratorByRow : MatrixIterator
    {
        public MatrixIteratorByRow(Matrix matrix) : base(matrix)
        {
        }
    }

    public enum Iterators
    {
        ByColumn,
        ByRow
    }
}

using DataAnalysis.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public class TableRow : MatrixRow, IRowSchema, IRowDefinition
    {
        private IEnumerable<IColumnDefinition> _columnDefinitions;
        private string _rowName;

        public TableRow(int dimension, IEnumerable<IColumnDefinition> columnDefinitions) : base(dimension)
        {
            _columnDefinitions = columnDefinitions;
        }

        public TableRow(ArrayList values, IEnumerable<IColumnDefinition> columnDefinitions) : base(values)
        {
            _columnDefinitions = columnDefinitions;
        }

        public TableRow(double[] values, IEnumerable<IColumnDefinition> columnDefinitions) : base(values)
        {
            _columnDefinitions = columnDefinitions;
        }

        public TableRow(double[] values, IEnumerable<IColumnDefinition> columnDefinitions, string rowName) : base(values)
        {
            _rowName = rowName;
            _columnDefinitions = columnDefinitions;
        }

        public IEnumerable<IColumnDefinition> ColumnDefinitions
        {
            get
            {
                return _columnDefinitions = new List<ColumnDefinition>();
            }
            set
            {
                _columnDefinitions = value;
            }
        }

        public string RowName
        {
            get
            {
                return _rowName;
            }
            set
            {
                _rowName = value;
            }
        }

        public TableRow TakeColumns(int columnCount)
        {
            return new TableRow(Values.ToArrayFloating().Take(columnCount).ToArray(), ColumnDefinitions, RowName);
        }

        public static TableRow operator +(TableRow x, TableRow y)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentOutOfRangeException("Matrix columns does not have the same dimension");
            }

            ArrayList sumedColumn = new ArrayList(x.Count);

            for (int i = 0; i < x.Count; i++)
            {
                sumedColumn[i] = x[i] + y[i];
            }

            return new TableRow(sumedColumn, x.ColumnDefinitions);
        }

        public static TableRow operator -(TableRow x, TableRow y)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentOutOfRangeException("Matrix columns does not have the same dimension");
            }

            ArrayList subtractedColumn = new ArrayList(x.Count);

            for (int i = 0; i < x.Count; i++)
            {
                subtractedColumn.Add(x[i] - y[i]);
            }

            return new TableRow(subtractedColumn, x.ColumnDefinitions);
        }
    }
}
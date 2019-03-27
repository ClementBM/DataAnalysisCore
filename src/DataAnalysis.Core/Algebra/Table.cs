using DataAnalysis.Core.Extensions;
using DataAnalysis.Core.Statistic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public class Table : Matrix, ITableSchema
    {
        List<ColumnDefinition> _columnDefinitions;
        List<RowDefinition> _rowDefinitions;

        public Table() : base() { }

        public Table(IEnumerable<IRowVector> rowDoubleVectors) : base(rowDoubleVectors)
        {
            _columnDefinitions = new List<ColumnDefinition>(rowDoubleVectors.First().Count);
            _rowDefinitions = new List<RowDefinition>(rowDoubleVectors.Count());
        }

        public Table(int rowCount, int columnCount) : base(rowCount, columnCount)
        {
            _columnDefinitions = new List<ColumnDefinition>(columnCount);
            _rowDefinitions = new List<RowDefinition>(rowCount);
        }

        public Table(Matrix matrix) : base()
        {
            Values = matrix.Values;
            _columnDefinitions = new List<ColumnDefinition>(Values.ColumnCount());
            _rowDefinitions = new List<RowDefinition>(Values.RowCount());
        }

        public Table(double[,] values) : base(values)
        {
            _columnDefinitions = new List<ColumnDefinition>(values.ColumnCount());
            _rowDefinitions = new List<RowDefinition>(values.RowCount());
        }

        public Table(double[,] values, List<ColumnDefinition> columnDefinitions, List<RowDefinition> rowDefinitions) : base(values)
        {
            _columnDefinitions = columnDefinitions;
            _rowDefinitions = rowDefinitions;
        }

        public IEnumerable<IRowDefinition> RowDefinitions
        {
            get
            {
                if (_rowDefinitions == null)
                {
                    _rowDefinitions = new List<RowDefinition>();
                }
                return _rowDefinitions;
            }
            set
            {
                _rowDefinitions = value.Cast<RowDefinition>().ToList();
            }
        }

        public IEnumerable<IColumnDefinition> ColumnDefinitions { get { return _columnDefinitions; } }

        public new TableRow Row(int i)
        {
            string rowName = i < RowDefinitions.Count() ? RowDefinitions.ElementAt(i).RowName : null;
            return new TableRow(ColumnCount, ColumnDefinitions) { Values = Values.Row(i), Order = i, RowName = rowName };
        }

        public new TableColumn Column(int i)
        {
            return new TableColumn(ColumnCount, RowDefinitions) { Values = Values.Column(i), Order = i };
        }

        public new TableRows Rows
        {
            get
            {
                return new TableRows(ToRowCollection());
            }
        }

        public new IEnumerable<TableRow> ToRowCollection()
        {
            for (int i = 0; i < RowCount; i++)
            {
                yield return Row(i);
            }
        }

        public new TableColumns Columns
        {
            get
            {
                return new TableColumns(ToColumnCollection());
            }
        }

        public new IEnumerable<TableColumn> ToColumnCollection()
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                yield return Column(i);
            }
        }

        public void AddRow(TableRow row)
        {
            var newRow = new RowDefinition
            {
                RowName = row?.RowName,
                Order = RowDefinitions.Count()
            };
            _rowDefinitions.Add(newRow);
            _values.Add(new ArrayList(row.Values.ToArrayFloating()));
        }

        public void AddColumn(TableColumn column)
        {
            throw new Exception();
        }

        public List<BoxPlot> BoxPlot(int k)
        {
            List<BoxPlot> boxPlots = new List<BoxPlot>();
            for (int i = 0; i < ColumnCount; i++)
            {
                boxPlots.Add(Column(i).Values.BoxPlot(k));
            }
            return boxPlots;
        }

        public new TableRow Centroid()
        {
            return new TableRow(base.Centroid(), ColumnDefinitions);
        }
        public Matrix ToMatrix()
        {
            return new Matrix(_values);
        }
    }
}
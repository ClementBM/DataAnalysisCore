using System;
using System.Collections.Generic;

namespace DataAnalysis.Core.Algebra
{
    public class TableColumn : MatrixColumn, IColumnSchema, IColumnDefinition
    {
        private IEnumerable<IRowDefinition> _rowDefinitions;
        private string _columnName;

        public TableColumn(int dimension, IEnumerable<IRowDefinition> rowDefinitions) : base(dimension)
        {
            _rowDefinitions = rowDefinitions;
        }

        public string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }

        public Type ColumnType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IRowDefinition> RowDefinitions
        {
            get
            {
                return _rowDefinitions;
            }
            set
            {
                _rowDefinitions = value;
            }
        }

        public object Deserialize(string value)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object value)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace DataAnalysis.Core.Algebra
{
    public class ColumnDefinition : IColumnDefinition
    {
        public string ColumnName { get; set; }

        public Type ColumnType { get; set; }

        public int Order { get; set; }

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

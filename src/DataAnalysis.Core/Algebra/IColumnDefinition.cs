using System;

namespace DataAnalysis.Core.Algebra
{
    public interface IColumnDefinition
    {
        string ColumnName { get; }

        Type ColumnType { get; }

        object Deserialize(string value);

        string Serialize(object value);

        int Order { get; }
    }
}

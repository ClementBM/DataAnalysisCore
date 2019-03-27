using System.Collections.Generic;

namespace DataAnalysis.Core.Algebra
{
    public interface IRowSchema
    {
        IEnumerable<IColumnDefinition> ColumnDefinitions { get; }
    }

    public interface IColumnSchema
    {
        IEnumerable<IRowDefinition> RowDefinitions { get; }
    }

    public interface ITableSchema : IRowSchema, IColumnSchema { }
}
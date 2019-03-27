using System;

namespace DataAnalysis.Core.Algebra
{
    public class ColumnDefinitionAttribute : Attribute
    {
        public string Name { get; set; }

        public int Order { get; set; }
    }
}

using FlatFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAnalysis.DataBinding
{
    public class CsvReader<T>
        where T : new()
    {
        public CsvReader()
        {
        }

        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public IEnumerable<T> GetData(string filePath, string separator = ";")
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                SeparatedValueOptions options = new SeparatedValueOptions { IsFirstRecordSchema = true, Separator = separator, Quote = '\'' };
                var separatedValueSchema = new SeparatedValueSchema();

                var properties = Type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;
                    var columnDefinition = GetColumnDefinition(property.PropertyType, propertyName);
                    separatedValueSchema.AddColumn(columnDefinition);
                }

                SeparatedValueReader separatedValueReader = new SeparatedValueReader(streamReader, separatedValueSchema, options);
                List<T> values = new List<T>();

                while (separatedValueReader.Read())
                {
                    var rowValues = separatedValueReader.GetValues();
                    var tRow = new T();
                    var propertiesObjectValues = properties.Zip(rowValues, (a, b) => new { PropType = a, PropValue = b });
                    foreach (var propertiesObjectValue in propertiesObjectValues)
                    {
                        Type.GetProperty(propertiesObjectValue.PropType.Name).SetValue(tRow, propertiesObjectValue.PropValue);
                    }
                    values.Add(tRow);
                }
                return values;
            }
        }

        IColumnDefinition GetColumnDefinition(Type type, string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new Exception($"Column name can not be null or white space");
            }
            if (type == typeof(int))
            {
                return new Int32Column(columnName);
            }
            else if (type == typeof(string))
            {
                return new StringColumn(columnName);
            }
            else if (type == typeof(bool))
            {
                return new BooleanColumn(columnName);
            }
            else if (type == typeof(DateTime))
            {
                return new DateTimeColumn(columnName);
            }
            else if (type == typeof(decimal))
            {
                return new DecimalColumn(columnName);
            }
            else if (type == typeof(double))
            {
                return new DoubleColumn(columnName);
            }
            throw new Exception($"Column type [{type.Name}] is not mapped yet");
        }
    }
}
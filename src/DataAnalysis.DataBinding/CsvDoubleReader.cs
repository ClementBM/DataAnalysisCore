using FlatFiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAnalysis.DataBinding
{
    public class CsvDoubleReader
    {
        public CsvDoubleReader()
        {
        } 

        public IEnumerable<double[]> GetData(string filePath, string separator = ";")
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                SeparatedValueOptions options = new SeparatedValueOptions() { IsFirstRecordSchema = false, Separator = separator, Quote = '\'' };
                SeparatedValueReader reader = new SeparatedValueReader(streamReader, options);

                List<double[]> values = new List<double[]>();

                while (reader.Read())
                {
                    var actualValues = reader.GetValues();
                    values.Add(Array.ConvertAll(actualValues, x => Convert.ToDouble(x)));
                }
                return values;
            }
        }
    }
}

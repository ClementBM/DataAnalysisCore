using FlatFiles;
using System;
using System.Collections.Generic;
using System.IO;


namespace DataAnalysis.DataBinding
{
    public class CsvIntReader
    {
        public CsvIntReader()
        {
        }

        public IEnumerable<int[]> GetData(string filePath, string separator = ";")
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                SeparatedValueOptions options = new SeparatedValueOptions() { IsFirstRecordSchema = false, Separator = separator, Quote = '\'' };
                SeparatedValueReader reader = new SeparatedValueReader(streamReader, options);

                List<int[]> values = new List<int[]>();

                while (reader.Read())
                {
                    var actualValues = reader.GetValues();
                    values.Add(Array.ConvertAll(actualValues, x => Convert.ToInt32(x)));
                }
                return values;
            }
        }
    }
}

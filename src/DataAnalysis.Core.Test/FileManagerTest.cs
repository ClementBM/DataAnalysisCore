using System;
using System.IO;

namespace DataAnalysis.Test
{
    public class FileManagerTest
    {
        public static string GetTestDataByName(params string[] name)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string tempPath = Path.Combine(name);
            string filePath = Path.Combine(directoryPath, tempPath);
            if (!File.Exists(filePath))
            {
                return null;
            }
            return filePath;
        }
    }
}

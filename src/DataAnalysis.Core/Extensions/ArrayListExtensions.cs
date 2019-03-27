using DataAnalysis.Core.Statistic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class ArrayListExtensions
    {
        public static ArrayList Column(this ArrayList matrix, int j)
        {
            ArrayList column = new ArrayList(matrix.RowCount());
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                column.Add(matrix.Element(i, j));
            }
            return column;
        }

        public static double[] ToArrayFloating(this ArrayList vector)
        {
            return Array.ConvertAll(vector.ToArray(), o => (double)o);
        }

        public static double[,] ToMatrix(this ArrayList matrix)
        {
            if (!matrix.CheckDimensionIntegrity())
            {
                throw new Exception();
            }
            double[,] matrixRes = new double[matrix.RowCount(), matrix.ColumnCount()];
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                var arrayList = (ArrayList)matrix[i];
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    var element = arrayList[j];
                    matrixRes[i, j] = double.Parse(element.ToString());
                }
            }
            return matrixRes;
        }

        public static double[][] ToDoubleArray(this ArrayList matrix)
        {
            if (!matrix.CheckDimensionIntegrity())
            {
                throw new Exception();
            }
            double[][] matrixRes = new double[matrix.RowCount()][];
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                ArrayList arrayList = matrix[i] as ArrayList;
                matrixRes[i] = arrayList.ToArrayFloating();
            }
            return matrixRes;
        }

        public static ArrayList Row(this ArrayList matrix, int i)
        {
            return (ArrayList)matrix[i];
        }

        public static int RowCount(this ArrayList matrix)
        {
            return matrix.Count;
        }

        public static double Element(this ArrayList matrix, int i, int j)
        {
            if (i >= 0 && i < matrix.RowCount() && j >= 0 && j < matrix.ColumnCount())
            {
                var element = ((ArrayList)matrix[i])[j];
                return double.Parse(element.ToString());
            }
            throw new Exception();
        }

        public static int ColumnCount(this ArrayList matrix)
        {
            if (!matrix.CheckDimensionIntegrity())
            {
                throw new Exception();
            }
            return matrix.GetFirstColumnCount();
        }

        public static bool CheckDimensionIntegrity(this ArrayList matrix)
        {
            int columnCount = matrix.GetFirstColumnCount();
            for (int i = 1; i < matrix.RowCount(); i++)
            {
                if (columnCount != ((ArrayList)matrix[i]).Count)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetFirstColumnCount(this ArrayList matrix)
        {
            return ((ArrayList)matrix[0]).Count;
        }

        public static void SetColumn(this ArrayList matrix, int j, ArrayList column)
        {
            int columnCount = column.Count;
            for (int i = 0; i < columnCount; i++)
            {
                ((ArrayList)matrix[i])[j] = column[i];
            }
        }

        public static ArrayList Zero(int length)
        {
            ArrayList res = new ArrayList(length);
            for (int i = 0; i < length; i++)
            {
                res.Add(0.0);
            }
            return res;
        }

        public static ArrayList Ones(int length)
        {
            ArrayList res = new ArrayList(length);
            for (int i = 0; i < length; i++)
            {
                res.Add(1.0);
            }
            return res;
        }

        public static double Maximum(this ArrayList vector)
        {
            return vector.Cast<double>().Max();
        }
        public static double Sum(this ArrayList vector)
        {
            return vector.Cast<double>().Sum();
        }

        public static double Median(this ArrayList vector)
        {
            return vector.Cast<double>().ToArray().Median();
        }

        public static double Mean(this ArrayList vector)
        {
            return vector.Cast<double>().Mean();
        }

        public static int Count(this ArrayList vector)
        {
            return vector.Count;
        }

        public static ArrayList ReplaceColumnIf(this ArrayList matrix, Func<ArrayList, bool> function)
        {
            List<int> selectedIndexes = new List<int>();
            for (int i = 0; i < matrix.ColumnCount(); i++)
            {
                if (function(matrix.Column(i)))
                {
                    selectedIndexes.Add(i);
                }
            }
            if (!selectedIndexes.Any())
            {
                return matrix;
            }

            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    if (selectedIndexes.Contains(j))
                    {
                        ((ArrayList)matrix[i])[j] = 0;
                    }
                }
            }
            return matrix;
        }

        public static double StandardDeviation(this ArrayList vector)
        {
            return vector.Cast<double>().ToList().StandardDeviation();
        }

        public static ArrayList Subtract(this ArrayList xVector, ArrayList yVector)
        {
            return new ArrayList(xVector.Cast<double>().ToArray().Subtract(yVector.Cast<double>().ToArray()));
        }

        public static BoxPlot BoxPlot(this ArrayList vector, double k = 1.5)
        {
            return vector.Cast<double>().ToArray().BoxPlot(k);
        }
    }
}
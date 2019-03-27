using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Extensions
{
    public static class DoubleMatrixExtensions
    {
        public static double[,] Transpose(this double[,] matrix)
        {
            int m = matrix.GetLength(0); // nombre de points, ligne
            int n = matrix.GetLength(1); // nombre de dimensions, colonnes
            var transposedMatrix = new double[n, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }
            return transposedMatrix;
        }

        public static void SetColumn(this double[,] matrix, int j, double[] column)
        {
            int m = matrix.GetLength(0); // nombre de lignes
            int rowCount = column.Count();
            if (m != rowCount)
            {
                throw new Exception();
            }

            for (int i = 0; i < m; i++)
            {
                matrix[i, j] = column[i];
            }
        }

        public static int ColumnCount(this double[,] matrix)
        {
            int n = matrix.GetLength(1); // nombre de colonnes
            return n;
        }

        public static int RowCount(this double[,] matrix)
        {
            int m = matrix.GetLength(0); // nombre de lignes
            return m;
        }

        public static double[] Column(this double[,] matrix, int j)
        {
            int m = matrix.GetLength(0); // nombre de lignes
            double[] col = new double[m];

            for (int i = 0; i < m; i++)
            {
                col[i] = matrix[i, j];
            }

            return col;
        }

        public static double[][] Columns(this double[,] matrix)
        {
            double[][] columns = new double[matrix.ColumnCount()][];
            for (int i = 0; i < matrix.ColumnCount(); i++)
            {
                columns[i] = matrix.Column(i);
            }
            return columns;
        }

        public static double[][] Rows(this double[,] matrix)
        {
            double[][] rows = new double[matrix.RowCount()][];
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                rows[i] = matrix.Row(i);
            }
            return rows;
        }

        public static double[,] ReplaceColumnIf(this double[,] matrix, Func<double[], bool> function)
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
                        matrix[i, j] = 0;
                    }
                }
            }
            return matrix;
        }

        public static double[,] ReplaceElementIf(this double[,] matrix, Func<double, bool> function, double replaceBy)
        {
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    if (function(matrix[i, j]))
                    {
                        matrix[i, j] = replaceBy;
                    }
                }
            }
            return matrix;
        }

        public static double[,] Fill(this double[,] matrix, Func<int, int, double> function)
        {
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    matrix[i, j] = function(i, j);
                }
            }
            return matrix;
        }

        public static double[,] RemoveColumnIf(this double[,] matrix, Func<double[], bool> function)
        {
            List<int> selectedIndexes = new List<int>();
            for (int i = 0; i < matrix.ColumnCount(); i++)
            {
                if (!function(matrix.Column(i)))
                {
                    selectedIndexes.Add(i);
                }
            }
            double[,] selectedMatrix = new double[matrix.RowCount(), selectedIndexes.Count()];
            for (int i = 0; i < selectedMatrix.RowCount(); i++)
            {
                for (int j = 0; j < selectedMatrix.ColumnCount(); j++)
                {
                    selectedMatrix[i, j] = matrix[i, selectedIndexes[j]];
                }
            }
            return selectedMatrix;
        }

        public static double[] Row(this double[,] matrix, int i)
        {
            int n = matrix.GetLength(1); // nombre de dimensions, colonnes
            double[] row = new double[n];

            for (int j = 0; j < n; j++)
            {
                row[j] = matrix[i, j];
            }
            return row;
        }

        public static double[,] Scale(this double[,] matrix)
        {
            double[] means = new double[matrix.ColumnCount()];
            double[] standardDeviation = new double[matrix.ColumnCount()];
            for (int i = 0; i < matrix.ColumnCount(); i++)
            {
                means[i] = matrix.Column(i).Mean();
                standardDeviation[i] = matrix.Column(i).StandardDeviation();
            }

            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    matrix[i, j] = (matrix[i, j] - means[j]) / standardDeviation[j];
                }
            }
            return matrix;
        }

        public static double[,] Center(this double[,] matrix)
        {
            double[] means = new double[matrix.ColumnCount()];
            for (int i = 0; i < matrix.ColumnCount(); i++)
            {
                means[i] = matrix.Column(i).Mean();
            }

            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    matrix[i, j] = matrix[i, j] - means[j];
                }
            }
            return matrix;
        }

        public static int Count(this double[,] matrix)
        {
            return matrix.ColumnCount() * matrix.RowCount();
        }

        public static double[] UpperTri(this double[,] matrix)
        {
            if (!matrix.IsSquare())
            {
                throw new Exception();
            }
            int count = matrix.RowCount() * (matrix.RowCount() - 1) / 2;

            double[] upperTriangle = new double[count];
            int k = 0;
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = i + 1; j < matrix.ColumnCount(); j++)
                {
                    upperTriangle[k] = matrix[i, j];
                    k++;
                }
            }
            return upperTriangle;
        }

        public static double[] LowerTri(this double[,] matrix)
        {
            return UpperTri(matrix.Transpose());
        }

        public static double[,] Covariance(this double[,] matrix)
        {
            double[,] covMatrix = new double[matrix.RowCount(), matrix.ColumnCount()];
            matrix = matrix.Center();
            var covarianceMatrix = Multiply(matrix.Transpose(), matrix);
            return Divide(covarianceMatrix, matrix.RowCount() - 1);
        }

        public static double[,] Multiply(double[,] xMatrix, double[,] yMatrix)
        {
            if (xMatrix.ColumnCount() != yMatrix.RowCount())
            {
                throw new Exception();
            }
            int m = xMatrix.ColumnCount();
            double[,] resultMatrix = new double[xMatrix.RowCount(), yMatrix.ColumnCount()];
            for (int i = 0; i < resultMatrix.RowCount(); i++)
            {
                for (int j = 0; j < resultMatrix.ColumnCount(); j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        resultMatrix[i, j] += xMatrix[i, k] * yMatrix[k, j];
                    }
                }
            }
            return resultMatrix;
        }

        public static double[,] Divide(double[,] matrix, double scalar)
        {
            if (scalar.AboutEqual(0, 1.0e-10))
            {
                throw new Exception();
            }
            for (int i = 0; i < matrix.RowCount(); i++)
            {
                for (int j = 0; j < matrix.ColumnCount(); j++)
                {
                    matrix[i, j] /= scalar;
                }
            }
            return matrix;
        }

        public static bool IsSquare(this double[,] matrix)
        {
            if (matrix.ColumnCount() != matrix.RowCount())
            {
                return false;
            }
            return true;
        }
    }
}

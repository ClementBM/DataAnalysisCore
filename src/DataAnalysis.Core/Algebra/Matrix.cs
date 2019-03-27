using DataAnalysis.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAnalysis.Core.Algebra
{
    public partial class Matrix
    {
        public Matrix()
        {
            _values = new ArrayList();
        }

        public Matrix(ArrayList matrix)
        {
            _values = matrix;
        }

        public Matrix(double[,] values)
        {
            if (values == null)
            {
                Values = new ArrayList();
            }

            int rowCount = (int)values.GetLongLength(0);
            int columnCount = (int)values.GetLongLength(1);

            Values = new ArrayList(rowCount);

            for (int i = 0; i < rowCount; i++)
            {
                Values.Add(new ArrayList(columnCount));

                for (int j = 0; j < columnCount; j++)
                {
                    ((ArrayList)Values[i]).Add(values[i, j]);
                }
            }
        }

        public Matrix(IEnumerable<IRowVector> rowDoubleVectors)
        {
            if (!rowDoubleVectors.Any())
            {
                return;
            }
            int columnCount = rowDoubleVectors.First().Count;
            var rows = rowDoubleVectors.Select(x => (double[])x.Values);

            _values = new ArrayList(rowDoubleVectors.Count());

            for (int i = 0; i < rows.Count(); i++)
            {
                _values.Add(new ArrayList(columnCount));
                _values[i] = new ArrayList(rows.ElementAt(i));
            }
        }

        public Matrix(IEnumerable<double[]> doubles)
        {
            if (!doubles.Any())
            {
                return;
            }
            int columnCount = doubles.First().Count();

            _values = new ArrayList(doubles.Count());

            for (int i = 0; i < doubles.Count(); i++)
            {
                _values.Add(new ArrayList(columnCount));
                _values[i] = new ArrayList(doubles.ElementAt(i));
            }
        }

        public Matrix(List<MatrixRow> rowDoubleVectors)
        {
            if (!rowDoubleVectors.Any())
            {
                return;
            }
            int columnCount = rowDoubleVectors.First().Count;
            var rows = rowDoubleVectors.Select(x => x.Values.ToArrayFloating());

            _values = new ArrayList(rowDoubleVectors.Count());

            for (int i = 0; i < rows.Count(); i++)
            {
                _values.Add(new ArrayList(columnCount));
                _values[i] = new ArrayList(rows.ElementAt(i));
            }
        }

        public Matrix(int rowCount, int columnCount)
        {
            _values = new ArrayList(rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                _values.Add(ArrayListExtensions.Zero(columnCount));
            }
        }

        public Matrix(double[] values, int direction = 1)
        {
            if (direction == 1)
            {
                int rowCount = values.Count();

                Values = new ArrayList(rowCount);

                for (int i = 0; i < rowCount; i++)
                {
                    Values.Add(new ArrayList(1));
                    ((ArrayList)Values[i]).Add(values[i]);
                }
                return;
            }
            else if (direction == 2)
            {
                int columnCount = values.Count();

                var row = new ArrayList(columnCount);
                for (int i = 0; i < columnCount; i++)
                {
                    row.Add(values[i]);
                }
                Values = new ArrayList(1);
                Values.Add(row);
                return;
            }
            throw new Exception();
        }

        public Matrix Unroll(int direction = 1)
        {
            Matrix unrolled = new Matrix(ColumnCount * RowCount, 1);
            if (direction == 1)
            {
                int k = 0;
                for (int j = 0; j < ColumnCount; j++)
                {
                    for (int i = 0; i < RowCount; i++)
                    {
                        unrolled[k++, 0] = this[i, j];
                    }
                }
                return unrolled;
            }
            else if (direction == 2)
            {
                int k = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        unrolled[k++, 0] = this[i, j];
                    }
                }
                return unrolled;
            }
            throw new Exception();
        }

        protected ArrayList _values;

        public ArrayList Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public int ColumnCount
        {
            get
            {
                return _values.ColumnCount();
            }
        }

        protected int GetRowCount()
        {
            return _values.RowCount();
        }

        public int RowCount
        {
            get { return GetRowCount(); }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < RowCount && j >= 0 && j < ColumnCount)
                {
                    return double.Parse(((ArrayList)Values[i])[j].ToString());
                }
                throw new Exception();
            }
            set
            {
                if (i < 0 && i >= RowCount)
                {
                    throw new Exception();
                }
                if (j < 0 && j >= ColumnCount)
                {
                    throw new Exception();
                }

                ((ArrayList)Values[i])[j] = value;
            }
        }

        public void SetColumn(int i, MatrixColumn column)
        {
            _values.SetColumn(i, column.Values);
        }

        public Matrix Set(int i, int j, double value)
        {
            this[i, j] = value;
            return this;
        }

        public MatrixColumns Columns
        {
            get
            {
                return new MatrixColumns(ToColumnCollection());
            }
        }

        public IEnumerable<MatrixColumn> ToColumnCollection()
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                yield return Column(i);
            }
        }

        public MatrixRows Rows
        {
            get
            {
                return new MatrixRows(ToRowCollection());
            }
        }

        public virtual IEnumerable<MatrixRow> ToRowCollection()
        {
            for (int i = 0; i < RowCount; i++)
            {
                yield return Row(i);
            }
        }

        public virtual MatrixColumn Column(int j)
        {
            return new MatrixColumn
            {
                Values = _values.Column(j),
                Order = j
            };
        }

        public virtual MatrixRow Row(int i)
        {
            return new MatrixRow(_values.Row(i)) { Order = i };
        }

        public virtual Matrix GetRow(int i)
        {
            return new Matrix(_values.Row(i).ToArrayFloating());
        }

        public virtual Matrix GetColumn(int i)
        {
            return new Matrix(_values.Column(i).ToArrayFloating());
        }

        public double RowSum(int i)
        {
            return Row(i).Sum;
        }

        public double ColumnSum(int i)
        {
            return Column(i).Sum;
        }

        public double[] Centroid()
        {
            double[] centroid = new double[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                centroid[i] = Column(i).Mean;
            }
            return centroid;
        }

        public void ReplaceColumnIf(Func<ArrayList, bool> function)
        {
            _values = _values.ReplaceColumnIf(function);
        }

        public Matrix Covariance()
        {
            var covMatrix = _values.ToMatrix().Covariance();
            return new Matrix(covMatrix);
        }

        public double[] UpperTri()
        {
            return _values.ToMatrix().UpperTri();
        }

        public double[,] Scale()
        {
            return _values.ToMatrix().Scale();
        }

        public double[,] ToMatrixDouble(int[] selectedColumns)
        {
            List<MatrixColumn> columns = new List<MatrixColumn>();
            for (int i = 0; i < selectedColumns.Count(); i++)
            {
                columns.Add(Column(selectedColumns[i]));
            }
            double[,] matrix = new double[columns[0].Count, selectedColumns.Count()];

            for (int i = 0; i < columns[0].Count; i++)
            {
                for (int j = 0; j < selectedColumns.Count(); j++)
                {
                    matrix[i, j] = columns[j][i];
                }
            }
            return matrix;
        }

        public Matrix ToMatrix(int[] selectedColumns)
        {
            var matrix = ToMatrixDouble(selectedColumns);
            return new Matrix(matrix);
        }

        public Matrix GetColumns(int firstColumnIndex, int lastColumnIndex)
        {
            if (firstColumnIndex < 0 || firstColumnIndex >= ColumnCount)
            {
                throw new Exception("firstColumnIndex must be greater or equal to zero and smaller than ColumnCount");
            }
            if (lastColumnIndex < 0 || lastColumnIndex >= ColumnCount)
            {
                throw new Exception("lastColumnIndex must be greater or equal to zero and smaller than ColumnCount");
            }
            if (lastColumnIndex < firstColumnIndex)
            {
                throw new Exception("lastColumnIndex must be greater or equal to firstColumnIndex");
            }
            int columnCount = lastColumnIndex - firstColumnIndex + 1;
            int[] indexes = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                indexes[i] = firstColumnIndex + i;
            }
            var matrix = ToMatrixDouble(indexes);
            return new Matrix(matrix);
        }

        public double[,] ToMatrixDouble()
        {
            double[,] matrix = new double[RowCount, ColumnCount];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    matrix[i, j] = this[i, j];
                }
            }
            return matrix;
        }

        public IEnumerable<MatrixCell> Max(int direction)
        {
            List<MatrixCell> maxValues = new List<MatrixCell>();
            if (direction == 1)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    double max = double.MinValue;
                    int iMax = 0;
                    int jMax = 0;

                    for (int i = 0; i < RowCount; i++)
                    {
                        if (max < this[i, j])
                        {
                            max = this[i, j];
                            iMax = i;
                            jMax = j;
                        }
                    }
                    maxValues.Add(new MatrixCell
                    {
                        ColumnIndex = jMax,
                        RowIndex = iMax,
                        Value = max
                    });
                }
                return maxValues;
            }
            else if (direction == 2)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    double max = double.MinValue;
                    int iMax = 0;
                    int jMax = 0;

                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (max < this[i, j])
                        {
                            max = this[i, j];
                            iMax = i;
                            jMax = j;
                        }
                    }
                    maxValues.Add(new MatrixCell
                    {
                        ColumnIndex = jMax,
                        RowIndex = iMax,
                        Value = max
                    });
                }
                return maxValues;
            }
            throw new Exception();

        }

        public Tuple<double, int, int> Max()
        {
            double max = double.MinValue;
            int iMax = 0;
            int jMax = 0;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (max < this[i, j])
                    {
                        max = this[i, j];
                        iMax = i;
                        jMax = j;
                    }
                }
            }
            return new Tuple<double, int, int>(max, iMax, jMax);
        }

        public void AddRow(MatrixRow row)
        {
            _values.Add(row.Values);
        }

        public void AddColumn(MatrixColumn column)
        {
            throw new Exception();
        }

        public Matrix Transpose()
        {
            return new Matrix(Values.ToMatrix().Transpose());
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var line in _values)
            {
                stringBuilder.AppendLine(string.Join(",", ((ArrayList)line).ToArrayFloating()));
                stringBuilder.Append("; ");
            }
            return stringBuilder.ToString();
        }
    }
}
using DataAnalysis.Core.Statistic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public class MatrixColumns : IEnumerable<MatrixColumn>
    {
        private List<MatrixColumn> _columns;

        internal MatrixColumns()
        {
            _columns = new List<MatrixColumn>();
        }

        public MatrixColumns(IEnumerable<MatrixColumn> rows)
        {
            _columns = rows.ToList();
        }

        public MatrixColumn this[int index]
        {
            get { return _columns[index]; }
            set { _columns[index] = value; }
        }

        public int Count
        {
            get { return _columns.Count; }
        }

        public IEnumerator<MatrixColumn> GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        public double[] Mean
        {
            get { return _columns.Select(x => x.Mean).ToArray(); }
        }
        public double[] Median
        {
            get { return _columns.Select(x => x.Median).ToArray(); }
        }

        public double[] Maximum
        {
            get { return _columns.Select(x => x.Maximum).ToArray(); }
        }

        public BoxPlot[] BoxPlot(double k = 1.5)
        {
            BoxPlot[] boxPlots = new BoxPlot[Count];
            for (int i = 0; i < Count; i++)
            {
                boxPlots[i] = this[i].BoxPlot(k);
            }
            return boxPlots;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public class MatrixRows : IEnumerable<MatrixRow>
    {
        private List<MatrixRow> rows;

        internal MatrixRows()
        {
            rows = new List<MatrixRow>();
        }

        public MatrixRows(IEnumerable<MatrixRow> rows)
        {
            this.rows = rows.ToList();
        }

        public MatrixRow this[int index]
        {
            get { return rows[index]; }
        }

        public int Count
        {
            get { return rows.Count; }
        }

        public IEnumerator<MatrixRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        public double[] Mean
        {
            get { return rows.Select(x => x.Mean).ToArray(); }
        }
    }
}

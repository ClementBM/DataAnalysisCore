using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace DataAnalysis.Core.Algebra
{
    public class TableRows : IEnumerable<TableRow>
    {
        private List<TableRow> _rows;

        internal TableRows()
        {
            _rows = new List<TableRow>();
        }
        public TableRows(IEnumerable<TableRow> rows)
        {
            _rows = rows.ToList();
        }

        public TableRow this[int index]
        {
            get { return _rows[index]; }
        }
        public int Count
        {
            get { return _rows.Count; }
        }

        public IEnumerator<TableRow> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        public double[] Mean
        {
            get { return _rows.Select(x => x.Mean).ToArray(); }
        }
    }
}

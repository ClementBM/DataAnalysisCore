using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public class TableColumns : IEnumerable<TableColumn>
    {
        private List<TableColumn> _columns;

        internal TableColumns()
        {
            _columns = new List<TableColumn>();
        }

        public TableColumns(IEnumerable<TableColumn> columns)
        {
            _columns = columns.ToList();
        }
        public TableColumn this[int index]
        {
            get { return _columns[index]; }
        }
        public int Count
        {
            get { return _columns.Count; }
        }

        public IEnumerator<TableColumn> GetEnumerator()
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
    }
}

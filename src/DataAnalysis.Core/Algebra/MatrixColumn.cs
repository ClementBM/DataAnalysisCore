using System.Collections;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public partial class MatrixColumn : MatrixElement
    {
        public MatrixColumn() { }

        public MatrixColumn(double[] x) : base(x) { }

        public MatrixColumn(int dimension) : base(dimension) { }

        public MatrixColumn(ArrayList x) : base(x) { }

        public override Matrix ToMatrix()
        {
            var values = new ArrayList(Count);
            for (int i = 0; i < Count; i++)
            {
                values.Add(new ArrayList(1));
                values[i] = new ArrayList(new double[] { (double)_values[i] });
            }
            return new Matrix(values);
        }
    }
}
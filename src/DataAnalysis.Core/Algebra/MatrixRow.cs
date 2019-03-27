using System.Collections;
using System.Linq;

namespace DataAnalysis.Core.Algebra
{
    public partial class MatrixRow : MatrixElement
    {
        public MatrixRow() { }

        public MatrixRow(int dimension) : base(dimension) { }

        public MatrixRow(double[] x) : base(x) { }

        public MatrixRow(ArrayList x) : base(x) { }

        public override Matrix ToMatrix()
        {
            var values = new ArrayList(1);
            values.Add(new ArrayList(_values));
            return new Matrix(values);
        }
    }
}

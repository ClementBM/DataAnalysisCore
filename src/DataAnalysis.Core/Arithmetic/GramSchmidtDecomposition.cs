using DataAnalysis.Core.Algebra;

namespace DataAnalysis.Core.Arithmetic
{
    public class GramSchmidtDecomposition : Algorithm<Matrix, GramSchmidtResult>
    {
        protected override bool CheckInputs(Matrix parameters)
        {
            return true;
        }

        protected override GramSchmidtResult DoCompute(Matrix a)
        {
            int m = a.RowCount;
            int n = a.ColumnCount;

            var q = new Matrix(m, n);
            var r = new Matrix(n, 1);

            for (int i = 0; i < n; i++)
            {

            }
            return new GramSchmidtResult();
        }
    }

    public class GramSchmidtResult
    {
        public Matrix R { get; set; }
        public Algebra.Matrix Q { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Arithmetic
{
    public abstract class Algorithm<TParameters, TOutput>
    {
        public TOutput Compute(TParameters parameters)
        {
            if (!CheckInputs(parameters))
            {
                return default(TOutput);
            }
            return DoCompute(parameters);
        }

        protected abstract bool CheckInputs(TParameters parameters);

        protected abstract TOutput DoCompute(TParameters parameters);
    }
}

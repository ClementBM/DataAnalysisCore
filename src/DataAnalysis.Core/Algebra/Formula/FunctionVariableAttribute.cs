using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Core.Algebra.Formula
{
    public class FunctionVariableAttribute : Attribute
    {
        public int Order { get; set; }
    }
}

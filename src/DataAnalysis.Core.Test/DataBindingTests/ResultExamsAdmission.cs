using DataAnalysis.Core.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Test.DataBindingTests
{
    public class ResultExamsAdmission
    {
        public double Exam1 { get; set; }
        public double Exam2 { get; set; }

        public int AdmittedCode { get; set; }
    }

    public class ResultExamsAdmissionRow : ResultExamsAdmission, IRowVector
    {
        public bool Admitted
        {
            get
            {
                if (AdmittedCode == 0)
                {
                    return false;
                }
                if (AdmittedCode == 1)
                {
                    return true;
                }
                throw new Exception("Invalid data");
            }
        }

        public int Count => 3;

        public object Values
        {
            get
            {
                return new double[3] { AdmittedCode, Exam1, Exam2 };
            }
        }

        public ResultExamsAdmissionRow(ResultExamsAdmission resultExamsAdmission)
        {
            AdmittedCode = resultExamsAdmission.AdmittedCode;
            Exam1 = resultExamsAdmission.Exam1;
            Exam2 = resultExamsAdmission.Exam2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAnalysis.Core.Algebra.Formula
{
    public abstract class FunctionVariable<Variable>
    {
        private readonly Type Type = typeof(Variable);

        private readonly List<PropertyInfo> _fieldInfos;

        public int Count()
        {
            return Type.GetProperties().Count();
        }

        public abstract Variable MakeAInstance();

        protected FunctionVariable()
        {
            _fieldInfos = new List<PropertyInfo>();
            var fieldInfos = typeof(Variable)
                .GetProperties()
                .Where(z => z.PropertyType == typeof(double))
                .OrderBy(y => y.GetCustomAttribute<FunctionVariableAttribute>().Order);
            foreach (var fieldInfo in _fieldInfos)
            {
                _fieldInfos.Add(fieldInfo);
            }
        }

        public double this[int i]
        {
            get
            {
                return (double)Type.GetProperty(_fieldInfos[i].Name).GetValue(this);
            }
            set
            {
                Type.GetProperty(_fieldInfos[i].Name).SetValue(this, value);
            }
        }

        public Variable Set(int i, double parameter)
        {
            this[i] = parameter;
            return MakeAInstance();
        }
    }
}

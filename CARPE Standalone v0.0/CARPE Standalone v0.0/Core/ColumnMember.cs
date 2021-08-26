using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.Core
{
    public class ColumnMember<T>
    { 
        #region Members

        public T Value { get; set; }
        private readonly Func<T, string> _toString;

        #endregion

        #region Constructors
        public ColumnMember(T value) => Value = value;
        public ColumnMember(T value, Func<T, string> toString)
        {
            Value = value;
            _toString = toString;
        }

        #endregion

        #region Method
        public override string ToString() => _toString != null ? _toString(Value) : Value.ToString();

        #endregion

    }
}

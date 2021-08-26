using CARPE_Standalone_v0._0.MVVM.ViewModel.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.Model.TreeView
{
    public class Filter
    {
        #region Members
        public int ColumnIndex { get; set; }
        public int OperatorIndex { get; set; }
        public string OriginalString { get; set; }

        public string ColumnName => TreeViewModel.ColumnNames[ColumnIndex];
        public string PropertyName => TreeViewModel.PropertyNames[ColumnIndex];
        public string OperatorName => Constant.FILTER_OPERATOR_NAMES[OperatorIndex];

        #endregion

        #region Methods
        public Filter(int columnIndex, int operatorIndex, string originalString)
        {
            ColumnIndex = columnIndex;
            OperatorIndex = operatorIndex;
            OriginalString = originalString;
        }
        #endregion
    }
}

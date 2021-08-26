using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.Model
{
    class Constant
    {
        #region Members
        public static ReadOnlyCollection<string> BYTE_UNIT = new List<string>
        { "B", "KB", "MB", "GB", "TB", "PB" }.AsReadOnly();
        public static ReadOnlyCollection<string> FILTER_OPERATOR_NAMES = new List<string>
        { "equal to", "contain", "greater than", "greater than or equal to", "less than", "less than or eqaul to" }.AsReadOnly();
        #endregion

        #region Methods
        public static DateTime ConvertDate(string dateString)
        {
            return dateString.Equals("") ? default : DateTime.ParseExact(dateString.Substring(0, 19), "yyyy-MM-ddTHH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        public static long ConvertSize(string sizeString)
        {
            return sizeString.Equals("?") ? -1 : Convert.ToInt64(sizeString);
        }

        #endregion


    }
}

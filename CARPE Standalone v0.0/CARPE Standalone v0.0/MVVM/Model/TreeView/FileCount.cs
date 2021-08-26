using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.Model.TreeView
{
    public class FileCount : IComparable
    {
        #region Members
        public int RevCount { get; set; }
        public int TotalCount { get; set; }
        public bool IsCounted { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public FileCount()
        {
            RevCount = 0;
            TotalCount = 0;
            IsCounted = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="revCount"></param>
        /// <param name="totalCount"></param>
        public FileCount(int revCount, int totalCount)
        {
            RevCount = revCount;
            TotalCount = totalCount;
            IsCounted = true;
        }
        #endregion

        #region Methods
        public int CompareTo(object obj) => GetRatio().CompareTo(((FileCount)obj).GetRatio());
        private double GetRatio() => IsCounted == false ? -2 : TotalCount == 0 ? -1 : (double)RevCount / TotalCount;
        public override string ToString() => IsCounted == false ? "" : TotalCount == 0 ? "Empty" : String.Format("{0:P1}", (double)RevCount / TotalCount);
        #endregion
    }
}

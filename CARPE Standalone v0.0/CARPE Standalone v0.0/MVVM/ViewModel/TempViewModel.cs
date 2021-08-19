using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class TempViewModel : ObservableObject
    {
        private DataTable _dataTable;

        public DataTable TmpDataTable
        {
            get { return _dataTable; }
            set 
            { 
                _dataTable = value; 
                OnPropertyChanged();
            }
        }

        public TempViewModel()
        {
            _dataTable = new DataTable();

            // 컬럼 생성
            _dataTable.Columns.Add("ID", typeof(string));
            _dataTable.Columns.Add("NAME", typeof(string));
            _dataTable.Columns.Add("TEL_NO", typeof(string));

            // 데이터 생성
            _dataTable.Rows.Add(new string[] { "ID-01", "Name 01", "010-0001-0000" });
            _dataTable.Rows.Add(new string[] { "ID-02", "Name 02", "010-0002-0000" });
            _dataTable.Rows.Add(new string[] { "ID-03", "Name 03", "010-0003-0000" });
            _dataTable.Rows.Add(new string[] { "ID-04", "Name 04", "010-0004-0000" });
            
        }
    }
}

using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel.Analyze
{
    class TableViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region Member
        SQLiteConnection con;
        SQLiteCommand sql_cmd;
        SQLiteDataReader sql_reader;

        private DataTable _dataTable;
        private ObservableCollection<string> _dbTables;
        private string _selectedTable;

        public DataTable MyDataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> DBTables
        {
            get { return _dbTables; }
            set
            {
                _dbTables = value;
                OnPropertyChanged();
            }
        }
        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SelectedTableChanged { get; set; }

        #endregion

        #region Method

        public TableViewModel()
        {
            _dataTable = new DataTable();
            _dbTables = new ObservableCollection<string>();

            // Table 이름 불러오기
            con = new SQLiteConnection("Data Source=" + TempData.DBPath);
            con.Open();
            sql_cmd = new SQLiteCommand("SELECT * FROM case_info", con);
            for (int i = 0; i < con.GetSchema("Tables").Rows.Count; i++)
            {
                DataRow row = con.GetSchema("Tables").Rows[i];
                if ((string)row[2] == "sqlite_sequence") continue;
                DBTables.Add((string)row[2]);
            }

            // 새로운 Table 선택했을때 DataGrid 업데이트
            SelectedTableChanged = new RelayCommand(o =>
            {
                DataTable tmpdataTable = new DataTable();
                sql_cmd = new SQLiteCommand("SELECT * FROM " + _selectedTable, con);

                sql_reader = sql_cmd.ExecuteReader();
                tmpdataTable.Load(sql_reader);

                MyDataTable = tmpdataTable;

            });

        }

        #endregion
    }
}

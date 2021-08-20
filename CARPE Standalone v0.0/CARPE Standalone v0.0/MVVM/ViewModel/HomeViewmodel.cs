using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.Data;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class HomeViewmodel : ObservableObject
    {
        #region Member
        private ObservableCollection<DataTable> _myDataTable;
        private ObservableCollection<string> _myDataString;
        private string _dbLoaded;

        public ObservableCollection<DataTable> MyDataTable
        {
            get { return _myDataTable; }
            set 
            { 
                _myDataTable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> MyDataString
        {
            get { return _myDataString; }
            set
            {
                _myDataString = value;
                OnPropertyChanged();
            }
        }

        public string DBLoaded
        {
            get { return _dbLoaded; }
            set
            {
                _dbLoaded = value;
                OnPropertyChanged();
            }
            }

        SQLiteConnection con;
        SQLiteCommand sql_cmd;
        SQLiteDataReader sql_reader;
        #endregion

        #region Method
        public HomeViewmodel()
        {
            _myDataTable = new ObservableCollection<DataTable>();
            _myDataString = new ObservableCollection<string>();
            _dbLoaded = "Hidden";
            Update();
        }

        /// <summary>
        /// 윈도우 로딩시 Update하도록 함
        /// </summary>
        public void LoadedEvent() => Update();


        /// <summary>
        /// 메인 메뉴의 DataTable들 업데이트
        /// </summary>
        /// <returns> 업데이트 성공시 0, 실패시 그 외 정수</returns>
        public int Update()
        {
            //DB 로딩이 아직 되지 않았을 시 return
            if (TempData.DBPath == null)
            {
                _dbLoaded = "Hidden";
                return -1;
            }
            _dbLoaded = "Visible";


            //DB 로딩이 된 경우 sql connection 설정
            con = new SQLiteConnection("Data Source=" + TempData.DBPath);
            con.Open();           
            sql_cmd = con.CreateCommand();

            // 각각의 Table들 업데이트
            if (!DashBoardUpdate()) return 1;
            if (!SummaryUpdate()) return 2;
            if (!TableCountUpdate()) return 3;
            if (!FileExtensionCountUpdate()) return 4;

            con.Close();

            return 0;
        }

        /// <summary>
        /// Case 정보 가져옴
        /// </summary>
        /// <returns> 업데이트 성공시 True, 실패시 False</returns>
        private bool DashBoardUpdate()
        {            
            // Case 기본 정보
            sql_cmd = new SQLiteCommand("SELECT * FROM case_info", con);
            sql_reader = sql_cmd.ExecuteReader();
            while (sql_reader.Read())
            {
                if(MyDataString.Count > 0) MyDataString.Clear();
                MyDataString.Add("Case Name : " + sql_reader["case_name"].ToString());               
                MyDataString.Add("Description : " + sql_reader["description"].ToString());
                MyDataString.Add("Processed at : " + sql_reader["create_date"].ToString());
            }

            // Case 상세 정보
            sql_cmd = new SQLiteCommand("SELECT * FROM evidence_info", con);
            sql_reader = sql_cmd.ExecuteReader();
            if (MyDataTable.Count > 0)
            {
                MyDataTable.Clear();
                
            }
            MyDataTable.Add(new DataTable());

            MyDataTable[0].Columns.Add("Item");
            MyDataTable[0].Columns.Add("Value");
            sql_reader.Read();
            for (int i = 0; i < 9; i++)
            {
                MyDataTable[0].Rows.Add(new List<string>
                    {
                        sql_reader.GetName(i), sql_reader[i].ToString()
                    }.ToArray());
            }
            return true;
        }

        /// <summary>
        /// 전체 File Count를 세고
        /// Partition_info에서 Summary 가져옴
        /// </summary>
        /// <returns> 업데이트 성공시 True, 실패시 False</returns>
        private bool SummaryUpdate()
        {
            //전체 File Count 값
            sql_cmd = new SQLiteCommand("SELECT * FROM sqlite_sequence", con);
            sql_reader = sql_cmd.ExecuteReader();
            sql_reader.Read();
            MyDataString.Add("File Count : " + sql_reader["seq"].ToString());
            
            // Partition_info에서 Summary 가져옴
            sql_cmd = new SQLiteCommand("SELECT * FROM partition_info", con);
            sql_reader = sql_cmd.ExecuteReader();
            MyDataTable.Add(new DataTable());
            MyDataTable[1].Load(sql_reader);
            
            return true;
        }

        /// <summary>
        /// 각각의 Table별로 Row Count 값 가져옴
        /// </summary>
        /// <returns> 업데이트 성공시 True, 실패시 False</returns>
        private bool TableCountUpdate()
        {
            MyDataTable.Add(new DataTable());

            MyDataTable[2].Columns.Add("Table Name");
            MyDataTable[2].Columns.Add("Row Count", 1.GetType());

            //각각 테이블의 Row 갯수 Count
            foreach (DataRow row in con.GetSchema("Tables").Rows)
            {
                if ((string)row[2] == "sqlite_sequence") continue;
                sql_cmd = new SQLiteCommand(string.Format("SELECT count(*) FROM {0}", (string)row[2]), con);
                sql_reader = sql_cmd.ExecuteReader();

                sql_reader.Read();
                MyDataTable[2].Rows.Add(new List<string>
                    {
                        (string)row[2], sql_reader[0].ToString()
                    }.ToArray());
            }

            return true;
        }

        /// <summary>
        /// File Extension Count 가져옴
        /// </summary>
        /// <returns> 업데이트 성공시 True, 실패시 False</returns>
        private bool FileExtensionCountUpdate()
        {
            sql_cmd = new SQLiteCommand("Select extension, count(extension) as count FROM file_info group by extension", con);
            sql_reader = sql_cmd.ExecuteReader();

            MyDataTable.Add(new DataTable());
            MyDataTable[3].Load(sql_reader);
            return true;
        }
        #endregion
    }
}

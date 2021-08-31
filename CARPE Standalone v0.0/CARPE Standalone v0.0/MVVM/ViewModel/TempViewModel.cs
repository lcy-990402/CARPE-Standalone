using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LiveCharts;
using CARPE_Standalone_v0._0.Data;
using CARPE_Standalone_v0._0.MVVM.Model;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class TempViewModel : ObservableObject
    {
        #region Members
        public Func<double, string> Formatter { get; set; }

        SQLiteConnection con;
        SQLiteDataReader sql_reader;

        private DataTable myDataTable;
        private Dictionary<(int, int), int> tmpDict;
        private SeriesCollection series;

        public DataTable MyDataTable
        {
            get
            {
                return myDataTable;
            }
            set
            {
                myDataTable = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<(int, int), int> TmpDict
        {
            get
            {
                return tmpDict;
            }
            set
            {
                tmpDict = value;
                OnPropertyChanged();
            }
        }
        public SeriesCollection Series
        {
            get
            {
                return series;
            }
            set
            {
                series = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Constructors
        public TempViewModel()
        {
            MyDataTable = new DataTable();
            TmpDict = new Dictionary<(int, int), int>();
            TmpDict.Add((0, 0), 0);
            Formatter = value => new DateTime((long)value).ToString("yyyy-MM");

            SetData();
            DrawChart();
        }

        #endregion
        #region Methods
        private bool SetData()
        {
            con = new SQLiteConnection("Data Source=" + TempData.DBPath);
            con.Open();
            sql_reader = new SQLiteCommand("SELECT process_name, execution_time, process_path, reference_file, source FROM lv2_os_app_history", con).ExecuteReader();
            MyDataTable.Columns.Add("Process Name");
            MyDataTable.Columns.Add("Execution Time");
            MyDataTable.Columns.Add("Process Path");
            MyDataTable.Columns.Add("Reference File");
            MyDataTable.Columns.Add("Source");
            while (sql_reader.Read())
            {
                // DataTable 채우기
                List<string> tmpdata = new List<string>();
                for (int i = 0; i < sql_reader.FieldCount; i++)
                {
                    if (i == 1 && sql_reader[i] + "" != "")
                    {
                        string tmpstring2 = DateTime.Parse((sql_reader[i] + "").Substring(0, 19)).ToString("");
                        tmpdata.Add(tmpstring2);
                        continue;
                    }
                    tmpdata.Add(sql_reader[i] + "");
                }
                MyDataTable.Rows.Add(tmpdata.ToArray());

                // 월별로 App 실행 횟수 Count
                if (sql_reader[1] + "" == "") continue;
                DateTime tmp = DateTime.Parse((sql_reader[1] + "").Substring(0, 19));
                int tmpyear = Int32.Parse(tmp.ToString("yyyy"));
                int tmpmonth = Int32.Parse(tmp.ToString("MM"));
                if (!tmpDict.ContainsKey((tmpyear, tmpmonth))) tmpDict.Add((tmpyear, tmpmonth), 1);
                else tmpDict[(tmpyear, tmpmonth)] += 1;

            }
            return true;
        }
        private bool DrawChart()
        {
            DateTime tmpDateTime = new DateTime(2010, 1, 1);
            ChartValues<DateModel> tmpchartvalue = new ChartValues<DateModel>();

            var dayConfig = Mappers.Xy<DateModel>()
                .X(dateModel => dateModel.DT.Ticks)
                .Y(dateModel => dateModel.Value);

            var list = tmpDict.Keys.ToList();
            list.Sort();

            foreach((int, int) tmpvalue in list)
            {
                if (tmpvalue.Item1 <= 1960) continue;
                tmpchartvalue.Add(
                    new DateModel
                    {
                        DT = new DateTime(tmpvalue.Item1, tmpvalue.Item2, 1),
                        Value = tmpDict[tmpvalue]
                    }
                    );
            }

            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = tmpchartvalue,
                    Fill = Brushes.Transparent
                }
            };

            return true;
        }
        #endregion
    }
}

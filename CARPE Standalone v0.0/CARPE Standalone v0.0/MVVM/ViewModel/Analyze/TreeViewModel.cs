using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.Data;
using CARPE_Standalone_v0._0.MVVM.Model;
using CARPE_Standalone_v0._0.MVVM.Model.TreeView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel.Analyze
{
    class TreeViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region StaticMembers
        public static readonly List<string> ColumnNames = new List<string>();
        public static readonly List<string> PropertyNames = new List<string>();
        #endregion

        #region Members
        // TreeView에서 Partition을 나타낼 때 사용하는 string을 나타냅니다.
        private readonly string PARTITIONSTRING = "Partition ";

        // 파일 시스템에서 루트를 나타낼 때 사용하는 string을 나타냅니다.
        private readonly string ROOTSTRING = "<root>";

        // UTC 설정을 위한 변수들입니다.
        private bool UTCSetting_IsZone = true;
        private int UTCSetting_CurrentIndex = -1;

        private List<Filter> filters = new List<Filter>();

        // MainTreeView에서 체크된 TreeViewItem의 체크 박스 리스트입니다.
        private readonly List<CheckBox> checkedItemBoxes = new List<CheckBox>();

        // MainListView에 보여줄 TreeNode 리스트입니다.
        private readonly List<TreeNode> viewNodes = new List<TreeNode>();

        // MainListView의 column들의 Header와 Visibility를 관리하기 위한 변수입니다.
        private readonly List<double> columnWidths = new List<double>();

        // MainListView의 column들의 header를 클릭하여 정렬하는 기능을 위한 변수들입니다.
        private readonly List<GridViewColumnHeader> clickedHeaders = new List<GridViewColumnHeader>();
        private readonly List<string> sortBys = new List<string>();
        private readonly List<ListSortDirection> directions = new List<ListSortDirection>();

        // partitions와 그 이름들을 선언합니다.
        // partitions는 최상위 TreeNode로, DB의 par_id에 따라 식별됩니다.
        private List<TreeNode> _partitions;
        public List<TreeNode> partitions
        {
            get { return _partitions; }
            set
            {
                _partitions = value;
                OnPropertyChanged();
            }
        }
        public List<string> partitionIds = new List<string>();

        private List<TreeNode> mainListView;
        public List<TreeNode> MainListView
        {
            get { return mainListView; }
            set
            {
                mainListView = value;
                OnPropertyChanged();
            }
        }

        private TreeNode selectedTreeNode;
        public TreeNode SelectedTreeNode
        {
            get { return selectedTreeNode; }
            set
            {
                selectedTreeNode = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SelectedItemChanged { get; set; }

        #endregion

        #region Constructor
        public TreeViewModel()
        {
            //초기화
            _partitions = new List<TreeNode>();
            mainListView = new List<TreeNode>();
            selectedTreeNode = new TreeNode("");

            // UTC 값과 UTC Setting 창의 TimeZone 인덱스를 한국 시간대에 맞는 인덱스로 조정합니다.
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                if (z.Id.Equals("Korea Standard Time"))
                {
                    UTCSetting_CurrentIndex = TimeZoneInfo.GetSystemTimeZones().IndexOf(z);
                    Settings.UTC = new TimeSpan(9, 0, 0);
                }
            }

            //TreeView를 만듭니다.
            SetTreeView();

            //새로은 TreeNode를 선택했을때 ListView를 업데이트합니다.
            SelectedItemChanged = new RelayCommand(o =>
            {
                MainListView = GetTreeNodeChild(SelectedTreeNode);
            });

        }
        #endregion

        #region Methods

        private void SetTreeView()
        {
            // 각각의 파일별로 데이터 저장하는 변수
            string partitionId;
            string name;
            string path;
            string extension; 
            DateTime createdTime;
            DateTime modifiedTime;
            DateTime accessedTime;
            long size;
            bool isDirectory;
            TreeNode cur;
           
            //DB 연결
            SQLiteConnection con = new SQLiteConnection("Data Source=" + TempData.DBPath);
            con.Open();
            SQLiteDataReader sql_reader = new SQLiteCommand("SELECT * FROM lv1_fs_ntfs_mft", con).ExecuteReader();
         
            while (sql_reader.Read())
            {
                // 파일에서 데이터를 읽어옵니다
                partitionId = sql_reader["par_id"].ToString();
                path = sql_reader["path"].ToString();
                createdTime = Constant.ConvertDate(sql_reader["SI_ctime"].ToString());
                modifiedTime = Constant.ConvertDate(sql_reader["SI_mtime"].ToString());
                accessedTime = Constant.ConvertDate(sql_reader["SI_atime"].ToString());
                size = Constant.ConvertSize(sql_reader["file_size"].ToString());
                isDirectory = sql_reader["is_directory"].ToString().Equals("Y");

                // 경로가 비어있을 경우 처리하지 않습니다.
                if (path.Equals("")) continue;

                // 현재 가리키는 TreeNode를 선언합니다.
                // 최초에는 par_id로 식렵되는 partition으로 시작합니다.
                // 이미 존재하면 존재하는 partition을, 존재하지 않으면 새로 만든 partition을 가리킵니다.
                if (partitionIds.Exists(x => x.Equals(partitionId))) cur = partitions[partitionIds.IndexOf(partitionId)];
                else
                {
                    TreeNode partition = new TreeNode(PARTITIONSTRING + string.Format("{0}", partitions.Count + 1));

                    partitions.Add(partition);
                    partitionIds.Add(partitionId);
                    cur = partition;
                }

                // 읽은 path를 조작하여 탐색할 경로를 만듭니다.
                // 첫 번째 문자열이 빈 문자열일 경우 root로 지정합니다.
                string[] route = path.Split('/');
                if(route[0] == "")
                {
                    route[0] = ROOTSTRING;
                    path = ROOTSTRING + path;
                }

                // path의 앞에 partition 이름을 삽입합니다.
                path = cur.Name + "/" + path;

                // 경로를 탐색하며 TreeNode를 만들고 연결시킵니다.
                for(int index = 0, length = route.Length; index < length; index++)
                {
                    name = route[index];
                    extension = name.IndexOf('.') != -1 && isDirectory == false ? name.Split('.').Last() : "";

                    // 이름이 "."일 경우 하위에 TreeNode를 만드는 것이 아니라
                    // 현재 TreeNode의 정보를 가리키는 것이라 판단하여 구분합니다.
                    if (name.Equals("."))
                    {
                        //cur.Info.SetDetail(createdTime, modifiedTime, accessedTime, size, isDirectory, path, extension);
                        break;
                    }

                    // 추가되는 TreeNode가 파일일 경우 현재 TreeNode의 Size와 FileCount를 증가시키고,
                    // 시간 역전 현상이 발생했다면 RevCount도 증가시킵니다.
                    if(isDirectory == false)
                    {
                        cur.Info.Size.Value += size;
                        cur.Info.Reversal.Value.TotalCount++;
                        if (DateTime.Compare(createdTime, modifiedTime) < 0) cur.Info.Reversal.Value.RevCount++;
                    }

                    // 다음 탐색할 TreeNode를 검샣가여 다음 탐색 대상으로 지정합니다.
                    // 찾지 못한 경우 새로운 TreeNode를 만들고 추가한 후 다음 탐색 대상으로 지정합니다.
                    TreeNode next = cur.Find(name);
                    if(next == null)
                    {
                        TreeNode newNode = new TreeNode(name);

                        // 경로의 마지막이면서 디렉토리가 아닌 경우 SubFile에,
                        // 그 이외에는 SubDirectory에 추가합니다.
                        if (index == length - 1 && isDirectory != true) cur.SubFile.Add(newNode);
                        else cur.SubDirectory.Add(newNode);

                        newNode.Parent = cur;
                        next = newNode;
                    }

                    cur = next;

                    // 경로의 마지막에는 해당 TreeNode의 정보를 입력합니다.
                    if(index == length - 1)
                    {
                        cur.Info.SetDetail(createdTime, modifiedTime, accessedTime, size, isDirectory, path, extension);
                    }                            
                 }
            }

            // 연결을 닫고 MainTreeView에 아이템을 추가합니다.
            sql_reader.Close();
            con.Close();           
        }

        /// <summary>
        /// 해당 TreeNode의 SubDirectory와 SubFile을 합하여 리스트로 반환합니다.
        /// </summary>
        /// <param name="cur"></param>
        /// <returns> SubDirectory + SubFile </returns>
        private List<TreeNode> GetTreeNodeChild(TreeNode cur)
        {
            List<TreeNode> nodeList = new List<TreeNode>();
            if (cur == null) return nodeList;
            foreach (TreeNode node in cur.SubDirectory) nodeList.Add(node);
            foreach (TreeNode node in cur.SubFile) nodeList.Add(node);
            return nodeList;
        }


        /// <summary>
        /// nodeList를 정렬하는 함수
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns> sorted list </returns>
        private List<TreeNode> SotrTreeNodeList(List<TreeNode> nodeList)
        {
            List<TreeNode> result = new List<TreeNode>();
            IOrderedEnumerable<TreeNode> sortedList;

            if (clickedHeaders.Count == 0)
            {
                sortedList = nodeList.OrderBy(x => !x.Info.IsDirectory.Value).ThenBy(x => x.Name.Value);
            }
            else
            {
                sortedList = nodeList.OrderBy(x => x.GetType().Equals(typeof(TreeNode)));

                foreach (string sortBy in sortBys)
                {
                    if (directions[sortBys.IndexOf(sortBy)] == ListSortDirection.Ascending)
                    {
                        sortedList = sortedList.ThenBy(x => x.GetColumnMemberValue(sortBy));
                    }
                    else
                    {
                        sortedList = sortedList.ThenByDescending(x => x.GetColumnMemberValue(sortBy));
                    }
                }
            }
            return sortedList.ToList();
        }

        private void ClearMainListView()
        {
            MainListView.Clear();
        }

        #endregion
    }
}

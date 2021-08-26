using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.Model.TreeView
{
    class TreeNode : IComparable<TreeNode>
    {
        #region Members
        public ColumnMember<string> Name { get; }
        public TreeNodeInfo Info { get; set; }
        public TreeNode Parent { get; set; }
        public SortedSet<TreeNode> SubDirectory { get; }
        public SortedSet<TreeNode> SubFile { get; }

        #endregion

        #region Constructor
        public TreeNode(string name)
        {
            Name = new ColumnMember<string>(name);
            Info = new TreeNodeInfo();
            SubDirectory = new SortedSet<TreeNode>();
            SubFile = new SortedSet<TreeNode>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 다른 TreeNode와 이름을 비교하는 함수
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns>이 인스턴스와 ,treeNode 인스턴스의 Name 변수의 ComepareTo 값</returns>
        public int CompareTo(TreeNode treeNode) => Name.Value.CompareTo(treeNode.Name.Value);

        /// <summary>
        /// 자식노드들에서 이름을 검색하는 함수
        /// </summary>
        /// <param name="name"></param>
        /// <returns>검색에 성공했다면 찾은 노드를, 아니라면 null</returns>
        public TreeNode Find(string name) => SubDirectory.TryGetValue(new TreeNode(name), out TreeNode node) ? node : null;

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="propertystring"></param>
        /// <returns></returns>
        public object GetColumnMemberValue(string propertystring)
        {
            List<string> splitedString = propertystring.Split('.').ToList();
            splitedString.Add("Value");

            object result = this;

            foreach(string propertyName in splitedString)
            {
                object tmp = result.GetType().GetProperty(propertyName).GetValue(result, null);
                result = tmp;
            }
            return result;
        }
        #endregion
    }
}

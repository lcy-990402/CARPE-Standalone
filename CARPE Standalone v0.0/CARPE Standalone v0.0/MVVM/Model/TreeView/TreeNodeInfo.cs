using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CARPE_Standalone_v0._0.MVVM.Model.TreeView
{
    /// <summary>
    /// TreeNode의 세부 정보를 나타냅ㄴ디ㅏ.
    /// </summary>
    public class TreeNodeInfo
    {
        #region Members
        public ColumnMember<DateTime> CreatedTime { get; set; }
        public ColumnMember<DateTime> ModifiedTime { get; set; }
        public ColumnMember<DateTime> AccessedTime { get; set; }
        public ColumnMember<long> Size { get; set; }
        public ColumnMember<FileCount> Reversal { get; set; }
        public ColumnMember<bool ?> IsDirectory { get; set; }
        public ColumnMember<string> Path { get; set; }
        public ColumnMember<string> Extension { get; set; }
        public bool Ischecked { get; set; }
        public ImageSource Source
        {
            get
            {
                Image myImage = new Image();
                BitmapImage bit = new BitmapImage();

                bit.BeginInit();
                if (IsDirectory.Value == true)
                {
                    bit.UriSource = new Uri("/Images/Folder.png", UriKind.Relative);
                }
                else if (IsDirectory.Value == false)
                {
                    bit.UriSource = new Uri("/Images/File.png", UriKind.Relative);
                }
                else
                {
                    return null;
                }
                bit.EndInit();

                myImage.Stretch = Stretch.Fill;
                myImage.Source = bit;
                return myImage.Source;
            }
        }
        #endregion

        #region Constructor
        public TreeNodeInfo()
        {
            CreatedTime = new ColumnMember<DateTime>(default, DateTime_ToString);
            ModifiedTime = new ColumnMember<DateTime>(default, DateTime_ToString);
            AccessedTime = new ColumnMember<DateTime>(default, DateTime_ToString);
            Size = new ColumnMember<long>(0, size =>
            {
                if (size == -1)
                {
                    return "";
                }

                int cnt = 0;
                double tmp = size;
                while (tmp >= 1024)
                {
                    tmp /= 1024;
                    cnt++;
                }

                return String.Format("{0:F2}", tmp) + Constant.BYTE_UNIT[cnt];
            });
            Reversal = new ColumnMember<FileCount>(new FileCount());
            IsDirectory = new ColumnMember<bool?>(null, b =>
            {
                if (b == true) return "Directory";
                else if (b == false) return "File";
                else return "Unknown";
            });
            Path = new ColumnMember<string>("");
            Extension = new ColumnMember<string>("");
            Ischecked = false;

        }
        #endregion

        #region Methods
        /// <summary>
        /// 매개변수들을 받아 해당 변수들을 변경합니다.
        /// </summary>
        /// <param name="createdTime"></param>
        /// <param name="modifiedTime"></param>
        /// <param name="accessedTime"></param>
        /// <param name="size"></param>
        /// <param name="isDirectory"></param>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        public void SetDetail(DateTime createdTime, DateTime modifiedTime, DateTime accessedTime,
            long size, bool isDirectory, string path, string extension)
        {
            CreatedTime.Value = createdTime;
            ModifiedTime.Value = modifiedTime;
            AccessedTime.Value = accessedTime;
            if (isDirectory == false) Size.Value = size;
            if (isDirectory == true) Reversal.Value.IsCounted = true;
            IsDirectory.Value = isDirectory;
            Path.Value = path;
            Extension.Value = extension;
        }

        private string DateTime_ToString(DateTime time)
        {
            return time.Equals(default) ? "" : time.Add(Settings.UTC).ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion

    }
}

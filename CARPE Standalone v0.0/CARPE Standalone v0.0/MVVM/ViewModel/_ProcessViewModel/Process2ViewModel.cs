using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel._ProcessViewModel
{
    class Process2ViewModel : ObservableObject
    {

        public RelayCommand SelectAllCommand { get; set; }
        public RelayCommand disSelectAllCommand { get; set; }


        private List<Module> _moduleList;
        public List<Module> ModuleList
        {
            get { return _moduleList; }
            set
            {
                _moduleList = value;
                OnPropertyChanged("ModuleList");
            }
        }


        public Process2ViewModel()
        {
            ModuleList = new List<Module>
            {
               new Module() { chk = true, index = 0, module_name = "andforensics connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 1, module_name = "android basic apps connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 2, module_name = "android user apps connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 3, module_name = "chromium connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 4, module_name = "defa connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 5, module_name = "email connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 6, module_name = "esedb connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 7, module_name = "evernote connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 8, module_name = "filehistory connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 9, module_name = "iconcache connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 10, module_name = "jumplist connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 11, module_name = "kakaotalk mobile decrypt connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 12, module_name = "link connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 13, module_name = "mysql recovery connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 14, module_name = "notification connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 15, module_name = "ntfs connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 16, module_name = "prefetch connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 17, module_name = "recyclebin connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 18, module_name = "registry connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 19, module_name = "eventlog connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 20, module_name = "searchdb connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 21, module_name = "shellbag connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 22, module_name = "stickynote connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 23, module_name = "superfetch connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 24, module_name = "thumbnailcache connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 25, module_name = "windows timeline connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 26, module_name = "google drive connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 27, module_name = "google drive entry connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 28, module_name = "google drive fs connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 29, module_name = "google takeout connector", module_level = 1, description = "TBD..." },
               new Module() { chk = true, index = 30, module_name = "macos connector", module_level = 1, description = "TBD..." },
                //Module Level 2
               new Module() { chk = true, index = 31, module_name = "os app history analyzer", module_level = 2, description = "TBD..." },
               new Module() { chk = true, index = 32, module_name = "os log history analyzer", module_level = 2, description = "TBD..." },
               new Module() { chk = true, index = 33, module_name = "os mft history analyzer", module_level = 2, description = "TBD..." },
               new Module() { chk = true, index = 34, module_name = "os usage history analyzer", module_level = 2, description = "TBD..." },
               new Module() { chk = true, index = 35, module_name = "communication analyzer", module_level = 2, description = "TBD..." },
               new Module() { chk = true, index = 36, module_name = "timeline analyzer", module_level = 2, description = "TBD..." }
               //Add Update Plugins.
            };

            SelectAllCommand = new RelayCommand(o =>
            {
                foreach (Module item in ModuleList)
                {
                    item.chk = true;
                }
            });
            disSelectAllCommand = new RelayCommand(o =>
            {
                foreach (Module item in ModuleList)
                {
                    item.chk = false;
                }

            });
        }

    }
}

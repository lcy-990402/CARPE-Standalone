using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.Model
{
    public class Module : ObservableObject
    {
        private bool _chk;
        public bool chk
        {
            get
            {
                return _chk;
            }

            set
            {
                _chk = value;
                OnPropertyChanged("chk");
            }
        }
        public int index { get; set; }
        public string module_name { get; set; }
        public int module_level { get; set; }
        public string description { get; set; }
    }
}

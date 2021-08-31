using CarpeGUI_rework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpeGUI_rework.MVVM.Model
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

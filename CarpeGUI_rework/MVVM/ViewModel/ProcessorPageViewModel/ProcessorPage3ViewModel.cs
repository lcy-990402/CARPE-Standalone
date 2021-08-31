using CarpeGUI_rework.Core;
using CarpeGUI_rework.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;

namespace CarpeGUI_rework.MVVM.ViewModel.ProcessorPageViewModel
{
    class ProcessorPage3ViewModel:ObservableObject
    {
        public ProcessorPage2ViewModel page2 { get; set; }
        public ProcessorPage1ViewModel page1 { get; set; }
        

        private int _progress;

        public int progress
        {
            get { return _progress; }
            set 
            {
                _progress = value;
                OnPropertyChanged("progress");
            }
        }

        private string _percent;

        public string percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                OnPropertyChanged("percent");
            }
        }

    }
}

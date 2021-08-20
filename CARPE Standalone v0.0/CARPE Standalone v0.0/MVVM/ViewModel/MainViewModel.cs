using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region Member       
        
        /// <summary>
        /// Process용 ViewModel과 Analyze용 ViewModel
        /// </summary>
        public ProcessViewModel ProcessVM { get; set; }
        public AnalyzeViewModel AnalyzeVM { get; set; }

        /// <summary>
        /// Process, Analyze 버튼 커맨드
        /// </summary>
        public RelayCommand ProcessCommand { get; set; }
        public RelayCommand AnalyzeCommand { get; set; }

        /// <summary>
        /// 현재 View
        /// </summary>
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Method
        public MainViewModel()
        {                     

            ProcessCommand = new RelayCommand(o =>
            {
                ProcessVM = new ProcessViewModel();
                CurrentView = ProcessVM;
            });

            AnalyzeCommand = new RelayCommand(o =>
            {
                OpenFileDialog dig = new OpenFileDialog();
                dig.Filter = "db files (*.db) |*.db|All files (*.*)|*.*";
                dig.FilterIndex = 1;
                bool? result = dig.ShowDialog();

                if (result == true) TempData.DBPath = dig.FileName;

                AnalyzeVM = new AnalyzeViewModel();
                CurrentView = AnalyzeVM;
            });
        }
        #endregion
    }
}

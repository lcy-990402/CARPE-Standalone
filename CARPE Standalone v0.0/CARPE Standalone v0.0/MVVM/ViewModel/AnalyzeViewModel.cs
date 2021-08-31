using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.Data;
using CARPE_Standalone_v0._0.MVVM.ViewModel.Analyze;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class AnalyzeViewModel : ObservableObject
    {
        #region Member
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand TableViewCommand { get; set; }

        public RelayCommand TreeViewCommand { get; set; }

        public RelayCommand TempViewCommand { get; set; }

        public RelayCommand OffButtonCommand { get; set; }

        public HomeViewmodel HomeVM { get; set; }

        public TempViewModel TempVM { get; set; }

        public TableViewModel TableVM { get; set; }

        public TreeViewModel TreeVM { get; set; }



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
        public AnalyzeViewModel()
        {
            HomeVM = new HomeViewmodel();           
            TableVM = new TableViewModel();
            TreeVM = new TreeViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            TableViewCommand = new RelayCommand(o =>
            {
                CurrentView = TableVM;
            });

            TreeViewCommand = new RelayCommand(o =>
            {
                CurrentView = TreeVM;
            });

            TempViewCommand = new RelayCommand(o =>
            {
                if(TempVM == null) TempVM = new TempViewModel();
                CurrentView = TempVM;
            });

            OffButtonCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

        }
        #endregion
    }
}

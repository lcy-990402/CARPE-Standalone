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

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand OpenDBCommand { get; set; }

        public RelayCommand TempViewCommand { get; set; }

        public RelayCommand ProcessViewCommand { get; set; }

        public RelayCommand OffButtonCommand { get; set; }

        public HomeViewmodel HomeVM { get; set; }

        public TempViewModel TempVM { get; set; }

        public ProcessViewModel ProcessVM { get; set; }

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

        public MainViewModel()
        {
            HomeVM = new HomeViewmodel();            
            TempVM = new TempViewModel();
            ProcessVM = new ProcessViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            TempViewCommand = new RelayCommand(o =>
            {
                CurrentView = TempVM;
            });

            ProcessViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProcessVM;
            });

            OpenDBCommand = new RelayCommand(o =>
            {
                OpenFileDialog dig = new OpenFileDialog();
                dig.Filter = "db files (*.db) |*.db|All files (*.*)|*.*";
                dig.FilterIndex = 1;
                bool? result = dig.ShowDialog();

                if (result == true)
                {
                    TempData.DBPath = dig.FileName;
                }
                HomeVM.Update();
            });

            OffButtonCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

        }

    }
}

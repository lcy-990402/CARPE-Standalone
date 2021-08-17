using CarpeGUI_rework.Core;
using System;
using System.Text;
using System.Windows;

namespace CarpeGUI_rework.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProcessorViewCommand { get; set; }
        public RelayCommand Processor2ViewCommand { get; set; }



        public HomeViewModel HomeVm{ get; set; }
        public ProcessorViewModel ProcessorVM { get; set; }


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
            HomeVm = new HomeViewModel();
            ProcessorVM = new ProcessorViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            ProcessorViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProcessorVM;
            });


        }
    }
}

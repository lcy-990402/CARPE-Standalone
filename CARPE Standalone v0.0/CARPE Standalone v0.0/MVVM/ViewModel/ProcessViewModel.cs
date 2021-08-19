using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.MVVM.ViewModel._ProcessViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class ProcessViewModel : ObservableObject
    {
        public RelayCommand NextButtonCommand { get; set; }

        public RelayCommand HomeButtonCommand { get; set; }

        public Process1ViewModel P1VM { get; set; }
        public Process2ViewModel P2VM { get; set; }

        public Process3ViewModel P3VM { get; set; }



        private object _pv_currentView;

        public object PV_CurrentView
        {
            get { return _pv_currentView; }
            set {
                _pv_currentView = value;
                OnPropertyChanged();
            }
        }

        public ProcessViewModel()
        {
            P1VM = new Process1ViewModel();
            P2VM = new Process2ViewModel();
            P3VM = new Process3ViewModel();

            PV_CurrentView = P1VM;

            NextButtonCommand = new RelayCommand(o =>
            {
                if (PV_CurrentView == P1VM) PV_CurrentView = P2VM;
                else if (PV_CurrentView == P2VM) PV_CurrentView = P3VM;
            });

            HomeButtonCommand = new RelayCommand(o =>
            {

            });

        }

    }
}

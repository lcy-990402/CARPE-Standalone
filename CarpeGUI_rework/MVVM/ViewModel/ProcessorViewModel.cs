using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarpeGUI_rework.Core;
using CarpeGUI_rework.MVVM.ViewModel.ProcessorPageViewModel;
namespace CarpeGUI_rework.MVVM.ViewModel
{

    class ProcessorViewModel : ObservableObject
    {


        public RelayCommand NextButtonCommand { get; set; }
        public RelayCommand BeforeButtonCommand { get; set; }

        public RelayCommand SendDataCommand { get; set; }

        public ProcessorPage1ViewModel page1 { get; set; }
        public ProcessorPage2ViewModel page2 { get; set; }
        public ProcessorPage3ViewModel page3 { get; set; }


        private object _page_CurrentView;
        public object Page_CurrentView
        {
            get { return _page_CurrentView; }
            set
            {
                _page_CurrentView = value;
                OnPropertyChanged();
            }
        }
        

        public ProcessorViewModel()
        {
            page1 = new ProcessorPage1ViewModel();
            page2 = new ProcessorPage2ViewModel();
            page3 = new ProcessorPage3ViewModel();

            Page_CurrentView = page1;
            page1.timezone = "Asia/Seoul +09:00";

            NextButtonCommand = new RelayCommand(o =>
            {
                if (Page_CurrentView == page1)
                { 
                    Page_CurrentView = page2;
                }
                else
                if (Page_CurrentView == page2)
                {
                    Page_CurrentView = page3;
                }

            });

            BeforeButtonCommand = new RelayCommand(o =>
            {
                if (Page_CurrentView == page3) Page_CurrentView = page2;
                else if (Page_CurrentView == page2) Page_CurrentView = page1;

            });

        }
    }
}

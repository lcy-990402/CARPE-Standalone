using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CarpeGUI_rework.Core;
using CarpeGUI_rework.MVVM.ViewModel.ProcessorPageViewModel;
namespace CarpeGUI_rework.MVVM.ViewModel
{

    class ProcessorViewModel : ObservableObject
    {


        public RelayCommand NextButtonCommand { get; set; }
        public RelayCommand BeforeButtonCommand { get; set; }
        
        public RelayCommand ProcessCommand { get; set; }

        public RelayCommand SendDataCommand { get; set; }

        public ProcessorPage1ViewModel page1 { get; set; }
        public ProcessorPage2ViewModel page2 { get; set; }
        public ProcessorPage3ViewModel page3 { get; set; }

        private string _log_text;
        public string log_text
        {
            get { return _log_text; }
            set
            {
                _log_text = value;
                OnPropertyChanged("log_text");
            }

        }
        private Button _BeforeButton;
        public Button BeforeButton
        {
            get { return _BeforeButton; }
            set
            {
                _BeforeButton = value;
                OnPropertyChanged("BeforeButton");
            }

        }
        private Button _afterbutton;
        public Button Afterbutton
        {
            get { return _afterbutton; }
            set
            {
                _afterbutton = value;
                OnPropertyChanged("Afterbutton");
            }

        }

        private Button _processButton;

        public  Button ProcessButton
        {
            get { return _processButton; }
            set 
            { 
                _processButton = value;
                OnPropertyChanged("ProcessButton");
            }
        }


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
            //BeforeButton.Visibility = Visibility.Hidden;

            page1.timezone = "Asia/Seoul +09:00";
            NextButtonCommand = new RelayCommand(o =>
            {
                if (Page_CurrentView == page1)
                { 
                    // check if values are empty
                    if(page1.src_input == "" || page1.output_input == "" || page1.CaseID == "" || page1.evidenceid == "")
                    {
                        MessageBox.Show("Input Path, Output Path, CaseID, Evidence ID Must be Filled.");
                    }
                    else
                    {
                        Page_CurrentView = page2;
                        //BeforeButton.Visibility = Visibility.Visible;
                        //Afterbutton.Visibility = Visibility.Visible;
                    }
                }
                else
                if (Page_CurrentView == page2)
                {
                    Page_CurrentView = page3;
                    //BeforeButton.Visibility = Visibility.Hidden;
                    //Afterbutton.Visibility = Visibility.Hidden;
                    //start processing
                    string a = "asdfasdf";
                    log_text = a;
                    MessageBox.Show(log_text);
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

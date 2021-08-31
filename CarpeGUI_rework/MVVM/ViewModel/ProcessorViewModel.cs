using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CarpeGUI_rework.Core;
using CarpeGUI_rework.MVVM.Model;
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
            string payload = "";
            Page_CurrentView = page1;
            //BeforeButton.Visibility = Visibility.Hidden;

            page1.timezone = "Asia/Seoul +09:00";
            NextButtonCommand = new RelayCommand(o =>
            {
                if (Page_CurrentView == page1)
                { 
                    // check if values are empty
                    if(page1.src_input == null || page1.output_input == null || page1.caseid == null || page1.evidenceid == null)
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
                    
                    if (page1.caseid != "")
                    {
                        payload += "--cid " + page1.caseid + " ";
                    }
                    if (page1.casename != "")
                    {
                        payload += "--case-name " + page1.casename + " ";
                    }
                    if (page1.evidenceid != "")
                    {
                        payload += "--eid " + page1.evidenceid + " ";
                    }
                    if (page1.investigator != "")
                    {
                        payload += "--investigator " + page1.investigator + " ";
                    }
                    if (page1.description != "")
                    {
                        payload += "--case_desc " + page1.description + " ";
                    }
                    if (page1.timezone != null)
                    {
                        payload += "-z " + page1.timezone + " ";
                    }
                    //
                    //make ignore option ->  TBD

                    //get checked values from processor02
                    List<string> mod_checked = new List<string>();
                    foreach (Module item in page2.ModuleList)
                    {
                        if (item.chk == true)
                        {
                            mod_checked.Add(item.module_name.ToString());
                        }
                    }

                    //add to payload
                    if (mod_checked.Count != 0)
                        payload += "--modules ";
                    var last_idx = mod_checked.Last();
                    foreach (var mod in mod_checked)
                    {
                        payload += mod.Replace(" ", "_") + ",";
                        if (mod == last_idx) payload += mod.Replace(" ", "_");
                    }

                    payload += " --sqlite ";
                    //page3.log_text += payload;                    
                    page3.initiate(payload);
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

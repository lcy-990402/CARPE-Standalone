using CARPE_Standalone_v0._0.Core;
using CARPE_Standalone_v0._0.MVVM.Model;
using CARPE_Standalone_v0._0.MVVM.ViewModel._ProcessViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel
{
    class ProcessViewModel : ObservableObject
    {


        public RelayCommand NextButtonCommand { get; set; }
        public RelayCommand BeforeButtonCommand { get; set; }

        public RelayCommand ProcessCommand { get; set; }

        public RelayCommand SendDataCommand { get; set; }

        public RelayCommand OffButtonCommand { get; set; }

        public Process1ViewModel page1 { get; set; }
        public Process2ViewModel page2 { get; set; }
        public Process3ViewModel page3 { get; set; }




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


        public ProcessViewModel()
        {
            page1 = new Process1ViewModel();
            page2 = new Process2ViewModel();
            page3 = new Process3ViewModel();
            string payload = "";
            Page_CurrentView = page1;
            //BeforeButton.Visibility = Visibility.Hidden;

            page1.timezone = "Asia/Seoul +09:00";
            NextButtonCommand = new RelayCommand(o =>
            {
                if (Page_CurrentView == page1)
                {
                    // check if values are empty
                    if (page1.src_input == null || page1.output_input == null || page1.caseid == null || page1.evidenceid == null)
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

                    payload += "\"" + page1.src_input + "\" ";
                    payload += "\"" + page1.output_input + "\" ";

                    if (page1.caseid != null)
                    {
                        payload += "--cid " + page1.caseid + " ";
                    }
                    if (page1.casename != null)
                    {
                        payload += "--case-name " + page1.casename + " ";
                    }
                    if (page1.evidenceid != null)
                    {
                        payload += "--eid " + page1.evidenceid + " ";
                    }
                    if (page1.investigator != null)
                    {
                        payload += "--investigator " + page1.investigator + " ";
                    }
                    if (page1.description != null)
                    {
                        payload += "--case_desc " + page1.description + " ";
                    }
                    if (page1.timezone != null)
                    {
                        payload += "-z " + page1.timezone.Split(' ')[0] + " ";
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

            OffButtonCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

        }
    }
}

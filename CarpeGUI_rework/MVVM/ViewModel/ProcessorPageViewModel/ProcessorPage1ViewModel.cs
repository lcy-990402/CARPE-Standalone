using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CarpeGUI_rework.Core;
using CarpeGUI_rework.MVVM.View.ProcessorPage;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarpeGUI_rework.MVVM.ViewModel.ProcessorPageViewModel
{
    class ProcessorPage1ViewModel: ObservableObject
    {
        private string _src_input;
        public string src_input
        {
            get { return _src_input; }
            set 
            {
                _src_input = value;
                OnPropertyChanged("src_input");
            }
            
        }

        private string _output_input;
        public string output_input
        {
            get { return _output_input; }
            set
            {
                _output_input = value;
                OnPropertyChanged("output_input");
            }

        }

        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("output_input");
            }

        }

        private string _casename;
        public string casename
        {
            get { return _casename; }
            set
            {
                _casename = value;
                OnPropertyChanged("casename");
            }

        }
        
        private string _CaseID;
        public string CaseID
        {
            get { return _CaseID; }
            set
            {
                _CaseID = value;
                OnPropertyChanged("CaseID");
            }

        }
        
        private string _evidenceid;
        public string evidenceid
        {
            get { return _evidenceid; }
            set
            {
                _evidenceid = value;
                OnPropertyChanged("evidenceid");
            }

        }
        
        private string _investigator;
        public string investigator
        {
            get { return _investigator; }
            set
            {
                _investigator = value;
                OnPropertyChanged("investigator");
            }

        }
        
        public string _timezone;
        public string timezone
        {
            get 
            {
                return _timezone;
            }
            set
            {
                _timezone = value;
                OnPropertyChanged("timezone");
            }
        }

        public Processor01 pg1 { get; set; }
        public ProcessorPage1ViewModel()
        {
            pg1 = new Processor01();

        }
    }
}

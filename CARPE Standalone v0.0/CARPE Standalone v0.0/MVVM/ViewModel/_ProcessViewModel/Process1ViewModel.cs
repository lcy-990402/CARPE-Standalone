using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel._ProcessViewModel
{
    class Process1ViewModel : ObservableObject
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

        private string _caseid;
        public string caseid
        {
            get { return _caseid; }
            set
            {
                _caseid = value;
                OnPropertyChanged("caseid");
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

        public RelayCommand OpenfileClick { get; set; }
        public RelayCommand OpenpathClick { get; set; }

        public Process1ViewModel pg1 { get; set; }
        public Process1ViewModel()
        {
            OpenfileClick = new RelayCommand(o =>
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    src_input = openFileDialog.FileName;
                }
            });

            OpenpathClick = new RelayCommand(o =>
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    output_input = fbd.SelectedPath;
                }
            });



        }
    }
}

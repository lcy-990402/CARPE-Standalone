using CARPE_Standalone_v0._0.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CARPE_Standalone_v0._0.MVVM.ViewModel._ProcessViewModel
{
    class Process3ViewModel : ObservableObject
    {
        public Process2ViewModel page2 { get; set; }
        public Process1ViewModel page1 { get; set; }
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

        private int _progress;

        public int progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged("progress");
            }
        }

        private string _percent;

        public string percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                OnPropertyChanged("percent");
            }
        }
        public void initiate(string payload)
        {
            Process p = new Process();
            p.StartInfo.FileName = @".\carpe.exe";
            p.StartInfo.Arguments = payload;
            p.StartInfo.WorkingDirectory = @".\";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.EnableRaisingEvents = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { if (e.Data != null) log_text += "\n" + e.Data.ToString(); }));
                    //progress bar 
                    string data;
                    data = e.Data.ToString();

                    if (data.Equals("  ____                      "))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 1;
                            percent = "1%";
                        }));

                    }
                    if (data.Equals("Checking availability and versions of dependencies."))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 2;
                            percent = "2%";
                        }));

                    }
                    if (data.Equals("[OK]"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 5;
                            percent = "5%";
                        }));
                    }
                    if (data.Equals("Processing started."))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 8;
                            percent = "8%";
                        }));
                    }
                    if (data.Split(':')[0].Equals("Output Path"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 9;
                            percent = "9%";
                        }));
                    }
                    if (data.Equals(""))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 9;
                            percent = "9%";
                        }));
                    }
                    if (data.Split(':')[0].Equals("The number of partition "))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 12;
                            percent = "12%";
                        }));
                    }
                    if (data.Contains("Finish Analyze Image"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 100;
                            percent = "100%";
                        }));
                    }
                    if (data.Contains("Start Analyze Image"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 10;
                            percent = "10%";
                        }));
                    }
                    if (data.Contains("Insert File Information"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 12;
                            percent = "12%";
                        }));
                    }
                    if (data.Contains("Determine Operation System"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 14;
                            percent = "14%";
                        }));
                    }
                    if (data.Contains("Set Timezone"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 17;
                            percent = "17%";
                        }));
                    }
                    if (data.Contains("Set Modules"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 19;
                            percent = "19%";
                        }));
                    }
                    if (data.Contains("Module for AndForensics Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 21;
                            percent = "21%";
                        }));
                    }
                    if (data.Contains("Module for Android Basic Apps Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 24;
                            percent = "24%";
                        }));
                    }
                    if (data.Contains("Module for Android User Apps Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 26;
                            percent = "26%";
                        }));
                    }
                    if (data.Contains("Module for Chromium Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 28;
                            percent = "28%";
                        }));
                    }
                    if (data.Contains("Module for DEFA Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 31;
                            percent = "31%";
                        }));
                    }
                    if (data.Contains("MModule for E-mail Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 33;
                            percent = "33%";
                        }));
                    }
                    if (data.Contains("Module for ESEDB Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 35;
                            percent = "35%";
                        }));
                    }
                    if (data.Contains("Module for Evernote Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 38;
                            percent = "38%";
                        }));
                    }
                    if (data.Contains("Module for FileHistory Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 40;
                            percent = "40%";
                        }));
                    }
                    if (data.Contains("Module for Iconcache Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 42;
                            percent = "42%";
                        }));
                    }
                    if (data.Contains("Module for Jumplist Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 45;
                            percent = "45%";
                        }));
                    }
                    if (data.Contains("Module for Chromium Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 48;
                            percent = "48%";
                        }));
                    }
                    if (data.Contains("Module for Kakaotalk Mobile Decrypt Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 50;
                            percent = "50%";
                        }));
                    }
                    if (data.Contains("Module for Link Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 52;
                            percent = "52%";
                        }));
                    }
                    if (data.Contains("Module for Mysql Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 55;
                            percent = "55%";
                        }));
                    }
                    if (data.Contains("Module for Chromium Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 57;
                            percent = "57%";
                        }));
                    }
                    if (data.Contains("Module for Notification Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 59;
                            percent = "59%";
                        }));
                    }
                    if (data.Contains("Module for DFIR_NTFS Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 62;
                            percent = "62%";
                        }));
                    }
                    if (data.Contains("Module for Prefetch Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 64;
                            percent = "64%";
                        }));
                    }
                    if (data.Contains("Module for RecycleBin Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 66;
                            percent = "66%";
                        }));
                    }
                    if (data.Contains("Module for Eventlog Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 69;
                            percent = "69%";
                        }));
                    }
                    if (data.Contains("Module for SearchDB Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 71;
                            percent = "71%";
                        }));
                    }
                    if (data.Contains("Module for Shellbag Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 73;
                            percent = "73%";
                        }));
                    }
                    if (data.Contains("Module for StickyNote Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 76;
                            percent = "76%";
                        }));
                    }
                    if (data.Contains("Module for Superfetch Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 78;
                            percent = "78%";
                        }));
                    }
                    if (data.Contains("Module for ThumbnailCache Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 80;
                            percent = "80%";
                        }));
                    }
                    if (data.Contains("Module for Windows_timeline Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 83;
                            percent = "83%";
                        }));
                    }
                    if (data.Contains("Module for Googledrive_Filestream Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 85;
                            percent = "85%";
                        }));
                    }
                    if (data.Contains("Module for Googledrive_Sync_local_entry_info Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 87;
                            percent = "87%";
                        }));
                    }
                    if (data.Contains("Module for Googledrive_Sync_fschange Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 90;
                            percent = "90%";
                        }));
                    }
                    if (data.Contains("Google Takeout Connector Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 92;
                            percent = "92%";
                        }));
                    }
                    if (data.Contains("Module for MacOS Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 94;
                            percent = "94%";
                        }));
                    }
                    if (data.Contains("Module for Android Email Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 97;
                            percent = "97%";
                        }));
                    }
                    if (data.Contains("Module for Googledrive_Sync_fschange Start"))
                    {
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                            progress = 99;
                            percent = "99%";
                        }));
                    }
                }
            });
            p.Start();
            p.BeginOutputReadLine();
            string stderrx = p.StandardError.ReadToEnd();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                if (stderrx != "")
                {
                    log_text += "\n------------------------------------------------------------------------";
                    log_text += "\n[ERROR]\n" + stderrx;
                    log_text += "\n------------------------------------------------------------------------";
                }
            }));
        }
        public Process3ViewModel()
        {

        }
    }
}

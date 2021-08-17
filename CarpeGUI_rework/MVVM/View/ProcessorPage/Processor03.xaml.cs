using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data.SQLite;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarpeGUI_rework.MVVM.View.ProcessorPage
{
    /// <summary>
    /// Processor03.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Processor03 : UserControl
    {
        public Processor03()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = @".\CarpeAnalyzer_GUI.exe";
            p.StartInfo.WorkingDirectory = @".\";
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.EnableRaisingEvents = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();

            //명령어 전달

        }
        private void log_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            log_text.ScrollToEnd();
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            string src;
            string query;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarpeGUI_rework.MVVM.View.ProcessorPage
{
    /// <summary>
    /// Processor01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Processor01 : UserControl
    {
        public Processor01()
        {
            InitializeComponent();
        }

        private void OpenFilePath_Click1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                source_input.Text = openFileDialog.FileName;
            }
        }
        private void OpenFilePath_Click2(object sender, RoutedEventArgs e)
        {

            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                output_input.Text = fbd.SelectedPath;
            }

        }

    }
}

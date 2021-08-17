using System;
using System.Collections.Generic;
using System.IO;
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
    /// Processor02.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Processor02 : UserControl
    {
        public static string e_tmp;
        List<Module_List> mod_list = null;
        public string cmd = "";
        public Processor02()
        {
            InitializeComponent();
            fill_table();
        }
        public class Module_List
        {
            public bool chk { get; set; }
            public int index { get; set; }
            public string module_name { get; set; }
            public string module_level { get; set; }
            public string description { get; set; }
        }
        private void fill_table()
        {
            var lineCount = File.ReadAllLines("./etc/__init__.py").Length;
            mod_list = new List<Module_List>();
            int count = 1;
            for (int i = 0; i < lineCount; i++)
            {
                string[] text = File.ReadAllLines("./etc/__init__.py");
                if (text[i].IndexOf('#') == 0 || text[i] == "")
                {
                    continue;
                }
                if (text[i].IndexOf("DEFA") > 0)
                {
                    continue;
                }
                text[i] = text[i].Replace("from modules import ", "").Replace("_", " ");
                mod_list.Add(new Module_List() { chk = true, index = count, module_name = text[i], module_level = "1", description = "" });
                count++;
            }
            lineCount = File.ReadAllLines(@"./etc/__init__(2).py").Length;
            for (int i = 0; i < lineCount; i++)
            {
                string[] text = File.ReadAllLines(@"./etc/__init__(2).py");
                if (text[i].IndexOf('#') == 0 || text[i] == "")
                {
                    continue;
                }
                if (text[i].IndexOf("DEFA") > 0)
                {
                    continue;
                }
                text[i] = text[i].Replace("from advanced_modules import ", "").Replace("_", " ").Replace("lv2 ", "");
                mod_list.Add(new Module_List() { chk = true, index = count, module_name = text[i], module_level = "2", description = "" });
                count++;
            }
            moduleListView.ItemsSource = mod_list;
        }
        private void select1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < mod_list.Count; i++)
            {
                mod_list[i].chk = true;
            }
            moduleListView.ItemsSource = mod_list;
            moduleListView.Items.Refresh();
        }
        private void select2_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < mod_list.Count; i++)
            {
                mod_list[i].chk = false;
            }
            moduleListView.ItemsSource = mod_list;
            moduleListView.Items.Refresh();
        }
    }
}

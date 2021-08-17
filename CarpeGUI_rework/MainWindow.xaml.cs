using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Data.SQLite;
using System.ComponentModel;
using System.Windows.Navigation;
using System.Windows.Input;

namespace CarpeGUI_rework
{
    public partial class MainWindow : Window {
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
    
}

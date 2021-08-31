using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Member
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Method
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPE_Standalone_v0._0.Data
{
    class TempData
    { 

        public static string DBPath { get; set; }

        private static Dictionary<string, DataTable> CarpeTable;

        public static DataTable getTable(string str)
        {
            if (CarpeTable == null) CarpeTable = new Dictionary<string, DataTable>();
            return CarpeTable[str];
        }
        public static void setTable(string str, DataTable dt)
        {
            if (CarpeTable == null) CarpeTable = new Dictionary<string, DataTable>();
            if (!CarpeTable.ContainsKey(str)) CarpeTable.Add(str, dt);
        }
    }
}

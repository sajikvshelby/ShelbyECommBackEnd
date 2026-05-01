using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelbyBackEnd.Application.Common.Service
{
    public class SharedLib
    {

        public static string GetRegKeyVal(string SubKeyName, string RegValueName)
        {
            Microsoft.Win32.RegistryKey RegKey = Microsoft.Win32.Registry.LocalMachine;
            string RegValue = "";

            try
            {
                RegValue = RegKey.OpenSubKey(SubKeyName, false).GetValue(RegValueName, "").ToString();
            }
            catch
            {
            }

            return RegValue;
        }
    }
}

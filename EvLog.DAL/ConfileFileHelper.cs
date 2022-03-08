using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.DAL
{
    public class ConfileFileHelper
    {
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        private static extern long WritePrivatefileString(string section, string key, string value, string filePath);
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern IntPtr GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern IntPtr GetPrivateProfileStringA(string section, string key, string def, Byte[] retVal, int size, string filePath);



        public string ReadIniData(string section, string key, string noText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder();
                GetPrivateProfileString(section, key, noText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public bool WriteIniData(string section, string key, string value, string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    long Opstation = WritePrivatefileString(section, key, value, filePath);
                    if (Opstation == 0)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
                return false;
        }



    }
}


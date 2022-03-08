using EvLog.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class EventTXTRead_Service
    {
        /// <summary>
        /// 读取报警ID 和对应的信息列表.
        /// </summary>
        /// <param name="configfile"></param>
        /// <param name="section"></param>
        /// <returns>返回Dic结构的信息表</returns>
        public Dictionary<int, string> EventTXTRead(ConfigModel configfile, string section)
        {
            Dictionary<int, string> MSG_dic = new Dictionary<int, string>();
            MSG_dic.Clear();
            
            if (section == configfile.EIP_Config.Section && !string.IsNullOrEmpty(configfile.EIP_Config.Section) && File.Exists(configfile.EIP_Config.EventTxTFilePath))
            {
                try
                {
                    FileStream fs = new FileStream(configfile.EIP_Config.EventTxTFilePath, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    fs.Seek(0, SeekOrigin.Begin);
                    string content = sr.ReadLine();
                    while (content != null)
                    {
                        if (content.Contains(","))
                        {
                            string[] arraryFile;
                            arraryFile = content.Split(',');
                            MSG_dic.Add(Convert.ToInt16(arraryFile[0]), arraryFile[1]);
                        }
                        content = sr.ReadLine();
                    }
                }
                catch (IOException e)
                {

                    throw e;
                }

            }
            if (section == configfile.SIE_Config.Section && !string.IsNullOrEmpty(configfile.EIP_Config.Section) && File.Exists(configfile.EIP_Config.Section))
            {
                try
                {
                    FileStream fs = new FileStream(configfile.SIE_Config.EventTxTFilePath, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    fs.Seek(0, SeekOrigin.Begin);
                    string content = sr.ReadLine();
                    while (content != null)
                    {
                        if (content.Contains(","))
                        {
                            string[] arraryFile;
                            arraryFile = content.Split(',');
                            MSG_dic.Add(Convert.ToInt16(arraryFile[0]), arraryFile[1]);
                        }
                        content = sr.ReadLine();
                    }
                }
                catch (IOException e)
                {

                    throw e;
                }

            }
            return MSG_dic;
        }




    }
}

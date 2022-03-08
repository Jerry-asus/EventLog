using EvLog.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class GetLogFileService
    {
        /// <summary>
        /// 获取日志文件的编号,初始化应用时获取目录下的文件编号
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static LogFileCfg GetLogFile(string sectionName,string appPath)
        {
            if(sectionName == null || appPath == null)
            {
                return null;
            }

            List<int> list = new List<int>();
            LogFileCfg logFile_Temp = new LogFileCfg();
            string logMainFolder = appPath+"\\LogFile";
            //如果LogFile目录不存在  创建目录
            if (!Directory.Exists(logMainFolder))
            {
                //create folder
                try
                {
                    Directory.CreateDirectory(logMainFolder);
                }
                catch (IOException e)
                {
                    throw e;
                }
            }
            //如果存在Section 查询是否存在该目录,不存在创建该目录,同时创建1号文件

                string logFolder = logMainFolder + "\\" + sectionName;
                if (!Directory.Exists(logFolder))
                {
                    //create folder
                    try
                    {
                        Directory.CreateDirectory(logFolder);
                    }
                    catch (IOException e)
                    {
                        throw e;
                    }
                    logFile_Temp.CurrentFileNum = 1;
                    
                }
                logFile_Temp.LogFileFolder = logFolder;

            //获取Section目录下所有的文件,如果不存在任何问价,则创建一个只有一行的文件,,并标记为1.txt.
            DirectoryInfo LogFileFolder = new DirectoryInfo(logFile_Temp.LogFileFolder);
            try
            {
                FileInfo[] fileList = LogFileFolder.GetFiles();
                if (LogFileFolder.Exists == true && fileList.Length == 0)
                {
                    logFile_Temp.CurrentFileNum = 1;
                }
                //如果目录下存在多个文件  获取所有文件的最后修改时间  获取最后修改文件的.
                if (LogFileFolder.Exists == true && fileList.Length > 0)
                {
                    var fc = new FileComparer();
                    Array.Sort(fileList, fc);
                    string[] arraryFile;
                    arraryFile = fileList[0].Name.Split('.');
                    logFile_Temp.CurrentFileNum = Convert.ToInt16(arraryFile[0]);
                }
            }
            catch (IOException e)
            {

                throw e;
            }
            
            
            return logFile_Temp;
        }
    }
    public class FileComparer : IComparer
    {
        /// <summary>
        /// 文件排序
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        int IComparer.Compare(object o1, object o2)
        {
            FileInfo fi1 = o1 as FileInfo;
            FileInfo fi2 = o2 as FileInfo;
            return fi2.LastWriteTime.CompareTo(fi1.LastWriteTime);
        }


    }
}

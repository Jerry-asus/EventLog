using EvLog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class LogMessageService
    {
        public  string Section { get; set; }
        public  string Appath { get; set; }
        public  int FileSize { get; set; }
        public  int FileLength { get; set; }
        /// <summary>
        /// 日志记录到LogFile
        /// </summary>
        /// <param name="msg"></param>
         public  void LogMessage(LogView logview)
        {
            if (string.IsNullOrEmpty(Section)) return;
            if(string.IsNullOrEmpty(Appath)) return;
            //DataGridModel model = new DataGridModel();
            LogFileCfg logFile=new LogFileCfg();
            logFile = GetLogFileService.GetLogFile(Section, Appath);
            string logFileFullPath = logFile.LogFileFolder + "\\" + logFile.CurrentFileNum.ToString() + ".txt";
            if(!File.Exists(logFileFullPath))
            {
                CreatEmptyFile(logFileFullPath);
            }
            FileInfo logFileInfo = new FileInfo(logFileFullPath);
            if (logFileInfo.Length > FileLength)
            {
                logFile.CurrentFileNum = logFile.CurrentFileNum + 1;
                if (logFile.CurrentFileNum >= FileSize)
                {
                    logFile.CurrentFileNum = logFile.CurrentFileNum + 1;
                }
                logFileFullPath = logFile.LogFileFolder + "\\" + logFile.CurrentFileNum.ToString() + ".txt";
                CreatEmptyFile(logFileFullPath);
            }
            try
            {
                FileStream fs = new FileStream(logFileFullPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(logview.CurrentTime+ "  "+ logview.Descript + "\n");
                sw.Close();
                fs.Close();
                
            }
            catch (IOException e)
            {
                throw e;

            }
        }


        //public  void LogMessage( List<LogView> logList)
        //{
        //    List<DataGridModel> BingL =new List<DataGridModel>();
        //    DataGridModel model = new DataGridModel();
        //    LogFileCfg logFile = new LogFileCfg();
        //    logFile = GetLogFileService.GetLogFile(Section, Appath);
        //    string logFileFullPath = logFile.LogFileFolder + "\\" + logFile.CurrentFileNum.ToString() + ".txt";
        //    if (!File.Exists(logFileFullPath))
        //    {
        //        CreatEmptyFile(logFileFullPath);
        //    }
        //    FileInfo logFileInfo = new FileInfo(logFileFullPath);
        //    if (logFileInfo.Length > 1024)
        //    {
        //        logFile.CurrentFileNum = logFile.CurrentFileNum + 1;
        //        if (logFile.CurrentFileNum >= 100)
        //        {
        //            logFile.CurrentFileNum = logFile.CurrentFileNum + 1;
        //        }
        //        logFileFullPath = logFile.LogFileFolder + "\\" + logFile.CurrentFileNum.ToString() + ".txt";
        //        CreatEmptyFile(logFileFullPath);
        //    }
        //    try
        //    {
        //        FileStream fs = new FileStream(logFileFullPath, FileMode.Append);
        //        StreamWriter sw = new StreamWriter(fs);
        //        for (int i = 0; i < msg.Count-1; i++)
        //        {
        //            string dt = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss:fff");
        //            sw.Write(dt + "  " + msg[i] + "\n");
        //            BingL.Add(model);
        //        }
        //        sw.Close();
        //        fs.Close();

        //    }
        //    catch (IOException e)
        //    {
        //        throw e;
        //    }
        //}
        private  void CreatEmptyFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            string startMsg = "===============" + Section + "===============" + "\n" +"Time                                   "+"Descrip"+"\n";             
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(startMsg);
                    sw.Close();
                    fs.Close();
                }
                catch (IOException e)
                {

                    throw e;
                }
        }



    }
}

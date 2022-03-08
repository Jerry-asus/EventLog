using EvLog.DAL;
using EvLog.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class GetConfigService
    {
        ConfileFileHelper CfgHelper_Obj = new ConfileFileHelper();
        /// <summary>
        /// 读取配置文件地址设定等参数
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>返回配置文件对象</returns>
        public ConfigModel GetIniFile(string filepath)
        {
            //如果文件不存在,将先创建一个默认的文件.
            if (!File.Exists(filepath))
            {
                try
                {
                    IniConfigFile(filepath);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            ConfigModel configModel = new ConfigModel();
            try
            {
                configModel.EIP_Config.PLC_Address = CfgHelper_Obj.ReadIniData("ControlLogix", "IPAddress", "192.168.100.1", filepath);
                configModel.EIP_Config.CPU_Path = CfgHelper_Obj.ReadIniData("ControlLogix", "Rock_num", "1,0", filepath);
                configModel.EIP_Config.AutoStart = CfgHelper_Obj.ReadIniData("ControlLogix", "AutoStart", "false", filepath);
                configModel.EIP_Config.Var_BufferNotEmpty = CfgHelper_Obj.ReadIniData("ControlLogix", "BufferNotEmpty", "BufferNotEmpty", filepath);
                configModel.EIP_Config.Var_MsgCount = CfgHelper_Obj.ReadIniData("ControlLogix", "MsgCount", "MsgCount", filepath);
                configModel.EIP_Config.Var_SendMsg = CfgHelper_Obj.ReadIniData("ControlLogix", "SendMsg", "SendMsg", filepath);
                configModel.EIP_Config.LogFilePath = CfgHelper_Obj.ReadIniData("ControlLogix", "LogFilePath", "", filepath);
                configModel.EIP_Config.EventTxTFilePath = CfgHelper_Obj.ReadIniData("ControlLogix", "EventTxTFilePath", "", filepath);
            }
            catch (Exception e)
            {
                throw e;
            }
            return configModel;
        }
        /// <summary>
        ///初始化 配置文件
        /// </summary>
        private void IniConfigFile(string filePath)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(filePath,false,Encoding.Unicode);
                string EventTableFile = filePath.Replace("Cfg.ini", "")+ "EventTable.txt";
                TextWriter textWriter1 = new StreamWriter(EventTableFile, false, Encoding.Unicode);
                textWriter.Close();
                textWriter1.Close();
                CfgHelper_Obj.WriteIniData("ControlLogix", "IPAddress", "192.168.0.1", filePath);
                CfgHelper_Obj.WriteIniData("ControlLogix", "Rock_num", "0.2", filePath);
                CfgHelper_Obj.WriteIniData("ControlLogix", "BufferNotEmpty", "SendBuffer.BufferNotEmpty", filePath);
                CfgHelper_Obj.WriteIniData("ControlLogix", "MsgCount", "SendBuffer.MsgCount", filePath);
                CfgHelper_Obj.WriteIniData("ControlLogix", "SendMsg", "SendBuffer.SendMSG", filePath);
                CfgHelper_Obj.WriteIniData("ControlLogix", "EventTxTFilePath", EventTableFile, filePath);
                CfgHelper_Obj.WriteIniData("SIE_1517F", "IPAddress", "192.168.0.1", filePath);
                CfgHelper_Obj.WriteIniData("SIE_1517F", "Rock_num", "0.2", filePath);
            }
            catch (IOException e)
            {

                throw e;
            }




        }


    }
}

using EvLog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using EvLog.BLL;
using libplctag;
using libplctag.DataTypes;
using System.IO;
using Sharp7;
using System.Reflection;
using System.Collections.Concurrent;

namespace EvLogSevice
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();

        }

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

        static UI_Status uI_Status = new UI_Status();

        /// <summary>
        /// 配置文件路径
        /// </summary>
        static string CfgFilepath = Application.StartupPath + "\\Cfg.ini";
        /// <summary>
        /// LogFile文件目录
        /// </summary>
        static string LogFileFolder = Application.StartupPath + "\\LogFile";

        static string EventTableFile = Application.StartupPath + "\\Event.txt";

        static string AppPath = Application.StartupPath;
        /// <summary>
        /// 配置文件
        /// </summary>
        public static ConfigModel Cfg_Obj = new ConfigModel();
        /// <summary>
        /// EIP Event 信息表
        /// </summary>
        static Dictionary<int, string> EIPEventDic_Obj = new Dictionary<int, string>();
        /// <summary>
        /// 1500 Event 信息表
        /// </summary>
        static Dictionary<int, string> SIEEventDic_Obj = new Dictionary<int, string>();


        public delegate EIP_Model Refresh_EIPUI();


        static Tag<BoolPlcMapper, bool> BufferNotEmpty_St = null;
        static Tag<SintPlcMapper, sbyte> MsgCount = null;
        static Tag<StringPlcMapper, string> EventMsg = null;

        RsLogixFrm rsLogixFrm = null;
        Tia1500Frm tia1500Frm = null;
        public delegate void UpDataCfg(EIP_Model o);
        public event UpDataCfg _UpDataCfg;


        static BindingList<LogView> BingL = new BindingList<LogView>();
        //public delegate void BindListUpData(LogView o);

        static ConcurrentQueue<LogView> LogMsg = new ConcurrentQueue<LogView>();

        private void MainFrm_Load(object sender, EventArgs e)
        {
            //获取配置文件参数
            GetConfigService getCfg = new GetConfigService();
            Cfg_Obj = getCfg.GetIniFile(CfgFilepath);
            //获取logFile文档目录
            GetLogFileService getLogFile = new GetLogFileService();
           // Lfg_Obj = getLogFile.GetLogFile(Cfg_Obj, LogFileFolder);
            //获取时间解析文件表
            EventTXTRead_Service GetEventTxt = new EventTXTRead_Service();
            EIPEventDic_Obj = GetEventTxt.EventTXTRead(Cfg_Obj, Cfg_Obj.EIP_Config.Section);
            SIEEventDic_Obj = GetEventTxt.EventTXTRead(Cfg_Obj, Cfg_Obj.SIE_Config.Section);
            ReadPLC(Cfg_Obj, EIPEventDic_Obj);



            CloseForm();
            rsLogixFrm = new RsLogixFrm();
            rsLogixFrm.receive += rsLogixFrm.Updata;
            rsLogixFrm.SetValue(Cfg_Obj.EIP_Config);
            OpenForm(rsLogixFrm);

            Dgv_LogView.AutoGenerateColumns = true;
            Dgv_LogView.DataSource = BingL;

            Thread LogFileThrad = new Thread(LogFile);
            LogFileThrad.IsBackground = true;
            LogFileThrad.Start();


        }

        public static void AddLogMsg(string Msg)
        {
            if(LogMsg.Count < 65530)
            {
                LogMsg.Enqueue(new LogView
                { CurrentTime = DateTime.Now.ToString(""),Descript = Msg });
            }
        }
        /// <summary>
        /// 独立线程保存日志文件
        /// </summary>
        private void LogFile()
        {
            LogMessageService LogMsgObj=new LogMessageService();
            LogMsgObj.Appath = AppPath;
            LogMsgObj.Section = Cfg_Obj.EIP_Config.Section;
            LogMsgObj.FileLength = 100;
            LogMsgObj.FileSize = 1024;
            while (true)
            {
                if (LogMsg.Count >0)
                {
                    LogView logView = new LogView();
                    LogMsg.TryDequeue(out logView);
                    LogMsgObj.LogMessage(logView);
                    if(Dgv_LogView.InvokeRequired)
                    {
                        if(BingL.Count>5)
                        {
                            Action<LogView> action1 = (x) => { BingL.RemoveAt(0); };
                        }
                        Action<LogView> action = (x) => { BingL.Add(logView); };
                        Dgv_LogView.Invoke(action, logView);
                    }
                    else
                    {
                        if (BingL.Count > 5)
                        {
                            BingL.RemoveAt(0);
                        }
                        BingL.Add(logView);
                    }
                }
            }
        }


        private  void ReadPLC(object obj, Dictionary<int, string> dic)
        {
            ConfigModel tempcfg = new ConfigModel();
            string SectionName = string.Empty;
            ReadABModel abCfg = new ReadABModel();

            tempcfg = (ConfigModel)obj;
            abCfg.CfgModel = tempcfg;
            abCfg.EventDic = dic;

            if (tempcfg.EIP_Config.Section == "ControlLogix")
            {
                LoadTag(tempcfg);
                Thread CycleReadRAPLC = new Thread(new ParameterizedThreadStart(ReadRAPLC));
                CycleReadRAPLC.IsBackground = true;
                CycleReadRAPLC.Start(abCfg);
                //ReadRAPLC(abCfg);
            }
            if (tempcfg.SIE_Config.Section == "SIE_1500")
            {
                ;
                ReadSIEPLC(tempcfg, SIEEventDic_Obj);
            }
        }

        private static void ReadSIEPLC(ConfigModel cfg, Dictionary<int, string> SIEMsg_Dic)
        {

        }

        private static void LoadTag(ConfigModel Cfg_Obj)
        {
            BufferNotEmpty_St = new Tag<BoolPlcMapper, bool>()
            {
                Name = Cfg_Obj.EIP_Config.Var_BufferNotEmpty,
                Gateway = Cfg_Obj.EIP_Config.PLC_Address,
                Path = Cfg_Obj.EIP_Config.CPU_Path,
                PlcType = PlcType.ControlLogix,
                Protocol = Protocol.ab_eip,
                Timeout = TimeSpan.FromSeconds(5)
            };

            MsgCount = new Tag<SintPlcMapper, sbyte>()
            {
                Name = Cfg_Obj.EIP_Config.Var_MsgCount,
                Gateway = Cfg_Obj.EIP_Config.PLC_Address,
                Path = Cfg_Obj.EIP_Config.CPU_Path,
                PlcType = PlcType.ControlLogix,
                Protocol = Protocol.ab_eip,
                Timeout = TimeSpan.FromSeconds(5)
            };

            EventMsg = new Tag<StringPlcMapper, string>()
            {
                Name = Cfg_Obj.EIP_Config.Var_SendMsg,
                Gateway = Cfg_Obj.EIP_Config.PLC_Address,
                Path = Cfg_Obj.EIP_Config.CPU_Path,
                PlcType = PlcType.ControlLogix,
                Protocol = Protocol.ab_eip,
                Timeout = TimeSpan.FromSeconds(5)
            };
           
        }
        private  void ReadRAPLC(object abCfg)
        {
            ReadABModel readABModel = (ReadABModel)abCfg;
            bool LockSave = false;
            bool lastStatus = false;

           

            AddLogMsg("开始读取PLC数据!");

            while (true)
            {
                if (readABModel.CfgModel.EIP_Config.Start && !lastStatus)
                {
                    LoadTag(readABModel.CfgModel);
                    BufferNotEmpty_St.Initialize();
                    lastStatus = readABModel.CfgModel.EIP_Config.Start;
                }
                if (!readABModel.CfgModel.EIP_Config.Start)
                {
                    lastStatus = false;
                }

                if (readABModel.CfgModel.EIP_Config.Start)
                {
                    try
                    {
                        
                        BufferNotEmpty_St.Read();
                    }
                    catch (Exception e)
                    {
                        AddLogMsg("通讯超时");
                        Thread.Sleep(2000);
                    }

                    if (BufferNotEmpty_St.GetStatus() == Status.Ok)
                    {
                        try
                        {
                            MsgCount.Read();
                            EventMsg.Read();
                            if (BufferNotEmpty_St.Value && !LockSave)
                            {
                                if (MsgCount.Value <= 0)
                                {
                                    //LogErr
                                    //LogMessage("读取的数据长度数据为0,请检查PLC数据长度设定!", Lfg_Obj.EIP_LogFile.FileFolder);
                                    AddLogMsg( "读取的数据长度数据为0,请检查PLC数据长度设定");
                                    Thread.Sleep(2000);
                                }

                                if (MsgCount.Value > 0 && EventMsg.Value.Length == 0)
                                {
                                    //LogErr
                                    //LogErr
                                    //   LogMessage("读取的数据长度为0!,但计算长度不是0.", Lfg_Obj.EIP_LogFile.FileFolder);
                                    AddLogMsg("取的数据长度为0!,但计算长度不是0");
                                    Thread.Sleep(2000);
                                }

                                string[] arraryFile;
                                arraryFile = EventMsg.Value.Split(':');
                                if (MsgCount.Value > 0 && MsgCount.Value != (arraryFile.Length)/2)
                                {
                                    //LogErr
                                    //LogErr
                                    // LogMessage("读取的数据长度和信息的长度不对应,请检查PLC数据长度设定!", Lfg_Obj.EIP_LogFile.FileFolder);
                                    AddLogMsg("读取的数据长度和信息的长度不对应,请检查PLC数据长度设定!");
                                    Thread.Sleep(2000);
                                }

                                if (MsgCount.Value > 0 && MsgCount.Value == (arraryFile.Length)/2)
                                {
                                    LockSave = true;
                                    List<string> tempListMsg = ParseMSGService.ParseMSG(EventMsg.Value, readABModel.CfgModel.EIP_Config.Section, readABModel.EventDic);
                                    for (int i = 0; i < tempListMsg.Count; i++)
                                    {
                                        AddLogMsg(tempListMsg[i]);
                                    }
                                }
                            }
                            if (LockSave)
                            {
                                BufferNotEmpty_St.Value = false;
                                BufferNotEmpty_St.Write();
                                LockSave = false;
                            }
                        }
                        catch (Exception)
                        {
                            // LogMessage("读取数据失败!", Lfg_Obj.EIP_LogFile.FileFolder);
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
        }

        private static  void UpDataDGV(DataGridModel o)
        {
           
             
        }

        private static void ReadSIEPLC(object SIECfg)
        {
            var s7Client = new S7Client();
            int result = s7Client.ConnectTo("127.0.0.1", 0, 1);
            if (result == 0)
            {

            }
        }
        /// <summary>
        /// 更新回子窗体数据
        /// </summary>
        /// <param name="cfgModel"></param>
        private void UC_EIP_TransfEvent(ConfigModel cfgModel)
        {
            Cfg_Obj.EIP_Config.PLC_Address = cfgModel.EIP_Config.PLC_Address;
            Cfg_Obj.EIP_Config.CPU_Path = cfgModel.EIP_Config.CPU_Path;
            Cfg_Obj.EIP_Config.Start = cfgModel.EIP_Config.Start;
            Cfg_Obj.EIP_Config.AutoStart = cfgModel.EIP_Config.AutoStart;


        }
        private void Close_btn_Click(object sender, EventArgs e)
        {
            CloseForm();
            SaveCfg();
            this.Close();
            this.Dispose();

        }

        private void SaveCfg()
        {
            SaveCfgService saveCfgService = new SaveCfgService();
            saveCfgService.SaveCfg(Cfg_Obj, CfgFilepath);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                Location = mouseSet;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }
        private void GetUCDataBack()
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            CloseForm();

            rsLogixFrm = new RsLogixFrm();
            rsLogixFrm.receive += rsLogixFrm.Updata;
            rsLogixFrm.SetValue(Cfg_Obj.EIP_Config);
            OpenForm(rsLogixFrm);

        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            CloseForm();
            tia1500Frm = new Tia1500Frm();
            tia1500Frm.receive += tia1500Frm.Updata;
            tia1500Frm.SetValue(Cfg_Obj.SIE_Config);
            OpenForm(tia1500Frm);
        }

        private void OpenForm(Form objFrm)
        {
            objFrm.TopLevel = false;
            objFrm.WindowState = FormWindowState.Maximized;
            objFrm.FormBorderStyle= FormBorderStyle.None;
            objFrm.Parent = this.panel2;
            objFrm.Show();
        }
        private void CloseForm()
        {

            if (rsLogixFrm !=null)
            {
                EIP_Model temp=new EIP_Model();
                rsLogixFrm.send += rsLogixFrm.GetDataBack;
                temp = rsLogixFrm.SetValue();
                Cfg_Obj.EIP_Config.PLC_Address = temp.PLC_Address;
                Cfg_Obj.EIP_Config.CPU_Path = temp.CPU_Path;
                rsLogixFrm.Dispose();
                rsLogixFrm = null;
            }

            if (tia1500Frm != null)
            {
                SIE_Model temp = new SIE_Model();
                tia1500Frm.send += tia1500Frm.GetDataBack;
                temp = tia1500Frm.SetValue();
                Cfg_Obj.SIE_Config.PLC_Address = temp.PLC_Address;
                Cfg_Obj.SIE_Config.CPU_Path = temp.CPU_Path;
                tia1500Frm.Dispose();
                tia1500Frm = null;
            }
        }

        protected virtual void OnUpdataCfg(EIP_Model o)
        {
            if (_UpDataCfg != null)
            {
                _UpDataCfg(o);
            }
        }

        public void SetValue(EIP_Model o)
        {
            OnUpdataCfg(o);
        }

        public void UpdataCfg(EIP_Model o)
        {
            Cfg_Obj.EIP_Config.PLC_Address=o.PLC_Address;
            Cfg_Obj.EIP_Config.CPU_Path = o.CPU_Path;
            Cfg_Obj.EIP_Config.Start = o.Start;
        }


    }


   



}

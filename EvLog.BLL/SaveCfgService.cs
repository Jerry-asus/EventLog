using EvLog.DAL;
using EvLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class SaveCfgService
    {
        ConfileFileHelper IniConfigHelper = new ConfileFileHelper();
        public bool SaveCfg(ConfigModel cfg,string cfgFilePath)
        {
            bool Result = true;
            Result &= IniConfigHelper.WriteIniData("ControlLogix", "IPAddress", cfg.EIP_Config.PLC_Address, cfgFilePath);
            Result &= IniConfigHelper.WriteIniData("ControlLogix", "Rock_num", cfg.EIP_Config.CPU_Path, cfgFilePath);
            Result &= IniConfigHelper.WriteIniData("SIE_1500", "IPAddress", cfg.SIE_Config.PLC_Address, cfgFilePath);
            Result &= IniConfigHelper.WriteIniData("SIE_1500", "Rock_num", cfg.SIE_Config.CPU_Path, cfgFilePath);
            if (Result == false)
            {
                return false;
                
            }
            return true;



        }


    }
}

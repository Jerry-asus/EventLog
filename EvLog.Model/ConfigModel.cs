using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public class ConfigModel
    {
        public ConfigModel()
        {
            EIP_Config =new EIP_Model();
            SIE_Config =new SIE_Model();
        }

        public EIP_Model EIP_Config { get; set; }
        public SIE_Model SIE_Config { get; set; }

        
    }
}

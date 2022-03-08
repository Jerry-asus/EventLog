using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public  class SIE_Model
    {
        public string Section { get; set; }
        public string PLC_Address { get; set; }

        public string CPU_Path { get; set; }

        public bool Start { get; set; } = false;

        public string AutoStart { get; set; }

        public string Var_BufferNotEmpty { get; set; }

        public string Var_MsgCount { get; set; }

        public string Var_SendMsg { get; set; }

        public string LogFilePath { get; set; } = null;

        public string EventTxTFilePath { get; set; }
    }
}

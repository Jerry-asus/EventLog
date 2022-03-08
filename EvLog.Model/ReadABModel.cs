using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public class ReadABModel
    {
        public ConfigModel CfgModel { get; set; }

        public Dictionary<int, string> EventDic { get; set; }

    }
}

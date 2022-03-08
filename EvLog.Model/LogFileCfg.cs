using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public class LogFileCfg
    {
        private int _CurrentFileNum;

        public int CurrentFileNum
        {
            get { return _CurrentFileNum; }
            set
            {
                if(value >=100 )
                {
                    _CurrentFileNum = 1;
                }
                else
                {
                    _CurrentFileNum = value;
                }
            }
        }


        public string LogFileFolder { get; set; }


    }
}

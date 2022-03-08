using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public  class UI_Status
    {
        public UI_Status()
        {
            logViewsList = new List<LogView>();
        }

        public List<LogView> logViewsList { get; set; }


    }
}

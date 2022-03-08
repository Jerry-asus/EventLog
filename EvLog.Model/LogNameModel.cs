using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.Model
{
    public class LogNameModel
    {
        /// <summary>
        /// 当前文件编号
        /// </summary>
        public int CurrentFileNum { get; set; }
        /// <summary>
        /// 下一个文件编号
        /// </summary>
        public int NextFileNum { get; set; }
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string FullFilePath { get; set; }


        public string FileFolder { get; set; }
    }
}

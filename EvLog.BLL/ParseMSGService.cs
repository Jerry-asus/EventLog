using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvLog.BLL
{
    public class ParseMSGService
    {
        public static List<string> ParseMSG(string msg, Object section,Dictionary<int,string> msg_Dic)
        {

            string[] arraryFile;
            string NewMsg=msg.Remove(msg.Length-1);
            arraryFile = NewMsg.Split(':');
            List<string> resStr = new List<string>();
            for (int i = 0; i < arraryFile.Length/2; i++)
            {
                string tempMsg = null;
                if (msg_Dic.ContainsKey(Convert.ToInt16(arraryFile[(i*2)])))
                {
                    tempMsg = "事件产生时间:" + arraryFile[2*i+1] + ";信息ID:" + arraryFile[2*i] + ",信息内容:" + msg_Dic[Convert.ToInt16(arraryFile[2*i])];
                    resStr.Add(tempMsg);
                }
                else
                {
                    tempMsg = "事件产生时间:" + arraryFile[2*i+1] + ";信息ID:" + arraryFile[2*i];
                    resStr.Add(tempMsg);
                }
            }
            return resStr;
        }



    }
}

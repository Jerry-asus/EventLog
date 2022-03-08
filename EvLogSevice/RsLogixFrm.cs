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

namespace EvLogSevice
{
    public partial class RsLogixFrm : Form
    {

        public delegate void receiveData(EIP_Model o);
        public event receiveData receive;

        public delegate EIP_Model SendDataback();
        public event SendDataback send;
        public RsLogixFrm()
        {
            InitializeComponent();
        }

        protected virtual void recveDataChange(EIP_Model o)
        {
            if(receive != null)
            {
                receive(o);
            }
        }

        protected virtual EIP_Model recveDataChange()
        {
            EIP_Model eIP_Model = new EIP_Model();
            if (send != null)
            {
                eIP_Model= send();
            }
            return eIP_Model;
        }

        public void SetValue(EIP_Model o)
        {
            recveDataChange(o);
        }

        public EIP_Model SetValue()
        {
          return send();
        }

        public void Updata(EIP_Model o)
        {
            tbx_IPAddress.Text = o.PLC_Address;
            tbx_Path.Text = o.CPU_Path;
            
           if(o.Start)
            {
                btn_Start.Text = "Running";
                btn_Start.BackColor = Color.Green;
            }
           else
            {
                btn_Start.Text = "Stop";
                btn_Start.BackColor = Color.FromArgb(246, 246, 246);
            }
        }

        public EIP_Model GetDataBack()
        {
            EIP_Model temp =new  EIP_Model();
            temp.PLC_Address = tbx_IPAddress.Text;
            temp.CPU_Path = tbx_Path.Text;

            switch (btn_Start.Text)
            {
                case "Running":
                    temp.Start = true;
                    break;
                case "Stop":
                    temp.Start = true;
                    break;

                default: break;
            }
            return temp;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            EIP_Model tempModel=new EIP_Model();
            tempModel.PLC_Address=tbx_IPAddress.Text;
            tempModel.CPU_Path=tbx_Path.Text;
            switch (btn_Start.Text)
            {
                case "Running":
                    btn_Start.Text = "Stop";
                    tempModel.Start = false;
                    btn_Start.BackColor = Color.FromArgb(246, 246, 246);
                    break;
                case "Stop":
                    btn_Start.Text = "Running";
                    tempModel.Start = true;
                    btn_Start.BackColor = Color.Green;
                    break;
                default:break;
            }
            MainFrm frm = new MainFrm();
            frm._UpDataCfg += frm.UpdataCfg;
            frm.SetValue(tempModel);
        }
    }
}

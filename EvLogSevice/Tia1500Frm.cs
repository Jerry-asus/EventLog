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
    public partial class Tia1500Frm : Form
    {
        public delegate void receiveData(SIE_Model o);
        public event receiveData receive;

        public delegate SIE_Model SendDataback();
        public event SendDataback send;
        public Tia1500Frm()
        {
            InitializeComponent();
        }
        protected virtual void recveDataChange(SIE_Model o)
        {
            if (receive != null)
            {
                receive(o);
            }
        }

        protected virtual SIE_Model recveDataChange()
        {
            SIE_Model sIE_Model = new SIE_Model();
            if (send != null)
            {
                sIE_Model = send();
            }
            return sIE_Model;
        }

        public void SetValue(SIE_Model o)
        {
            recveDataChange(o);
        }

        public SIE_Model SetValue()
        {
            return send();
        }

        public void Updata(SIE_Model o)
        {
            tbx_IPAddress.Text = o.PLC_Address;
            tbx_Path.Text = o.CPU_Path;
            if (o.Start)
            {
                btn_Start.Text = "Running";
                btn_Start.ForeColor = Color.Green;
            }
            else
            {
                btn_Start.Text = "Stop";
                btn_Start.ForeColor = Color.FromArgb(246, 246, 246);
            }
        }
        public SIE_Model GetDataBack()
        {
            SIE_Model temp = new SIE_Model();
            temp.PLC_Address = tbx_IPAddress.Text;
            temp.CPU_Path = tbx_Path.Text;
            if (btn_Start.Text =="Running")
            {
                temp.Start = true;
            }
            else
            {
                temp.Start = false;
            }
            return temp;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
           
        }
    }
}

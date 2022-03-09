namespace EvLogSevice
{
    partial class Tia1500Frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbx_Path = new System.Windows.Forms.TextBox();
            this.tbx_IPAddress = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.cbx_AutoStart = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbx_Path
            // 
            this.tbx_Path.Location = new System.Drawing.Point(171, 129);
            this.tbx_Path.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.tbx_Path.Name = "tbx_Path";
            this.tbx_Path.Size = new System.Drawing.Size(261, 27);
            this.tbx_Path.TabIndex = 16;
            // 
            // tbx_IPAddress
            // 
            this.tbx_IPAddress.Location = new System.Drawing.Point(171, 78);
            this.tbx_IPAddress.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.tbx_IPAddress.Name = "tbx_IPAddress";
            this.tbx_IPAddress.Size = new System.Drawing.Size(261, 27);
            this.tbx_IPAddress.TabIndex = 17;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(40, 174);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(392, 49);
            this.btn_Start.TabIndex = 15;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // cbx_AutoStart
            // 
            this.cbx_AutoStart.AutoSize = true;
            this.cbx_AutoStart.Location = new System.Drawing.Point(457, 187);
            this.cbx_AutoStart.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.cbx_AutoStart.Name = "cbx_AutoStart";
            this.cbx_AutoStart.Size = new System.Drawing.Size(107, 25);
            this.cbx_AutoStart.TabIndex = 14;
            this.cbx_AutoStart.Text = "AutoStart";
            this.cbx_AutoStart.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "CPU   Path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tia1500  通讯参数配置:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "IP Address:";
            // 
            // Tia1500Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(598, 269);
            this.Controls.Add(this.tbx_Path);
            this.Controls.Add(this.tbx_IPAddress);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.cbx_AutoStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Tia1500Frm";
            this.Text = "Tia1500Frm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_Path;
        private System.Windows.Forms.TextBox tbx_IPAddress;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.CheckBox cbx_AutoStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
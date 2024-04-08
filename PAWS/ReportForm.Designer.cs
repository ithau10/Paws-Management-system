
namespace PAWS
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.dtToDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtFromDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.btnLoad = new Guna.UI2.WinForms.Guna2Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 13);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.guna2Button1);
            this.panel2.Controls.Add(this.dtToDate);
            this.panel2.Controls.Add(this.lblToDate);
            this.panel2.Controls.Add(this.dtFromDate);
            this.panel2.Controls.Add(this.lblFromDate);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 45);
            this.panel2.TabIndex = 2;
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.Location = new System.Drawing.Point(976, -1);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(21, 33);
            this.guna2Button1.TabIndex = 50;
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // dtToDate
            // 
            this.dtToDate.BackColor = System.Drawing.Color.Transparent;
            this.dtToDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.dtToDate.BorderRadius = 8;
            this.dtToDate.Checked = true;
            this.dtToDate.FillColor = System.Drawing.Color.White;
            this.dtToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtToDate.Location = new System.Drawing.Point(483, 6);
            this.dtToDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtToDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(179, 33);
            this.dtToDate.TabIndex = 14;
            this.dtToDate.Value = new System.DateTime(2024, 3, 15, 0, 0, 0, 0);
            this.dtToDate.ValueChanged += new System.EventHandler(this.dtToDate_ValueChanged);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(417, 12);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(68, 20);
            this.lblToDate.TabIndex = 15;
            this.lblToDate.Text = "To Date:";
            // 
            // dtFromDate
            // 
            this.dtFromDate.BackColor = System.Drawing.Color.Transparent;
            this.dtFromDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.dtFromDate.BorderRadius = 8;
            this.dtFromDate.Checked = true;
            this.dtFromDate.FillColor = System.Drawing.Color.White;
            this.dtFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtFromDate.Location = new System.Drawing.Point(106, 6);
            this.dtFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(179, 33);
            this.dtFromDate.TabIndex = 3;
            this.dtFromDate.Value = new System.DateTime(2024, 3, 15, 0, 0, 0, 0);
            this.dtFromDate.ValueChanged += new System.EventHandler(this.dtFromDate_ValueChanged);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(12, 12);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(88, 20);
            this.lblFromDate.TabIndex = 3;
            this.lblFromDate.Text = "From Date:";
            // 
            // btnLoad
            // 
            this.btnLoad.AutoRoundedCorners = true;
            this.btnLoad.BorderRadius = 15;
            this.btnLoad.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoad.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoad.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoad.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoad.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(796, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(69, 33);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "&Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PAWS.PaymentReport.rdlc";
            this.reportViewer.LocalReport.ReportPath = "C:\\Users\\ithau.DESKTOP-1P0GCC5\\Desktop\\DENNIS MUTUNGI\\PAWS\\PAWS\\PaymentReport.rdl" +
    "c";
            this.reportViewer.Location = new System.Drawing.Point(0, 64);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(997, 566);
            this.reportViewer.TabIndex = 3;
            this.reportViewer.Load += new System.EventHandler(this.reportViewer_Load);
            // 
            // ReportForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1000, 619);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btnLoad;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtToDate;
        private System.Windows.Forms.Label lblToDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}
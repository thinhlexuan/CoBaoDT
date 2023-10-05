namespace CBClient.NhienLieu
{
    partial class BCTonKhoForm
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
            this.ExpTraTim = new DevComponents.DotNetBar.ExpandablePanel();
            this.cboTramNL = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.sdNgayKT = new CBClient.Controls.SmartDate();
            this.sdNgayBD = new CBClient.Controls.SmartDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboLoaiDM = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.cboLoaiBC = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDonVi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ExpTraTim.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.AutoScroll = true;
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.cboTramNL);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.sdNgayKT);
            this.ExpTraTim.Controls.Add(this.sdNgayBD);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.label6);
            this.ExpTraTim.Controls.Add(this.cboLoaiDM);
            this.ExpTraTim.Controls.Add(this.label9);
            this.ExpTraTim.Controls.Add(this.btnTraTim);
            this.ExpTraTim.Controls.Add(this.cboLoaiBC);
            this.ExpTraTim.Controls.Add(this.label8);
            this.ExpTraTim.Controls.Add(this.cboDonVi);
            this.ExpTraTim.Controls.Add(this.lblTableCount);
            this.ExpTraTim.Controls.Add(this.label4);
            this.ExpTraTim.DisabledBackColor = System.Drawing.Color.Empty;
            this.ExpTraTim.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExpTraTim.HideControlsWhenCollapsed = true;
            this.ExpTraTim.Location = new System.Drawing.Point(0, 0);
            this.ExpTraTim.Name = "ExpTraTim";
            this.ExpTraTim.Size = new System.Drawing.Size(188, 621);
            this.ExpTraTim.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.ExpTraTim.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.ExpTraTim.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.ExpTraTim.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ExpTraTim.Style.GradientAngle = 90;
            this.ExpTraTim.TabIndex = 0;
            this.ExpTraTim.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.ExpTraTim.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.ExpTraTim.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.ExpTraTim.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ExpTraTim.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ExpTraTim.TitleStyle.ForeColor.Color = System.Drawing.Color.Maroon;
            this.ExpTraTim.TitleStyle.GradientAngle = 90;
            this.ExpTraTim.TitleText = "Thông tin tra tìm";
            // 
            // cboTramNL
            // 
            this.cboTramNL.DisplayMember = "Text";
            this.cboTramNL.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTramNL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTramNL.ForeColor = System.Drawing.Color.Black;
            this.cboTramNL.FormattingEnabled = true;
            this.cboTramNL.ItemHeight = 16;
            this.cboTramNL.Location = new System.Drawing.Point(6, 235);
            this.cboTramNL.Name = "cboTramNL";
            this.cboTramNL.Size = new System.Drawing.Size(176, 22);
            this.cboTramNL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTramNL.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 131;
            this.label2.Text = "Trạm nhiên liệu";
            // 
            // sdNgayKT
            // 
            this.sdNgayKT.BackColor = System.Drawing.Color.White;
            this.sdNgayKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayKT.ForeColor = System.Drawing.Color.Black;
            this.sdNgayKT.IsTime = false;
            this.sdNgayKT.Location = new System.Drawing.Point(80, 146);
            this.sdNgayKT.Name = "sdNgayKT";
            this.sdNgayKT.Size = new System.Drawing.Size(102, 23);
            this.sdNgayKT.TabIndex = 3;
            this.sdNgayKT.Text = "10.04.2023";
            this.sdNgayKT.Value = new System.DateTime(2023, 4, 10, 0, 0, 0, 0);
            this.sdNgayKT.Validated += new System.EventHandler(this.sdNgayKT_Validated);
            // 
            // sdNgayBD
            // 
            this.sdNgayBD.BackColor = System.Drawing.Color.White;
            this.sdNgayBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayBD.ForeColor = System.Drawing.Color.Black;
            this.sdNgayBD.IsTime = false;
            this.sdNgayBD.Location = new System.Drawing.Point(80, 119);
            this.sdNgayBD.Name = "sdNgayBD";
            this.sdNgayBD.Size = new System.Drawing.Size(102, 23);
            this.sdNgayBD.TabIndex = 2;
            this.sdNgayBD.Text = "10.04.2023";
            this.sdNgayBD.Value = new System.DateTime(2023, 4, 10, 0, 0, 0, 0);
            this.sdNgayBD.Validated += new System.EventHandler(this.sdNgayBD_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 128;
            this.label1.Text = "Từ ngày";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 129;
            this.label6.Text = "Đến ngày";
            // 
            // cboLoaiDM
            // 
            this.cboLoaiDM.DisplayMember = "Text";
            this.cboLoaiDM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiDM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiDM.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiDM.FormattingEnabled = true;
            this.cboLoaiDM.ItemHeight = 16;
            this.cboLoaiDM.Location = new System.Drawing.Point(6, 191);
            this.cboLoaiDM.Name = "cboLoaiDM";
            this.cboLoaiDM.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiDM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiDM.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 113;
            this.label9.Text = "Loại dầu mỡ";
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(6, 263);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(176, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 6;
            this.btnTraTim.Text = "&Tra Tìm";
            this.btnTraTim.Click += new System.EventHandler(this.btnTraTim_Click);
            // 
            // cboLoaiBC
            // 
            this.cboLoaiBC.DisplayMember = "Text";
            this.cboLoaiBC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiBC.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiBC.FormattingEnabled = true;
            this.cboLoaiBC.ItemHeight = 16;
            this.cboLoaiBC.Location = new System.Drawing.Point(6, 91);
            this.cboLoaiBC.Name = "cboLoaiBC";
            this.cboLoaiBC.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiBC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiBC.TabIndex = 1;
            this.cboLoaiBC.SelectedIndexChanged += new System.EventHandler(this.cboLoaiBC_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 83;
            this.label8.Text = "Loại báo cáo";
            // 
            // cboDonVi
            // 
            this.cboDonVi.DisplayMember = "Text";
            this.cboDonVi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonVi.ForeColor = System.Drawing.Color.Black;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.ItemHeight = 16;
            this.cboDonVi.Location = new System.Drawing.Point(6, 47);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(176, 22);
            this.cboDonVi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDonVi.TabIndex = 0;
            // 
            // lblTableCount
            // 
            this.lblTableCount.AutoSize = true;
            this.lblTableCount.BackColor = System.Drawing.Color.White;
            this.lblTableCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTableCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableCount.ForeColor = System.Drawing.Color.Black;
            this.lblTableCount.Location = new System.Drawing.Point(0, 605);
            this.lblTableCount.Name = "lblTableCount";
            this.lblTableCount.Size = new System.Drawing.Size(136, 16);
            this.lblTableCount.TabIndex = 75;
            this.lblTableCount.Text = "Tổng số bản ghi: 0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Xí nghiệp đầu máy";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.ForeColor = System.Drawing.Color.Black;
            this.reportViewer1.Location = new System.Drawing.Point(188, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(820, 621);
            this.reportViewer1.TabIndex = 1;
            // 
            // BCTonKhoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "BCTonKhoForm";
            this.Text = "BIỂU BÁO CÁO TỒN KHO";
            this.Load += new System.EventHandler(this.BCTonKhoForm_Load);
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDonVi;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiBC;
        private System.Windows.Forms.Label label8;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiDM;
        private System.Windows.Forms.Label label9;
        private Controls.SmartDate sdNgayKT;
        private Controls.SmartDate sdNgayBD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTramNL;
        private System.Windows.Forms.Label label2;
    }
}
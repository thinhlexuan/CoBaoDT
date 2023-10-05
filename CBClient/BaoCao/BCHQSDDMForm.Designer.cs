namespace CBClient.BaoCao
{
    partial class BCHQSDDMForm
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
            this.sdNgayKT = new CBClient.Controls.SmartDate();
            this.sdNgayBD = new CBClient.Controls.SmartDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboNguondl = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.cboLoaiBC = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDonVi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboNhomBC = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.ExpTraTim.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.AutoScroll = true;
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.cboNhomBC);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.sdNgayKT);
            this.ExpTraTim.Controls.Add(this.sdNgayBD);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.label6);
            this.ExpTraTim.Controls.Add(this.cboNguondl);
            this.ExpTraTim.Controls.Add(this.label9);
            this.ExpTraTim.Controls.Add(this.cboLoaiBC);
            this.ExpTraTim.Controls.Add(this.label8);
            this.ExpTraTim.Controls.Add(this.cboDonVi);
            this.ExpTraTim.Controls.Add(this.btnTraTim);
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
            // sdNgayKT
            // 
            this.sdNgayKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayKT.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayKT.IsTime = false;
            this.sdNgayKT.Location = new System.Drawing.Point(80, 195);
            this.sdNgayKT.Name = "sdNgayKT";
            this.sdNgayKT.Size = new System.Drawing.Size(102, 23);
            this.sdNgayKT.TabIndex = 4;
            this.sdNgayKT.Text = "23.02.2023";
            this.sdNgayKT.Value = new System.DateTime(2023, 2, 23, 0, 0, 0, 0);
            this.sdNgayKT.Validated += new System.EventHandler(this.sdNgayKT_Validated);
            // 
            // sdNgayBD
            // 
            this.sdNgayBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayBD.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayBD.IsTime = false;
            this.sdNgayBD.Location = new System.Drawing.Point(80, 168);
            this.sdNgayBD.Name = "sdNgayBD";
            this.sdNgayBD.Size = new System.Drawing.Size(102, 23);
            this.sdNgayBD.TabIndex = 3;
            this.sdNgayBD.Text = "23.02.2023";
            this.sdNgayBD.Value = new System.DateTime(2023, 2, 23, 0, 0, 0, 0);
            this.sdNgayBD.Validated += new System.EventHandler(this.sdNgayBD_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 122;
            this.label1.Text = "Từ ngày";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 123;
            this.label6.Text = "Đến ngày";
            // 
            // cboNguondl
            // 
            this.cboNguondl.DisplayMember = "Text";
            this.cboNguondl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNguondl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNguondl.ForeColor = System.Drawing.Color.Black;
            this.cboNguondl.FormattingEnabled = true;
            this.cboNguondl.ItemHeight = 16;
            this.cboNguondl.Location = new System.Drawing.Point(6, 240);
            this.cboNguondl.Name = "cboNguondl";
            this.cboNguondl.Size = new System.Drawing.Size(176, 22);
            this.cboNguondl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNguondl.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 16);
            this.label9.TabIndex = 119;
            this.label9.Text = "Nguồn dữ liệu";
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
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(6, 268);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(176, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 6;
            this.btnTraTim.Text = "&Tra Tìm";
            this.btnTraTim.Click += new System.EventHandler(this.btnTraTim_Click);
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
            this.reportViewer1.Location = new System.Drawing.Point(188, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(820, 621);
            this.reportViewer1.TabIndex = 5;
            // 
            // cboNhomBC
            // 
            this.cboNhomBC.DisplayMember = "Text";
            this.cboNhomBC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhomBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhomBC.ForeColor = System.Drawing.Color.Black;
            this.cboNhomBC.FormattingEnabled = true;
            this.cboNhomBC.ItemHeight = 16;
            this.cboNhomBC.Location = new System.Drawing.Point(6, 135);
            this.cboNhomBC.Name = "cboNhomBC";
            this.cboNhomBC.Size = new System.Drawing.Size(176, 22);
            this.cboNhomBC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNhomBC.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 125;
            this.label2.Text = "Nhóm báo cáo";
            // 
            // BCHQSDDMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "BCHQSDDMForm";
            this.Text = "BIỂU HIỆU QUẢ SỬ DỤNG ĐẦU MÁY";
            this.Load += new System.EventHandler(this.BCHQSDDMForm_Load);
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDonVi;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiBC;
        private System.Windows.Forms.Label label8;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNguondl;
        private System.Windows.Forms.Label label9;
        private Controls.SmartDate sdNgayKT;
        private Controls.SmartDate sdNgayBD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNhomBC;
        private System.Windows.Forms.Label label2;
    }
}
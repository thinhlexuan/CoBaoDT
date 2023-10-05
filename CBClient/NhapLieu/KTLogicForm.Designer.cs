namespace CBClient.NhapLieu
{
    partial class KTLogicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KTLogicForm));
            this.ExpTraTim = new DevComponents.DotNetBar.ExpandablePanel();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.txtSHDauMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLoaiLG = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtThongTin = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.cboDonVi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboNamDT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboThangDT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.btnExport);
            this.ExpTraTim.Controls.Add(this.txtSHDauMay);
            this.ExpTraTim.Controls.Add(this.label3);
            this.ExpTraTim.Controls.Add(this.cboLoaiLG);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.rtxtThongTin);
            this.ExpTraTim.Controls.Add(this.cboDonVi);
            this.ExpTraTim.Controls.Add(this.cboNamDT);
            this.ExpTraTim.Controls.Add(this.cboThangDT);
            this.ExpTraTim.Controls.Add(this.btnTraTim);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.lblTableCount);
            this.ExpTraTim.Controls.Add(this.label6);
            this.ExpTraTim.Controls.Add(this.label4);
            this.ExpTraTim.DisabledBackColor = System.Drawing.Color.Empty;
            this.ExpTraTim.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExpTraTim.HideControlsWhenCollapsed = true;
            this.ExpTraTim.Location = new System.Drawing.Point(0, 0);
            this.ExpTraTim.Name = "ExpTraTim";
            this.ExpTraTim.Size = new System.Drawing.Size(253, 621);
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
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.Location = new System.Drawing.Point(12, 233);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(235, 28);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.Symbol = "";
            this.btnExport.SymbolColor = System.Drawing.Color.Blue;
            this.btnExport.TabIndex = 114;
            this.btnExport.Text = "&Xuất Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtSHDauMay
            // 
            this.txtSHDauMay.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSHDauMay.Border.Class = "TextBoxBorder";
            this.txtSHDauMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSHDauMay.DisabledBackColor = System.Drawing.Color.White;
            this.txtSHDauMay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSHDauMay.ForeColor = System.Drawing.Color.Black;
            this.txtSHDauMay.Location = new System.Drawing.Point(107, 170);
            this.txtSHDauMay.Name = "txtSHDauMay";
            this.txtSHDauMay.PreventEnterBeep = true;
            this.txtSHDauMay.Size = new System.Drawing.Size(141, 23);
            this.txtSHDauMay.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 103;
            this.label3.Text = "SH đầu máy";
            // 
            // cboLoaiLG
            // 
            this.cboLoaiLG.DisplayMember = "Text";
            this.cboLoaiLG.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiLG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiLG.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiLG.FormattingEnabled = true;
            this.cboLoaiLG.ItemHeight = 16;
            this.cboLoaiLG.Location = new System.Drawing.Point(73, 138);
            this.cboLoaiLG.Name = "cboLoaiLG";
            this.cboLoaiLG.Size = new System.Drawing.Size(175, 22);
            this.cboLoaiLG.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiLG.TabIndex = 4;
            this.cboLoaiLG.SelectedIndexChanged += new System.EventHandler(this.cboLoaiLG_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 77;
            this.label2.Text = "Loại logic";
            // 
            // rtxtThongTin
            // 
            this.rtxtThongTin.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.rtxtThongTin.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtxtThongTin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtxtThongTin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtThongTin.ForeColor = System.Drawing.Color.Black;
            this.rtxtThongTin.Location = new System.Drawing.Point(0, 332);
            this.rtxtThongTin.Name = "rtxtThongTin";
            this.rtxtThongTin.ReadOnly = true;
            this.rtxtThongTin.Rtf = resources.GetString("rtxtThongTin.Rtf");
            this.rtxtThongTin.Size = new System.Drawing.Size(253, 273);
            this.rtxtThongTin.TabIndex = 76;
            this.rtxtThongTin.Text = "Thông tin kiểm tra";
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
            this.cboDonVi.Size = new System.Drawing.Size(242, 22);
            this.cboDonVi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDonVi.TabIndex = 1;
            // 
            // cboNamDT
            // 
            this.cboNamDT.DisplayMember = "Text";
            this.cboNamDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNamDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNamDT.ForeColor = System.Drawing.Color.Black;
            this.cboNamDT.FormattingEnabled = true;
            this.cboNamDT.ItemHeight = 16;
            this.cboNamDT.Location = new System.Drawing.Point(134, 105);
            this.cboNamDT.Name = "cboNamDT";
            this.cboNamDT.Size = new System.Drawing.Size(114, 22);
            this.cboNamDT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNamDT.TabIndex = 3;
            // 
            // cboThangDT
            // 
            this.cboThangDT.DisplayMember = "Text";
            this.cboThangDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboThangDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThangDT.ForeColor = System.Drawing.Color.Black;
            this.cboThangDT.FormattingEnabled = true;
            this.cboThangDT.ItemHeight = 16;
            this.cboThangDT.Location = new System.Drawing.Point(134, 75);
            this.cboThangDT.Name = "cboThangDT";
            this.cboThangDT.Size = new System.Drawing.Size(114, 22);
            this.cboThangDT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboThangDT.TabIndex = 2;
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(12, 199);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(236, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 6;
            this.btnTraTim.Text = "&Tra Tìm";
            this.btnTraTim.Click += new System.EventHandler(this.btnTraTim_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 71;
            this.label1.Text = "Tháng đoạn thống";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 72;
            this.label6.Text = "Năm đoạn thống";
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(253, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(755, 621);
            this.dataGridView1.TabIndex = 1;
            // 
            // KTLogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "KTLogicForm";
            this.Text = "KIỂM TRA LOGIC ĐT";
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboThangDT;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNamDT;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDonVi;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtThongTin;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiLG;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSHDauMay;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX btnExport;
    }
}
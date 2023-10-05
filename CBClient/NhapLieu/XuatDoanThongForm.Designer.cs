namespace CBClient.NhapLieu
{
    partial class XuatDoanThongForm
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
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.lblBanGhi = new DevComponents.DotNetBar.LabelX();
            this.cboDonVi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.cboNamDT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboThangDT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.btnExport);
            this.ExpTraTim.Controls.Add(this.lblBanGhi);
            this.ExpTraTim.Controls.Add(this.cboDonVi);
            this.ExpTraTim.Controls.Add(this.label7);
            this.ExpTraTim.Controls.Add(this.btnTraTim);
            this.ExpTraTim.Controls.Add(this.cboNamDT);
            this.ExpTraTim.Controls.Add(this.cboThangDT);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.label6);
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
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.Location = new System.Drawing.Point(1, 160);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(182, 25);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.Symbol = "";
            this.btnExport.SymbolColor = System.Drawing.Color.Blue;
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "&Xuất Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblBanGhi
            // 
            // 
            // 
            // 
            this.lblBanGhi.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBanGhi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBanGhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanGhi.ForeColor = System.Drawing.Color.Blue;
            this.lblBanGhi.Location = new System.Drawing.Point(0, 597);
            this.lblBanGhi.Name = "lblBanGhi";
            this.lblBanGhi.Size = new System.Drawing.Size(188, 24);
            this.lblBanGhi.TabIndex = 0;
            this.lblBanGhi.Text = "Tổng số bản ghi: 0";
            // 
            // cboDonVi
            // 
            this.cboDonVi.DisplayMember = "Text";
            this.cboDonVi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonVi.ForeColor = System.Drawing.Color.Black;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.ItemHeight = 16;
            this.cboDonVi.Location = new System.Drawing.Point(2, 101);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(182, 22);
            this.cboDonVi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDonVi.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(3, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 16);
            this.label7.TabIndex = 111;
            this.label7.Text = "Xí nghiệp đầu máy";
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(2, 129);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(181, 25);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.SymbolColor = System.Drawing.Color.Teal;
            this.btnTraTim.TabIndex = 3;
            this.btnTraTim.Text = "Tr&a Tìm";
            this.btnTraTim.Click += new System.EventHandler(this.btnTraTim_Click);
            // 
            // cboNamDT
            // 
            this.cboNamDT.DisplayMember = "Text";
            this.cboNamDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNamDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNamDT.ForeColor = System.Drawing.Color.Black;
            this.cboNamDT.FormattingEnabled = true;
            this.cboNamDT.ItemHeight = 16;
            this.cboNamDT.Location = new System.Drawing.Point(73, 57);
            this.cboNamDT.Name = "cboNamDT";
            this.cboNamDT.Size = new System.Drawing.Size(109, 22);
            this.cboNamDT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNamDT.TabIndex = 1;
            // 
            // cboThangDT
            // 
            this.cboThangDT.DisplayMember = "Text";
            this.cboThangDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboThangDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThangDT.ForeColor = System.Drawing.Color.Black;
            this.cboThangDT.FormattingEnabled = true;
            this.cboThangDT.ItemHeight = 16;
            this.cboThangDT.Location = new System.Drawing.Point(73, 29);
            this.cboThangDT.Name = "cboThangDT";
            this.cboThangDT.Size = new System.Drawing.Size(109, 22);
            this.cboThangDT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboThangDT.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 71;
            this.label1.Text = "Tháng ĐT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 16);
            this.label6.TabIndex = 72;
            this.label6.Text = "Năm ĐT";
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.Location = new System.Drawing.Point(188, 0);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.RowHeadersVisible = false;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(820, 621);
            this.dgView.TabIndex = 0;
            // 
            // XuatDoanThongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "XuatDoanThongForm";
            this.Text = "XUẤT ĐOẠN THỐNG ĐT";
            this.Load += new System.EventHandler(this.XuatDoanThongForm_Load);
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboThangDT;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNamDT;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.DataGridView dgView;
        private DevComponents.DotNetBar.LabelX lblBanGhi;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDonVi;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.ButtonX btnExport;
    }
}
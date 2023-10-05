namespace CBClient.HaNoi
{
    partial class HNPhieuThuongForm
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
            this.components = new System.ComponentModel.Container();
            this.ExpTraTim = new DevComponents.DotNetBar.ExpandablePanel();
            this.txtMacTauTT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.sdNgayTT = new CBClient.Controls.SmartDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboLoaiPhieuTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiPhieuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.macTauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gaNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGiaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsLoaiPhieu = new System.Windows.Forms.BindingSource(this.components);
            this.tblTraTim = new System.Windows.Forms.TableLayoutPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtGaName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtGaID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMacTau = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tblButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnThem = new DevComponents.DotNetBar.ButtonX();
            this.btnSua = new DevComponents.DotNetBar.ButtonX();
            this.btnXoa = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnLuu = new DevComponents.DotNetBar.ButtonX();
            this.btnHuy = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboLoaiPhieu = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.txtDonGia = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDonVi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.sdNgayHL = new CBClient.Controls.SmartDate();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLoaiPhieu)).BeginInit();
            this.tblTraTim.SuspendLayout();
            this.tblButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.AutoScroll = true;
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.txtMacTauTT);
            this.ExpTraTim.Controls.Add(this.sdNgayTT);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.label8);
            this.ExpTraTim.Controls.Add(this.cboLoaiPhieuTT);
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
            // txtMacTauTT
            // 
            this.txtMacTauTT.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtMacTauTT.Border.Class = "TextBoxBorder";
            this.txtMacTauTT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMacTauTT.DisabledBackColor = System.Drawing.Color.White;
            this.txtMacTauTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtMacTauTT.ForeColor = System.Drawing.Color.Black;
            this.txtMacTauTT.Location = new System.Drawing.Point(3, 128);
            this.txtMacTauTT.Name = "txtMacTauTT";
            this.txtMacTauTT.PreventEnterBeep = true;
            this.txtMacTauTT.Size = new System.Drawing.Size(176, 22);
            this.txtMacTauTT.TabIndex = 2;
            // 
            // sdNgayTT
            // 
            this.sdNgayTT.BackColor = System.Drawing.Color.White;
            this.sdNgayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayTT.ForeColor = System.Drawing.Color.Black;
            this.sdNgayTT.IsTime = false;
            this.sdNgayTT.Location = new System.Drawing.Point(74, 31);
            this.sdNgayTT.Name = "sdNgayTT";
            this.sdNgayTT.Size = new System.Drawing.Size(105, 23);
            this.sdNgayTT.TabIndex = 0;
            this.sdNgayTT.Text = "13.12.2022";
            this.sdNgayTT.Value = new System.DateTime(2022, 12, 13, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 77;
            this.label1.Text = "Ngày HL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Mác tầu";
            // 
            // cboLoaiPhieuTT
            // 
            this.cboLoaiPhieuTT.DisplayMember = "Text";
            this.cboLoaiPhieuTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiPhieuTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiPhieuTT.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiPhieuTT.FormattingEnabled = true;
            this.cboLoaiPhieuTT.ItemHeight = 16;
            this.cboLoaiPhieuTT.Location = new System.Drawing.Point(3, 79);
            this.cboLoaiPhieuTT.Name = "cboLoaiPhieuTT";
            this.cboLoaiPhieuTT.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiPhieuTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiPhieuTT.TabIndex = 1;
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(3, 161);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(176, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 3;
            this.btnTraTim.Text = "T&ra Tìm";
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
            this.label4.Location = new System.Drawing.Point(0, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Loại phiếu";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.loaiPhieuDataGridViewTextBoxColumn,
            this.macTauDataGridViewTextBoxColumn,
            this.gaIDDataGridViewTextBoxColumn,
            this.gaNameDataGridViewTextBoxColumn,
            this.donGiaDataGridViewTextBoxColumn,
            this.donViDataGridViewTextBoxColumn,
            this.ngayHLDataGridViewTextBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.createdNameDataGridViewTextBoxColumn,
            this.modifyDateDataGridViewTextBoxColumn,
            this.modifyByDataGridViewTextBoxColumn,
            this.modifyNameDataGridViewTextBoxColumn});
            this.tblTraTim.SetColumnSpan(this.dataGridView1, 7);
            this.dataGridView1.DataSource = this.bsLoaiPhieu;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 478);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 43;
            // 
            // loaiPhieuDataGridViewTextBoxColumn
            // 
            this.loaiPhieuDataGridViewTextBoxColumn.DataPropertyName = "LoaiPhieu";
            this.loaiPhieuDataGridViewTextBoxColumn.HeaderText = "LoaiPhieu";
            this.loaiPhieuDataGridViewTextBoxColumn.Name = "loaiPhieuDataGridViewTextBoxColumn";
            this.loaiPhieuDataGridViewTextBoxColumn.ReadOnly = true;
            this.loaiPhieuDataGridViewTextBoxColumn.Width = 79;
            // 
            // macTauDataGridViewTextBoxColumn
            // 
            this.macTauDataGridViewTextBoxColumn.DataPropertyName = "MacTau";
            this.macTauDataGridViewTextBoxColumn.HeaderText = "MacTau";
            this.macTauDataGridViewTextBoxColumn.Name = "macTauDataGridViewTextBoxColumn";
            this.macTauDataGridViewTextBoxColumn.ReadOnly = true;
            this.macTauDataGridViewTextBoxColumn.Width = 72;
            // 
            // gaIDDataGridViewTextBoxColumn
            // 
            this.gaIDDataGridViewTextBoxColumn.DataPropertyName = "GaID";
            this.gaIDDataGridViewTextBoxColumn.HeaderText = "GaID";
            this.gaIDDataGridViewTextBoxColumn.Name = "gaIDDataGridViewTextBoxColumn";
            this.gaIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.gaIDDataGridViewTextBoxColumn.Width = 57;
            // 
            // gaNameDataGridViewTextBoxColumn
            // 
            this.gaNameDataGridViewTextBoxColumn.DataPropertyName = "GaName";
            this.gaNameDataGridViewTextBoxColumn.HeaderText = "GaName";
            this.gaNameDataGridViewTextBoxColumn.Name = "gaNameDataGridViewTextBoxColumn";
            this.gaNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.gaNameDataGridViewTextBoxColumn.Width = 74;
            // 
            // donGiaDataGridViewTextBoxColumn
            // 
            this.donGiaDataGridViewTextBoxColumn.DataPropertyName = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.HeaderText = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.Name = "donGiaDataGridViewTextBoxColumn";
            this.donGiaDataGridViewTextBoxColumn.ReadOnly = true;
            this.donGiaDataGridViewTextBoxColumn.Width = 68;
            // 
            // donViDataGridViewTextBoxColumn
            // 
            this.donViDataGridViewTextBoxColumn.DataPropertyName = "DonVi";
            this.donViDataGridViewTextBoxColumn.HeaderText = "DonVi";
            this.donViDataGridViewTextBoxColumn.Name = "donViDataGridViewTextBoxColumn";
            this.donViDataGridViewTextBoxColumn.ReadOnly = true;
            this.donViDataGridViewTextBoxColumn.Width = 61;
            // 
            // ngayHLDataGridViewTextBoxColumn
            // 
            this.ngayHLDataGridViewTextBoxColumn.DataPropertyName = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.HeaderText = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.Name = "ngayHLDataGridViewTextBoxColumn";
            this.ngayHLDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayHLDataGridViewTextBoxColumn.Width = 71;
            // 
            // createdDateDataGridViewTextBoxColumn
            // 
            this.createdDateDataGridViewTextBoxColumn.DataPropertyName = "CreatedDate";
            this.createdDateDataGridViewTextBoxColumn.HeaderText = "CreatedDate";
            this.createdDateDataGridViewTextBoxColumn.Name = "createdDateDataGridViewTextBoxColumn";
            this.createdDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDateDataGridViewTextBoxColumn.Width = 92;
            // 
            // createdByDataGridViewTextBoxColumn
            // 
            this.createdByDataGridViewTextBoxColumn.DataPropertyName = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.HeaderText = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.Name = "createdByDataGridViewTextBoxColumn";
            this.createdByDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdByDataGridViewTextBoxColumn.Width = 81;
            // 
            // createdNameDataGridViewTextBoxColumn
            // 
            this.createdNameDataGridViewTextBoxColumn.DataPropertyName = "CreatedName";
            this.createdNameDataGridViewTextBoxColumn.HeaderText = "CreatedName";
            this.createdNameDataGridViewTextBoxColumn.Name = "createdNameDataGridViewTextBoxColumn";
            this.createdNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdNameDataGridViewTextBoxColumn.Width = 97;
            // 
            // modifyDateDataGridViewTextBoxColumn
            // 
            this.modifyDateDataGridViewTextBoxColumn.DataPropertyName = "ModifyDate";
            this.modifyDateDataGridViewTextBoxColumn.HeaderText = "ModifyDate";
            this.modifyDateDataGridViewTextBoxColumn.Name = "modifyDateDataGridViewTextBoxColumn";
            this.modifyDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifyDateDataGridViewTextBoxColumn.Width = 86;
            // 
            // modifyByDataGridViewTextBoxColumn
            // 
            this.modifyByDataGridViewTextBoxColumn.DataPropertyName = "ModifyBy";
            this.modifyByDataGridViewTextBoxColumn.HeaderText = "ModifyBy";
            this.modifyByDataGridViewTextBoxColumn.Name = "modifyByDataGridViewTextBoxColumn";
            this.modifyByDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifyByDataGridViewTextBoxColumn.Width = 75;
            // 
            // modifyNameDataGridViewTextBoxColumn
            // 
            this.modifyNameDataGridViewTextBoxColumn.DataPropertyName = "ModifyName";
            this.modifyNameDataGridViewTextBoxColumn.HeaderText = "ModifyName";
            this.modifyNameDataGridViewTextBoxColumn.Name = "modifyNameDataGridViewTextBoxColumn";
            this.modifyNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifyNameDataGridViewTextBoxColumn.Width = 91;
            // 
            // bsLoaiPhieu
            // 
            this.bsLoaiPhieu.DataSource = typeof(CBClient.BLLTypes.HNPhieuThuong);
            // 
            // tblTraTim
            // 
            this.tblTraTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblTraTim.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTraTim.ColumnCount = 7;
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tblTraTim.Controls.Add(this.labelX4, 2, 1);
            this.tblTraTim.Controls.Add(this.txtGaName, 3, 2);
            this.tblTraTim.Controls.Add(this.txtGaID, 2, 2);
            this.tblTraTim.Controls.Add(this.txtMacTau, 1, 3);
            this.tblTraTim.Controls.Add(this.labelX2, 0, 3);
            this.tblTraTim.Controls.Add(this.labelX1, 0, 1);
            this.tblTraTim.Controls.Add(this.dataGridView1, 0, 0);
            this.tblTraTim.Controls.Add(this.txtID, 0, 2);
            this.tblTraTim.Controls.Add(this.tblButton, 0, 4);
            this.tblTraTim.Controls.Add(this.labelX6, 1, 1);
            this.tblTraTim.Controls.Add(this.cboLoaiPhieu, 1, 2);
            this.tblTraTim.Controls.Add(this.labelX3, 3, 1);
            this.tblTraTim.Controls.Add(this.labelX14, 4, 1);
            this.tblTraTim.Controls.Add(this.labelX13, 5, 1);
            this.tblTraTim.Controls.Add(this.txtDonGia, 4, 2);
            this.tblTraTim.Controls.Add(this.txtDonVi, 5, 2);
            this.tblTraTim.Controls.Add(this.labelX10, 6, 1);
            this.tblTraTim.Controls.Add(this.sdNgayHL, 6, 2);
            this.tblTraTim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTraTim.ForeColor = System.Drawing.Color.Black;
            this.tblTraTim.Location = new System.Drawing.Point(188, 0);
            this.tblTraTim.Name = "tblTraTim";
            this.tblTraTim.RowCount = 5;
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.Size = new System.Drawing.Size(820, 621);
            this.tblTraTim.TabIndex = 1;
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX4.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.Black;
            this.labelX4.Location = new System.Drawing.Point(243, 490);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(94, 23);
            this.labelX4.TabIndex = 36;
            this.labelX4.Text = "Ga ID";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtGaName
            // 
            this.txtGaName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGaName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGaName.Border.Class = "TextBoxBorder";
            this.txtGaName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGaName.DisabledBackColor = System.Drawing.Color.White;
            this.txtGaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGaName.ForeColor = System.Drawing.Color.Black;
            this.txtGaName.Location = new System.Drawing.Point(344, 523);
            this.txtGaName.Name = "txtGaName";
            this.txtGaName.PreventEnterBeep = true;
            this.txtGaName.Size = new System.Drawing.Size(161, 23);
            this.txtGaName.TabIndex = 1;
            this.txtGaName.Validated += new System.EventHandler(this.txtGaName_Validated);
            // 
            // txtGaID
            // 
            this.txtGaID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGaID.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGaID.Border.Class = "TextBoxBorder";
            this.txtGaID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGaID.DisabledBackColor = System.Drawing.Color.White;
            this.txtGaID.Enabled = false;
            this.txtGaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGaID.ForeColor = System.Drawing.Color.Black;
            this.txtGaID.Location = new System.Drawing.Point(243, 523);
            this.txtGaID.Name = "txtGaID";
            this.txtGaID.PreventEnterBeep = true;
            this.txtGaID.Size = new System.Drawing.Size(94, 23);
            this.txtGaID.TabIndex = 7;
            // 
            // txtMacTau
            // 
            this.txtMacTau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMacTau.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtMacTau.Border.Class = "TextBoxBorder";
            this.txtMacTau.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.txtMacTau, 6);
            this.txtMacTau.DisabledBackColor = System.Drawing.Color.White;
            this.txtMacTau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMacTau.ForeColor = System.Drawing.Color.Black;
            this.txtMacTau.Location = new System.Drawing.Point(75, 556);
            this.txtMacTau.Name = "txtMacTau";
            this.txtMacTau.PreventEnterBeep = true;
            this.txtMacTau.Size = new System.Drawing.Size(741, 23);
            this.txtMacTau.TabIndex = 5;
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(4, 556);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(64, 23);
            this.labelX2.TabIndex = 32;
            this.labelX2.Text = "Mác Tầu";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(4, 490);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "ID";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtID.Border.Class = "TextBoxBorder";
            this.txtID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtID.DisabledBackColor = System.Drawing.Color.White;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.Black;
            this.txtID.Location = new System.Drawing.Point(4, 523);
            this.txtID.Name = "txtID";
            this.txtID.PreventEnterBeep = true;
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(64, 23);
            this.txtID.TabIndex = 6;
            // 
            // tblButton
            // 
            this.tblButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblButton.ColumnCount = 8;
            this.tblTraTim.SetColumnSpan(this.tblButton, 7);
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButton.Controls.Add(this.btnThem, 1, 0);
            this.tblButton.Controls.Add(this.btnSua, 2, 0);
            this.tblButton.Controls.Add(this.btnXoa, 3, 0);
            this.tblButton.Controls.Add(this.btnExport, 6, 0);
            this.tblButton.Controls.Add(this.btnLuu, 4, 0);
            this.tblButton.Controls.Add(this.btnHuy, 5, 0);
            this.tblButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButton.ForeColor = System.Drawing.Color.Black;
            this.tblButton.Location = new System.Drawing.Point(2, 586);
            this.tblButton.Margin = new System.Windows.Forms.Padding(1);
            this.tblButton.Name = "tblButton";
            this.tblButton.RowCount = 1;
            this.tblButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButton.Size = new System.Drawing.Size(816, 33);
            this.tblButton.TabIndex = 5;
            // 
            // btnThem
            // 
            this.btnThem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnThem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(113, 1);
            this.btnThem.Margin = new System.Windows.Forms.Padding(1);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 31);
            this.btnThem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThem.Symbol = "";
            this.btnThem.SymbolColor = System.Drawing.Color.Blue;
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "&Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSua.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(213, 1);
            this.btnSua.Margin = new System.Windows.Forms.Padding(1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 31);
            this.btnSua.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSua.Symbol = "";
            this.btnSua.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "&Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnXoa.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(313, 1);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 31);
            this.btnXoa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXoa.Symbol = "";
            this.btnXoa.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(613, 1);
            this.btnExport.Margin = new System.Windows.Forms.Padding(1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 31);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.Symbol = "";
            this.btnExport.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "&Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnLuu.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(413, 1);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 31);
            this.btnLuu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLuu.Symbol = "";
            this.btnLuu.SymbolColor = System.Drawing.Color.Blue;
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnHuy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(513, 1);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(1);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 31);
            this.btnHuy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHuy.Symbol = "";
            this.btnHuy.SymbolColor = System.Drawing.Color.Gray;
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX6.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.Color.Black;
            this.labelX6.Location = new System.Drawing.Point(75, 490);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(161, 23);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "Loại Phiếu";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // cboLoaiPhieu
            // 
            this.cboLoaiPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiPhieu.DisplayMember = "Text";
            this.cboLoaiPhieu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiPhieu.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiPhieu.FormattingEnabled = true;
            this.cboLoaiPhieu.ItemHeight = 16;
            this.cboLoaiPhieu.Location = new System.Drawing.Point(75, 524);
            this.cboLoaiPhieu.Name = "cboLoaiPhieu";
            this.cboLoaiPhieu.Size = new System.Drawing.Size(161, 22);
            this.cboLoaiPhieu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiPhieu.TabIndex = 0;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.Black;
            this.labelX3.Location = new System.Drawing.Point(344, 490);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(161, 23);
            this.labelX3.TabIndex = 31;
            this.labelX3.Text = "Ga Name";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX14
            // 
            this.labelX14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX14.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX14.ForeColor = System.Drawing.Color.Black;
            this.labelX14.Location = new System.Drawing.Point(512, 490);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(94, 23);
            this.labelX14.TabIndex = 30;
            this.labelX14.Text = "Đơn Giá";
            this.labelX14.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX13
            // 
            this.labelX13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX13.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX13.ForeColor = System.Drawing.Color.Black;
            this.labelX13.Location = new System.Drawing.Point(613, 490);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(94, 23);
            this.labelX13.TabIndex = 29;
            this.labelX13.Text = "Đơn Vị";
            this.labelX13.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtDonGia
            // 
            this.txtDonGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonGia.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDonGia.Border.Class = "TextBoxBorder";
            this.txtDonGia.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDonGia.DisabledBackColor = System.Drawing.Color.White;
            this.txtDonGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonGia.ForeColor = System.Drawing.Color.Black;
            this.txtDonGia.Location = new System.Drawing.Point(512, 523);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.PreventEnterBeep = true;
            this.txtDonGia.Size = new System.Drawing.Size(94, 23);
            this.txtDonGia.TabIndex = 2;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonVi.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDonVi.Border.Class = "TextBoxBorder";
            this.txtDonVi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDonVi.DisabledBackColor = System.Drawing.Color.White;
            this.txtDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonVi.ForeColor = System.Drawing.Color.Black;
            this.txtDonVi.Location = new System.Drawing.Point(613, 523);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.PreventEnterBeep = true;
            this.txtDonVi.Size = new System.Drawing.Size(94, 23);
            this.txtDonVi.TabIndex = 3;
            // 
            // labelX10
            // 
            this.labelX10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX10.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.Color.Black;
            this.labelX10.Location = new System.Drawing.Point(714, 490);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(102, 23);
            this.labelX10.TabIndex = 25;
            this.labelX10.Text = "Ngày Hiệu Lực";
            this.labelX10.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sdNgayHL
            // 
            this.sdNgayHL.BackColor = System.Drawing.Color.White;
            this.sdNgayHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayHL.ForeColor = System.Drawing.Color.Black;
            this.sdNgayHL.IsTime = false;
            this.sdNgayHL.Location = new System.Drawing.Point(714, 522);
            this.sdNgayHL.Name = "sdNgayHL";
            this.sdNgayHL.Size = new System.Drawing.Size(94, 23);
            this.sdNgayHL.TabIndex = 4;
            this.sdNgayHL.Text = "13.12.2022";
            this.sdNgayHL.Value = new System.DateTime(2022, 12, 13, 0, 0, 0, 0);
            // 
            // HNPhieuThuongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.tblTraTim);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.Name = "HNPhieuThuongForm";
            this.Text = "DANH SÁCH PHIẾU THƯỞNG";
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLoaiPhieu)).EndInit();
            this.tblTraTim.ResumeLayout(false);
            this.tblTraTim.PerformLayout();
            this.tblButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiPhieuTT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tblTraTim;
        private System.Windows.Forms.BindingSource bsLoaiPhieu;       
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtID;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.TableLayoutPanel tblButton;
        private DevComponents.DotNetBar.ButtonX btnThem;
        private DevComponents.DotNetBar.ButtonX btnSua;
        private DevComponents.DotNetBar.ButtonX btnXoa;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnLuu;
        private DevComponents.DotNetBar.ButtonX btnHuy;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiPhieu;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDonGia;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDonVi;
        private Controls.SmartDate sdNgayTT;
        private System.Windows.Forms.Label label1;
        private Controls.SmartDate sdNgayHL;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMacTauTT;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGaName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGaID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMacTau;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiPhieuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn macTauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gaNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyNameDataGridViewTextBoxColumn;
    }
}
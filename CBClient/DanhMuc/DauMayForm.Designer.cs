namespace CBClient.DanhMuc
{
    partial class DauMayForm
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
            this.txtLoaiMayTT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.cboCTQLTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDauMayTT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCTSHTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dauMayIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiMayIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maCTSoHuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenCTSoHuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maCTQuanLyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenCTQuanLyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsDauMay = new System.Windows.Forms.BindingSource(this.components);
            this.tblTraTim = new System.Windows.Forms.TableLayoutPanel();
            this.cboCTQL = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.sdNgayHL = new CBClient.Controls.SmartDate();
            this.cboCTSH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtLoaiMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDauMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tblButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnThem = new DevComponents.DotNetBar.ButtonX();
            this.btnSua = new DevComponents.DotNetBar.ButtonX();
            this.btnXoa = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnLuu = new DevComponents.DotNetBar.ButtonX();
            this.btnHuy = new DevComponents.DotNetBar.ButtonX();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDauMay)).BeginInit();
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
            this.ExpTraTim.Controls.Add(this.txtLoaiMayTT);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.cboCTQLTT);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.txtDauMayTT);
            this.ExpTraTim.Controls.Add(this.label7);
            this.ExpTraTim.Controls.Add(this.cboCTSHTT);
            this.ExpTraTim.Controls.Add(this.label5);
            this.ExpTraTim.Controls.Add(this.btnTraTim);
            this.ExpTraTim.Controls.Add(this.lblTableCount);
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
            // txtLoaiMayTT
            // 
            this.txtLoaiMayTT.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLoaiMayTT.Border.Class = "TextBoxBorder";
            this.txtLoaiMayTT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLoaiMayTT.DisabledBackColor = System.Drawing.Color.White;
            this.txtLoaiMayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiMayTT.ForeColor = System.Drawing.Color.Black;
            this.txtLoaiMayTT.Location = new System.Drawing.Point(81, 123);
            this.txtLoaiMayTT.Name = "txtLoaiMayTT";
            this.txtLoaiMayTT.PreventEnterBeep = true;
            this.txtLoaiMayTT.Size = new System.Drawing.Size(105, 23);
            this.txtLoaiMayTT.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(1, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 121;
            this.label2.Text = "Loại máy TT";
            // 
            // cboCTQLTT
            // 
            this.cboCTQLTT.DisplayMember = "Text";
            this.cboCTQLTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCTQLTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCTQLTT.ForeColor = System.Drawing.Color.Black;
            this.cboCTQLTT.FormattingEnabled = true;
            this.cboCTQLTT.ItemHeight = 16;
            this.cboCTQLTT.Location = new System.Drawing.Point(4, 89);
            this.cboCTQLTT.Name = "cboCTQLTT";
            this.cboCTQLTT.Size = new System.Drawing.Size(182, 22);
            this.cboCTQLTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCTQLTT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 16);
            this.label1.TabIndex = 119;
            this.label1.Text = "Xí nghiệp quản lý đầu máy";
            // 
            // txtDauMayTT
            // 
            this.txtDauMayTT.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDauMayTT.Border.Class = "TextBoxBorder";
            this.txtDauMayTT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDauMayTT.DisabledBackColor = System.Drawing.Color.White;
            this.txtDauMayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDauMayTT.ForeColor = System.Drawing.Color.Black;
            this.txtDauMayTT.Location = new System.Drawing.Point(80, 152);
            this.txtDauMayTT.Name = "txtDauMayTT";
            this.txtDauMayTT.PreventEnterBeep = true;
            this.txtDauMayTT.Size = new System.Drawing.Size(105, 23);
            this.txtDauMayTT.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 16);
            this.label7.TabIndex = 117;
            this.label7.Text = "Đầu máy TT";
            // 
            // cboCTSHTT
            // 
            this.cboCTSHTT.DisplayMember = "Text";
            this.cboCTSHTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCTSHTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCTSHTT.ForeColor = System.Drawing.Color.Black;
            this.cboCTSHTT.FormattingEnabled = true;
            this.cboCTSHTT.ItemHeight = 16;
            this.cboCTSHTT.Location = new System.Drawing.Point(4, 45);
            this.cboCTSHTT.Name = "cboCTSHTT";
            this.cboCTSHTT.Size = new System.Drawing.Size(182, 22);
            this.cboCTSHTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCTSHTT.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 16);
            this.label5.TabIndex = 103;
            this.label5.Text = "Xí nghiệp sở hữu đầu máy";
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(4, 181);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(182, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 4;
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.dauMayIDDataGridViewTextBoxColumn,
            this.loaiMayIDDataGridViewTextBoxColumn,
            this.maCTSoHuuDataGridViewTextBoxColumn,
            this.tenCTSoHuuDataGridViewTextBoxColumn,
            this.maCTQuanLyDataGridViewTextBoxColumn,
            this.tenCTQuanLyDataGridViewTextBoxColumn,
            this.ngayHLDataGridViewTextBoxColumn,
            this.activeDataGridViewCheckBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.createdNameDataGridViewTextBoxColumn,
            this.modifyDateDataGridViewTextBoxColumn,
            this.modifyByDataGridViewTextBoxColumn,
            this.modifyNameDataGridViewTextBoxColumn});
            this.tblTraTim.SetColumnSpan(this.dataGridView1, 6);
            this.dataGridView1.DataSource = this.bsDauMay;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 511);
            this.dataGridView1.TabIndex = 6;
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
            // dauMayIDDataGridViewTextBoxColumn
            // 
            this.dauMayIDDataGridViewTextBoxColumn.DataPropertyName = "DauMayID";
            this.dauMayIDDataGridViewTextBoxColumn.HeaderText = "DauMayID";
            this.dauMayIDDataGridViewTextBoxColumn.Name = "dauMayIDDataGridViewTextBoxColumn";
            this.dauMayIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dauMayIDDataGridViewTextBoxColumn.Width = 83;
            // 
            // loaiMayIDDataGridViewTextBoxColumn
            // 
            this.loaiMayIDDataGridViewTextBoxColumn.DataPropertyName = "LoaiMayID";
            this.loaiMayIDDataGridViewTextBoxColumn.HeaderText = "LoaiMayID";
            this.loaiMayIDDataGridViewTextBoxColumn.Name = "loaiMayIDDataGridViewTextBoxColumn";
            this.loaiMayIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.loaiMayIDDataGridViewTextBoxColumn.Width = 83;
            // 
            // maCTSoHuuDataGridViewTextBoxColumn
            // 
            this.maCTSoHuuDataGridViewTextBoxColumn.DataPropertyName = "MaCTSoHuu";
            this.maCTSoHuuDataGridViewTextBoxColumn.HeaderText = "MaCTSoHuu";
            this.maCTSoHuuDataGridViewTextBoxColumn.Name = "maCTSoHuuDataGridViewTextBoxColumn";
            this.maCTSoHuuDataGridViewTextBoxColumn.ReadOnly = true;
            this.maCTSoHuuDataGridViewTextBoxColumn.Width = 94;
            // 
            // tenCTSoHuuDataGridViewTextBoxColumn
            // 
            this.tenCTSoHuuDataGridViewTextBoxColumn.DataPropertyName = "TenCTSoHuu";
            this.tenCTSoHuuDataGridViewTextBoxColumn.HeaderText = "TenCTSoHuu";
            this.tenCTSoHuuDataGridViewTextBoxColumn.Name = "tenCTSoHuuDataGridViewTextBoxColumn";
            this.tenCTSoHuuDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCTSoHuuDataGridViewTextBoxColumn.Width = 98;
            // 
            // maCTQuanLyDataGridViewTextBoxColumn
            // 
            this.maCTQuanLyDataGridViewTextBoxColumn.DataPropertyName = "MaCTQuanLy";
            this.maCTQuanLyDataGridViewTextBoxColumn.HeaderText = "MaCTQuanLy";
            this.maCTQuanLyDataGridViewTextBoxColumn.Name = "maCTQuanLyDataGridViewTextBoxColumn";
            this.maCTQuanLyDataGridViewTextBoxColumn.ReadOnly = true;
            this.maCTQuanLyDataGridViewTextBoxColumn.Width = 98;
            // 
            // tenCTQuanLyDataGridViewTextBoxColumn
            // 
            this.tenCTQuanLyDataGridViewTextBoxColumn.DataPropertyName = "TenCTQuanLy";
            this.tenCTQuanLyDataGridViewTextBoxColumn.HeaderText = "TenCTQuanLy";
            this.tenCTQuanLyDataGridViewTextBoxColumn.Name = "tenCTQuanLyDataGridViewTextBoxColumn";
            this.tenCTQuanLyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCTQuanLyDataGridViewTextBoxColumn.Width = 102;
            // 
            // ngayHLDataGridViewTextBoxColumn
            // 
            this.ngayHLDataGridViewTextBoxColumn.DataPropertyName = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.HeaderText = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.Name = "ngayHLDataGridViewTextBoxColumn";
            this.ngayHLDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayHLDataGridViewTextBoxColumn.Width = 71;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            this.activeDataGridViewCheckBoxColumn.Width = 43;
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
            // bsDauMay
            // 
            this.bsDauMay.DataSource = typeof(CBClient.BLLTypes.ViewDauMay);
            // 
            // tblTraTim
            // 
            this.tblTraTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblTraTim.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTraTim.ColumnCount = 6;
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tblTraTim.Controls.Add(this.cboCTQL, 3, 2);
            this.tblTraTim.Controls.Add(this.sdNgayHL, 4, 2);
            this.tblTraTim.Controls.Add(this.cboCTSH, 2, 2);
            this.tblTraTim.Controls.Add(this.labelX8, 5, 1);
            this.tblTraTim.Controls.Add(this.labelX1, 0, 1);
            this.tblTraTim.Controls.Add(this.dataGridView1, 0, 0);
            this.tblTraTim.Controls.Add(this.txtLoaiMay, 0, 2);
            this.tblTraTim.Controls.Add(this.txtDauMay, 1, 2);
            this.tblTraTim.Controls.Add(this.tblButton, 0, 3);
            this.tblTraTim.Controls.Add(this.chkActive, 5, 2);
            this.tblTraTim.Controls.Add(this.labelX6, 1, 1);
            this.tblTraTim.Controls.Add(this.labelX7, 3, 1);
            this.tblTraTim.Controls.Add(this.labelX3, 2, 1);
            this.tblTraTim.Controls.Add(this.labelX4, 4, 1);
            this.tblTraTim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTraTim.ForeColor = System.Drawing.Color.Black;
            this.tblTraTim.Location = new System.Drawing.Point(188, 0);
            this.tblTraTim.Name = "tblTraTim";
            this.tblTraTim.RowCount = 4;
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.Size = new System.Drawing.Size(820, 621);
            this.tblTraTim.TabIndex = 1;
            // 
            // cboCTQL
            // 
            this.cboCTQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCTQL.DisplayMember = "Text";
            this.cboCTQL.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCTQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCTQL.ForeColor = System.Drawing.Color.Black;
            this.cboCTQL.FormattingEnabled = true;
            this.cboCTQL.ItemHeight = 16;
            this.cboCTQL.Location = new System.Drawing.Point(455, 557);
            this.cboCTQL.MaxDropDownItems = 1;
            this.cboCTQL.Name = "cboCTQL";
            this.cboCTQL.Size = new System.Drawing.Size(192, 22);
            this.cboCTQL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCTQL.TabIndex = 3;
            // 
            // sdNgayHL
            // 
            this.sdNgayHL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.sdNgayHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayHL.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayHL.IsTime = false;
            this.sdNgayHL.Location = new System.Drawing.Point(654, 556);
            this.sdNgayHL.Name = "sdNgayHL";
            this.sdNgayHL.Size = new System.Drawing.Size(94, 23);
            this.sdNgayHL.TabIndex = 4;
            this.sdNgayHL.Text = "15.03.2023";
            this.sdNgayHL.Value = new System.DateTime(2023, 3, 15, 0, 0, 0, 0);
            // 
            // cboCTSH
            // 
            this.cboCTSH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCTSH.DisplayMember = "Text";
            this.cboCTSH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCTSH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCTSH.ForeColor = System.Drawing.Color.Black;
            this.cboCTSH.FormattingEnabled = true;
            this.cboCTSH.ItemHeight = 16;
            this.cboCTSH.Location = new System.Drawing.Point(256, 557);
            this.cboCTSH.MaxDropDownItems = 1;
            this.cboCTSH.Name = "cboCTSH";
            this.cboCTSH.Size = new System.Drawing.Size(192, 22);
            this.cboCTSH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCTSH.TabIndex = 2;
            // 
            // labelX8
            // 
            this.labelX8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX8.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.ForeColor = System.Drawing.Color.Black;
            this.labelX8.Location = new System.Drawing.Point(755, 523);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(61, 23);
            this.labelX8.TabIndex = 24;
            this.labelX8.Text = "Active";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.labelX1.Location = new System.Drawing.Point(4, 523);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(94, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "Loại máy";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtLoaiMay
            // 
            this.txtLoaiMay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoaiMay.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLoaiMay.Border.Class = "TextBoxBorder";
            this.txtLoaiMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLoaiMay.DisabledBackColor = System.Drawing.Color.White;
            this.txtLoaiMay.Enabled = false;
            this.txtLoaiMay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiMay.ForeColor = System.Drawing.Color.Black;
            this.txtLoaiMay.Location = new System.Drawing.Point(4, 556);
            this.txtLoaiMay.Name = "txtLoaiMay";
            this.txtLoaiMay.PreventEnterBeep = true;
            this.txtLoaiMay.Size = new System.Drawing.Size(94, 23);
            this.txtLoaiMay.TabIndex = 0;
            // 
            // txtDauMay
            // 
            this.txtDauMay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDauMay.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDauMay.Border.Class = "TextBoxBorder";
            this.txtDauMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDauMay.DisabledBackColor = System.Drawing.Color.White;
            this.txtDauMay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDauMay.ForeColor = System.Drawing.Color.Black;
            this.txtDauMay.Location = new System.Drawing.Point(105, 556);
            this.txtDauMay.Name = "txtDauMay";
            this.txtDauMay.PreventEnterBeep = true;
            this.txtDauMay.Size = new System.Drawing.Size(144, 23);
            this.txtDauMay.TabIndex = 1;
            // 
            // tblButton
            // 
            this.tblButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblButton.ColumnCount = 8;
            this.tblTraTim.SetColumnSpan(this.tblButton, 6);
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
            this.tblButton.TabIndex = 8;
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
            // chkActive
            // 
            this.chkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.chkActive.AutoSize = true;
            this.chkActive.BackColor = System.Drawing.Color.White;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.ForeColor = System.Drawing.Color.Black;
            this.chkActive.Location = new System.Drawing.Point(778, 555);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 26);
            this.chkActive.TabIndex = 5;
            this.chkActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkActive.UseVisualStyleBackColor = false;
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
            this.labelX6.Location = new System.Drawing.Point(105, 523);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(144, 23);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "Đầu máy";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX7
            // 
            this.labelX7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX7.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.Color.Black;
            this.labelX7.Location = new System.Drawing.Point(455, 523);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(192, 23);
            this.labelX7.TabIndex = 12;
            this.labelX7.Text = "Cty Quản lý";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.labelX3.Location = new System.Drawing.Point(256, 523);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(192, 23);
            this.labelX3.TabIndex = 15;
            this.labelX3.Text = "Cty Sở hữu";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.labelX4.Location = new System.Drawing.Point(654, 523);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(94, 23);
            this.labelX4.TabIndex = 23;
            this.labelX4.Text = "Ngày HL";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // DauMayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.tblTraTim);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.Name = "DauMayForm";
            this.Text = "DANH SÁCH ĐẦU MÁY ĐV SỞ HỮU";
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDauMay)).EndInit();
            this.tblTraTim.ResumeLayout(false);
            this.tblTraTim.PerformLayout();
            this.tblButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tblTraTim;
        private System.Windows.Forms.BindingSource bsDauMay;       
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLoaiMay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDauMay;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.TableLayoutPanel tblButton;
        private DevComponents.DotNetBar.ButtonX btnThem;
        private DevComponents.DotNetBar.ButtonX btnSua;
        private DevComponents.DotNetBar.ButtonX btnXoa;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnLuu;
        private DevComponents.DotNetBar.ButtonX btnHuy;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCTSHTT;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDauMayTT;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.CheckBox chkActive;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCTSH;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dauMayIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiMayIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maCTSoHuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCTSoHuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maCTQuanLyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCTQuanLyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyNameDataGridViewTextBoxColumn;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLoaiMayTT;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCTQLTT;
        private System.Windows.Forms.Label label1;
        private Controls.SmartDate sdNgayHL;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCTQL;
    }
}
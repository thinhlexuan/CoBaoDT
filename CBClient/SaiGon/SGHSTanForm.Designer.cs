namespace CBClient.SaiGon
{
    partial class SGHSTanForm
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
            this.sdNgayTT = new CBClient.Controls.SmartDate();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLoaiMayTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiMayIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanMinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanMaxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heSoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createddateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdbyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifydateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifybyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsHSTan = new System.Windows.Forms.BindingSource(this.components);
            this.tblTraTim = new System.Windows.Forms.TableLayoutPanel();
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
            this.cboLoaiMay = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.txtTanMin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtTanMax = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtHeSo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.txtDonVi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.sdNgayHL = new CBClient.Controls.SmartDate();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHSTan)).BeginInit();
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
            this.ExpTraTim.Controls.Add(this.sdNgayTT);
            this.ExpTraTim.Controls.Add(this.label1);
            this.ExpTraTim.Controls.Add(this.cboLoaiMayTT);
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
            // sdNgayTT
            // 
            this.sdNgayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayTT.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayTT.IsTime = false;
            this.sdNgayTT.Location = new System.Drawing.Point(74, 31);
            this.sdNgayTT.Name = "sdNgayTT";
            this.sdNgayTT.Size = new System.Drawing.Size(105, 23);
            this.sdNgayTT.TabIndex = 0;
            this.sdNgayTT.Text = "25.03.2022";
            this.sdNgayTT.Value = new System.DateTime(2022, 3, 25, 0, 0, 0, 0);
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
            // cboLoaiMayTT
            // 
            this.cboLoaiMayTT.DisplayMember = "Text";
            this.cboLoaiMayTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiMayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiMayTT.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiMayTT.FormattingEnabled = true;
            this.cboLoaiMayTT.ItemHeight = 16;
            this.cboLoaiMayTT.Location = new System.Drawing.Point(3, 79);
            this.cboLoaiMayTT.Name = "cboLoaiMayTT";
            this.cboLoaiMayTT.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiMayTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiMayTT.TabIndex = 1;
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(3, 117);
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
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Loại máy";
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
            this.loaiMayIDDataGridViewTextBoxColumn,
            this.tanMinDataGridViewTextBoxColumn,
            this.tanMaxDataGridViewTextBoxColumn,
            this.heSoDataGridViewTextBoxColumn,
            this.donViDataGridViewTextBoxColumn,
            this.ngayHLDataGridViewTextBoxColumn,
            this.createddateDataGridViewTextBoxColumn,
            this.createdbyDataGridViewTextBoxColumn,
            this.createdNameDataGridViewTextBoxColumn,
            this.modifydateDataGridViewTextBoxColumn,
            this.modifybyDataGridViewTextBoxColumn,
            this.modifyNameDataGridViewTextBoxColumn});
            this.tblTraTim.SetColumnSpan(this.dataGridView1, 7);
            this.dataGridView1.DataSource = this.bsHSTan;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 511);
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
            // loaiMayIDDataGridViewTextBoxColumn
            // 
            this.loaiMayIDDataGridViewTextBoxColumn.DataPropertyName = "LoaiMayID";
            this.loaiMayIDDataGridViewTextBoxColumn.HeaderText = "LoaiMayID";
            this.loaiMayIDDataGridViewTextBoxColumn.Name = "loaiMayIDDataGridViewTextBoxColumn";
            this.loaiMayIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.loaiMayIDDataGridViewTextBoxColumn.Width = 83;
            // 
            // tanMinDataGridViewTextBoxColumn
            // 
            this.tanMinDataGridViewTextBoxColumn.DataPropertyName = "TanMin";
            this.tanMinDataGridViewTextBoxColumn.HeaderText = "TanMin";
            this.tanMinDataGridViewTextBoxColumn.Name = "tanMinDataGridViewTextBoxColumn";
            this.tanMinDataGridViewTextBoxColumn.ReadOnly = true;
            this.tanMinDataGridViewTextBoxColumn.Width = 68;
            // 
            // tanMaxDataGridViewTextBoxColumn
            // 
            this.tanMaxDataGridViewTextBoxColumn.DataPropertyName = "TanMax";
            this.tanMaxDataGridViewTextBoxColumn.HeaderText = "TanMax";
            this.tanMaxDataGridViewTextBoxColumn.Name = "tanMaxDataGridViewTextBoxColumn";
            this.tanMaxDataGridViewTextBoxColumn.ReadOnly = true;
            this.tanMaxDataGridViewTextBoxColumn.Width = 71;
            // 
            // heSoDataGridViewTextBoxColumn
            // 
            this.heSoDataGridViewTextBoxColumn.DataPropertyName = "HeSo";
            this.heSoDataGridViewTextBoxColumn.HeaderText = "HeSo";
            this.heSoDataGridViewTextBoxColumn.Name = "heSoDataGridViewTextBoxColumn";
            this.heSoDataGridViewTextBoxColumn.ReadOnly = true;
            this.heSoDataGridViewTextBoxColumn.Width = 59;
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
            // createddateDataGridViewTextBoxColumn
            // 
            this.createddateDataGridViewTextBoxColumn.DataPropertyName = "Createddate";
            this.createddateDataGridViewTextBoxColumn.HeaderText = "Createddate";
            this.createddateDataGridViewTextBoxColumn.Name = "createddateDataGridViewTextBoxColumn";
            this.createddateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createddateDataGridViewTextBoxColumn.Width = 90;
            // 
            // createdbyDataGridViewTextBoxColumn
            // 
            this.createdbyDataGridViewTextBoxColumn.DataPropertyName = "Createdby";
            this.createdbyDataGridViewTextBoxColumn.HeaderText = "Createdby";
            this.createdbyDataGridViewTextBoxColumn.Name = "createdbyDataGridViewTextBoxColumn";
            this.createdbyDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdbyDataGridViewTextBoxColumn.Width = 80;
            // 
            // createdNameDataGridViewTextBoxColumn
            // 
            this.createdNameDataGridViewTextBoxColumn.DataPropertyName = "CreatedName";
            this.createdNameDataGridViewTextBoxColumn.HeaderText = "CreatedName";
            this.createdNameDataGridViewTextBoxColumn.Name = "createdNameDataGridViewTextBoxColumn";
            this.createdNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdNameDataGridViewTextBoxColumn.Width = 97;
            // 
            // modifydateDataGridViewTextBoxColumn
            // 
            this.modifydateDataGridViewTextBoxColumn.DataPropertyName = "Modifydate";
            this.modifydateDataGridViewTextBoxColumn.HeaderText = "Modifydate";
            this.modifydateDataGridViewTextBoxColumn.Name = "modifydateDataGridViewTextBoxColumn";
            this.modifydateDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifydateDataGridViewTextBoxColumn.Width = 84;
            // 
            // modifybyDataGridViewTextBoxColumn
            // 
            this.modifybyDataGridViewTextBoxColumn.DataPropertyName = "Modifyby";
            this.modifybyDataGridViewTextBoxColumn.HeaderText = "Modifyby";
            this.modifybyDataGridViewTextBoxColumn.Name = "modifybyDataGridViewTextBoxColumn";
            this.modifybyDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifybyDataGridViewTextBoxColumn.Width = 74;
            // 
            // modifyNameDataGridViewTextBoxColumn
            // 
            this.modifyNameDataGridViewTextBoxColumn.DataPropertyName = "ModifyName";
            this.modifyNameDataGridViewTextBoxColumn.HeaderText = "ModifyName";
            this.modifyNameDataGridViewTextBoxColumn.Name = "modifyNameDataGridViewTextBoxColumn";
            this.modifyNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifyNameDataGridViewTextBoxColumn.Width = 91;
            // 
            // bsHSTan
            // 
            this.bsHSTan.DataSource = typeof(CBClient.BLLTypes.SGHSTan);
            // 
            // tblTraTim
            // 
            this.tblTraTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblTraTim.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTraTim.ColumnCount = 7;
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTraTim.Controls.Add(this.labelX1, 0, 1);
            this.tblTraTim.Controls.Add(this.dataGridView1, 0, 0);
            this.tblTraTim.Controls.Add(this.txtID, 0, 2);
            this.tblTraTim.Controls.Add(this.tblButton, 0, 3);
            this.tblTraTim.Controls.Add(this.labelX6, 1, 1);
            this.tblTraTim.Controls.Add(this.cboLoaiMay, 1, 2);
            this.tblTraTim.Controls.Add(this.labelX12, 2, 1);
            this.tblTraTim.Controls.Add(this.txtTanMin, 2, 2);
            this.tblTraTim.Controls.Add(this.labelX8, 3, 1);
            this.tblTraTim.Controls.Add(this.txtTanMax, 3, 2);
            this.tblTraTim.Controls.Add(this.labelX4, 4, 1);
            this.tblTraTim.Controls.Add(this.txtHeSo, 4, 2);
            this.tblTraTim.Controls.Add(this.labelX13, 5, 1);
            this.tblTraTim.Controls.Add(this.txtDonVi, 5, 2);
            this.tblTraTim.Controls.Add(this.labelX10, 6, 1);
            this.tblTraTim.Controls.Add(this.sdNgayHL, 6, 2);
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
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.Size = new System.Drawing.Size(820, 621);
            this.tblTraTim.TabIndex = 1;
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
            this.labelX1.Size = new System.Drawing.Size(40, 23);
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
            this.txtID.ButtonCustom.Tooltip = "";
            this.txtID.ButtonCustom2.Tooltip = "";
            this.txtID.DisabledBackColor = System.Drawing.Color.White;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.Black;
            this.txtID.Location = new System.Drawing.Point(4, 556);
            this.txtID.Name = "txtID";
            this.txtID.PreventEnterBeep = true;
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(40, 23);
            this.txtID.TabIndex = 1;
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
            this.tblButton.TabIndex = 7;
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
            this.labelX6.Location = new System.Drawing.Point(51, 523);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(121, 23);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "Loại Máy";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // cboLoaiMay
            // 
            this.cboLoaiMay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiMay.DisplayMember = "Text";
            this.cboLoaiMay.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiMay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiMay.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiMay.FormattingEnabled = true;
            this.cboLoaiMay.ItemHeight = 16;
            this.cboLoaiMay.Location = new System.Drawing.Point(51, 557);
            this.cboLoaiMay.Name = "cboLoaiMay";
            this.cboLoaiMay.Size = new System.Drawing.Size(121, 22);
            this.cboLoaiMay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiMay.TabIndex = 0;
            // 
            // labelX12
            // 
            this.labelX12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX12.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.Color.Black;
            this.labelX12.Location = new System.Drawing.Point(179, 523);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(121, 23);
            this.labelX12.TabIndex = 27;
            this.labelX12.Text = "Tấn Min";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtTanMin
            // 
            this.txtTanMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTanMin.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTanMin.Border.Class = "TextBoxBorder";
            this.txtTanMin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTanMin.ButtonCustom.Tooltip = "";
            this.txtTanMin.ButtonCustom2.Tooltip = "";
            this.txtTanMin.DisabledBackColor = System.Drawing.Color.White;
            this.txtTanMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTanMin.ForeColor = System.Drawing.Color.Black;
            this.txtTanMin.Location = new System.Drawing.Point(179, 556);
            this.txtTanMin.Name = "txtTanMin";
            this.txtTanMin.PreventEnterBeep = true;
            this.txtTanMin.Size = new System.Drawing.Size(121, 23);
            this.txtTanMin.TabIndex = 2;
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
            this.labelX8.Location = new System.Drawing.Point(307, 523);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(121, 23);
            this.labelX8.TabIndex = 23;
            this.labelX8.Text = "Tấn Max";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtTanMax
            // 
            this.txtTanMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTanMax.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTanMax.Border.Class = "TextBoxBorder";
            this.txtTanMax.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTanMax.ButtonCustom.Tooltip = "";
            this.txtTanMax.ButtonCustom2.Tooltip = "";
            this.txtTanMax.DisabledBackColor = System.Drawing.Color.White;
            this.txtTanMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTanMax.ForeColor = System.Drawing.Color.Black;
            this.txtTanMax.Location = new System.Drawing.Point(307, 556);
            this.txtTanMax.Name = "txtTanMax";
            this.txtTanMax.PreventEnterBeep = true;
            this.txtTanMax.Size = new System.Drawing.Size(121, 23);
            this.txtTanMax.TabIndex = 3;
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
            this.labelX4.Location = new System.Drawing.Point(435, 523);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(121, 23);
            this.labelX4.TabIndex = 28;
            this.labelX4.Text = "Hệ Số";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtHeSo
            // 
            this.txtHeSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeSo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtHeSo.Border.Class = "TextBoxBorder";
            this.txtHeSo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHeSo.ButtonCustom.Tooltip = "";
            this.txtHeSo.ButtonCustom2.Tooltip = "";
            this.txtHeSo.DisabledBackColor = System.Drawing.Color.White;
            this.txtHeSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeSo.ForeColor = System.Drawing.Color.Black;
            this.txtHeSo.Location = new System.Drawing.Point(435, 556);
            this.txtHeSo.Name = "txtHeSo";
            this.txtHeSo.PreventEnterBeep = true;
            this.txtHeSo.Size = new System.Drawing.Size(121, 23);
            this.txtHeSo.TabIndex = 4;
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
            this.labelX13.Location = new System.Drawing.Point(563, 523);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(121, 23);
            this.labelX13.TabIndex = 29;
            this.labelX13.Text = "Đơn Vị";
            this.labelX13.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.txtDonVi.ButtonCustom.Tooltip = "";
            this.txtDonVi.ButtonCustom2.Tooltip = "";
            this.txtDonVi.DisabledBackColor = System.Drawing.Color.White;
            this.txtDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonVi.ForeColor = System.Drawing.Color.Black;
            this.txtDonVi.Location = new System.Drawing.Point(563, 556);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.PreventEnterBeep = true;
            this.txtDonVi.Size = new System.Drawing.Size(121, 23);
            this.txtDonVi.TabIndex = 5;
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
            this.labelX10.Location = new System.Drawing.Point(691, 523);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(125, 23);
            this.labelX10.TabIndex = 25;
            this.labelX10.Text = "Ngày Hiệu Lực";
            this.labelX10.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sdNgayHL
            // 
            this.sdNgayHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayHL.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayHL.IsTime = false;
            this.sdNgayHL.Location = new System.Drawing.Point(691, 555);
            this.sdNgayHL.Name = "sdNgayHL";
            this.sdNgayHL.Size = new System.Drawing.Size(105, 23);
            this.sdNgayHL.TabIndex = 6;
            this.sdNgayHL.Text = "25.03.2022";
            this.sdNgayHL.Value = new System.DateTime(2022, 3, 25, 0, 0, 0, 0);
            // 
            // SGHSTanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.tblTraTim);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.Name = "SGHSTanForm";
            this.Text = "DANH SÁCH HỆ SỐ TẤN SG";
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHSTan)).EndInit();
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
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiMayTT;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tblTraTim;
        private System.Windows.Forms.BindingSource bsHSTan;       
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
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiMay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTanMin;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTanMax;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHeSo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDonVi;
        private Controls.SmartDate sdNgayTT;
        private System.Windows.Forms.Label label1;
        private Controls.SmartDate sdNgayHL;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiMayIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanMinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanMaxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heSoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createddateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdbyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifydateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifybyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyNameDataGridViewTextBoxColumn;
    }
}
namespace CBClient.NhienLieu
{
    partial class BangGiaForm
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
            this.sdNgayKT = new CBClient.Controls.SmartDate();
            this.sdNgayBD = new CBClient.Controls.SmartDate();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboLoaiDMTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTramNLTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.maTramNLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenTramNLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maDauMoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenDauMoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phieuNhapIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGiaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tyTrongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghiChuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsBangGia = new System.Windows.Forms.BindingSource(this.components);
            this.tblTraTim = new System.Windows.Forms.TableLayoutPanel();
            this.txtGioHL = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboLoaiDM = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTramNL = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtGhiChu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.tblButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnThem = new DevComponents.DotNetBar.ButtonX();
            this.btnSua = new DevComponents.DotNetBar.ButtonX();
            this.btnXoa = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnLuu = new DevComponents.DotNetBar.ButtonX();
            this.btnHuy = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.sdNgayHL = new CBClient.Controls.SmartDate();
            this.txtDonVi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.txtDonGia = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtPhiepNhap = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtTyTrong = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ExpTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBangGia)).BeginInit();
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
            this.ExpTraTim.Controls.Add(this.sdNgayKT);
            this.ExpTraTim.Controls.Add(this.sdNgayBD);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.label6);
            this.ExpTraTim.Controls.Add(this.cboLoaiDMTT);
            this.ExpTraTim.Controls.Add(this.cboTramNLTT);
            this.ExpTraTim.Controls.Add(this.label1);
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
            // sdNgayKT
            // 
            this.sdNgayKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayKT.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayKT.IsTime = false;
            this.sdNgayKT.Location = new System.Drawing.Point(77, 61);
            this.sdNgayKT.Name = "sdNgayKT";
            this.sdNgayKT.Size = new System.Drawing.Size(105, 23);
            this.sdNgayKT.TabIndex = 1;
            this.sdNgayKT.Text = "20.06.2023";
            this.sdNgayKT.Value = new System.DateTime(2023, 6, 20, 0, 0, 0, 0);
            this.sdNgayKT.Validated += new System.EventHandler(this.sdNgayKT_Validated);
            // 
            // sdNgayBD
            // 
            this.sdNgayBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayBD.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayBD.IsTime = false;
            this.sdNgayBD.Location = new System.Drawing.Point(77, 34);
            this.sdNgayBD.Name = "sdNgayBD";
            this.sdNgayBD.Size = new System.Drawing.Size(105, 23);
            this.sdNgayBD.TabIndex = 0;
            this.sdNgayBD.Text = "20.06.2023";
            this.sdNgayBD.Value = new System.DateTime(2023, 6, 20, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 110;
            this.label2.Text = "Từ ngày";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 111;
            this.label6.Text = "Đến ngày";
            // 
            // cboLoaiDMTT
            // 
            this.cboLoaiDMTT.DisplayMember = "Text";
            this.cboLoaiDMTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiDMTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiDMTT.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiDMTT.FormattingEnabled = true;
            this.cboLoaiDMTT.ItemHeight = 16;
            this.cboLoaiDMTT.Location = new System.Drawing.Point(3, 159);
            this.cboLoaiDMTT.Name = "cboLoaiDMTT";
            this.cboLoaiDMTT.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiDMTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiDMTT.TabIndex = 3;
            // 
            // cboTramNLTT
            // 
            this.cboTramNLTT.DisplayMember = "Text";
            this.cboTramNLTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTramNLTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTramNLTT.ForeColor = System.Drawing.Color.Black;
            this.cboTramNLTT.FormattingEnabled = true;
            this.cboTramNLTT.ItemHeight = 16;
            this.cboTramNLTT.Location = new System.Drawing.Point(6, 106);
            this.cboTramNLTT.Name = "cboTramNLTT";
            this.cboTramNLTT.Size = new System.Drawing.Size(176, 22);
            this.cboTramNLTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTramNLTT.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 105;
            this.label1.Text = "Loại dầu mỡ tra tìm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 16);
            this.label5.TabIndex = 103;
            this.label5.Text = "Trạm nhiên liệu tra tìm";
            // 
            // btnTraTim
            // 
            this.btnTraTim.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTraTim.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTraTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraTim.Location = new System.Drawing.Point(3, 187);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(178, 28);
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
            this.maTramNLDataGridViewTextBoxColumn,
            this.tenTramNLDataGridViewTextBoxColumn,
            this.maDauMoDataGridViewTextBoxColumn,
            this.tenDauMoDataGridViewTextBoxColumn,
            this.donViTinhDataGridViewTextBoxColumn,
            this.ngayHLDataGridViewTextBoxColumn,
            this.phieuNhapIDDataGridViewTextBoxColumn,
            this.donGiaDataGridViewTextBoxColumn,
            this.tyTrongDataGridViewTextBoxColumn,
            this.ghiChuDataGridViewTextBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.createdNameDataGridViewTextBoxColumn,
            this.modifyDateDataGridViewTextBoxColumn,
            this.modifyByDataGridViewTextBoxColumn,
            this.modifyNameDataGridViewTextBoxColumn});
            this.tblTraTim.SetColumnSpan(this.dataGridView1, 6);
            this.dataGridView1.DataSource = this.bsBangGia;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 445);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // maTramNLDataGridViewTextBoxColumn
            // 
            this.maTramNLDataGridViewTextBoxColumn.DataPropertyName = "MaTramNL";
            this.maTramNLDataGridViewTextBoxColumn.HeaderText = "MaTramNL";
            this.maTramNLDataGridViewTextBoxColumn.Name = "maTramNLDataGridViewTextBoxColumn";
            this.maTramNLDataGridViewTextBoxColumn.ReadOnly = true;
            this.maTramNLDataGridViewTextBoxColumn.Width = 85;
            // 
            // tenTramNLDataGridViewTextBoxColumn
            // 
            this.tenTramNLDataGridViewTextBoxColumn.DataPropertyName = "TenTramNL";
            this.tenTramNLDataGridViewTextBoxColumn.HeaderText = "TenTramNL";
            this.tenTramNLDataGridViewTextBoxColumn.Name = "tenTramNLDataGridViewTextBoxColumn";
            this.tenTramNLDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenTramNLDataGridViewTextBoxColumn.Width = 89;
            // 
            // maDauMoDataGridViewTextBoxColumn
            // 
            this.maDauMoDataGridViewTextBoxColumn.DataPropertyName = "MaDauMo";
            this.maDauMoDataGridViewTextBoxColumn.HeaderText = "MaDauMo";
            this.maDauMoDataGridViewTextBoxColumn.Name = "maDauMoDataGridViewTextBoxColumn";
            this.maDauMoDataGridViewTextBoxColumn.ReadOnly = true;
            this.maDauMoDataGridViewTextBoxColumn.Width = 82;
            // 
            // tenDauMoDataGridViewTextBoxColumn
            // 
            this.tenDauMoDataGridViewTextBoxColumn.DataPropertyName = "TenDauMo";
            this.tenDauMoDataGridViewTextBoxColumn.HeaderText = "TenDauMo";
            this.tenDauMoDataGridViewTextBoxColumn.Name = "tenDauMoDataGridViewTextBoxColumn";
            this.tenDauMoDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenDauMoDataGridViewTextBoxColumn.Width = 86;
            // 
            // donViTinhDataGridViewTextBoxColumn
            // 
            this.donViTinhDataGridViewTextBoxColumn.DataPropertyName = "DonViTinh";
            this.donViTinhDataGridViewTextBoxColumn.HeaderText = "DonViTinh";
            this.donViTinhDataGridViewTextBoxColumn.Name = "donViTinhDataGridViewTextBoxColumn";
            this.donViTinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.donViTinhDataGridViewTextBoxColumn.Width = 82;
            // 
            // ngayHLDataGridViewTextBoxColumn
            // 
            this.ngayHLDataGridViewTextBoxColumn.DataPropertyName = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.HeaderText = "NgayHL";
            this.ngayHLDataGridViewTextBoxColumn.Name = "ngayHLDataGridViewTextBoxColumn";
            this.ngayHLDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayHLDataGridViewTextBoxColumn.Width = 71;
            // 
            // phieuNhapIDDataGridViewTextBoxColumn
            // 
            this.phieuNhapIDDataGridViewTextBoxColumn.DataPropertyName = "PhieuNhapID";
            this.phieuNhapIDDataGridViewTextBoxColumn.HeaderText = "PhieuNhapID";
            this.phieuNhapIDDataGridViewTextBoxColumn.Name = "phieuNhapIDDataGridViewTextBoxColumn";
            this.phieuNhapIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.phieuNhapIDDataGridViewTextBoxColumn.Width = 96;
            // 
            // donGiaDataGridViewTextBoxColumn
            // 
            this.donGiaDataGridViewTextBoxColumn.DataPropertyName = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.HeaderText = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.Name = "donGiaDataGridViewTextBoxColumn";
            this.donGiaDataGridViewTextBoxColumn.ReadOnly = true;
            this.donGiaDataGridViewTextBoxColumn.Width = 68;
            // 
            // tyTrongDataGridViewTextBoxColumn
            // 
            this.tyTrongDataGridViewTextBoxColumn.DataPropertyName = "TyTrong";
            this.tyTrongDataGridViewTextBoxColumn.HeaderText = "TyTrong";
            this.tyTrongDataGridViewTextBoxColumn.Name = "tyTrongDataGridViewTextBoxColumn";
            this.tyTrongDataGridViewTextBoxColumn.ReadOnly = true;
            this.tyTrongDataGridViewTextBoxColumn.Width = 72;
            // 
            // ghiChuDataGridViewTextBoxColumn
            // 
            this.ghiChuDataGridViewTextBoxColumn.DataPropertyName = "GhiChu";
            this.ghiChuDataGridViewTextBoxColumn.HeaderText = "GhiChu";
            this.ghiChuDataGridViewTextBoxColumn.Name = "ghiChuDataGridViewTextBoxColumn";
            this.ghiChuDataGridViewTextBoxColumn.ReadOnly = true;
            this.ghiChuDataGridViewTextBoxColumn.Width = 67;
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
            // bsBangGia
            // 
            this.bsBangGia.DataSource = typeof(CBClient.BLLTypes.NL_BangGia);
            // 
            // tblTraTim
            // 
            this.tblTraTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblTraTim.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTraTim.ColumnCount = 6;
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tblTraTim.Controls.Add(this.txtTyTrong, 3, 4);
            this.tblTraTim.Controls.Add(this.labelX2, 3, 3);
            this.tblTraTim.Controls.Add(this.txtGioHL, 5, 2);
            this.tblTraTim.Controls.Add(this.cboLoaiDM, 1, 2);
            this.tblTraTim.Controls.Add(this.cboTramNL, 0, 2);
            this.tblTraTim.Controls.Add(this.txtGhiChu, 0, 4);
            this.tblTraTim.Controls.Add(this.labelX7, 0, 3);
            this.tblTraTim.Controls.Add(this.labelX6, 1, 1);
            this.tblTraTim.Controls.Add(this.dataGridView1, 0, 0);
            this.tblTraTim.Controls.Add(this.tblButton, 0, 5);
            this.tblTraTim.Controls.Add(this.labelX3, 0, 1);
            this.tblTraTim.Controls.Add(this.labelX1, 4, 1);
            this.tblTraTim.Controls.Add(this.labelX11, 2, 3);
            this.tblTraTim.Controls.Add(this.txtDonGia, 2, 4);
            this.tblTraTim.Controls.Add(this.labelX9, 4, 3);
            this.tblTraTim.Controls.Add(this.txtPhiepNhap, 4, 4);
            this.tblTraTim.Controls.Add(this.sdNgayHL, 4, 2);
            this.tblTraTim.Controls.Add(this.labelX4, 3, 1);
            this.tblTraTim.Controls.Add(this.txtDonVi, 3, 2);
            this.tblTraTim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTraTim.ForeColor = System.Drawing.Color.Black;
            this.tblTraTim.Location = new System.Drawing.Point(188, 0);
            this.tblTraTim.Name = "tblTraTim";
            this.tblTraTim.RowCount = 6;
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblTraTim.Size = new System.Drawing.Size(820, 621);
            this.tblTraTim.TabIndex = 1;
            // 
            // txtGioHL
            // 
            this.txtGioHL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGioHL.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGioHL.Border.Class = "TextBoxBorder";
            this.txtGioHL.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGioHL.DisabledBackColor = System.Drawing.Color.White;
            this.txtGioHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGioHL.ForeColor = System.Drawing.Color.Black;
            this.txtGioHL.Location = new System.Drawing.Point(732, 490);
            this.txtGioHL.Name = "txtGioHL";
            this.txtGioHL.PreventEnterBeep = true;
            this.txtGioHL.Size = new System.Drawing.Size(84, 23);
            this.txtGioHL.TabIndex = 4;
            this.txtGioHL.Validated += new System.EventHandler(this.txtGioHL_Validated);
            // 
            // cboLoaiDM
            // 
            this.cboLoaiDM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tblTraTim.SetColumnSpan(this.cboLoaiDM, 2);
            this.cboLoaiDM.DisplayMember = "Text";
            this.cboLoaiDM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiDM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiDM.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiDM.FormattingEnabled = true;
            this.cboLoaiDM.ItemHeight = 16;
            this.cboLoaiDM.Location = new System.Drawing.Point(158, 491);
            this.cboLoaiDM.Name = "cboLoaiDM";
            this.cboLoaiDM.Size = new System.Drawing.Size(345, 22);
            this.cboLoaiDM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiDM.TabIndex = 1;
            this.cboLoaiDM.SelectedIndexChanged += new System.EventHandler(this.cboLoaiDM_SelectedIndexChanged);
            // 
            // cboTramNL
            // 
            this.cboTramNL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTramNL.DisplayMember = "Text";
            this.cboTramNL.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTramNL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTramNL.ForeColor = System.Drawing.Color.Black;
            this.cboTramNL.FormattingEnabled = true;
            this.cboTramNL.ItemHeight = 16;
            this.cboTramNL.Location = new System.Drawing.Point(4, 491);
            this.cboTramNL.Name = "cboTramNL";
            this.cboTramNL.Size = new System.Drawing.Size(147, 22);
            this.cboTramNL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTramNL.TabIndex = 0;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGhiChu.Border.Class = "TextBoxBorder";
            this.txtGhiChu.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.txtGhiChu, 2);
            this.txtGhiChu.DisabledBackColor = System.Drawing.Color.White;
            this.txtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhiChu.ForeColor = System.Drawing.Color.Black;
            this.txtGhiChu.Location = new System.Drawing.Point(4, 556);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.PreventEnterBeep = true;
            this.txtGhiChu.Size = new System.Drawing.Size(378, 23);
            this.txtGhiChu.TabIndex = 5;
            // 
            // labelX7
            // 
            this.labelX7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX7.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.labelX7, 2);
            this.labelX7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.Color.Black;
            this.labelX7.Location = new System.Drawing.Point(4, 523);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(378, 23);
            this.labelX7.TabIndex = 27;
            this.labelX7.Text = "Ghi chú";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX6.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.labelX6, 2);
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.Color.Black;
            this.labelX6.Location = new System.Drawing.Point(158, 457);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(345, 23);
            this.labelX6.TabIndex = 26;
            this.labelX6.Text = "Loại dầu mỡ";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.tblButton.TabIndex = 9;
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
            this.labelX3.Location = new System.Drawing.Point(4, 457);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(147, 23);
            this.labelX3.TabIndex = 23;
            this.labelX3.Text = "Trạm nhiên liệu";
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
            this.labelX4.Location = new System.Drawing.Point(510, 457);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(114, 23);
            this.labelX4.TabIndex = 24;
            this.labelX4.Text = "Đơn vị tính";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.labelX1, 2);
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(631, 457);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(185, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "Ngày hiệu lực";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sdNgayHL
            // 
            this.sdNgayHL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.sdNgayHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayHL.ForeColor = System.Drawing.Color.Navy;
            this.sdNgayHL.IsTime = false;
            this.sdNgayHL.Location = new System.Drawing.Point(631, 490);
            this.sdNgayHL.Name = "sdNgayHL";
            this.sdNgayHL.Size = new System.Drawing.Size(94, 23);
            this.sdNgayHL.TabIndex = 3;
            this.sdNgayHL.Text = "20.06.2023";
            this.sdNgayHL.Value = new System.DateTime(2023, 6, 20, 0, 0, 0, 0);
            this.sdNgayHL.Validated += new System.EventHandler(this.sdNgayHL_Validated);
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
            this.txtDonVi.Location = new System.Drawing.Point(510, 490);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.PreventEnterBeep = true;
            this.txtDonVi.ReadOnly = true;
            this.txtDonVi.Size = new System.Drawing.Size(114, 23);
            this.txtDonVi.TabIndex = 2;
            // 
            // labelX11
            // 
            this.labelX11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX11.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX11.ForeColor = System.Drawing.Color.Black;
            this.labelX11.Location = new System.Drawing.Point(389, 523);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(114, 23);
            this.labelX11.TabIndex = 31;
            this.labelX11.Text = "Đơn giá";
            this.labelX11.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.txtDonGia.Location = new System.Drawing.Point(389, 556);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.PreventEnterBeep = true;
            this.txtDonGia.Size = new System.Drawing.Size(114, 23);
            this.txtDonGia.TabIndex = 6;
            // 
            // labelX9
            // 
            this.labelX9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX9.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.labelX9, 2);
            this.labelX9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX9.ForeColor = System.Drawing.Color.Black;
            this.labelX9.Location = new System.Drawing.Point(631, 523);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(185, 23);
            this.labelX9.TabIndex = 29;
            this.labelX9.Text = "Phiếu nhập";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtPhiepNhap
            // 
            this.txtPhiepNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhiepNhap.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhiepNhap.Border.Class = "TextBoxBorder";
            this.txtPhiepNhap.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tblTraTim.SetColumnSpan(this.txtPhiepNhap, 2);
            this.txtPhiepNhap.DisabledBackColor = System.Drawing.Color.White;
            this.txtPhiepNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhiepNhap.ForeColor = System.Drawing.Color.Black;
            this.txtPhiepNhap.Location = new System.Drawing.Point(631, 556);
            this.txtPhiepNhap.Name = "txtPhiepNhap";
            this.txtPhiepNhap.PreventEnterBeep = true;
            this.txtPhiepNhap.ReadOnly = true;
            this.txtPhiepNhap.Size = new System.Drawing.Size(185, 23);
            this.txtPhiepNhap.TabIndex = 8;
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
            this.labelX2.Location = new System.Drawing.Point(510, 523);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(114, 23);
            this.labelX2.TabIndex = 32;
            this.labelX2.Text = "Tỷ trọng";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtTyTrong
            // 
            this.txtTyTrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTyTrong.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTyTrong.Border.Class = "TextBoxBorder";
            this.txtTyTrong.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTyTrong.DisabledBackColor = System.Drawing.Color.White;
            this.txtTyTrong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTyTrong.ForeColor = System.Drawing.Color.Black;
            this.txtTyTrong.Location = new System.Drawing.Point(510, 556);
            this.txtTyTrong.Name = "txtTyTrong";
            this.txtTyTrong.PreventEnterBeep = true;
            this.txtTyTrong.Size = new System.Drawing.Size(114, 23);
            this.txtTyTrong.TabIndex = 7;
            // 
            // BangGiaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.tblTraTim);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "BangGiaForm";
            this.Text = "DANH SÁCH GIÁ NHIÊN LIỆU";
            this.Load += new System.EventHandler(this.BangGiaForm_Load);
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBangGia)).EndInit();
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
        private System.Windows.Forms.BindingSource bsBangGia;       
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TableLayoutPanel tblButton;
        private DevComponents.DotNetBar.ButtonX btnThem;
        private DevComponents.DotNetBar.ButtonX btnSua;
        private DevComponents.DotNetBar.ButtonX btnXoa;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnLuu;
        private DevComponents.DotNetBar.ButtonX btnHuy;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDonVi;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGhiChu;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDonGia;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX11;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhiepNhap;
        private DevComponents.DotNetBar.LabelX labelX9;
        private Controls.SmartDate sdNgayHL;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiDMTT;
        private Controls.SmartDate sdNgayKT;
        private Controls.SmartDate sdNgayBD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTramNLTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn doViTinhDataGridViewTextBoxColumn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiDM;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTramNL;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGioHL;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGiaVCFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTramNLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenTramNLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maDauMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDauMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phieuNhapIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tyTrongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghiChuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyNameDataGridViewTextBoxColumn;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTyTrong;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}
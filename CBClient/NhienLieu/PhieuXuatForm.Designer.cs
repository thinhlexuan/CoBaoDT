namespace CBClient.NhienLieu
{
    partial class PhieuXuatForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhieuXuat = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDauMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboLoaiMayTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTramNLTT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTraTim = new DevComponents.DotNetBar.ButtonX();
            this.lblTableCount = new System.Windows.Forms.Label();
            this.tblTraTim = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewCT = new System.Windows.Forms.DataGridView();
            this.dataGridViewPX = new System.Windows.Forms.DataGridView();
            this.tblButton = new System.Windows.Forms.TableLayoutPanel();
            this.lblChiTietSum = new System.Windows.Forms.Label();
            this.lblChiTietCount = new System.Windows.Forms.Label();
            this.btnThem = new DevComponents.DotNetBar.ButtonX();
            this.btnSua = new DevComponents.DotNetBar.ButtonX();
            this.btnXoa = new DevComponents.DotNetBar.ButtonX();
            this.btnMo = new DevComponents.DotNetBar.ButtonX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.btnKhoa = new DevComponents.DotNetBar.ButtonX();
            this.bsPhieuXuatCT = new System.Windows.Forms.BindingSource(this.components);
            this.bsPhieuXuat = new System.Windows.Forms.BindingSource(this.components);
            this.sdNgayKT = new CBClient.Controls.SmartDate();
            this.sdNgayBD = new CBClient.Controls.SmartDate();
            this.phieuXuatIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiPhieuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maTramNLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenTramNLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dauMayIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiMayIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soChungTuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nguoiNhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lyDoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.khoaSoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phieuXuatIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maDauMoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenDauMoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tyTrongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongVCFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phieuNhapIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGiaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bangGiaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thanhTienDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpTraTim.SuspendLayout();
            this.tblTraTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPX)).BeginInit();
            this.tblButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhieuXuatCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhieuXuat)).BeginInit();
            this.SuspendLayout();
            // 
            // ExpTraTim
            // 
            this.ExpTraTim.AutoScroll = true;
            this.ExpTraTim.CanvasColor = System.Drawing.SystemColors.Control;
            this.ExpTraTim.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.ExpTraTim.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExpTraTim.Controls.Add(this.label4);
            this.ExpTraTim.Controls.Add(this.txtPhieuXuat);
            this.ExpTraTim.Controls.Add(this.label3);
            this.ExpTraTim.Controls.Add(this.txtDauMay);
            this.ExpTraTim.Controls.Add(this.sdNgayKT);
            this.ExpTraTim.Controls.Add(this.sdNgayBD);
            this.ExpTraTim.Controls.Add(this.label2);
            this.ExpTraTim.Controls.Add(this.label6);
            this.ExpTraTim.Controls.Add(this.cboLoaiMayTT);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 115;
            this.label4.Text = "Phiếu xuất";
            // 
            // txtPhieuXuat
            // 
            this.txtPhieuXuat.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhieuXuat.Border.Class = "TextBoxBorder";
            this.txtPhieuXuat.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhieuXuat.DisabledBackColor = System.Drawing.Color.White;
            this.txtPhieuXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhieuXuat.ForeColor = System.Drawing.Color.Black;
            this.txtPhieuXuat.Location = new System.Drawing.Point(79, 206);
            this.txtPhieuXuat.Name = "txtPhieuXuat";
            this.txtPhieuXuat.PreventEnterBeep = true;
            this.txtPhieuXuat.Size = new System.Drawing.Size(103, 23);
            this.txtPhieuXuat.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 113;
            this.label3.Text = "Đầu máy";
            // 
            // txtDauMay
            // 
            this.txtDauMay.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDauMay.Border.Class = "TextBoxBorder";
            this.txtDauMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDauMay.DisabledBackColor = System.Drawing.Color.White;
            this.txtDauMay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDauMay.ForeColor = System.Drawing.Color.Black;
            this.txtDauMay.Location = new System.Drawing.Point(79, 179);
            this.txtDauMay.Name = "txtDauMay";
            this.txtDauMay.PreventEnterBeep = true;
            this.txtDauMay.Size = new System.Drawing.Size(103, 23);
            this.txtDauMay.TabIndex = 4;
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
            // cboLoaiMayTT
            // 
            this.cboLoaiMayTT.DisplayMember = "Text";
            this.cboLoaiMayTT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiMayTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiMayTT.ForeColor = System.Drawing.Color.Black;
            this.cboLoaiMayTT.FormattingEnabled = true;
            this.cboLoaiMayTT.ItemHeight = 16;
            this.cboLoaiMayTT.Location = new System.Drawing.Point(6, 151);
            this.cboLoaiMayTT.Name = "cboLoaiMayTT";
            this.cboLoaiMayTT.Size = new System.Drawing.Size(176, 22);
            this.cboLoaiMayTT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiMayTT.TabIndex = 3;
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
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 105;
            this.label1.Text = "Loại máy tra tìm";
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
            this.btnTraTim.Location = new System.Drawing.Point(4, 235);
            this.btnTraTim.Name = "btnTraTim";
            this.btnTraTim.Size = new System.Drawing.Size(178, 28);
            this.btnTraTim.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTraTim.Symbol = "";
            this.btnTraTim.TabIndex = 6;
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
            // tblTraTim
            // 
            this.tblTraTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblTraTim.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTraTim.ColumnCount = 1;
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTraTim.Controls.Add(this.dataGridViewCT, 0, 1);
            this.tblTraTim.Controls.Add(this.dataGridViewPX, 0, 0);
            this.tblTraTim.Controls.Add(this.tblButton, 0, 2);
            this.tblTraTim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTraTim.ForeColor = System.Drawing.Color.Black;
            this.tblTraTim.Location = new System.Drawing.Point(188, 0);
            this.tblTraTim.Name = "tblTraTim";
            this.tblTraTim.RowCount = 3;
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblTraTim.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblTraTim.Size = new System.Drawing.Size(820, 621);
            this.tblTraTim.TabIndex = 1;
            // 
            // dataGridViewCT
            // 
            this.dataGridViewCT.AllowUserToAddRows = false;
            this.dataGridViewCT.AllowUserToDeleteRows = false;
            this.dataGridViewCT.AutoGenerateColumns = false;
            this.dataGridViewCT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phieuXuatIDDataGridViewTextBoxColumn1,
            this.maDauMoDataGridViewTextBoxColumn,
            this.tenDauMoDataGridViewTextBoxColumn,
            this.donViTinhDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.tyTrongDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2,
            this.soLuongDataGridViewTextBoxColumn,
            this.soLuongVCFDataGridViewTextBoxColumn,
            this.phieuNhapIDDataGridViewTextBoxColumn,
            this.donGiaDataGridViewTextBoxColumn,
            this.bangGiaIDDataGridViewTextBoxColumn,
            this.thanhTienDataGridViewTextBoxColumn});
            this.dataGridViewCT.DataSource = this.bsPhieuXuatCT;
            this.dataGridViewCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCT.Location = new System.Drawing.Point(4, 354);
            this.dataGridViewCT.Name = "dataGridViewCT";
            this.dataGridViewCT.ReadOnly = true;
            this.dataGridViewCT.RowHeadersVisible = false;
            this.dataGridViewCT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCT.Size = new System.Drawing.Size(812, 226);
            this.dataGridViewCT.TabIndex = 1;
            // 
            // dataGridViewPX
            // 
            this.dataGridViewPX.AllowUserToAddRows = false;
            this.dataGridViewPX.AllowUserToDeleteRows = false;
            this.dataGridViewPX.AutoGenerateColumns = false;
            this.dataGridViewPX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewPX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPX.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phieuXuatIDDataGridViewTextBoxColumn,
            this.ngayXuatDataGridViewTextBoxColumn,
            this.loaiPhieuDataGridViewTextBoxColumn,
            this.maTramNLDataGridViewTextBoxColumn,
            this.tenTramNLDataGridViewTextBoxColumn,
            this.dauMayIDDataGridViewTextBoxColumn,
            this.loaiMayIDDataGridViewTextBoxColumn,
            this.soChungTuDataGridViewTextBoxColumn,
            this.nguoiNhanDataGridViewTextBoxColumn,
            this.lyDoDataGridViewTextBoxColumn,
            this.khoaSoDataGridViewCheckBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.createdNameDataGridViewTextBoxColumn,
            this.modifyDateDataGridViewTextBoxColumn,
            this.modifyByDataGridViewTextBoxColumn,
            this.modifyNameDataGridViewTextBoxColumn});
            this.dataGridViewPX.DataSource = this.bsPhieuXuat;
            this.dataGridViewPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPX.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewPX.Name = "dataGridViewPX";
            this.dataGridViewPX.ReadOnly = true;
            this.dataGridViewPX.RowHeadersVisible = false;
            this.dataGridViewPX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPX.Size = new System.Drawing.Size(812, 343);
            this.dataGridViewPX.TabIndex = 0;
            this.dataGridViewPX.SelectionChanged += new System.EventHandler(this.dataGridViewPX_SelectionChanged);
            // 
            // tblButton
            // 
            this.tblButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tblButton.ColumnCount = 8;
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButton.Controls.Add(this.lblChiTietSum, 7, 0);
            this.tblButton.Controls.Add(this.lblChiTietCount, 0, 0);
            this.tblButton.Controls.Add(this.btnThem, 1, 0);
            this.tblButton.Controls.Add(this.btnSua, 2, 0);
            this.tblButton.Controls.Add(this.btnXoa, 3, 0);
            this.tblButton.Controls.Add(this.btnMo, 6, 0);
            this.tblButton.Controls.Add(this.btnIn, 4, 0);
            this.tblButton.Controls.Add(this.btnKhoa, 5, 0);
            this.tblButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButton.ForeColor = System.Drawing.Color.Black;
            this.tblButton.Location = new System.Drawing.Point(2, 585);
            this.tblButton.Margin = new System.Windows.Forms.Padding(1);
            this.tblButton.Name = "tblButton";
            this.tblButton.RowCount = 1;
            this.tblButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButton.Size = new System.Drawing.Size(816, 34);
            this.tblButton.TabIndex = 2;
            // 
            // lblChiTietSum
            // 
            this.lblChiTietSum.AutoSize = true;
            this.lblChiTietSum.BackColor = System.Drawing.Color.White;
            this.lblChiTietSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChiTietSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietSum.ForeColor = System.Drawing.Color.Black;
            this.lblChiTietSum.Location = new System.Drawing.Point(711, 0);
            this.lblChiTietSum.Name = "lblChiTietSum";
            this.lblChiTietSum.Size = new System.Drawing.Size(102, 34);
            this.lblChiTietSum.TabIndex = 77;
            this.lblChiTietSum.Text = "Tổng tiền phiếu xuất: 0";
            this.lblChiTietSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChiTietCount
            // 
            this.lblChiTietCount.AutoSize = true;
            this.lblChiTietCount.BackColor = System.Drawing.Color.White;
            this.lblChiTietCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChiTietCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietCount.ForeColor = System.Drawing.Color.Black;
            this.lblChiTietCount.Location = new System.Drawing.Point(3, 0);
            this.lblChiTietCount.Name = "lblChiTietCount";
            this.lblChiTietCount.Size = new System.Drawing.Size(102, 34);
            this.lblChiTietCount.TabIndex = 76;
            this.lblChiTietCount.Text = "Tổng số chi tiết: 0";
            this.lblChiTietCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnThem.Size = new System.Drawing.Size(90, 32);
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
            this.btnSua.Size = new System.Drawing.Size(90, 32);
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
            this.btnXoa.Size = new System.Drawing.Size(90, 32);
            this.btnXoa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXoa.Symbol = "";
            this.btnXoa.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnMo
            // 
            this.btnMo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMo.Location = new System.Drawing.Point(613, 1);
            this.btnMo.Margin = new System.Windows.Forms.Padding(1);
            this.btnMo.Name = "btnMo";
            this.btnMo.Size = new System.Drawing.Size(90, 32);
            this.btnMo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMo.Symbol = "";
            this.btnMo.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMo.TabIndex = 5;
            this.btnMo.Text = "&Mở khóa";
            this.btnMo.Click += new System.EventHandler(this.btnMo_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnIn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Location = new System.Drawing.Point(413, 1);
            this.btnIn.Margin = new System.Windows.Forms.Padding(1);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(90, 32);
            this.btnIn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnIn.Symbol = "";
            this.btnIn.SymbolColor = System.Drawing.Color.Blue;
            this.btnIn.TabIndex = 3;
            this.btnIn.Text = "&In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnKhoa
            // 
            this.btnKhoa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKhoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnKhoa.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhoa.Location = new System.Drawing.Point(513, 1);
            this.btnKhoa.Margin = new System.Windows.Forms.Padding(1);
            this.btnKhoa.Name = "btnKhoa";
            this.btnKhoa.Size = new System.Drawing.Size(90, 32);
            this.btnKhoa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnKhoa.Symbol = "";
            this.btnKhoa.SymbolColor = System.Drawing.Color.Gray;
            this.btnKhoa.TabIndex = 4;
            this.btnKhoa.Text = "&Khóa";
            this.btnKhoa.Click += new System.EventHandler(this.btnKhoa_Click);
            // 
            // bsPhieuXuatCT
            // 
            this.bsPhieuXuatCT.DataSource = typeof(CBClient.BLLTypes.NL_PhieuXuatCT);
            // 
            // bsPhieuXuat
            // 
            this.bsPhieuXuat.DataSource = typeof(CBClient.BLLTypes.NL_PhieuXuat);
            // 
            // sdNgayKT
            // 
            this.sdNgayKT.BackColor = System.Drawing.Color.White;
            this.sdNgayKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayKT.ForeColor = System.Drawing.Color.Black;
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
            this.sdNgayBD.BackColor = System.Drawing.Color.White;
            this.sdNgayBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdNgayBD.ForeColor = System.Drawing.Color.Black;
            this.sdNgayBD.IsTime = false;
            this.sdNgayBD.Location = new System.Drawing.Point(77, 34);
            this.sdNgayBD.Name = "sdNgayBD";
            this.sdNgayBD.Size = new System.Drawing.Size(105, 23);
            this.sdNgayBD.TabIndex = 0;
            this.sdNgayBD.Text = "20.06.2023";
            this.sdNgayBD.Value = new System.DateTime(2023, 6, 20, 0, 0, 0, 0);
            // 
            // phieuXuatIDDataGridViewTextBoxColumn
            // 
            this.phieuXuatIDDataGridViewTextBoxColumn.DataPropertyName = "PhieuXuatID";
            this.phieuXuatIDDataGridViewTextBoxColumn.HeaderText = "PhieuXuatID";
            this.phieuXuatIDDataGridViewTextBoxColumn.Name = "phieuXuatIDDataGridViewTextBoxColumn";
            this.phieuXuatIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.phieuXuatIDDataGridViewTextBoxColumn.Width = 92;
            // 
            // ngayXuatDataGridViewTextBoxColumn
            // 
            this.ngayXuatDataGridViewTextBoxColumn.DataPropertyName = "NgayXuat";
            this.ngayXuatDataGridViewTextBoxColumn.HeaderText = "NgayXuat";
            this.ngayXuatDataGridViewTextBoxColumn.Name = "ngayXuatDataGridViewTextBoxColumn";
            this.ngayXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayXuatDataGridViewTextBoxColumn.Width = 79;
            // 
            // loaiPhieuDataGridViewTextBoxColumn
            // 
            this.loaiPhieuDataGridViewTextBoxColumn.DataPropertyName = "LoaiPhieu";
            this.loaiPhieuDataGridViewTextBoxColumn.HeaderText = "LoaiPhieu";
            this.loaiPhieuDataGridViewTextBoxColumn.Name = "loaiPhieuDataGridViewTextBoxColumn";
            this.loaiPhieuDataGridViewTextBoxColumn.ReadOnly = true;
            this.loaiPhieuDataGridViewTextBoxColumn.Width = 79;
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
            // soChungTuDataGridViewTextBoxColumn
            // 
            this.soChungTuDataGridViewTextBoxColumn.DataPropertyName = "SoChungTu";
            this.soChungTuDataGridViewTextBoxColumn.HeaderText = "SoChungTu";
            this.soChungTuDataGridViewTextBoxColumn.Name = "soChungTuDataGridViewTextBoxColumn";
            this.soChungTuDataGridViewTextBoxColumn.ReadOnly = true;
            this.soChungTuDataGridViewTextBoxColumn.Width = 89;
            // 
            // nguoiNhanDataGridViewTextBoxColumn
            // 
            this.nguoiNhanDataGridViewTextBoxColumn.DataPropertyName = "NguoiNhan";
            this.nguoiNhanDataGridViewTextBoxColumn.HeaderText = "NguoiNhan";
            this.nguoiNhanDataGridViewTextBoxColumn.Name = "nguoiNhanDataGridViewTextBoxColumn";
            this.nguoiNhanDataGridViewTextBoxColumn.ReadOnly = true;
            this.nguoiNhanDataGridViewTextBoxColumn.Width = 86;
            // 
            // lyDoDataGridViewTextBoxColumn
            // 
            this.lyDoDataGridViewTextBoxColumn.DataPropertyName = "LyDo";
            this.lyDoDataGridViewTextBoxColumn.HeaderText = "LyDo";
            this.lyDoDataGridViewTextBoxColumn.Name = "lyDoDataGridViewTextBoxColumn";
            this.lyDoDataGridViewTextBoxColumn.ReadOnly = true;
            this.lyDoDataGridViewTextBoxColumn.Width = 57;
            // 
            // khoaSoDataGridViewCheckBoxColumn
            // 
            this.khoaSoDataGridViewCheckBoxColumn.DataPropertyName = "KhoaSo";
            this.khoaSoDataGridViewCheckBoxColumn.HeaderText = "KhoaSo";
            this.khoaSoDataGridViewCheckBoxColumn.Name = "khoaSoDataGridViewCheckBoxColumn";
            this.khoaSoDataGridViewCheckBoxColumn.ReadOnly = true;
            this.khoaSoDataGridViewCheckBoxColumn.Width = 51;
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
            // phieuXuatIDDataGridViewTextBoxColumn1
            // 
            this.phieuXuatIDDataGridViewTextBoxColumn1.DataPropertyName = "PhieuXuatID";
            this.phieuXuatIDDataGridViewTextBoxColumn1.HeaderText = "PhieuXuatID";
            this.phieuXuatIDDataGridViewTextBoxColumn1.Name = "phieuXuatIDDataGridViewTextBoxColumn1";
            this.phieuXuatIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // maDauMoDataGridViewTextBoxColumn
            // 
            this.maDauMoDataGridViewTextBoxColumn.DataPropertyName = "MaDauMo";
            this.maDauMoDataGridViewTextBoxColumn.HeaderText = "MaDauMo";
            this.maDauMoDataGridViewTextBoxColumn.Name = "maDauMoDataGridViewTextBoxColumn";
            this.maDauMoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenDauMoDataGridViewTextBoxColumn
            // 
            this.tenDauMoDataGridViewTextBoxColumn.DataPropertyName = "TenDauMo";
            this.tenDauMoDataGridViewTextBoxColumn.HeaderText = "TenDauMo";
            this.tenDauMoDataGridViewTextBoxColumn.Name = "tenDauMoDataGridViewTextBoxColumn";
            this.tenDauMoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // donViTinhDataGridViewTextBoxColumn
            // 
            this.donViTinhDataGridViewTextBoxColumn.DataPropertyName = "DonViTinh";
            this.donViTinhDataGridViewTextBoxColumn.HeaderText = "DonViTinh";
            this.donViTinhDataGridViewTextBoxColumn.Name = "donViTinhDataGridViewTextBoxColumn";
            this.donViTinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NhietDo";
            this.dataGridViewTextBoxColumn1.HeaderText = "NhietDo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // tyTrongDataGridViewTextBoxColumn
            // 
            this.tyTrongDataGridViewTextBoxColumn.DataPropertyName = "TyTrong";
            this.tyTrongDataGridViewTextBoxColumn.HeaderText = "TyTrong";
            this.tyTrongDataGridViewTextBoxColumn.Name = "tyTrongDataGridViewTextBoxColumn";
            this.tyTrongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VCF";
            this.dataGridViewTextBoxColumn2.HeaderText = "VCF";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // soLuongDataGridViewTextBoxColumn
            // 
            this.soLuongDataGridViewTextBoxColumn.DataPropertyName = "SoLuong";
            this.soLuongDataGridViewTextBoxColumn.HeaderText = "SoLuong";
            this.soLuongDataGridViewTextBoxColumn.Name = "soLuongDataGridViewTextBoxColumn";
            this.soLuongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongVCFDataGridViewTextBoxColumn
            // 
            this.soLuongVCFDataGridViewTextBoxColumn.DataPropertyName = "SoLuongVCF";
            this.soLuongVCFDataGridViewTextBoxColumn.HeaderText = "SoLuongVCF";
            this.soLuongVCFDataGridViewTextBoxColumn.Name = "soLuongVCFDataGridViewTextBoxColumn";
            this.soLuongVCFDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phieuNhapIDDataGridViewTextBoxColumn
            // 
            this.phieuNhapIDDataGridViewTextBoxColumn.DataPropertyName = "PhieuNhapID";
            this.phieuNhapIDDataGridViewTextBoxColumn.HeaderText = "PhieuNhapID";
            this.phieuNhapIDDataGridViewTextBoxColumn.Name = "phieuNhapIDDataGridViewTextBoxColumn";
            this.phieuNhapIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // donGiaDataGridViewTextBoxColumn
            // 
            this.donGiaDataGridViewTextBoxColumn.DataPropertyName = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.HeaderText = "DonGia";
            this.donGiaDataGridViewTextBoxColumn.Name = "donGiaDataGridViewTextBoxColumn";
            this.donGiaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bangGiaIDDataGridViewTextBoxColumn
            // 
            this.bangGiaIDDataGridViewTextBoxColumn.DataPropertyName = "BangGiaID";
            this.bangGiaIDDataGridViewTextBoxColumn.HeaderText = "BangGiaID";
            this.bangGiaIDDataGridViewTextBoxColumn.Name = "bangGiaIDDataGridViewTextBoxColumn";
            this.bangGiaIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // thanhTienDataGridViewTextBoxColumn
            // 
            this.thanhTienDataGridViewTextBoxColumn.DataPropertyName = "ThanhTien";
            this.thanhTienDataGridViewTextBoxColumn.HeaderText = "ThanhTien";
            this.thanhTienDataGridViewTextBoxColumn.Name = "thanhTienDataGridViewTextBoxColumn";
            this.thanhTienDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // PhieuXuatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.tblTraTim);
            this.Controls.Add(this.ExpTraTim);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "PhieuXuatForm";
            this.Text = "DANH SÁCH PHIẾU XUẤT";
            this.Load += new System.EventHandler(this.PhieuXuatForm_Load);
            this.ExpTraTim.ResumeLayout(false);
            this.ExpTraTim.PerformLayout();
            this.tblTraTim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPX)).EndInit();
            this.tblButton.ResumeLayout(false);
            this.tblButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhieuXuatCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhieuXuat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel ExpTraTim;
        private DevComponents.DotNetBar.ButtonX btnTraTim;
        private System.Windows.Forms.Label lblTableCount;
        private System.Windows.Forms.TableLayoutPanel tblTraTim;
        private System.Windows.Forms.BindingSource bsPhieuXuat;       
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiMayTT;
        private Controls.SmartDate sdNgayKT;
        private Controls.SmartDate sdNgayBD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTramNLTT;        
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDauMay;
        private System.Windows.Forms.BindingSource bsPhieuXuatCT;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhieuXuat;
        private System.Windows.Forms.DataGridView dataGridViewCT;
        private System.Windows.Forms.DataGridView dataGridViewPX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghiChuDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tblButton;
        private System.Windows.Forms.Label lblChiTietSum;
        private System.Windows.Forms.Label lblChiTietCount;
        private DevComponents.DotNetBar.ButtonX btnThem;
        private DevComponents.DotNetBar.ButtonX btnSua;
        private DevComponents.DotNetBar.ButtonX btnXoa;
        private DevComponents.DotNetBar.ButtonX btnMo;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.ButtonX btnKhoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn nhietDoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vCFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phieuXuatIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maDauMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDauMoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tyTrongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongVCFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phieuNhapIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bangGiaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thanhTienDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phieuXuatIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiPhieuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTramNLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenTramNLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dauMayIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiMayIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soChungTuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nguoiNhanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lyDoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn khoaSoDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifyNameDataGridViewTextBoxColumn;
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.HaNoi
{
    public partial class HNNLDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        DataTable dt = new DataTable();
        List<HNNLDinhMuc> listHNNLDinhMuc = new List<HNNLDinhMuc>();
        public HNNLDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            
        }
        private void HNNLDinhMucForm_Load(object sender, EventArgs e)
        {
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForNumber(txtSTT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMin);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMax);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMuc);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtHeSo);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtHeSoC);

            //txtGaXPName.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            //txtGaXPName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtGaXPName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;           

            var loaiMay = (from ct in AppGlobal.DMLoaimayList
                           select new
                           {
                               MaLM = ct.LoaiMayId,
                               TenLM = ct.LoaiMayName
                           }).ToList();

            var loaiMayTT = loaiMay.ToList();
            loaiMayTT.Add(new { MaLM = "ALL", TenLM = "Tất cả các loại máy" });
            var loaiMayTT1 = loaiMayTT.OrderBy(x => x.MaLM).ToList();
            cboLoaiMayTT.DataSource = loaiMayTT1;
            cboLoaiMayTT.DisplayMember = "TenLM";
            cboLoaiMayTT.ValueMember = "MaLM";
            cboLoaiMayTT.SelectedIndex = 0;

            var loaiMayAdd = loaiMay.OrderBy(f => f.MaLM).ToList();
            cboLoaiMay.DataSource = loaiMayAdd;
            cboLoaiMay.DisplayMember = "TenLM";
            cboLoaiMay.ValueMember = "MaLM";
            cboLoaiMay.SelectedIndex = -1;

            sdNgayBD.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            sdNgayKT.Value = DateTime.Today;

            ShowControl(false);
        }

        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }

        private void fnTraTim()
        {
            try
            {
                bsNLDinhMuc.DataSource = null;
                progressBar1.Value = 0;
                base.Cursor = Cursors.WaitCursor;
                DateTime ngaKT = sdNgayKT.Value.AddDays(1);
                string data = "?NgayBD=" + sdNgayBD.Value.ToString();
                data += "&NgayKT=" + ngaKT.ToString();
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();
                data += "&LoaiTau=" + txtLoaiTauTT.Text.Trim();
                var query = HttpHelper.GetList<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLDinhMuc" + data)
                   .OrderBy(x => x.LoaiMayID).ThenBy(x => x.NgayHL).ToList();
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                listHNNLDinhMuc=(from x in query
                                                    group x by new { x.LoaiMayID, x.CongTac, x.KhuDoan,x.LoaiTau, x.TanMin, x.TanMax } into g
                                                    select new HNNLDinhMuc
                                                    {
                                                        ID = g.LastOrDefault().ID,
                                                        LoaiMayID = g.Key.LoaiMayID,
                                                        STT=g.LastOrDefault().STT,                                                        
                                                        TanMin = g.Key.TanMin,
                                                        TanMax = g.Key.TanMax,
                                                        DinhMuc = g.LastOrDefault().DinhMuc,
                                                        HeSo = g.LastOrDefault().HeSo,
                                                        HeSoC=g.LastOrDefault().HeSoC,
                                                        DonVi = g.LastOrDefault().DonVi,
                                                        NgayHL = g.LastOrDefault().NgayHL,
                                                        CongTac = g.Key.CongTac,
                                                        KhuDoan = g.Key.KhuDoan,
                                                        LoaiTau = g.Key.LoaiTau,
                                                        Createddate = g.LastOrDefault().Createddate,
                                                        Createdby = g.LastOrDefault().Createdby,
                                                        CreatedName = g.LastOrDefault().CreatedName,
                                                        Modifydate = g.LastOrDefault().Modifydate,
                                                        Modifyby = g.LastOrDefault().Modifyby,
                                                        ModifyName = g.LastOrDefault().ModifyName
                                                    }).ToList();
                bsNLDinhMuc.DataSource = listHNNLDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHNNLDinhMuc.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsNLDinhMuc.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboLoaiMay.Enabled = b;
            txtSTT.Enabled = b;          
            txtTanMin.Enabled = b;
            txtTanMax.Enabled = b;            
            txtDinhMuc.Enabled = b;
            txtHeSo.Enabled = b;
            txtHeSoC.Enabled = b;
            txtDonVi.Enabled = b;
            sdNgayHL.Enabled = b;
            txtCongTac.Enabled = b;
            txtKhuDoan.Enabled = b;
            txtLoaiTau.Enabled = b;
            btnThem.Enabled = !b;
            if (b == false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoaAll.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnExport.Enabled = dataGridView1.CurrentRow == null ? false : true;
            }
            else
            {
                btnSua.Enabled = !b;
                btnXoa.Enabled = !b;
                btnXoaAll.Enabled = !b;
                btnExport.Enabled = !b;
            }
            btnLuu.Enabled = b;
            btnHuy.Enabled = b;
        }

        private void ClearControl()
        {
            txtID.Text="0";
            //cboLoaiMay.SelectedIndex = -1;
            //txtCongTac.ResetText();
            //txtKhuDoan.ResetText();            
            txtTanMin.ResetText();
            txtTanMax.ResetText();           
            txtDinhMuc.ResetText();
            txtHeSo.ResetText();           
            //txtHeSoC.ResetText();           
        }

        private void BindControl()
        {
            HNNLDinhMuc dm = bsNLDinhMuc.Current as HNNLDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                txtSTT.Text = dm.STT.ToString();               
                txtTanMin.Text = dm.TanMin.ToString();
                txtTanMax.Text = dm.TanMax.ToString();
                txtDinhMuc.Text = dm.DinhMuc.ToString();
                txtHeSo.Text = dm.HeSo.ToString();
                txtHeSoC.Text = dm.HeSoC.ToString();
                txtDonVi.Text = dm.DonVi;                
                sdNgayHL.Value = dm.NgayHL;
                txtCongTac.Text = dm.CongTac;
                txtKhuDoan.Text = dm.KhuDoan;
                txtLoaiTau.Text = dm.LoaiTau;
            }
        }

        private HNNLDinhMuc BindObject()
        {
            HNNLDinhMuc dm = new HNNLDinhMuc();
            if (!bThem)
                dm = bsNLDinhMuc.Current as HNNLDinhMuc;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();
            dm.STT = String.IsNullOrWhiteSpace(txtSTT.Text) ? (short)0 : short.Parse(txtSTT.Text);            
            dm.TanMin = String.IsNullOrWhiteSpace(txtTanMin.Text) ? 0 : decimal.Parse(txtTanMin.Text);
            dm.TanMax = String.IsNullOrWhiteSpace(txtTanMax.Text) ? 0 : decimal.Parse(txtTanMax.Text);
            dm.DinhMuc = String.IsNullOrWhiteSpace(txtDinhMuc.Text) ? 0 : decimal.Parse(txtDinhMuc.Text);
            dm.HeSo = String.IsNullOrWhiteSpace(txtHeSo.Text) ? 0 : decimal.Parse(txtHeSo.Text);
            dm.HeSoC = String.IsNullOrWhiteSpace(txtHeSoC.Text) ? 0 : decimal.Parse(txtHeSoC.Text);
            dm.DonVi = txtDonVi.Text;
            dm.NgayHL = sdNgayHL.Value;            
            dm.CongTac = txtCongTac.Text;
            dm.KhuDoan = txtKhuDoan.Text;
            dm.LoaiTau = txtLoaiTau.Text;
            return dm;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            bsNLDinhMuc.MoveLast();
            ClearControl();
            bThem = true;
            ShowControl(true);
            cboLoaiMay.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần sửa.");
                return;
            }
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            BindControl();
            ShowControl(true);           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần xóa.");
                return;
            }
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            HNNLDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNDeleteNLDinhMuc?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsNLDinhMuc.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }
        private async void btnXoaAll_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            if (Library.DialogHelper.Confirm("Xóa những định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                int i = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = listHNNLDinhMuc.Count;
                foreach (HNNLDinhMuc dm in listHNNLDinhMuc)
                {
                    if (dm != null)
                    {
                        progressBar1.Value = i;
                        i++;
                        lblTableCount.Text = "Xóa định mức: " + dm.LoaiMayID + ": Khu đoạn: " + dm.KhuDoan + " .Của: " + i + "/" + listHNNLDinhMuc.Count.ToString("N0");
                        try
                        {
                            dm.Modifyby = AppGlobal.User.Username;
                            dm.ModifyName = AppGlobal.User.FullName;
                            var opStatus = await HttpHelper.Delete<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNDeleteNLDinhMuc?id=" + dm.ID);
                            if (opStatus == null) throw new Exception("Lỗi xóa định mức: " + dm.LoaiMayID + ": Khu đoạn: " + dm.KhuDoan);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            continue;
                        }
                    }
                }
                fnTraTim();
            }
        }
        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                HNNLDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNPostNLDinhMuc", dm);                   
                    dm.ID = objInsert.ID;
                    bsNLDinhMuc.Add(dm);
                    bsNLDinhMuc.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNPutNLDinhMuc?id=" + dm.ID, dm);
                    bsNLDinhMuc.EndEdit();
                }
            }
            catch (Exception ex)
            {
                Library.DialogHelper.Error(ex.Message);
            }
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            BindControl();
        }
        private void sdNgayKT_Validated(object sender, EventArgs e)
        {
            if (sdNgayKT.Value < sdNgayBD.Value)
                sdNgayKT.Value = DateTime.Today;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dataGridView1);
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            //fdlg.InitialDirectory = @"c:\";
            fdlg.InitialDirectory = Application.StartupPath;
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel 97-2003(*.xls)|*.xls|Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                Application.DoEvents();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Xin vui lòng chọn tập tin excel cần import");
                return;
            }
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Import();
                Application.DoEvents();
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                btnInsert.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Import()
        {
            if (txtFileName.Text.Trim() != string.Empty)
            {
                try
                {
                    //Nạp dữ liệu
                    string Extension = Path.GetExtension(txtFileName.Text).ToLower();
                    dt = new DataTable();
                    bsNLDinhMuc.DataSource = null;
                    dt = ReadDataFromExcelFile(txtFileName.Text, Extension);
                    dt.AcceptChanges();
                    btnInsert.Enabled = dt.Rows.Count > 0 ? true : false;
                    lblTableCount.Text = "Tổng số bản ghi:" + dt.Rows.Count.ToString("N0");
                    bsNLDinhMuc.DataSource = dt.DefaultView;
                    //dataGridView1.DataSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private DataTable ReadDataFromExcelFile(string fileName, string Extension)
        {
            string connectionString = string.Empty;
            string connectionString97_03 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName.Trim() + ";Extended Properties = Excel 8.0";
            string connectionString07 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName.Trim() + ";Extended Properties = Excel 12.0 Xml";
            connectionString = Extension == ".xls" ? connectionString97_03 : connectionString07;
            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                // Mở kết nối
                oledbConn.Open();
                // Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + tableName + "$]", oledbConn);

                // Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
                DataSet ds = new DataSet();

                // Đổ đữ liệu từ tập excel vào DataSet
                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;
        }

        private async void btnInsert_Click(object sender, EventArgs e)
        {
            HNNLDinhMuc dm = new HNNLDinhMuc();
            if (dt.Rows.Count > 0)
            {
                int stt = 0;
                base.Cursor = Cursors.WaitCursor;
                try
                {
                    foreach (DataRow ct in dt.Rows)
                    {
                        dm = new HNNLDinhMuc();
                        dm.ID = 0;
                        dm.LoaiMayID = ct["LoaiMayID"].ToString();
                        dm.STT= String.IsNullOrWhiteSpace(ct["STT"].ToString()) ? (short)0 : short.Parse(ct["STT"].ToString());                        
                        dm.TanMin = String.IsNullOrWhiteSpace(ct["TanMin"].ToString()) ? 0 : decimal.Parse(ct["TanMin"].ToString());
                        dm.TanMax = String.IsNullOrWhiteSpace(ct["TanMax"].ToString()) ? 0 : decimal.Parse(ct["TanMax"].ToString());
                        dm.DinhMuc = String.IsNullOrWhiteSpace(ct["DinhMuc"].ToString()) ? 0 : decimal.Parse(ct["DinhMuc"].ToString());
                        dm.HeSo = String.IsNullOrWhiteSpace(ct["HeSo"].ToString()) ? 0 : decimal.Parse(ct["HeSo"].ToString());
                        dm.HeSoC = String.IsNullOrWhiteSpace(ct["HeSoC"].ToString()) ? 0 : decimal.Parse(ct["HeSoC"].ToString());
                        dm.DonVi = ct["DonVi"].ToString();
                        dm.NgayHL = DateTime.Parse(ct["NgayHL"].ToString());                        
                        dm.CongTac = ct["CongTac"].ToString();
                        dm.KhuDoan = ct["KhuDoan"].ToString();
                        dm.LoaiTau = ct["LoaiTau"].ToString();
                        dm.Createddate = DateTime.Now;
                        dm.Createdby = AppGlobal.User.Username;
                        dm.CreatedName = AppGlobal.User.FullName;
                        dm.Modifydate = dm.Createddate;
                        dm.Modifyby = dm.Createdby;
                        dm.ModifyName = dm.CreatedName;
                        var objInsert = await HttpHelper.Post<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNPostNLDinhMuc", dm);
                        dm.ID = objInsert.ID;
                        stt++;
                    }
                    base.Cursor = Cursors.Default;
                    MessageBox.Show("Nạp thành công: " + stt.ToString() + "/" + dt.Rows.Count.ToString());
                }
                catch (Exception ex)
                {
                    base.Cursor = Cursors.Default;
                    MessageBox.Show("Lỗi: " + ex.Message + " .Nạp thành công: " + stt.ToString() + "/" + dt.Rows.Count.ToString());

                }
            }
        }

       
    }
}

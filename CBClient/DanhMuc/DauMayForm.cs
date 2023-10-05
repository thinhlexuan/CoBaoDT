using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.DanhMuc
{
    public partial class DauMayForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        int _ID = 0;
        public DauMayForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
           

            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();            
            //if (AppGlobal.User.MaDVQL != "TCT")
            //{
            //    if (AppGlobal.User.MaDVQL == "YV")
            //        donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
            //    else if (AppGlobal.User.MaDVQL == "DN")
            //        donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
            //    else
            //        donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            //}

            var ctSHTT = donViTT.ToList();
            cboCTSHTT.DataSource = ctSHTT;
            cboCTSHTT.DisplayMember = "TenDV";
            cboCTSHTT.ValueMember = "MaDV";
            cboCTSHTT.SelectedIndex = 0;

            var ctQLTT = donViTT.ToList();
            cboCTQLTT.DataSource = ctQLTT;
            cboCTQLTT.DisplayMember = "TenDV";
            cboCTQLTT.ValueMember = "MaDV";
            cboCTQLTT.SelectedIndex = 0;

            var donVi = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenDV
                           }).OrderBy(x => x.TenDV).ToList();

            var ctSH = donVi.ToList();
            cboCTSH.DataSource = ctSH;
            cboCTSH.DisplayMember = "TenDV";
            cboCTSH.ValueMember = "MaDV";
            cboCTSH.SelectedIndex = -1;

            var ctQL = donVi.ToList();
            cboCTQL.DataSource = ctQL;
            cboCTQL.DisplayMember = "TenDV";
            cboCTQL.ValueMember = "MaDV";
            cboCTQL.SelectedIndex = -1;

            ShowControl(false);
            FnAutoComplete();
        }      
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }

        private void fnTraTim()
        {
            try
            {
                bsDauMay.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?maDVSH=" + cboCTSHTT.SelectedValue.ToString();
                data += "&maDVQL=" + cboCTQLTT.SelectedValue.ToString();
                data += "&loaiMay=" + txtLoaiMayTT.Text.Trim();
                data += "&dauMay=" + txtDauMayTT.Text.Trim();
                List<ViewDauMay> listDauMay = HttpHelper.GetList<ViewDauMay>(Configuration.UrlCBApi + "api/DauMays/GetViewDauMay" + data)
                   .OrderBy(x=>x.LoaiMayID).ThenBy(x => x.DauMayID).ThenBy(x=>x.NgayHL).ToList();                
                if (listDauMay.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsDauMay.DataSource = listDauMay;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listDauMay.Count.ToString("N0");              
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsDauMay.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void FnAutoComplete()
        {
            txtDauMay.AutoCompleteCustomSource = AppGlobal.MaDauMayAutoComplate;
            txtDauMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDauMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtLoaiMay.AutoCompleteCustomSource = AppGlobal.MaLoaiMayAutoComplate;
            txtLoaiMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtLoaiMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtLoaiMay.Enabled = b;
            txtDauMay.Enabled = b;
            cboCTSH.Enabled = b;
            cboCTQL.Enabled = b;
            sdNgayHL.Enabled = b;            
            chkActive.Enabled = b;
            btnThem.Enabled = !b;
            if (b == false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnExport.Enabled = dataGridView1.CurrentRow == null ? false : true;
            }
            else
            {
                btnSua.Enabled = !b;
                btnXoa.Enabled = !b;
                btnExport.Enabled = !b;
            }
            btnLuu.Enabled = b;
            btnHuy.Enabled = b;
        }

        private void ClearControl()
        {
            _ID = 0;
            txtLoaiMay.ResetText();
            txtDauMay.ResetText();
            cboCTSH.SelectedIndex = -1;
            cboCTQL.SelectedIndex = -1;
            sdNgayHL.Value=DateTime.Today;            
            chkActive.Checked = true;
        }

        private void BindControl()
        {
            ViewDauMay dm = bsDauMay.Current as ViewDauMay;
            if (dm != null)
            {
                _ID = dm.ID;
                txtLoaiMay.Text = dm.LoaiMayID;
                txtDauMay.Text = dm.DauMayID;
                cboCTSH.SelectedValue = dm.MaCTSoHuu;
                cboCTSH.Text = dm.TenCTSoHuu;
                cboCTQL.SelectedValue = dm.MaCTQuanLy;
                cboCTQL.Text = dm.TenCTQuanLy;
                sdNgayHL.Value = (DateTime)dm.NgayHL;                
                chkActive.Checked = dm.Active;
            }
        }

        private ViewDauMay BindViewObject()
        {
            ViewDauMay vdm = new ViewDauMay();
            if (!bThem)
                vdm = bsDauMay.Current as ViewDauMay;
            vdm.ID = _ID;
            vdm.LoaiMayID = txtLoaiMay.Text;
            vdm.DauMayID = txtDauMay.Text;
            vdm.MaCTSoHuu = cboCTSH.SelectedValue.ToString();
            vdm.TenCTSoHuu = cboCTSH.Text;
            vdm.MaCTQuanLy = cboCTQL.SelectedValue.ToString();
            vdm.TenCTQuanLy = cboCTQL.Text;
            vdm.NgayHL = sdNgayHL.Value.Date;
            vdm.Active = chkActive.Checked;
            if (bThem)
            {
                vdm.CreatedDate = DateTime.Now;
                vdm.CreatedBy = AppGlobal.User.Username;
                vdm.CreatedName = AppGlobal.User.FullName;
            }
            vdm.ModifyDate = DateTime.Now;
            vdm.ModifyBy = AppGlobal.User.Username;
            vdm.ModifyName = AppGlobal.User.FullName;
            return vdm;
        }
        private DauMay BindObject(ViewDauMay vdm)
        {           
            DauMay dm = new DauMay();
            dm.ID = vdm.ID;
            dm.LoaiMayID = vdm.LoaiMayID;
            dm.DauMayID = vdm.DauMayID;
            dm.MaCTSoHuu = vdm.MaCTSoHuu;
            dm.MaCTQuanLy = vdm.MaCTQuanLy;
            dm.NgayHL = vdm.NgayHL;
            dm.Active = vdm.Active;
            dm.CreatedDate = vdm.CreatedDate;
            dm.CreatedBy = vdm.CreatedBy;
            dm.CreatedName = vdm.CreatedName;
            dm.ModifyDate = vdm.ModifyDate;
            dm.ModifyBy = vdm.ModifyBy;
            dm.ModifyName = vdm.ModifyName;
            return dm;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            ClearControl();
            bThem = true;
            ShowControl(true);
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
            ViewDauMay dm = bsDauMay.Current as ViewDauMay;
            if (Library.DialogHelper.Confirm("Xóa đầu máy thuộc công ty sở hữu này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<DauMay>(Configuration.UrlCBApi + "api/DauMays/DeleteByID?id=" + dm.ID);
                if (opStatus.Result.ID==dm.ID)
                    bsDauMay.Remove(dm);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                ViewDauMay vdm = BindViewObject();
                DauMay dm = BindObject(vdm);               
                if (bThem)
                {                  
                    var objInsert = await HttpHelper.Post<DauMay>(Configuration.UrlCBApi + "api/DauMays/PostByID", dm);
                    bsDauMay.Add(vdm);
                    bsDauMay.MoveLast();
                }
                else
                {                    
                    var objUpdate = await HttpHelper.Put<DauMay>(Configuration.UrlCBApi + "api/DauMays/PutByID?id=" + _ID, dm);
                    bsDauMay.EndEdit();
                    dataGridView1.Refresh();
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
    }
}

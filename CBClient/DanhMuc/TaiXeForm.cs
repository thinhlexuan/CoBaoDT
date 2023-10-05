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
    public partial class TaiXeForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        int NhanVienID = 0;
        public TaiXeForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            Library.FormHelper.AddKeyPressEventHandlerForNumber(txtMaSo);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtPhoneNumber);

            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();            
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
                else
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            cboDonVi.DataSource = donViTT;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = 0;
            string[] arRays = new string[] { "LTAU", "PLTAU" };
            cboChucVu.Items.AddRange(arRays);
            cboChucVu.SelectedIndex = -1;

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
                bsTaiXe.DataSource = null;
                cboDoiMay.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                if (AppGlobal.User.MaDVQL == "TCT" && AppGlobal.User.MaQH < 3 && cboDonVi.SelectedValue.ToString()!= "TCT")
                {
                    fnCapNhat();
                }
                //Nạp danh sách trạm và đội theo mã đơn vị đầu máy
                string maDV = cboDonVi.SelectedValue.ToString();               
                var donVi = AppGlobal.DMDonviList;
                if (maDV != "TCT")
                {
                    if (maDV == "YV")
                        donVi = donVi.Where(x => x.MaCt == maDV || x.MaCt == "HN").ToList();
                    else if (AppGlobal.User.MaDVQL == "DN")
                        donVi = donVi.Where(x => x.MaCt == maDV || x.MaCt == "SG").ToList();
                    else
                        donVi = donVi.Where(x => x.MaCt == maDV).ToList();
                }
                var donViTT = (from dm in donVi
                               group dm by new { dm.MaDv } into g
                             select new
                             {
                                 MaDV = g.Key.MaDv,
                                 TenDV = g.First().TenDv
                             }).OrderBy(x => x.TenDV).ToList();

                cboDoiMay.DataSource = donViTT;
                cboDoiMay.DisplayMember = "TenDV";
                cboDoiMay.ValueMember = "MaDV";
                cboDoiMay.SelectedIndex = -1;
                //Nạp dữ liệu lên lưới
                string data = "?MaDV=" + maDV;
                data += "&MaNV=" + txtSHTaiXe.Text.Trim();                
                List<ViewDMNhanVien> listTaiXe = HttpHelper.GetList<ViewDMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/GetViewDMNhanVien" + data)
                   .OrderBy(x=>x.MaSo).OrderBy(x => x.MaDV).ToList();                
                if (listTaiXe.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }  
                bsTaiXe.DataSource = listTaiXe;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listTaiXe.Count.ToString("N0");

                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsTaiXe.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }
        private async void fnCapNhat()
        {
            if (DialogHelper.Confirm("Cập nhật lại danh sách tài xế còn thiếu không?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    base.Cursor = Cursors.WaitCursor;                    
                    var DMTaixeList = HttpHelper.GetList<DMTaiXe>(Configuration.UrlCBApi + "api/DanhMucs/GetDMTaiXe?MaDV=" + cboDonVi.SelectedValue.ToString()).Where(x=> x.TaiXeID.Length>=4).ToList();
                    var DMPhoxeList = HttpHelper.GetList<DMPhoXe>(Configuration.UrlCBApi + "api/DanhMucs/GetDMPhoXe?MaDV=" + cboDonVi.SelectedValue.ToString()).Where(x => x.PhoXeID.Length>=4).ToList();
                    string data = "?MaDV=" + cboDonVi.SelectedValue.ToString();
                    data += "&MaNV=";
                    List<ViewDMNhanVien> listTaiXe = HttpHelper.GetList<ViewDMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/GetViewDMNhanVien" + data)
                       .OrderBy(x => x.MaSo).OrderBy(x => x.MaDV).ToList();
                    if (listTaiXe.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu.");

                    }
                    var TaiXeFisrt = listTaiXe.FirstOrDefault();
                    var listTaiXeNotInDMNhanVien = DMTaixeList.Where(x => !listTaiXe.Any(x1 => x1.MaSo == x.TaiXeID)).ToList();
                    if (listTaiXeNotInDMNhanVien.Count > 0)
                    {
                        int stt = 1;
                        foreach (DMTaiXe tx in listTaiXeNotInDMNhanVien)
                        {
                            DMNhanVien nv = new DMNhanVien();
                            nv.NhanVienID = 0;
                            nv.Username = TaiXeFisrt.MaDV+"-"+ stt+"lt";
                            nv.FullName = tx.TaiXeName;
                            nv.ChucVu = "LTAU";
                            nv.MaSo = tx.TaiXeID;
                            nv.Email = nv.Username + "@gmail.com";
                            nv.PhoneNumber = string.Empty;
                            nv.MaDV = TaiXeFisrt.MaDV;
                            nv.IsActive =true;
                            nv.CreatedDate = DateTime.Now;
                            nv.CreatedBy = AppGlobal.User.Username;
                            nv.ModifyDate = nv.CreatedDate;
                            nv.ModifyBy = nv.CreatedBy;
                            var objInsert = await HttpHelper.Post<DMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/PostByID", nv);
                            stt++;
                        }
                    }
                    var listPhoXeNotInDMNhanVien = DMPhoxeList.Where(x => !listTaiXe.Any(x1 => x1.MaSo == x.PhoXeID)).ToList();
                    if (listPhoXeNotInDMNhanVien.Count > 0)
                    {
                        int stt = 1;
                        foreach (DMPhoXe px in listPhoXeNotInDMNhanVien)
                        {
                            DMNhanVien nv = new DMNhanVien();
                            nv.NhanVienID = 0;
                            nv.Username = TaiXeFisrt.MaDV + "-" + stt + "lt";
                            nv.FullName = px.PhoXeName;
                            nv.ChucVu = "PLTAU";
                            nv.MaSo = px.PhoXeID;
                            nv.Email = nv.Username + "@gmail.com";
                            nv.PhoneNumber = string.Empty;
                            nv.MaDV = TaiXeFisrt.MaDV;
                            nv.IsActive = true;
                            nv.CreatedDate = DateTime.Now;
                            nv.CreatedBy = AppGlobal.User.Username;
                            nv.ModifyDate = nv.CreatedDate;
                            nv.ModifyBy = nv.CreatedBy;
                            var objInsert = await HttpHelper.Post<DMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/PostByID", nv);
                            stt++;
                        }
                    }

                    base.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    base.Cursor = Cursors.Default;
                }
            }
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtUserName.Enabled = b;
            txtFullName.Enabled = b;
            cboChucVu.Enabled = b;
            txtMaSo.Enabled = b;
            txtEmail.Enabled = b;
            txtPhoneNumber.Enabled = b;
            cboDoiMay.Enabled = b;
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
            NhanVienID = 0;
            txtUserName.ResetText();
            txtFullName.ResetText();
            cboChucVu.SelectedIndex = -1;
            txtMaSo.ResetText();
            txtEmail.ResetText();
            txtPhoneNumber.ResetText();
            cboDoiMay.SelectedIndex = -1;
            chkActive.Checked = true;
        }

        private void BindControl()
        {
            ViewDMNhanVien nv = bsTaiXe.Current as ViewDMNhanVien;
            if (nv != null)
            {
                NhanVienID = nv.NhanVienID;
                txtUserName.Text = nv.Username;
                txtFullName.Text = nv.FullName;
                cboChucVu.Text = nv.ChucVu;
                txtMaSo.Text = nv.MaSo;
                txtEmail.Text = nv.Email;
                txtPhoneNumber.Text = nv.PhoneNumber;
                cboDoiMay.SelectedValue = nv.MaDV;
                cboDoiMay.Text = nv.TenDV;
                chkActive.Checked = true;
            }
        }

        private DMNhanVien BindObject()
        {
            DMNhanVien nv = new DMNhanVien();
            nv.NhanVienID = NhanVienID;
            nv.Username = txtUserName.Text;
            nv.FullName = txtFullName.Text;
            nv.ChucVu = cboChucVu.Text;
            nv.MaSo = txtMaSo.Text;
            nv.Email = txtEmail.Text;
            nv.PhoneNumber = txtPhoneNumber.Text;
            nv.MaDV = cboDoiMay.SelectedValue.ToString();
            nv.IsActive = chkActive.Checked;
            return nv;
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
            if (Library.DialogHelper.Confirm("Xóa tài xế này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<DMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/DeleteByID?id=" + NhanVienID);
                if (opStatus.Result.NhanVienID==NhanVienID)                    
                    fnTraTim();
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DMNhanVien nv = BindObject();
                nv.CreatedBy = AppGlobal.User.Username;
                nv.CreatedDate = DateTime.Today;
                if (bThem)
                {
                                     
                    nv.ModifyBy = nv.CreatedBy;
                    nv.ModifyDate = nv.CreatedDate;
                    var objInsert = await HttpHelper.Post<DMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/PostByID", nv);
                    fnTraTim();
                }
                else
                {
                    nv.ModifyBy = AppGlobal.User.Username;
                    nv.ModifyDate = DateTime.Today;
                    var objUpdate = await HttpHelper.Put<DMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/PutByID?id=" + NhanVienID, nv);
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

    class DMNhanVien
    {        
        public int NhanVienID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string MaSo { get; set; }
        public string ChucVu { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string MaDV { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
}

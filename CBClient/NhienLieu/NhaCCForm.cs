using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.NhienLieu
{
    public partial class NhaCCForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;       
        public NhaCCForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
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
                bsNhaCC.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?tenNCC=" + txtTenNCCTT.Text.Trim();                             
                List<NL_NhaCC> listNhaCC = HttpHelper.GetList<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLGetNhaCC" + data).ToList();                
                if (listNhaCC.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsNhaCC.DataSource = listNhaCC;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listNhaCC.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsNhaCC.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtID.Enabled = b;
            txtTenTat.Enabled = b;
            txtTenNCC.Enabled = b;
            txtDiaChi.Enabled = b;
            txtDienThoai.Enabled = b;
            txtMst.Enabled = b;
            txtEmail.Enabled = b;
            txtWebsite.Enabled = b;
            txtNganHang.Enabled = b;
            txtTaiKhoan.Enabled = b;
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
            txtID.Text = "0";
            txtTenTat.ResetText();
            txtTenNCC.ResetText();
            txtDiaChi.ResetText();
            txtDienThoai.ResetText();
            txtMst.ResetText();
            txtEmail.ResetText();
            txtWebsite.ResetText();
            txtNganHang.ResetText();
            txtTaiKhoan.ResetText();
            chkActive.Checked = true;
        }

        private void BindControl()
        {
            NL_NhaCC ncc = bsNhaCC.Current as NL_NhaCC;
            if (ncc != null)
            {
                txtID.Text = ncc.ID.ToString();
                txtTenTat.Text = ncc.TenTat;
                txtTenNCC.Text = ncc.TenNCC;
                txtDiaChi.Text = ncc.DiaChi;
                txtDienThoai.Text = ncc.DienThoai;
                txtMst.Text = ncc.Mst;
                txtEmail.Text = ncc.Email;
                txtWebsite.Text = ncc.Website;
                txtNganHang.Text = ncc.NganHang;
                txtTaiKhoan.Text = ncc.TaiKhoan;
                chkActive.Checked = ncc.Active;
            }
        }

        private NL_NhaCC BindObject()
        {
            NL_NhaCC ncc = new NL_NhaCC();
            if (!bThem)
                ncc = bsNhaCC.Current as NL_NhaCC;
            ncc.ID = int.Parse(txtID.Text);
            ncc.TenTat = txtTenTat.Text;
            ncc.TenNCC = txtTenNCC.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.DienThoai = txtDienThoai.Text;
            ncc.Mst = txtMst.Text;
            ncc.Email = txtEmail.Text;
            ncc.Website = txtWebsite.Text;
            ncc.NganHang = txtNganHang.Text;
            ncc.TaiKhoan = txtTaiKhoan.Text;
            ncc.Active = chkActive.Checked;
            return ncc;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
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
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
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
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            NL_NhaCC ncc = bsNhaCC.Current as NL_NhaCC;
            if (Library.DialogHelper.Confirm("Xóa nhà cung cấp này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLDeleteNhaCC?id=" + ncc.ID);
                if (opStatus.Result.ID== ncc.ID)
                    bsNhaCC.Remove(ncc);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                NL_NhaCC ncc = BindObject();               
                if (bThem)
                {
                    ncc.CreatedBy = AppGlobal.User.Username;
                    ncc.CreatedName = AppGlobal.User.FullName;
                    ncc.CreatedDate = DateTime.Now;
                    ncc.ModifyBy = ncc.CreatedBy;
                    ncc.ModifyName = ncc.CreatedName;
                    ncc.ModifyDate = ncc.CreatedDate;
                    var objInsert = await HttpHelper.Post<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLPostNhaCC", ncc);
                    bsNhaCC.Add(ncc);
                    bsNhaCC.MoveLast();
                }
                else
                {
                    ncc.ModifyBy = AppGlobal.User.Username;
                    ncc.ModifyName= AppGlobal.User.FullName;
                    ncc.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLPutNhaCC?id=" + ncc.ID, ncc);
                    bsNhaCC.EndEdit();                   
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

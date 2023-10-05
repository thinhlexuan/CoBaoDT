using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.HaNoi
{
    public partial class HNPhieuThuongForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public HNPhieuThuongForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this); 
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDonGia);

            txtGaName.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            string[] arRays = new string[] { "Tất cả", "Đúng giờ", "Gỡ giờ" };
            cboLoaiPhieuTT.Items.AddRange(arRays);
            cboLoaiPhieuTT.SelectedIndex = 0;         

            arRays = new string[] {"Đúng giờ", "Gỡ giờ" };
            cboLoaiPhieu.Items.AddRange(arRays);
            cboLoaiPhieu.SelectedIndex = -1;

            sdNgayTT.Value = DateTime.Today;

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
                bsLoaiPhieu.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiPhieu=" + cboLoaiPhieuTT.Text;
                data += "&MacTau=" + txtMacTauTT.Text;
                List<HNPhieuThuong> listHNPhieuThuong = HttpHelper.GetList<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNGetPhieuThuong" + data)
                   .OrderBy(x=>x.LoaiPhieu).ThenBy(x => x.NgayHL).ToList();                
                if (listHNPhieuThuong.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }               
                bsLoaiPhieu.DataSource = listHNPhieuThuong;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHNPhieuThuong.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsLoaiPhieu.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboLoaiPhieu.Enabled = b;
            txtMacTau.Enabled = b;
            txtGaName.Enabled = b;
            txtDonGia.Enabled = b;                  
            txtDonVi.Enabled = b;
            sdNgayHL.Enabled = b;            
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
            txtID.Text="0";            
            txtDonGia.ResetText();                                 
        }

        private void BindControl()
        {
            HNPhieuThuong pt = bsLoaiPhieu.Current as HNPhieuThuong;
            if (pt != null)
            {
                txtID.Text = pt.ID.ToString();                
                cboLoaiPhieu.Text = pt.LoaiPhieu;
                txtMacTau.Text = pt.MacTau;
                txtGaID.Text = pt.GaID.ToString();
                txtGaName.Text = pt.GaName;
                txtDonGia.Text = pt.DonGia.ToString();                
                txtDonVi.Text = pt.DonVi;
                sdNgayHL.Value = pt.NgayHL;
            }
        }

        private HNPhieuThuong BindObject()
        {
            HNPhieuThuong pt = new HNPhieuThuong();
            if (!bThem)
                pt = bsLoaiPhieu.Current as HNPhieuThuong;
            pt.ID = long.Parse(txtID.Text);
            pt.LoaiPhieu = cboLoaiPhieu.Text;
            pt.MacTau=txtMacTau.Text;
            pt.GaID = String.IsNullOrWhiteSpace(txtGaID.Text) ? 0 : int.Parse(txtGaID.Text);
            pt.GaName = txtGaName.Text;
            pt.DonGia = String.IsNullOrWhiteSpace(txtDonGia.Text) ? 0 : decimal.Parse(txtDonGia.Text);
            pt.NgayHL = sdNgayHL.Value;
            pt.DonVi = txtDonVi.Text;
            return pt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            bsLoaiPhieu.MoveLast();
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
            HNPhieuThuong pt = BindObject();
            pt.ModifyBy = AppGlobal.User.Username;
            pt.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa dầu mỡ định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNDeletePhieuThuong?id=" + pt.ID);
                if (opStatus.Result.ID == pt.ID)
                    bsLoaiPhieu.Remove(pt);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                HNPhieuThuong pt = BindObject();
                if (bThem)
                {
                    pt.CreatedDate = DateTime.Now;
                    pt.CreatedBy = AppGlobal.User.Username;
                    pt.CreatedName = AppGlobal.User.FullName;
                    pt.ModifyDate = pt.CreatedDate;
                    pt.ModifyBy = pt.CreatedBy;
                    pt.ModifyName = pt.CreatedName;
                    var objInsert = await HttpHelper.Post<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNPostPhieuThuong", pt);
                    pt.ID = objInsert.ID;
                    bsLoaiPhieu.Add(pt);
                    bsLoaiPhieu.MoveLast();
                }
                else
                {
                    pt.ModifyDate = DateTime.Now;
                    pt.ModifyBy = AppGlobal.User.Username;
                    pt.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNPutPhieuThuong?id=" + pt.ID, pt);
                    bsLoaiPhieu.EndEdit();
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

        private void txtGaName_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGaName.Text))
            {
                txtGaID.Text = string.Empty;
                return;
            }
            if (AppGlobal.GaDic.ContainsValue(txtGaName.Text))
                txtGaID.Text = AppGlobal.GaDic.Where(x => x.Value == txtGaName.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtGaID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng ga hãy nhập lại.");
                txtGaName.Focus();
                txtGaName.SelectAll();
            }
        }
    }
}

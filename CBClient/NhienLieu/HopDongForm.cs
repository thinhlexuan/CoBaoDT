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
    public partial class HopDongForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;       
        public HopDongForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtTyLe);
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);

            txtTenNCC.AutoCompleteCustomSource = AppGlobal.TenNhaCCAutoComplate;
            txtTenNCC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenNCC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

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
                bsHopDong.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?tenNCC=" + txtTenNCCTT.Text.Trim();
                data += "&hopDong=" + txtHopDongTT.Text.Trim();
                List<NL_HopDong> listHopDong = HttpHelper.GetList<NL_HopDong>(Configuration.UrlCBApi + "api/NhienLieus/NLGetHopDong" + data).ToList();                
                if (listHopDong.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsHopDong.DataSource = listHopDong;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHopDong.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsHopDong.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtID.Enabled = b;           
            txtHopDong.Enabled = b;
            txtDienGiai.Enabled = b;
            txtTyLe.Enabled = b;
            txtMaNCC.Enabled = b;
            txtTenNCC.Enabled = b;
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
            txtID.Text = "0";            
            txtHopDong.ResetText();
            txtDienGiai.ResetText();
            sdNgayHL.Value = DateTime.Today;
            txtTyLe.ResetText();
            txtMaNCC.ResetText();
            txtTenNCC.ResetText();
        }

        private void BindControl()
        {
            NL_HopDong hd = bsHopDong.Current as NL_HopDong;
            if (hd != null)
            {
                txtID.Text = hd.ID.ToString();                
                txtHopDong.Text = hd.HopDong;
                txtDienGiai.Text = hd.DienGiai;
                sdNgayHL.Value = hd.NgayHL;
                txtTyLe.Text = hd.TyLe.ToString();
                txtMaNCC.Text = hd.MaNCC.ToString();
                txtTenNCC.Text = hd.TenNCC;                
            }
        }

        private NL_HopDong BindObject()
        {
            NL_HopDong hd = new NL_HopDong();
            if (!bThem)
                hd = bsHopDong.Current as NL_HopDong;
            hd.ID = int.Parse(txtID.Text);
            hd.MaNCC = int.Parse(txtMaNCC.Text);
            hd.TenNCC = txtTenNCC.Text;
            hd.HopDong = txtHopDong.Text;
            hd.DienGiai = txtDienGiai.Text;
            hd.NgayHL = sdNgayHL.Value;
            hd.TyLe = decimal.Parse(txtTyLe.Text);            
            return hd;
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
            NL_HopDong hd = bsHopDong.Current as NL_HopDong;
            if (Library.DialogHelper.Confirm("Xóa hợp đồng này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<NL_HopDong>(Configuration.UrlCBApi + "api/NhienLieus/NLDeleteHopDong?id=" + hd.ID);
                if (opStatus.Result.ID== hd.ID)
                    bsHopDong.Remove(hd);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                NL_HopDong ncc = BindObject();               
                if (bThem)
                {
                    ncc.CreatedBy = AppGlobal.User.Username;
                    ncc.CreatedName = AppGlobal.User.FullName;
                    ncc.CreatedDate = DateTime.Now;
                    ncc.ModifyBy = ncc.CreatedBy;
                    ncc.ModifyName = ncc.CreatedName;
                    ncc.ModifyDate = ncc.CreatedDate;
                    var objInsert = await HttpHelper.Post<NL_HopDong>(Configuration.UrlCBApi + "api/NhienLieus/NLPostHopDong", ncc);
                    bsHopDong.Add(ncc);
                    bsHopDong.MoveLast();
                }
                else
                {
                    ncc.ModifyBy = AppGlobal.User.Username;
                    ncc.ModifyName= AppGlobal.User.FullName;
                    ncc.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<NL_HopDong>(Configuration.UrlCBApi + "api/NhienLieus/NLPutHopDong?id=" + ncc.ID, ncc);
                    bsHopDong.EndEdit();                   
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

        private void txtTenNCC_Validated(object sender, EventArgs e)
        {
            if (AppGlobal.NLNhaccDic.ContainsValue(txtTenNCC.Text))
                txtMaNCC.Text = AppGlobal.NLNhaccDic.Where(x => x.Value == txtTenNCC.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtMaNCC.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng nhà cung cấp hãy nhập lại.");
                txtTenNCC.Focus();
                txtTenNCC.SelectAll();
            }
        }
    }
}

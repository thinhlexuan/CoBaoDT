using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.Vinh
{
    public partial class VINLDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public VINLDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this); 
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMuc);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMucDon);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTyLeDon);

            var loaiMay = (from ct in AppGlobal.DMLoaimayList
                             select new
                             {
                                 MaLM = ct.LoaiMayId,
                                 TenLM = ct.LoaiMayName
                             }).ToList();
            
            var loaiMayTT = loaiMay.ToList();
            loaiMayTT.Add(new { MaLM = "ALL", TenLM = "Tất cả các loại máy" });
            var loaiMayTT1=loaiMayTT.OrderBy(x => x.MaLM).ToList();
            cboLoaiMayTT.DataSource = loaiMayTT1;
            cboLoaiMayTT.DisplayMember = "TenLM";
            cboLoaiMayTT.ValueMember = "MaLM";
            cboLoaiMayTT.SelectedIndex = 0;
            
            var loaiMayAdd = loaiMay.OrderBy(f => f.MaLM).ToList();
            cboLoaiMay.DataSource = loaiMayAdd;
            cboLoaiMay.DisplayMember = "TenLM";
            cboLoaiMay.ValueMember = "MaLM";
            cboLoaiMay.SelectedIndex = -1;

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
                bsNLDinhMuc.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();
                data += "&LoaiTau=" + txtLoaiTauTT.Text.Trim();
                var query = HttpHelper.GetList<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMuc" + data)
                   .OrderBy(x => x.LoaiMayID).ThenBy(x=>x.DinhMuc).ThenBy(x=>x.NgayHL).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<VINLDinhMuc> listNLDinhMuc= (from x in query
                                                  group x by new { x.LoaiMayID, x.KhuDoan, x.LoaiTau } into g
                                                  select new VINLDinhMuc
                                                  {
                                                      ID = g.LastOrDefault().ID,
                                                      LoaiMayID = g.Key.LoaiMayID,                                                    
                                                      KhuDoan = g.Key.KhuDoan,
                                                      LoaiTau=g.Key.LoaiTau,                                                      
                                                      DinhMuc = g.LastOrDefault().DinhMuc,                                                     
                                                      DonVi = g.LastOrDefault().DonVi,
                                                      DinhMucDon=g.LastOrDefault().DinhMucDon,
                                                      TyLeDon=g.LastOrDefault().TyLeDon,
                                                      DonViDon=g.LastOrDefault().DonViDon,
                                                      NgayHL = g.LastOrDefault().NgayHL,
                                                      Createddate = g.LastOrDefault().Createddate,
                                                      Createdby = g.LastOrDefault().Createdby,
                                                      CreatedName = g.LastOrDefault().CreatedName,
                                                      Modifydate = g.LastOrDefault().Modifydate,
                                                      Modifyby = g.LastOrDefault().Modifyby,
                                                      ModifyName = g.LastOrDefault().ModifyName
                                                  }).ToList();
                bsNLDinhMuc.DataSource = listNLDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listNLDinhMuc.Count.ToString("N0");
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
            txtLoaiTau.Enabled = b;
            txtKhuDoan.Enabled = b;
            txtDinhMuc.Enabled = b;                 
            txtDonVi.Enabled = b;
            txtDinhMucDon.Enabled = b;
            txtTyLeDon.Enabled = b;
            txtDonViDon.Enabled = b;
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
            //cboLoaiMay.SelectedIndex = -1;
            //txtLoaiTau.ResetText();
            //txtKhuDoan.ResetText();
            txtDinhMuc.ResetText();
            txtDinhMucDon.ResetText();
            txtTyLeDon.ResetText();
            //txtDonVi.ResetText();           
        }

        private void BindControl()
        {
            VINLDinhMuc dm = bsNLDinhMuc.Current as VINLDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                txtKhuDoan.Text = dm.KhuDoan;
                txtLoaiTau.Text = dm.LoaiTau;              
                txtDinhMuc.Text = dm.DinhMuc.ToString();               
                txtDonVi.Text = dm.DonVi;
                txtDinhMucDon.Text = dm.DinhMucDon.ToString();
                txtTyLeDon.Text = dm.TyLeDon.ToString();
                txtDonViDon.Text = dm.DonViDon;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private VINLDinhMuc BindObject()
        {
            VINLDinhMuc dm = new VINLDinhMuc();
            if (!bThem)
                dm = bsNLDinhMuc.Current as VINLDinhMuc;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();            
            dm.KhuDoan = txtKhuDoan.Text;
            dm.LoaiTau = txtLoaiTau.Text;           
            dm.DinhMuc = String.IsNullOrWhiteSpace(txtDinhMuc.Text) ? 0 : decimal.Parse(txtDinhMuc.Text);
            dm.DonVi = txtDonVi.Text;
            dm.DinhMucDon = String.IsNullOrWhiteSpace(txtDinhMucDon.Text) ? 0 : decimal.Parse(txtDinhMucDon.Text);
            dm.TyLeDon = String.IsNullOrWhiteSpace(txtTyLeDon.Text) ? 0 : decimal.Parse(txtTyLeDon.Text);
            dm.DonViDon = txtDonViDon.Text;
            dm.NgayHL = sdNgayHL.Value;                        
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
            VINLDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIDeleteNLDinhMuc?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsNLDinhMuc.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                VINLDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIPostNLDinhMuc", dm);
                    //fnTraTim();
                    dm.ID = objInsert.ID;
                    bsNLDinhMuc.Add(dm);
                    bsNLDinhMuc.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIPutNLDinhMuc?id=" + dm.ID, dm);
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

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
    public partial class VINLDDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public VINLDDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForNumber(txtMayChinhTL);
            FormHelper.AddKeyPressEventHandlerForNumber(txtMayPhuTL);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtMayChinhDM);            
            FormHelper.AddKeyPressEventHandlerForDecimal(txtMayPhuDM);           
            FormHelper.AddKeyPressEventHandlerForDecimal(txtKM);           

            var loaiMay = (from ct in AppGlobal.DMLoaimayList
                             select new
                             {
                                 MaLM = ct.LoaiMayId,
                                 TenLM = ct.LoaiMayName
                             }).ToList();
            
            var loaiMayTT = loaiMay.ToList();
            loaiMayTT.Add(new { MaLM = "ALL", TenLM = "Tất cả các loại máy" });
            var loaiMayTT1=loaiMayTT.OrderBy(x => x.MaLM).ToList();
            cboMayChinhTT.DataSource = loaiMayTT1;
            cboMayChinhTT.DisplayMember = "TenLM";
            cboMayChinhTT.ValueMember = "MaLM";
            cboMayChinhTT.SelectedIndex = 0;

            var loaiMayTT2 = loaiMayTT.OrderBy(x => x.MaLM).ToList();
            cboMayPhuTT.DataSource = loaiMayTT2;
            cboMayPhuTT.DisplayMember = "TenLM";
            cboMayPhuTT.ValueMember = "MaLM";
            cboMayPhuTT.SelectedIndex = 0;

            var loaiMayAdd1 = loaiMay.OrderBy(f => f.MaLM).ToList();
            cboMayChinh.DataSource = loaiMayAdd1;
            cboMayChinh.DisplayMember = "TenLM";
            cboMayChinh.ValueMember = "MaLM";
            cboMayChinh.SelectedIndex = -1;

            var loaiMayAdd2 = loaiMay.OrderBy(f => f.MaLM).ToList();
            cboMayPhu.DataSource = loaiMayAdd2;
            cboMayPhu.DisplayMember = "TenLM";
            cboMayPhu.ValueMember = "MaLM";
            cboMayPhu.SelectedIndex = -1;

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
                bsNLDDinhMuc.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&MayChinh=" + cboMayChinhTT.SelectedValue.ToString();
                data += "&MayPhu=" + cboMayPhuTT.SelectedValue.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();               
                 var query = HttpHelper.GetList<VINLDDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDDinhMuc" + data)
                   .OrderBy(x => x.KhuDoan).ThenBy(x => x.MayChinhID).ThenBy(x => x.NgayHL).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<VINLDDinhMuc> listNLDDinhMuc=(from x in query
                                                    group x by new { x.KhuDoan, x.MayChinhID, x.MayPhuID } into g
                                                    select new VINLDDinhMuc
                                                    {
                                                        ID = g.LastOrDefault().ID,                                                       
                                                        KhuDoan = g.Key.KhuDoan,
                                                        MayChinhID = g.Key.MayChinhID,
                                                        MayPhuID = g.Key.MayPhuID,
                                                        MayChinhTL = g.LastOrDefault().MayChinhTL,
                                                        MayChinhDM = g.LastOrDefault().MayChinhDM,
                                                        MayPhuTL=g.LastOrDefault().MayPhuTL,
                                                        MayPhuDM=g.LastOrDefault().MayPhuDM,
                                                        Km=g.LastOrDefault().Km,
                                                        DonVi = g.LastOrDefault().DonVi,
                                                        NgayHL = g.LastOrDefault().NgayHL,
                                                        Createddate = g.LastOrDefault().Createddate,
                                                        Createdby = g.LastOrDefault().Createdby,
                                                        CreatedName = g.LastOrDefault().CreatedName,
                                                        Modifydate = g.LastOrDefault().Modifydate,
                                                        Modifyby = g.LastOrDefault().Modifyby,
                                                        ModifyName = g.LastOrDefault().ModifyName
                                                    }).ToList();
                bsNLDDinhMuc.DataSource = listNLDDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listNLDDinhMuc.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsNLDDinhMuc.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtKhuDoan.Enabled = b;
            cboMayChinh.Enabled = b;
            cboMayPhu.Enabled = b;
            txtMayChinhTL.Enabled = b;
            txtMayChinhDM.Enabled = b;
            txtMayPhuTL.Enabled = b;
            txtMayPhuDM.Enabled = b;
            txtKM.Enabled = b;
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
            txtMayChinhTL.ResetText();
            txtMayChinhDM.ResetText();
            txtMayPhuTL.ResetText();
            txtMayPhuDM.ResetText();
            txtKM.ResetText();
        }

        private void BindControl()
        {
            VINLDDinhMuc dm = bsNLDDinhMuc.Current as VINLDDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();
                txtKhuDoan.Text = dm.KhuDoan;
                if (!String.IsNullOrWhiteSpace(dm.MayChinhID)) cboMayChinh.SelectedValue = dm.MayChinhID;
                if (!String.IsNullOrWhiteSpace(dm.MayPhuID)) cboMayPhu.SelectedValue = dm.MayPhuID;
                txtMayChinhTL.Text = dm.MayChinhTL.ToString();
                txtMayChinhDM.Text = dm.MayChinhDM.ToString();
                txtMayPhuTL.Text = dm.MayPhuTL.ToString();
                txtMayPhuDM.Text = dm.MayPhuDM.ToString();
                txtKM.Text = dm.Km.ToString();
                txtDonVi.Text = dm.DonVi;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private VINLDDinhMuc BindObject()
        {
            VINLDDinhMuc dm = new VINLDDinhMuc();
            if (!bThem)
                dm = bsNLDDinhMuc.Current as VINLDDinhMuc;
            dm.ID = long.Parse(txtID.Text);
            dm.KhuDoan = txtKhuDoan.Text;
            dm.MayChinhID = cboMayChinh.SelectedValue.ToString();
            dm.MayPhuID = cboMayPhu.SelectedValue.ToString();
            dm.MayChinhTL= String.IsNullOrWhiteSpace(txtMayChinhTL.Text) ? 0 : int.Parse(txtMayChinhTL.Text);
            dm.MayChinhDM = String.IsNullOrWhiteSpace(txtMayChinhDM.Text) ? 0 : decimal.Parse(txtMayChinhDM.Text);
            dm.MayPhuTL = String.IsNullOrWhiteSpace(txtMayPhuTL.Text) ? 0 : int.Parse(txtMayPhuTL.Text);
            dm.MayPhuDM = String.IsNullOrWhiteSpace(txtMayPhuDM.Text) ? 0 : decimal.Parse(txtMayPhuDM.Text);
            dm.Km = String.IsNullOrWhiteSpace(txtKM.Text) ? 0 : decimal.Parse(txtKM.Text);
            dm.DonVi = txtDonVi.Text;
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
            bsNLDDinhMuc.MoveLast();
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
            VINLDDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức đẩy này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<VINLDDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIDeleteNLDDinhMuc?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsNLDDinhMuc.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                VINLDDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<VINLDDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIPostNLDDinhMuc", dm);
                    //fnTraTim();
                    dm.ID = objInsert.ID;
                    bsNLDDinhMuc.Add(dm);
                    bsNLDDinhMuc.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<VINLDDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIPutNLDDinhMuc?id=" + dm.ID, dm);
                    bsNLDDinhMuc.EndEdit();
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

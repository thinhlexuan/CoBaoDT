using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.DuongSat
{
    public partial class DSNLDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public DSNLDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this); 
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMuc);          

            var donVi = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();
            var donViTT = donVi.ToList();            
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
                else
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            cboDonViTT.DataSource = donViTT;
            cboDonViTT.DisplayMember = "TenDV";
            cboDonViTT.ValueMember = "MaDV";
            cboDonViTT.SelectedIndex = 0;

            var donViAdd = donVi.Where(x=>x.MaDV!="TCT").OrderBy(f => f.MaDV).ToList();
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                donViAdd = donViAdd.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            cboDonVi.DataSource = donViAdd;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = -1;

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


            var congTac = (from ct in AppGlobal.CongtacList
                           select new
                           {
                               MaCT = (short)ct.CongTacId,
                               TenCT = ct.CongTacName
                           }).ToList();

            var conTacTT = congTac.ToList();
            conTacTT.Add(new {MaCT = (short)0, TenCT = "Tất cả các công tác" });
            var congTacTT1 = conTacTT.OrderBy(x => x.MaCT).ToList();
            cboCongTacTT.DataSource = congTacTT1;
            cboCongTacTT.DisplayMember = "TenCT";
            cboCongTacTT.ValueMember = "MaCT";
            cboCongTacTT.SelectedIndex = 0;

            var congTacAdd = congTac.OrderBy(f => f.MaCT).ToList();
            cboCongTac.DataSource = congTacAdd;
            cboCongTac.DisplayMember = "TenCT";
            cboCongTac.ValueMember = "MaCT";
            cboCongTac.SelectedIndex = -1;

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
                data += "&CongTac=" + cboCongTacTT.SelectedValue.ToString();
                data += "&MaDV=" + cboDonViTT.SelectedValue.ToString();
                var query = HttpHelper.GetList<DSNLDinhMuc>(Configuration.UrlCBApi + "api/DuongSats/DSGetNLDinhMuc" + data)
                   .OrderBy(x => x.MaDV).ThenBy(x => x.LoaiMayID).ThenBy(x => x.CongTacId).ThenBy(x => x.NgayHL).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<DSNLDinhMuc> listDSNLDinhMuc = (from x in query
                                                     group x by new { x.MaDV, x.LoaiMayID, x.CongTacId, x.GhiChu } into g
                                                     select new DSNLDinhMuc
                                                     {
                                                         ID = g.LastOrDefault().ID,
                                                         MaDV=g.Key.MaDV,
                                                         LoaiMayID=g.Key.LoaiMayID,
                                                         CongTacId=g.Key.CongTacId,
                                                         GhiChu=g.Key.GhiChu,
                                                         DinhMuc=g.LastOrDefault().DinhMuc,
                                                         DonVi=g.LastOrDefault().DonVi,
                                                         NgayHL=g.LastOrDefault().NgayHL,
                                                         Createddate=g.LastOrDefault().Createddate,
                                                         Createdby = g.LastOrDefault().Createdby,
                                                         CreatedName = g.LastOrDefault().CreatedName,
                                                         Modifydate = g.LastOrDefault().Modifydate,
                                                         Modifyby = g.LastOrDefault().Modifyby,
                                                         ModifyName = g.LastOrDefault().ModifyName
                                                     }).ToList();
                bsNLDinhMuc.DataSource = listDSNLDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listDSNLDinhMuc.Count.ToString("N0");
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
            cboDonVi.Enabled = b;
            cboLoaiMay.Enabled = b;
            cboCongTac.Enabled = b;
            txtGhiChu.Enabled = b;
            txtDinhMuc.Enabled = b;                   
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
            txtGhiChu.ResetText();
            txtDinhMuc.ResetText();
        }

        private void BindControl()
        {
            DSNLDinhMuc dm = bsNLDinhMuc.Current as DSNLDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();
                if (!String.IsNullOrWhiteSpace(dm.MaDV)) cboDonVi.SelectedValue = dm.MaDV;
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                if (!String.IsNullOrWhiteSpace(dm.CongTacId.ToString())) cboCongTac.SelectedValue = dm.CongTacId;
                txtGhiChu.Text = dm.GhiChu;
                txtDinhMuc.Text = dm.DinhMuc.ToString();
                txtDonVi.Text = dm.DonVi;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private DSNLDinhMuc BindObject()
        {
            DSNLDinhMuc dm = new DSNLDinhMuc();
            if (!bThem)
                dm = bsNLDinhMuc.Current as DSNLDinhMuc;
            dm.ID = long.Parse(txtID.Text);
            dm.MaDV = cboDonVi.SelectedValue.ToString();
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();
            dm.CongTacId = short.Parse(cboCongTac.SelectedValue.ToString());
            dm.GhiChu = txtGhiChu.Text;
            dm.DinhMuc = String.IsNullOrWhiteSpace(txtDinhMuc.Text) ? 0 : decimal.Parse(txtDinhMuc.Text);            
            dm.NgayHL = sdNgayHL.Value;
            dm.DonVi = txtDonVi.Text;
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
            DSNLDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<DSNLDinhMuc>(Configuration.UrlCBApi + "api/DuongSats/DSDeleteNLDinhMuc?id=" + dm.ID);
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
                DSNLDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<DSNLDinhMuc>(Configuration.UrlCBApi + "api/DuongSats/DSPostNLDinhMuc", dm);
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
                    var objUpdate = await HttpHelper.Put<DSNLDinhMuc>(Configuration.UrlCBApi + "api/DuongSats/DSPutNLDinhMuc?id=" + dm.ID, dm);
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

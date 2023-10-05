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
    public partial class HNNLPDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public HNNLPDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForNumber(txtSTT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMuc);           

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
                bsNLPDinhMuc.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();                
                var query = HttpHelper.GetList<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMuc" + data)
                   .OrderBy(x => x.NgayHL).OrderBy(x=>x.LoaiMayID).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<HNNLPDinhMuc> listHNNLPDinhMuc = (from x in query
                                                       group x by new { x.LoaiMayID, x.CongTac, x.KhuDoan } into g
                                                       select new HNNLPDinhMuc
                                                       {
                                                           ID = g.LastOrDefault().ID,
                                                           LoaiMayID=g.Key.LoaiMayID,
                                                           STT=g.LastOrDefault().STT,
                                                           DinhMuc=g.LastOrDefault().DinhMuc,
                                                           DonVi=g.LastOrDefault().DonVi,
                                                           NgayHL=g.LastOrDefault().NgayHL,
                                                           CongTac = g.Key.CongTac,
                                                           DienGiai = g.LastOrDefault().DienGiai,
                                                           KhuDoan = g.Key.KhuDoan,
                                                           Createddate = g.LastOrDefault().Createddate,
                                                           Createdby = g.LastOrDefault().Createdby,
                                                           CreatedName = g.LastOrDefault().CreatedName,
                                                           Modifydate = g.LastOrDefault().Modifydate,
                                                           Modifyby = g.LastOrDefault().Modifyby,
                                                           ModifyName = g.LastOrDefault().ModifyName
                                                       }).ToList();
                bsNLPDinhMuc.DataSource = listHNNLPDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHNNLPDinhMuc.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsNLPDinhMuc.DataSource = null;
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
            txtDinhMuc.Enabled = b;                  
            txtDonVi.Enabled = b;
            sdNgayHL.Enabled = b;
            txtCongTac.Enabled = b;
            txtDienGiai.Enabled = b;
            txtKhuDoan.Enabled = b;
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
            txtCongTac.ResetText();
            txtDienGiai.ResetText();
            txtKhuDoan.ResetText();
            txtDinhMuc.ResetText();                     
            //txtDonVi.ResetText();           
        }

        private void BindControl()
        {
            HNNLPDinhMuc dm = bsNLPDinhMuc.Current as HNNLPDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                txtSTT.Text = dm.STT.ToString();
                txtCongTac.Text = dm.CongTac;
                txtDienGiai.Text = dm.DienGiai.ToString();
                txtKhuDoan.Text = dm.KhuDoan; 
                txtDinhMuc.Text = dm.DinhMuc.ToString();                
                txtDonVi.Text = dm.DonVi;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private HNNLPDinhMuc BindObject()
        {
            HNNLPDinhMuc dm = new HNNLPDinhMuc();
            if (!bThem)
                dm = bsNLPDinhMuc.Current as HNNLPDinhMuc;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();
            dm.STT = String.IsNullOrWhiteSpace(txtSTT.Text) ? (short)0 : short.Parse(txtSTT.Text);
            dm.CongTac = txtCongTac.Text;
            dm.DienGiai = txtDienGiai.Text;
            dm.KhuDoan = txtKhuDoan.Text;           
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
            bsNLPDinhMuc.MoveLast();
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
            HNNLPDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức phụ này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNDeleteNLPDinhMuc?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsNLPDinhMuc.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                HNNLPDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNPostNLPDinhMuc", dm);
                    //fnTraTim();
                    dm.ID = objInsert.ID;
                    bsNLPDinhMuc.Add(dm);
                    bsNLPDinhMuc.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNPutNLPDinhMuc?id=" + dm.ID, dm);
                    bsNLPDinhMuc.EndEdit();
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

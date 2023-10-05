using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.YenVien
{
    public partial class YVNLDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public YVNLDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMin);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMax);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDinhMuc);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtHeSo);

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
                bsNLDinhMuc.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();                
               var query = HttpHelper.GetList<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLDinhMuc" + data)
                   .OrderBy(x => x.LoaiMayID).ThenBy(x => x.NgayHL).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<YVNLDinhMuc> listYVNLDinhMuc= (from x in query
                                                    group x by new { x.LoaiMayID,x.CongTac,x.KhuDoan,x.TanMin,x.TanMax } into g
                                                    select new YVNLDinhMuc
                                                    {
                                                        ID=g.LastOrDefault().ID,
                                                        LoaiMayID=g.Key.LoaiMayID,
                                                        CongTac=g.Key.CongTac,
                                                        KhuDoan=g.Key.KhuDoan,
                                                        TanMin=g.Key.TanMin,
                                                        TanMax=g.Key.TanMax,
                                                        DinhMuc=g.LastOrDefault().DinhMuc,
                                                        HeSo=g.LastOrDefault().HeSo,
                                                        DonVi=g.LastOrDefault().DonVi,
                                                        NgayHL=g.LastOrDefault().NgayHL,
                                                        Createddate=g.LastOrDefault().Createddate,
                                                        Createdby=g.LastOrDefault().Createdby,
                                                        CreatedName=g.LastOrDefault().CreatedName,
                                                        Modifydate=g.LastOrDefault().Modifydate,
                                                        Modifyby=g.LastOrDefault().Modifyby,
                                                        ModifyName=g.LastOrDefault().ModifyName
                                                    }).ToList();
                bsNLDinhMuc.DataSource = listYVNLDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listYVNLDinhMuc.Count.ToString("N0");
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
            txtCongTac.Enabled = b;
            txtKhuDoan.Enabled = b;            
            txtTanMin.Enabled = b;
            txtTanMax.Enabled = b;            
            txtDinhMuc.Enabled = b;
            txtHeSo.Enabled = b;           
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
            //cboLoaiMay.SelectedIndex = -1;
            txtCongTac.ResetText();
            txtKhuDoan.ResetText();            
            txtTanMin.ResetText();
            txtTanMax.ResetText();           
            txtDinhMuc.ResetText();
            txtHeSo.ResetText();           
            //txtDonVi.ResetText();           
        }

        private void BindControl()
        {
            YVNLDinhMuc dm = bsNLDinhMuc.Current as YVNLDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                txtCongTac.Text = dm.CongTac;
                txtKhuDoan.Text = dm.KhuDoan;                
                txtTanMin.Text = dm.TanMin.ToString();
                txtTanMax.Text = dm.TanMax.ToString();
                txtDinhMuc.Text = dm.DinhMuc.ToString();
                txtHeSo.Text = dm.HeSo.ToString();
                txtDonVi.Text = dm.DonVi;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private YVNLDinhMuc BindObject()
        {
            YVNLDinhMuc dm = new YVNLDinhMuc();
            if (!bThem)
                dm = bsNLDinhMuc.Current as YVNLDinhMuc;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();
            dm.CongTac = txtCongTac.Text;
            dm.KhuDoan = txtKhuDoan.Text;
            dm.TanMin = String.IsNullOrWhiteSpace(txtTanMin.Text) ? 0 : decimal.Parse(txtTanMin.Text);
            dm.TanMax = String.IsNullOrWhiteSpace(txtTanMax.Text) ? 0 : decimal.Parse(txtTanMax.Text);
            dm.DinhMuc = String.IsNullOrWhiteSpace(txtDinhMuc.Text) ? 0 : decimal.Parse(txtDinhMuc.Text);
            dm.HeSo = String.IsNullOrWhiteSpace(txtHeSo.Text) ? 0 : decimal.Parse(txtHeSo.Text);
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
            YVNLDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVDeleteNLDinhMuc?id=" + dm.ID);
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
                YVNLDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVPostNLDinhMuc", dm);
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
                    var objUpdate = await HttpHelper.Put<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVPutNLDinhMuc?id=" + dm.ID, dm);
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

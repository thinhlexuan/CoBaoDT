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
    public partial class YVDMDinhMucForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public YVDMDinhMucForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this); 
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

            var loaiDM = (from ct in AppGlobal.DMLoaidmList
                          select new
                           {
                               MaDM = (short)ct.ID,
                               TenDM = ct.LoaiDauMo
                           }).ToList();

            var loaiDMTT = loaiDM.ToList();
            loaiDMTT.Add(new { MaDM = (short)0, TenDM = "Tất cả các loại dầu mỡ" });
            var loaiDMTT1 = loaiDMTT.OrderBy(x => x.MaDM).ToList();
            cboLoaidmTT.DataSource = loaiDMTT1;
            cboLoaidmTT.DisplayMember = "TenDM";
            cboLoaidmTT.ValueMember = "MaDM";
            cboLoaidmTT.SelectedIndex = 0;

            var loaiDMAdd = loaiDM.OrderBy(f => f.MaDM).ToList();
            cboLoaiDM.DataSource = loaiDMAdd;
            cboLoaiDM.DisplayMember = "TenDM";
            cboLoaiDM.ValueMember = "MaDM";
            cboLoaiDM.SelectedIndex = -1;

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
                bsDMDinhMuc.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();
                data += "&DauMoID=" + cboLoaidmTT.SelectedValue.ToString();                
                List<YVDMDinhMuc> listYVDMDinhMuc = HttpHelper.GetList<YVDMDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetDMDinhMuc" + data)
                   .OrderBy(x=>x.LoaiMayID).ToList();                
                if (listYVDMDinhMuc.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsDMDinhMuc.DataSource = listYVDMDinhMuc;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listYVDMDinhMuc.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsDMDinhMuc.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboLoaiMay.Enabled = b;
            cboLoaiDM.Enabled = b;
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
            //cboLoaiMay.SelectedIndex = -1;           
            txtDinhMuc.ResetText();                     
            //txtDonVi.ResetText();           
        }

        private void BindControl()
        {
            YVDMDinhMuc dm = bsDMDinhMuc.Current as YVDMDinhMuc;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;
                if (!String.IsNullOrWhiteSpace(dm.DauMoID.ToString())) cboLoaiDM.SelectedValue = dm.DauMoID;               
                txtDinhMuc.Text = dm.DinhMuc.ToString();                
                txtDonVi.Text = dm.DonVi;
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private YVDMDinhMuc BindObject()
        {
            YVDMDinhMuc dm = new YVDMDinhMuc();
            if (!bThem)
                dm = bsDMDinhMuc.Current as YVDMDinhMuc;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();
            dm.DauMoID=short.Parse(cboLoaiDM.SelectedValue.ToString());
            dm.DauMoName = cboLoaiDM.Text;
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
            bsDMDinhMuc.MoveLast();
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
            YVDMDinhMuc dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa dầu mỡ định mức này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<YVDMDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVDeleteDMDinhMuc?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsDMDinhMuc.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                YVDMDinhMuc dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<YVDMDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVPostDMDinhMuc", dm);
                    //fnTraTim();
                    dm.ID = objInsert.ID;
                    bsDMDinhMuc.Add(dm);
                    bsDMDinhMuc.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<YVDMDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVPutDMDinhMuc?id=" + dm.ID, dm);
                    bsDMDinhMuc.EndEdit();
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

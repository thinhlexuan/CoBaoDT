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
    public partial class VIHSTanForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public VIHSTanForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMin);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanMax);            
            FormHelper.AddKeyPressEventHandlerForDecimal(txtHeSo);

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
                bsHSTan.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();               
                data += "&LoaiMay=" + cboLoaiMayTT.SelectedValue.ToString();                              
                List<VIHSTan> listHSTan = HttpHelper.GetList<VIHSTan>(Configuration.UrlCBApi + "api/Vinhs/VIGetHSTan" + data)
                   .OrderBy(x=>x.TanMax).OrderBy(x => x.TanMin).OrderBy(x => x.LoaiMayID).ToList();                
                if (listHSTan.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsHSTan.DataSource = listHSTan;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHSTan.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsHSTan.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboLoaiMay.Enabled = b;                     
            txtTanMin.Enabled = b;
            txtTanMax.Enabled = b;
            txtHeSo.Enabled = b;
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
            txtTanMin.ResetText();
            txtTanMax.ResetText();
            txtHeSo.ResetText(); 
        }

        private void BindControl()
        {
            VIHSTan dm = bsHSTan.Current as VIHSTan;
            if (dm != null)
            {
                txtID.Text = dm.ID.ToString();                
                if (!String.IsNullOrWhiteSpace(dm.LoaiMayID)) cboLoaiMay.SelectedValue = dm.LoaiMayID;                             
                txtTanMin.Text = dm.TanMin.ToString();
                txtTanMax.Text = dm.TanMax.ToString();                
                txtHeSo.Text = dm.HeSo.ToString();               
                sdNgayHL.Value = dm.NgayHL;
            }
        }

        private VIHSTan BindObject()
        {
            VIHSTan dm = new VIHSTan();
            if (!bThem)
                dm = bsHSTan.Current as VIHSTan;
            dm.ID = long.Parse(txtID.Text);            
            dm.LoaiMayID = cboLoaiMay.SelectedValue.ToString();            
            dm.TanMin = String.IsNullOrWhiteSpace(txtTanMin.Text) ? 0 : decimal.Parse(txtTanMin.Text);
            dm.TanMax = String.IsNullOrWhiteSpace(txtTanMax.Text) ? 0 : decimal.Parse(txtTanMax.Text);           
            dm.HeSo = String.IsNullOrWhiteSpace(txtHeSo.Text) ? 0 : decimal.Parse(txtHeSo.Text);
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
            bsHSTan.MoveLast();
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
            VIHSTan dm = BindObject();
            dm.Modifyby = AppGlobal.User.Username;
            dm.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa hệ số tấn này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<VIHSTan>(Configuration.UrlCBApi + "api/Vinhs/VIDeleteHSTan?id=" + dm.ID);
                if (opStatus.Result.ID == dm.ID)
                    bsHSTan.Remove(dm);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                VIHSTan dm = BindObject();
                if (bThem)
                {
                    dm.Createddate = DateTime.Now;
                    dm.Createdby = AppGlobal.User.Username;
                    dm.CreatedName = AppGlobal.User.FullName;
                    dm.Modifydate = dm.Createddate;
                    dm.Modifyby = dm.Createdby;
                    dm.ModifyName = dm.CreatedName;
                    var objInsert = await HttpHelper.Post<VIHSTan>(Configuration.UrlCBApi + "api/Vinhs/VIPostHSTan", dm);
                    //fnTraTim();
                    dm.ID = objInsert.ID;
                    bsHSTan.Add(dm);
                    bsHSTan.MoveLast();
                }
                else
                {
                    dm.Modifydate = DateTime.Now;
                    dm.Modifyby = AppGlobal.User.Username;
                    dm.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<VIHSTan>(Configuration.UrlCBApi + "api/Vinhs/VIPutHSTan?id=" + dm.ID, dm);
                    bsHSTan.EndEdit();
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

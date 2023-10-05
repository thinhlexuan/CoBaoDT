using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.DanhMuc
{
    public partial class CongLenhSKForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public CongLenhSKForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDocHC);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD4H);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD5H);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD8E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD9E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD10H);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD11H);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD12E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD13E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD14Er);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD18E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD19E);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD19Er);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtD20E);

            txtGaXP.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaXP.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaXP.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaKT.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaKT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaKT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var tuyenTT = (from ct in AppGlobal.DMTuyenList
                           where ct.Active == true
                           select new
                           {
                               TuyenID = ct.TuyenID,
                               TuyenName = ct.TuyenName
                           }).ToList();

            var lisTuyen = tuyenTT.OrderBy(f => f.TuyenID).ToList();
            cboTuyen.DataSource = lisTuyen;
            cboTuyen.DisplayMember = "TuyenName";
            cboTuyen.ValueMember = "TuyenID";
            cboTuyen.SelectedIndex = -1;

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
                bsCongLenhSK.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?NgayHL=" + sdNgayTT.Value.ToString();
                data += "&KhuDoan=" + txtKhuDoanTT.Text.Trim();                
                List<CongLenhSK> listCongLenhSK = HttpHelper.GetList<CongLenhSK>(Configuration.UrlCBApi + "api/CongLenhSKs/GetByTraTim" + data).ToList();                
                if (listCongLenhSK.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsCongLenhSK.DataSource = listCongLenhSK;
                bsCongLenhSK.MoveLast();
                dataGridView1.Refresh();
                
                lblTableCount.Text = "Tổng số bản ghi:" + listCongLenhSK.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsCongLenhSK.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            tblID.Enabled = b;
            tblLoaiMay.Enabled = b;
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
            txtKhuDoan.ResetText();
            txtDocHC.ResetText();
            txtGhiChu.ResetText();
            txtGaXP.ResetText();
            txtGaKT.ResetText();             
            txtD4H.ResetText();
            txtD5H.ResetText();
            txtD8E.ResetText();
            txtD9E.ResetText();
            txtD10H.ResetText();
            txtD11H.ResetText();
            txtD12E.ResetText();
            txtD13E.ResetText();
            txtD14Er.ResetText();
            txtD18E.ResetText();
            txtD19E.ResetText();
            txtD19Er.ResetText();
            txtD20E.ResetText();
        }

        private void BindControl()
        {
            CongLenhSK cl = bsCongLenhSK.Current as CongLenhSK;
            if (cl != null)
            {
                txtID.Text = cl.ID.ToString();
                if (!String.IsNullOrWhiteSpace(cl.TuyenID)) cboTuyen.SelectedValue = cl.TuyenID;
                txtKhuDoan.Text = cl.KhuDoan;
                txtDocHC.Text = cl.DocHC.ToString();
                txtGhiChu.Text = cl.GhiChu;
                sdNgayHL.Value = cl.NgayHL;
                txtGaXP.Text = AppGlobal.GaDic[(int)cl.GaXP];
                txtGaKT.Text= AppGlobal.GaDic[(int)cl.GaKT];               
                txtD4H.Text = cl.D4H.ToString();
                txtD5H.Text = cl.D5H.ToString();
                txtD8E.Text = cl.D8E.ToString();
                txtD9E.Text = cl.D9E.ToString();
                txtD10H.Text = cl.D10H.ToString();
                txtD11H.Text = cl.D11H.ToString();
                txtD12E.Text = cl.D12E.ToString();
                txtD13E.Text = cl.D13E.ToString();
                txtD14Er.Text = cl.D14Er.ToString();
                txtD18E.Text = cl.D18E.ToString();
                txtD19E.Text = cl.D19E.ToString();
                txtD19Er.Text = cl.D19Er.ToString();
                txtD20E.Text = cl.D20E.ToString();
            }
        }

        private CongLenhSK BindObject()
        {
            CongLenhSK cl = new CongLenhSK();
            if (!bThem)
                cl = bsCongLenhSK.Current as CongLenhSK;
            cl.ID = long.Parse(txtID.Text);
            cl.TuyenID = cboTuyen.SelectedValue.ToString();
            cl.KhuDoan = txtKhuDoan.Text;
            cl.DocHC = decimal.Parse(txtDocHC.Text);
            cl.GhiChu = txtGhiChu.Text;
            cl.NgayHL = sdNgayHL.Value;
            cl.GaXP= AppGlobal.GaDic.Where(x => x.Value == txtGaXP.Text).FirstOrDefault().Key;
            cl.GaKT = AppGlobal.GaDic.Where(x => x.Value == txtGaKT.Text).FirstOrDefault().Key;           
            cl.D4H = decimal.Parse(txtD4H.Text);
            cl.D5H = decimal.Parse(txtD5H.Text);
            cl.D8E = decimal.Parse(txtD8E.Text);
            cl.D9E = decimal.Parse(txtD9E.Text);
            cl.D10H = decimal.Parse(txtD10H.Text);
            cl.D11H = decimal.Parse(txtD11H.Text);
            cl.D12E = decimal.Parse(txtD12E.Text);
            cl.D13E = decimal.Parse(txtD13E.Text);
            cl.D14Er = decimal.Parse(txtD14Er.Text);
            cl.D18E = decimal.Parse(txtD18E.Text);
            cl.D19E = decimal.Parse(txtD19E.Text);
            cl.D19Er = decimal.Parse(txtD19Er.Text);
            cl.D20E = decimal.Parse(txtD20E.Text);
            return cl;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
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
            CongLenhSK cl = BindObject();
            if (Library.DialogHelper.Confirm("Xóa công lệnh này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<CongLenhSK>(Configuration.UrlCBApi + "api/CongLenhSKs/DeleteByID?id=" + cl.ID);
                if (opStatus.Result.ID==cl.ID)                    
                    fnTraTim();
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                CongLenhSK cl = BindObject();
                if (bThem)
                {
                    cl.Createdby = AppGlobal.User.Username;
                    cl.CreatedName = AppGlobal.User.FullName;
                    cl.Modifyby = cl.Createdby;
                    cl.ModifyName = cl.CreatedName;
                    var objInsert = await HttpHelper.Post<CongLenhSK>(Configuration.UrlCBApi + "api/CongLenhSKs/PostByID", cl);
                    fnTraTim();
                }
                else
                {
                    cl.Modifyby = AppGlobal.User.Username;
                    cl.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<CongLenhSK>(Configuration.UrlCBApi + "api/CongLenhSKs/PutByID?id=" + cl.ID, cl);
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

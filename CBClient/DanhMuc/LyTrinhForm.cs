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
    public partial class LyTrinhForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public LyTrinhForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            Library.FormHelper.AddKeyPressEventHandlerForNumber(txtGaID);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtKM);

            txtTuyenID.AutoCompleteCustomSource = AppGlobal.MaTuyenAutoComplate;
            txtTuyenID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTuyenID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTuyenName.AutoCompleteCustomSource = AppGlobal.TenTuyenAutoComplate;
            txtTuyenName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTuyenName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaID.AutoCompleteCustomSource = AppGlobal.MaGaAutoComplate;
            txtGaID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTenGa.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtTenGa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenGa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var tuyenTT = (from ct in AppGlobal.DMTuyenList
                           where ct.Active==true
                           select new
                           {
                               TuyenID = ct.TuyenID,
                               TuyenName = ct.TuyenName
                           }).ToList();
            tuyenTT.Add(new { TuyenID = "ALL", TuyenName = "Tất cả các tuyến" });           
            var lisTT = tuyenTT.OrderBy(f => f.TuyenID).ToList();
            cboTuyenTT.DataSource = lisTT;
            cboTuyenTT.DisplayMember = "TuyenName";
            cboTuyenTT.ValueMember = "TuyenID";
            cboTuyenTT.SelectedIndex = 0;
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
                bsLyTrinh.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?TuyenID=" + cboTuyenTT.SelectedValue.ToString();
                data += "&TenGa=" + txtGaTT.Text.Trim();                
                List<LyTrinh> listLytrinh = HttpHelper.GetList<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetByTraTim" + data)
                   .OrderBy(x=>x.Km).OrderBy(x => x.TuyenID).ToList();                
                if (listLytrinh.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsLyTrinh.DataSource = listLytrinh;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listLytrinh.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsLyTrinh.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtTuyenID.Enabled = b;
            txtTuyenName.Enabled = b;
            txtGaID.Enabled = b;
            txtTenGa.Enabled = b;
            txtKM.Enabled = b;          
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
            txtTuyenID.ResetText();
            txtTuyenName.ResetText();
            txtGaID.ResetText();
            txtTenGa.ResetText();
            txtKM.ResetText();            
        }

        private void BindControl()
        {
            LyTrinh lt = bsLyTrinh.Current as LyTrinh;
            if (lt != null)
            {
                txtID.Text = lt.ID.ToString();
                txtTuyenID.Text = lt.TuyenID;
                txtTuyenName.Text = lt.TuyenName;
                txtGaID.Text = lt.GaID.ToString();
                txtTenGa.Text = lt.TenGa;
                txtKM.Text = lt.Km.ToString();               
            }
        }

        private LyTrinh BindObject()
        {
            LyTrinh lt = new LyTrinh();
            if (!bThem)
                lt = bsLyTrinh.Current as LyTrinh;
            lt.ID = long.Parse(txtID.Text);
            lt.TuyenID = txtTuyenID.Text;
            lt.TuyenName = txtTuyenName.Text;
            lt.GaID = int.Parse(txtGaID.Text);
            lt.TenGa = txtTenGa.Text;
            lt.Km = decimal.Parse(txtKM.Text);
            return lt;
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
            LyTrinh lt = BindObject();
            if (Library.DialogHelper.Confirm("Xóa lý trình này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/DeleteByID?id=" + lt.ID);
                if (opStatus.Result.ID==lt.ID)                    
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
                LyTrinh lt = BindObject();
                if (bThem)
                {
                    lt.Createdby = AppGlobal.User.Username;
                    lt.CreatedName = AppGlobal.User.FullName;
                    lt.Modifyby = lt.Createdby;
                    lt.ModifyName = lt.CreatedName;
                    var objInsert = await HttpHelper.Post<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/PostByID", lt);
                    fnTraTim();
                }
                else
                {
                    lt.Modifyby = AppGlobal.User.Username;
                    lt.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/PutByID?id=" + lt.ID, lt);
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

        private void txtTuyenID_Validated(object sender, EventArgs e)
        {
            txtTuyenID.Text = txtTuyenID.Text.ToUpper();
            if (AppGlobal.TuyenDic.ContainsKey(txtTuyenID.Text))
                txtTuyenName.Text = AppGlobal.TuyenDic[txtTuyenID.Text];
            else
            {
                txtTuyenID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng tuyến hãy nhập lại.");
                txtTuyenID.Focus();
                txtTuyenID.SelectAll();
            }
        }

        private void txtTenGa_Validated(object sender, EventArgs e)
        {            
            if (AppGlobal.GaDic.ContainsValue(txtTenGa.Text))
                txtGaID.Text = AppGlobal.GaDic.Where(x=>x.Value==txtTenGa.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng ga hãy nhập lại.");
                txtTenGa.Focus();
                txtTenGa.SelectAll();
            }
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

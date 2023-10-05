using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.SaiGon
{
    public partial class SGKhuDoanForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public SGKhuDoanForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtKM);

            txtGaXPName.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaXPName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaXPName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaKTName.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaKTName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaKTName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaDT1Name.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaDT1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaDT1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var loaiMay = (from ct in AppGlobal.DMLoaimayList
                             select new
                             {
                                 MaLM = ct.LoaiMayId,
                                 TenLM = ct.LoaiMayName
                             }).ToList();

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

            var lisTuyen1 = tuyenTT.OrderBy(f => f.TuyenID).ToList();
            cboTuyen1.DataSource = lisTuyen1;
            cboTuyen1.DisplayMember = "TuyenName";
            cboTuyen1.ValueMember = "TuyenID";
            cboTuyen1.SelectedIndex = -1;            

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
                bsKhuDoan.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data= "?KhuDoan=" + txtKhuDoanTT.Text.Trim();                
                List<SGKhuDoan> listSGLytrinh = HttpHelper.GetList<SGKhuDoan>(Configuration.UrlCBApi + "api/SaiGons/SGGetKhuDoan" + data)
                   .OrderBy(x=>x.KhuDoanID).ToList();                
                if (listSGLytrinh.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsKhuDoan.DataSource = listSGLytrinh;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listSGLytrinh.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsKhuDoan.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;            
            txtKhuDoan.Enabled = b;           
            txtGaXPName.Enabled = b;
            txtGaXPID.Enabled = b;
            cboTuyen.Enabled = b;
            txtGaKTName.Enabled = b;
            txtGaKTID.Enabled = b;
            txtGaDT1Name.Enabled = b;
            txtGaDT1ID.Enabled = b;
            cboTuyen1.Enabled = b;            
            btnThem.Enabled = !b;
            if (b == false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnIn.Enabled = dataGridView1.CurrentRow == null ? false : true;
            }
            else
            {
                btnSua.Enabled = !b;
                btnXoa.Enabled = !b;
                btnIn.Enabled = !b;
            }
            btnLuu.Enabled = b;
            btnHuy.Enabled = b;
        }

        private void ClearControl()
        {
            txtID.Text="0";
            txtKM.ResetText();            
            txtKhuDoan.ResetText();           
            txtGaXPName.ResetText();
            txtGaXPID.ResetText();
            cboTuyen.SelectedIndex = -1;
            txtGaKTName.ResetText();
            txtGaKTID.ResetText();
            txtGaDT1Name.ResetText();
            txtGaDT1ID.ResetText();
            cboTuyen1.SelectedIndex = -1;           
        }

        private void BindControl()
        {
            SGKhuDoan kd = bsKhuDoan.Current as SGKhuDoan;
            if (kd != null)
            {
                txtID.Text = kd.ID.ToString();
                txtKM.Text = kd.Km.ToString();               
                txtKhuDoan.Text = kd.KhuDoanID;                
                txtGaXPName.Text = kd.GaXP <= 0 ? string.Empty : AppGlobal.GaDic[(int)kd.GaXP];
                txtGaXPID.Text = kd.GaXP <= 0 ? string.Empty : kd.GaXP.ToString();
                if(!String.IsNullOrWhiteSpace(kd.Tuyen)) cboTuyen.SelectedValue =  kd.Tuyen;
                else cboTuyen.SelectedIndex = -1;
                txtGaKTName.Text = kd.GaKT <= 0 ? string.Empty : AppGlobal.GaDic[(int)kd.GaKT];
                txtGaKTID.Text = kd.GaKT <= 0 ? string.Empty : kd.GaKT.ToString();
                txtGaDT1Name.Text = kd.GaDT1 <= 0 ? string.Empty : AppGlobal.GaDic[(int)kd.GaDT1];
                txtGaDT1ID.Text = kd.GaDT1 <= 0 ? string.Empty : kd.GaDT1.ToString();
                if (!String.IsNullOrWhiteSpace(kd.Tuyen1)) cboTuyen1.SelectedValue = kd.Tuyen1;
                else cboTuyen1.SelectedIndex = -1;               
            }
        }

        private SGKhuDoan BindObject()
        {
            SGKhuDoan kd = new SGKhuDoan();
            if (!bThem)
                kd = bsKhuDoan.Current as SGKhuDoan;
            kd.ID = long.Parse(txtID.Text);
            kd.Km = String.IsNullOrWhiteSpace(txtKM.Text) ? 0 : decimal.Parse(txtKM.Text);           
            kd.KhuDoanID = txtKhuDoan.Text;           
            kd.GaXP = String.IsNullOrWhiteSpace(txtGaXPID.Text) ? 0 : int.Parse(txtGaXPID.Text);
            kd.Tuyen = cboTuyen.SelectedValue.ToString();
            kd.GaKT = String.IsNullOrWhiteSpace(txtGaKTID.Text) ? 0 : int.Parse(txtGaKTID.Text);
            kd.GaDT1 = String.IsNullOrWhiteSpace(txtGaDT1ID.Text) ? 0 : int.Parse(txtGaDT1ID.Text);
            kd.Tuyen1 = cboTuyen1.SelectedIndex < 0 ? null : cboTuyen1.SelectedValue.ToString();           
            TinhCacGa_KM(kd);
            return kd;
        }

        private void TinhCacGa_KM(SGKhuDoan kd)
        {
            kd.Km = 0;
            kd.CacGa = kd.GaXP.ToString();
            bool isFisrt = true;
            //Nếu có ga đổi tuyến 1
            if (kd.GaDT1>0)
            {
                string data = "?GaXP=" + kd.GaXP + "&GaKT=" + kd.GaDT1 + "&Tuyen=";
                var dm = HttpHelper.Get<DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                if (dm!=null)
                {                   
                    kd.Km += Math.Abs((decimal)dm.GaDenKM - (decimal)dm.GaDiKM);
                    data = "?TuyenID=" + dm.TuyenId + "&TenGa=";
                    var listLyTrinh = HttpHelper.GetList<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetByTraTim" + data);
                    if (dm.Chieu == "di")
                        listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDiKM && x.Km <= dm.GaDenKM).OrderBy(x => x.Km).ToList();
                    else
                        listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDenKM && x.Km <= dm.GaDiKM).OrderByDescending(x=>x.Km).ToList();
                    isFisrt = true;
                    foreach (LyTrinh lt in listLyTrinh)
                    {
                        if (isFisrt==false) kd.CacGa += "," + lt.GaID.ToString();
                        isFisrt = false;
                    }
                    //Cộng thêm đến ga Kết thúc
                    data = "?GaXP=" + kd.GaDT1 + "&GaKT=" + kd.GaKT + "&Tuyen=";
                    dm = HttpHelper.Get<DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                    if (dm!=null)
                    {                        
                        kd.Km += Math.Abs((decimal)dm.GaDenKM - (decimal)dm.GaDiKM);
                        data = "?TuyenID=" + dm.TuyenId + "&TenGa=";
                        listLyTrinh = HttpHelper.GetList<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetByTraTim" + data);
                        if (dm.Chieu == "di")
                            listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDiKM && x.Km <= dm.GaDenKM).OrderBy(x => x.Km).ToList();
                        else
                            listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDenKM && x.Km <= dm.GaDiKM).OrderByDescending(x => x.Km).ToList();
                        isFisrt = true;
                        foreach (LyTrinh lt in listLyTrinh)
                        {
                            if (isFisrt == false) kd.CacGa += "," + lt.GaID.ToString();
                            isFisrt = false;
                        }
                        return;
                    }                    
                }                
            }   
            else
            {
                string data = "?GaXP=" + kd.GaXP + "&GaKT=" + kd.GaKT + "&Tuyen=";
                var dm = HttpHelper.Get<DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                if (dm!=null)
                {                                     
                    kd.Km += Math.Abs((decimal)dm.GaDenKM - (decimal)dm.GaDiKM );
                    data = "?TuyenID=" + dm.TuyenId + "&TenGa=";
                    var listLyTrinh= HttpHelper.GetList<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetByTraTim" + data);
                    if (dm.Chieu == "di")
                        listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDiKM && x.Km <= dm.GaDenKM).OrderBy(x => x.Km).ToList();
                    else
                        listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDenKM && x.Km <= dm.GaDiKM).OrderByDescending(x => x.Km).ToList();
                    isFisrt = true;
                    foreach (LyTrinh lt in listLyTrinh)
                    {
                        if (isFisrt == false) kd.CacGa += "," + lt.GaID.ToString();
                        isFisrt = false;
                    }
                }
            }    
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            bsKhuDoan.MoveLast();
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
            SGKhuDoan kd = BindObject();
            kd.Modifyby = AppGlobal.User.Username;
            kd.ModifyName = AppGlobal.User.FullName;
            if (Library.DialogHelper.Confirm("Xóa khu đoạn này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<SGKhuDoan>(Configuration.UrlCBApi + "api/SaiGons/SGDeleteKhuDoan?id=" + kd.ID);
                if (opStatus.Result.ID == kd.ID)
                    bsKhuDoan.Remove(kd);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                SGKhuDoan kd = BindObject();
                if (bThem)
                {
                    kd.Createddate = DateTime.Now;
                    kd.Createdby = AppGlobal.User.Username;
                    kd.CreatedName = AppGlobal.User.FullName;
                    kd.Modifydate = kd.Createddate;
                    kd.Modifyby = kd.Createdby;
                    kd.ModifyName = kd.CreatedName;
                    var objInsert = await HttpHelper.Post<SGKhuDoan>(Configuration.UrlCBApi + "api/SaiGons/SGPostKhuDoan", kd);                   
                    kd.ID = objInsert.ID;
                    bsKhuDoan.Add(kd);
                    bsKhuDoan.MoveLast();
                }
                else
                {
                    kd.Modifydate = DateTime.Now;
                    kd.Modifyby = AppGlobal.User.Username;
                    kd.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<SGKhuDoan>(Configuration.UrlCBApi + "api/SaiGons/SGPutKhuDoan?id=" + kd.ID, kd);
                    bsKhuDoan.EndEdit();
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
        private void txtGaXPName_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGaXPName.Text))
            {
                txtGaXPID.Text = string.Empty;
                return;
            }
            if (AppGlobal.GaDic.ContainsValue(txtGaXPName.Text))
                txtGaXPID.Text = AppGlobal.GaDic.Where(x=>x.Value==txtGaXPName.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtGaXPID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng ga hãy nhập lại.");
                txtGaXPName.Focus();
                txtGaXPName.SelectAll();
            }
        }
        private void txtGaKTName_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGaKTName.Text))
            {
                txtGaKTID.Text = string.Empty;
                return;
            }
            if (AppGlobal.GaDic.ContainsValue(txtGaKTName.Text))
                txtGaKTID.Text = AppGlobal.GaDic.Where(x => x.Value == txtGaKTName.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtGaKTID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng ga hãy nhập lại.");
                txtGaKTName.Focus();
                txtGaKTName.SelectAll();
            }
        }
        private void txtGaDT1Name_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGaDT1Name.Text))
            {
                txtGaDT1ID.Text = string.Empty;
                return;
            }
            if (AppGlobal.GaDic.ContainsValue(txtGaDT1Name.Text))
                txtGaDT1ID.Text = AppGlobal.GaDic.Where(x => x.Value == txtGaDT1Name.Text).FirstOrDefault().Key.ToString();
            else
            {
                txtGaDT1ID.Text = string.Empty;
                Library.DialogHelper.Error("Không đúng ga hãy nhập lại.");
                txtGaDT1Name.Focus();
                txtGaDT1Name.SelectAll();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
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

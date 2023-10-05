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
    public partial class MienPhatForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        public MienPhatForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtTyLe);            
            string[] arRays = new string[] { "Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm", "Khác" };
            cboLoaiBC.Items.AddRange(arRays);
            cboLoaiBC.SelectedIndex = 0;
            for (int i = 1; i <= 12; i++)
            {
                cboTuThang.Items.Add(i.ToString());
                cboDenThang.Items.Add(i.ToString());
                cboThangAdd.Items.Add(i.ToString());
            }

            int year = DateTime.Today.Year;
            for (int i = year - 10; i <= year + 1; i++)
            {
                cboTuNam.Items.Add(i.ToString());
                cboDenNam.Items.Add(i.ToString());
                cboNamAdd.Items.Add(i.ToString());
            }                   
            cboTuThang.Text = DateTime.Today.Month.ToString();
            cboDenThang.Text = cboTuThang.Text;
            cboThangAdd.Text = cboTuThang.Text;            
            cboTuNam.Text = year.ToString();
            cboDenNam.Text = cboTuNam.Text;
            cboNamAdd.Text = cboTuNam.Text;
            cboDenThang.Enabled = false;
            cboDenNam.Enabled = false;
            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();            
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
                else
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            cboDonVi.DataSource = donViTT;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = 0;
            ShowControl(false);
        }
        
        private void cboLoaiBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTuThang.Enabled = true;
            cboTuNam.Enabled = true;
            if(cboLoaiBC.SelectedIndex==0)
            {               
                cboDenThang.Enabled = false;
                cboDenNam.Enabled = false;
                cboDenThang.Text = cboTuThang.Text;
                cboDenNam.Text = cboTuNam.Text; 
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                cboDenThang.Enabled = false;
                cboDenNam.Enabled = false;
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text)<4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3"; 
                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text)<7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6";
                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text)<10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9";
                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12";
                }
                cboDenNam.Text = cboTuNam.Text;                
            }
            else if (cboLoaiBC.SelectedIndex == 2 || cboLoaiBC.SelectedIndex == 3 || cboLoaiBC.SelectedIndex == 4)
            {
                cboTuThang.Enabled = false;
                cboDenThang.Enabled = false;
                cboDenNam.Enabled = false;
                cboTuThang.Text = "1";
                if (cboLoaiBC.SelectedIndex == 2)
                {
                    cboDenThang.Text = "6";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9"; 
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";
                }
                cboDenNam.Text = cboTuNam.Text;                
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {
                cboTuThang.Enabled = true;
                cboDenThang.Enabled = true;
                cboDenNam.Enabled = true;
                DateTime tuNgay = new DateTime(int.Parse(cboTuNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboDenNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;               
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                    cboDenNam.Text = cboTuNam.Text;
                }                    
            }            
        }

        private void cboTuThang_Validated(object sender, EventArgs e)
        {
            if (cboLoaiBC.SelectedIndex == 0)
            {
                cboDenThang.Text = cboTuThang.Text;
                cboDenNam.Text = cboTuNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text) < 4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3"; 
                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text) < 7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6"; 
                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text) < 10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9"; 
                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12"; 
                }
                cboDenNam.Text = cboTuNam.Text;               
            }
            else if (cboLoaiBC.SelectedIndex == 2 || cboLoaiBC.SelectedIndex == 3 || cboLoaiBC.SelectedIndex == 4)
            {
                cboTuThang.Text = "1";
                if (cboLoaiBC.SelectedIndex == 2)
                {
                    cboDenThang.Text = "6";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9";
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";
                }
                cboDenNam.Text = cboTuNam.Text;                
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {               
                DateTime tuNgay = new DateTime(int.Parse(cboTuNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboDenNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                    cboDenNam.Text = cboTuNam.Text;
                }
            }
        }

        private void cboTuNam_Validated(object sender, EventArgs e)
        {
            if (cboLoaiBC.SelectedIndex == 5)
            {
                DateTime tuNgay = new DateTime(int.Parse(cboTuNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboDenNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                    cboDenNam.Text = cboTuNam.Text;
                }
            }
            else
                cboDenNam.Text = cboTuNam.Text;
        }
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }

        private void fnTraTim()
        {
            try
            {
                bsMienPhat.DataSource = null;
                base.Cursor = Cursors.WaitCursor;               
                string data = "?MaDV=" + cboDonVi.SelectedValue.ToString();
                data += "&TuThang=" + cboTuThang.Text;
                data += "&DenThang=" + cboDenThang.Text;
                data += "&TuNam="+ cboTuNam.Text;
                data += "&DenNam=" + cboTuNam.Text;
                List<MienPhat> listMienPhat = HttpHelper.GetList<MienPhat>(Configuration.UrlCBApi + "api/MienPhats/GetMienPhat"+data);
                if (listMienPhat.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsMienPhat.DataSource = listMienPhat;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listMienPhat.Count.ToString("N0");
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsMienPhat.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboThangAdd.Enabled = b;
            cboNamAdd.Enabled = b;
            txtSoCB.Enabled = b;
            txtTyLe.Enabled = b;
            txtLyDo.Enabled = b;
            btnThem.Enabled = !b;
            if(b==false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow==null ? false : true;
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
            cboThangAdd.Text = DateTime.Today.Month.ToString();
            cboNamAdd.Text = DateTime.Today.Year.ToString();
            txtSoCB.ResetText();
            txtTyLe.ResetText();
            txtLyDo.ResetText();
        }

        private void BindControl()
        {
            MienPhat mp = bsMienPhat.Current as MienPhat;
            if (mp != null)
            {
                cboThangAdd.Text = mp.ThangDT.ToString();
                cboNamAdd.Text = mp.NamDT.ToString();
                txtSoCB.Text = mp.SoCB.ToString();
                txtTyLe.Text = mp.TyLe.ToString();
                txtLyDo.Text = mp.LyDo;
            }           
        }

        private MienPhat BindObject()
        {
            MienPhat mp = new MienPhat();
            if (!bThem)
                mp = bsMienPhat.Current as MienPhat;
            else
            {
                mp.CoBaoID = 0;
                mp.SoCB = txtSoCB.Text;
                mp.MaDV= cboDonVi.SelectedValue.ToString();
                mp.ThangDT= int.Parse(cboThangAdd.Text);
                mp.NamDT= int.Parse(cboNamAdd.Text);
            }
            if (string.IsNullOrEmpty(txtTyLe.Text))
                txtTyLe.Text = "0";
            mp.TyLe = decimal.Parse(txtTyLe.Text);
            mp.LyDo = txtLyDo.Text;
            return mp;
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
            cboThangAdd.Enabled = false;
            cboNamAdd.Enabled = false;
            txtSoCB.Enabled = false;
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
            MienPhat mp = bsMienPhat.Current as MienPhat;
            if (Library.DialogHelper.Confirm("Xóa miễn phạt này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?id=" + mp.CoBaoID;
                data += "&manv=" + AppGlobal.User.Username;
                data += "&tennv=" + AppGlobal.User.FullName;
                var opStatus = HttpHelper.Delete<MienPhat>(Configuration.UrlCBApi + "api/MienPhats/DeleteMienPhat" + data);
                if (opStatus.Result.CoBaoID == mp.CoBaoID && opStatus.Result.MaDV == mp.MaDV)
                    bsMienPhat.Remove(mp);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }            
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                MienPhat mp = BindObject();
                if (bThem)
                {
                    mp.CreatedBy = AppGlobal.User.Username;
                    mp.CreatedName = AppGlobal.User.FullName;
                    mp.CreatedDate = DateTime.Now;
                    mp.ModifyBy = mp.CreatedBy;
                    mp.ModifyName = mp.CreatedName;
                    mp.ModifyDate = mp.CreatedDate;
                    var objInsert = await HttpHelper.Post<MienPhat>(Configuration.UrlCBApi + "api/MienPhats/PostMienPhat", mp);
                    mp = objInsert;
                    bsMienPhat.Add(mp);
                    bsMienPhat.MoveLast();
                }
                else
                {
                    mp.ModifyBy = AppGlobal.User.Username;
                    mp.ModifyName = AppGlobal.User.FullName;
                    mp.ModifyDate = DateTime.Now;
                    var objUpdate = await HttpHelper.Put<MienPhat>(Configuration.UrlCBApi + "api/MienPhats/PutMienPhat", mp);
                    bsMienPhat.EndEdit();
                    dataGridView1.Refresh();
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

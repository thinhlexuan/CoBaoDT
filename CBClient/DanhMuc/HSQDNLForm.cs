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
    public partial class HSQDNLForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        private HeSoQdnl HeSoinfo = new HeSoQdnl();
        bool bThem = false;
        public HSQDNLForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtHSLit);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtHSKg);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtNhietDoQD);
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
            var donVi = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();           
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    donVi = donVi.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    donVi = donVi.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
                else
                    donVi = donVi.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            var donViTT = donVi.OrderBy(x => x.TenDV).ToList();
            cboDonViTT.DataSource = donViTT;
            cboDonViTT.DisplayMember = "TenDV";
            cboDonViTT.ValueMember = "MaDV";
            cboDonViTT.SelectedIndex = 0;

            var donViAdd = donVi.OrderBy(x => x.TenDV).ToList();
            cboDonVi.DataSource = donViAdd;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = -1;

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
                bsHSQDNL.DataSource = null;
                base.Cursor = Cursors.WaitCursor;               
                string data = "?MaDV=" + cboDonViTT.SelectedValue.ToString();
                data += "&TuThang=" + cboTuThang.Text;
                data += "&DenThang=" + cboDenThang.Text;
                data += "&TuNam="+ cboTuNam.Text;
                data += "&DenNam=" + cboTuNam.Text;
                List<HeSoQdnl> listHSQDNL = HttpHelper.GetList<HeSoQdnl>(Configuration.UrlCBApi + "api/HeSoQdnls/GetByTraTim"+data);
                if (listHSQDNL.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsHSQDNL.DataSource = listHSQDNL;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listHSQDNL.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsHSQDNL.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboDonVi.Enabled = b;
            cboThangAdd.Enabled = b;
            cboNamAdd.Enabled = b;
            txtHSLit.Enabled = b;
            txtHSKg.Enabled = b;
            txtNhietDoQD.Enabled = b;
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
            txtHSLit.ResetText();
            txtHSKg.ResetText();
            txtNhietDoQD.ResetText();
        }

        private void BindControl()
        {
            HeSoQdnl hs = bsHSQDNL.Current as HeSoQdnl;
            if (hs != null)
            {
                if (!String.IsNullOrWhiteSpace(hs.MaDv)) cboDonVi.SelectedValue = hs.MaDv;
                cboThangAdd.Text = hs.Thang.ToString();
                cboNamAdd.Text = hs.Nam.ToString();
                txtHSLit.Text = hs.HesoLit.ToString();
                txtHSKg.Text = hs.HesoKg.ToString();
                txtNhietDoQD.Text = hs.NhietDo.ToString();
            }
        }

        private HeSoQdnl BindObject()
        {
            HeSoQdnl hs = new HeSoQdnl();
            if (!bThem)
                hs = bsHSQDNL.Current as HeSoQdnl;
            hs.ID = bThem ? 0 : hs.ID;
            hs.MaDv = bThem ? cboDonVi.SelectedValue.ToString() : hs.MaDv;
            hs.Thang = int.Parse(cboThangAdd.Text);
            hs.Nam = int.Parse(cboNamAdd.Text);
            hs.HesoLit = String.IsNullOrWhiteSpace(txtHSLit.Text) ? 0 : decimal.Parse(txtHSLit.Text);          
            hs.HesoKg = String.IsNullOrWhiteSpace(txtHSKg.Text) ? 0 : decimal.Parse(txtHSKg.Text);           
            hs.NhietDo = String.IsNullOrWhiteSpace(txtNhietDoQD.Text) ? 15 : decimal.Parse(txtNhietDoQD.Text);
            return hs;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            bsHSQDNL.MoveLast();
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
            HeSoQdnl hs = BindObject();
            if (Library.DialogHelper.Confirm("Xóa hệ số này không?") == System.Windows.Forms.DialogResult.Yes)
            {               
                var opStatus = HttpHelper.Delete<HeSoQdnl>(Configuration.UrlCBApi + "api/HeSoQdnls/DeleteByID?id=" + hs.ID);
                if (opStatus.Result.ID == hs.ID)
                    bsHSQDNL.Remove(hs);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }            
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                HeSoQdnl hs = BindObject();
                if (hs.HesoLit <= 0 || hs.HesoLit>=1)
                    throw new Exception("Nhập hệ số quy đổi chưa đúng.");                
                if (bThem)
                {
                    var objInsert = await HttpHelper.Post<HeSoQdnl>(Configuration.UrlCBApi + "api/HeSoQdnls/PostByID", hs);                    
                    hs.ID = objInsert.ID;
                    bsHSQDNL.Add(hs);
                    bsHSQDNL.MoveLast();
                }
                else
                {
                    var objUpdate = await HttpHelper.Put<HeSoQdnl>(Configuration.UrlCBApi + "api/HeSoQdnls/PutByID?id=" + hs.ID, hs);
                    bsHSQDNL.EndEdit();
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

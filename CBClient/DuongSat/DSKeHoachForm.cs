using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLDaos;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Microsoft.Reporting.WinForms;

namespace CBClient.DuongSat
{
    public partial class DSKeHoachForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        string loaiBC = string.Empty;
        long iD = 0;
        private List<LoaiKeHoach> listLoaiKH = new List<LoaiKeHoach>();
        public DSKeHoachForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this); 
            FormHelper.AddKeyPressEventHandlerForDecimal(txtYenVien);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtHaNoi);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtVinh);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDaNang);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtSaiGon);

            listLoaiKH = HttpHelper.GetList<LoaiKeHoach>(Configuration.UrlCBApi + "api/KeHoachs/GetLoaiKeHoach")
                  .OrderBy(x => x.MaLoai).ToList();
            cboLoaiKH.DataSource = listLoaiKH;
            cboLoaiKH.DisplayMember = "TenLoai";
            cboLoaiKH.ValueMember = "MaLoai";
            cboLoaiKH.SelectedIndex = 0;

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

            string[] arRayTTs = new string[] { "ALL","Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm" };
            cboNhomKHTT.Items.AddRange(arRayTTs);
            cboNhomKHTT.SelectedIndex = 0;

            string[] arRays = new string[] { "Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm" };
            cboNhomKH.Items.AddRange(arRays);
            cboNhomKH.SelectedIndex = 0;

            for (int i = 1; i <= 12; i++)
            {
                cboKyKHTT.Items.Add(i.ToString());
                cboKyKH.Items.Add(i.ToString());

            }
            cboKyKHTT.SelectedIndex = 0;
            cboKyKHTT.Enabled = false;
            cboKyKH.SelectedIndex = 0;

            int year = DateTime.Today.Year;
            for (int i = year - 10; i <= year + 1; i++)
            {
                cboNamKHTT.Items.Add(i.ToString());
                cboNamKH.Items.Add(i.ToString());
            }
            cboNamKHTT.Text = year.ToString();
            cboNamKH.Text = year.ToString();

            loaiBC = "Năm " + cboNamKHTT.Text;
            ShowControl(false);
        }
        private void cboNhomKHTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKyKHTT.Enabled = false;
            cboKyKHTT.Items.Clear();
            if (cboNhomKHTT.SelectedIndex == 0 || cboNhomKHTT.SelectedIndex == 5)
            {
                cboKyKHTT.Enabled = false;
                cboKyKHTT.Text = "1";
                loaiBC = "Năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 1)
            {
                cboKyKHTT.Enabled = true;
                for (int i = 1; i <= 12; i++)
                {
                    cboKyKHTT.Items.Add(i.ToString());
                }
                cboKyKHTT.SelectedIndex = 0;
                loaiBC = "Tháng " + cboKyKHTT.Text + " năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 2)
            {
                cboKyKHTT.Enabled = true;
                for (int i = 1; i <= 4; i++)
                {
                    cboKyKHTT.Items.Add(i.ToString());

                }
                cboKyKHTT.SelectedIndex = 0;
                loaiBC = "Quý I năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 3)
            {
                cboKyKHTT.Enabled = true;
                for (int i = 1; i <= 2; i++)
                {
                    cboKyKHTT.Items.Add(i.ToString());

                }
                cboKyKHTT.SelectedIndex = 0;
                loaiBC = "Sáu tháng đầu năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 4)
            {
                cboKyKHTT.Enabled = false;
                cboKyKHTT.Text = "1";
                loaiBC = "Chín tháng năm " + cboNamKHTT.Text;
            }
        }
        private void cboKyKHTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhomKHTT.SelectedIndex == 0 || cboNhomKHTT.SelectedIndex == 5)
            {                
                loaiBC = "Năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 1)
            {                
                loaiBC = "Tháng " + cboKyKHTT.Text + " năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 2)
            {
                if(cboKyKHTT.SelectedIndex==0)
                    loaiBC = "Quý I năm " + cboNamKHTT.Text;
                else if (cboKyKHTT.SelectedIndex == 1)
                    loaiBC = "Quý II năm " + cboNamKHTT.Text;
                else if (cboKyKHTT.SelectedIndex == 2)
                    loaiBC = "Quý III năm " + cboNamKHTT.Text;
                else if (cboKyKHTT.SelectedIndex == 3)
                    loaiBC = "Quý IV năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 3)
            {
                if (cboKyKHTT.SelectedIndex == 0)
                    loaiBC = "Sáu tháng đầu năm " + cboNamKHTT.Text;
                else if (cboKyKHTT.SelectedIndex == 1)
                    loaiBC = "Sáu tháng cuối năm " + cboNamKHTT.Text;
            }
            else if (cboNhomKHTT.SelectedIndex == 4)
            {
                cboKyKHTT.Enabled = false;
                cboKyKHTT.Text = "1";
                loaiBC = "Chín tháng năm " + cboNamKHTT.Text;
            }
        }
        private void cboNhomKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKyKH.Items.Clear();
            if (cboNhomKH.SelectedIndex == 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    cboKyKH.Items.Add(i.ToString());
                }
            }
            else if (cboNhomKH.SelectedIndex == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    cboKyKH.Items.Add(i.ToString());
                }
            }
            else if (cboNhomKH.SelectedIndex == 2)
            {
                for (int i = 1; i <= 2; i++)
                {
                    cboKyKH.Items.Add(i.ToString());
                }
            }
            else if (cboNhomKH.SelectedIndex == 3 || cboNhomKH.SelectedIndex == 4)
            {
                for (int i = 1; i <= 1; i++)
                {
                    cboKyKH.Items.Add(i.ToString());
                }
            }
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }

        private void fnTraTim()
        {
            try
            {
                bsKeHoach.DataSource = null;
                base.Cursor = Cursors.WaitCursor;
                string data = "?nhomKH=" + cboNhomKHTT.Text;               
                data += "&kyKH=" + int.Parse(cboKyKHTT.Text);
                data += "&namKH=" + int.Parse(cboNamKHTT.Text);
                var listKeHoach = HttpHelper.GetList<KeHoachView>(Configuration.UrlCBApi + "api/KeHoachs/GetKeHoachView" + data)
                   .OrderBy(x => x.NamKH).ThenBy(x => x.MaLoai).ToList();                
                if (listKeHoach.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }              
                bsKeHoach.DataSource = listKeHoach;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listKeHoach.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsKeHoach.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboLoaiKH.Enabled = b;
            cboNhomKH.Enabled = b;
            cboKyKH.Enabled = b;
            cboNamKH.Enabled = b;
            txtYenVien.Enabled = b;
            txtHaNoi.Enabled = b;                   
            txtVinh.Enabled = b;
            txtDaNang.Enabled = b;
            txtSaiGon.Enabled = b;
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
            iD = 0;
            cboLoaiKH.SelectedIndex = -1;
            cboNhomKH.SelectedIndex = -1;
            cboKyKH.SelectedIndex = -1;
            cboNamKH.SelectedIndex = -1;
            txtYenVien.ResetText();
            txtHaNoi.ResetText();
            txtVinh.ResetText();
            txtDaNang.ResetText();
            txtSaiGon.ResetText();
        }

        private void BindControl()
        {
            KeHoachView kh = bsKeHoach.Current as KeHoachView;
            if (kh != null)
            {
                iD = kh.ID;
                cboLoaiKH.SelectedValue = kh.MaLoai.ToString();
                cboLoaiKH.Text = kh.TenLoai;
                cboNhomKH.Text = kh.NhomKH;
                cboKyKH.Text = kh.KyKH.ToString();
                cboNamKH.Text = kh.NamKH.ToString();
                txtYenVien.Text = kh.YV.ToString();
                txtHaNoi.Text = kh.HN.ToString();
                txtVinh.Text = kh.VIN.ToString();
                txtDaNang.Text = kh.DN.ToString();
                txtSaiGon.Text = kh.SG.ToString();                
            }
        }

        private KeHoach BindObject()
        {
            KeHoach kh = new KeHoach();
            kh.ID = iD;
            kh.MaLoai = short.Parse(cboLoaiKH.SelectedValue.ToString());
            kh.NhomKH = cboNhomKH.Text;
            kh.KyKH = short.Parse(cboKyKH.Text);
            kh.NamKH = short.Parse(cboNamKH.Text);            
            kh.YV = String.IsNullOrWhiteSpace(txtYenVien.Text) ? 0 : decimal.Parse(txtYenVien.Text);
            kh.HN = String.IsNullOrWhiteSpace(txtHaNoi.Text) ? 0 : decimal.Parse(txtHaNoi.Text);
            kh.VIN = String.IsNullOrWhiteSpace(txtVinh.Text) ? 0 : decimal.Parse(txtVinh.Text);
            kh.DN = String.IsNullOrWhiteSpace(txtDaNang.Text) ? 0 : decimal.Parse(txtDaNang.Text);
            kh.SG = String.IsNullOrWhiteSpace(txtSaiGon.Text) ? 0 : decimal.Parse(txtSaiGon.Text);
            return kh;
        }

        private void BindObjectView(KeHoach kh, ref KeHoachView khView)
        {           
            khView.ID = kh.ID;
            khView.MaLoai = kh.MaLoai;
            LoaiKeHoach loaiKH = listLoaiKH.Where(x => x.MaLoai == kh.MaLoai).FirstOrDefault();
            khView.SoTT = loaiKH.SoTT;
            khView.TenLoai = loaiKH.TenLoai;
            khView.DonVi = loaiKH.DonVi;
            khView.NhomKH = kh.NhomKH;
            khView.KyKH = kh.KyKH;
            khView.NamKH = kh.NamKH;
            khView.YV = kh.YV;
            khView.HN = kh.HN;
            khView.VIN = kh.VIN;
            khView.DN = kh.DN;
            khView.SG = kh.SG;
            khView.CreatedDate = kh.CreatedDate;
            khView.CreatedBy = kh.CreatedBy;
            khView.CreatedName = kh.CreatedName;
            khView.ModifyDate = kh.ModifyDate;
            khView.ModifyBy = kh.ModifyBy;
            khView.ModifyName = kh.ModifyName;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            bsKeHoach.MoveLast();
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
            KeHoachView khView = bsKeHoach.Current as KeHoachView;            
            if (Library.DialogHelper.Confirm("Xóa kế hoạch này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?id=" + khView.ID;
                data += "&maNV=" + AppGlobal.User.Username;
                data += "&tenNV=" + AppGlobal.User.FullName;
                var opStatus = HttpHelper.Delete<KeHoach>(Configuration.UrlCBApi + "api/KeHoachs/DeleteKeHoach" + data);
                if (opStatus.Result.ID == khView.ID)
                    bsKeHoach.Remove(khView);                   
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                KeHoach kh = BindObject();
                KeHoachView khView = new KeHoachView();
                if (bThem)
                {
                    kh.CreatedDate = DateTime.Now;
                    kh.CreatedBy = AppGlobal.User.Username;
                    kh.CreatedName = AppGlobal.User.FullName;
                    kh.ModifyDate = kh.CreatedDate;
                    kh.ModifyBy = kh.CreatedBy;
                    kh.ModifyName = kh.CreatedName;
                    var objInsert = await HttpHelper.Post<KeHoach>(Configuration.UrlCBApi + "api/KeHoachs/PostKeHoach", kh);                    
                    kh = objInsert;
                   
                }
                else
                {
                    khView = bsKeHoach.Current as KeHoachView;
                    kh.CreatedDate = khView.CreatedDate;
                    kh.CreatedBy = khView.CreatedBy;
                    kh.CreatedName = khView.CreatedName;
                    kh.ModifyDate = DateTime.Now;
                    kh.ModifyBy = AppGlobal.User.Username;
                    kh.ModifyName = AppGlobal.User.FullName;
                    var objUpdate = await HttpHelper.Put<KeHoach>(Configuration.UrlCBApi + "api/KeHoachs/PutKeHoach?id=" + kh.ID, kh);
                    kh = objUpdate;
                }
                BindObjectView(kh,ref khView);
                if (!bThem)
                {   
                    bsKeHoach.EndEdit();
                }
                else
                {
                    bsKeHoach.Add(khView);
                    bsKeHoach.MoveLast();
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
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (bsKeHoach.Count<= 0) return;
            ShowReport();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            BindControl();
        }
        private void ShowReport()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                string maDV = cboDonViTT.SelectedValue.ToString();
                string nhomKH = cboNhomKHTT.Text;
                short kyKH = short.Parse(cboKyKHTT.Text);
                short namKH = short.Parse(cboNamKHTT.Text);

                int TongSoBG = 0;
                List<BCKeHoachInfo> list = new List<BCKeHoachInfo>();
                //Lấy dữ liệu
                BaoCaoDAO.NapBCKeHoach(maDV, nhomKH, kyKH, namKH, listLoaiKH, ref TongSoBG, ref list);
                //Kiểm tra xem có dữ liệu hay không
                if (list.Count <= 0)
                    throw new Exception("Không có dữ liệu.");

                List<ReportParameter> rptParamList = new List<ReportParameter>();
                var dVQL = AppGlobal.DonviDMList.Where(x => x.MaDV == maDV).FirstOrDefault();
                string tenDV = dVQL.TenDV.ToUpper();
                string tenDVCha = AppGlobal.DonviDMList.Where(x => x.MaDV == dVQL.MaCha).FirstOrDefault().TenDV;
                ReportParameter rptParam = new ReportParameter("prmDonvicha", tenDVCha.ToUpper());
                rptParamList.Add(rptParam);


                rptParam = new ReportParameter("prmDonvicon", tenDV);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmSocv", "Số:................/BC-ĐM.");
                rptParamList.Add(rptParam);
               
                rptParam = new ReportParameter("prmLoaibc", loaiBC + " - " + cboDonViTT.Text);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayth", "Ngày " + DateTime.Today.ToString("dd") + " tháng " + DateTime.Today.ToString("MM") + " năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmLapbieu", "NGƯỜI LẬP BIỂU");
                rptParamList.Add(rptParam);                

                rptParam = new ReportParameter("prmPhongban", AppGlobal.User.MaDVQL != "C12" ? "PHÒNG KẾ HOẠCH VẬT TƯ" : "BAN KẾ HOẠCCH KINH DOANH");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmGiamdoc", AppGlobal.User.MaDVQL != "C12" ? "GIÁM ĐỐC":"TỔNG GIÁM ĐỐC");
                rptParamList.Add(rptParam);                

                string rptResource = "CBClient.Report.RptKeHoach.rdlc";
                string rptName = "BaoCaoDS";
                
                BaoCao.PreViewDialogKH PrintDlg = new BaoCao.PreViewDialogKH(rptResource, rptName, list, rptParamList);
                DialogResult aFormResult = PrintDlg.ShowDialog();
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                base.Cursor = Cursors.Default;
                return;
            }
        }

       
    }
}

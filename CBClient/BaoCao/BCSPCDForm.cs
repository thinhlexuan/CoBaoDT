using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CBClient.Library;
using CBClient.BLLDaos;
using Microsoft.Reporting.WinForms;
using CBClient.BLLTypes;

namespace CBClient.BaoCao
{
    public partial class BCSPCDForm : DevComponents.DotNetBar.Metro.MetroForm
    {   
        string loaiBC = string.Empty;
        string cacThang = string.Empty;
        public BCSPCDForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            string[] arRays = new string[] { "Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm", "Khác" };
            cboLoaiBC.Items.AddRange(arRays);
            cboLoaiBC.SelectedIndex = 0;
            for (int i = 1; i <= 12; i++)
            {
                cboTuThang.Items.Add(i.ToString());
                cboDenThang.Items.Add(i.ToString());
            }

            int year = DateTime.Today.Year;
            for (int i = year - 10; i <= year + 1; i++)
            {
                cboTuNam.Items.Add(i.ToString());
                cboDenNam.Items.Add(i.ToString());
            }
            //cboTuThang.Text = "10";            
            cboTuThang.Text = DateTime.Today.Month.ToString();
            cboDenThang.Text = cboTuThang.Text;
            //cboTuNam.Text = "2014";
            cboTuNam.Text = year.ToString();
            cboDenNam.Text = cboTuNam.Text;
            cboDenThang.Enabled = false;
            cboDenNam.Enabled = false;           
            loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboTuNam.Text;
            cacThang = cboTuThang.Text;
            DataView dvDonVi = AppGlobal.LookupDS.Tables["DonVi"].Copy().DefaultView;
            if (AppGlobal.User.MaDV == 1 || AppGlobal.User.MaDV == 2 || AppGlobal.User.MaDV == 3)
                dvDonVi.RowFilter = "MaCha>=1 OR MaDV=1";
            //else if (AppGlobal.User.MaDV == 2)
            //    dvDonVi.RowFilter = "MaCha=2 OR MaDV=2";
            //else if (AppGlobal.User.MaDV == 3)
            //    dvDonVi.RowFilter = "MaCha=3 OR MaDV=3";
            else
            {
                dvDonVi.RowFilter = "MaDV=" + AppGlobal.User.MaDV;
                cboDonVi.Enabled = false;
            }
            cboDonVi.DataSource = dvDonVi;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = 0;

            DataView dvLoaiMay = AppGlobal.LookupDS.Tables["LoaiMay"].Copy().DefaultView;
            DataTable dtLoaiMay = new DataTable();
            dtLoaiMay.Columns.Add("MaLM", typeof(String));
            dtLoaiMay.Columns.Add("TenLM", typeof(String));
            DataRow aRow = dtLoaiMay.NewRow();
            aRow["MaLM"] = "ALL";
            aRow["TenLM"] = "Tất cả các loại máy";
            dtLoaiMay.Rows.Add(aRow);
            foreach (DataRowView dv in dvLoaiMay)
            {
                aRow = dtLoaiMay.NewRow();
                aRow["MaLM"] = dv["LoaiMayID"].ToString();
                aRow["TenLM"] = dv["LoaiMayName"].ToString();
                dtLoaiMay.Rows.Add(aRow);
            }
            cboLoaiMay.DataSource = dtLoaiMay;
            cboLoaiMay.DisplayMember = "TenLM";
            cboLoaiMay.ValueMember = "MaLM";
            cboLoaiMay.SelectedIndex = 0;
        }

        private void BCKTKTForm_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
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
                loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboTuNam.Text;
                cacThang = cboTuThang.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                cboDenThang.Enabled = false;
                cboDenNam.Enabled = false;
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text)<4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3";
                    loaiBC = "Quý I";
                    cacThang = "1,2,3";

                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text)<7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6";
                    loaiBC = "Quý II";
                    cacThang = "4,5,6";

                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text)<10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9";
                    loaiBC = "Quý III";
                    cacThang = "7,8,9";

                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12";
                    loaiBC = "Quý IV";
                    cacThang = "10,11,12";
                }
                cboDenNam.Text = cboTuNam.Text;
                loaiBC += " năm " + cboTuNam.Text;
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
                    loaiBC = "Sáu tháng năm ";
                    cacThang = "1,2,3,4,5,6";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9";
                    loaiBC = "Chín tháng năm ";
                    cacThang = "1,2,3,4,5,6,7,8,9";
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";     
                    loaiBC = "Năm ";
                    cacThang = "1,2,3,4,5,6,7,8,9,10,11,12";
                }
                cboDenNam.Text = cboTuNam.Text;
                loaiBC +=cboTuNam.Text;
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
                loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboTuNam.Text;
                cacThang = cboTuThang.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text) < 4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3";
                    loaiBC = "Quý I";
                    cacThang = "1,2,3";
                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text) < 7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6";
                    loaiBC = "Quý II";
                    cacThang = "4,5,6";

                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text) < 10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9";
                    loaiBC = "Quý III";
                    cacThang = "7,8,9";
                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12";
                    loaiBC = "Quý IV";
                    cacThang = "10,11,12";
                }
                cboDenNam.Text = cboTuNam.Text;
                loaiBC += " năm " + cboTuNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 2 || cboLoaiBC.SelectedIndex == 3 || cboLoaiBC.SelectedIndex == 4)
            {
                cboTuThang.Text = "1";
                if (cboLoaiBC.SelectedIndex == 2)
                {
                    cboDenThang.Text = "6";
                    loaiBC = "Sáu tháng năm ";
                    cacThang = "1,2,3,4,5,6";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9";
                    loaiBC = "Chín tháng năm ";
                    cacThang = "1,2,3,4,5,6,7,8,9";
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";
                    loaiBC = "Năm ";
                    cacThang = "1,2,3,4,5,6,7,8,9,10,11,12";
                }
                cboDenNam.Text = cboTuNam.Text;
                loaiBC += cboTuNam.Text;
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
            try
            {
                string tableName = string.Empty;
                if (cboDonVi.SelectedValue.ToString() == "4")
                    tableName = "DoanThongYV";
                else if (cboDonVi.SelectedValue.ToString() == "5")
                    tableName = "DoanThongHN";
                else if (cboDonVi.SelectedValue.ToString() == "6")
                    tableName = "DoanThongVI";
                else if (cboDonVi.SelectedValue.ToString() == "7")
                    tableName = "DoanThongDN";
                else if (cboDonVi.SelectedValue.ToString() == "8")
                    tableName = "DoanThongSG";

                base.Cursor = Cursors.WaitCursor;
                short maDV = short.Parse(cboDonVi.SelectedValue.ToString());
                int thangDT = int.Parse(cboTuThang.Text);
                int namDT = int.Parse(cboTuNam.Text);
                string loaiMay = cboLoaiMay.SelectedValue.ToString();
                int TongSoBG = 0;
                List<BCSPCDInfo> listTH = new List<BCSPCDInfo>();                             
                //Lấy dữ liệu
                BaoCaoDAO.NapBCSPCD(maDV, tableName, cacThang, namDT,loaiMay, ref TongSoBG, ref listTH);
                //Kiểm tra xem có dữ liệu hay không
                if (listTH.Count <= 0)
                    throw new Exception("Không có dữ liệu.");
                
                List<ReportParameter> rptParamList = new List<ReportParameter>();
                //short maCha = AppGlobal.User.MaCha == 0 ? (short)1 : AppGlobal.User.MaCha;
                short maCha = (short)1;
                DataRow[] foundRows = AppGlobal.LookupDS.Tables["DonVi"].Select("MaDV = " + maCha);
                ReportParameter rptParam = new ReportParameter("prmDonvicha", foundRows[0]["TenDV"].ToString().ToUpper());
                rptParamList.Add(rptParam);

                string tenDV = maDV == 1 ? AppGlobal.User.PhongBan.ToUpper() : cboDonVi.Text.ToUpper();
                if (maDV >= 4) tenDV = "CHI NHÁNH " + cboDonVi.Text.ToUpper();
                rptParam = new ReportParameter("prmDonvicon", tenDV);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmSocv", "Số:................/BC-ĐM.");
                rptParamList.Add(rptParam);

                string strLoaiDM = "Loại đầu máy: " + cboLoaiMay.Text + " (" + cboLoaiMay.SelectedValue.ToString() + ") ";          
                rptParam = new ReportParameter("prmNhombc", strLoaiDM);
                rptParamList.Add(rptParam);

                if (cboLoaiBC.SelectedIndex == 5)
                    loaiBC = "Từ tháng " + cboTuThang.Text + " năm " + cboTuNam.Text + " đến tháng " + cboDenThang.Text + " năm " + cboDenNam.Text;
                rptParam = new ReportParameter("prmLoaibc", loaiBC);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayth", "Ngày " + DateTime.Today.ToString("dd") + " tháng " + DateTime.Today.ToString("MM") + " năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmLapbieu", "NGƯỜI LẬP BIỂU");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmPhongban", maDV == 6 ? "PHÒNG KẾ HOẠCH VẬT TƯ" : "PHÒNG BAN");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmGiamdoc", maDV < 4 ? "TỔNG GIÁM ĐỐC" : "GIÁM ĐỐC");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoilb", maDV == 6 ? "Tống Thị Thu" : " ");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoipb", maDV == 6 ? "Lê Hoài Anh" : " ");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoigd", maDV == 6 ? "Trần Anh Tú" : " ");
                rptParamList.Add(rptParam);

                ShowReport("RptSPCD", "BaoCaoDS", listTH, rptParamList);
               lblTableCount.Text = "Tổng số bản ghi:" + TongSoBG.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reportViewer1.Reset();
                base.Cursor = Cursors.Default;
                return;
            }
        }

        private void ShowReport(string rptResource, string rptName,
      object rptValue, List<ReportParameter> rptParamList)
        {
            try
            {
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportEmbeddedResource ="CBClient.Report." + rptResource + ".rdlc";

                ReportDataSource rds = new ReportDataSource();
                rds.Name = rptName;                
                rds.Value = rptValue;

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.LocalReport.SetParameters(rptParamList);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;                
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message, ex);
            }
        }
        
    }
}

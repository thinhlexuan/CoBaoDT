using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.Library;
using CBClient.BLLDaos;
using Microsoft.Reporting.WinForms;
using CBClient.BLLTypes;

namespace CBClient.BaoCao
{
    public partial class BCDauMoForm : DevComponents.DotNetBar.Metro.MetroForm
    {   
        string loaiBC = string.Empty;
        string cacThang = string.Empty;
        public BCDauMoForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            var loaidmTT = (from ct in AppGlobal.DMLoaidmList
                            select new
                             {
                                 MaDM = (short)ct.ID,
                                 TenDM = ct.LoaiDauMo
                             }).ToList();
            loaidmTT.Add(new { MaDM = (short)-1, TenDM = "Tồn nhiên liệu" });           
            var lisTT = loaidmTT.OrderBy(f => f.MaDM).ToList();           
            cboLoaiDM.DataSource = lisTT;
            cboLoaiDM.DisplayMember = "TenDM";
            cboLoaiDM.ValueMember = "MaDM";
            cboLoaiDM.SelectedIndex = 0;
          
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
                cboNam.Items.Add(i.ToString());
            }
            cboTuThang.Text = DateTime.Today.Month.ToString();
            cboDenThang.Text = cboTuThang.Text;
            cboNam.Text = year.ToString();
            cboDenThang.Enabled = false;
            loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboNam.Text;

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

            string[] arRayNguondls = new string[] { "Cơ báo điện tử", "Cơ báo giấy" };
            cboNguondl.Items.AddRange(arRayNguondls);
            cboNguondl.SelectedIndex = 0;
        }



        private void BCDauMoForm_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();           
        }

        private void cboLoaiBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTuThang.Enabled = true;
            cboNam.Enabled = true;
            if (cboLoaiBC.SelectedIndex == 0)
            {
                cboDenThang.Enabled = false;
                cboDenThang.Text = cboTuThang.Text;
                loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                cboDenThang.Enabled = false;
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text) < 4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3";
                    loaiBC = "Quý I";
                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text) < 7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6";
                    loaiBC = "Quý II";

                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text) < 10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9";
                    loaiBC = "Quý III";

                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12";
                    loaiBC = "Quý IV";
                }
                loaiBC += " năm " + cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 2 || cboLoaiBC.SelectedIndex == 3 || cboLoaiBC.SelectedIndex == 4)
            {
                cboTuThang.Enabled = false;
                cboDenThang.Enabled = false;
                cboTuThang.Text = "1";
                if (cboLoaiBC.SelectedIndex == 2)
                {
                    cboDenThang.Text = "6";
                    loaiBC = "Sáu tháng năm ";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9";
                    loaiBC = "Chín tháng năm ";
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";
                    loaiBC = "Năm ";
                }
                loaiBC += cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {
                cboTuThang.Enabled = true;
                cboDenThang.Enabled = true;
                DateTime tuNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                }
            }
        }

        private void cboTuThang_Validated(object sender, EventArgs e)
        {
            if (cboLoaiBC.SelectedIndex == 0)
            {
                cboDenThang.Text = cboTuThang.Text;
                loaiBC = "Tháng " + cboTuThang.Text + " năm " + cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                if (int.Parse(cboTuThang.Text) >= 1 && int.Parse(cboTuThang.Text) < 4)
                {
                    cboTuThang.Text = "1";
                    cboDenThang.Text = "3";
                    loaiBC = "Quý I";
                }
                else if (int.Parse(cboTuThang.Text) >= 4 && int.Parse(cboTuThang.Text) < 7)
                {
                    cboTuThang.Text = "4";
                    cboDenThang.Text = "6";
                    loaiBC = "Quý II";

                }
                else if (int.Parse(cboTuThang.Text) >= 7 && int.Parse(cboTuThang.Text) < 10)
                {
                    cboTuThang.Text = "7";
                    cboDenThang.Text = "9";
                    loaiBC = "Quý III";
                }
                else
                {
                    cboTuThang.Text = "10";
                    cboDenThang.Text = "12";
                    loaiBC = "Quý IV";
                }
                loaiBC += " năm " + cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 2 || cboLoaiBC.SelectedIndex == 3 || cboLoaiBC.SelectedIndex == 4)
            {
                cboTuThang.Text = "1";
                if (cboLoaiBC.SelectedIndex == 2)
                {
                    cboDenThang.Text = "6";
                    loaiBC = "Sáu tháng năm ";
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    cboDenThang.Text = "9";
                    loaiBC = "Chín tháng năm ";
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {
                    cboDenThang.Text = "12";
                    loaiBC = "Năm ";
                }
                loaiBC += cboNam.Text;
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {
                DateTime tuNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                }
            }
        }

        private void cboTuNam_Validated(object sender, EventArgs e)
        {
            if (cboLoaiBC.SelectedIndex == 5)
            {
                DateTime tuNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboTuThang.Text), 1);
                DateTime denNgay = new DateTime(int.Parse(cboNam.Text), int.Parse(cboDenThang.Text), 1);
                TimeSpan timeSpan = denNgay - tuNgay;
                if ((int)timeSpan.TotalDays < 0)
                {
                    cboDenThang.Text = cboTuThang.Text;
                }
            }
        }
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                string maDV = cboDonVi.SelectedValue.ToString();
                int tuThang = int.Parse(cboTuThang.Text);
                int denThang = int.Parse(cboDenThang.Text);
                int namDT = int.Parse(cboNam.Text);
                int nguonDL = cboNguondl.SelectedIndex;
                int TongSoBG = 0;
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

                rptParam = new ReportParameter("prmNhombc", "Đơn vị: " + cboDonVi.Text +"-Loại báo cáo: "+ cboLoaiDM.Text);
                rptParamList.Add(rptParam);

                if (cboLoaiBC.SelectedIndex == 5)
                    loaiBC = "Từ tháng " + cboTuThang.Text + " đến tháng " + cboDenThang.Text + " năm " + cboNam.Text;
                rptParam = new ReportParameter("prmLoaibc", loaiBC);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayth", "................,Ngày     tháng      năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmLapbieu", "NGƯỜI LẬP BIỂU");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmPhongban", "PHÒNG BAN");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmGiamdoc", "GIÁM ĐỐC");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoilb", " ");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoipb", " ");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoigd", " ");
                rptParamList.Add(rptParam);

                //Lấy dữ liệu
                short loaiDM = short.Parse(cboLoaiDM.SelectedValue.ToString());
                if (loaiDM == -1)
                {
                    List<BCTonNLInfo> listTonNL = new List<BCTonNLInfo>();
                    BaoCaoDAO.NapBCTonNL(nguonDL,maDV, tuThang, denThang, namDT, ref TongSoBG, ref listTonNL);
                    //Kiểm tra xem có dữ liệu hay không
                    if (listTonNL.Count <= 0)
                        throw new Exception("Không có dữ liệu.");
                    FormHelper.ShowReport(reportViewer1,"RptTonNL", "BaoCaoDS", listTonNL, rptParamList);
                }
                else
                {
                    List<BKDauMoInfo> listBKDauMo = new List<BKDauMoInfo>();
                    BaoCaoDAO.NapBCBKDauMo(nguonDL,loaiDM, maDV, tuThang, denThang, namDT, ref TongSoBG, ref listBKDauMo);
                    //Kiểm tra xem có dữ liệu hay không
                    if (listBKDauMo.Count <= 0)
                        throw new Exception("Không có dữ liệu.");
                    FormHelper.ShowReport(reportViewer1, "RptBKDauMo", "BaoCaoDS", listBKDauMo, rptParamList);
                }
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
        
    }
}

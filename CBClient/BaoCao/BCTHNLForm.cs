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
    public partial class BCTHNLForm : DevComponents.DotNetBar.Metro.MetroForm
    {   
        string loaiBC = string.Empty;        
        public BCTHNLForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            string[] arRays = new string[] { "Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm", "Khác" };
            cboLoaiBC.Items.AddRange(arRays);
            cboLoaiBC.SelectedIndex = 0;            
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

            var tuyenTT = (from ct in AppGlobal.DMTuyenmapList
                           select new
                           {
                               TuyenID = ct.TuyenId,
                               TuyenName = ct.TuyenName
                           }).ToList();
            tuyenTT.Add(new { TuyenID = (short)0, TuyenName = "Tất cả các tuyến" });
            var lisTY = tuyenTT.OrderBy(f => f.TuyenID).ToList();
            cboTuyen.DataSource = lisTY;
            cboTuyen.DisplayMember = "TuyenName";
            cboTuyen.ValueMember = "TuyenID";
            cboTuyen.SelectedIndex = 0;

            string[] arRayNhombcs = new string[] { "Theo loại công tác", "Theo loại tính chất", "Theo khu đoạn" };
            cboNhombc.Items.AddRange(arRayNhombcs);
            cboNhombc.SelectedIndex = 0;

            string[] arRayKhoduongs = new string[] { "Tất cả khổ đường", "Khổ đường 1000", "Khổ đường 1435" };
            cboKhoduong.Items.AddRange(arRayKhoduongs);
            cboKhoduong.SelectedIndex = 0;

            string[] arRayNguondls = new string[] { "Cơ báo điện tử", "Cơ báo giấy" };
            cboNguondl.Items.AddRange(arRayNguondls);
            cboNguondl.SelectedIndex = 0;
        }

        private void BCTHNLForm_Load(object sender, EventArgs e)
        {
            sdNgayBD.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, DateTime.DaysInMonth(sdNgayBD.Value.Year, sdNgayBD.Value.Month));
            this.reportViewer1.RefreshReport();           
        }

        private void cboLoaiBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            sdNgayBD.Enabled = true;
            sdNgayKT.Enabled = false;
            if (cboLoaiBC.SelectedIndex == 0)
            {
                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, DateTime.DaysInMonth(sdNgayBD.Value.Year, sdNgayBD.Value.Month));
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                if (sdNgayBD.Value.Month < 4)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 3, DateTime.DaysInMonth(sdNgayBD.Value.Year, 3));
                }
                else if (sdNgayBD.Value.Month >= 4 && sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 4, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else if (sdNgayBD.Value.Month >= 7 && sdNgayBD.Value.Month < 10)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 10, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 2)
            {
                if (sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 3)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
            }
            else if (cboLoaiBC.SelectedIndex == 4)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {
                sdNgayKT.Enabled = true;
                sdNgayKT.Value = DateTime.Today;
            }
        }
        private void sdNgayBD_Validated(object sender, EventArgs e)
        {
            sdNgayBD.Enabled = true;
            sdNgayKT.Enabled = false;
            if (cboLoaiBC.SelectedIndex == 0)
            {
                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, DateTime.DaysInMonth(sdNgayBD.Value.Year, sdNgayBD.Value.Month));
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                if (sdNgayBD.Value.Month < 4)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 3, DateTime.DaysInMonth(sdNgayBD.Value.Year, 3));
                }
                else if (sdNgayBD.Value.Month >= 4 && sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 4, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else if (sdNgayBD.Value.Month >= 7 && sdNgayBD.Value.Month < 10)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 10, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 2)
            {
                if (sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 3)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
            }
            else if (cboLoaiBC.SelectedIndex == 4)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {
                sdNgayKT.Enabled = true;
                sdNgayKT.Value = DateTime.Today;
            }
        }

        private void sdNgayKT_Validated(object sender, EventArgs e)
        {
            if (sdNgayKT.Value < sdNgayBD.Value)
                sdNgayKT.Value = DateTime.Today;
        }

        private void btnTraTim_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                string maDV = cboDonVi.SelectedValue.ToString();                            
                short tuyen = short.Parse(cboTuyen.SelectedValue.ToString());
                string strTuyen = "ALL";
                int nguonDL = cboNguondl.SelectedIndex;
                int nhomBC= cboNhombc.SelectedIndex;
                if (tuyen > 0)
                {
                    strTuyen = string.Empty;
                    var listTuyen = AppGlobal.DMTuyenList.Where(x => x.TuyenMap == tuyen).ToList();
                    foreach (Tuyen dm in listTuyen)
                    {
                        strTuyen += dm.TuyenID + ",";
                    }
                    strTuyen = strTuyen.Substring(0, strTuyen.Length - 1);
                }

                string strLoaiMay = string.Empty;
                var listLoaiMay = new List<LoaiMay>();
                if (cboKhoduong.SelectedIndex == 1)
                {
                    listLoaiMay = AppGlobal.DMLoaimayList.Where(x => x.KhoDuong == 1000).ToList();
                }
                else if (cboKhoduong.SelectedIndex == 2)
                {
                    listLoaiMay = AppGlobal.DMLoaimayList.Where(x => x.KhoDuong == 1435).ToList();
                }
                if (listLoaiMay.Count > 0)
                {
                    foreach (var dm in listLoaiMay)
                    {
                        strLoaiMay += dm.LoaiMayId + "',";
                    }
                    strLoaiMay = strLoaiMay.Substring(0, strLoaiMay.Length - 1);
                }

                List<BCTHNLInfo> list = new List<BCTHNLInfo>();
                List<BCTHNLKDInfo> listKD = new List<BCTHNLKDInfo>();
                int TongSoBG = 0;
                //Lấy dữ liệu
                if (nhomBC == 0)
                {
                    BaoCaoDAO.NapBCTHNL(nguonDL, maDV, cboLoaiBC.SelectedIndex, sdNgayBD.Value, sdNgayKT.Value,strLoaiMay, strTuyen, ref TongSoBG, ref list);
                    //Kiểm tra xem có dữ liệu hay không
                    if (list.Count <= 0)
                        throw new Exception("Không có dữ liệu.");
                }
                else if (nhomBC == 1)
                {
                    BaoCaoDAO.NapBCTHNLDM(nguonDL, maDV, cboLoaiBC.SelectedIndex, sdNgayBD.Value, sdNgayKT.Value, strTuyen, ref TongSoBG, ref list);
                    //Kiểm tra xem có dữ liệu hay không
                    if (list.Count <= 0)
                        throw new Exception("Không có dữ liệu.");
                }
                else 
                {
                    BaoCaoDAO.NapBCTHNLKD(nguonDL, maDV, cboLoaiBC.SelectedIndex, sdNgayBD.Value, sdNgayKT.Value, strTuyen, ref TongSoBG, ref listKD);
                    //Kiểm tra xem có dữ liệu hay không
                    if (listKD.Count <= 0)
                        throw new Exception("Không có dữ liệu.");
                }

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

                string strLoaiDM = "Tuyến: " + cboTuyen.Text;              
                if (cboKhoduong.SelectedIndex == 1)
                    strLoaiDM += " - Khổ đường 1000";
                else if (cboKhoduong.SelectedIndex == 2)
                    strLoaiDM += " - Khổ đường 1435";
                rptParam = new ReportParameter("prmNhombc", strLoaiDM);
                rptParamList.Add(rptParam);

                string tenBC = "Thực hiện định mức và chênh lệch nhiên liệu chạy tầu ";
                if (nhomBC == 1) tenBC += " theo khu đoạn ";

                if (cboLoaiBC.SelectedIndex == 0)
                    loaiBC = "Tháng " + sdNgayBD.Value.Month + " năm " + sdNgayBD.Value.Year;
                else if (cboLoaiBC.SelectedIndex == 1)
                {
                    if (sdNgayBD.Value.Month >= 1 && sdNgayKT.Value.Month <= 3)
                        loaiBC = "Quý I năm " + sdNgayBD.Value.Year;
                    else if (sdNgayBD.Value.Month >= 4 && sdNgayKT.Value.Month <= 6)
                        loaiBC = "Quý II năm " + sdNgayBD.Value.Year;
                    else if (sdNgayBD.Value.Month >= 7 && sdNgayKT.Value.Month <= 9)
                        loaiBC = "Quý III năm " + sdNgayBD.Value.Year;
                    else
                        loaiBC = "Quý IV năm " + sdNgayBD.Value.Year;
                }
                else if (cboLoaiBC.SelectedIndex == 2)
                {
                    if (sdNgayBD.Value.Month >= 1 && sdNgayKT.Value.Month <= 6)
                        loaiBC = "Sáu tháng đầu năm " + sdNgayBD.Value.Year;
                    else
                        loaiBC = "Sáu tháng cuối năm " + sdNgayBD.Value.Year;
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                    loaiBC = "Chín tháng năm " + sdNgayBD.Value.Year;
                else if (cboLoaiBC.SelectedIndex == 4)
                    loaiBC = "Năm " + sdNgayBD.Value.Year;
                else
                    loaiBC = "Từ ngày " + sdNgayBD.Value.ToString("dd.MM.yyyy") + " đến ngày " + sdNgayKT.Value.ToString("dd.MM.yyyy");
                rptParam = new ReportParameter("prmLoaibc", tenBC + loaiBC);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgaytct", "................,Ngày     tháng      năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayxn", "................,Ngày     tháng      năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);

                string donViCha = AppGlobal.User.MaDVQL != "C12" ? tenDVCha.ToUpper() : tenDV.ToUpper();
                rptParam = new ReportParameter("prmTongct", donViCha);
                rptParamList.Add(rptParam);

                string donViCon = cboDonVi.SelectedValue.ToString() != "C12" ? cboDonVi.Text.Split('.')[1].ToUpper() : "BAN KẾ HOẠCH KINH DOANH";
                rptParam = new ReportParameter("prmXinghiep", donViCon);
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoitct", " ");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmNguoixn", " ");
                rptParamList.Add(rptParam);
                if(nhomBC==0)
                    FormHelper.ShowReport(reportViewer1, "RptThucHienNL", "BaoCaoDS", list, rptParamList);
                else if (nhomBC == 1)
                    FormHelper.ShowReport(reportViewer1, "RptThucHienNL", "BaoCaoDS", list, rptParamList);
                else
                    FormHelper.ShowReport(reportViewer1, "RptThucHienNLKD", "BaoCaoDS", listKD, rptParamList);
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

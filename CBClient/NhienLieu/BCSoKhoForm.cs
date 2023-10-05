
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.Library;
using CBClient.BLLTypes;
using Microsoft.Reporting.WinForms;
using CBClient.BLLDaos;

namespace CBClient.NhienLieu
{
    public partial class BCSoKhoForm : DevComponents.DotNetBar.Metro.MetroForm
    {   
        string loaiBC = string.Empty;       
        public BCSoKhoForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
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

            var loaiDM = (from ct in AppGlobal.DMLoaidmList
                          select new
                          {
                              MaDM = (short)ct.ID,
                              TenDM = ct.LoaiDauMo
                          }).ToList();
            var loaiDMTT = loaiDM.ToList();
           
            loaiDMTT = loaiDMTT.OrderBy(f => f.MaDM).ToList();
            cboLoaiDM.DataSource = loaiDMTT;
            cboLoaiDM.DisplayMember = "TenDM";
            cboLoaiDM.ValueMember = "MaDM";
            cboLoaiDM.SelectedIndex = 0;

            List<DmtramNhienLieu> TramnlList = AppGlobal.DMTramnlList;
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "SG").ToList();
                else
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL).ToList();
            }
            var tramNLTT = (from ct in TramnlList
                            select new
                            {
                                MaTram = ct.MaTram,
                                TenTram = ct.TenTram
                            }).ToList();
            tramNLTT.Add(new { MaTram = "ALL", TenTram = "Tất cả" });
            tramNLTT = tramNLTT.OrderBy(x => x.MaTram).ToList();
            cboTramNL.DataSource = tramNLTT;
            cboTramNL.DisplayMember = "TenTram";
            cboTramNL.ValueMember = "MaTram";
            cboTramNL.SelectedIndex = 0;
        }

        private void BCSoKhoForm_Load(object sender, EventArgs e)
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
                string strTram = string.Empty;
                if (cboTramNL.SelectedValue.ToString() != "ALL")
                    strTram = cboTramNL.SelectedValue.ToString();
                else
                {
                    List<DmtramNhienLieu> TramnlList = AppGlobal.DMTramnlList;
                    if (maDV != "TCT")
                    {
                        if (maDV == "YV")
                            TramnlList = TramnlList.Where(x => x.MaDvql == maDV || x.MaDvql == "HN").ToList();
                        else if (maDV == "DN")
                            TramnlList = TramnlList.Where(x => x.MaDvql == maDV || x.MaDvql == "SG").ToList();
                        else
                            TramnlList = TramnlList.Where(x => x.MaDvql == maDV).ToList();
                    }

                    foreach (DmtramNhienLieu tr in TramnlList)
                    {
                        strTram += tr.MaTram + ",";
                    }
                    strTram = strTram.Substring(0, strTram.Length - 1);
                }
                short maDauMo = (short)cboLoaiDM.SelectedValue;
                int TongSoBG = 0;
                List<NL_BCSoKho> listTH = new List<NL_BCSoKho>();                             
                //Lấy dữ liệu
                BaoCaoDAO.NapBCSoKho(strTram,maDauMo, sdNgayBD.Value, sdNgayKT.Value, ref TongSoBG, ref listTH);
                //Kiểm tra xem có dữ liệu hay không
                if (listTH.Count <= 0)
                    throw new Exception("Không có dữ liệu.");
                
                List<ReportParameter> rptParamList = new List<ReportParameter>();
                var dVQL = AppGlobal.DonviDMList.Where(x => x.MaDV == maDV).FirstOrDefault();
                string tenDV = dVQL.TenDV.ToUpper();
                string tenDVCha = AppGlobal.DonviDMList.Where(x => x.MaDV == dVQL.MaCha).FirstOrDefault().TenDV;
                ReportParameter rptParam = new ReportParameter("prmDonvicha", tenDVCha.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmDonvicon", tenDV);
                rptParamList.Add(rptParam);

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
                loaiBC += "-Trạm NL: " + cboTramNL.Text;
                rptParam = new ReportParameter("prmLoaibc", loaiBC);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmTendm", "Tên, quy cách sản phẩm, hàng hóa: " +cboLoaiDM.Text);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmDonvi", "Đơn vị tính: " + AppGlobal.DMLoaidmList.Where(x=>x.ID==maDauMo).FirstOrDefault().DonViTinh);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayth", "Ngày "+DateTime.Today.ToString("dd")+ " tháng " + DateTime.Today.ToString("MM")+ " năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);

                FormHelper.ShowReport(reportViewer1, "RptBCSoKho", "BaoCaoDS", listTH, rptParamList);
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

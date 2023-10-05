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
    public partial class BCHQSDDMForm : DevComponents.DotNetBar.Metro.MetroForm
    {        
        public BCHQSDDMForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
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

            string[] arRayLoaiBCs = new string[] { "Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm", "Khác" };
            cboLoaiBC.Items.AddRange(arRayLoaiBCs);
            cboLoaiBC.SelectedIndex = 0;

            string[] arRayNhomBCs = new string[] { "Theo đơn vị", "Theo loại máy" };
            cboNhomBC.Items.AddRange(arRayNhomBCs);
            cboNhomBC.SelectedIndex = 0;

            string[] arRayNguondls = new string[] { "Cơ báo điện tử", "Cơ báo giấy" };
            cboNguondl.Items.AddRange(arRayNguondls);
            cboNguondl.SelectedIndex = 0;
        }



        private void BCHQSDDMForm_Load(object sender, EventArgs e)
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
                DateTime ngayBDHT = sdNgayBD.Value;
                DateTime ngayKTHT = sdNgayKT.Value;
                DateTime ngayBDKT = sdNgayBD.Value;
                DateTime ngayKTKT = sdNgayKT.Value;
                DateTime ngayBDCK = sdNgayBD.Value;
                DateTime ngayKTCK = sdNgayKT.Value;
                string congtacHT = string.Empty;
                string congtacKT = string.Empty;
                string congtacCK = string.Empty;
                if (cboLoaiBC.SelectedIndex == 0)
                {
                    ngayBDCK = new DateTime(ngayBDHT.Year - 1, ngayBDHT.Month, 1);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 1, ngayKTHT.Month, DateTime.DaysInMonth(ngayKTHT.Year - 1, ngayKTHT.Month));
                    if (ngayBDHT.Month==1)
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year - 1, 12, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year-1, 12, DateTime.DaysInMonth(ngayKTHT.Year - 1, 12));
                    }  
                    else
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year, ngayBDHT.Month-1, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year, ngayKTHT.Month-1, DateTime.DaysInMonth(ngayKTHT.Year, ngayKTHT.Month-1));
                    }                  
                    congtacHT = "Hiện tại tháng " + ngayBDHT.Month + " năm " + ngayBDHT.Year;
                    congtacKT = "Kỳ trước tháng " + ngayBDKT.Month + " năm " + ngayBDKT.Year;
                    congtacCK = "Cùng kỳ tháng " + ngayBDCK.Month + " năm " + ngayBDCK.Year;

                }
                else if (cboLoaiBC.SelectedIndex == 1)
                {
                    ngayBDCK = new DateTime(ngayBDHT.Year - 1, ngayBDHT.Month, 1);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 1, ngayKTHT.Month, DateTime.DaysInMonth(ngayKTHT.Year - 1, ngayKTHT.Month));
                    if (sdNgayBD.Value.Month < 4)
                    {                        
                        ngayBDKT = new DateTime(ngayBDHT.Year - 1, 10, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year - 1, 12, DateTime.DaysInMonth(ngayKTHT.Year - 1, 12));                       
                        congtacHT = "Hiện tại quý I năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước quý IV năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ quý I năm " + ngayBDCK.Year;
                    }
                    else if (sdNgayBD.Value.Month >= 4 && sdNgayBD.Value.Month < 7)
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year, 1, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year, 3, DateTime.DaysInMonth(ngayKTHT.Year, 3));
                        congtacHT = "Hiện tại quý II năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước quý I năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ quý II năm " + ngayBDCK.Year;

                    }
                    else if (sdNgayBD.Value.Month >= 7 && sdNgayBD.Value.Month < 10)
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year, 4, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year, 6, DateTime.DaysInMonth(ngayKTHT.Year, 6));
                        congtacHT = "Hiện tại quý III năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước quý II năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ quý III năm " + ngayBDCK.Year;
                    }
                    else
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year, 7, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year, 9, DateTime.DaysInMonth(ngayKTHT.Year, 9));
                        congtacHT = "Hiện tại quý IV năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước quý I năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ quý IV năm " + ngayBDCK.Year;
                    }                   
                }
                else if (cboLoaiBC.SelectedIndex == 2)
                {
                    ngayBDCK = new DateTime(ngayBDHT.Year - 1, ngayBDHT.Month, 1);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 1, ngayKTHT.Month, DateTime.DaysInMonth(ngayKTHT.Year - 1, ngayKTHT.Month));
                    if (sdNgayBD.Value.Month < 7)
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year - 1, 7, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year - 1, 12, DateTime.DaysInMonth(ngayKTHT.Year - 1, 12));
                        congtacHT = "Hiện tại 6 tháng đầu năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước 6 tháng cuối năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ 6 tháng đầu năm " + ngayBDCK.Year;
                    }
                    else
                    {
                        ngayBDKT = new DateTime(ngayBDHT.Year, 1, 1);
                        ngayKTKT = new DateTime(ngayKTHT.Year, 6, DateTime.DaysInMonth(ngayKTHT.Year, 6));
                        congtacHT = "Hiện tại 6 tháng cuối năm " + ngayBDHT.Year;
                        congtacKT = "Kỳ trước 6 tháng đầu năm " + ngayBDKT.Year;
                        congtacCK = "Cùng kỳ 6 tháng cuối năm " + ngayBDCK.Year;
                    }
                }
                else if (cboLoaiBC.SelectedIndex == 3)
                {
                    ngayBDKT = new DateTime(ngayBDHT.Year - 1, 1, 1);
                    ngayKTKT = new DateTime(ngayKTHT.Year - 1, 9, DateTime.DaysInMonth(ngayKTHT.Year - 1, 9));
                    ngayBDCK = new DateTime(ngayBDHT.Year - 2, ngayBDHT.Month, 1);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 2, ngayKTHT.Month, DateTime.DaysInMonth(ngayKTHT.Year - 2, ngayKTHT.Month));
                    congtacHT = "Hiện tại 9 tháng năm " + ngayBDHT.Year;
                    congtacKT = "Kỳ trước 9 tháng năm " + ngayBDKT.Year;
                    congtacCK = "Cùng kỳ 9 tháng năm " + ngayBDCK.Year;
                }
                else if (cboLoaiBC.SelectedIndex == 4)
                {   
                    ngayBDKT = new DateTime(ngayBDHT.Year - 1, 1, 1);
                    ngayKTKT = new DateTime(ngayKTHT.Year - 1, 12, DateTime.DaysInMonth(ngayKTHT.Year - 1, 12));
                    ngayBDCK = new DateTime(ngayBDHT.Year - 2, ngayBDHT.Month, 1);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 2, ngayKTHT.Month, DateTime.DaysInMonth(ngayKTHT.Year - 2, ngayKTHT.Month));
                    congtacHT = "Hiện tại năm " + ngayBDHT.Year;
                    congtacKT = "Kỳ trước năm " + ngayBDKT.Year;
                    congtacCK = "Cùng kỳ năm " + ngayBDCK.Year;
                }
                else
                {
                    int ngayDM = int.Parse((ngayKTHT - ngayBDHT).TotalDays.ToString());
                    ngayBDKT = ngayBDHT.AddDays(-ngayDM);
                    ngayKTKT = ngayBDHT.AddDays(-1);
                    ngayBDCK = new DateTime(ngayBDHT.Year - 1, ngayBDHT.Month, ngayBDHT.Day);
                    ngayKTCK = new DateTime(ngayKTHT.Year - 1, ngayKTHT.Month, ngayKTHT.Day);
                    congtacHT = "Hiện tại từ " + ngayBDHT.ToString("dd/MM/yyyy")+ " đến " + ngayKTHT.ToString("dd/MM/yyyy");
                    congtacKT = "Kỳ trước từ " + ngayBDKT.ToString("dd/MM/yyyy") + " đến " + ngayKTKT.ToString("dd/MM/yyyy");
                    congtacCK = "Cùng kỳ từ " + ngayBDCK.ToString("dd/MM/yyyy") + " đến " + ngayKTCK.ToString("dd/MM/yyyy");
                }


                base.Cursor = Cursors.WaitCursor;
                string maDV = cboDonVi.SelectedValue.ToString();
                int nguonDL = cboNguondl.SelectedIndex;
                int loaiBC = cboLoaiBC.SelectedIndex;
                int TongSoBG = 0;
                //Lấy dữ liệu
                List<BCHieuQuaSDDMInfo> listHT = new List<BCHieuQuaSDDMInfo>();
                List<BCHieuQuaSDDMInfo> listKT = new List<BCHieuQuaSDDMInfo>();
                List<BCHieuQuaSDDMInfo> listCK = new List<BCHieuQuaSDDMInfo>();
                List<BCHieuQuaSDDMInfo> list = new List<BCHieuQuaSDDMInfo>();
                if (cboNhomBC.SelectedIndex == 0)
                {                  
                    BaoCaoDAO.NapBCHQSDDMDV(nguonDL, maDV, loaiBC, ngayBDHT, ngayKTHT, ref listHT);
                    foreach(var ct in listHT)
                    {                      
                        ct.CongTac = "1."+congtacHT;
                        list.Add(ct);                       
                    }                    
                    BaoCaoDAO.NapBCHQSDDMDV(nguonDL, maDV, loaiBC, ngayBDKT, ngayKTKT, ref listKT);
                    foreach (var ct in listKT)
                    {
                        ct.CongTac = "2."+congtacKT;
                        list.Add(ct);
                        var tyLe = list.Where(x => x.XiNghiep == ct.XiNghiep && x.CongTac.Substring(0, 2) == "1.").FirstOrDefault();
                        if(tyLe!=null)
                        {
                            var ctCL = new BCHieuQuaSDDMInfo();
                            ctCL.XiNghiep = ct.XiNghiep;
                            ctCL.CongTac = "3.Chênh lệch với kỳ trước";
                            ctCL.GioDM = tyLe.GioDM - ct.GioDM;
                            ctCL.GioDon = tyLe.GioDon - ct.GioDon;
                            ctCL.KmChinh = tyLe.KmChinh - ct.KmChinh;
                            ctCL.KmPhuTro = tyLe.KmPhuTro - ct.KmPhuTro;
                            ctCL.VTKm = tyLe.VTKm - ct.VTKm;
                            ctCL.KmBQ = tyLe.KmBQ - ct.KmBQ;
                            ctCL.TanBQ = tyLe.TanBQ - ct.TanBQ;
                            ctCL.NSuatBQ = tyLe.NSuatBQ - ct.NSuatBQ;
                            ctCL.MayBQ = tyLe.MayBQ - ct.MayBQ;
                            ctCL.TieuThu = tyLe.TieuThu - ct.TieuThu;
                            list.Add(ctCL);
                            var ctTL = new BCHieuQuaSDDMInfo();
                            ctTL.XiNghiep = ct.XiNghiep;
                            ctTL.CongTac = "4.Tỷ lệ % với kỳ trước";
                            ctTL.GioDM = ct.GioDM > 0 ? 100 * (tyLe.GioDM / ct.GioDM) : 100;
                            ctTL.GioDon = ct.GioDon > 0 ? 100 * (tyLe.GioDon / ct.GioDon) : 100;
                            ctTL.KmChinh = ct.KmChinh > 0 ? 100 * (tyLe.KmChinh / ct.KmChinh) : 100;
                            ctTL.KmPhuTro = ct.KmPhuTro > 0 ? 100 * (tyLe.KmPhuTro / ct.KmPhuTro) : 100;
                            ctTL.VTKm = ct.VTKm > 0 ? 100 * (tyLe.VTKm / ct.VTKm) : 100;
                            ctTL.KmBQ = ct.KmBQ > 0 ? 100 * (tyLe.KmBQ / ct.KmBQ) : 100;
                            ctTL.TanBQ = ct.TanBQ > 0 ? 100 * (tyLe.TanBQ / ct.TanBQ) : 100;
                            ctTL.NSuatBQ = ct.NSuatBQ > 0 ? 100 * (tyLe.NSuatBQ / ct.NSuatBQ) : 100;
                            ctTL.MayBQ = ct.MayBQ > 0 ? 100 * (tyLe.MayBQ / ct.MayBQ) : 100;
                            ctTL.TieuThu = ct.TieuThu > 0 ? 100 * (tyLe.TieuThu / ct.TieuThu) : 100;
                            list.Add(ctTL);
                        }    
                    }
                    BaoCaoDAO.NapBCHQSDDMDV(nguonDL, maDV, loaiBC, ngayBDCK, ngayKTCK, ref listCK);
                    foreach (var ct in listCK)
                    {
                        ct.CongTac = "5." + congtacCK;
                        list.Add(ct);
                        var tyLe = list.Where(x => x.XiNghiep == ct.XiNghiep && x.CongTac.Substring(0, 2) == "1.").FirstOrDefault();
                        if (tyLe != null)
                        {
                            var ctCL = new BCHieuQuaSDDMInfo();
                            ctCL.XiNghiep = ct.XiNghiep;
                            ctCL.CongTac = "6.Chênh lệch với cùng kỳ";
                            ctCL.GioDM = tyLe.GioDM - ct.GioDM;
                            ctCL.GioDon = tyLe.GioDon - ct.GioDon;
                            ctCL.KmChinh = tyLe.KmChinh - ct.KmChinh;
                            ctCL.KmPhuTro = tyLe.KmPhuTro - ct.KmPhuTro;
                            ctCL.VTKm = tyLe.VTKm - ct.VTKm;
                            ctCL.KmBQ = tyLe.KmBQ - ct.KmBQ;
                            ctCL.TanBQ = tyLe.TanBQ - ct.TanBQ;
                            ctCL.NSuatBQ = tyLe.NSuatBQ - ct.NSuatBQ;
                            ctCL.MayBQ = tyLe.MayBQ - ct.MayBQ;
                            ctCL.TieuThu = tyLe.TieuThu - ct.TieuThu;
                            list.Add(ctCL);
                            var ctTL = new BCHieuQuaSDDMInfo();
                            ctTL.XiNghiep = ct.XiNghiep;
                            ctTL.CongTac = "7.Tỷ lệ % với cùng kỳ";
                            ctTL.GioDM = ct.GioDM > 0 ? 100 * (tyLe.GioDM / ct.GioDM) : 100;
                            ctTL.GioDon = ct.GioDon > 0 ? 100 * (tyLe.GioDon / ct.GioDon) : 100;
                            ctTL.KmChinh = ct.KmChinh > 0 ? 100 * (tyLe.KmChinh / ct.KmChinh) : 100;
                            ctTL.KmPhuTro = ct.KmPhuTro > 0 ? 100 * (tyLe.KmPhuTro / ct.KmPhuTro) : 100;
                            ctTL.VTKm = ct.VTKm > 0 ? 100 * (tyLe.VTKm / ct.VTKm) : 100;
                            ctTL.KmBQ = ct.KmBQ > 0 ? 100 * (tyLe.KmBQ / ct.KmBQ) : 100;
                            ctTL.TanBQ = ct.TanBQ > 0 ? 100 * (tyLe.TanBQ / ct.TanBQ) : 100;
                            ctTL.NSuatBQ = ct.NSuatBQ > 0 ? 100 * (tyLe.NSuatBQ / ct.NSuatBQ) : 100;
                            ctTL.MayBQ = ct.MayBQ > 0 ? 100 * (tyLe.MayBQ / ct.MayBQ) : 100;
                            ctTL.TieuThu = ct.TieuThu > 0 ? 100 * (tyLe.TieuThu / ct.TieuThu) : 100;
                            list.Add(ctTL);
                        }
                    }

                    foreach (var ct in list)
                    {
                        if (ct.XiNghiep == "YV") ct.XiNghiep = "1.Chi nhánh XNĐM Yên Viên";
                        else if (ct.XiNghiep == "HN") ct.XiNghiep = "2.Chi nhánh XNĐM Hà Nội";
                        else if (ct.XiNghiep == "VIN") ct.XiNghiep = "3.Chi nhánh XNĐM Vinh";
                        else if (ct.XiNghiep == "DN") ct.XiNghiep = "4.Chi nhánh XNĐM Đà Nẵng";
                        else if (ct.XiNghiep == "SG") ct.XiNghiep = "5.Chi nhánh XNĐM Sài Gòn";
                        else ct.XiNghiep = "6.Tổng công ty ĐSVN";                        
                    }
                }
                else
                {
                    BaoCaoDAO.NapBCHQSDDMLM(nguonDL, maDV, loaiBC, ngayBDHT, ngayKTHT, ref listHT);
                    foreach (var ct in listHT)
                    {
                        ct.CongTac = "1." + congtacHT;
                        list.Add(ct);
                    }
                    BaoCaoDAO.NapBCHQSDDMLM(nguonDL, maDV, loaiBC, ngayBDKT, ngayKTKT, ref listKT);
                    foreach (var ct in listKT)
                    {
                        ct.CongTac = "2." + congtacKT;
                        list.Add(ct);
                        var tyLe = list.Where(x => x.LoaiMay == ct.LoaiMay && x.CongTac.Substring(0, 2) == "1.").FirstOrDefault();
                        if (tyLe != null)
                        {
                            var ctCL = new BCHieuQuaSDDMInfo();
                            ctCL.LoaiMay = ct.LoaiMay;
                            ctCL.CongTac = "3.Chênh lệch với kỳ trước";
                            ctCL.GioDM = tyLe.GioDM - ct.GioDM;
                            ctCL.GioDon = tyLe.GioDon - ct.GioDon;
                            ctCL.KmChinh = tyLe.KmChinh - ct.KmChinh;
                            ctCL.KmPhuTro = tyLe.KmPhuTro - ct.KmPhuTro;
                            ctCL.VTKm = tyLe.VTKm - ct.VTKm;
                            ctCL.KmBQ = tyLe.KmBQ - ct.KmBQ;
                            ctCL.TanBQ = tyLe.TanBQ - ct.TanBQ;
                            ctCL.NSuatBQ = tyLe.NSuatBQ - ct.NSuatBQ;
                            ctCL.MayBQ = tyLe.MayBQ - ct.MayBQ;
                            ctCL.TieuThu = tyLe.TieuThu - ct.TieuThu;
                            list.Add(ctCL);
                            var ctTL = new BCHieuQuaSDDMInfo();
                            ctTL.LoaiMay = ct.LoaiMay;
                            ctTL.CongTac = "4.Tỷ lệ % với kỳ trước";
                            ctTL.GioDM = ct.GioDM > 0 ? 100 * (tyLe.GioDM / ct.GioDM) : 100;
                            ctTL.GioDon = ct.GioDon > 0 ? 100 * (tyLe.GioDon / ct.GioDon) : 100;
                            ctTL.KmChinh = ct.KmChinh > 0 ? 100 * (tyLe.KmChinh / ct.KmChinh) : 100;
                            ctTL.KmPhuTro = ct.KmPhuTro > 0 ? 100 * (tyLe.KmPhuTro / ct.KmPhuTro) : 100;
                            ctTL.VTKm = ct.VTKm > 0 ? 100 * (tyLe.VTKm / ct.VTKm) : 100;
                            ctTL.KmBQ = ct.KmBQ > 0 ? 100 * (tyLe.KmBQ / ct.KmBQ) : 100;
                            ctTL.TanBQ = ct.TanBQ > 0 ? 100 * (tyLe.TanBQ / ct.TanBQ) : 100;
                            ctTL.NSuatBQ = ct.NSuatBQ > 0 ? 100 * (tyLe.NSuatBQ / ct.NSuatBQ) : 100;
                            ctTL.MayBQ = ct.MayBQ > 0 ? 100 * (tyLe.MayBQ / ct.MayBQ) : 100;
                            ctTL.TieuThu = ct.TieuThu > 0 ? 100 * (tyLe.TieuThu / ct.TieuThu) : 100;
                            list.Add(ctTL);
                        }
                    }
                    BaoCaoDAO.NapBCHQSDDMLM(nguonDL, maDV, loaiBC, ngayBDCK, ngayKTCK, ref listCK);
                    foreach (var ct in listCK)
                    {
                        ct.CongTac = "5." + congtacCK;
                        list.Add(ct);
                        var tyLe = list.Where(x => x.LoaiMay == ct.LoaiMay && x.CongTac.Substring(0, 2) == "1.").FirstOrDefault();
                        if (tyLe != null)
                        {
                            var ctCL = new BCHieuQuaSDDMInfo();
                            ctCL.LoaiMay = ct.LoaiMay;
                            ctCL.CongTac = "6.Chênh lệch với cùng kỳ";
                            ctCL.GioDM = tyLe.GioDM - ct.GioDM;
                            ctCL.GioDon = tyLe.GioDon - ct.GioDon;
                            ctCL.KmChinh = tyLe.KmChinh - ct.KmChinh;
                            ctCL.KmPhuTro = tyLe.KmPhuTro - ct.KmPhuTro;
                            ctCL.VTKm = tyLe.VTKm - ct.VTKm;
                            ctCL.KmBQ = tyLe.KmBQ - ct.KmBQ;
                            ctCL.TanBQ = tyLe.TanBQ - ct.TanBQ;
                            ctCL.NSuatBQ = tyLe.NSuatBQ - ct.NSuatBQ;
                            ctCL.MayBQ = tyLe.MayBQ - ct.MayBQ;
                            ctCL.TieuThu = tyLe.TieuThu - ct.TieuThu;
                            list.Add(ctCL);
                            var ctTL = new BCHieuQuaSDDMInfo();
                            ctTL.LoaiMay = ct.LoaiMay;
                            ctTL.CongTac = "7.Tỷ lệ % với cùng kỳ";
                            ctTL.GioDM = ct.GioDM > 0 ? 100 * (tyLe.GioDM / ct.GioDM) : 100;
                            ctTL.GioDon = ct.GioDon > 0 ? 100 * (tyLe.GioDon / ct.GioDon) : 100;
                            ctTL.KmChinh = ct.KmChinh > 0 ? 100 * (tyLe.KmChinh / ct.KmChinh) : 100;
                            ctTL.KmPhuTro = ct.KmPhuTro > 0 ? 100 * (tyLe.KmPhuTro / ct.KmPhuTro) : 100;
                            ctTL.VTKm = ct.VTKm > 0 ? 100 * (tyLe.VTKm / ct.VTKm) : 100;
                            ctTL.KmBQ = ct.KmBQ > 0 ? 100 * (tyLe.KmBQ / ct.KmBQ) : 100;
                            ctTL.TanBQ = ct.TanBQ > 0 ? 100 * (tyLe.TanBQ / ct.TanBQ) : 100;
                            ctTL.NSuatBQ = ct.NSuatBQ > 0 ? 100 * (tyLe.NSuatBQ / ct.NSuatBQ) : 100;
                            ctTL.MayBQ = ct.MayBQ > 0 ? 100 * (tyLe.MayBQ / ct.MayBQ) : 100;
                            ctTL.TieuThu = ct.TieuThu > 0 ? 100 * (tyLe.TieuThu / ct.TieuThu) : 100;
                            list.Add(ctTL);
                        }
                    }
                }
                //Kiểm tra xem có dữ liệu hay không
                if (list.Count <= 0)
                    throw new Exception("Không có dữ liệu.");
                TongSoBG = list.Count;

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
               

                rptParam = new ReportParameter("prmNgayth", "Ngày " + DateTime.Today.ToString("dd") + " tháng " + DateTime.Today.ToString("MM") + " năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmLapbieu", "NGƯỜI LẬP BIỂU");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmPhongban","PHÒNG BAN");
                rptParamList.Add(rptParam);
                rptParam = new ReportParameter("prmGiamdoc", "GIÁM ĐỐC");
                rptParamList.Add(rptParam);
                if (cboNhomBC.SelectedIndex == 0)
                {
                    FormHelper.ShowReport(reportViewer1, "RptHQSDDV", "BaoCaoDS", list, rptParamList);
                }
                else
                {
                    FormHelper.ShowReport(reportViewer1, "RptHQSDLM", "BaoCaoDS", list, rptParamList);
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

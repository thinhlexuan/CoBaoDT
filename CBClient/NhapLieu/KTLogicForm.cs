using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.NhapLieu
{
    public partial class KTLogicForm : DevComponents.DotNetBar.Metro.MetroForm
    { 
        string loaiLG = string.Empty;
        public KTLogicForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();           
            for (int i = 1; i <= 12; i++)
            {
                cboThangDT.Items.Add(i.ToString());
            }

            int year = DateTime.Today.Year;
            for (int i = year - 10; i <= year + 1; i++)
            {
                cboNamDT.Items.Add(i.ToString());
            }
            cboThangDT.SelectedText = DateTime.Today.Month.ToString();
            cboNamDT.SelectedText = year.ToString();

            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT" || ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();
            if (string.IsNullOrWhiteSpace(AppGlobal.User.MaDVQL))
            {
                var listDonVi = AppGlobal.DMDonviList.Where(x => x.MaDv == AppGlobal.User.MaDV).First();
                AppGlobal.User.MaDVQL = listDonVi.Dvql;
            }
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
           
            string[] arRays = new string[] { "Giờ quay vòng", "Giờ đơn thuần", "Vận tốc kỹ thuật", "Dừng kho bãi", "Nhiên liệu giao nhận" };
            cboLoaiLG.Items.AddRange(arRays);
            cboLoaiLG.SelectedIndex = 0;
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            dataGridView1.DataSource = null;
            DataTable dt = new DataTable();
            try
            {                
                base.Cursor = Cursors.WaitCursor;
                string tableName = string.Empty;               
                string data = "?madv=" + cboDonVi.SelectedValue;                
                data += "&thangdt=" + cboThangDT.Text;
                data += "&namdt=" + cboNamDT.Text;
                data += "&daumay=" + txtSHDauMay.Text;
                if (cboLoaiLG.SelectedIndex == 0)
                {
                    var listqvs = HttpHelper.GetList<KTQuayVong>(Configuration.UrlCBApi + "api/KtLogics/GetKTQuayVong"+data);
                    if (listqvs != null)
                    {
                        dt = Funcs.ToDataTable<KTQuayVong>(listqvs);
                    }
                }
                else if (cboLoaiLG.SelectedIndex == 1)
                {
                    var listdts = HttpHelper.GetList<KTDonThuan>(Configuration.UrlCBApi + "api/KtLogics/GetKTDonThuan" + data);
                    if (listdts != null)
                    {
                        dt = Funcs.ToDataTable<KTDonThuan>(listdts);
                    }
                }
                else if (cboLoaiLG.SelectedIndex == 2)
                {
                    var listvts = new List<KTVanTocKT>();
                    var query = HttpHelper.GetList<KTVanTocKT>(Configuration.UrlCBApi + "api/KtLogics/GetKTVantocKT" + data);

                    if (query != null)
                    {
                        foreach (var vt in query)
                        {
                            vt.VanToc = Math.Round(vt.VanToc, 2);
                            if(vt.CongTacID==1 &&(vt.VanToc<35 ||vt.VanToc>65))
                            {
                                listvts.Add(vt);
                            }                            
                            if (vt.CongTacID >= 2 && vt.CongTacID<=3 && (vt.VanToc < 30 || vt.VanToc > 65))
                            {
                                listvts.Add(vt);
                            }
                            if (vt.CongTacID ==4 && (vt.VanToc < 20 || vt.VanToc > 60))
                            {
                                listvts.Add(vt);
                            }
                            if (vt.CongTacID >= 5 && vt.CongTacID <= 7 && (vt.VanToc < 8 || vt.VanToc > 50))
                            {
                                listvts.Add(vt);
                            }
                            if (vt.CongTacID ==10 && (vt.VanToc < 30 || vt.VanToc > 70))
                            {
                                listvts.Add(vt);
                            }
                        }                       
                        dt = Funcs.ToDataTable<KTVanTocKT>(listvts);
                    }
                }
                else if (cboLoaiLG.SelectedIndex == 3)
                {
                    var listkbs = HttpHelper.GetList<KTDungKB>(Configuration.UrlCBApi + "api/KtLogics/GetKTDungKB" + data);
                    if (listkbs != null)
                    {
                        dt = Funcs.ToDataTable<KTDungKB>(listkbs);
                    }
                }
                else if (cboLoaiLG.SelectedIndex == 4)
                {                   
                    var listnls = HttpHelper.GetList<KTNhienLieu>(Configuration.UrlCBApi + "api/KtLogics/GetKTNhienLieu" + data);
                    if (listnls != null)
                    {
                        dt = Funcs.ToDataTable<KTNhienLieu>(listnls);
                    }
                }
                dataGridView1.DataSource = dt.DefaultView;
                lblTableCount.Text = "Tổng số bản ghi:" + dt.Rows.Count.ToString("N0");
                base.Cursor = Cursors.Default;
                btnExport.Enabled = true;
            }
            catch (Exception ex)
            {                
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void cboLoaiLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaiLG = "Thông tin kiểm tra";
            if (cboLoaiLG.SelectedIndex == 0)
            {
                loaiLG += ": Giờ quay vòng nhỏ hơn 0 hoặc giờ quay vòng lớn hơn 720 phút (12 giờ).";
            }
            else if (cboLoaiLG.SelectedIndex == 1)
            {
                loaiLG += ": Giờ đơn thuần nhỏ hơn 0 hoặc giờ giờ đơn thuần lớn hơn 600 phút (10 giờ).";
            }
            else if (cboLoaiLG.SelectedIndex == 2)
            {
                loaiLG += ": Vận tốc kỹ thuật km/giờ.\r\n";
                loaiLG += "Đơn,thoi,đá (5,6,7) <8 và >50.\r\n";
                loaiLG += "Hàng thường (4) <20 và >60.\r\n";
                loaiLG += "Hàng nhanh80 (10) <30 và >70.\r\n";
                loaiLG += "Khách ĐP (2) <30 và >65.\r\n";
                loaiLG += "Khách TN (1) <35 và >65.\r\n";
            }
            else if (cboLoaiLG.SelectedIndex == 3)
            {
                loaiLG += ": Giờ dừng kho bãi nhỏ hơn 0 là trùng cơ báo, lớn hơn 1440 phút (24 giờ) là mất cơ báo hoặc vào cấp sửa chữa.";
            }
            else if (cboLoaiLG.SelectedIndex == 4)
            {
                loaiLG += ": Nhiên liệu cùng một đầu máy giao ban trước không trùng khớp với nhận ban sau.";
            }
            rtxtThongTin.Text = loaiLG;
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
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

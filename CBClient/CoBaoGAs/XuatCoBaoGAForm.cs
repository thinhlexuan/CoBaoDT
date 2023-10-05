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

namespace CBClient.CoBaoGAs
{
    public partial class XuatCoBaoGAForm : DevComponents.DotNetBar.Metro.MetroForm    {    
        
        public XuatCoBaoGAForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
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
            var loaiMayTT = (from ct in AppGlobal.DMLoaimayList
                             select new
                             {
                                 MaLM = ct.LoaiMayId,
                                 TenLM = ct.LoaiMayName
                             }).ToList();
            loaiMayTT.Add(new { MaLM = "ALL", TenLM = "Tất cả các loại máy" });
            var lisTT = loaiMayTT.OrderBy(f => f.MaLM).ToList();
            cboLoaiMay.DataSource = lisTT;
            cboLoaiMay.DisplayMember = "TenLM";
            cboLoaiMay.ValueMember = "MaLM";
            cboLoaiMay.SelectedIndex = 0;

            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT"
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
            string[] arRays = new string[] { "Cơ báo", "Cơ báo chi tiết", "Cơ báo dầu mỡ" };
            cboLoaiDL.Items.AddRange(arRays);
            cboLoaiDL.SelectedIndex = 0;
        }       

        private void btnTraTim_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            dgView.DataSource = null;
            lblBanGhi.Text = "Tổng số bản ghi: 0";
            try
            {
                base.Cursor = Cursors.WaitCursor;
                int thangDT = int.Parse(cboThangDT.Text);
                int namDT = int.Parse(cboNamDT.Text);
                string data = "thangDT=" + thangDT;
                data += "&namDT=" + namDT;
                data += "&DonVi=" + cboDonVi.SelectedValue;
                data += "&LoaiMay=" + cboLoaiMay.SelectedValue;
                if (cboLoaiDL.SelectedIndex == 0)
                {
                    var obj = HttpHelper.GetList<XCoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/GetXCoBaoGA?" + data);
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    if (cboDonVi.SelectedValue.ToString() == "HN")
                    {
                        foreach (XCoBaoGA ct in obj)
                        {
                            //ct.SoCB = "'" + ct.SoCB;
                            //ct.TaiXe1ID = string.IsNullOrEmpty(ct.TaiXe1ID) ? ct.TaiXe1ID : "'" + ct.TaiXe1ID;
                            //ct.TaiXe2ID = string.IsNullOrEmpty(ct.TaiXe2ID) ? ct.TaiXe2ID : "'" + ct.TaiXe2ID;
                            //ct.TaiXe3ID = string.IsNullOrEmpty(ct.TaiXe3ID) ? ct.TaiXe3ID : "'" + ct.TaiXe3ID;
                            //ct.PhoXe1ID = string.IsNullOrEmpty(ct.PhoXe1ID) ? ct.PhoXe1ID : "'" + ct.PhoXe1ID;
                            //ct.PhoXe2ID = string.IsNullOrEmpty(ct.PhoXe2ID) ? ct.PhoXe2ID : "'" + ct.PhoXe2ID;
                            //ct.PhoXe3ID = string.IsNullOrEmpty(ct.PhoXe3ID) ? ct.PhoXe3ID : "'" + ct.PhoXe3ID;
                            //ct.MaCB = "'" + ct.MaCB;
                            ct.SHDT = string.IsNullOrEmpty(ct.SHDT) ? ct.SHDT : "'" + ct.SHDT;
                        }
                    }
                    dgView.DataSource = obj;
                    lblBanGhi.Text = "Tổng số " + cboLoaiDL.Text + ": " + obj.Count.ToString("N0");
                }
                else if (cboLoaiDL.SelectedIndex == 1)
                {
                    var obj = HttpHelper.GetList<XCoBaoGACT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetXCoBaoGACT?" + data);
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo chi tiết.");
                    }
                    if (cboDonVi.SelectedValue.ToString() == "HN")
                    {
                        foreach (XCoBaoGACT ct in obj)
                        {
                            ct.MacTauID = string.IsNullOrEmpty(ct.MacTauID) ? ct.MacTauID : "'" + ct.MacTauID;
                        }
                    }
                    dgView.DataSource = obj;
                    lblBanGhi.Text = "Tổng số " + cboLoaiDL.Text + ": " + obj.Count.ToString("N0");
                }
                else if (cboLoaiDL.SelectedIndex == 2)
                {
                    var obj = HttpHelper.GetList<XCoBaoGADM>(Configuration.UrlCBApi + "api/CoBaoGAs/GetXCoBaoGADM?" + data);
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo dầu mỡ.");
                    }
                    dgView.DataSource = obj;
                    lblBanGhi.Text = "Tổng số " + cboLoaiDL.Text + ": " + obj.Count.ToString("N0");
                }
                base.Cursor = Cursors.Default;
                btnExport.Enabled = true;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dgView);
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

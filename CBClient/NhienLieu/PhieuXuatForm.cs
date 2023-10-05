using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Microsoft.Reporting.WinForms;

namespace CBClient.NhienLieu
{
    public partial class PhieuXuatForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        private List<DmtramNhienLieu> TramnlList = new List<DmtramNhienLieu>();
        public PhieuXuatForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();           
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForNumber(txtDauMay);

            TramnlList = AppGlobal.DMTramnlList;
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "SG").ToList();
                else
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL).ToList();
            }
            var tramNLTT=(from ct in TramnlList                        
                           select new
                           {
                               MaTram = ct.MaTram,
                               TenTram = ct.TenTram
                           }).ToList();
            tramNLTT.Add(new { MaTram = "ALL", TenTram = "Tất cả" });
            tramNLTT = tramNLTT.OrderBy(x => x.MaTram).ToList();
            cboTramNLTT.DataSource = tramNLTT;
            cboTramNLTT.DisplayMember = "TenTram";
            cboTramNLTT.ValueMember = "MaTram";
            cboTramNLTT.SelectedIndex = 0;
            var loaiMayTT = (from ct in AppGlobal.DMLoaimayList
                             select new
                             {
                                 MaLM = ct.LoaiMayId,
                                 TenLM = ct.LoaiMayName
                             }).ToList();
            loaiMayTT.Add(new { MaLM = "ALL", TenLM = "Tất cả các loại máy" });
            var lisLM = loaiMayTT.OrderBy(f => f.MaLM).ToList();
            cboLoaiMayTT.DataSource = lisLM;
            cboLoaiMayTT.DisplayMember = "TenLM";
            cboLoaiMayTT.ValueMember = "MaLM";
            cboLoaiMayTT.SelectedIndex = 0;
        }
        private void PhieuXuatForm_Load(object sender, EventArgs e)
        {
            sdNgayBD.Value = DateTime.Today.AddDays(-1);
            sdNgayKT.Value = DateTime.Today;
        }
        private void sdNgayKT_Validated(object sender, EventArgs e)
        {
            if (sdNgayKT.Value < sdNgayBD.Value)
                sdNgayKT.Value = sdNgayBD.Value;
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }      
        private void fnTraTim()
        {
            try
            {
                bsPhieuXuat.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string strTram = string.Empty;
                if(cboTramNLTT.SelectedValue.ToString()=="ALL")
                {
                    foreach(DmtramNhienLieu tr in TramnlList)
                    {
                        strTram += tr.MaTram + ",";
                    }
                    strTram = strTram.Substring(0, strTram.Length - 1);
                }
                else
                {
                    strTram = cboTramNLTT.SelectedValue.ToString();
                }
                string data = "?maTram=" + strTram;
                data += "&loaiMay=" + cboLoaiMayTT.SelectedValue;
                data += "&dauMay=" + txtDauMay.Text;
                data += "&maPX=" + txtPhieuXuat.Text;
                data += "&ngayBD=" + sdNgayBD.Value;
                data += "&ngayKT=" + sdNgayKT.Value;
                List<NL_PhieuXuat> listPhieuXuat = HttpHelper.GetList<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLGetPhieuXuat" + data).OrderBy(x=>x.NgayXuat).ToList();                
                if (listPhieuXuat.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsPhieuXuat.DataSource = listPhieuXuat;
                dataGridViewPX.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listPhieuXuat.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridViewPX.Focus();
                dataGridViewPX.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsPhieuXuat.DataSource = null;
                base.Cursor = Cursors.Default;
            }           
        }
        private void dataGridViewPX_SelectionChanged(object sender, EventArgs e)
        {
            NL_PhieuXuat px = bsPhieuXuat.Current as NL_PhieuXuat;
            if (px != null)
            {
                bsPhieuXuatCT.DataSource = px.NL_PhieuXuatCTs;
                dataGridViewCT.Refresh();
                lblChiTietCount.Text = "Tổng số chi tiết:" + px.NL_PhieuXuatCTs.Count.ToString("N0");
                lblChiTietSum.Text = "Tổng tiền:" + px.NL_PhieuXuatCTs.Sum(x => x.ThanhTien).ToString("N3");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            bThem = true;
            try
            {
                NL_PhieuXuat phieuXuat = new NL_PhieuXuat();
                PhieuXuatDialog xuatNLDlg = new PhieuXuatDialog(phieuXuat);
                DialogResult aFormResult = xuatNLDlg.ShowDialog();
                phieuXuat = xuatNLDlg.phieuXuat;
                if (phieuXuat.PhieuXuatID > 0 && xuatNLDlg.comPlate)
                {
                    bsPhieuXuat.Add(phieuXuat);
                    bsPhieuXuat.MoveLast();
                    bsPhieuXuatCT.DataSource = phieuXuat.NL_PhieuXuatCTs;
                    lblChiTietCount.Text = "Tổng số chi tiết:" + phieuXuat.NL_PhieuXuatCTs.Count.ToString("N0");
                    lblChiTietSum.Text = "Tổng tiền:" + phieuXuat.NL_PhieuXuatCTs.Sum(x => x.ThanhTien).ToString("N3");
                    dataGridViewCT.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewPX.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần sửa.");
                return;
            }
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            if (dataGridViewPX.CurrentRow != null)
            {
                try
                {
                    NL_PhieuXuat phieuXuat = bsPhieuXuat.Current as NL_PhieuXuat;
                    if (phieuXuat.KhoaSo == true)
                    {
                        throw new Exception("Phiếu xuất này đã khóa sổ, không sửa được.");
                    }                    
                    PhieuXuatDialog xuatNLDlg = new PhieuXuatDialog(phieuXuat);
                    DialogResult aFormResult = xuatNLDlg.ShowDialog();
                    phieuXuat = xuatNLDlg.phieuXuat;
                    if (xuatNLDlg.comPlate)
                    {
                        bsPhieuXuat.EndEdit();
                        dataGridViewPX.Refresh();
                        bsPhieuXuatCT.DataSource = phieuXuat.NL_PhieuXuatCTs;
                        lblChiTietCount.Text = "Tổng số chi tiết:" + phieuXuat.NL_PhieuXuatCTs.Count.ToString("N0");
                        lblChiTietSum.Text = "Tổng tiền:" + phieuXuat.NL_PhieuXuatCTs.Sum(x => x.ThanhTien).ToString("N3");
                        dataGridViewCT.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewPX.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần xóa.");
                return;
            }
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            NL_PhieuXuat phieuXuat = bsPhieuXuat.Current as NL_PhieuXuat;
            if (phieuXuat.KhoaSo == true)
            {
                MessageBox.Show("Phiếu xuất này đã khóa sổ, không xóa được.");
                return;
            }            
            if (Library.DialogHelper.Confirm("Xóa phiếu xuất này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?id=" + phieuXuat.PhieuXuatID;
                data += "&maNV=" + AppGlobal.User.Username;
                data += "&tenNV=" + AppGlobal.User.FullName;                
                var opStatus = HttpHelper.Delete<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLDeletePhieuXuat" + data);
                if (opStatus.Result!=null)
                    bsPhieuXuat.Remove(phieuXuat);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }          
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dataGridViewPX.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần in.");
                return;
            }
            ShowReport();
        }
        private async void btnKhoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewPX.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần khóa.");
                return;
            }
            if (AppGlobal.User.NL <= 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            
            try
            {
                NL_PhieuXuat phieuXuat = bsPhieuXuat.Current as NL_PhieuXuat;
                if (phieuXuat.KhoaSo == true)
                {
                    MessageBox.Show("Phiếu xuất này đã khóa sổ, không khóa được.");
                    return;
                }
                phieuXuat.KhoaSo = true;
                phieuXuat.ModifyDate = DateTime.Now;
                phieuXuat.ModifyBy = AppGlobal.User.Username;
                phieuXuat.ModifyName = AppGlobal.User.FullName;
                var objpx = await HttpHelper.Put<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLKhoaPhieuXuat", phieuXuat);
                if (objpx == null) throw new Exception(phieuXuat.PhieuXuatID + "- Ngày xuất: " + phieuXuat.NgayXuat);
                phieuXuat = objpx;
                bsPhieuXuat.EndEdit();
                dataGridViewPX.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khóa phiếu xuất: " + ex.Message);
            }
        }
        private async void btnMo_Click(object sender, EventArgs e)
        {
            if (dataGridViewPX.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần mở khóa.");
                return;
            }
            if (AppGlobal.User.NL <= 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }

            try
            {
                NL_PhieuXuat phieuXuat = bsPhieuXuat.Current as NL_PhieuXuat;
                if (phieuXuat.KhoaSo == false)
                {
                    MessageBox.Show("Phiếu xuất này đã mở khóa, không mở khóa được.");
                    return;
                }
                phieuXuat.KhoaSo = false;
                phieuXuat.ModifyDate = DateTime.Now;
                phieuXuat.ModifyBy = AppGlobal.User.Username;
                phieuXuat.ModifyName = AppGlobal.User.FullName;
                var objpx = await HttpHelper.Put<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLKhoaPhieuXuat", phieuXuat);
                if (objpx == null) throw new Exception(phieuXuat.PhieuXuatID + "- Ngày xuất: " + phieuXuat.NgayXuat);
                phieuXuat = objpx;
                bsPhieuXuat.EndEdit();
                dataGridViewPX.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi mở khóa phiếu xuất: " + ex.Message);
            }
        }
        private void ShowReport()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                NL_PhieuXuat phieuXuat = bsPhieuXuat.Current as NL_PhieuXuat;

                List<ReportParameter> rptParamList = new List<ReportParameter>();
                string maDV = AppGlobal.DMTramnlList.Where(x => x.MaTram == phieuXuat.MaTramNL).FirstOrDefault().MaDvql;
                string tenDV = AppGlobal.DonviDMList.Where(x => x.MaDV == maDV).FirstOrDefault().TenDV;
                ReportParameter rptParam = new ReportParameter("prmDonvicha", tenDV.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmDonvicon", "TRẠM NHIÊN LIỆU " + phieuXuat.TenTramNL.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmLoaibc", phieuXuat.NgayXuat.ToString("HH:mm") + " Ngày " + phieuXuat.NgayXuat.ToString("dd") + " tháng " + phieuXuat.NgayXuat.ToString("MM") + " năm " + phieuXuat.NgayXuat.ToString("yyyy"));
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmCount", "Tổng số gồm: " + phieuXuat.NL_PhieuXuatCTs.Count + " mặt hàng");
                rptParamList.Add(rptParam);

                long tongTien = (long)phieuXuat.NL_PhieuXuatCTs.Sum(x => x.ThanhTien);
                string chuTien = FormHelper.NumberToText(tongTien);
                rptParam = new ReportParameter("prmChutien", chuTien);
                rptParamList.Add(rptParam);

                string rptResource = "CBClient.Report.RptPhieuXuat.rdlc";

                string rptName1 = "PhieuXuatDS";
                List<NL_PhieuXuat> listPhieuXuat = new List<NL_PhieuXuat>();
                listPhieuXuat.Add(phieuXuat);

                string rptName2 = "PhieuXuatCTDS";
                List<NL_PhieuXuatCT> listPhieuXuatCT = new List<NL_PhieuXuatCT>();
                listPhieuXuatCT =(from ct in phieuXuat.NL_PhieuXuatCTs                                 
                                   group ct by new { ct.MaDauMo,ct.TenDauMo,ct.DonGia,ct.BangGiaID } into g
                                   select new NL_PhieuXuatCT
                                   {
                                       PhieuXuatID = g.FirstOrDefault().PhieuXuatID,
                                       MaDauMo = g.Key.MaDauMo,
                                       TenDauMo=g.Key.TenDauMo,
                                       DonViTinh=g.FirstOrDefault().DonViTinh,
                                       NhietDo=g.FirstOrDefault().NhietDo,
                                       TyTrong=g.FirstOrDefault().TyTrong,
                                       VCF=g.FirstOrDefault().VCF,
                                       SoLuong = g.Sum(x => x.SoLuong),
                                       SoLuongVCF=g.Sum(x=>x.SoLuongVCF),
                                       PhieuNhapID=g.FirstOrDefault().PhieuNhapID,
                                       DonGia=g.Key.DonGia,
                                       BangGiaID=g.Key.BangGiaID,
                                       ThanhTien = g.Sum(x => x.ThanhTien)
                                   }).ToList();               

                PreViewNXDialog PrintDlg = new PreViewNXDialog
                    (rptResource, rptName1, listPhieuXuat, rptName2, listPhieuXuatCT, rptParamList);
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

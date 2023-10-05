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
    public partial class PhieuNhapForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        private List<DmtramNhienLieu> TramnlList = new List<DmtramNhienLieu>();
        public PhieuNhapForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();           
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhieuNhap);

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
            
            var nhaCCTT = (from ct in AppGlobal.NLNhaccList
                          select new
                            {
                                MaNCC = (short)ct.ID,
                                TenNCC = ct.TenNCC
                            }).ToList();
            nhaCCTT.Add(new { MaNCC = (short)0, TenNCC = "Tất cả" });
            nhaCCTT = nhaCCTT.OrderBy(f => f.MaNCC).ToList();
            cboNhaCCTT.DataSource = nhaCCTT;
            cboNhaCCTT.DisplayMember = "TenNCC";
            cboNhaCCTT.ValueMember = "MaNCC";
            cboNhaCCTT.SelectedIndex = 0;            
        }
        private void PhieuNhapForm_Load(object sender, EventArgs e)
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
                bsPhieuNhap.DataSource = null;                
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
                data += "&maNCC=" + cboNhaCCTT.SelectedValue;
                data += "&maPN=" + txtPhieuNhap.Text;
                data += "&ngayBD=" + sdNgayBD.Value;
                data += "&ngayKT=" + sdNgayKT.Value;
                List<NL_PhieuNhap> listPhieuNhap = HttpHelper.GetList<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLGetPhieuNhap" + data).OrderBy(x=>x.NgayNhap).ToList();                
                if (listPhieuNhap.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsPhieuNhap.DataSource = listPhieuNhap;
                dataGridViewPN.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listPhieuNhap.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridViewPN.Focus();
                dataGridViewPN.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsPhieuNhap.DataSource = null;
                base.Cursor = Cursors.Default;
            }           
        }
        private void dataGridViewPN_SelectionChanged(object sender, EventArgs e)
        {
            NL_PhieuNhap pn = bsPhieuNhap.Current as NL_PhieuNhap;
            if (pn != null)
            {
                bsPhieuNhapCT.DataSource = pn.NL_PhieuNhapCTs;
                dataGridViewCT.Refresh();
                lblChiTietCount.Text = "Tổng số chi tiết:" + pn.NL_PhieuNhapCTs.Count.ToString("N0");
                lblChiTietSum.Text= "Tổng tiền:" + pn.NL_PhieuNhapCTs.Sum(x=>x.ThanhTien).ToString("N3");
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
                NL_PhieuNhap phieuNhap = new NL_PhieuNhap();
                PhieuNhapDialog nhapCBGADlg = new PhieuNhapDialog(phieuNhap);
                DialogResult aFormResult = nhapCBGADlg.ShowDialog();
                phieuNhap = nhapCBGADlg.phieuNhap;
                if (phieuNhap.PhieuNhapID > 0 && nhapCBGADlg.comPlate)
                {
                    bsPhieuNhap.Add(phieuNhap);
                    bsPhieuNhap.MoveLast();
                    bsPhieuNhapCT.DataSource = phieuNhap.NL_PhieuNhapCTs;
                    lblChiTietCount.Text = "Tổng số chi tiết:" + phieuNhap.NL_PhieuNhapCTs.Count.ToString("N0");
                    lblChiTietSum.Text = "Tổng tiền:" + phieuNhap.NL_PhieuNhapCTs.Sum(x => x.ThanhTien).ToString("N3");
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
            if (dataGridViewPN.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần sửa.");
                return;
            }
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            if (dataGridViewPN.CurrentRow != null)
            {
                try
                {
                    NL_PhieuNhap phieuNhap = bsPhieuNhap.Current as NL_PhieuNhap;
                    if(phieuNhap.KhoaSo==true)
                    {
                        throw new Exception("Phiếu nhập này đã khóa sổ, không sửa được.");
                    }    
                    //var checkConLai = phieuNhap.NL_PhieuNhapCTs.Where(x => x.ConLai != x.SoLuongVCF).FirstOrDefault();
                    //if(checkConLai!=null)
                    //{
                    //    throw new Exception("Phiếu nhập này đã xuất, không sửa được.");
                    //}    
                    PhieuNhapDialog nhapCBGADlg = new PhieuNhapDialog(phieuNhap);
                    DialogResult aFormResult = nhapCBGADlg.ShowDialog();                    
                    phieuNhap = nhapCBGADlg.phieuNhap;
                    if (nhapCBGADlg.comPlate)
                    {
                        bsPhieuNhap.EndEdit();
                        dataGridViewPN.Refresh();
                        bsPhieuNhapCT.DataSource = phieuNhap.NL_PhieuNhapCTs;
                        lblChiTietCount.Text = "Tổng số chi tiết:" + phieuNhap.NL_PhieuNhapCTs.Count.ToString("N0");
                        lblChiTietSum.Text = "Tổng tiền:" + phieuNhap.NL_PhieuNhapCTs.Sum(x => x.ThanhTien).ToString("N3");
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
            if (dataGridViewPN.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần xóa.");
                return;
            }
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            NL_PhieuNhap phieuNhap = bsPhieuNhap.Current as NL_PhieuNhap;
            if (phieuNhap.KhoaSo == true)
            {
                MessageBox.Show("Phiếu nhập này đã khóa sổ, không xóa được.");
                return;
            }
            var checkConLai = phieuNhap.NL_PhieuNhapCTs.Where(x => x.ConLai != x.SoLuongVCF).FirstOrDefault();
            if (checkConLai != null)
            {
                MessageBox.Show("Phiếu nhập này đã xuất, không xóa được.");
                return;
            }
            if (Library.DialogHelper.Confirm("Xóa phiếu nhập này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?id=" + phieuNhap.PhieuNhapID;
                data += "&maNV=" + AppGlobal.User.Username;
                data += "&tenNV=" + AppGlobal.User.FullName;                
                var opStatus = HttpHelper.Delete<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLDeletePhieuNhap" + data);
                if (opStatus.Result!=null)
                    bsPhieuNhap.Remove(phieuNhap);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }          
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dataGridViewPN.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần in.");
                return;
            }
            ShowReport();
        }
        private async void btnKhoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewPN.CurrentRow == null)
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
                NL_PhieuNhap phieuNhap = bsPhieuNhap.Current as NL_PhieuNhap;
                if (phieuNhap.KhoaSo == true)
                {
                    MessageBox.Show("Phiếu nhập này đã khóa sổ, không khóa được.");
                    return;
                }
                phieuNhap.KhoaSo = true;
                phieuNhap.ModifyDate = DateTime.Now;
                phieuNhap.ModifyBy = AppGlobal.User.Username;
                phieuNhap.ModifyName = AppGlobal.User.FullName;
                var objpn = await HttpHelper.Put<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLKhoaPhieuNhap", phieuNhap);
                if (objpn == null) throw new Exception(phieuNhap.PhieuNhapID + "- Ngày nhập: " + phieuNhap.NgayNhap);
                phieuNhap = objpn;
                bsPhieuNhap.EndEdit();
                dataGridViewPN.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khóa phiếu nhập: " + ex.Message);
            }
        }
        private async void btnMo_Click(object sender, EventArgs e)
        {
            if (dataGridViewPN.CurrentRow == null)
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
                NL_PhieuNhap phieuNhap = bsPhieuNhap.Current as NL_PhieuNhap;
                if (phieuNhap.KhoaSo == false)
                {
                    MessageBox.Show("Phiếu nhập này đã mở khóa, không mở khóa được.");
                    return;
                }
                phieuNhap.KhoaSo = false;
                phieuNhap.ModifyDate = DateTime.Now;
                phieuNhap.ModifyBy = AppGlobal.User.Username;
                phieuNhap.ModifyName = AppGlobal.User.FullName;
                var objpn = await HttpHelper.Put<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLKhoaPhieuNhap", phieuNhap);
                if (objpn == null) throw new Exception(phieuNhap.PhieuNhapID + "- Ngày nhập: " + phieuNhap.NgayNhap);
                phieuNhap = objpn;
                bsPhieuNhap.EndEdit();
                dataGridViewPN.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khóa phiếu nhập: " + ex.Message);
            }
        }
        private void ShowReport()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                NL_PhieuNhap phieuNhap = bsPhieuNhap.Current as NL_PhieuNhap;

                List<ReportParameter> rptParamList = new List<ReportParameter>();
                string maDV = AppGlobal.DMTramnlList.Where(x => x.MaTram == phieuNhap.MaTramNL).FirstOrDefault().MaDvql;
                string tenDV = AppGlobal.DonviDMList.Where(x => x.MaDV == maDV).FirstOrDefault().TenDV;                
                ReportParameter rptParam = new ReportParameter("prmDonvicha", tenDV.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmDonvicon", "TRẠM NHIÊN LIỆU " + phieuNhap.TenTramNL.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmLoaibc", phieuNhap.NgayNhap.ToString("HH:mm") + " Ngày " +phieuNhap.NgayNhap.ToString("dd")+" tháng " + phieuNhap.NgayNhap.ToString("MM") + " năm " + phieuNhap.NgayNhap.ToString("yyyy"));
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmCount", "Tổng số gồm: " + phieuNhap.NL_PhieuNhapCTs.Count +" mặt hàng");
                rptParamList.Add(rptParam);

                long tongTien = (long)phieuNhap.NL_PhieuNhapCTs.Sum(x => x.ThanhTien * x.Vat);               
                string chuTien = FormHelper.NumberToText(tongTien);
                rptParam = new ReportParameter("prmChutien", chuTien);
                rptParamList.Add(rptParam);

                string rptResource = "CBClient.Report.RptPhieuNhap.rdlc";

                string rptName1 = "PhieuNhapDS";
                List<NL_PhieuNhap> listPhieuNhap = new List<NL_PhieuNhap>();
                listPhieuNhap.Add(phieuNhap);

                string rptName2 = "PhieuNhapCTDS";
                List<NL_PhieuNhapCT> listPhieuNhapCT = new List<NL_PhieuNhapCT>();
                listPhieuNhapCT = phieuNhap.NL_PhieuNhapCTs;                
               
                PreViewNXDialog PrintDlg = new PreViewNXDialog
                    (rptResource, rptName1, listPhieuNhap, rptName2, listPhieuNhapCT, rptParamList);
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

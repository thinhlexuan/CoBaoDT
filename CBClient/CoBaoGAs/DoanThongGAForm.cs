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
    public partial class DoanThongGAForm : DevComponents.DotNetBar.Metro.MetroForm
    {    
        private List<DoanThongGAView> listDoanThong = new List<DoanThongGAView>();
        public DoanThongGAForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
        }

        private void DoanThongGAForm_Load(object sender, EventArgs e)
        {
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
            tableLayoutPanel2.RowStyles[1].Height = 0;

            AppGlobal.MactauList = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTau?CongTac=0&MacTau=")
               .OrderBy(x => x.MacTauID).OrderBy(x => x.CongTacID).ToList();
        }

        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }

        private void fnTraTim()
        {
            rtxtMesage.Text = string.Empty;
            bsDoanThong.DataSource = null;
            bsDoanThongDM.DataSource = null;
            //dgDoanThong.DataSource = null;
            //dgDoanThongDM.DataSource = null;
            dgDoanThongCT.DataSource = null;
            bool _fistTT = true;
            progressBar1.Value = 0;
            try
            {
                base.Cursor = Cursors.WaitCursor;
                string data = "ThangDT=" + cboThangDT.Text;
                data += "&NamDT=" + cboNamDT.Text;
                data += "&LoaiMay=" + cboLoaiMay.SelectedValue;
                data += "&DonVi=" + cboDonVi.SelectedValue;
                data += "&DauMay=" + txtSHDauMay.Text;
                data += "&SoCB=" + txtSHCoBao.Text;
                data += "&TaiXe=" + txtSHTaiXe.Text;
                data += "&MacTau=" + txtSHMacTau.Text;
                var listDT = HttpHelper.GetList<DoanThongGAView>(Configuration.UrlCBApi + "api/CoBaoGAs/GetDoanThongGAView?" + data);
               
                if (listDT.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu đoạn thống.");
                }
                listDoanThong = (from dt in listDT
                                 group dt by new { dt.DoanThongID } into g
                                 select new DoanThongGAView
                                 {
                                     DoanThongID = g.Key.DoanThongID,
                                     CoBaoGoc = g.FirstOrDefault().CoBaoGoc,
                                     SoCB = g.FirstOrDefault().SoCB,
                                     DauMayID = g.FirstOrDefault().DauMayID,
                                     LoaiMayID = g.FirstOrDefault().LoaiMayID,
                                     DvdmName = g.FirstOrDefault().DvdmName,
                                     DvcbID = g.FirstOrDefault().DvcbID,
                                     DvcbName = g.FirstOrDefault().DvcbName,
                                     NgayCB = g.FirstOrDefault().NgayCB,
                                     TaiXe1ID = g.FirstOrDefault().TaiXe1ID,
                                     TaiXe1Name = g.FirstOrDefault().TaiXe1Name,
                                     PhoXe1ID = g.FirstOrDefault().PhoXe1ID,
                                     PhoXe1Name = g.FirstOrDefault().PhoXe1Name,
                                     TaiXe2ID = g.FirstOrDefault().TaiXe2ID,
                                     TaiXe2Name = g.FirstOrDefault().TaiXe2Name,
                                     PhoXe2ID = g.FirstOrDefault().PhoXe2ID,
                                     PhoXe2Name = g.FirstOrDefault().PhoXe2Name,
                                     TaiXe3ID = g.FirstOrDefault().TaiXe3ID,
                                     TaiXe3Name = g.FirstOrDefault().TaiXe3Name,
                                     PhoXe3ID = g.FirstOrDefault().PhoXe3ID,
                                     PhoXe3Name = g.FirstOrDefault().PhoXe3Name,
                                     KiemTra1ID=g.FirstOrDefault().KiemTra1ID,
                                     KiemTra1Name=g.FirstOrDefault().KiemTra1Name,
                                     KiemTra2ID=g.FirstOrDefault().KiemTra2ID,
                                     KiemTra2Name=g.FirstOrDefault().KiemTra2Name,
                                     KiemTra3ID=g.FirstOrDefault().KiemTra3ID,
                                     KiemTra3Name=g.FirstOrDefault().KiemTra3Name,
                                     LenBan = g.FirstOrDefault().LenBan,
                                     NhanMay = g.FirstOrDefault().NhanMay,
                                     RaKho = g.FirstOrDefault().RaKho,
                                     VaoKho = g.FirstOrDefault().VaoKho,
                                     GiaoMay = g.FirstOrDefault().GiaoMay,
                                     XuongBan = g.FirstOrDefault().XuongBan,
                                     DungKB = g.FirstOrDefault().DungKB,
                                     NLBanTruoc = g.FirstOrDefault().NLBanTruoc,
                                     NLThucNhan = g.FirstOrDefault().NLThucNhan,
                                     NLLinh = g.FirstOrDefault().NLLinh,
                                     TramNLID = g.FirstOrDefault().TramNLID,
                                     NLTrongDoan = g.FirstOrDefault().NLTrongDoan,
                                     NLBanSau = g.FirstOrDefault().NLBanSau,
                                     ThangDT = g.FirstOrDefault().ThangDT,
                                     NamDT = g.FirstOrDefault().NamDT,
                                     Createddate = g.FirstOrDefault().Createddate,
                                     Createdby = g.FirstOrDefault().Createdby,
                                     CreatedName = g.FirstOrDefault().CreatedName,
                                     Modifydate = g.FirstOrDefault().Modifydate,
                                     Modifyby = g.FirstOrDefault().Modifyby,
                                     ModifyName = g.FirstOrDefault().ModifyName
                                 }).OrderByDescending(x => x.NhanMay).ToList();
                bsDoanThong.DataSource = listDoanThong;
                lblDoanThong.Text = "Tổng số đoạn thống:" + listDoanThong.Count.ToString("N0");
                if (_fistTT == true) _fistTT = false;
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                rtxtMesage.Text = string.Empty;
                bsDoanThong.DataSource = null;
                bsDoanThongDM.DataSource = null;
                //dgDoanThong.DataSource = null;
                //dgDoanThongDM.DataSource = null;
                dgDoanThongCT.DataSource = null;
                return;
            }
        }

        private async void dgDoanThong_SelectionChanged(object sender, EventArgs e)
        {  
            if (bsDoanThong.Count == 0) return;
            rtxtMesage.Text = string.Empty;
            bsDoanThongDM.DataSource = null;
            //dgDoanThongDM.DataSource = null;
            dgDoanThongCT.DataSource = null;
            tableLayoutPanel2.RowStyles[1].Height = 0;
            if (dgDoanThong.CurrentRow != null)
            {
                try
                {
                    base.Cursor = Cursors.WaitCursor;
                    DoanThongGAView aRow = (DoanThongGAView)dgDoanThong.CurrentRow.DataBoundItem;
                    long cobaoID = string.IsNullOrWhiteSpace(aRow.DoanThongID.ToString()) ? 0 : aRow.DoanThongID;
                    long cobaoIDGoc = string.IsNullOrWhiteSpace(aRow.CoBaoGoc.ToString()) ? 0 : aRow.CoBaoGoc;
                    //Nạp đoạn thống dầu mỡ
                    var listdm = HttpHelper.GetList<DoanThongGADM>(Configuration.UrlCBApi + "api/CoBaoGAs/GetDoanThongGADMView?id=" + cobaoID);
                    if (listdm == null)
                    {
                        throw new Exception("Không có dữ liệu đoạn thống dầu mỡ.");
                    }
                    if (listdm.Count > 0)
                        tableLayoutPanel2.RowStyles[1].Height = 80;
                    bsDoanThongDM.DataSource = listdm;                    
                    lblDoanThongDM.Text = "Tổng số dầu mỡ:" + listdm.Count.ToString("N0");
                    //Nạp đoạn thông chi tiết
                    var listct = HttpHelper.GetList<DoanThongGACT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetDoanThongGACTView?id=" + cobaoID);
                    if (listct==null)
                    {
                        throw new Exception("Không có dữ liệu đoạn thống chi tiết.");
                    }
                    dgDoanThongCT.DataSource = listct;
                    rtxtMesage.Text = await DoanThongGADAO.NapThanhTich(cobaoID);
                    lblDoanThongCT.Text = "Tổng số chi tiết:" + listct.Count.ToString("N0");
                    base.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    base.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message);
                    rtxtMesage.Text = string.Empty;
                    bsDoanThongDM.DataSource = null;
                    //dgDoanThongDM.DataSource = null;
                    dgDoanThongCT.DataSource = null;
                    return;
                }
            }
        }

        private async void btnTinhDT_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH == 5)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listDoanThong.Count;
            foreach (DoanThongGAView dt in listDoanThong)
            {
                progressBar1.Value = i;
                try
                {
                    CoBaoGAALL coBaoALL = new CoBaoGAALL();
                    //Nạp dữ liệu cơ báo
                    coBaoALL = await HttpHelper.Get<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAALL?id=" + dt.DoanThongID);
                    //if (coBaoALL != null && coBaoALL.coBaos.KhoaCB == false)
                    if (coBaoALL != null)
                    { 
                        i++;
                        lblDoanThongDM.Text = "Đang tính cơ báo: " + coBaoALL.coBaoGAs.SoCB + " .Của: " + i + "/" + listDoanThong.Count.ToString("N0");
                        CoBaoGA rowCoBao = coBaoALL.coBaoGAs;
                        List<CoBaoGACT> listcobaoct = coBaoALL.coBaoGAs.coBaoGACTs.OrderBy(f => f.GioDi).ToList();
                        List<CoBaoGADM> listcobaodm = coBaoALL.coBaoGAs.coBaoGADMs;
                        //Nạp dữ liệu đoạn thống
                        DoanThongGA doanThong = await DoanThongGADAO.bindDoanThongToCoBao(coBaoALL.coBaoGAs);
                        List<DoanThongGADM> listdoanthongdm = listcobaodm.Count > 0 ? DoanThongGADAO.bindDoanThongDM(coBaoALL.coBaoGAs, listcobaodm) : new List<DoanThongGADM>();
                        List<DoanThongGACT> listdoanthongct = listcobaoct.Count >= 0 ? DoanThongGADAO.bindDoanThongCT(doanThong.DungKB, coBaoALL.coBaoGAs, listcobaoct) : new List<DoanThongGACT>();
                        //1.Phân bổ nhiên liệu
                        DateTime ngayCB = new DateTime(2023, 1, 1);
                        if (rowCoBao.NgayCB >= ngayCB)
                        {
                            if (rowCoBao.DvcbID == "YV")
                            {
                                rowCoBao.DvcbID = "HN";
                                rowCoBao.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                            }
                            if (rowCoBao.DvdmID == "YV")
                            {
                                rowCoBao.DvdmID = "HN";
                                rowCoBao.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                            }
                            if (rowCoBao.DvcbID == "DN")
                            {
                                rowCoBao.DvcbID = "SG";
                                rowCoBao.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                            }
                            if (rowCoBao.DvdmID == "DN")
                            {
                                rowCoBao.DvdmID = "SG";
                                rowCoBao.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                            }
                        }
                        listdoanthongct=DoanThongGADAO.fnPhanBoNhienLieu(coBaoALL.coBaoGAs, listdoanthongct);
                        //2.Định mức nhiên liệu                   
                        DinhMucGADAO.YVNapNLDinhMuc(coBaoALL.coBaoGAs, listcobaoct, listdoanthongct);
                        DinhMucGADAO.HNNapNLDinhMuc(coBaoALL.coBaoGAs, listdoanthongct);
                        //if (rowCoBao.DvcbID == "HN")
                        //{
                        //    foreach (DoanThongGACT ct in listdoanthongct)
                        //    {
                        //        ct.DinhMuc = ct.TieuThu;
                        //    }
                        //}
                        DinhMucGADAO.VINapNLDinhMuc(coBaoALL.coBaoGAs, listcobaoct, listdoanthongct);
                        // DinhMucDAO.DNNapNLDinhMuc(coBaoALL.coBaos, listdoanthongct);
                        if (rowCoBao.DvcbID == "DN")
                        {                            
                            foreach (DoanThongGACT ct in listdoanthongct)
                            {
                                ct.DinhMuc = ct.TieuThu;
                            }
                        }
                        DinhMucGADAO.SGNapNLDinhMuc(coBaoALL.coBaoGAs, listcobaoct, listdoanthongct);

                        //3. định mức dầu mỡ
                        decimal kmToTal = listdoanthongct.Sum(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                        DinhMucGADAO.YVNapDMDinhMuc(coBaoALL.coBaoGAs, kmToTal, listdoanthongdm);
                        DinhMucGADAO.HNNapDMDinhMuc(coBaoALL.coBaoGAs, kmToTal, listdoanthongdm);
                        DinhMucGADAO.VINapDMDinhMuc(coBaoALL.coBaoGAs, kmToTal, listdoanthongdm);
                        //Gán dữ liệu đoạn thống
                        doanThong.ThangDT = dt.ThangDT;
                        doanThong.NamDT = dt.NamDT;
                        coBaoALL.doanThongGAs = doanThong;
                        coBaoALL.doanThongGAs.doanThongGACTs = listdoanthongct;
                        coBaoALL.doanThongGAs.doanThongGADMs = listdoanthongdm;

                        coBaoALL.doanThongGAs.Modifydate = DateTime.Now;
                        coBaoALL.doanThongGAs.Modifyby = AppGlobal.User.Username;
                        coBaoALL.doanThongGAs.ModifyName = AppGlobal.User.FullName;
                        //Lưu dữ liệu về db  
                        var objdt = await HttpHelper.Put<DoanThongGA>(Configuration.UrlCBApi + "api/CoBaoGAs/PutDoanThongGAALL?id=" + coBaoALL.coBaoGAs.CoBaoID, coBaoALL.doanThongGAs);
                        //if (objdt == null) throw new Exception("Lỗi lưu sửa đoạn thống: " + coBaoALL.coBaoGAs.CoBaoID + "-" + coBaoALL.coBaoGAs.SoCB);
                    }
                }
                catch
                {
                    //Library.DialogHelper.Error(ex.Message);
                    continue;
                }
            }
            fnTraTim();
        }

        private async void btnKhoaDT_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listDoanThong.Count;
            foreach (DoanThongGAView dt in listDoanThong)
            {
                progressBar1.Value = i;
                CoBaoGAALL coBaoALL = new CoBaoGAALL();
                coBaoALL = await HttpHelper.Get<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAALL?id=" + dt.DoanThongID);
                if (coBaoALL != null)
                {
                    i++;
                    lblDoanThongDM.Text = "Đang khóa cơ báo: " + coBaoALL.coBaoGAs.SoCB + " .Của: " + i + "/" + listDoanThong.Count.ToString("N0");
                    //Nạp dữ liệu
                    coBaoALL.coBaoGAs.KhoaCB = true;
                    coBaoALL.coBaoGAs.KhoaCBdate = DateTime.Now;
                    coBaoALL.coBaoGAs.KhoaCBby = AppGlobal.User.Username;
                    coBaoALL.coBaoGAs.KhoaCBName = AppGlobal.User.FullName;
                    //Lưu dữ liệu về db                    
                    try
                    {
                        var objdt = await HttpHelper.Put<CoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/KhoaCoBaoGA?id=" + coBaoALL.coBaoGAs.CoBaoID, coBaoALL.coBaoGAs);
                        if (objdt != null) throw new Exception("Lỗi lưu sửa cơ báo: " + coBaoALL.coBaoGAs.CoBaoID + "-" + coBaoALL.coBaoGAs.SoCB);
                    }
                    catch (Exception ex)
                    {
                        Library.DialogHelper.Error("Lỗi sửa cơ báo: " + ex.Message);
                        return;
                    }
                }
            }
        }

        private async void btnMoKhoaDT_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listDoanThong.Count;
            foreach (DoanThongGAView dt in listDoanThong)
            {
                progressBar1.Value = i;
                CoBaoGAALL coBaoALL = new CoBaoGAALL();
                coBaoALL = await HttpHelper.Get<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAALL?id=" + dt.DoanThongID);
                if (coBaoALL != null)
                {
                    i++;
                    lblDoanThongDM.Text = "Đang mở khóa cơ báo: " + coBaoALL.coBaoGAs.SoCB + " .Của: " + i + "/" + listDoanThong.Count.ToString("N0");
                    //Nạp dữ liệu
                    coBaoALL.coBaoGAs.KhoaCB = false;
                    coBaoALL.coBaoGAs.KhoaCBdate = DateTime.Now;
                    coBaoALL.coBaoGAs.KhoaCBby = AppGlobal.User.Username;
                    coBaoALL.coBaoGAs.KhoaCBName = AppGlobal.User.FullName;
                    //Lưu dữ liệu về db
                    //Sửa đoạn thống
                    try
                    {
                        var objdt = await HttpHelper.Put<CoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/KhoaCoBaoGA?id=" + coBaoALL.coBaoGAs.CoBaoID, coBaoALL.coBaoGAs);
                        if (objdt != null) throw new Exception("Lỗi lưu sửa cơ báo: " + coBaoALL.coBaoGAs.CoBaoID + "-" + coBaoALL.coBaoGAs.SoCB);
                    }
                    catch (Exception ex)
                    {
                        Library.DialogHelper.Error("Lỗi sửa cơ báo: " + ex.Message);
                        return;
                    }
                }
            }
        }

        private async void btnChuyenDT_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 2)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listDoanThong.Count;
            //Chuyển sang tháng trước
            if (Library.DialogHelper.Confirm("Chuyển những cơ báo trên sang tháng trước không?") == System.Windows.Forms.DialogResult.Yes)
            {               
                foreach (DoanThongGAView dt in listDoanThong)
                {
                    progressBar1.Value = i;
                    CoBaoGAALL coBaoALL = new CoBaoGAALL();
                    coBaoALL = await HttpHelper.Get<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAALL?id=" + dt.DoanThongID);
                    if (coBaoALL != null)
                    {
                        i++;
                        lblDoanThongDM.Text = "Đang chuyển cơ báo: " + coBaoALL.coBaoGAs.SoCB + " .Của: " + i + "/" + listDoanThong.Count.ToString("N0");
                        //Nạp dữ liệu
                        if(coBaoALL.doanThongGAs.ThangDT!=1)
                        {
                            coBaoALL.doanThongGAs.ThangDT += -1;
                        }
                        else
                        {
                            coBaoALL.doanThongGAs.ThangDT = 12;
                            coBaoALL.doanThongGAs.NamDT += -1;
                        }
                        coBaoALL.doanThongGAs.Modifydate = DateTime.Now;
                        coBaoALL.doanThongGAs.Modifyby = AppGlobal.User.Username;
                        coBaoALL.doanThongGAs.ModifyName = AppGlobal.User.FullName;
                        //Lưu dữ liệu về db
                        //Sửa đoạn thống
                        try
                        {
                            var objdt = await HttpHelper.Put<DoanThongGA>(Configuration.UrlCBApi + "api/CoBaoGAs/PutDoanThongGA?id=" + coBaoALL.coBaoGAs.CoBaoID, coBaoALL.doanThongGAs);
                            if (objdt != null) throw new Exception("Lỗi lưu chuyển tháng đoạn thống: " + coBaoALL.coBaoGAs.CoBaoID + "-" + coBaoALL.coBaoGAs.SoCB);
                        }
                        catch (Exception ex)
                        {
                            Library.DialogHelper.Error("Lỗi: " + ex.Message);
                            return;
                        }
                    }
                }
            }
            //Chuyển sang tháng sau
            else if (Library.DialogHelper.Confirm("Chuyển những cơ báo trên sang tháng sau không?") == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (DoanThongGAView dt in listDoanThong)
                {
                    progressBar1.Value = i;
                    CoBaoGAALL coBaoALL = new CoBaoGAALL();
                    coBaoALL = await HttpHelper.Get<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAALL?id=" + dt.DoanThongID);
                    if (coBaoALL != null)
                    {
                        i++;
                        lblDoanThongDM.Text = "Đang chuyển cơ báo: " + coBaoALL.coBaoGAs.SoCB + " .Của: " + i + "/" + listDoanThong.Count.ToString("N0");
                        //Nạp dữ liệu
                        if (coBaoALL.doanThongGAs.ThangDT != 12)
                        {
                            coBaoALL.doanThongGAs.ThangDT += 1;
                        }
                        else
                        {
                            coBaoALL.doanThongGAs.ThangDT = 1;
                            coBaoALL.doanThongGAs.NamDT += 1;
                        }
                        coBaoALL.doanThongGAs.Modifydate = DateTime.Now;
                        coBaoALL.doanThongGAs.Modifyby = AppGlobal.User.Username;
                        coBaoALL.doanThongGAs.ModifyName = AppGlobal.User.FullName;
                        //Lưu dữ liệu về db
                        //Sửa đoạn thống
                        try
                        {
                            var objdt = await HttpHelper.Put<DoanThongGA>(Configuration.UrlCBApi + "api/CoBaoGAs/PutDoanThongGA?id=" + coBaoALL.coBaoGAs.CoBaoID, coBaoALL.doanThongGAs);
                            if (objdt != null) throw new Exception("Lỗi lưu chuyển tháng đoạn thống: " + coBaoALL.coBaoGAs.CoBaoID + "-" + coBaoALL.coBaoGAs.SoCB);
                        }
                        catch (Exception ex)
                        {
                            Library.DialogHelper.Error("Lỗi: " + ex.Message);
                            return;
                        }
                    }
                }
            }

        }
    }
}

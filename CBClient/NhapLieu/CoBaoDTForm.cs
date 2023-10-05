using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using CBClient.BLLDaos;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Microsoft.Reporting.WinForms;

namespace CBClient.NhapLieu
{
    public partial class CoBaoDTForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        private CoBao rowCoBao;       
        private List<CoBaoCT> listcobaoct = new List<CoBaoCT>();
        private List<CoBaoDM> listcobaodm = new List<CoBaoDM>();
        private static CoBaoDTForm _form;
        internal static CoBaoDTForm Instance
        {
            get { return _form; }
        }
        public CoBaoDTForm()
        {           
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            _form = this;
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
        }
        private void CoBaoDTForm_Load(object sender, EventArgs e)
        {
            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT"|| ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();
            cboDonVi.DataSource = donViTT;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = 0;

            string[] arRays = new string[] { "Tất cả", "Khởi tạo", "Đã ra kho", "Đã hủy", "Đã xóa", "Đã vào kho", "Đã hoàn thành" };            
            cboTrangthai.Items.AddRange(arRays);
            cboTrangthai.SelectedIndex = 0;

            sdNgayBD.Value = DateTime.Today.AddDays(-1);          
            sdNgayKT.Value = DateTime.Today;
            tblChiTiet.RowStyles[1].Height = 0;
        }        
        private async void btnTraTim_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            rtxtMesage.Text = string.Empty;
            bsCoBao.DataSource = null;            
            //dgCoBao.DataSource = null;
            dgCoBaoDM.DataSource = null;
            dgCoBaoCT.DataSource = null;            
            bool _fistTT = true;
            List<CoBao> listCoBao = new List<CoBao>();
            try
            {
                base.Cursor = Cursors.WaitCursor;
                short loaiTT = (short)cboTrangthai.SelectedIndex;                                                          
                var access_token = string.Format("Bearer {0}{1}", MainForm.Instance.Data.userClientId, MainForm.Instance.Data.access_token);
                DateTime dNgay = sdNgayBD.Value;
                bool isFist = true;
                while (dNgay < sdNgayKT.Value)
                {
                    TimeSpan timeSpan = sdNgayKT.Value - dNgay;
                    int iLen = timeSpan.Days;                    
                    string ngayBD = isFist?dNgay.ToString("yyyy/MM/dd"): dNgay.AddDays(1).ToString("yyyy/MM/dd");
                    if (string.IsNullOrWhiteSpace(txtSHCoBao.Text))
                    {
                        if (timeSpan.Days > 2) iLen = 2;
                        dNgay = isFist ? dNgay.AddDays(iLen - 1) : dNgay.AddDays(iLen);
                    }
                    else
                    {
                        if (timeSpan.Days > 15) iLen = 15;
                        dNgay = isFist ? dNgay.AddDays(iLen - 1) : dNgay.AddDays(iLen);                        
                    }
                    string ngayKT = dNgay.ToString("yyyy/MM/dd");
                    isFist = false;
                    var res = await AuthenticationService.GetListCoBaoDienTuByDate(ngayBD, ngayKT, txtSHCoBao.Text, txtSHDauMay.Text, loaiTT, MainForm.Instance.Data.userName, access_token);
                    if (res == null)
                    {
                        throw new Exception("Cảnh báo: Không lấy được dữ liệu");
                    }
                    if (res.IsOK > 0)
                    {
                        string maDV = cboDonVi.SelectedValue.ToString();
                        var data = maDV != "TCT" ? res.Data.Where(x => x.MaXNVanDung == maDV).ToList() : res.Data.ToList();
                        data = loaiTT != 0 ? data.Where(x => x.TrangThai == loaiTT).ToList() : data;
                        foreach (var obj in data)
                        {
                            CoBao cb = new CoBao();
                            cb.CoBaoID = obj.TrangThai == 6 && !string.IsNullOrWhiteSpace(obj.CoBaoTach) ? long.Parse(obj.CoBaoTach) : 0;
                            cb.CoBaoGoc = obj.CoBaoID.HasValue ? (long)obj.CoBaoID : 0;
                            cb.DauMayID = obj.DauMaySo;
                            cb.DvdmID = obj.MaXNQuanLy;
                            cb.DvdmName = obj.TenXNQuanLy;
                            cb.LoaiMayID = obj.LoaiMay;
                            cb.SoCB = obj.SoCoBao;
                            cb.DvcbID = obj.MaXNVanDung;
                            cb.DvcbName = obj.TenXNVanDung;
                            cb.NgayCB = obj.NgayCoBao.HasValue ? (DateTime)obj.NgayCoBao : DateTime.Today;
                            cb.RutGio = obj.RutGio.HasValue ? (int)obj.RutGio : 0;
                            cb.ChatLuong = obj.ChatLuong;
                            cb.SoLanRaKho = obj.SoLanRaKho.HasValue ? (decimal)obj.SoLanRaKho : 0;

                            cb.TaiXe1ID = obj.TaiXe1_MaSo;
                            cb.TaiXe1Name = obj.TaiXe1_Ten;
                            cb.TaiXe1GioLT = obj.TaiXe1_GioLuuTru.HasValue ? obj.TaiXe1_GioLuuTru.Value : (short)0;
                            cb.PhoXe1ID = obj.TaiXe2_MaSo;
                            cb.PhoXe1Name = obj.TaiXe2_Ten;
                            cb.PhoXe1GioLT = obj.TaiXe2_GioLuuTru.HasValue ? obj.TaiXe2_GioLuuTru.Value : (short)0;
                            cb.TaiXe2ID = obj.TaiXe3_MaSo;
                            cb.TaiXe2Name = obj.TaiXe3_Ten;
                            cb.TaiXe2GioLT = obj.TaiXe3_GioLuuTru.HasValue ? obj.TaiXe3_GioLuuTru.Value : (short)0;
                            cb.PhoXe2ID = obj.TaiXe4_MaSo;
                            cb.PhoXe2Name = obj.TaiXe4_Ten;
                            cb.PhoXe2GioLT = obj.TaiXe4_GioLuuTru.HasValue ? obj.TaiXe4_GioLuuTru.Value : (short)0;
                            cb.TaiXe3ID = obj.TaiXe5_MaSo;
                            cb.TaiXe3Name = obj.TaiXe5_Ten;
                            cb.TaiXe3GioLT = obj.TaiXe5_GioLuuTru.HasValue ? obj.TaiXe5_GioLuuTru.Value : (short)0;
                            cb.PhoXe3ID = obj.TaiXe6_MaSo;
                            cb.PhoXe3Name = obj.TaiXe6_Ten;
                            cb.PhoXe3GioLT = obj.TaiXe6_GioLuuTru.HasValue ? obj.TaiXe6_GioLuuTru.Value : (short)0;

                            cb.LenBan = obj.GioLenBan.HasValue ? obj.GioLenBan.Value : DateTime.Today;
                            cb.NhanMay = obj.GioNhanMay.HasValue ? obj.GioNhanMay.Value : DateTime.Today;
                            cb.RaKho = obj.GioRaKho.HasValue ? obj.GioRaKho.Value : DateTime.Today;

                            cb.VaoKho = obj.GioVaoKho.HasValue ? obj.GioVaoKho.Value : cb.RaKho;
                            cb.GiaoMay = obj.GioGiaoMay.HasValue ? obj.GioGiaoMay.Value : cb.RaKho;
                            cb.XuongBan = obj.GioXuongBan.HasValue ? obj.GioXuongBan.Value : cb.RaKho;

                            cb.NLBanTruoc = obj.NhienLieu_BanTruoc.HasValue ? (int)obj.NhienLieu_BanTruoc : 0;
                            cb.NLThucNhan = obj.NhienLieu_BanNhan.HasValue ? (int)obj.NhienLieu_BanNhan : 0;
                            cb.NLLinh = obj.NhienLieu_Linh.HasValue ? (int)obj.NhienLieu_Linh : 0;
                            cb.TramNLID = obj.NhienLieu_MaTram == null ? string.Empty : obj.NhienLieu_MaTram;
                            cb.NLBanSau = obj.NhienLieu_BanSau.HasValue ? (int)obj.NhienLieu_BanSau : 0;

                            cb.SHDT = obj.ThongTinPhu_SoHieuDuoiTau;
                            cb.MaCB = obj.ThongTinPhu_MaCoBao;
                            cb.DonDocDuong = obj.ThongTinPhu_DonDocDuong.HasValue ? obj.ThongTinPhu_DonDocDuong.Value : 0;
                            cb.DungDocDuong = obj.ThongTinPhu_DungDocDuong.HasValue ? obj.ThongTinPhu_DungDocDuong.Value : 0;
                            cb.DungNoMay = obj.ThongTinPhu_DungNoMay.HasValue ? obj.ThongTinPhu_DungNoMay.Value : 0;
                            cb.GhiChu = obj.GhiChu;
                            cb.GaID = obj.GaID.HasValue ? obj.GaID.Value : 0;
                            cb.GaName = obj.TenGa;

                            cb.Createddate = obj.createddate.HasValue ? (DateTime)obj.createddate : DateTime.Now;
                            cb.Createdby = obj.createdby;
                            cb.CreatedName = obj.createdName;
                            cb.Modifydate = obj.modifydate.HasValue ? (DateTime)obj.modifydate : DateTime.Now;
                            cb.Modifyby = obj.modifyby;
                            cb.ModifyName = obj.modifyName;
                            cb.TrangThai = obj.TenTrangThai;

                            cb.KhoaCB = false;                            
                            listCoBao.Add(cb);
                        }
                    }
                    //else
                    //{
                    //    throw new Exception("Cảnh báo: " + res.msg);
                    //}
                }
                List<CoBao> listCoBaoTT = listCoBao.OrderByDescending(x => x.NhanMay).ToList();
                bsCoBao.DataSource = listCoBaoTT;                
                lblCoBao.Text = "Tổng số cơ báo:" + listCoBaoTT.Count.ToString("N0");
                if(_fistTT==true) _fistTT = false;
                base.Cursor = Cursors.Default;
                btnExport.Enabled = true;
            }
            catch (Exception ex)
            {               
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);               
                listcobaoct = null;
                listcobaodm = null;
                rtxtMesage.Text = string.Empty;
                bsCoBao.DataSource = null;               
                dgCoBaoDM.DataSource = null;
                dgCoBaoCT.DataSource = null;
                return;
            }
        }
        private async void dgCoBao_SelectionChanged(object sender, EventArgs e)
        {
            tblChiTiet.RowStyles[1].Height = 0;
            listcobaoct = new List<CoBaoCT>();
            listcobaodm = new List<CoBaoDM>();
            dgCoBaoDM.DataSource = null;
            dgCoBaoCT.DataSource = null;
            rtxtMesage.Text = string.Empty;
            if (bsCoBao.Count == 0) return;
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    base.Cursor = Cursors.WaitCursor;
                    CoBao rowCoBaoOld= (CoBao)dgCoBao.CurrentRow.DataBoundItem;
                    rowCoBao = rowCoBaoOld;
                    long cobaoID = string.IsNullOrWhiteSpace(rowCoBao.CoBaoID.ToString()) ? 0 : rowCoBao.CoBaoID;
                    long cobaoIDGoc = string.IsNullOrWhiteSpace(rowCoBao.CoBaoGoc.ToString()) ? 0 : rowCoBao.CoBaoGoc;
                    //Nạp dầu mỡ                    
                    listcobaodm = HttpHelper.GetList<CoBaoDM>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoDM?id=" + cobaoID);
                    // dgCoBaoDM.DataSource = listcobaodm;
                    BindingList<CoBaoDM> lstBinding = new BindingList<CoBaoDM>();
                   
                        foreach (CoBaoDM ct in listcobaodm)
                            lstBinding.Add(ct);
                        if (lstBinding.Count > 0)
                            tblChiTiet.RowStyles[1].Height = 80;

                        dgCoBaoDM.DataSource = lstBinding;
                        dgCoBaoDM.Refresh();
                        lblCoBaoDM.Text = "Tổng số cơ báo dầu mỡ:" + listcobaodm.Count.ToString("N0");
                   
                    //Nạp cơ báo chi tiết                    
                    if (rowCoBao.TrangThai == "Đã hoàn thành")//Thì lấy chi tiết bên Cơbaos
                    {
                        try
                        {
                            //rowCoBao = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoID?id=" + rowCoBao.CoBaoID);
                            rowCoBao = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoGocID?id=" + rowCoBao.CoBaoGoc);
                            cobaoID = rowCoBao.CoBaoID;
                            rowCoBao.GaID = rowCoBaoOld.GaID;
                            rowCoBao.GaName = rowCoBaoOld.GaName;
                            //listcobaoct = HttpHelper.GetList<CoBaoCT>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoCTByCoBaoGoc?id=" + cobaoIDGoc);
                        }
                        catch
                        {
                            cobaoID = 0;
                            rowCoBao.CoBaoID = -1;
                        }                        
                        
                    }
                    else
                    {
                        try
                        {
                            var cbDelete = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoGocID?id=" + rowCoBao.CoBaoGoc);
                            if (cbDelete != null)
                            {
                                if (Library.DialogHelper.Confirm("Cảnh báo: " + cbDelete.SoCB + " đã tồn tại.Bạn có chắc chắn xóa và hoàn thành lại không?.") == DialogResult.Yes)
                                {
                                    string data = "?id=" + cbDelete.CoBaoGoc;
                                    data += "&manv=" + AppGlobal.User.Username;
                                    data += "&tennv=" + AppGlobal.User.FullName;
                                    var objcbd = await HttpHelper.Delete<CoBao>(Configuration.UrlCBApi + "api/CoBaos/DeleteCoBaoGocALL" + data);
                                }
                            }
                        }
                        catch { }
                    }
                    // Lấy chi tiết bên lõi QTHH
                    var access_token = string.Format("Bearer {0}{1}", MainForm.Instance.Data.userClientId, MainForm.Instance.Data.access_token);
                    var resCT = await AuthenticationService.PartnerTCTGetCoBaoDienTuByID(cobaoIDGoc, MainForm.Instance.Data.userName, access_token);
                    if (resCT == null || resCT.Data == null)
                    {
                        throw new Exception("Cảnh báo: Không lấy được dữ liệu chi tiết");
                    }
                    if (resCT != null && resCT.Data != null && resCT.Data.CoBaoID > 0)
                    {
                        List<CoBaoCT> listTemp = new List<CoBaoCT>();
                        CoBaoCT cobaoct = new CoBaoCT();
                        CoBaoCT cobaoctOld = new CoBaoCT();
                        //Nếu có chi tiết cơ báo
                        if (resCT.Data.ThongTinChiTietData != null)
                        {
                            foreach (var obj in resCT.Data.ThongTinChiTietData)
                            {
                                cobaoct = new CoBaoCT();
                                cobaoct.CoBaoID = cobaoID;
                                cobaoct.NgayXP = obj.NgayXP.HasValue ? obj.NgayXP.Value : rowCoBao.NgayCB;
                                cobaoct.GioDen = obj.GioDen.HasValue ? obj.GioDen.Value : obj.GioDi.Value;
                                cobaoct.GioDi = obj.GioDi.HasValue ? obj.GioDi.Value : obj.GioDen.Value;
                                cobaoct.GioDon = obj.GioDon.HasValue ? obj.GioDon.Value : 0;
                                cobaoct.TauID= obj.TauID.HasValue ? obj.TauID.Value : 0;
                                cobaoct.MacTauID = obj.MacTau;
                                cobaoct.CongTyID = obj.MaCTSoHuuTau;
                                cobaoct.CongTyName = obj.TenCTSoHuuTau;
                                cobaoct.CongTacID = obj.LoaiTau.HasValue ? obj.LoaiTau.Value : (short)1;
                                cobaoct.CongTacName = obj.TenLoaiTau;
                                cobaoct.GaID = obj.GaID.HasValue ? obj.GaID.Value : 0;
                                cobaoct.GaName = obj.TenGa;
                                cobaoct.TuyenID = obj.TuyenDSVNID.HasValue ? obj.TuyenDSVNID : (short)1;
                                cobaoct.TuyenName = obj.TenTuyenDSVN;
                                cobaoct.Tan = obj.TanSo.HasValue ? obj.TanSo.Value : 0;
                                cobaoct.XeTotal = obj.TongSoXe.HasValue ? obj.TongSoXe.Value : 0;
                                cobaoct.TanXeRong = obj.TanXeRong.HasValue ? obj.TanXeRong.Value : 0;
                                cobaoct.XeRongTotal = obj.SLXeRong.HasValue ? obj.SLXeRong.Value : 0;
                                cobaoct.TinhChatID = obj.TinhChat.HasValue ? obj.TinhChat.Value : (short)1;
                                cobaoct.TinhChatName = obj.TenTinhChat;
                                if(cobaoct.TinhChatID==4)
                                {
                                    cobaoct.Tan = 0;
                                    cobaoct.XeTotal = 0;
                                    cobaoct.TanXeRong = 0;
                                    cobaoct.XeRongTotal = 0;
                                }
                                cobaoct.MayGhepID = obj.MayGhep;
                                cobaoct.KmAdd = obj.KmAdd.HasValue ? obj.KmAdd.Value : 0;
                                if (cobaoctOld != null && cobaoctOld.GioDi == cobaoct.GioDen && cobaoct.GioDi > cobaoct.GioDen)
                                    cobaoct.GioDen=cobaoct.GioDen.AddMinutes(1);
                                listTemp.Add(cobaoct);
                                cobaoctOld = cobaoct;
                            }
                        }
                        //Nếu có chi tiết dồn
                        if (resCT.Data.DonChiTietData != null)
                        {
                            foreach (var objDon in resCT.Data.DonChiTietData)
                            {
                                //den-bd-di
                                var ngayXPMT = listTemp.Where(x => x.GioDen <= objDon.ThoiGianDonBD && x.GioDi >= objDon.ThoiGianDonKT && x.GaID == objDon.GaID && objDon.LoaiDon == 1).FirstOrDefault();
                                if (ngayXPMT != null)
                                {
                                    int gioDung = (int)(ngayXPMT.GioDi - ngayXPMT.GioDen).TotalMinutes;
                                    cobaoct = ngayXPMT;
                                    cobaoct.GioDon += objDon.GioDon.HasValue ? objDon.GioDon.Value : 0;                                    
                                    if (cobaoct.GioDon > (decimal)gioDung)
                                    {
                                        cobaoct.GioDon = (decimal)gioDung;
                                    }
                                    continue;
                                }
                                cobaoct = new CoBaoCT();
                                cobaoct.CoBaoID = cobaoID;
                                cobaoct.NgayXP = objDon.ThoiGianDonBD.HasValue ? objDon.ThoiGianDonBD.Value : objDon.NgayXP.Value;
                                cobaoct.GioDen = objDon.ThoiGianDonBD.HasValue ? objDon.ThoiGianDonBD.Value : objDon.NgayXP.Value;
                                cobaoct.GioDi = objDon.ThoiGianDonKT.HasValue ? objDon.ThoiGianDonKT.Value : objDon.NgayXP.Value;
                                cobaoct.GioDon = objDon.GioDon.HasValue ? objDon.GioDon.Value : 0;
                                cobaoct.TauID = 0;
                                cobaoct.MacTauID = objDon.LoaiDon == 1 ? "KDON" : "CDON";
                                cobaoct.CongTyID = objDon.DiaDiemDon==1? rowCoBao.DvcbID : "C12";
                                cobaoct.CongTyName = objDon.DiaDiemDon == 1 ? rowCoBao.DvcbName : "Tổng công ty";
                                cobaoct.CongTacID = objDon.LoaiDon == 1 ? (short)999 : (short)998;
                                cobaoct.CongTacName = objDon.TenLoaiDon;
                                cobaoct.GaID = objDon.GaID.HasValue ? objDon.GaID.Value : 0;
                                cobaoct.GaName = objDon.TenGa;
                                //Chỗ này căn giờ dồn khi nhập vào sai
                                //Giờ đến nắm trong khoảng giờ dồn
                                var gioDiLoi = listTemp.Where(x => x.GioDen >= cobaoct.GioDen && x.GioDen <= cobaoct.GioDi && x.GioDi >= cobaoct.GioDi && x.GaID == cobaoct.GaID).FirstOrDefault();
                                if (gioDiLoi != null)
                                {
                                    decimal gioDon = (decimal)(gioDiLoi.GioDen - cobaoct.GioDen).TotalMinutes;
                                    cobaoct.GioDon = gioDon <= cobaoct.GioDon ? gioDon - 1 : cobaoct.GioDon;
                                    cobaoct.GioDi = cobaoct.GioDen.AddMinutes((double)cobaoct.GioDon);
                                }
                                //Giờ đi nắm trong khoảng giờ dồn
                                var gioDenLoi = listTemp.Where(x => x.GioDen <= cobaoct.GioDen && x.GioDi >= cobaoct.GioDen && x.GioDi <= cobaoct.GioDi && x.GaID == cobaoct.GaID).FirstOrDefault();
                                if (gioDenLoi != null)
                                {
                                    decimal gioDon = (decimal)(cobaoct.GioDi - gioDenLoi.GioDi).TotalMinutes;
                                    cobaoct.GioDon = gioDon <= cobaoct.GioDon ? gioDon - 1 : cobaoct.GioDon;
                                    cobaoct.GioDen = cobaoct.GioDi.AddMinutes(-(double)cobaoct.GioDon);
                                }
                                //Giờ đến và giờ đi nắm trong khoảng giờ dồn
                                var gioDenDiLoi = listTemp.Where(x => x.GioDen >= cobaoct.GioDen && x.GioDi <= cobaoct.GioDi && x.GaID == cobaoct.GaID).FirstOrDefault();
                                if (gioDenDiLoi != null)
                                {
                                    decimal gioDonDau = (decimal)(gioDenDiLoi.GioDen - cobaoct.GioDen).TotalMinutes;
                                    decimal gioDonCuoi = (decimal)(cobaoct.GioDi - gioDenDiLoi.GioDi).TotalMinutes;
                                    if (gioDonCuoi > gioDonDau)
                                    {
                                        cobaoct.GioDon = gioDonCuoi <= cobaoct.GioDon ? gioDonCuoi - 1 : cobaoct.GioDon;
                                        cobaoct.GioDen = cobaoct.GioDi.AddMinutes(-(double)cobaoct.GioDon);
                                    }
                                    else
                                    {
                                        cobaoct.GioDon = gioDonDau <= cobaoct.GioDon ? gioDonDau - 1 : cobaoct.GioDon;
                                        cobaoct.GioDi = cobaoct.GioDen.AddMinutes((double)cobaoct.GioDon);
                                    }
                                }
                                cobaoct.TuyenID = 1;
                                cobaoct.TuyenName = string.Empty;
                                cobaoct.Tan = 0;
                                cobaoct.XeTotal = 0;
                                cobaoct.TanXeRong = 0;
                                cobaoct.XeRongTotal = 0;
                                cobaoct.TinhChatID = (short)1;
                                cobaoct.TinhChatName = "Máy chính";
                                cobaoct.MayGhepID = string.Empty;
                                cobaoct.KmAdd = 0;
                                listTemp.Add(cobaoct);
                            }
                        }
                        listcobaoct = listTemp.OrderBy(f => f.MacTauID).OrderBy(f => f.GioDi).OrderBy(f => f.GioDen).ToList();
                    }
                    else//Không có dữ liệu chi tiết
                    {
                        throw new Exception("Cảnh báo: Không có dữ liệu dồn chi tiết");
                    }

                    string strResult = CoBaoDAO.NapThanhTichByCoBaoGoc(cobaoIDGoc);
                    rtxtMesage.Text = strResult;

                    dgCoBaoCT.DataSource = listcobaoct;
                    lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
                    base.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    base.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message);
                    listcobaoct = new List<CoBaoCT>();
                    rtxtMesage.Text = string.Empty;
                    dgCoBaoDM.DataSource = null;
                    dgCoBaoCT.DataSource = null;
                    return;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    if (rowCoBao.TrangThai != "Đã ra kho" && rowCoBao.TrangThai != "Đã vào kho")
                        throw new Exception("Cơ báo: " + rowCoBao.SoCB + " " + rowCoBao.TrangThai + ". Ấn nút cập nhật lại cơ báo (nếu cần).");
                    if (listcobaoct.Count <= 0)
                    {
                        if (Library.DialogHelper.Confirm("Cơ báo: " + rowCoBao.SoCB + ". Không có bản ghi chi tiết. Bạn có làm tác nghiệp bãi bỏ hoặc nằm chờ ở ga không?") == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    CBClient.NhapLieu.NhapCBDTDialog nhapCBDTDlg = new CBClient.NhapLieu.NhapCBDTDialog(rowCoBao, listcobaoct,listcobaodm);
                    DialogResult aFormResult = nhapCBDTDlg.ShowDialog();
                    dgCoBao.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    if (rowCoBao.TrangThai != "Đã hoàn thành")
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + rowCoBao.TrangThai + ". Không cập nhật được được.");
                    if (rowCoBao.KhoaCB ==true)
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " đã khóa. Không cập nhật được được.");
                    CBClient.NhapLieu.NhapCBDTDialog nhapCBDTDlg = new CBClient.NhapLieu.NhapCBDTDialog(rowCoBao, listcobaoct,listcobaodm);
                    DialogResult aFormResult = nhapCBDTDlg.ShowDialog();
                    dgCoBao.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        private void btnInCB_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                List<ReportParameter> rptParamList = new List<ReportParameter>();
                                  
                    string tenDV = "THUỘC: " + rowCoBao.DvcbName.ToUpper();
                    ReportParameter rptParam = new ReportParameter("prmLoaibc", tenDV);
                    rptParamList.Add(rptParam);

                    string rptResource = "CBClient.Report.RptCoBao.rdlc";

                    string rptName1 = "CoBaoDS";
                    List<CoBao> listCoBao = new List<CoBao>();
                    listCoBao.Add(rowCoBao);

                    string rptName2 = "CoBaoTTDS";
                    var objthanhtich = CoBaoDAO.NapObThanhTichByCoBaoGoc(rowCoBao.CoBaoGoc);
                    List<BCCoBaoTTInfo> listCoBaoTT = new List<BCCoBaoTTInfo>();
                    listCoBaoTT.Add(objthanhtich);

                    string rptName3 = "CoBaoCTDS";
                    List<BCCoBaoCTInfo> listCoBaoCT = new List<BCCoBaoCTInfo>();
                    int soTT = 1;
                    foreach (CoBaoCT row in listcobaoct)
                    {
                        BCCoBaoCTInfo info = new BCCoBaoCTInfo();
                        info.SoTT = soTT;
                        info.NgayXP = row.NgayXP;
                        info.GioDen = row.GioDen.ToString("HH:mm");
                        info.GioDi = row.GioDi.ToString("HH:mm");
                        info.GioDon = row.GioDon.ToString();
                        info.MacTau = row.MacTauID;
                        info.GaDi = row.GaName;
                        info.Tan = (decimal)row.Tan;
                        info.xeTotal = (int)row.XeTotal;
                        info.TanRong = (decimal)row.TanXeRong;
                        info.xeRong = (int)row.XeRongTotal;
                        info.TinhChat = row.TinhChatName;
                        info.MayGhep = row.MayGhepID;
                        info.KmAdd = (decimal)row.KmAdd;
                        listCoBaoCT.Add(info);
                        soTT++;
                    }

                    string rptName4 = "CoBaoDMDS";
                    CBClient.BaoCao.PreViewDialog PrintDlg = new CBClient.BaoCao.PreViewDialog
                        (rptResource, rptName1, listCoBao, rptName2, listCoBaoTT, rptName3, listCoBaoCT, rptName4, listcobaodm, rptParamList);
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
        public void ShowThanhTich(string strResult)
        {
            rtxtMesage.Text = strResult;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dgCoBao);
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

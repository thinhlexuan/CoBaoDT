using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net.Http;
using CBClient.BLLTypes;
using CBClient.BLLDaos;
using CBClient.Models;
using CBClient.Common;
using CBClient.Services;
using System.Net.NetworkInformation;

namespace CBClient.Library
{
    static class AppGlobal
    {   
        //public static Dijkstra dijkstra;
        public static List<DonViDM> DonviDMList = new List<DonViDM>();
        public static List<KhuDoanInfo> KhuDoanList = new List<KhuDoanInfo>();
        public static List<DMGa> DMGaList = new List<DMGa>();
        //public static List<DmlyTrinh> DMLyTrinhList = new List<DmlyTrinh>();
        public static List<DmtramNhienLieu> DMTramnlList = new List<DmtramNhienLieu>();
        public static List<DMLoaiDauMo> DMLoaidmList = new List<DMLoaiDauMo>();
        public static List<LoaiMay> DMLoaimayList = new List<LoaiMay>();
        public static List<DMDauMay> DMDaumayList = new List<DMDauMay>();
        public static List<DMTaiXe> DMTaixeList = new List<DMTaiXe>();
        public static List<DMPhoXe> DMPhoxeList = new List<DMPhoXe>();
        public static List<DmdonVi> DMDonviList = new List<DmdonVi>();
        public static List<CongTy> CongtyList = new List<CongTy>();
        public static List<CongTac> CongtacList = new List<CongTac>();
        public static List<LoaiTau> LoaitauList = new List<LoaiTau>();        
        public static List<MacTau> MactauList = new List<MacTau>();
        public static List<Tuyen> DMTuyenList = new List<Tuyen>();
        public static List<TuyenMap> DMTuyenmapList = new List<TuyenMap>();
        public static List<TinhChat> TinhchatList = new List<TinhChat>();
        public static List<NhanVien> NhanvienList = new List<NhanVien>();
        public static List<NL_NhaCC> NLNhaccList = new List<NL_NhaCC>();
        public static List<NL_HopDong> HopdongList = new List<NL_HopDong>();
        //public static List<ViewDMNhanVien> ViewDMNhanVienList = new List<ViewDMNhanVien>();
        public static DMNhanVien dmNhanVien = new DMNhanVien();
       
        public static NguoiDung User = new NguoiDung();

        #region AutoComplete
        public static AutoCompleteStringCollection MaDauMayAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaTaiXeAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenTaiXeAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaPhoXeAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenPhoXeAutoComplate { get; set; }       
        public static AutoCompleteStringCollection MaTuyenAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenTuyenAutoComplate { get; set; }
        public static AutoCompleteStringCollection MacTauAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaGaAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenGaAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaTinhChatAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaLoaiMayAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaTramAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenTramAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaDMAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenDMAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaCongTacAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaNhaCCAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenNhaCCAutoComplate { get; set; }
        public static AutoCompleteStringCollection MaHDAutoComplate { get; set; }
        public static AutoCompleteStringCollection TenHDAutoComplate { get; set; }

        #endregion

        #region Dictionary
        public static Dictionary<string, string> DauMayDic { get; set; }
        public static Dictionary<string, string> TaiXeDic { get; set; }
        public static Dictionary<string, string> PhoXeDic { get; set; }
        public static Dictionary<string, string> MacTauDic { get; set; }
        public static Dictionary<string, string> TuyenDic { get; set; }
        public static Dictionary<int, string> GaDic { get; set; }        
        public static Dictionary<short, string> TinhChatDic { get; set; }
        public static Dictionary<string, string> LoaiMayDic { get; set; } 
        public static Dictionary<string, string> CongTacDic { get; set; }
        public static Dictionary<int, string> NLNhaccDic { get; set; }
        
        #endregion

        public static void LoadServiceData()
        {           
            try
            {
                DonviDMList = HttpHelper.GetList<DonViDM>(Configuration.UrlCBApi + "api/DanhMucs/GetDonViDM");
                DMGaList = HttpHelper.GetList<DMGa>(Configuration.UrlCBApi + "api/DanhMucs/GetGa");
                DMTramnlList = HttpHelper.GetList<DmtramNhienLieu>(Configuration.UrlCBApi + "api/DanhMucs/GetDmtramNhienLieu");
                DMLoaidmList = HttpHelper.GetList<DMLoaiDauMo>(Configuration.UrlCBApi + "api/DanhMucs/GetDMLoaiDauMo");
                DMLoaimayList= HttpHelper.GetList<LoaiMay>(Configuration.UrlCBApi + "api/DanhMucs/GetLoaiMay");
                DMDaumayList= HttpHelper.GetList<DMDauMay>(Configuration.UrlCBApi + "api/DanhMucs/GetDauMay");               
                DMDonviList = HttpHelper.GetList<DmdonVi>(Configuration.UrlCBApi + "api/DanhMucs/GetDmdonVi");
                CongtyList = HttpHelper.GetList<CongTy>(Configuration.UrlCBApi + "api/DanhMucs/GetCongTy");
                CongtacList = HttpHelper.GetList<CongTac>(Configuration.UrlCBApi + "api/DanhMucs/GetCongTac");
                LoaitauList = HttpHelper.GetList<LoaiTau>(Configuration.UrlCBApi + "api/DanhMucs/GetLoaiTau");               
                MactauList = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTau?CongTac=0&MacTau=")
                  .OrderBy(x => x.MacTauID).OrderBy(x => x.CongTacID).ToList();
                DMTuyenList = HttpHelper.GetList<Tuyen>(Configuration.UrlCBApi + "api/DanhMucs/GetTuyen");
                DMTuyenmapList = HttpHelper.GetList<TuyenMap>(Configuration.UrlCBApi + "api/DanhMucs/GetTuyenMap");
                TinhchatList = HttpHelper.GetList<TinhChat>(Configuration.UrlCBApi + "api/DanhMucs/GetTinhChat");
                NhanvienList = HttpHelper.GetList<NhanVien>(Configuration.UrlCBApi + "api/DanhMucs/GetNhanVien");
                //ViewDMNhanVienList = HttpHelper.GetList<ViewDMNhanVien>(Configuration.UrlCBApi + "api/DMNhanViens/GetViewDMNhanVien?MaDV=C12&MaNV=");
                TenNhaCCAutoComplate = new AutoCompleteStringCollection();
                NLNhaccDic = new Dictionary<int, string>();
                User = new NguoiDung();
                {
                    User.Username = dmNhanVien.Username;
                    User.FullName = dmNhanVien.FullName;
                    User.MaDV = dmNhanVien.MaDV;
                    User.TenDV = dmNhanVien.TenDV;
                    User.ChucVu = dmNhanVien.ChucVu;
                    User.MaSo = dmNhanVien.MaSo;
                    User.MaQH = 5;
                    User.NL = 0;
                    User.Active = false;
                    try
                    {
                        User.MaDVQL = DMDonviList.Where(x => x.MaDv == User.MaDV).FirstOrDefault().MaCt;                        
                        User.TenDVQL = DonviDMList.Where(x => x.MaDV == User.MaDVQL).FirstOrDefault().TenDV;
                        var nhanVien = NhanvienList.Where(x => x.MaNV == User.Username).FirstOrDefault();
                        if(nhanVien!=null)
                        {
                            User.MaQH = nhanVien.MaQH;
                            User.NL = nhanVien.NL;
                            User.Active = nhanVien.Active;
                            if (User.NL > 0)
                            {
                                NLNhaccList = HttpHelper.GetList<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLGetNhaCC?tenNCC=").ToList();
                                NLNhaccDic = new Dictionary<int, string>();                               
                                TenNhaCCAutoComplate = new AutoCompleteStringCollection();
                                foreach (NL_NhaCC ncc in NLNhaccList)
                                {                                    
                                    TenNhaCCAutoComplate.Add(ncc.TenNCC.ToString());
                                    NLNhaccDic.Add(ncc.ID, ncc.TenNCC);
                                }
                                HopdongList = HttpHelper.GetList<NL_HopDong>(Configuration.UrlCBApi + "api/NhienLieus/NLGetHopDong?tenNCC=&hopDong=").ToList();
                                                             
                                TenHDAutoComplate = new AutoCompleteStringCollection();
                                foreach (NL_HopDong hd in HopdongList)
                                {                                   
                                    TenHDAutoComplate.Add(hd.HopDong.ToString());                                  
                                }
                            }
                        }                       
                    }
                    catch
                    {}

                }

                //DMTaixeList = HttpHelper.GetList<DMTaiXe>(Configuration.UrlCBApi + "api/DanhMucs/GetDMTaiXe?MaDV=" + User.MaDVQL);
                //DMPhoxeList = HttpHelper.GetList<DMPhoXe>(Configuration.UrlCBApi + "api/DanhMucs/GetDMPhoXe?MaDV=" + User.MaDVQL);

                DMTaixeList = HttpHelper.GetList<DMTaiXe>(Configuration.UrlCBApi + "api/DMNhanViens/GetDMTaiXe?MaDV=" + User.MaDVQL);
                DMPhoxeList = HttpHelper.GetList<DMPhoXe>(Configuration.UrlCBApi + "api/DMNhanViens/GetDMPhoXe?MaDV=" + User.MaDVQL);

                DauMayDic = new Dictionary<string, string>();
                MaDauMayAutoComplate = new AutoCompleteStringCollection();               
                foreach (DMDauMay Row in DMDaumayList)
                {
                    MaDauMayAutoComplate.Add(Row.DauMaySo.ToString());
                    DauMayDic.Add(Row.DauMaySo.ToString(), Row.PhanLoai.ToString());
                }

                TaiXeDic = new Dictionary<string, string>();
                MaTaiXeAutoComplate = new AutoCompleteStringCollection();
                TenTaiXeAutoComplate = new AutoCompleteStringCollection();
                foreach (DMTaiXe Row in DMTaixeList)
                {
                    MaTaiXeAutoComplate.Add(Row.TaiXeID.ToString());
                    TenTaiXeAutoComplate.Add(Row.TaiXeName.ToString());
                    //TaiXeDic.Add(Row.TaiXeID.ToString(), Row.TaiXeName.ToString());
                }

                PhoXeDic = new Dictionary<string, string>();
                MaPhoXeAutoComplate = new AutoCompleteStringCollection();
                TenPhoXeAutoComplate = new AutoCompleteStringCollection();
                foreach (DMPhoXe Row in DMPhoxeList)
                {
                    MaPhoXeAutoComplate.Add(Row.PhoXeID.ToString());
                    TenPhoXeAutoComplate.Add(Row.PhoXeName.ToString());
                    //PhoXeDic.Add(Row.PhoXeID.ToString(), Row.PhoXeName.ToString());
                }

                MacTauDic = new Dictionary<string, string>();
                MacTauAutoComplate = new AutoCompleteStringCollection();
                foreach (MacTau Row in MactauList)
                {
                   MacTauAutoComplate.Add(Row.MacTauID.ToString());
                    //MacTauDic.Add(Row["MacTauID"].ToString(), Row["CongTacID"].ToString());
                }

                TuyenDic = new Dictionary<string, string>();
                MaTuyenAutoComplate = new AutoCompleteStringCollection();
                TenTuyenAutoComplate = new AutoCompleteStringCollection();
                var listTuyen = DMTuyenList;
                foreach (Tuyen Row in DMTuyenList)
                {
                    MaTuyenAutoComplate.Add(Row.TuyenID);
                    TenTuyenAutoComplate.Add(Row.TuyenName);
                    TuyenDic.Add(Row.TuyenID, Row.TuyenName);
                }

                GaDic = new Dictionary<int, string>();               
                MaGaAutoComplate = new AutoCompleteStringCollection();
                TenGaAutoComplate = new AutoCompleteStringCollection();               
                foreach (DMGa Row in DMGaList)
                {
                    MaGaAutoComplate.Add(Row.GaId.ToString());
                    TenGaAutoComplate.Add(Row.TenGa);
                    GaDic.Add(Row.GaId, Row.TenGa);
                }

                TinhChatDic = new Dictionary<short, string>();
                MaTinhChatAutoComplate = new AutoCompleteStringCollection();               
                foreach (TinhChat Row in TinhchatList)
                {
                    MaTinhChatAutoComplate.Add(Row.TinhChatId.ToString());
                    TinhChatDic.Add(Row.TinhChatId, Row.TinhChatName);
                }

                LoaiMayDic = new Dictionary<string, string>();
                MaLoaiMayAutoComplate = new AutoCompleteStringCollection();
                foreach (LoaiMay Row in DMLoaimayList)
                {
                    MaLoaiMayAutoComplate.Add(Row.LoaiMayId.ToString());
                    LoaiMayDic.Add(Row.LoaiMayId.ToString(), Row.LoaiMayName.ToString());
                }
              
                MaTramAutoComplate = new AutoCompleteStringCollection();
                TenTramAutoComplate = new AutoCompleteStringCollection();
                foreach (DmtramNhienLieu Row in DMTramnlList)
                {
                    MaTramAutoComplate.Add(Row.MaTram.ToString());
                    TenTramAutoComplate.Add(Row.TenTram.ToString());
                }
               
                MaDMAutoComplate = new AutoCompleteStringCollection();
                TenDMAutoComplate = new AutoCompleteStringCollection();
                foreach (DMLoaiDauMo Row in DMLoaidmList)
                {
                    MaDMAutoComplate.Add(Row.ID.ToString());
                    TenDMAutoComplate.Add(Row.LoaiDauMo.ToString());
                }

                KhuDoanList = new List<KhuDoanInfo>();
                //DataView dvKhuDoan = AppGlobal.LookupDS.Tables["KhuDoanVI"].Copy().DefaultView;
                //foreach (DataRowView Row in dvKhuDoan)
                //{
                //    KhuDoanInfo kd = new KhuDoanInfo();
                //    kd.KhuDoan = Row["KhuDoan"].ToString();
                //    kd.CacGa = Row["CacGa"].ToString();
                //    kd.GhiChu = Row["GhiChu"].ToString();
                //    kd.NgayTH = Convert.ToDateTime(Row["NgayTH"].ToString());
                //    kd.NguoiTH = Row["NguoiTH"].ToString();
                //    KhuDoanList.Add(kd);
                //}
                String firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }           
        }
    }
}

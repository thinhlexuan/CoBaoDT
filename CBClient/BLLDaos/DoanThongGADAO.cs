using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CBClient.BLLDaos
{
    public class DoanThongGADAO
    {
        #region Tinh_DoanThong
        public async static Task<DoanThongGA> bindDoanThongToCoBao(CoBaoGA rowCoBao)
        {
            DoanThongGA dtInfo = new DoanThongGA();
            dtInfo.DoanThongID = rowCoBao.CoBaoID;
            // Giờ dừng kho bãi
            string data = "?NgayGM=" + rowCoBao.GiaoMay + "&LoaiMay=" + rowCoBao.LoaiMayID + "&DauMay=" + rowCoBao.DauMayID + "&CoBaoGoc=" + rowCoBao.CoBaoGoc;
            var rowCoBaoPrev = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoPrev" + data);
            if (rowCoBaoPrev != null)
            {
                dtInfo.DungKB = (int)(rowCoBao.NhanMay - rowCoBaoPrev.GiaoMay).TotalMinutes;
            } 
            dtInfo.ThangDT = rowCoBao.NgayCB.Month;
            dtInfo.NamDT = rowCoBao.NgayCB.Year;
            dtInfo.Createddate = rowCoBao.Createddate;
            dtInfo.Createdby = rowCoBao.Createdby;
            dtInfo.CreatedName = rowCoBao.CreatedName;
            dtInfo.Modifydate = rowCoBao.Modifydate;
            dtInfo.Modifyby = rowCoBao.Modifyby;
            dtInfo.ModifyName = rowCoBao.ModifyName;
            return dtInfo;
        }
        public static List<DoanThongGADM> bindDoanThongDM(CoBaoGA cobao, List<CoBaoGADM> listcobaodm)
        {
            List<DoanThongGADM> listDTDM = new List<DoanThongGADM>();
            try
            {
                if (listcobaodm.Count > 0)
                {
                   foreach(CoBaoGADM cbdm in listcobaodm)
                    {
                        DoanThongGADM dtdm = new DoanThongGADM();
                        dtdm.LoaiDauMoID = cbdm.LoaiDauMoID;
                        dtdm.LoaiDauMoName = cbdm.LoaiDauMoName;
                        dtdm.DonViTinh = cbdm.DonViTinh;

                        dtdm.TieuThu = (cbdm.Nhan + cbdm.Linh) - cbdm.Giao;
                        listDTDM.Add(dtdm);
                    }    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tính đoạn thống dầu mỡ: " + ex.Message);
            }
            return listDTDM;
        }
        public static List<DoanThongGACT> bindDoanThongCT(int dungKB, CoBaoGA cobao, List<CoBaoGACT> listcobaoct)
        {
            listcobaoct = listcobaoct.OrderBy(x => x.GioDi).ToList();
            List<DoanThongGACT> listDTCT = new List<DoanThongGACT>();
            try
            {
                //Trường hợp tác nghiệp bãi bỏ không có cơ báo chi tiết
                if(listcobaoct.Count == 0)
                {
                    DoanThongGACT doanThongct = new DoanThongGACT();
                    doanThongct.DoanThongID = cobao.CoBaoID;
                    doanThongct.STT = 1;
                    doanThongct.NgayXP = cobao.NhanMay;
                    doanThongct.TauID = 0;
                    doanThongct.MacTauID = string.Empty;
                    doanThongct.CongTyID = string.Empty;
                    doanThongct.CongTyName = string.Empty;
                    doanThongct.CongTacID = (short)4;
                    doanThongct.CongTacName = string.Empty;
                    doanThongct.TinhChatID = (short)1;
                    doanThongct.TinhChatName = string.Empty;
                    doanThongct.TuyenID = string.Empty;
                    doanThongct.TuyenName = string.Empty;
                    doanThongct.GaXPID = (int)cobao.GaID;
                    doanThongct.GaXPName = cobao.GaName;
                    doanThongct.GaKTID = doanThongct.GaXPID;
                    doanThongct.GaKTName = doanThongct.GaXPName;
                    doanThongct.MayGhepID = string.Empty;
                    doanThongct.QuayVong = (int)(cobao.VaoKho - cobao.RaKho).TotalMinutes;
                    var gadoanminh = AppGlobal.DonviDMList.Where(x => x.MaDV == cobao.DvcbID).FirstOrDefault().GaDMList;
                    if (gadoanminh.Contains(cobao.GaID.ToString()))
                    {
                        doanThongct.DungDM = (int)(cobao.RaKho - cobao.NhanMay).TotalMinutes;
                        doanThongct.DungKhoDM = (int)(cobao.GiaoMay - cobao.VaoKho).TotalMinutes;
                    }   
                    else
                    {
                        doanThongct.DungDN = (int)(cobao.RaKho - cobao.NhanMay).TotalMinutes;
                        doanThongct.DungKhoDN = (int)(cobao.GiaoMay - cobao.VaoKho).TotalMinutes;
                    } 
                    doanThongct.DungXP = (int)(cobao.VaoKho - cobao.RaKho).TotalMinutes;
                    //doanThongct.TieuThu = cobao.NLThucNhan + cobao.NLLinh - cobao.NLBanSau;
                    //doanThongct.DinhMuc = cobao.NLTrongDoan;

                    listDTCT.Add(doanThongct);
                }    
                //Có bản ghi chi tiết
                else if (listcobaoct.Count > 0)
                {
                    //Duyệt cơ báo chi tiết gán cho đoạn thống chi tiết
                   var listDTCTCN = fnNapDoanThongCT(dungKB, cobao, listcobaoct);
                    //Duyệt lại đoan thống chi tiết để gán lại tuyến khu đoạn chung tuyến
                    DoanThongGACT ctOld = new DoanThongGACT();
                    bool isFisrt = true;
                    foreach (DoanThongGACT ct in listDTCTCN)
                    {
                        //Duyệt những tường hợp có cùng mác tầu và cùng ga đi cùng ga đến nhưng tính chất khác nhau thì phân bổ lại giờ 
                        var ctTrung = listDTCTCN.Where(x => x.MacTauID == ct.MacTauID && x.GaXPID == x.GaKTID && x.TinhChatID != ct.TinhChatID && x.CongTacID != 8 && x.CongTacID != 9).FirstOrDefault();
                        if (ctTrung != null)
                        {
                            if (isFisrt)
                            {
                                ctOld = listDTCTCN.Where(x => x.MacTauID == ct.MacTauID && x.GaXPID == x.GaKTID && x.QuayVong > 0 && x.CongTacID != 8 && x.CongTacID != 9).FirstOrDefault();
                                ct.QuayVong = ctOld.QuayVong / 2;
                                ct.LuHanh = ctOld.LuHanh / 2;
                                ct.DonThuan = ctOld.DonThuan / 2;
                            }
                            else
                            {
                                ct.QuayVong += ctOld.QuayVong;
                                ct.LuHanh += ctOld.LuHanh;
                                ct.DonThuan += ctOld.DonThuan;
                            }
                            isFisrt = false;
                        }
                        decimal sumKM = ct.KMChinh + ct.KMDon + ct.KMDay + ct.KMGhep;
                        if (ct.CongTacID == 8 || ct.CongTacID == 9 || (ct.GaXPID == ct.GaKTID && sumKM <= 0))
                        {
                            ct.DonThuan = 0;
                            ct.LuHanh = 0;
                        }
                        //Nếu đoạn thống chi tiết là của 1 ga thì gán tuyến cho mác tầu của ga gần nó
                        if (ct.GaXPID==ct.GaKTID && ct.CongTacID!=8 && ct.CongTacID!=9)
                        {
                            var tuyenTau = listDTCTCN.Where(x => x.MacTauID == ct.MacTauID && x.GaXPID != x.GaKTID && (x.GaXPID==ct.GaXPID || x.GaKTID == ct.GaXPID)).FirstOrDefault();
                            if (tuyenTau != null)
                            {
                                ct.TuyenID = tuyenTau.TuyenID;
                                ct.TuyenName = tuyenTau.TuyenName;
                            }
                        }
                        //Gán lại tuyến theo tầu và có lý trình dài nhất
                        var maxKMTau = listDTCTCN.Where(x => x.MacTauID == ct.MacTauID).OrderByDescending(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay).FirstOrDefault().TuyenID;
                        string data = "?GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID + "&Tuyen=" + maxKMTau;
                        var lyTrinh = HttpHelper.Get<DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                        if (lyTrinh != null)
                        {
                            ct.TuyenID = lyTrinh.TuyenId;
                            ct.TuyenName = lyTrinh.TuyenName;
                        }                       
                        int[] IntArrayHNHP = { 2972, 3012, 2968 };                       
                        //Nếu dồn Gia Lâm-Long Biên thì gán cho tuyến HNHP
                        if (IntArrayHNHP.Contains(ct.GaXPID) && IntArrayHNHP.Contains(ct.GaKTID) && (ct.CongTacID==8 || ct.CongTacID == 9))
                        {
                            ct.TuyenID = "HNHP";
                            ct.TuyenName = "Hà Nội - Hải Phòng";
                        }                       
                        //Nạp lại Khu đoạn nhiên liệu
                        //Yên Viên
                        if (cobao.DvcbID == "YV")
                        {
                            if (ct.CongTacID == 8|| ct.CongTacID == 9)
                            {
                                ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            }
                            else
                            {                                
                                string CongTac = ct.CongTacID <= 3 ? "Khach" : ct.CongTacName;
                                data = "?LoaiMay=" + cobao.LoaiMayID + "&Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID + "&CongTac=" + CongTac;
                                var khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetKhuDoanOBJ" + data).Result;
                                if (string.IsNullOrWhiteSpace(khuDoan))
                                {
                                    CongTac = ct.CongTacID <= 3 ? "Khach" : string.Empty;
                                    data = "?LoaiMay=" + cobao.LoaiMayID + "&Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID + "&CongTac=" + CongTac;
                                    khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetKhuDoanOBJ" + data).Result;
                                }
                                ct.KhuDoan = khuDoan;                                
                            }
                            
                        }
                        //Hà Nội
                        if (cobao.DvcbID == "HN")
                        {
                            if (ct.CongTacID == 8 || ct.CongTacID == 9)
                            {
                                ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            }
                            else
                            {
                                string CongTac = ct.CongTacID <= 3 ? "Khach" : string.Empty;
                                data = "?NgayHL=" + cobao.NgayCB + "&LoaiMay=" + cobao.LoaiMayID + "&Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID + "&CongTac=" + CongTac;
                                var khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/HaNois/HNGetKhuDoanOBJ" + data).Result;
                                ct.KhuDoan = khuDoan;
                            }
                            var khuDoanMin = listDTCTCN.Where(x => x.MacTauID == ct.MacTauID && (x.KhuDoan.Contains(ct.KhuDoan) || ct.KhuDoan.Contains(x.KhuDoan))).OrderBy(x => x.KhuDoan.Length).FirstOrDefault().KhuDoan;
                            ct.KhuDoan = khuDoanMin.ToString().Contains(";") ? khuDoanMin.ToString().Split(';')[0].ToString() : khuDoanMin.ToString();

                        }
                        //Vinh
                        if (cobao.DvcbID == "VIN")
                        {
                            if (ct.CongTacID == 8 || ct.CongTacID == 9)
                            {
                                ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            }
                            else
                            {
                                string mayDay = string.Empty;
                                if (!string.IsNullOrWhiteSpace(ct.MayGhepID)&&ct.MayGhepID.Substring(0,1)=="D")
                                {
                                    if (ct.TinhChatID == 5)
                                        mayDay = "DN";
                                    else
                                    {
                                        string[] mayGheps = ct.MayGhepID.Split(',');
                                        foreach (var mayGhep in mayGheps)
                                        {
                                            string[] dauMays = mayGhep.Split('-');                                           
                                            if (ct.TinhChatID == 1 && dauMays[2] == "DN")
                                            {
                                                mayDay = "DN";
                                                break;
                                            }
                                        }
                                    }                                   
                                }                                
                                data = "?Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID+"&TinhChat="+ mayDay;
                                var khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/Vinhs/VIGetKhuDoanOBJ" + data).Result;
                                ct.KhuDoan = khuDoan;
                            }
                        }
                        //Đà Nẵng
                        if (cobao.DvcbID == "DN")
                        {
                            if (ct.CongTacID == 8 || ct.CongTacID == 9)
                            {
                                ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            }
                            else
                            {                                
                                data = "?Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID;
                                var khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/DaNangs/DNGetKhuDoanOBJ" + data).Result;
                                ct.KhuDoan = khuDoan;
                            }
                        }
                        //Sài Gon
                        if (cobao.DvcbID == "SG")
                        {
                            if (ct.CongTacID == 8 || ct.CongTacID == 9)
                            {
                                ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            }
                            else
                            {
                                data = "?Tuyen=" + ct.TuyenID + "&GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID;
                                var khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/SaiGons/SGGetKhuDoanOBJ" + data).Result;
                                ct.KhuDoan = khuDoan;
                            }                            
                        }
                    }
                    //Ra ngoài thì nhóm lại lần nữa.
                    listDTCT = (from ct in listDTCTCN
                                group ct by new { ct.TauID, ct.MacTauID, ct.TinhChatID, ct.TuyenID,ct.KhuDoan,ct.MayGhepID, ct.XeTotal, ct.TanXeRong, ct.XeRongTotal } into g
                                       select new DoanThongGACT
                                       {
                                           DoanThongID = g.FirstOrDefault().DoanThongID,
                                           NgayXP = g.FirstOrDefault().NgayXP,
                                           TauID=g.Key.TauID,
                                           MacTauID = g.Key.MacTauID,
                                           CongTyID = g.FirstOrDefault().CongTyID,
                                           CongTyName = g.FirstOrDefault().CongTyName,
                                           CongTacID = g.FirstOrDefault().CongTacID,
                                           CongTacName = g.FirstOrDefault().CongTacName,
                                           TinhChatID = g.Key.TinhChatID,
                                           TinhChatName = g.FirstOrDefault().TinhChatName,
                                           TuyenID = g.Key.TuyenID,
                                           KhuDoan=g.Key.KhuDoan,
                                           TuyenName = g.FirstOrDefault().TuyenName,
                                           GaXPID = g.FirstOrDefault().GaXPID,
                                           GaXPName = g.FirstOrDefault().GaXPName,
                                           GaKTID = g.LastOrDefault().GaKTID,
                                           GaKTName = g.LastOrDefault().GaKTName,
                                           MayGhepID = g.Key.MayGhepID,
                                           QuayVong = g.Sum(f => f.QuayVong),
                                           LuHanh = g.Sum(f => f.LuHanh),
                                           DonThuan = g.Sum(f => f.DonThuan),
                                           DungDM = g.Sum(f => f.DungDM),
                                           DungDN = g.Sum(f => f.DungDN),
                                           DungQD = g.Sum(f => f.DungQD),
                                           DungXP = g.Sum(f => f.DungXP),
                                           DungDD = g.Sum(f => f.DungDD),
                                           DungKT = g.Sum(f => f.DungKT),
                                           DungKhoDM = g.Sum(f => f.DungKhoDM),
                                           DungKhoDN = g.Sum(f => f.DungKhoDN),
                                           DungNM = g.Sum(f => f.DungNM),
                                           DonXP = g.Sum(f => f.DonXP),
                                           DonDD = g.Sum(f => f.DonDD),
                                           DonKT = g.Sum(f => f.DonKT),
                                           KMChinh = g.Sum(f => f.KMChinh),
                                           KMDon = g.Sum(f => f.KMDon),
                                           KMGhep = g.Sum(f => f.KMGhep),
                                           KMDay = g.Sum(f => f.KMDay),
                                           TKMChinh = g.Sum(f=>f.TKMChinh),
                                           TKMDon = g.Sum(f => f.TKMDon),
                                           TKMGhep = g.Sum(f => f.TKMGhep),
                                           TKMDay = g.Sum(f => f.TKMDay),
                                           Tan = g.Sum(f => f.KMChinh + f.KMDon + f.KMGhep + f.KMDay) > 0 ? g.Sum(f => f.TKMChinh + f.TKMDon + f.TKMGhep + f.TKMDay) / g.Sum(f => f.KMChinh + f.KMDon + f.KMGhep + f.KMDay) : 0,
                                           XeTotal = g.Key.XeTotal,
                                           TanXeRong = g.Key.TanXeRong,
                                           XeRongTotal = g.Key.XeRongTotal,
                                           SLRKDM = g.Sum(f => f.SLRKDM),
                                           SLRKDN = g.Sum(f => f.SLRKDN),
                                           RutGioNL=g.Sum(f=>f.RutGioNL)
                                       }).OrderBy(x => x.NgayXP).ToList<DoanThongGACT>();                   
                    //Duyệt lại đoạn thống phân bổ giờ và nhiên liệu
                    //List<DoanThongCT> listDTCTPB = listDTCT.Where(x => x.TinhChatID != 4 && x.TinhChatID != 6).ToList();
                    if (listDTCT.Count > 0)
                        fnPhanBoGio(cobao, ref listDTCT);
                }
                //Thêm số thứ tự.
                short sTT = 1;
                foreach (DoanThongGACT ct in listDTCT)
                {
                    ct.STT = sTT;
                    sTT++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cơ báo số:" + cobao.SoCB + " .Lỗi tính đoạn thống chi tiết: " + ex.Message);
            }
            return listDTCT;
        }
        private static List<DoanThongGACT> fnNapDoanThongCT(int dungKB, CoBaoGA cobao,List<CoBaoGACT> listcobaoct)
        {
            List<DoanThongGACT> listdoanthongct = new List<DoanThongGACT>();
            DoanThongGACT doanThongct = new DoanThongGACT();
            List<DoanThongGACT> listTemp = new List<DoanThongGACT>();
            var gadoanminh = AppGlobal.DonviDMList.Where(x => x.MaDV == cobao.DvcbID).FirstOrDefault().GaDMList;
            CoBaoGACT rowPrev = null;
            CoBaoGACT rowNext = null;
            int soTT = 1;
            string tuyenId = string.Empty;
            string tuyenName = string.Empty;
            string khuDoan = string.Empty;
            foreach (CoBaoGACT row in listcobaoct)
            {
                if (soTT > 1)
                {
                    rowPrev = listcobaoct.Where(x => x.GioDi < row.GioDi && x.GioDi <= row.GioDen).OrderByDescending(x => x.GioDi).FirstOrDefault();
                    if (rowPrev == null)
                        throw new Exception("Mác tầu: " + row.MacTauID + "-Ga: " + row.GaName + " không có hành trình của ga trước.");
                }
                if (soTT < listcobaoct.Count)
                {
                    rowNext = listcobaoct.Where(x => x.GioDen > row.GioDen && x.GioDen >= row.GioDi).OrderBy(x => x.GioDen).FirstOrDefault();
                    if (rowNext == null)
                        throw new Exception("Mác tầu: " + row.MacTauID + "-Ga: " + row.GaName + " không có hành trình của ga sau.");
                }

                doanThongct = new DoanThongGACT();
                doanThongct.DoanThongID = row.CoBaoID;                
                doanThongct.NgayXP = row.GioDi;
                doanThongct.TauID = row.TauID;               
                string macTauFist = row.MacTauID;
                if (row.MacTauID.Contains("/"))
                    macTauFist = row.MacTauID.Split('/')[0];
                doanThongct.MacTauID = macTauFist;
                doanThongct.CongTyID = row.CongTyID;
                doanThongct.CongTyName = row.CongTyName;
                doanThongct.RutGioNL += row.RutGioNL;                
                doanThongct.CongTacID = AppGlobal.LoaitauList.Where(x=>x.LoaiTauID==row.LoaiTauID).FirstOrDefault().CongTacID;
                doanThongct.CongTacName = AppGlobal.CongtacList.Where(x => x.CongTacId == doanThongct.CongTacID).FirstOrDefault().CongTacName;
                var congTac = AppGlobal.MactauList.Where(x => x.MacTauID.ToUpper() == doanThongct.MacTauID.ToUpper()).FirstOrDefault();
                if (congTac != null)
                {
                    doanThongct.CongTacID = congTac.CongTacID;
                    doanThongct.CongTacName = congTac.CongTacName;
                }                
                if (soTT == listcobaoct.Count)
                {
                    if (doanThongct.CongTacID != 8 && doanThongct.CongTacID != 9 && row.KmAdd <= 0)
                    {
                        row.TinhChatID = rowPrev != null ? rowPrev.TinhChatID : row.TinhChatID;                        
                    }
                }
                doanThongct.TinhChatID = (short)row.TinhChatID;
                doanThongct.TinhChatName = AppGlobal.TinhchatList.Where(x => x.TinhChatId == doanThongct.TinhChatID).FirstOrDefault().TinhChatName;
                doanThongct.GaXPID =(int)row.GaID;
                doanThongct.GaXPName = row.GaName;
                if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                {  
                    doanThongct.GaKTID = (int)row.GaID;
                    doanThongct.GaKTName = row.GaName;
                }
                else
                {
                    doanThongct.GaKTID = rowNext != null ? (int)rowNext.GaID : (int)row.GaID;
                    doanThongct.GaKTName = rowNext != null ? rowNext.GaName : row.GaName;
                }
                //decimal kmChay = rowNext != null ? rowNext.KmAdd : 0M;
                decimal kmChay = 0M;
               
                string data = "?GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID+"&Tuyen=";
                var lyTrinh = HttpHelper.Get <DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                kmChay += lyTrinh.GaDiKM.HasValue ? Math.Abs((decimal)lyTrinh.GaDiKM-(decimal)lyTrinh.GaDenKM) : 0M;
                if (rowNext != null && row.GaID == rowNext.GaID)
                {
                    kmChay += row.KmAdd > 0 ? row.KmAdd : rowNext.KmAdd;
                }
                //Chiều vào Tân Ấp-Đồng Chuối.
                if (doanThongct.GaXPID == 3065 && doanThongct.GaKTID == 2956)
                    kmChay += 2.82M;
                //Chiều vào Đồng Chuối-Kim Lũ.
                if (doanThongct.GaXPID == 2956 && doanThongct.GaKTID == 2992)
                    kmChay += -2.82M;
                //Chiều ra Kim Lũ-Đồng Chuối.
                if (doanThongct.GaXPID == 2992 && doanThongct.GaKTID == 2956)
                    kmChay += 3.18M;
                //Chiều ra Đồng Chuối-Tân Ấp.
                if (doanThongct.GaXPID == 2956 && doanThongct.GaKTID == 3065)
                    kmChay += -3.18M;
                //Không chạy qua đường vòng Thanh KHê-Đà Nẵng mà chạy thẳng.
                if ((doanThongct.GaXPID==2991 && doanThongct.GaKTID==3169)|| (doanThongct.GaXPID == 3169 && doanThongct.GaKTID == 2991))
                    kmChay+=-4.4M;
                doanThongct.TuyenID = lyTrinh.TuyenId != null ? lyTrinh.TuyenId : string.Empty;
                doanThongct.TuyenName = lyTrinh.TuyenName != null ? lyTrinh.TuyenName : string.Empty;
                //Nếu là máy khổ rộng đi lên ga Cổ Loa, hoặc ga Đông Anh thì gán lại tuyến về HNQT
                if(cobao.LoaiMayID=="D14Er" || cobao.LoaiMayID == "D19Er")
                {
                    if(doanThongct.GaXPID==2946||doanThongct.GaXPID== 2955||doanThongct.GaKTID == 2946 || doanThongct.GaKTID == 2955)
                    {
                        doanThongct.TuyenID = "HNQT";
                        doanThongct.TuyenName = "Hà Nội - Quán Triều";
                    }    
                }    
                //Nạp Khu đoạn nhiên liệu
                //Yên Viên
                if (cobao.DvcbID == "YV")
                {
                    if (doanThongct.CongTacID == 8||doanThongct.CongTacID==9)
                    {
                        khuDoan = doanThongct.CongTacID == 8 ? "CDON" : "KDON";
                    }
                    else if (soTT != listcobaoct.Count && doanThongct.GaXPID!= doanThongct.GaKTID)
                    {                       
                        string CongTac = doanThongct.CongTacID <= 3 ? "Khach" : doanThongct.CongTacName;
                        data = "?LoaiMay=" + cobao.LoaiMayID +"&Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID + "&CongTac=" + CongTac;
                        khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetKhuDoanOBJ" + data).Result;  
                        if(string.IsNullOrWhiteSpace(khuDoan))
                        { 
                            CongTac = doanThongct.CongTacID <= 3 ? "Khach" : string.Empty;
                            data = "?LoaiMay=" + cobao.LoaiMayID + "&Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID + "&CongTac=" + CongTac;
                            khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetKhuDoanOBJ" + data).Result;
                        }    
                    }                   
                    doanThongct.KhuDoan = khuDoan;                   
                }
                //Hà Nội
                if (cobao.DvcbID == "HN")
                {
                    if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                    {
                        khuDoan = doanThongct.CongTacID == 8 ? "CDON" : "KDON";
                    }
                    else if (soTT != listcobaoct.Count && doanThongct.GaXPID != doanThongct.GaKTID)
                    {
                        string CongTac = doanThongct.CongTacID <= 3 ? "Khach" : string.Empty;
                        data = "?NgayHL=" + cobao.NgayCB + "&LoaiMay=" + cobao.LoaiMayID + "&Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID + "&CongTac=" + CongTac;
                        khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/HaNois/HNGetKhuDoanOBJ" + data).Result;
                    }
                    doanThongct.KhuDoan = khuDoan;
                }
                //Vinh
                if (cobao.DvcbID == "VIN")
                {
                    if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                    {
                        khuDoan = doanThongct.CongTacID == 8 ? "CDON" : "KDON";
                    }
                    else if (soTT != listcobaoct.Count && doanThongct.GaXPID != doanThongct.GaKTID)
                    {
                        string mayDay = string.Empty;
                        if (!string.IsNullOrWhiteSpace(row.MayGhepID) && row.MayGhepID.Substring(0, 1) == "D")
                        {
                            if (doanThongct.TinhChatID == 5)
                                mayDay = "DN";
                            else
                            {
                                string[] mayGheps = doanThongct.MayGhepID.Split(',');
                                foreach (var mayGhep in mayGheps)
                                {
                                    string[] dauMays = mayGhep.Split('-');
                                    if (doanThongct.TinhChatID == 1 && dauMays[2] == "DN")
                                    {
                                        mayDay = "DN";
                                        break;
                                    }
                                }
                            }
                        }                        
                        data = "?Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID + "&TinhChat=" + mayDay;
                        khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/Vinhs/VIGetKhuDoanOBJ" + data).Result;
                    }
                    doanThongct.KhuDoan = khuDoan;
                    if (doanThongct.GaXPID==2969 || doanThongct.GaKTID==2969)//Ga Giáp Bát
                    {
                        if(doanThongct.GaXPID != 2915 || doanThongct.GaKTID != 2915)//Ga Bắc Hồng
                            doanThongct.KhuDoan = "HN-TH";
                        else if (doanThongct.GaXPID != 3012 || doanThongct.GaKTID != 3012)//Ga Gia Lâm
                            doanThongct.KhuDoan = "HN-TH";

                    }                    
                }
                //Đà Nẵng
                if (cobao.DvcbID == "DN")
                {
                    if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                    {
                        khuDoan = doanThongct.CongTacID == 8 ? "CDON" : "KDON";
                    }
                    else if (soTT != listcobaoct.Count && doanThongct.GaXPID != doanThongct.GaKTID)
                    {
                        data = "?Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID;
                        khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/DaNangs/DNGetKhuDoanOBJ" + data).Result;
                    }
                    doanThongct.KhuDoan = khuDoan;
                }
                //Sài Gon
                if (cobao.DvcbID == "SG")
                {
                    if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                    {
                        khuDoan = doanThongct.CongTacID == 8 ? "CDON" : "KDON";
                    }
                    else if (soTT != listcobaoct.Count && doanThongct.GaXPID != doanThongct.GaKTID)
                    {                        
                       data = "?Tuyen=" + doanThongct.TuyenID + "&GaXP=" + doanThongct.GaXPID + "&GaKT=" + doanThongct.GaKTID;
                        khuDoan = HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/SaiGons/SGGetKhuDoanOBJ" + data).Result;
                    }
                    doanThongct.KhuDoan = khuDoan;
                }
                doanThongct.MayGhepID = row.MayGhepID;
                if (soTT == 1)
                {
                    if (gadoanminh.Contains(row.GaID.ToString()))
                        doanThongct.SLRKDM = cobao.SoLanRaKho;
                    else
                        doanThongct.SLRKDN = cobao.SoLanRaKho;
                }
                else
                {
                    doanThongct.SLRKDM = 0;
                    doanThongct.SLRKDN = 0;
                }
                //Quay Vòng
                if (soTT == 1)
                    doanThongct.QuayVong = (int)(row.GioDen - cobao.RaKho).TotalMinutes;
                if (soTT < listcobaoct.Count)
                {
                    doanThongct.QuayVong += (int)(rowNext.GioDen - row.GioDen).TotalMinutes;
                }
                if (soTT == listcobaoct.Count)
                    doanThongct.QuayVong += (int)(cobao.VaoKho - row.GioDen).TotalMinutes;
                //Lữ Hành
                if (soTT == 1 && rowNext != null)
                    doanThongct.LuHanh = (int)(rowNext.GioDen - row.GioDi).TotalMinutes;
                else if (soTT < listcobaoct.Count)
                {
                    if (soTT == listcobaoct.Count - 1 && (rowNext.MacTauID == "CDON" || rowNext.MacTauID == "KDON"))
                    {
                        doanThongct.LuHanh = 0;
                    }
                    else
                    {
                        doanThongct.LuHanh = (int)(rowNext.GioDen - row.GioDen).TotalMinutes;
                    }
                }
                //Đơn Thuần
                if (soTT < listcobaoct.Count)
                {
                    if (doanThongct.CongTacID == 8 || doanThongct.CongTacID == 9)
                        doanThongct.DonThuan = 0;
                    else if (row.GaID == rowNext.GaID && row.KmAdd == 0 && rowNext.KmAdd == 0)
                        doanThongct.DonThuan = 0;
                    else if (row.GaID == rowNext.GaID && row.MacTauID != rowNext.MacTauID)
                        doanThongct.DonThuan = 0;
                    else
                        doanThongct.DonThuan = (int)(rowNext.GioDen - row.GioDi).TotalMinutes;
                }
                //Dừng đoạn 
                if (soTT == 1)
                {
                    if (gadoanminh.Contains(row.GaID.ToString()))
                    {
                        doanThongct.DungKhoDM = dungKB > 1440 || dungKB < 0 ? 0 : dungKB;
                        doanThongct.DungDM = (int)(cobao.RaKho - cobao.NhanMay).TotalMinutes;
                    }
                    else
                    {
                        doanThongct.DungKhoDN = dungKB > 1440 || dungKB < 0 ? 0 : dungKB;
                        doanThongct.DungDN = (int)(cobao.RaKho - cobao.NhanMay).TotalMinutes;
                    }
                }

                if (soTT == listcobaoct.Count)
                {
                    if (gadoanminh.Contains(row.GaID.ToString()))
                        doanThongct.DungDM += (int)(cobao.GiaoMay - cobao.VaoKho).TotalMinutes;
                    else
                        doanThongct.DungDN += (int)(cobao.GiaoMay - cobao.VaoKho).TotalMinutes;
                }
                //Dừng ngoài khu gian
                if (soTT == 1)
                {
                    doanThongct.DonXP += (int)row.PhutDon;
                    doanThongct.DungXP += (int)(row.GioDi - cobao.RaKho).TotalMinutes - (int)row.PhutDon;
                    if (listcobaoct.Count == 1)
                        doanThongct.DungKT = (int)(cobao.VaoKho - row.GioDi).TotalMinutes;
                }
                else if (soTT == listcobaoct.Count)
                {
                    doanThongct.DonKT += (int)row.PhutDon;
                    doanThongct.DungKT += (int)(cobao.VaoKho - row.GioDen).TotalMinutes;
                    if (row.GaID == rowPrev.GaID && row.KmAdd == 0 && rowPrev.KmAdd == 0 && row.MacTauID != "CDON" && row.MacTauID != "KDON")
                        doanThongct.DungKT += (int)(row.GioDen - rowPrev.GioDi).TotalMinutes;
                    doanThongct.DungKT -= (int)row.PhutDon;
                }
                else if (row.MacTauID != rowNext.MacTauID)
                {
                    //Nếu bản ghi cuối cùng là dồn, thì bản ghi bán ghi trước đó tính cho dừng kết thúc
                    if (soTT == listcobaoct.Count - 1 && (rowNext.MacTauID == "CDON" || rowNext.MacTauID == "KDON"))
                    {
                        doanThongct.DonKT += (int)row.PhutDon;
                        doanThongct.DungKT += (int)(row.GioDi - row.GioDen).TotalMinutes;
                        if (row.GaID == rowNext.GaID && rowNext.KmAdd == 0)
                            doanThongct.DungKT += (int)(rowNext.GioDen - row.GioDi).TotalMinutes;
                        doanThongct.DungKT -= (int)row.PhutDon;
                    }
                    else
                    {
                        doanThongct.DonDD += (int)row.PhutDon;
                        doanThongct.DungQD += (int)(row.GioDi - row.GioDen).TotalMinutes;
                        if (row.GaID == rowNext.GaID && rowNext.KmAdd == 0)
                            doanThongct.DungQD += (int)(rowNext.GioDen - row.GioDi).TotalMinutes;
                        doanThongct.DungQD -= (int)row.PhutDon;
                    }

                }
                else
                {
                    //Nếu bản ghi đầu tiên là dồn, thì bản ghi tiếp theo kéo tầu tình thời gian ấy cho dừng xuất phát
                    if (soTT == 2 && (rowPrev.MacTauID == "CDON" || rowPrev.MacTauID == "KDON") && row.MacTauID != "CDON" && row.MacTauID != "KDON")
                    {
                        doanThongct.DonXP += (int)row.PhutDon;
                        doanThongct.DungXP += (int)(row.GioDi - row.GioDen).TotalMinutes;
                        if (row.GaID == rowPrev.GaID && row.KmAdd == 0 && rowPrev.KmAdd == 0)
                            doanThongct.DungXP += (int)(row.GioDen - rowPrev.GioDi).TotalMinutes;
                        doanThongct.DungXP -= (int)row.PhutDon;
                    }
                    else
                    {
                        doanThongct.DonDD += (int)row.PhutDon;
                        doanThongct.DungDD += (int)(row.GioDi - row.GioDen).TotalMinutes;
                        if (row.GaID == rowPrev.GaID && row.MacTauID == rowPrev.MacTauID && row.KmAdd == 0 && rowPrev.KmAdd == 0)
                            doanThongct.DungDD += (int)(row.GioDen - rowPrev.GioDi).TotalMinutes;
                        doanThongct.DungDD -= (int)row.PhutDon;
                    }
                }

                decimal tanChia = (decimal)row.Tan;
                decimal tkmChay = 0M;
                decimal kmThem = 0M;
                //decimal tanChia = (decimal)(row.Tan + row.TanXeRong);
                //Nếu là máy ghép nóng hoặc ghép nguội thì KM=0
                if (row.TinhChatID == 3 || row.TinhChatID == 4)
                {
                    tanChia = 0;
                    tkmChay = 0;
                }                                
                //chia tấn số nếu có máy ghép hoặc đẩy nóng
                else if (!string.IsNullOrWhiteSpace(row.MayGhepID) && row.MayGhepID.Substring(0,1)=="D")
                {
                    string[] mayGheps = row.MayGhepID.Split(',');
                    foreach (var mayGhep in mayGheps)
                    {
                        string[] dauMays = mayGhep.Split('-');
                        if (row.TinhChatID <= 2 && dauMays.Length == 2)
                            throw new Exception("Không có tính chất máy ghép: " + row.MayGhepID + " tại ga: " + row.GaName);
                        if (row.TinhChatID <= 2 && (dauMays[2] == "GH" || dauMays[2] == "GN"))
                        {
                            tkmChay = kmChay * tanChia;
                            continue;
                        }
                        //Các khu đoạn đẩy đèo
                        if ((row.GaID == 3065 && rowNext.GaID == 2956) || (row.GaID == 3065 && rowNext.GaID == 2992) || (row.GaID == 2992 && rowNext.GaID == 2956) || (row.GaID == 2992 && rowNext.GaID == 3065)) //Khu đoạn đèo khe nét
                            data = "?Tuyen=" + lyTrinh.TuyenId + "&KmXP=" + lyTrinh.GaDenKM + "&KmKT=" + lyTrinh.GaDiKM + "&Chieu=di";
                        else if (rowNext.GaID == 2976) //Khu đoạn đèo hải vân
                            data = "?Tuyen=" + lyTrinh.TuyenId + "&KmXP=" + lyTrinh.GaDiKM + "&KmKT=" + lyTrinh.GaDenKM + "&Chieu=di";
                        else//Khu đoạn đèo khác
                            data = "?Tuyen=" + doanThongct.TuyenID + "&KmXP=" + lyTrinh.GaDiKM + "&KmKT=" + lyTrinh.GaDenKM + "&Chieu=" + lyTrinh.Chieu;
                        var congLenhSK = HttpHelper.Get<CongLenhSK>(Configuration.UrlCBApi + "api/CongLenhSKs/GetByLyTrinh" + data).Result;
                        if (congLenhSK != null)
                        {
                            decimal mayChinh = Tanchia(cobao.LoaiMayID, congLenhSK);
                            decimal mayPhu = Tanchia(dauMays[0], congLenhSK);
                            if (congLenhSK.ID == 26)//Lên dốc khe nét
                            {
                                if (doanThongct.GaXPID == 3065)//Ga Tân ấp (chiều vào)
                                {
                                    if (doanThongct.GaKTID == 2956) kmThem = kmChay; //Ga kết thúc đồng chuối
                                    else kmThem = kmChay - (425.96M - 417.75M);
                                }
                                else if (doanThongct.GaXPID == 2992) //Ga Kim lũ (chiều ra)
                                {
                                    if (doanThongct.GaKTID == 2956) kmThem = kmChay;//Ga kết thúc đồng chuối
                                    else kmThem = kmChay - (411.75M - 408.80M);
                                }
                                tkmChay = doanThongct.TinhChatID <=2 && kmChay > kmThem ? (kmChay - kmThem) * (decimal)row.Tan : 0;
                                tanChia = (mayChinh / (mayChinh + mayPhu)) * (decimal)row.Tan;
                                tkmChay += kmThem * tanChia;
                            }
                            else
                            {
                                if ((row.GaID == 2956 || row.GaID == 2976) && row.MayGhepID!=rowNext.MayGhepID)//Nếu gaxp là đồng chuối hoặc hải vân và thay đổi tính chất
                                {
                                    tkmChay = doanThongct.TinhChatID <= 2 ? kmChay * (decimal)row.Tan : 0;
                                }
                                else
                                {
                                    tanChia = (mayChinh / (mayChinh + mayPhu)) * (decimal)row.Tan;
                                    tkmChay = kmChay * tanChia;
                                }
                            }
                        }
                    }
                }
                else
                {
                    tkmChay = kmChay * tanChia;
                }
                switch (row.TinhChatID)
                {
                    case 1:
                        doanThongct.KMChinh = kmChay;
                        doanThongct.TKMChinh = tkmChay;
                        break;
                    case 2:
                        doanThongct.KMDon = kmChay;
                        doanThongct.TKMDon = tkmChay;
                        break;
                    case 3:
                    case 4:
                        doanThongct.KMGhep = kmChay;
                        doanThongct.TKMGhep = tkmChay;
                        break;
                    case 5:
                    case 6:
                        doanThongct.KMDay = kmChay;
                        doanThongct.TKMDay = tkmChay;
                        break;
                }
                doanThongct.Tan = row.Tan;
                doanThongct.XeTotal = row.XeTotal;
                doanThongct.TanXeRong = row.TanXeRong;
                doanThongct.XeRongTotal = row.XeRongTotal;
                if(rowPrev!=null && doanThongct.GaXPID==doanThongct.GaKTID &&(doanThongct.CongTacID!=8||doanThongct.CongTacID!=9))
                {
                    doanThongct.Tan = rowPrev.Tan;
                    doanThongct.XeTotal = rowPrev.XeTotal;
                    doanThongct.TanXeRong = rowPrev.TanXeRong;
                    doanThongct.XeRongTotal = rowPrev.XeRongTotal;
                }
                //Nếu là tầu công trình làm việc ngoài khu gian thì quy về giờ chuyên dồn
                if (rowNext != null || soTT == listcobaoct.Count)
                {
                    string tauCT = doanThongct.MacTauID.ToUpper();
                    //string tauCT = doanThongct.MacTauID.Substring(doanThongct.MacTauID.Length - 2).ToUpper();
                    //if ((doanThongct.CongTacID == 5 || doanThongct.CongTacID == 7)
                    //    && ((row.KmAdd > 0 || rowNext.KmAdd > 0 || tauCT.Contains("C9") || tauCT.Contains("C59")) || (soTT == listcobaoct.Count && (rowPrev.KmAdd > 0 || row.KmAdd > 0 || tauCT.Contains("C9") || tauCT.Contains("C59")))))
                        if ((doanThongct.CongTacID == 5 || doanThongct.CongTacID == 7) && (tauCT.Contains("C9") || tauCT.Contains("C59")))
                        {
                        doanThongct.CongTyID = "C12";
                        doanThongct.CongTyName = "Tổng công ty";
                        doanThongct.CongTacID = 8;
                        doanThongct.CongTacName = "Chuyên dồn";
                        doanThongct.TinhChatID = 1;
                        doanThongct.TinhChatName = "Máy chính";
                        doanThongct.GaKTID = doanThongct.GaXPID;
                        doanThongct.GaKTName = doanThongct.GaKTName;
                        doanThongct.LuHanh = 0;                       
                        doanThongct.DonXP += doanThongct.DonThuan;
                        doanThongct.DonThuan = 0;
                        doanThongct.KMChinh = 0;
                        doanThongct.KMDon = 0;
                        doanThongct.KMGhep = 0;
                        doanThongct.KMDay = 0;
                        doanThongct.TKMChinh = 0;
                        doanThongct.TKMDon = 0;
                        doanThongct.TKMGhep = 0;
                        doanThongct.TKMDay = 0;
                        doanThongct.Tan = 0;
                        doanThongct.XeTotal = 0;
                        doanThongct.TanXeRong = 0;
                        doanThongct.XeRongTotal = 0;
                        doanThongct.KhuDoan = "CDON";
                    }
                }
                listTemp.Add(doanThongct);
                soTT++;
            }
            //Nếu không có tuyến thì lấy tuyến của ga gần nhất có
            foreach (DoanThongGACT ct in listTemp)
            {
               if (ct.GaXPID == ct.GaKTID && (ct.CongTacID!=8||ct.CongTacID!=9))
                {
                    var ctTuyen = listTemp.Where(x => x.GaXPID != ct.GaKTID && x.GaKTID==ct.GaKTID && x.MacTauID==ct.MacTauID).FirstOrDefault();
                    if (ctTuyen != null)
                    {
                        ct.TinhChatID = ctTuyen.TinhChatID;
                        ct.TuyenID = ctTuyen.TuyenID;
                        ct.TuyenName = ctTuyen.TuyenName;
                        ct.KhuDoan = ctTuyen.KhuDoan;
                        ct.MayGhepID = ctTuyen.MayGhepID;
                        ct.Tan = ctTuyen.Tan;
                        ct.XeTotal = ctTuyen.XeTotal;
                        ct.TanXeRong = ctTuyen.TanXeRong;
                        ct.XeRongTotal = ctTuyen.XeRongTotal;
                    }
                }
            }

            listdoanthongct = (from ct in listTemp
                               group ct by new { ct.TauID, ct.MacTauID, ct.TinhChatID, ct.TuyenID,ct.KhuDoan,ct.MayGhepID,ct.Tan,ct.XeTotal,ct.TanXeRong,ct.XeRongTotal } into g
                               select new DoanThongGACT
                               {
                                   DoanThongID = g.FirstOrDefault().DoanThongID,                                   
                                   NgayXP = g.FirstOrDefault().NgayXP,
                                   TauID=g.Key.TauID,
                                   MacTauID = g.Key.MacTauID,
                                   CongTyID = g.FirstOrDefault().CongTyID,
                                   CongTyName = g.FirstOrDefault().CongTyName,
                                   CongTacID = g.FirstOrDefault().CongTacID,
                                   CongTacName = g.FirstOrDefault().CongTacName,
                                   TinhChatID = g.Key.TinhChatID,
                                   TinhChatName = g.FirstOrDefault().TinhChatName,                                  
                                   TuyenID = g.Key.TuyenID,
                                   KhuDoan=g.Key.KhuDoan,
                                   TuyenName = g.FirstOrDefault().TuyenName,
                                   GaXPID = g.FirstOrDefault().GaXPID,
                                   GaXPName = g.FirstOrDefault().GaXPName,
                                   GaKTID=g.LastOrDefault().GaKTID,
                                   GaKTName=g.LastOrDefault().GaKTName,
                                   MayGhepID = g.Key.MayGhepID,
                                   QuayVong = g.Sum(f => f.QuayVong),
                                   LuHanh=g.Sum(f=>f.LuHanh),
                                   DonThuan=g.Sum(f=>f.DonThuan),
                                   DungDM=g.Sum(f=>f.DungDM),
                                   DungDN=g.Sum(f=>f.DungDN),
                                   DungQD=g.Sum(f=>f.DungQD),
                                   DungXP=g.Sum(f=>f.DungXP),
                                   DungDD=g.Sum(f=>f.DungDD),
                                   DungKT=g.Sum(f=>f.DungKT),
                                   DungKhoDM=g.Sum(f=>f.DungKhoDM),
                                   DungKhoDN=g.Sum(f=>f.DungKhoDN),
                                   DungNM = g.Sum(f => f.DungNM),
                                   DonXP =g.Sum(f=>f.DonXP),
                                   DonDD=g.Sum(f=>f.DonDD),
                                   DonKT=g.Sum(f=>f.DonKT),
                                   KMChinh=g.Sum(f=>f.KMChinh),
                                   KMDon=g.Sum(f=>f.KMDon),
                                   KMGhep=g.Sum(f=>f.KMGhep),
                                   KMDay = g.Sum(f => f.KMDay),
                                   TKMChinh =g.Sum(f=>f.TKMChinh),
                                   TKMDon= g.Sum(f => f.TKMDon),
                                   TKMGhep = g.Sum(f => f.TKMGhep),
                                   TKMDay = g.Sum(f => f.TKMDay),
                                   Tan=g.Key.Tan,
                                   XeTotal=g.Key.XeTotal,
                                   TanXeRong=g.Key.TanXeRong,
                                   XeRongTotal=g.Key.XeRongTotal,
                                   SLRKDM = g.Sum(f => f.SLRKDM),
                                   SLRKDN = g.Sum(f => f.SLRKDN),
                                   RutGioNL=g.Sum(f=>f.RutGioNL)
                               }).OrderBy(x => x.NgayXP).ToList<DoanThongGACT>();
            return listdoanthongct;
        }             
        private static decimal Tanchia(string data, CongLenhSK cl)
        {
            decimal tan = 0;
            switch (data)
            {
                case "D4H":tan = (decimal)cl.D4H; break;
                case "D5H": tan = (decimal)cl.D5H; break;
                case "D8E": tan = (decimal)cl.D8E; break;
                case "D9E": tan = (decimal)cl.D9E; break;
                case "D10H": tan = (decimal)cl.D10H; break;
                case "D11H": tan = (decimal)cl.D11H; break;
                case "D12E": tan = (decimal)cl.D12E; break;
                case "D13E": tan = (decimal)cl.D13E; break;
                case "D14Er": tan = (decimal)cl.D14Er; break;
                case "D18E": tan = (decimal)cl.D18E; break;
                case "D19E": tan = (decimal)cl.D19E; break;
                case "D19Er": tan = (decimal)cl.D19Er; break;
                case "D20E": tan = (decimal)cl.D20E; break;
            }
            return tan;
        }
        private static void fnPhanBoGio(CoBaoGA cobao, ref List<DoanThongGACT> listdoanthongct)
        {
            int tongDonDD = (int)cobao.DonDocDuong;
            int tongDungDD = (int)cobao.DungDocDuong;
            int tongDungNM = (int)cobao.DungNoMay;
            int sumDung = listdoanthongct.Sum(ct => ct.DungQD + ct.DungDD);
            int sumDonThuan = listdoanthongct.Sum(ct => ct.DonThuan);
            decimal kmTotal = listdoanthongct.Sum(f => f.KMChinh + f.KMDon + f.KMGhep + f.KMDay);
            if (tongDungNM == 0)
            {
                tongDungNM = sumDung <= 15 ? sumDung : 15;
            }
            int gioDonRunning = 0, gioDungRunning = 0, gioNMRunning = 0; int gioDonThua = 0; int gioDungThua = 0;
            foreach (DoanThongGACT row in listdoanthongct)
            {
                decimal kmChay = row.KMChinh + row.KMDon + row.KMGhep + row.KMDay;
                //Tinh Gio
                if (row.DonThuan > 0)
                {
                    int tongDung = row.DungDD + row.DungQD;
                    int donDD = sumDonThuan > 0 ? tongDonDD * row.DonThuan / sumDonThuan : (int)(tongDonDD * kmChay / kmTotal);
                    int dungDD = sumDonThuan > 0 ? tongDungDD * row.DonThuan / sumDonThuan : (int)(tongDungDD * kmChay / kmTotal);
                    int dungNM = sumDonThuan > 0 ? tongDungNM * row.DonThuan / sumDonThuan : (int)(tongDungNM * kmChay / kmTotal);
                    donDD += gioDonThua;
                    dungDD += gioDungThua;
                    gioDonThua = 0;
                    gioDungThua = 0;
                    //Kiểm tra xem khi phân bổ giờ dừng và dồn vào thì có lớn hơn giờ lữ hành không
                    int tongGioDungDonSauPhanBo = tongDung + row.DonDD + dungDD + donDD;
                    //Nếu giờ lữ hành mà nhỏ hơn giờ dừng và dồn thì kiểm tra chênh lệch 
                    if (row.LuHanh < tongGioDungDonSauPhanBo)
                    {
                        int chenhLech = tongGioDungDonSauPhanBo - row.LuHanh;
                        if (donDD > chenhLech)
                        {
                            donDD = donDD - chenhLech;
                            gioDonThua = chenhLech;
                            gioDungThua = dungDD;
                            dungDD = 0;
                        }
                        else
                        {
                            gioDonThua = 0;
                            gioDungThua = chenhLech;
                            dungDD = dungDD - chenhLech;
                        }
                    }
                    gioDonRunning += donDD;
                    row.DonDD += donDD;
                    gioDungRunning += dungDD;
                    row.DungDD += dungDD;
                    gioNMRunning += dungNM;
                    row.DungNM += dungNM;
                    row.DonThuan = (row.DungDD + row.DungQD + row.DonDD) < row.LuHanh ? row.LuHanh - (row.DungDD + row.DungQD + row.DonDD) : 0;
                }
            }
            //Cộng bù còn lại cho bản ghi cuối
            DoanThongGACT rowC = listdoanthongct.OrderByDescending(x => x.DonThuan).FirstOrDefault();
            int donCC = tongDonDD - gioDonRunning;
            rowC.DonDD += donCC + gioDonThua;
            int dungCC = tongDungDD - gioDungRunning;
            rowC.DungDD += dungCC + gioDungThua;
            rowC.DungNM += tongDungNM - gioNMRunning;
            rowC.DonThuan = (rowC.DungDD + rowC.DungQD + rowC.DonDD) < rowC.LuHanh ? rowC.LuHanh - (rowC.DungDD + rowC.DungQD + rowC.DonDD) : 0;
        }
       
        public static List<DoanThongGACT> fnPhanBoNhienLieu(CoBaoGA cobao, List<DoanThongGACT> listdoanthongctOld)
        {
            List<DoanThongGACT> listdoanthongct = new List<DoanThongGACT>();
            decimal NLTieuThuRunning = 0, NLTieuChuanRunning = 0;
            decimal _NLTieuThu = cobao.NLTrongDoan + cobao.NLThucNhan + cobao.NLLinh - cobao.NLBanSau;
            decimal _NLTieuChuan = cobao.NLTrongDoan;
            //Lấy đoạn thống ct là đơn, ghép nguội và dồn
            var listdoanthongDB = listdoanthongctOld.Where(x => x.TinhChatID == 4 || x.TinhChatID == 2 || x.CongTacID == 8 || x.CongTacID == 9).ToList();
            if (listdoanthongDB.Count > 0)
            {
                foreach (DoanThongGACT row in listdoanthongDB)
                {
                    //Tinh NL                
                    if (NLTieuThuRunning < _NLTieuThu && row.TinhChatID == 2)
                    {
                        decimal nlTieuThu = row.KMDon > 0 ? Math.Round(DoanThongDAO.fnHeSoChayDon(cobao.LoaiMayID) * row.KMDon, 2) : 0;
                        nlTieuThu = _NLTieuThu - NLTieuThuRunning > nlTieuThu ? nlTieuThu : _NLTieuThu- NLTieuThuRunning;
                        NLTieuThuRunning += nlTieuThu;
                        row.TieuThu += nlTieuThu;
                    }
                    else if (NLTieuThuRunning < _NLTieuThu && (row.CongTacID == 8 || row.CongTacID == 9))
                    {
                        decimal gioDon = (decimal)(row.DonXP + row.DonDD + row.DonKT) / 60;
                        decimal nlTieuThu = gioDon > 0 ? Math.Round(DoanThongDAO.fnHeSoDon(cobao.LoaiMayID) * gioDon, 2) : 0;
                        nlTieuThu = _NLTieuThu - NLTieuThuRunning > nlTieuThu ? nlTieuThu : _NLTieuThu- NLTieuThuRunning;
                        NLTieuThuRunning += nlTieuThu;
                        row.TieuThu += nlTieuThu;
                    }
                    listdoanthongct.Add(row);
                }
            }
            //Lấy đoạn thống ct còn lại
            var listdoanthongBT = listdoanthongctOld.Where(x => x.TinhChatID != 4 && x.TinhChatID != 2 && x.CongTacID != 8 && x.CongTacID != 9).ToList();
            if (listdoanthongBT.Count > 0)//Nếu có kéo tầu có tấn số
            {
                NLTieuThuRunning = 0;
                decimal nlDaTieuThu = listdoanthongct.Sum(x => x.TieuThu);
                _NLTieuThu = cobao.NLTrongDoan + cobao.NLThucNhan + cobao.NLLinh - cobao.NLBanSau;
                _NLTieuThu = _NLTieuThu - nlDaTieuThu;
                decimal tkmTotal = listdoanthongBT.Sum(x => x.TKMChinh + x.TKMDon + x.TKMGhep + x.TKMDay);
                foreach (DoanThongGACT row in listdoanthongBT)
                {
                    decimal tKM = row.TKMChinh + row.TKMDon + row.TKMGhep + row.TKMDay;
                    //Tinh NL 
                    decimal nlTieuThu = tkmTotal > 0 ? Math.Round(_NLTieuThu * tKM / tkmTotal, 2) : _NLTieuThu;
                    nlTieuThu = _NLTieuThu - NLTieuThuRunning > nlTieuThu ? nlTieuThu : _NLTieuThu - NLTieuThuRunning;
                    NLTieuThuRunning += nlTieuThu;
                    row.TieuThu += nlTieuThu;
                    decimal nlTieuChuan = tkmTotal > 0 ? Math.Round(_NLTieuChuan * tKM / tkmTotal, 2) : _NLTieuChuan;
                    nlTieuChuan = _NLTieuChuan - NLTieuChuanRunning > nlTieuChuan ? nlTieuChuan : _NLTieuChuan - NLTieuChuanRunning;
                    NLTieuChuanRunning += nlTieuChuan;
                    row.DinhMuc += nlTieuChuan;
                    listdoanthongct.Add(row);
                }
            }
            else//Nếu không có bản ghi nào thì phân bổ nốt nl còn lại
            {
                decimal kmTotal = listdoanthongct.Where(x => x.TinhChatID != 4).Sum(f => f.KMChinh + f.KMDon + f.KMGhep + f.KMDay) + (listdoanthongct.Sum(f => f.DonXP + f.DonDD + f.DonKT) * 10 / 60) +
                (listdoanthongct.Sum(f => f.DungQD + f.DungXP + f.DungDD + f.DungKT) / 60);
                foreach (DoanThongGACT row in listdoanthongct)
                {
                    if (row.TinhChatID != 4)
                    {
                        //Tinh NL
                        decimal kmtinhDoi = (row.KMChinh + row.KMGhep + row.KMDay) +
                               ((row.DonXP + row.DonDD + row.DonKT) * 10 / 60) + ((row.DungQD + row.DungXP + row.DungDD + row.DungKT) / 60);
                        decimal nlTieuThu = kmTotal > 0 ? Math.Round(_NLTieuThu * kmtinhDoi / kmTotal, 2) : _NLTieuThu;
                        nlTieuThu = _NLTieuThu - NLTieuThuRunning > nlTieuThu ? nlTieuThu : _NLTieuThu - NLTieuThuRunning;
                        NLTieuThuRunning += nlTieuThu;
                        row.TieuThu += nlTieuThu;
                        decimal nlTieuChuan = kmTotal > 0 ? Math.Round(_NLTieuChuan * kmtinhDoi / kmTotal, 2) : _NLTieuChuan;
                        nlTieuChuan = _NLTieuChuan - NLTieuChuanRunning > nlTieuChuan ? nlTieuChuan : _NLTieuChuan - NLTieuChuanRunning;
                        NLTieuChuanRunning += nlTieuChuan;
                        row.DinhMuc += nlTieuChuan;
                    }
                }
            }
            //Cộng bù còn lại cho bản ghi cuối
            DoanThongGACT rowC = listdoanthongct.OrderByDescending(x => x.TKMChinh + x.TKMDon + x.TKMGhep + x.TKMDay).FirstOrDefault();
            if (rowC != null)
            {
                rowC.TieuThu += _NLTieuThu - NLTieuThuRunning;
                rowC.DinhMuc += _NLTieuChuan - NLTieuChuanRunning;
            }
            else
            {
                rowC = listdoanthongct.OrderByDescending(x => x.DonXP + x.DonDD + x.DonKT).FirstOrDefault();
                if (rowC != null)
                {
                    rowC.TieuThu += _NLTieuThu - NLTieuThuRunning;
                    rowC.DinhMuc += _NLTieuChuan - NLTieuChuanRunning;
                }
                else
                {
                    rowC = listdoanthongct.OrderByDescending(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay).FirstOrDefault();
                    if (rowC != null)
                    {
                        rowC.TieuThu += _NLTieuThu - NLTieuThuRunning;
                        rowC.DinhMuc += _NLTieuChuan - NLTieuChuanRunning;
                    }
                }
            }
            return listdoanthongct;
        }

        #endregion
        #region Thanh tich
        public static async Task<string> NapThanhTich(long CoBaoID)
        {
            string strResult = string.Empty;
            try
            {
                CoBaoTT info = await HttpHelper.Get<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetThanhTichGA?id=" + CoBaoID);
                if (info != null)
                {
                    strResult =
                      " 1.Cơ báo số: " + info.SoCB + "-" + info.CoBaoID + "-" + info.CoBaoGoc + ".\r\n"
                    + " 2.Ngày cơ báo: " + info.NgayCB.ToString("dd.MM.yyyy") + ".\r\n"
                    + " 3.Số hiệu ĐM: " + info.DauMayID + ".\r\n"
                    + " 4.Giờ đầu máy: " + Math.Round(info.QuayVong / 60M, 2) + " Giờ.\r\n"
                    + " 5.Giờ lữ hành: " + Math.Round(info.LuHanh / 60M, 2) + " Giờ.\r\n"
                    + " 6.Giờ đơn thuần: " + Math.Round(info.DonThuan / 60M, 2) + " Giờ.\r\n"
                    + " 7.Giờ dừng: " + Math.Round(info.GioDung / 60M, 2) + " Giờ.\r\n"
                    + " 8.Giờ dồn: " + Math.Round(info.GioDon / 60M, 2) + " Giờ.\r\n"
                    + " 9.KM chạy ĐM: " + Math.Round(info.KM, 2) + " KM.\r\n"
                    + "10.Tấn tổng trọng: " + Math.Round(info.TKM, 0) + " TấnKM.\r\n"
                    + "11.NL tiêu chuẩn: " + Math.Round(info.NLTieuChuan, 2) + " Lít.\r\n"
                    + "12.NL tiêu thụ: " + Math.Round(info.NLTieuThu, 2) + " Lít.\r\n"
                    + "13.NL lời lỗ: " + Math.Round(info.NLLoiLo, 2) + " Lít.";
                }
            }
            //catch (Exception ex)
            catch
            {
                //DialogHelper.Error(ex.Message);
                strResult = string.Empty;
            }
            return strResult;
        }
        public async static Task<BCCoBaoTTInfo> ObjNapThanhTich(long CoBaoID)
        {
            BCCoBaoTTInfo infobc = new BCCoBaoTTInfo();
            try
            {
                CoBaoTT info = await HttpHelper.Get<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetThanhTichGA?id=" + CoBaoID);
                if (info != null)
                {
                    infobc = new BCCoBaoTTInfo();
                    infobc.GioDM = (decimal)info.QuayVong / 60;
                    infobc.GioLH = (decimal)info.LuHanh / 60;
                    infobc.GioDT = (decimal)info.DonThuan / 60;
                    infobc.GioDung = (decimal)info.GioDung / 60;
                    infobc.GioDon = (decimal)info.GioDon / 60M;
                    infobc.KMChay = info.KM;
                    infobc.TKM = info.TKM;
                    infobc.DinhMuc = info.NLTieuChuan;
                    infobc.TieuThu = info.NLTieuThu;
                    infobc.LoiLo = info.NLLoiLo;

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(info.SoCB, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, null, 15);
                    MemoryStream ms = new MemoryStream();
                    qrCodeImage.Save(ms, ImageFormat.Png);  // save bitmap to a memory stream
                    infobc.Qrcode = ms.ToArray();
                }
            }            
            catch
            {               
                infobc = null;
            }
            return infobc;
        }
        #endregion
    }
}

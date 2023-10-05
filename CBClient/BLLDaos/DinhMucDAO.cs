using CBClient.BLLTypes;
using CBClient.Models;
using CBClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLDaos
{
    public class DinhMucDAO
    {
        #region YV_DinhMuc       
        public static void YVNapNLDinhMuc(CoBao rowCoBao, List<CoBaoCT> listCoBaoCT, List<DoanThongCT> listdoanthongct)
        {            
            try
            {
                if (rowCoBao.DvcbID == "YV")
                {
                    bool fistKepN = true;
                    foreach (DoanThongCT ct in listdoanthongct)
                    {
                        string cacGa = string.Empty;
                        int sumDon = ct.DonXP + ct.DonDD + ct.DonKT;
                        //1.Nạp khu đoạn
                        //Nếu là chuyên dồn
                        if (ct.CongTacID == 8)
                        {
                            ct.KhuDoan = "CDON";
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&CongTac=" + ct.KhuDoan;
                            data += "&KhuDoan=" + ct.GaXPID;
                            var dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                decimal gioDon = (decimal)sumDon / 60;
                                ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }
                            else
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=RDA";
                                data += "&KhuDoan=";
                                dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    decimal gioDon = (decimal)sumDon / 60;
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                            }
                            //Nếu dồn nhỏ hơn 120p thì cộng thêm
                            if (sumDon < 120)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=CDON120";
                                data += "&KhuDoan=";
                                dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "CDON120=" + dmdon.DinhMuc + " " + dmdon.DonVi;
                                }
                            }
                        }
                        //Nếu là kiêm dồn
                        else if(ct.CongTacID==9)
                        {
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&CongTac=KDON";
                            data += "&KhuDoan=";
                            var dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                decimal gioDon = (decimal)sumDon / 60;
                                ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += "KDON=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }
                        }
                        //Nếu là tác nghiệp bãi bỏ
                        else if (listdoanthongct.Count==1 && string.IsNullOrWhiteSpace(ct.MacTauID))
                        {
                            if (fistKepN == true)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=BAIBO";
                                data += "&KhuDoan=";
                                var dmBB = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                               
                                if (dmBB != null)
                                {
                                    ct.DinhMuc += (decimal)dmBB.DinhMuc;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "BAIBO=" + dmBB.DinhMuc + " " + dmBB.DonVi;                                   
                                }
                            }
                        }
                        //Nếu là Ghép nguội
                        else if (ct.TinhChatID == 4)
                        {
                            var listDoanThongCTKhongDon = listdoanthongct.Where(x => x.CongTacID != 8 && x.CongTacID != 9).ToList();
                            var knCoKeoTau = listDoanThongCTKhongDon.Where(x => x.TinhChatID != 4 && x.TinhChatID != 2).FirstOrDefault();
                            if(knCoKeoTau!=null) fistKepN = false;
                            if (fistKepN == true)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=KEPN";
                                data += "&KhuDoan=";
                                var dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "KEPN=" + dmdon.DinhMuc + " " + dmdon.DonVi;
                                    fistKepN = false;
                                }
                            }
                        }                        
                        else
                        {
                            //Nạp định mức
                            string CongTac = ct.CongTacID <= 3 ? "Khach" : ct.CongTacName;
                            decimal KMTotal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                            decimal TKMTotal = ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay;
                            decimal tanBQ = KMTotal > 0 ? TKMTotal / KMTotal : 0;
                            if(ct.XeRongTotal>10)
                            {
                                tanBQ += (ct.XeRongTotal - 10) * 4;
                            }    
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&KhuDoan=" + ct.KhuDoan;
                            string  dataCT = data + "&CongTac=" + CongTac;
                            decimal tanDM = decimal.Parse(HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetTanMax" + dataCT).Result);
                            if(tanDM<=0)
                            {
                                CongTac = ct.CongTacID <= 3 ? "Khach" : string.Empty;
                                dataCT = data + "&CongTac=" + CongTac;
                                tanDM = decimal.Parse(HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/YenViens/YVGetTanMax" + dataCT).Result);
                            } 
                            if (tanDM > 0)                            
                                tanDM = tanBQ > tanDM ? tanDM : tanBQ;
                            else
                                tanDM = tanBQ;
                            data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&KhuDoan=" + ct.KhuDoan;
                            data += "&Tan=" + tanDM;
                            dataCT = data + "&CongTac=" + CongTac;
                            var dmnl = HttpHelper.Get<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLDinhMucOBJ" + dataCT).Result;
                            if (dmnl != null)
                            {
                                if (dmnl.DonVi == "Lít/Km")
                                {
                                    ct.DinhMuc += (decimal)(dmnl.DinhMuc + (dmnl.HeSo * tanDM)) * KMTotal;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.CongTacName + "=(" + dmnl.DinhMuc + "+" + dmnl.HeSo + "*" + tanDM + ")*" + KMTotal;
                                }
                                else
                                {
                                    ct.DinhMuc += (decimal)(dmnl.DinhMuc - (dmnl.HeSo * tanDM)) * tanBQ * KMTotal * 0.0001M;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.CongTacName + "=(" + dmnl.DinhMuc + "-" + dmnl.HeSo + "*" + tanDM + ")*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001";
                                }

                            }
                            else
                            {
                                CongTac = ct.CongTacID <= 3 ? "Khach" : string.Empty;
                                dataCT = data + "&CongTac=" + CongTac;
                                dmnl = HttpHelper.Get<YVNLDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLDinhMucOBJ" + dataCT).Result;
                                if (dmnl != null)
                                {
                                    if (dmnl.DonVi == "Lít/Km")
                                    {
                                        ct.DinhMuc += (decimal)(dmnl.DinhMuc + (dmnl.HeSo * tanDM)) * KMTotal;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += ct.CongTacName + "=(" + dmnl.DinhMuc + "+" + dmnl.HeSo + "*" + tanDM + ")*" + KMTotal;
                                    }
                                    else
                                    {
                                        ct.DinhMuc += (decimal)(dmnl.DinhMuc - (dmnl.HeSo * tanDM)) * tanBQ * KMTotal * 0.0001M;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += ct.CongTacName + "=(" + dmnl.DinhMuc + "-" + dmnl.HeSo + "*" + tanDM + ")*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001";
                                    }

                                }
                            }
                            //Cộng thêm định mức dồn
                            if (sumDon > 0)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=KDON";
                                data += "&KhuDoan=";
                                var dmdon = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    decimal gioDon = (decimal)sumDon / 60;
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "KDON=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                            }
                            //Cộng thêm định mức dồn giải đá
                            //if (ct.CongTacID == 5 && ct.DonThuan > 0)
                            //{
                            //    data = "?NgayHL=" + rowCoBao.NgayCB;
                            //    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            //    data += "&CongTac=RDA";
                            //    data += "&KhuDoan=";
                            //    var dmtRDA = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                            //    if (dmtRDA != null)
                            //    {
                            //        decimal gioDonThuan = (decimal)ct.DonThuan / 60;
                            //        ct.DinhMuc += (decimal)dmtRDA.DinhMuc * gioDonThuan;
                            //        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                            //        ct.DienGiai += "Dồn rải đá=" + dmtRDA.DinhMuc + "*" + gioDonThuan.ToString("N2") + " " + dmtRDA.DonVi;
                            //    }
                            //}
                            //Cộng thêm định mức giữ hãm ga Sơn Yêu
                            if (ct.GaXPID == 3218 || ct.GaKTID == 3218)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=GHAM";
                                data += "&KhuDoan=3218";
                                var dmghSY = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmghSY != null)
                                {
                                    decimal gioDung = (decimal)ct.DungQD / 60;
                                    ct.DinhMuc += (decimal)dmghSY.DinhMuc * gioDung;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai +=  "Giữ hãm Sơn Yêu=" + dmghSY.DinhMuc + "*" + gioDung.ToString("N2") + " " + dmghSY.DonVi;
                                }
                            }
                            //Cộng thêm định mức giữ hãm ga Long Biên
                            if (ct.CongTacID <= 3 && ct.TinhChatID != 4 && (ct.GaXPID == 3012 || ct.GaKTID == 3012))
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=GHAM";
                                data += "&KhuDoan=3012";
                                var dmghLB = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                if (dmghLB != null)
                                {
                                    var cbGiuHamLB = listCoBaoCT.Where(x => x.GaID == 3012 && x.MacTauID == ct.MacTauID).FirstOrDefault();
                                    decimal gioGHLB = (int)(cbGiuHamLB.GioDi - cbGiuHamLB.GioDen).TotalMinutes;
                                    var cbGiuHamLBNext = listCoBaoCT.Where(x=> x.GioDen > cbGiuHamLB.GioDi).FirstOrDefault();
                                    if(cbGiuHamLBNext!=null && cbGiuHamLBNext.GaID== 3012)
                                    {
                                        gioGHLB = (int)(cbGiuHamLBNext.GioDi - cbGiuHamLB.GioDen).TotalMinutes;
                                    }    
                                    if (gioGHLB > 0)
                                    {
                                        gioGHLB = gioGHLB / 60;
                                        ct.DinhMuc += (decimal)dmghLB.DinhMuc * gioGHLB;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "Giữ hãm Long Biên=" + dmghLB.DinhMuc + "*" + gioGHLB.ToString("N2") + " " + dmghLB.DonVi;
                                    }
                                }
                            }
                            //Cộng thêm định mức thoi đẩy khách ga Long Biên-Gia Lâm
                            if (ct.CongTacID <= 3)
                            {
                                if ((ct.GaXPID == 3012 && ct.GaKTID == 2968) || (ct.GaXPID == 2968 && ct.GaKTID == 3012))
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&CongTac=TDAY";
                                    data += "&KhuDoan=";
                                    var dmtdLBGL = HttpHelper.Get<YVNLPDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetNLPDinhMucOBJ" + data).Result;
                                    if (dmtdLBGL != null)
                                    {
                                        ct.DinhMuc += (decimal)dmtdLBGL.DinhMuc;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "Thoi đẩy Long Biên-Gia Lâm=" + dmtdLBGL.DinhMuc + " " + dmtdLBGL.DonVi;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch
            { }
        }
        public static void YVNapDMDinhMuc(CoBao rowCoBao, decimal kmTotal, List<DoanThongDM> listdoanthongdm)
        {
            try
            {
                if (rowCoBao.DvcbID == "YV")
                {
                    foreach (DoanThongDM ct in listdoanthongdm)
                    {
                        decimal nlTieuThu = rowCoBao.NLThucNhan + rowCoBao.NLLinh - rowCoBao.NLBanSau;
                        string data = "?NgayHL=" + rowCoBao.NgayCB;
                        data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                        data += "&DauMoID=" + ct.LoaiDauMoID;                        
                        var dmList = HttpHelper.GetList<YVDMDinhMuc>(Configuration.UrlCBApi + "api/YenViens/YVGetDMDinhMuc" + data);
                        if (dmList.Count>0)
                        {
                            YVDMDinhMuc dm = dmList.FirstOrDefault();
                            if (dm.DonVi == "Kg/100Km")
                            {                                
                                ct.DinhMuc += (decimal)dm.DinhMuc * kmTotal*0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + kmTotal.ToString("N2") + "*0.01 " + dm.DonVi;
                            }
                            else if (dm.DonVi == "Kg/100Lít")
                            {
                                ct.DinhMuc += (decimal)dm.DinhMuc * nlTieuThu * 0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + nlTieuThu.ToString("N2") + "*0.01 " + dm.DonVi;
                            }    
                        }                       
                    }
                }
            }
            catch
            { }
        }
        #endregion

        #region HN_DinhMuc       
        public static void HNNapNLDinhMuc(CoBao rowCoBao, List<DoanThongCT> listdoanthongct)
        {
            try
            {
                if (rowCoBao.DvcbID == "HN")
                {
                    foreach (DoanThongCT ct in listdoanthongct)
                    {
                        string cacGa = string.Empty;
                        int sumDon = ct.DonXP + ct.DonDD + ct.DonKT;
                        //Nạp định mức
                        //Nếu là máy kiểm tra đường EM
                        if(rowCoBao.LoaiMayID=="EM")
                        {
                            decimal KMTotal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                            //nếu là chạy đơn
                            if (ct.TinhChatID == 2)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=CHAYDON";
                                data += "&KhuDoan=";
                                var dmchayDon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                                if (dmchayDon != null)
                                {                                   
                                    ct.DinhMuc += (decimal)dmchayDon.DinhMuc * KMTotal;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += dmchayDon.CongTac + "=" + dmchayDon.DinhMuc + "*" + KMTotal.ToString("N2") + " " + dmchayDon.DonVi;
                                }
                            }
                            else
                            {
                                
                                decimal TKMTotal = ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay;
                                decimal tanBQ = KMTotal > 0 ? TKMTotal / KMTotal : 0;
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&Tan=" + tanBQ;
                                data += "&CongTac=";
                                data += "&LoaiTau=" + ct.MacTauID;
                                var dmnl = HttpHelper.Get<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLDinhMucOBJ" + data).Result;
                                if (dmnl != null)
                                {
                                    ct.DinhMuc += (decimal)(dmnl.DinhMuc * KMTotal);
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += dmnl.LoaiTau + "=(" + dmnl.DinhMuc + "+" + dmnl.HeSo + "*"+ tanBQ .ToString("N2")+ ")*" + KMTotal.ToString("N2") + " " + dmnl.DonVi;
                                }
                            }
                        }    
                        //Nếu là chuyên dồn
                        else if (ct.CongTacID == 8 || ct.CongTacID == 9)
                        {

                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&CongTac=DON";
                            data += "&KhuDoan=" + ct.GaXPName;
                            var dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                decimal gioDon = (decimal)sumDon / 60;
                                ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += dmdon.CongTac + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }
                            else//Nếu không tìm được ga cụ thể thì gán cho dọc đường
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=DON";
                                data += "&KhuDoan=";
                                dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    decimal gioDon = (decimal)sumDon / 60;
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += dmdon.CongTac + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                            }
                        }                       
                        //Nếu là khách rỗng Hà Nội-Gia Lâm thì gán 15 lít/lượt
                        else if(ct.CongTacID<=3 && ct.Tan==ct.TanXeRong && ct.TinhChatID == 1 &&
                            ((ct.GaXPName=="Hà Nội" && ct.GaKTName=="Gia Lâm") || (ct.GaKTName == "Hà Nội" && ct.GaXPName == "Gia Lâm")))
                        {
                            ct.DinhMuc += 15;
                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                            ct.DienGiai += "Khách rỗng HN-GL = 15 lít/lượt";
                        }    
                        else
                        {
                            //Nạp định mức có tính chất khác ghép nguội và không phải là tác nghiệp bãi bỏ
                            if (ct.TinhChatID != 4 && !string.IsNullOrWhiteSpace(ct.MacTauID))
                            {
                                string CongTac = ct.CongTacID <= 3 ? "Khach" : ct.CongTacName;
                                decimal KMTotal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                                decimal TKMTotal = ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay;
                                //Lấy tấn bình quân theo cả đoàn tầu và khu đoạn
                                string khuDoanBoChieu = string.IsNullOrWhiteSpace(ct.KhuDoan) ? ct.KhuDoan : ct.KhuDoan.Substring(0, ct.KhuDoan.Length - 3);
                                decimal KMTotalBQ = listdoanthongct.Where(x=>x.MacTauID==ct.MacTauID && x.TinhChatID==ct.TinhChatID && x.KhuDoan.Contains(khuDoanBoChieu)).Sum(x=>x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                                decimal TKMTotalBQ = listdoanthongct.Where(x => x.MacTauID == ct.MacTauID && x.TinhChatID == ct.TinhChatID && x.KhuDoan.Contains(khuDoanBoChieu)).Sum(x => x.TKMChinh + x.TKMDon + x.TKMGhep + x.TKMDay);
                                decimal tanBQ = KMTotalBQ > 0 ? TKMTotalBQ / KMTotalBQ : 0;
                                if (ct.XeRongTotal >= 10 && ct.CongTacID > 3 && ct.TinhChatID != 3)
                                {
                                    //tanBQ += (ct.XeRongTotal - 10) * 4;
                                    tanBQ += ct.XeRongTotal * 4;
                                }
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&CongTac=" + CongTac;
                                decimal tanDM = decimal.Parse(HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/HaNois/HNGetTanMax" + data).Result);
                                if (tanDM > 0)
                                    tanDM = tanBQ > tanDM ? tanDM : tanBQ;
                                else
                                    tanDM = tanBQ;
                                tanDM = Math.Round(tanDM, 2);
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&Tan=" + tanDM;
                                data += "&CongTac=" + CongTac;
                                data += "&LoaiTau=" + ct.MacTauID;
                                var dmnl = HttpHelper.Get<HNNLDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLDinhMucOBJ" + data).Result;
                                if (dmnl != null)
                                {
                                    if (dmnl.DonVi == "Lít/Km")
                                    {
                                        ct.DinhMuc += (decimal)(dmnl.DinhMuc + (dmnl.HeSo * tanDM)) * KMTotal;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmnl.LoaiTau + "=(" + dmnl.DinhMuc + "+" + dmnl.HeSo + "*" + tanDM.ToString("N2") + ")*" + KMTotal.ToString("N2");
                                    }
                                    else
                                    {
                                        ct.DinhMuc += (decimal)(dmnl.DinhMuc - (dmnl.HeSo * tanDM)) * tanBQ * KMTotal * 0.0001M;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmnl.LoaiTau + "=(" + dmnl.DinhMuc + "-" + dmnl.HeSo + "*" + tanDM.ToString("N2") + ")*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001";
                                    }                                    
                                }
                                //Cộng thêm hệ số nếu
                                if (rowCoBao.LoaiMayID == "D19E")
                                {
                                    decimal _hsNL = 1M;
                                    int shDauMay = int.Parse(rowCoBao.DauMayID.Split('-')[1]);
                                    if (shDauMay >= 901 && shDauMay <= 910 && ct.CongTacID == 1)
                                        _hsNL = 1.01M;
                                    else if (shDauMay >= 921 && shDauMay <= 930)
                                        _hsNL = 0.99M;
                                    else if (shDauMay >= 941)
                                        _hsNL = 0.98M;
                                    ct.DinhMuc = ct.DinhMuc * _hsNL;
                                    ct.DienGiai += "*Hệ số NL:" + _hsNL;
                                }
                            }
                            //Cộng thêm định mức dồn
                            if (sumDon > 0)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&CongTac=DON";
                                data += "&KhuDoan=" + ct.GaXPName;
                                var dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    decimal gioDon = (decimal)sumDon / 60;
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += dmdon.CongTac + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                                else//Nếu không tìm được ga cụ thể thì gán cho dọc đường
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&CongTac=DON";
                                    data += "&KhuDoan=";
                                    dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                                    if (dmdon != null)
                                    {
                                        decimal gioDon = (decimal)sumDon / 60;
                                        ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmdon.CongTac + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                    }
                                }

                                //data += "&KhuDoan=";
                                //var dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                                //if (dmdon != null)
                                //{
                                //    decimal gioDon = (decimal)sumDon / 60;
                                //    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                //    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                //    ct.DienGiai += dmdon.CongTac + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                //}
                            }
                        }
                    }
                    //Cộng thêm nhiên liệu cho các trường hợp đặc biệt
                    int gioDonThem = 0;
                    //Nếu là các trường hợp chạy tầu khác nhỏ hơn 10 lít
                    var tongNLDM = listdoanthongct.Sum(x => x.DinhMuc);                  
                    //Nếu đầu máy ghép nguội
                    var dtGhepNguoi = listdoanthongct.Where(x => x.TinhChatID == 4).FirstOrDefault();
                    var tnBaiBo = listdoanthongct.Where(x => string.IsNullOrWhiteSpace(x.MacTauID)).FirstOrDefault();
                    if (dtGhepNguoi != null)
                    {
                        //Nếu chỉ có ghép nguội thì tính bù giờ dồn dọc đường
                        var dtNotGhepNguoi = listdoanthongct.Where(x => x.TinhChatID != 4).FirstOrDefault();
                        if (dtNotGhepNguoi==null)
                        {   
                                gioDonThem += 60;
                        }
                        else
                        {
                            //Nếu có kép nguội và có thêm dồn thì tính bù
                            var dtDonNotGhepNguoi = listdoanthongct.Where(x => x.TinhChatID != 4 && x.CongTacID != 8 && x.CongTacID != 9).FirstOrDefault();
                            if (dtDonNotGhepNguoi == null)
                            {
                                //Nếu có kép nguội và có thêm dồn thì tính bù
                                var dtDonGhepNguoi = listdoanthongct.Where(x => x.CongTacID == 8 || x.CongTacID == 9).FirstOrDefault();
                                if (dtDonGhepNguoi != null)
                                {
                                    gioDonThem += 60;
                                }
                            }
                            //Nếu có kép nguội và có tác nghiệp khác thì tính bù
                            else if (tongNLDM < 10)
                                gioDonThem += 60;
                        }
                    }
                    //Nếu là tác nghiệp bãi bỏ
                    else if (listdoanthongct.Count == 1 && tnBaiBo != null)
                    {
                        gioDonThem += 30;
                    }
                    //Nếu có kép nguội và có tác nghiệp khác thì tính bù
                    else if (tongNLDM < 10)
                        gioDonThem += 60;
                    
                    decimal nlThem = 0M;
                    //Nếu có bù giờ đồn
                    if(gioDonThem>0)
                    {
                        string data = "?NgayHL=" + rowCoBao.NgayCB;
                        data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                        data += "&CongTac=DON";
                        data += "&KhuDoan=";
                        var dmdon = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                        if (dmdon != null)
                        {
                            decimal gioDon = (decimal)gioDonThem / 60;
                            nlThem += (decimal)dmdon.DinhMuc * gioDon;                           
                        }
                    }
                    //Nếu đầu máy kéo đầu máy ghép nguội
                    var dtChinhGhepNguoi = listdoanthongct.Where(x => x.TinhChatID != 4 && x.MayGhepID.Contains("GH")).ToList();
                    if (dtChinhGhepNguoi.Count() > 0)
                    {
                        string[] mayGheps = dtChinhGhepNguoi.FirstOrDefault().MayGhepID.Split(',');
                        decimal kmChinhGN = dtChinhGhepNguoi.Sum(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                        if (kmChinhGN >= 50)
                        {
                            foreach (var mayGhepID in mayGheps)
                            {
                                if (mayGhepID.Contains("GH"))
                                {
                                    nlThem += mayGhepID.Contains("D10H") || mayGhepID.Contains("D11H") ? kmChinhGN * 0.12M : kmChinhGN * 0.1M;
                                }
                            }
                        }
                    }
                    //Nếu có dừng nổ máy
                    if (rowCoBao.DungNoMay>0)
                    {
                        var gaSonYeu = listdoanthongct.Where(x => x.GaXPName == "Sơn Yêu" || x.GaKTName == "Sơn Yêu").FirstOrDefault();
                        if (gaSonYeu != null)
                        {
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&CongTac=DUNGNM";
                            data += "&KhuDoan=Sơn Yêu";
                            var dmdungnm = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                            if (dmdungnm != null)
                            {
                                decimal gioDungNM = (decimal)rowCoBao.DungNoMay / 60;
                                nlThem += (decimal)dmdungnm.DinhMuc * gioDungNM;
                            }
                        }
                        else//Nếu không tìm được ga cụ thể thì gán cho dọc đường
                        {
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&CongTac=DUNGNM";
                            data += "&KhuDoan=";
                            var dmdungnm = HttpHelper.Get<HNNLPDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetNLPDinhMucOBJ" + data).Result;
                            if (dmdungnm != null)
                            {
                                decimal gioDungNM = (decimal)rowCoBao.DungNoMay / 60;
                                nlThem += (decimal)dmdungnm.DinhMuc * gioDungNM;
                            }
                        }
                    }
                    //Phân bổ nhiên liệu theo nhiên liệu tiêu thụ
                    decimal sumTieuThu = listdoanthongct.Sum(x => x.TieuThu);
                    if (nlThem > 0)
                    {
                        int stt = 1;
                        decimal dinhMucRunning = 0;
                        foreach (var ct in listdoanthongct)
                        {
                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                            if (stt == listdoanthongct.Count)
                            {
                                dinhMucRunning = nlThem - dinhMucRunning;
                                ct.DinhMuc += dinhMucRunning;
                                if(dinhMucRunning>0)
                                    ct.DienGiai += "Cộng bù ĐM=" + dinhMucRunning.ToString("N2") + " Lít";
                            }
                            else
                            {
                                decimal nlCongBu= nlThem * ct.TieuThu / sumTieuThu;
                                ct.DinhMuc += nlCongBu;
                                dinhMucRunning += nlCongBu;
                                if (nlCongBu > 0)
                                    ct.DienGiai += "Cộng bù ĐM=" + nlCongBu.ToString("N2") + " Lít";
                            } 
                            stt++;
                        }
                    }
                }
            }
            catch
            { }
        }
        public static void HNNapDMDinhMuc(CoBao rowCoBao, decimal kmTotal, List<DoanThongDM> listdoanthongdm)
        {
            try
            {
                if (rowCoBao.DvcbID == "HN")
                {
                    foreach (DoanThongDM ct in listdoanthongdm)
                    {
                        decimal nlTieuThu = rowCoBao.NLThucNhan + rowCoBao.NLLinh - rowCoBao.NLBanSau;
                        string data = "?NgayHL=" + rowCoBao.NgayCB;
                        data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                        data += "&DauMoID=" + ct.LoaiDauMoID;
                        var dmList = HttpHelper.GetList<HNDMDinhMuc>(Configuration.UrlCBApi + "api/HaNois/HNGetDMDinhMuc" + data);
                        if (dmList.Count > 0)
                        {
                            HNDMDinhMuc dm = dmList.FirstOrDefault();
                            if (dm.DonVi == "Kg/100Km")
                            {
                                ct.DinhMuc += (decimal)dm.DinhMuc * kmTotal * 0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + kmTotal.ToString("N2") + "*0.01 " + dm.DonVi;
                            }
                            else if (dm.DonVi == "Kg/100Lít")
                            {
                                ct.DinhMuc += (decimal)dm.DinhMuc * nlTieuThu * 0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + nlTieuThu.ToString("N2") + "*0.01 " + dm.DonVi;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        #endregion

        #region VI_DinhMuc        
        public static void VINapNLDinhMuc(CoBao rowCoBao, List<CoBaoCT> listcobaoct, List<DoanThongCT> listdoanthongct)
        {
            try
            {
                if (rowCoBao.DvcbID == "VIN")
                {
                    string[] strArrayMTU = { "D13E-716","D13E-720", "D13E-722", "D13E-725" };
                    string loaiMay = strArrayMTU.Contains(rowCoBao.DauMayID) ? rowCoBao.LoaiMayID + "-MTU" : rowCoBao.LoaiMayID;
                    decimal kmChay = 0;
                    foreach (DoanThongCT ct in listdoanthongct)
                    {
                        string cacGa = string.Empty;
                        string mayDay = string.Empty;                        
                        decimal tanDT = listcobaoct.Where(x => x.MacTauID.Contains(ct.MacTauID) && x.GaID == ct.GaXPID).FirstOrDefault().Tan;
                        if (!string.IsNullOrWhiteSpace(ct.MayGhepID) && ct.CongTacID != 8 && ct.CongTacID != 9)
                        {
                            if (ct.TinhChatID == 5)
                                mayDay = ct.MayGhepID.Split('-')[0];
                            else
                            {
                                string[] mayGheps = ct.MayGhepID.Split(',');
                                foreach (var mayGhep in mayGheps)
                                {
                                    string[] dauMays = mayGhep.Split('-');
                                    if (ct.TinhChatID == 1 && dauMays[2] == "DN")
                                    {
                                        mayDay = dauMays[0];
                                        break;
                                    }
                                }
                            }
                            //if (rowCoBao.DauMayID == "D13E-725") mayDay += "-MTU";
                        }
                        int sumDon = ct.DonXP + ct.DonDD + ct.DonKT;
                        decimal KMToTal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;                        
                        //1.Nạp khu đoạn
                        //Nếu là chuyên dồn
                        if (ct.CongTacID == 8 || ct.CongTacID == 9)
                        {                           
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + loaiMay;
                            data += "&KhuDoan=" + ct.GaXPName;
                            data += "&MacTau=" + ct.KhuDoan;
                            decimal gioDon = (decimal)sumDon / 60;
                            var dmdon = HttpHelper.Get<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                ct.DinhMuc += (decimal)dmdon.DinhMucDon * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMucDon + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }                           
                        }
                        //Nếu là ghép nóng
                        else if (ct.TinhChatID == 3)
                        {
                            string khuDoan = ct.KhuDoan;
                            if (ct.KhuDoan == "LC-KL") khuDoan = "HUE-DN";
                            else if (ct.KhuDoan == "PT-TA" || ct.KhuDoan == "TA-KL" || ct.KhuDoan == "KL-NL") khuDoan = "VI-DH";
                            else if (ct.KhuDoan == "PL-NB" || ct.KhuDoan == "NB-BS") khuDoan = "HN-TH";
                            else if (ct.KhuDoan == "TL-VI") khuDoan = "TH-VI";

                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + loaiMay;
                            data += "&KhuDoan=" + khuDoan;
                            data += "&MacTau=Ghép nóng";
                            var dmGN = HttpHelper.Get<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMucOBJ" + data).Result;
                            if (dmGN != null)
                            {
                                ct.DinhMuc += (decimal)(dmGN.DinhMuc * KMToTal * 0.01M);
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += "Ghép nóng=" + dmGN.DinhMuc + "*" + KMToTal + "*0.01 " + dmGN.DonVi;
                            }
                        }
                        //Nếu là ghép nguội
                        else if(ct.TinhChatID==4)
                        {
                            string khuDoan = ct.KhuDoan;
                            if (ct.KhuDoan == "LC-KL") khuDoan = "HUE-DN";
                            else if (ct.KhuDoan == "PT-TA" || ct.KhuDoan == "TA-KL" || ct.KhuDoan == "KL-NL") khuDoan = "VI-DH";
                            else if (ct.KhuDoan == "PL-NB" || ct.KhuDoan == "NB-BS") khuDoan = "HN-TH";
                            else if (ct.KhuDoan == "TL-VI") khuDoan = "TH-VI";

                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + loaiMay;
                            data += "&KhuDoan=" + khuDoan;
                            data += "&MacTau=Ghép nguội";                            
                            var dmGN = HttpHelper.Get<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMucOBJ" + data).Result;
                            if (dmGN != null)
                            {
                                ct.DinhMuc += (decimal)dmGN.DinhMuc;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += "Ghép nguội=" + dmGN.DinhMuc + " " + dmGN.DonVi;
                            }
                        }    
                        //Nếu là máy đẩy
                        else if (!string.IsNullOrWhiteSpace(mayDay))
                        {  
                            string mayChinh = string.Empty;
                            string mayPhu = string.Empty;
                            string data = string.Empty;                            
                            if (ct.TinhChatID == 5)
                            {
                                mayChinh = mayDay;
                                mayPhu = rowCoBao.LoaiMayID;
                            }
                            else
                            {
                                mayChinh= rowCoBao.LoaiMayID;
                                mayPhu = mayDay;
                            }    
                            //Nạp đinh mức                            
                            data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&MayChinh=" + mayChinh;
                            data += "&MayPhu=" + mayPhu;
                            data += "&KhuDoan=" + ct.KhuDoan;
                            var dmNL = HttpHelper.Get<VINLDDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDDinhMucOBJ" + data).Result;
                            if (dmNL != null)
                            {                               
                                //decimal kmNL = (decimal)dmNL.Km < KMTotal ? (decimal)dmNL.Km : KMTotal;
                                decimal kmNL = (decimal)dmNL.Km;
                                if (ct.TinhChatID == 5)
                                {
                                    ct.DinhMuc += (decimal)(dmNL.MayPhuDM * (dmNL.MayPhuTL * tanDT * 0.01M) * kmNL * 0.0001M);                                   
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "Phụ đẩy: " + dmNL.KhuDoan + "=" + dmNL.MayPhuDM + "*(" + dmNL.MayPhuTL + "*" +
                                        tanDT.ToString("N2") + "*0.01)*" + kmNL + "*0.0001 " + dmNL.DonVi;                                    
                                }
                                else
                                {
                                    string khuDoan = ct.KhuDoan;
                                    if (ct.KhuDoan == "LC-KL") khuDoan = "HUE-DN";
                                    else if (ct.KhuDoan == "PT-TA" || ct.KhuDoan == "TA-KL" || ct.KhuDoan == "KL-NL") khuDoan = "VI-DH";
                                    else if (ct.KhuDoan == "PL-NB" || ct.KhuDoan == "NB-BS") khuDoan = "HN-TH";
                                    else if (ct.KhuDoan == "TL-VI") khuDoan = "TH-VI";
                                    NapDinhMucMayChinh(rowCoBao,khuDoan, ct, sumDon, kmNL, (decimal)dmNL.MayChinhTL * 0.01M, tanDT);
                                    ct.DienGiai = "Đẩy chính-" + ct.DienGiai;
                                    kmChay = KMToTal - kmNL;
                                    if (kmChay > 0)
                                    {
                                        NapDinhMucMayChinh(rowCoBao,khuDoan, ct, sumDon, kmChay, 1, tanDT);
                                    }    
                                }
                                
                            }
                        }    
                        else
                        {
                            string khuDoan = ct.KhuDoan;
                            if (ct.KhuDoan == "LC-KL") khuDoan = "HUE-DN";
                            else if(ct.KhuDoan == "PT-TA"|| ct.KhuDoan == "TA-KL" || ct.KhuDoan == "KL-NL") khuDoan = "VI-DH";
                            else if (ct.KhuDoan == "PL-NB" || ct.KhuDoan == "NB-BS") khuDoan = "HN-TH";
                            else if (ct.KhuDoan == "TL-VI") khuDoan = "TH-VI";
                            //Chỗ này trừ bớt km đoạn còn lại vì đã tính cho đoạn trước rồi
                            if (kmChay < 0)
                            {
                                KMToTal = KMToTal + kmChay;
                                kmChay = 0;
                            }
                            NapDinhMucMayChinh(rowCoBao, khuDoan, ct, sumDon,KMToTal,1, ct.Tan);
                        }
                        //Xét trường hợp trước và sau đẩy nếu mà chạy đơn không tấn số hoặc ghép nguội thì không được ăn định mức
                        if (ct.TinhChatID == 5)
                        {
                            var truocDay = listdoanthongct.Where(x => x.GaKTID == ct.GaXPID && x.NgayXP <= ct.NgayXP && x.CongTacID!=8 && x.CongTacID!=9 && (x.TinhChatID == 4 || x.Tan == 0)).FirstOrDefault();
                            if (truocDay != null)
                            {
                                truocDay.DinhMuc = 0;
                                truocDay.DienGiai = "Không được tính định mức trước đẩy.";
                            }
                            var sauDay = listdoanthongct.Where(x => x.GaXPID == ct.GaKTID && x.NgayXP >= ct.NgayXP && x.CongTacID != 8 && x.CongTacID != 9 && (x.TinhChatID == 4 || x.Tan == 0)).FirstOrDefault();
                            if (sauDay != null)
                            {
                                sauDay.DinhMuc = 0;
                                sauDay.DienGiai = "Không được tính định mức sau đẩy.";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            { }
        }

        private static void NapDinhMucMayChinh(CoBao rowCoBao,string khuDoan, DoanThongCT ct, int sumDon,decimal kmDM,decimal tyleDay,decimal tanDT)
        {
            string[] strArrayMTU = { "D13E-716","D13E-720", "D13E-722", "D13E-725" };
            string loaiMay = strArrayMTU.Contains(rowCoBao.DauMayID) ? rowCoBao.LoaiMayID + "-MTU" : rowCoBao.LoaiMayID;
            //Nạp định mức
            decimal tanBQ = kmDM > 0 ? tanDT : 0;
            //Lấy hệ số tấn.
            string data = "?NgayHL=" + rowCoBao.NgayCB;
            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
            data += "&Tan=" + tanBQ;
            var hsTan = HttpHelper.Get<VIHSTan>(Configuration.UrlCBApi + "api/Vinhs/VIGetHSTanOBJ" + data).Result;
            tanBQ = hsTan != null ? (decimal)(tanBQ * hsTan.HeSo) : tanBQ;
            //Nếu có lớn hơn 4 xe rỗng thì lấy 25% tấn xe rỗng
            if (ct.XeRongTotal > 4)
            {
                tanBQ += ct.TanXeRong / 4;
            }
            if(tyleDay<1)
            {
                tanBQ = tanBQ * tyleDay;
            }    
            //Nạp đinh mức                            
            data = "?NgayHL=" + rowCoBao.NgayCB;
            data += "&LoaiMay=" + loaiMay;
            data += "&KhuDoan=" + khuDoan;
            data += "&MacTau=" + ct.MacTauID;
            var dmNL = HttpHelper.Get<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMucOBJ" + data).Result;
            if (dmNL != null && hsTan != null)
            {
                ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * kmDM * 0.0001M);
                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + kmDM + "*0.0001 " + dmNL.DonVi;
                if (dmNL.TyLeDon > 0 && dmNL.TyLeDon != 1)
                {
                    ct.DinhMuc += (decimal)(dmNL.DinhMucDon * dmNL.TyLeDon * kmDM * 0.01M);
                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                    ct.DienGiai += "Bù ĐM dồn=" + dmNL.DinhMucDon + "*" + dmNL.TyLeDon + "*" + kmDM + "*0.01 " + dmNL.DonViDon;
                }
                else if (sumDon > 0)
                {
                    decimal gioDon = (decimal)sumDon / 60;
                    ct.DinhMuc += (decimal)(dmNL.DinhMucDon * gioDon);
                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                    ct.DienGiai += "ĐM dồn=" + dmNL.DinhMucDon + "*" + gioDon.ToString("N2") + " " + dmNL.DonViDon;
                }

            }
            else//Nếu không có giá trị lấy trường hợp chạy đơn ở các noi khác
            {
                string loaiTau = ct.CongTacID <= 3 ? "Khách" : ct.CongTacName;
                if (tanBQ > 0 & hsTan == null) loaiTau = "Máy đơn";
                if (ct.TinhChatID == 2) loaiTau = ct.TinhChatName;
                if (ct.TinhChatID == 3) loaiTau = ct.TinhChatName;
                data = "?NgayHL=" + rowCoBao.NgayCB;
                data += "&LoaiMay=" + loaiMay;
                data += "&KhuDoan=" + khuDoan;
                data += "&MacTau=" + loaiTau;
                dmNL = HttpHelper.Get<VINLDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetNLDinhMucOBJ" + data).Result;
                if (dmNL != null)
                {
                    //Máy chạy đơn
                    if (dmNL.DonVi == "Lít/100Km")
                    {
                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * kmDM * 0.01M);
                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + kmDM + "*0.01 " + dmNL.DonVi;
                    }
                    else
                    {
                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * kmDM * 0.0001M);
                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + kmDM + "*0.0001 " + dmNL.DonVi;
                    }
                    if (dmNL.TyLeDon > 0 && dmNL.TyLeDon != 1)
                    {
                        ct.DinhMuc += (decimal)(dmNL.DinhMucDon * dmNL.TyLeDon * kmDM * 0.01M);
                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                        ct.DienGiai += "Bù ĐM dồn=" + dmNL.DinhMucDon + "*" + dmNL.TyLeDon + "*" + kmDM + "*0.01 " + dmNL.DonViDon;
                    }
                    else if (sumDon > 0)
                    {
                        decimal gioDon = (decimal)sumDon / 60;
                        ct.DinhMuc += (decimal)(dmNL.DinhMucDon * gioDon);
                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                        ct.DienGiai += "ĐM dồn=" + dmNL.DinhMucDon + "*" + gioDon.ToString("N2") + " " + dmNL.DonViDon;
                    }
                }
            }
        }

        public static void VINapDMDinhMuc(CoBao rowCoBao, decimal kmTotal, List<DoanThongDM> listdoanthongdm)
        {
            try
            {
                if (rowCoBao.DvcbID == "VIN")
                {
                    //string loaiMay = rowCoBao.DauMayID == "D13E-725" ? rowCoBao.LoaiMayID + "-MTU" : rowCoBao.LoaiMayID;
                    foreach (DoanThongDM ct in listdoanthongdm)
                    {
                        decimal nlTieuThu = rowCoBao.NLThucNhan + rowCoBao.NLLinh - rowCoBao.NLBanSau;
                        string data = "?NgayHL=" + rowCoBao.NgayCB;
                        data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                        data += "&DauMoID=" + ct.LoaiDauMoID;
                        var dmList = HttpHelper.GetList<VIDMDinhMuc>(Configuration.UrlCBApi + "api/Vinhs/VIGetDMDinhMuc" + data);
                        if (dmList.Count > 0)
                        {
                            VIDMDinhMuc dm = dmList.FirstOrDefault();
                            if (dm.DonVi == "Kg/100Km")
                            {
                                ct.DinhMuc += (decimal)dm.DinhMuc * kmTotal * 0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + kmTotal.ToString("N2") + "*0.01 " + dm.DonVi;
                            }
                            else if (dm.DonVi == "Kg/100Lít")
                            {
                                ct.DinhMuc += (decimal)dm.DinhMuc * nlTieuThu * 0.01M;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.LoaiDauMoName + "=" + dm.DinhMuc + "*" + nlTieuThu.ToString("N2") + "*0.01 " + dm.DonVi;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        #endregion

        #region DN_DinhMuc        
        public static void DNNapNLDinhMuc(CoBao rowCoBao, List<DoanThongCT> listdoanthongct)
        {
            try
            {
                if (rowCoBao.DvcbID == "DN")
                {
                    foreach (DoanThongCT ct in listdoanthongct)
                    {
                        string cacGa = string.Empty;
                        int sumDon = ct.DonXP + ct.DonDD + ct.DonKT;
                        //1.Nạp khu đoạn
                        //Nếu là chuyên dồn
                        if (ct.CongTacID == 8 || ct.CongTacID == 9)
                        {                           
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&KhuDoan=" + ct.GaXPName;
                            data += "&Tan=0";
                            data += "&LoaiTau=" + ct.KhuDoan;
                            decimal gioDon = (decimal)sumDon / 60;
                            var dmdon = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }
                            //Nếu không phải ga chuyên dồn
                            else
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=Dọc Đường";
                                data += "&Tan=0";
                                data += "&LoaiTau=" + ct.KhuDoan;
                                dmdon = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                            }
                        }
                        else
                        {
                            //Nạp định mức                           
                            decimal KMTotal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                            decimal TKMTotal = ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay;
                            decimal tanBQ = KMTotal > 0 ? TKMTotal / KMTotal : 0;
                            //Nạp danh sách các ga
                            string CacGa = ct.GaXPName;
                            bool isFisrt = true;
                            string data = "?GaXP=" + ct.GaXPID + "&GaKT=" + ct.GaKTID+"&Tuyen=";
                            var dm = HttpHelper.Get<DmLyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetDMLyTrinh" + data).Result;
                            if (dm != null)
                            {
                                data = "?TuyenID=" + dm.TuyenId + "&TenGa=";
                                var listLyTrinh = HttpHelper.GetList<LyTrinh>(Configuration.UrlCBApi + "api/LyTrinhs/GetByTraTim" + data);
                                if (dm.Chieu == "di")
                                    listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDiKM && x.Km <= dm.GaDenKM).OrderBy(x => x.Km).ToList();
                                else
                                    listLyTrinh = listLyTrinh.Where(x => x.Km >= dm.GaDenKM && x.Km <= dm.GaDiKM).OrderByDescending(x => x.Km).ToList();
                                isFisrt = true;
                                foreach (LyTrinh lt in listLyTrinh)
                                {
                                    if (isFisrt == false) CacGa += "," + lt.TenGa.ToString();
                                    isFisrt = false;
                                }
                            }
                            //Nạp đinh mức                            
                            if (CacGa.Contains("Hải Vân"))//Nếu có ga Hải Vân
                            {
                                if (ct.TinhChatID == 5 || ct.TinhChatID == 6)//Nếu là ghép nóng, đẩy nóng
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=Hải Vân";
                                    data += "&Tan=" + ct.Tan;
                                    data += "&LoaiTau=" + ct.TinhChatName;
                                    var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL != null)
                                    {
                                        ct.DinhMuc += (decimal)dmNL.DinhMuc * 10 * ct.Tan * 0.0001M;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += ct.TinhChatName + "=" + dmNL.DinhMuc + "*10*" + ct.Tan.ToString("N2") + " " + dmNL.DonVi;
                                    }
                                }                               
                                else if (ct.TinhChatID == 4)//Nếu là ghép nguội
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=Hải Vân";
                                    data += "&Tan=" + tanBQ;
                                    data += "&LoaiTau=Ghép nguội";
                                    var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL != null)
                                    {
                                        ct.DinhMuc += (decimal)dmNL.DinhMuc;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += ct.TinhChatName + "=" + dmNL.DinhMuc + " " + dmNL.DonVi;
                                    }
                                }
                                else if (!string.IsNullOrWhiteSpace(ct.MayGhepID))
                                {
                                    string[] mayGheps = ct.MayGhepID.Split(',');
                                    foreach (var mayGhep in mayGheps)
                                    {
                                        string[] dauMays = mayGhep.Split('-');
                                        if (ct.TinhChatID == 1 && dauMays[2] == "DN")
                                        {
                                            data = "?NgayHL=" + rowCoBao.NgayCB;
                                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                            data += "&KhuDoan=Hải Vân";
                                            data += "&Tan=" + tanBQ;
                                            data += "&LoaiTau=Đẩy nóng";
                                            var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                            if (dmNL != null)
                                            {
                                                ct.DinhMuc += (decimal)dmNL.DinhMuc * KMTotal * tanBQ * 0.0001M;
                                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                                ct.DienGiai += ct.TinhChatName + "=" + dmNL.DinhMuc + "*"+KMTotal.ToString("N2")+"*" + tanBQ.ToString("N2") + " " + dmNL.DonVi;
                                            }
                                        }
                                        else//Các trường hợp khác
                                        {
                                            data = "?NgayHL=" + rowCoBao.NgayCB;
                                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                            data += "&KhuDoan=" + ct.KhuDoan;
                                            data += "&Tan=" + tanBQ;
                                            data += "&LoaiTau=" + ct.MacTauID;
                                            var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                            if (dmNL != null)
                                            {
                                                ct.DinhMuc += (decimal)dmNL.DinhMuc * KMTotal * tanBQ * 0.0001M;
                                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                                ct.DienGiai += ct.MacTauID + "=" + dmNL.DinhMuc + "*" + KMTotal.ToString("N2") + "*" + tanBQ.ToString("N2") + "*0.0001 " + dmNL.DonVi;
                                            }
                                        }
                                    }
                                }
                                else if (ct.CongTacID == 6)//Nếu là Tầu thoi
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=Hải Vân";
                                    data += "&Tan=" + tanBQ;
                                    data += "&LoaiTau=" + ct.CongTacName;
                                    var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL != null)
                                    {
                                        if (dmNL.HeSo != 0)//Nếu có hệ số
                                        {
                                            ct.DinhMuc += (decimal)(dmNL.DinhMuc + dmNL.HeSo * tanBQ) * KMTotal * tanBQ * 0.0001M;
                                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                            ct.DienGiai += ct.CongTacName + "=(" + dmNL.DinhMuc + "+" + dmNL.HeSo.ToString() + "*" + tanBQ.ToString("N2") + ")*" +
                                                KMTotal.ToString("N2") + "*" + tanBQ.ToString("N2") + "*0.0001" + " " + dmNL.DonVi;
                                        }
                                        else
                                        {
                                            ct.DinhMuc += (decimal)dmNL.DinhMuc * KMTotal * tanBQ * 0.0001M;
                                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                            ct.DienGiai += ct.CongTacName + "=" + dmNL.DinhMuc + "*" + KMTotal.ToString("N2") + "*" + tanBQ.ToString("N2") + "*0.0001 " + dmNL.DonVi;
                                        }
                                    }
                                }
                                else//Các trường hợp khác
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=" + ct.KhuDoan;
                                    data += "&Tan=" + tanBQ;
                                    data += "&LoaiTau=" + ct.MacTauID;
                                    var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL != null)
                                    {
                                        ct.DinhMuc += (decimal)dmNL.DinhMuc * KMTotal * tanBQ * 0.0001M;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += ct.MacTauID + "=" + dmNL.DinhMuc + "*" + KMTotal.ToString("N2") + "*" + tanBQ.ToString("N2") + "*0.0001 " + dmNL.DonVi;
                                    }
                                }
                            }                           
                            else//Các trường hợp khác
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan="+ct.KhuDoan;
                                data += "&Tan=" + tanBQ;
                                data += "&LoaiTau=" + ct.MacTauID;
                                var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    ct.DinhMuc += (decimal)dmNL.DinhMuc * KMTotal * tanBQ * 0.0001M;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.MacTauID + "=" + dmNL.DinhMuc + "*" + KMTotal.ToString("N2") + "*" + tanBQ.ToString("N2") + "*0.0001 " + dmNL.DonVi;
                                }
                            }
                            //Nếu máy chạy đơn hoặc có tấn số nhỏ hơn bằng 150;
                            if ((ct.TinhChatID == 2 || tanBQ <= 150) && ct.TinhChatID!=4)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&Tan=" + tanBQ;
                                data += "&LoaiTau=Máy đơn";
                                var dmNL = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    decimal hsLoaiMay = rowCoBao.LoaiMayID == "D10H" || rowCoBao.LoaiMayID == "D12E" ? 0.0012M : 0.002M;
                                    ct.DinhMuc += (decimal)((dmNL.DinhMuc + hsLoaiMay * tanBQ) * dmNL.HeSo * KMTotal);
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "Máy đơn=(" + dmNL.DinhMuc + "+" + hsLoaiMay + "*" + tanBQ.ToString("N2") + ")*"
                                        + dmNL.HeSo + "*" + KMTotal.ToString("N2") + " " + dmNL.DonVi;
                                }
                            }

                            //Cộng thêm định mức dồn
                            if (sumDon > 0)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.GaXPName;
                                data += "&Tan=0";
                                data += "&LoaiTau=KDON";
                                decimal gioDon = (decimal)sumDon / 60;
                                var dmdon = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                if (dmdon != null)//Dồn các ga lập tầu
                                {
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "KDON=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                                else//Các ga dọc đường
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=Dọc Đường";
                                    data += "&Tan=0";
                                    data += "&LoaiTau=KDON";
                                    gioDon = (decimal)sumDon / 60;
                                    dmdon = HttpHelper.Get<DNNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                    if (dmdon != null)
                                    {
                                        ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "KDON=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                    }
                                }    
                            }
                            //Cộng thêm định mức dừng nổ máy
                            if (ct.DungNM > 0)
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=Nổ Máy";
                                data += "&Tan=0";
                                data += "&LoaiTau=Dừng";
                                var dmDung = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/DaNangs/DNGetNLDinhMucOBJ" + data).Result;
                                if (dmDung != null)
                                {
                                    {
                                        decimal gioDung = (decimal)ct.DungNM / 60;
                                        ct.DinhMuc += (decimal)dmDung.DinhMuc * gioDung;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "Dừng NM=" + dmDung.DinhMuc + "*" + gioDung.ToString("N2") + " " + dmDung.DonVi;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            { }
        }

        #endregion

        #region SG_DinhMuc        
        public static void SGNapNLDinhMuc(CoBao rowCoBao, List<CoBaoCT> listcobaoct, List<DoanThongCT> listdoanthongct)
        {
            try
            {
                if (rowCoBao.DvcbID == "SG")
                {
                    List<SGKhuDoan> listSGKhuDoan = HttpHelper.GetList<SGKhuDoan>(Configuration.UrlCBApi + "api/SaiGons/SGGetKhuDoan?KhuDoan=")
                  .OrderBy(x => x.KhuDoanID).ToList();
                    foreach (DoanThongCT ct in listdoanthongct)
                    {
                        string cacGa = string.Empty;
                        int sumDon = ct.DonXP + ct.DonDD + ct.DonKT;
                        //1.Nạp khu đoạn
                        //Nếu là chuyên dồn
                        if (ct.CongTacID == 8 || ct.CongTacID == 9)
                        {
                            //ct.KhuDoan = ct.CongTacID == 8 ? "CDON" : "KDON";
                            string data = "?NgayHL=" + rowCoBao.NgayCB;
                            data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                            data += "&KhuDoan=" + ct.GaXPName;
                            data += "&MacTau=" + ct.KhuDoan;
                            decimal gioDon = (decimal)sumDon / 60;
                            var dmdon = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                            if (dmdon != null)
                            {
                                ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                            }
                            //Nếu không phải ga chuyên dồn
                            else
                            {
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=Các nơi khác";
                                data += "&MacTau=" + ct.KhuDoan;
                                dmdon = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmdon != null)
                                {
                                    ct.DinhMuc += (decimal)dmdon.DinhMuc * gioDon;
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += ct.KhuDoan + "=" + dmdon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmdon.DonVi;
                                }
                            }
                        }
                        //Các trường hợp khác
                        else
                        {
                            string[] strArrayLC_KL = { "Lăng Cô", "Hải Vân Bắc", "Hải Vân", "Hải Vân Nam", "Kim Liên" };
                            string[] strArrayKL_DN = { "Kim Liên", "Thanh Khê", "Đà Nẵng" };
                            string[] strArrayLC_DN = { "Lăng Cô", "Hải Vân Bắc", "Hải Vân", "Hải Vân Nam", "Kim Liên", "Thanh Khê", "Đà Nẵng" };
                            //Nạp định mức                           
                            decimal KMTotal = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                            decimal TKMTotal = ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay;
                            decimal tanBQ = KMTotal > 0 ? TKMTotal / KMTotal : 0;
                            //Nếu là ghép nguội                           
                            if (ct.TinhChatID == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(ct.KhuDoan))
                                {
                                    if (ct.KhuDoan.Contains("-"))
                                    {
                                        string _khuDoan = ct.KhuDoan.Split('-')[0] + "-" + ct.KhuDoan.Split('-')[1];
                                        var khuDoanOBJ = listSGKhuDoan.Where(x => x.KhuDoanID.Contains(_khuDoan)).FirstOrDefault();
                                        if (khuDoanOBJ != null)
                                        {
                                            decimal sumKM = ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay;
                                            if (khuDoanOBJ.Km / 2 > sumKM)
                                            {
                                                ct.DinhMuc += 5;
                                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                                ct.DienGiai += "Ghép nguội nhỏ hơn 1/2 khu đoạn=5 Lít";
                                            }
                                            else
                                            {
                                                ct.DinhMuc += 15;
                                                if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                                ct.DienGiai += "Ghép nguội lớn hơn 1/2 khu đoạn=15 Lít";
                                            }
                                        }
                                    }
                                }
                            }
                            else if (ct.TinhChatID == 2 && strArrayKL_DN.Contains(ct.GaXPName) && strArrayKL_DN.Contains(ct.GaKTName))
                            {
                                decimal tanDT = listcobaoct.Where(x => x.MacTauID.Contains(ct.MacTauID) && x.GaID == ct.GaXPID).FirstOrDefault().Tan;
                                //Lấy hệ số tấn.
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&Tan=" + tanDT;
                                data += "&DonVi=";
                                var hsTan = HttpHelper.Get<SGHSTan>(Configuration.UrlCBApi + "api/SaiGons/SGGetHSTanOBJ" + data).Result;
                                decimal heSo = hsTan != null ? (decimal)hsTan.HeSo : 1;
                                //Nạp đinh mức                                    
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&MacTau=Chạy đơn";
                                var dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    if (dmNL.DonVi == "Lít/100Km")
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * heSo * KMTotal * 0.01M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + heSo + "*" + KMTotal + "*0.01 " + dmNL.DonVi;
                                    }
                                    else
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * KMTotal * 0.0001M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001 " + dmNL.DonVi;
                                    }
                                }
                            }
                            else if (ct.TinhChatID == 2 && (strArrayLC_KL.Contains(ct.GaXPName) || strArrayLC_KL.Contains(ct.GaKTName)))
                            {
                                decimal kmLCKL = KMTotal > 21.47M ? 21.47M : KMTotal;
                                decimal kmCL = KMTotal > 21.47M ? KMTotal - 21.47M : 0;
                                decimal tanDT = listcobaoct.Where(x => x.MacTauID.Contains(ct.MacTauID) && x.GaID == ct.GaXPID).FirstOrDefault().Tan;
                                //Lấy hệ số tấn.
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&Tan=" + tanDT;
                                data += "&DonVi=";
                                var hsTan = HttpHelper.Get<SGHSTan>(Configuration.UrlCBApi + "api/SaiGons/SGGetHSTanOBJ" + data).Result;
                                decimal heSo = hsTan != null ? (decimal)hsTan.HeSo : 1;

                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&MacTau=Đơn LC-KL";
                                var dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    ct.DinhMuc += (decimal)(dmNL.DinhMuc * heSo * kmLCKL * 0.01M);
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "Đơn LC-KL=" + dmNL.DinhMuc + "*" + heSo + "*" + kmLCKL + "*0.01 Lít/100Km";
                                }
                                if (kmCL > 0)
                                {
                                    //Nạp đinh mức                                    
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=" + ct.KhuDoan;
                                    data += "&MacTau=Chạy đơn";
                                    dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL != null)
                                    {
                                        if (dmNL.DonVi == "Lít/100Km")
                                        {
                                            ct.DinhMuc += (decimal)(dmNL.DinhMuc * heSo * kmCL * 0.01M);
                                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                            ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + heSo + "*" + kmCL + "*0.01 " + dmNL.DonVi;
                                        }
                                        else
                                        {
                                            ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * kmCL * 0.0001M);
                                            if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                            ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + kmCL + "*0.0001 " + dmNL.DonVi;
                                        }
                                    }
                                }
                            }
                            else if (ct.TinhChatID == 5 && strArrayLC_DN.Contains(ct.GaXPName) && strArrayLC_DN.Contains(ct.GaKTName))
                            {
                                decimal tanDT = listcobaoct.Where(x => x.MacTauID.Contains(ct.MacTauID) && x.GaID == ct.GaXPID).FirstOrDefault().Tan;
                                //Lấy hệ số tấn.
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&Tan=" + tanDT;
                                data += "&DonVi=";
                                var hsTan = HttpHelper.Get<SGHSTan>(Configuration.UrlCBApi + "api/SaiGons/SGGetHSTanOBJ" + data).Result;
                                tanBQ = hsTan != null ? (decimal)(tanDT * hsTan.HeSo) : tanDT;
                                string cachTinh = hsTan != null ? hsTan.DonVi : "P";
                                decimal heSo = hsTan != null ? (decimal)hsTan.HeSo : 1;
                                if (ct.XeRongTotal >= 5 && ct.CongTacID >= 4)
                                    tanBQ += ct.TanXeRong * 25 / 100;

                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&MacTau=Đẩy LC-ĐN";
                                var dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * KMTotal * 0.0001M);
                                    if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                    ct.DienGiai += "Đẩy LC-ĐN=" + dmNL.DinhMuc + "*" + tanDT.ToString("N2") + "*" + heSo + cachTinh + "*" + KMTotal + "*0.0001 Lít/10000T.Km";
                                }
                            }
                            else
                            {
                                //Lấy Tấn Max
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                decimal tanDM = decimal.Parse(HttpHelper.GetString<string>(Configuration.UrlCBApi + "api/SaiGons/SGGetTanMax" + data).Result);
                                if (tanDM > 0)
                                    tanDM = tanBQ > tanDM ? tanDM : tanBQ;
                                else
                                    tanDM = tanBQ;
                                //Lấy hệ số tấn.
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&Tan=" + tanDM;
                                data += "&DonVi=";
                                var hsTan = HttpHelper.Get<SGHSTan>(Configuration.UrlCBApi + "api/SaiGons/SGGetHSTanOBJ" + data).Result;
                                tanBQ = hsTan != null ? (decimal)(tanBQ * hsTan.HeSo) : tanBQ;
                                string cachTinh = hsTan != null ? hsTan.DonVi : "P";
                                decimal heSo = hsTan != null ? (decimal)hsTan.HeSo : 1;
                                if (ct.XeRongTotal >= 5 && ct.CongTacID >= 4)
                                    tanBQ += ct.TanXeRong * 25 / 100;
                                //Nạp đinh mức
                                string macTau = ct.TinhChatID == 2 || cachTinh == "D" ? "Chạy đơn" : ct.MacTauID;
                                data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=" + ct.KhuDoan;
                                data += "&MacTau=" + macTau;
                                var dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmNL != null)
                                {
                                    if (dmNL.DonVi == "Lít/100Km")
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * heSo * KMTotal * 0.01M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + heSo + "*" + KMTotal + "*0.01 " + dmNL.DonVi;
                                    }
                                    else
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * KMTotal * 0.0001M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001 " + dmNL.DonVi;
                                    }
                                }
                                else
                                {
                                    data = "?NgayHL=" + rowCoBao.NgayCB;
                                    data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                    data += "&KhuDoan=Các nơi khác";
                                    data += "&MacTau=" + macTau;
                                    dmNL = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                    if (dmNL.DonVi == "Lít/100Km")
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * heSo * KMTotal * 0.01M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + heSo + "*" + KMTotal + "*0.01 " + dmNL.DonVi;
                                    }
                                    else
                                    {
                                        ct.DinhMuc += (decimal)(dmNL.DinhMuc * tanBQ * KMTotal * 0.0001M);
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += dmNL.LoaiTau + "=" + dmNL.DinhMuc + "*" + tanBQ.ToString("N2") + "*" + KMTotal + "*0.0001 " + dmNL.DonVi;
                                    }
                                }

                            }
                            //Cộng thêm định mức dồn
                            if (sumDon > 0)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=Các nơi khác";
                                data += "&MacTau=KDON";
                                var dmDon = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmDon != null)
                                {
                                    {
                                        decimal gioDon = (decimal)sumDon / 60;
                                        ct.DinhMuc += (decimal)dmDon.DinhMuc * gioDon;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "KDON=" + dmDon.DinhMuc + "*" + gioDon.ToString("N2") + " " + dmDon.DonVi;
                                    }
                                }
                            }
                            //Cộng thêm định mức dừng nổ máy
                            if (ct.DungNM > 0)
                            {
                                string data = "?NgayHL=" + rowCoBao.NgayCB;
                                data += "&LoaiMay=" + rowCoBao.LoaiMayID;
                                data += "&KhuDoan=Dừng nổ máy";
                                data += "&MacTau=Dừng";
                                var dmDung = HttpHelper.Get<SGNLDinhMuc>(Configuration.UrlCBApi + "api/SaiGons/SGGetNLDinhMucOBJ" + data).Result;
                                if (dmDung != null)
                                {
                                    {
                                        decimal gioDung = (decimal)ct.DungNM / 60;
                                        ct.DinhMuc += (decimal)dmDung.DinhMuc * gioDung;
                                        if (!string.IsNullOrWhiteSpace(ct.DienGiai)) ct.DienGiai += "-";
                                        ct.DienGiai += "Dừng NM=" + dmDung.DinhMuc + "*" + gioDung.ToString("N2") + " " + dmDung.DonVi;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            { }
        }

        #endregion
    }
}

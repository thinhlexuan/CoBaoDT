using CBClient.Adapter;
using CBClient.BLLTypes;
using CBClient.Common;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLDaos
{
    public class CoBaoDAO
    {
       
        #region CoBao
        public static async Task<string> NapThanhTich(long CoBaoID)
        {
            string strResult = string.Empty;
            try
            {                
                CoBaoTT info = await HttpHelper.Get<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaos/GetThanhTich?id=" + CoBaoID);                
                if (info != null)
                {
                    strResult = 
                      " 1.Cơ báo số: " + info.SoCB + "-" + info.CoBaoID + "-" + info.CoBaoGoc + ".\r\n"
                    + " 2.Ngày cơ báo: " + info.NgayCB.ToString("dd.MM.yyyy") + ".\r\n"
                    + " 3.Số hiệu ĐM: " +info.DauMayID + ".\r\n"
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
        public static string NapThanhTichByCoBaoGoc(long CoBaoID)
        {
            string strResult = string.Empty;
            try
            {
                var listCobaoTT = HttpHelper.GetList<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaos/GetThanhTichByCoBaoGoc?id=" + CoBaoID);
                if(listCobaoTT!=null && listCobaoTT.Count>0)
                {
                    CoBaoTT info = (from x in listCobaoTT group x by new { x.CoBaoGoc, x.DauMayID } into g
                                    select new CoBaoTT
                                    {
                                        CoBaoID = g.FirstOrDefault().CoBaoID,
                                        CoBaoGoc = g.Key.CoBaoGoc,
                                        SoCB = g.FirstOrDefault().SoCB,
                                        NgayCB = g.FirstOrDefault().NgayCB,
                                        DauMayID = g.Key.DauMayID,
                                        LoaiMayID = g.FirstOrDefault().LoaiMayID,
                                        QuayVong = g.Sum(x => x.QuayVong),
                                        LuHanh=g.Sum(x=>x.LuHanh),
                                        DonThuan=g.Sum(x=>x.DonThuan),
                                        GioDung=g.Sum(x=>x.GioDung),
                                        GioDon=g.Sum(x=>x.GioDon),
                                        KM=g.Sum(x=>x.KM),
                                        TKM=g.Sum(x=>x.TKM),
                                        NLTieuChuan=g.Sum(x=>x.NLTieuChuan),
                                        NLTieuThu=g.Sum(x=>x.NLTieuThu),
                                        NLLoiLo=g.Sum(x=>x.NLLoiLo)
                                    }).FirstOrDefault();               
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
        public static async Task<BCCoBaoTTInfo> NapObThanhTich(long CoBaoID)
        {
            BCCoBaoTTInfo infobc = new BCCoBaoTTInfo();
            try
            {
                CoBaoTT info = await HttpHelper.Get<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaos/GetThanhTich?id=" + CoBaoID);
                if (info != null)
                {
                    infobc = new BCCoBaoTTInfo();
                    infobc.GioDM =  (decimal)info.QuayVong / 60;
                    infobc.GioLH = (decimal)info.LuHanh / 60;
                    infobc.GioDT = (decimal)info.DonThuan / 60;
                    infobc.GioDung = (decimal)info.GioDung / 60;
                    infobc.GioDon = (decimal)info.GioDon / 60M;
                    infobc.KMChay = info.KM;
                    infobc.TKM = info.TKM;
                    infobc.DinhMuc = info.NLTieuChuan;
                    infobc.TieuThu = info.NLTieuThu;
                    infobc.LoiLo = info.NLLoiLo;
                    infobc.SoCB = info.SoCB;

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(info.SoCB, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, null, 15);
                    MemoryStream ms = new MemoryStream();
                    qrCodeImage.Save(ms, ImageFormat.Png);  // save bitmap to a memory stream
                    infobc.Qrcode = ms.ToArray();
                }
            }
            //catch(Exception ex)
            catch
            {
                //DialogHelper.Error(ex.Message);
                infobc = null;
            }
            return infobc;
        }

        public static  BCCoBaoTTInfo NapObThanhTichByCoBaoGoc(long CoBaoID)
        {
            BCCoBaoTTInfo infobc = new BCCoBaoTTInfo();
            try
            {
                var listCobaoTT = HttpHelper.GetList<CoBaoTT>(Configuration.UrlCBApi + "api/CoBaos/GetThanhTichByCoBaoGoc?id=" + CoBaoID);
                if (listCobaoTT!=null && listCobaoTT.Count>0)
                {
                    CoBaoTT info = (from x in listCobaoTT
                                    group x by new { x.CoBaoGoc, x.DauMayID } into g
                                    select new CoBaoTT
                                    {
                                        CoBaoID = g.FirstOrDefault().CoBaoID,
                                        CoBaoGoc = g.Key.CoBaoGoc,
                                        SoCB = g.FirstOrDefault().SoCB,
                                        NgayCB = g.FirstOrDefault().NgayCB,
                                        DauMayID = g.Key.DauMayID,
                                        LoaiMayID = g.FirstOrDefault().LoaiMayID,
                                        QuayVong = g.Sum(x => x.QuayVong),
                                        LuHanh = g.Sum(x => x.LuHanh),
                                        DonThuan = g.Sum(x => x.DonThuan),
                                        GioDung = g.Sum(x => x.GioDung),
                                        GioDon = g.Sum(x => x.GioDon),
                                        KM = g.Sum(x => x.KM),
                                        TKM = g.Sum(x => x.TKM),
                                        NLTieuChuan = g.Sum(x => x.NLTieuChuan),
                                        NLTieuThu = g.Sum(x => x.NLTieuThu),
                                        NLLoiLo = g.Sum(x => x.NLLoiLo)
                                    }).FirstOrDefault();
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
                    infobc.SoCB = info.SoCB;

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(info.SoCB, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, null, 15);
                    MemoryStream ms = new MemoryStream();
                    qrCodeImage.Save(ms, ImageFormat.Png);  // save bitmap to a memory stream
                    infobc.Qrcode = ms.ToArray();
                }
            }
            //catch(Exception ex)
            catch
            {
                //DialogHelper.Error(ex.Message);
                infobc = null;
            }
            return infobc;
        }

        #endregion
    }
}

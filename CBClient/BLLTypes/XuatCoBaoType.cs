﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLTypes
{
    public class XCoBao
    {      
        public long CoBaoID { get; set; }
        public long CoBaoGoc { get; set; }
        public string SoCB { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DvdmID { get; set; }
        public string DvdmName { get; set; }
        public string DvcbID { get; set; }
        public string DvcbName { get; set; }
        public DateTime NgayCB { get; set; }
        public int RutGio { get; set; }
        public string ChatLuong { get; set; }
        public decimal SoLanRaKho { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public short TaiXe1GioLT { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public short PhoXe1GioLT { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public short TaiXe2GioLT { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public short PhoXe2GioLT { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public short TaiXe3GioLT { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public short PhoXe3GioLT { get; set; }
        public DateTime LenBan { get; set; }
        public DateTime NhanMay { get; set; }
        public DateTime RaKho { get; set; }
        public DateTime VaoKho { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime XuongBan { get; set; }
        public int NLBanTruoc { get; set; }
        public int NLThucNhan { get; set; }
        public int NLLinh { get; set; }
        public string TramNLID { get; set; }
        public decimal NLTrongDoan { get; set; }
        public int NLBanSau { get; set; }
        public string SHDT { get; set; }
        public string MaCB { get; set; }
        public decimal DonDocDuong { get; set; }
        public decimal DungDocDuong { get; set; }
        public decimal DungNoMay { get; set; }
        public string GhiChu { get; set; }
    }
    public class XCoBaoCT
    {       
        public long ID { get; set; }
        public long CoBaoID { get; set; }
        public DateTime NgayXP { get; set; }
        public DateTime GioDen { get; set; }
        public DateTime GioDi { get; set; }
        public decimal GioDon { get; set; }
        public string MacTauID { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short CongTacID { get; set; }
        public string CongTacName { get; set; }
        public int GaID { get; set; }
        public string GaName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
        public decimal Tan { get; set; }
        public int XeTotal { get; set; }
        public decimal TanXeRong { get; set; }
        public int XeRongTotal { get; set; }
        public short TinhChatID { get; set; }
        public string TinhChatName { get; set; }
        public string MayGhepID { get; set; }
        public decimal KmAdd { get; set; }
    }
    public class XCoBaoDM
    {       
        public long ID { get; set; }
        public long CoBaoID { get; set; }
        public short LoaiDauMoID { get; set; }
        public string LoaiDauMoName { get; set; }
        public string DonViTinh { get; set; }
        public decimal Nhan { get; set; }
        public decimal Linh { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public decimal Giao { get; set; }
    }
}

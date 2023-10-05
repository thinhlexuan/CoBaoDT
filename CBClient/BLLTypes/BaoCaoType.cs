using System;

namespace CBClient.BLLTypes
{
    #region Info
    public class BCVanDungInfo
    {
        public short SoTT { get; set; }
        public string ChiTieu { get; set; }
        public decimal KhachTNChinh { get; set; }
        public decimal KhachTNDon { get; set; }
        public decimal KhachTNDay { get; set; }
        public decimal KhachTNGhep { get; set; }
        public decimal KhachDPChinh { get; set; }
        public decimal KhachDPDon { get; set; }
        public decimal KhachDPDay { get; set; }
        public decimal KhachDPGhep { get; set; }
        public decimal HangChinh { get; set; }
        public decimal HangDon { get; set; }
        public decimal HangDay { get; set; }
        public decimal HangGhep { get; set; }
        public decimal DaChinh { get; set; }
        public decimal DaDon { get; set; }
        public decimal DaDay { get; set; }
        public decimal DaGhep { get; set; }
        public decimal ThoiChinh { get; set; }
        public decimal ThoiDon { get; set; }
        public decimal ThoiDay { get; set; }
        public decimal ThoiGhep { get; set; }
        public decimal ChuyenDon { get; set; }
        public decimal CongDung { get; set; }
        public decimal TongCong { get; set; }        
    }

    public class BCTonNLInfo
    {
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public decimal TonDau { get; set; }
        public decimal TFGBA { get; set; }
        public decimal TFYVI { get; set; }
        public decimal TFVIN { get; set; }
        public decimal TFDNA { get; set; }
        public decimal TFSGO { get; set; }
        public decimal TFHNO { get; set; }
        public decimal TFNBI { get; set; }
        public decimal TFHPH { get; set; }
        public decimal TFDTR { get; set; }
        public decimal TFHUE { get; set; }
        public decimal TFQNG { get; set; }
        public decimal TFBTH { get; set; }
        public decimal TFNTH { get; set; }        
        public decimal TFSOT { get; set; }
        public decimal TFDDA { get; set; }
        public decimal TFLCA { get; set; }
        public decimal TFLTH { get; set; }
        public decimal TFMKH { get; set; }
        public decimal TFVTR { get; set; }
        public decimal TFXGA { get; set; }
        public decimal TFYBI { get; set; }
        public decimal TFTHO { get; set; }
        public decimal TFPUT { get; set; }
        public decimal TFDHO { get; set; }
        public decimal TFNTR { get; set; }
        public decimal Linh { get; set; }
        public decimal TieuThu { get; set; }
        public decimal TonCuoi { get; set; }

    }

    public class BKDauMoInfo
    {
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public string SoCB { get; set; }
        public decimal TFGBA { get; set; }
        public decimal TFYVI { get; set; }
        public decimal TFVIN { get; set; }
        public decimal TFDNA { get; set; }
        public decimal TFSGO { get; set; }
        public decimal TFHNO { get; set; }
        public decimal TFNBI { get; set; }
        public decimal TFHPH { get; set; }
        public decimal TFDTR { get; set; }
        public decimal TFHUE { get; set; }
        public decimal TFQNG { get; set; }
        public decimal TFBTH { get; set; }
        public decimal TFNTH { get; set; }
        public decimal TFSOT { get; set; }
        public decimal TFDDA { get; set; }
        public decimal TFLCA { get; set; }
        public decimal TFLTH { get; set; }
        public decimal TFMKH { get; set; }
        public decimal TFVTR { get; set; }
        public decimal TFXGA { get; set; }
        public decimal TFYBI { get; set; }
        public decimal TFTHO { get; set; }
        public decimal TFPUT { get; set; }
        public decimal TFDHO { get; set; }
        public decimal TFNTR { get; set; }
        public decimal Linh { get; set; }
    }

    public class BKTinhLuongInfo
    {
        public short STT { get; set; }        
        public string SoCB { get; set; }
        public DateTime NgayCB { get; set; }
        public string DauMayID { get; set; }
        public decimal GioB { get; set; }
        public decimal GioVLM { get; set; }
        public string MacTauID { get; set; }
        public decimal GioLT { get; set; }
        public decimal GioTN { get; set; }
        public decimal NLLoi { get; set; }
        public decimal NLLo { get; set; }
        public decimal KM { get; set; }
        public decimal GioLD { get; set; }
        public string MaCB { get; set; }
        public decimal KepN { get; set; }
        public string MaLD { get; set; }
        public string TenLD { get; set; }
        public decimal TaiXe { get; set; }
        public decimal PhoXe { get; set; }
        public short BanLT { get; set; }
    }

    public class BCGioDonInfo
    {
        public short SoTT { get; set; }
        public short MaXN { get; set; }
        public string TenXN { get; set; }
        public string MaGa { get; set; }
        public string TenGa { get; set; }
        public decimal D4H { get; set; }
        public decimal D5H { get; set; }
        public decimal D9E { get; set; }
        public decimal D10H { get; set; }
        public decimal D10H_CAT { get; set; }
        public decimal D11H { get; set; }
        public decimal D12E { get; set; }
        public decimal D13E { get; set; }
        public decimal D14E { get; set; }
        public decimal D18E { get; set; }
        public decimal D19E { get; set; }
        public decimal D20E { get; set; }
        public decimal D4Hr { get; set; }        
        public decimal D19Er { get; set; }
        public decimal Tong { get; set; } 
    }

    public class BCCTGioDonInfo
    {
        public short SoTT { get; set; }        
        public string DonViDM { get; set; }
        public string DonViKT { get; set; }
        public string TenGa { get; set; }
        public DateTime NhanMay { get; set; }
        public string DauMay { get; set; }
        public string SoCB { get; set; }
        public string MaTX { get; set; }
        public string TenTX { get; set; }
        public int GioDon { get; set; }
    }
    public class BCKTKTXNInfo
    {        
        public string TenCT { get; set; }
        public string MaCT { get; set; }
        public string DonVi { get; set; }
        public decimal KVTN { get; set; }
        public decimal KVDP { get; set; }
        public decimal KVHH { get; set; }
        public decimal KVTong { get; set; }
        public decimal HVHang { get; set; }
        public decimal HVDa { get; set; }
        public decimal HVThoi { get; set; }
        public decimal HVTong { get; set; }
        public decimal CongDung { get; set; }
        public decimal KiemDon { get; set; }
        public decimal ChuyenDon { get; set; }
        public decimal TongCong { get; set; }        
    }

    public class BCKTKTTHInfo
    {
        public string TenCT { get; set; }
        public string MaCT { get; set; }
        public string DonVi { get; set; }
        public decimal KVYV1435 { get; set; }
        public decimal KVYV1000 { get; set; }
        public decimal KVHN { get; set; }
        public decimal KVVI { get; set; }
        public decimal KVDN { get; set; }
        public decimal KVSG { get; set; }
        public decimal KVTong { get; set; }
        public decimal HVYV1435 { get; set; }
        public decimal HVYV1000 { get; set; }
        public decimal HVHN { get; set; }
        public decimal HVVI { get; set; }
        public decimal HVDN { get; set; }
        public decimal HVSG { get; set; }        
        public decimal HVTong { get; set; }
        public decimal CongDung { get; set; }
        public decimal KiemDon { get; set; }
        public decimal ChuyenDon { get; set; }
        public decimal TongCong { get; set; }
    }
    public class BCSSKTKTInfo
    {
        public string XiNghiep { get; set; }
        public string CongTac { get; set; }
        public string LoaiMay { get; set; }
        public string LoaiCT { get; set; }
        public string DonVi { get; set; }
        public decimal HienTai { get; set; }
        public decimal KyTruoc { get; set; }
        public decimal ChenhLechKT { get; set; }
        public decimal TyLeKT { get; set; }
        public decimal CungKy { get; set; }
        public decimal ChenhLechCK { get; set; }
        public decimal TyLeCK { get; set; }
    }
    public class BCHieuQuaSDDMInfo
    {
        public string XiNghiep { get; set; }
        public string CongTac { get; set; }
        public string LoaiMay { get; set; }       
        public decimal KmChinh { get; set; }
        public decimal KmPhuTro { get; set; }
        public decimal VTKm { get; set; }
        public decimal GioDM { get; set; }
        public decimal GioDon { get; set; }
        public decimal KmBQ { get; set; }
        public decimal TanBQ { get; set; }
        public decimal NSuatBQ { get; set; }
        public decimal MayBQ { get; set; }
        public decimal TieuThu { get; set; }
    }

    public class BCTHNLInfo
    {
        public string MaCap1 { get; set; }
        public string TenCap1 { get; set; }
        public string MaCap2 { get; set; }
        public string TenCap2 { get; set; }
        public string MaCap3 { get; set; }
        public string TenCap3 { get; set; }
        public string MaLM { get; set; }
        public decimal DMKH { get; set; }
        public decimal DMTH { get; set; }
        public decimal TanKM { get; set; }
        public decimal GioDon { get; set; }
        public decimal NLTC { get; set; }
        public decimal NLTT { get; set; }
        public decimal NLLL { get; set; }       
    }
    public class BCTHNLKDInfo
    { 
        public string MaLM { get; set; }
        public short MaCT { get; set; }
        public string TenCT { get; set; }
        public string KhuDoan { get; set; }       
        public decimal GioDon { get; set; }
        public decimal KM { get; set; }
        public decimal TanKM { get; set; }
        public decimal TanBQ { get; set; }
        public decimal NLTT { get; set; }
        public decimal DMTH { get; set; }
    }

    public class BCTTNLInfo
    {
        public short MaCT { get; set; }
        public string TenCT { get; set; }       
        public string MaLM { get; set; }
        public decimal DMKH { get; set; }
        public decimal DMTH { get; set; }
        public decimal NLTC { get; set; }
        public decimal NLTT { get; set; }
        public decimal NLTT15 { get; set; }
        public decimal KMChinh { get; set; }
        public decimal KMDon { get; set; }
        public decimal KMGhep { get; set; }
        public decimal KMDay { get; set; }
        public decimal KMDonTD { get; set; }
        public decimal KMDungTD { get; set; }
        public decimal KMTong { get; set; }
        public decimal TanKM { get; set; }
        public decimal TanBQ { get; set; }

    }
    public class BCTTDMInfo
    {        
        public string MaDM { get; set; }
        public string MaLM { get; set; }
        public decimal DMKH { get; set; }
        public decimal DMTH { get; set; }
        public decimal NLTC { get; set; }
        public decimal NLTT { get; set; }
        public decimal NLTT15 { get; set; }
        public decimal KMChinh { get; set; }
        public decimal KMDon { get; set; }
        public decimal KMGhep { get; set; }
        public decimal KMDay { get; set; }
        public decimal KMDonTD { get; set; }
        public decimal KMDungTD { get; set; }
        public decimal KMTong { get; set; }
        public decimal TanKM { get; set; }
        public decimal TanBQ { get; set; }

    }
    public class BCTTTXInfo
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string Tram { get; set; }
        public string Doi { get; set; }
        public string MaTX { get; set; }
        public string TenTX { get; set; }
        public int SoCB { get; set; }
        public decimal KM { get; set; }
        public decimal NLLoi { get; set; }
        public decimal NLLo { get; set; }
        public string GhiChu { get; set; }
    }
    public class BCDCSPTNInfo
    {
        public string MaCap1 { get; set; }
        public string TenCap1 { get; set; }
        public string MaCap2 { get; set; }
        public string TenCap2 { get; set; }
        public string MaLM { get; set; }
        public decimal KMCH { get; set; }
        public decimal KMPT { get; set; }
        public decimal TanKM { get; set; }
        public decimal GioDon { get; set; }
        public decimal NLTT { get; set; }
    }

    public class BCTongHopInfo
    {
        public short MaDV { get; set; }
        public string TenDV { get; set; }
        public short MaCT { get; set; }
        public string TenCT { get; set; }
        public string MaLM { get; set; }        
        public decimal GioDM { get; set; }
        public decimal GioLH { get; set; }
        public decimal GioDon { get; set; }
        public decimal NgayMKT { get; set; }
        public decimal KMCH { get; set; }
        public decimal KMPT { get; set; }
        public decimal KMDungTD { get; set; }
        public decimal KMDonTD { get; set; }
        public decimal KMTO { get; set; }
        public decimal TanKM { get; set; }
        public decimal NLTT { get; set; }
        public decimal NLTT15 { get; set; }
        public decimal NLTC15 { get; set; }
        public decimal NLTK { get; set; }
        public decimal NLVTTT { get; set; }
        public decimal NLVTTC { get; set; }
        public decimal NLVTTK { get; set; }
    }

    public class BCSPCDInfo
    {
        public short MaDV { get; set; }
        public string TenDV { get; set; }
        public short MaCT { get; set; }
        public string TenCT { get; set; }
        public decimal GioDM { get; set; }
        public decimal GioLH { get; set; }
        public decimal NgayMKT { get; set; }
        public decimal GioDon { get; set; }
        public decimal DonTD { get; set; }
        public decimal DungTD { get; set; }        
        public decimal KMCH { get; set; }
        public decimal KMPT { get; set; }       
        public decimal KMTO { get; set; }
        public decimal TanKM { get; set; }
        public decimal NLTT { get; set; }
        public decimal NLTT15 { get; set; }
    }

    public class BCTHSPTNInfo
    {
        public short SoTT { get; set; }
        public short MaCT { get; set; }
        public string TenCT { get; set; }
        public string MaLM { get; set; }        
        public decimal KMCH { get; set; }
        public decimal KMPT { get; set; }
        public decimal DonTD { get; set; }        
        public decimal TanKM { get; set; }
    }

    public class BCCoBaoCTInfo
    {
        public int SoTT { get; set; }
        public DateTime NgayXP { get; set; }
        public string GioDen { get; set; }
        public string GioDi { get; set; }
        public string GioDon { get; set; }
        public string MacTau { get; set; }
        public string GaDi { get; set; }
        public decimal Tan { get; set; }
        public int xeTotal { get; set; }
        public decimal TanRong { get; set; }
        public int xeRong { get; set; }
        public string TinhChat { get; set; }
        public string MayGhep { get; set; }
        public decimal KmAdd { get; set; }        
    }

    public class BCCoBaoGACTInfo
    {
        public int SoTT { get; set; }
        public DateTime NgayXP { get; set; }
        public string GioDen { get; set; }
        public string GioDi { get; set; }
        public string GioDon { get; set; }
        public string RutGioNL { get; set; }
        public bool DungGioPT { get; set; }
        public string MacTau { get; set; }
        public string GaDi { get; set; }
        public decimal Tan { get; set; }
        public int xeTotal { get; set; }
        public decimal TanRong { get; set; }
        public int xeRong { get; set; }
        public string TinhChat { get; set; }
        public string MayGhep { get; set; }
        public decimal KmAdd { get; set; }
    }

    public class BCCoBaoTTInfo
    {
        public decimal GioDM { get; set; }
        public decimal GioLH { get; set; }
        public decimal GioDT { get; set; }
        public decimal GioDon { get; set; }
        public decimal GioDung { get; set; }
        public decimal KMChay { get; set; }
        public decimal TKM { get; set; }
        public decimal DinhMuc { get; set; }
        public decimal TieuThu { get; set; }
        public decimal LoiLo { get; set; }        
    }

    public class BCKeHoachInfo
    {
        public short MaLoai { get; set; }
        public string SoTT { get; set; }
        public string TenLoai { get; set; }
        public string DonVi { get; set; }
        public decimal YVKH { get; set; }
        public decimal YVTH { get; set; }
        public decimal YVTL { get; set; }
        public decimal YVPT { get; set; }
        public decimal HNKH { get; set; }
        public decimal HNTH { get; set; }
        public decimal HNTL { get; set; }
        public decimal HNPT { get; set; }
        public decimal VINKH { get; set; }
        public decimal VINTH { get; set; }
        public decimal VINTL { get; set; }
        public decimal VINPT { get; set; }
        public decimal DNKH { get; set; }
        public decimal DNTH { get; set; }
        public decimal DNTL { get; set; }
        public decimal DNPT { get; set; }
        public decimal SGKH { get; set; }
        public decimal SGTH { get; set; }
        public decimal SGTL { get; set; }
        public decimal SGPT { get; set; }
    }
    #endregion

    #region View
    public partial class ViewBcvanDung
    {
        public string DvcbID { get; set; }
        public int ThangDt { get; set; }
        public int NamDt { get; set; }
        public string LoaiMayId { get; set; }
        public string TuyenId { get; set; }
        public int? CongTacId { get; set; }
        public int? TinhChatId { get; set; }
        public int? GioDm { get; set; }
        public int? GioLh { get; set; }
        public int? GioDt { get; set; }       
        public int? Dgxp { get; set; }
        public int? Dgdd { get; set; }
        public int? Dgcc { get; set; }
        public int? Dgqd { get; set; }
        public int? Dgdm { get; set; }
        public int? Dgdn { get; set; }
        public int? Dgkm { get; set; }
        public int? Dgkn { get; set; }
        public int? Dnxp { get; set; }
        public int? Dndd { get; set; }
        public int? Dncc { get; set; }
        public decimal? Kmch { get; set; }
        public decimal? Kmdw { get; set; }
        public decimal? Kmgh { get; set; }
        public decimal? Kmdy { get; set; }
        public decimal? Tkch { get; set; }
        public decimal? Tkdw { get; set; }
        public decimal? Tkgh { get; set; }
        public decimal? Tkdy { get; set; }
        public decimal? Slrkm { get; set; }
        public decimal? Slrkn { get; set; }
        public decimal? Sltt { get; set; }
        public decimal? Sltt15 { get; set; }
        public decimal? Sltc { get; set; }
    }

    public partial class ViewBcSSKTKT
    {
        public string Dvcb { get; set; }
        public string CongTac { get; set; }        
        public string LoaiMay { get; set; }        
        public int? GioHT { get; set; }
        public int? GioKT { get; set; }
        public int? GioCK { get; set; }
        public decimal? KmHT { get; set; }
        public decimal? KmKT { get; set; }
        public decimal? KmCK { get; set; }
        public decimal? TKmHT { get; set; }
        public decimal? TKmKT { get; set; }
        public decimal? TKmCK { get; set; }
    }

    public partial class ViewBcGioDon
    {  
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string GaXPName { get; set; }
        public short? CongTacID { get; set; }
        public int? GioDon { get; set; }
    }

    public partial class ViewBcGioDonCT
    {       
        public string DvcbID { get; set; }
        public string GaName { get; set; }
        public string DauMayID { get; set; }
        public DateTime NhanMay { get; set; }
        public string SoCB { get; set; }
        public string TaiXeID { get; set; }
        public string TaiXeName { get; set; }
        public int? GioDon { get; set; }
    }

    public partial class ViewBcNhienLieu
    {
        public long DoanThongID { get; set; }
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string TuyenID { get; set; }
        public short? CongTacID { get; set; }
        public short? TinhChatID { get; set; }
        public int GaXPID { get; set; }
        public int GaKTID { get; set; }
        public int? GioDon { get; set; }
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
    }
    public partial class ViewBcNhienLieuKD
    {        
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public short? CongTacID { get; set; }
        public string KhuDoan { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public int? GioDon { get; set; }
        public decimal? KM { get; set; }
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
    }

    public partial class ViewBcTTNhienLieu
    {  
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string TuyenID { get; set; }
        public short? CongTacID { get; set; }
        public string CongTacName { get; set; }
        public short? TinhChatID { get; set; }
        public string GaXP { get; set; }
        public string GaKT { get; set; }
        public int? GioDon { get; set; }
        public int? GioDung { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMDon { get; set; }
        public decimal? KMGhep { get; set; }
        public decimal? KMDay { get; set; }       
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
        public decimal? TieuThu15 { get; set; }
    }
    public partial class ViewBcTTTaiXe
    {
        public long CoBaoID { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string DvcbID { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public int? GioDung { get; set; }
        public int? GioDon { get; set; }
        public decimal? KM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
    }
    public partial class ViewBcTacNghiep
    {        
        public long DoanThongID { get; set; }
        public string DvcbID { get; set; }
        public string DvdmID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public short? CongTacID { get; set; }
        public short? TinhChatID { get; set; }
        public string GaXP { get; set; }
        public string GaKT { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMPhuTro { get; set; }
        public int? GioLH { get; set; }
        public int? GioDon { get; set; }
        public decimal? TanKM { get; set; }
        public decimal? TieuThu { get; set; }
    }

    public partial class ViewBcTonNL
    {      
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NhanMay { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public decimal? TonDau { get; set; }
        public decimal? Linh { get; set; }
        public decimal? TieuThu { get; set; }
        public decimal? TonCuoi { get; set; }
    }

    public partial class ViewBcBKDauMo
    {       
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public string SoCB { get; set; }
        public string MaTram { get; set; }       
        public decimal? Linh { get; set; }
    }

    public partial class ViewBcBKLuong
    {        
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public string SoCB { get; set; }
        public string MaCB { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public string MacTauID { get; set; }
        public short? TinhChatID { get; set; }
        public int? GioDm { get; set; }
        public decimal? Km { get; set; }
        public decimal? NLLoiLo { get; set; }
    }
    public class ViewBcKeHoach
    {        
        public string DvcbID { get; set; }
        public short? CongTacID { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMPhuTro { get; set; }
        public decimal? TKMChinh { get; set; }
        public decimal? TKMPhuTro { get; set; }
        public int? GioDon { get; set; }
    }
    #endregion

}

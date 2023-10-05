using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public class OperationStatus
{
    [DataMember]
    public bool IsSuccess;
    [DataMember]
    public int RecordsAffected { get; set; }
    [DataMember]
    public string Message { get; set; }
    [DataMember]
    public long NewID { get; set; }

    public static OperationStatus CreateFromException(Exception ex)
    {
        OperationStatus opStatus = new OperationStatus
        {
            IsSuccess = false,
            RecordsAffected = -1,
            Message = ex.Message,
            NewID = -1
        };
        return opStatus;
    }
}

[DataContract]
public class NhanVienInfo
{
    [DataMember]
    public string MaNV { get; set; }
    [DataMember]
    public string TenNV { get; set; }
    [DataMember]
    public string MatKhau { get; set; }
    [DataMember]
    public short MaQH { get; set; }
    [DataMember]
    public string TenQH { get; set; }
    [DataMember]
    public short MaDV { get; set; }
    [DataMember]
    public string TenDV { get; set; }   
    [DataMember]
    public short MaCha { get; set; }
    [DataMember]
    public string PhongBan { get; set; }
    [DataMember]
    public bool Active { get; set; }
}

[DataContract]
public class DMDauMayInfo
{
    [DataMember]
    public string DauMayID { get; set; }
    [DataMember]
    public string LoaiMayID { get; set; }
    [DataMember]
    public string LoaiMayName { get; set; }
    [DataMember]
    public string TramID { get; set; }
    [DataMember]
    public string TramName { get; set; }
    [DataMember]
    public string GhiChu { get; set; }
    [DataMember]
    public bool Active { get; set; }   
}

[DataContract]
public class DMTaiXeInfo
{
    [DataMember]
    public string TaiXeID { get; set; }
    [DataMember]
    public string TaiXeName { get; set; }
    [DataMember]
    public string ChucDanh { get; set; }
    [DataMember]
    public string DoiMay { get; set; }    
    [DataMember]
    public bool Active { get; set; }
}

[DataContract]
public class DMMacTauInfo
{
    [DataMember]
    public string MacTauID { get; set; }
    [DataMember]
    public string CongTacID { get; set; }
    [DataMember]
    public string CongTacName { get; set; }    
    [DataMember]
    public bool Active { get; set; }    
}

[DataContract]
public class HeSoQDNLInfo
{
    [DataMember]
    public short MaDV { get; set; }
    [DataMember]
    public string TenDV { get; set; }
    [DataMember]
    public int Thang { get; set; }
    [DataMember]
    public int Nam { get; set; }
    [DataMember]
    public decimal HeSoLit { get; set; }
    [DataMember]
    public decimal HeSoKg { get; set; }
    [DataMember]
    public decimal NhietDo { get; set; }    
}

[DataContract]
public class DinhMucNLInfo
{
    [DataMember]
    public short MaDV { get; set; }
    [DataMember]
    public string TenDV { get; set; }
    [DataMember]
    public short MaCT { get; set; }
    [DataMember]
    public string LoaiMayID { get; set; }
    [DataMember]
    public string ThoiDB { get; set; }
    [DataMember]
    public string DVTinh { get; set; }    
    [DataMember]
    public decimal DMLit15 { get; set; }
    [DataMember]
    public DateTime NgayHL { get; set; }    
}

[DataContract]
public class CoBaoInfo
{
    [DataMember]
    public long CoBaoID { get; set; }
    [DataMember]
    public long CoBaoGoc { get; set; }
    [DataMember]
    public string DauMayID { get; set; }
    [DataMember]
    public string LoaiMayID { get; set; }
    [DataMember]
    public string DvdmID { get; set; }
    [DataMember]
    public string DvdmName { get; set; }
    [DataMember]
    public string SoCB { get; set; }
    [DataMember]
    public string DvcbID { get; set; }
    [DataMember]
    public string DvcbName { get; set; }
    [DataMember]
    public DateTime NgayCB { get; set; }
    [DataMember]
    public int RutGio { get; set; }
    [DataMember]
    public string ChatLuong { get; set; }
    [DataMember]
    public decimal SoLanRaKho { get; set; }
    [DataMember]
    public string TaiXe1ID { get; set; }
    [DataMember]
    public string TaiXe1Name { get; set; }
    [DataMember]
    public string PhoXe1ID { get; set; }
    [DataMember]
    public string PhoXe1Name { get; set; }
    [DataMember]
    public string TaiXe2ID { get; set; }
    [DataMember]
    public string TaiXe2Name { get; set; }
    [DataMember]
    public string PhoXe2ID { get; set; }
    [DataMember]
    public string PhoXe2Name { get; set; }
    [DataMember]
    public string TaiXe3ID { get; set; }
    [DataMember]
    public string TaiXe3Name { get; set; }
    [DataMember]
    public string PhoXe3ID { get; set; }
    [DataMember]
    public string PhoXe3Name { get; set; }
    [DataMember]
    public DateTime LenBan { get; set; }
    [DataMember]
    public DateTime NhanMay { get; set; }    
    [DataMember]
    public DateTime RaKho { get; set; }
    [DataMember]
    public DateTime VaoKho { get; set; }
    [DataMember]
    public DateTime GiaoMay { get; set; }
    [DataMember]
    public DateTime XuongBan { get; set; }
    [DataMember]
    public int NLBanTruoc { get; set; }
    [DataMember]
    public int NLThucNhan { get; set; }
    [DataMember]
    public int NLLinh { get; set; }
    [DataMember]
    public string TramNLID { get; set; }
    [DataMember]
    public decimal NLTrongDoan { get; set; }
    [DataMember]
    public int NLBanSau { get; set; }
    [DataMember]
    public decimal DauDCNhan { get; set; }
    [DataMember]
    public decimal DauDCLinh { get; set; }
    [DataMember]
    public string TramDCID { get; set; }
    [DataMember]
    public decimal DauDCGiao { get; set; }
    [DataMember]
    public decimal DauTLNhan { get; set; }
    [DataMember]
    public decimal DauTLLinh { get; set; }
    [DataMember]
    public string TramTLID { get; set; }
    [DataMember]
    public decimal DauTLGiao { get; set; }
    [DataMember]
    public decimal DauGTNhan { get; set; }
    [DataMember]
    public decimal DauGTLinh { get; set; }
    [DataMember]
    public string TramGTID { get; set; }
    [DataMember]
    public decimal DauGTGiao { get; set; }
    [DataMember]
    public string SHDT { get; set; }
    [DataMember]
    public string MaCB { get; set; }
    [DataMember]
    public decimal DonDocDuong { get; set; }
    [DataMember]
    public decimal DungDocDuong { get; set; }
    [DataMember]
    public decimal DungNoMay { get; set; }
    [DataMember]
    public DateTime Createddate { get; set; }
    [DataMember]
    public string Createdby { get; set; }
    [DataMember]
    public string CreatedName { get; set; }
    [DataMember]
    public DateTime Modifydate { get; set; }
    [DataMember]
    public string Modifyby { get; set; }
    [DataMember]
    public string ModifyName { get; set; }
    [DataMember]
    public string TrangThai { get; set; }    
    [DataMember]
    public bool KhoaCB { get; set; }
}

[DataContract]
public class DoanThongInfo
{
    [DataMember]
    public long CoBaoID { get; set; }    
    [DataMember]
    public int DungKB { get; set; }    
    [DataMember]
    public int NLTieuThu { get; set; }    
    [DataMember]
    public decimal DauDCTT { get; set; }    
    [DataMember]
    public decimal DauTLTT { get; set; }   
    [DataMember]
    public decimal DauGTTT { get; set; }
    [DataMember]
    public int ThangDT { get; set; }
    [DataMember]
    public int NamDT { get; set; }
    [DataMember]
    public DateTime Createddate { get; set; }
    [DataMember]
    public string Createdby { get; set; }
    [DataMember]
    public string CreatedName { get; set; }
    [DataMember]
    public DateTime Modifydate { get; set; }
    [DataMember]
    public string Modifyby { get; set; }
    [DataMember]
    public string ModifyName { get; set; }
}

[DataContract]
public class DoanThongCTInfo
{
    [DataMember]
    public long CoBaoID { get; set; }
    [DataMember]
    public int SoTT { get; set; }
    [DataMember]
    public DateTime NgayXP { get; set; }
    [DataMember]
    public string MacTauID { get; set; }   
    [DataMember]
    public string CongTyID { get; set; }
    [DataMember]
    public string CongTyName { get; set; }
    [DataMember]
    public string CongTacID { get; set; }
    [DataMember]
    public string CongTacName { get; set; }
    [DataMember]
    public string TinhChatID { get; set; }
    [DataMember]
    public string TinhChatName { get; set; }
    [DataMember]
    public string KhuDoanID { get; set; }
    [DataMember]
    public string DinhMuc { get; set; }
    [DataMember]
    public string TuyenID { get; set; }
    [DataMember]
    public string TuyenName { get; set; }
    [DataMember]
    public string GaXPID { get; set; }
    [DataMember]
    public string GaXPName { get; set; }
    [DataMember]
    public string GaKTID { get; set; }
    [DataMember]
    public string GaKTName { get; set; }
    [DataMember]
    public string MayGhepID { get; set; }
    [DataMember]
    public int QuayVong { get; set; }
    [DataMember]
    public int LuHanh { get; set; }
    [DataMember]
    public int DonThuan { get; set; }
    [DataMember]
    public int DungDM { get; set; }
    [DataMember]
    public int DungDN { get; set; }
    [DataMember]
    public int DungQD { get; set; }
    [DataMember]
    public int DungXP { get; set; }
    [DataMember]
    public int DungDD { get; set; }
    [DataMember]
    public int DungKT { get; set; }
    [DataMember]
    public int DungKhoDM { get; set; }
    [DataMember]
    public int DungKhoDN { get; set; }
    [DataMember]
    public int DonXP { get; set; }
    [DataMember]
    public int DonDD { get; set; }
    [DataMember]
    public int DonKT { get; set; }
    [DataMember]
    public int XeTotal { get; set; }
    [DataMember]
    public int XeRongTotal { get; set; }
    [DataMember]
    public decimal KMChinh { get; set; }
    [DataMember]
    public decimal KMDon { get; set; }
    [DataMember]
    public decimal KMGhep { get; set; }
    [DataMember]
    public decimal KMDay { get; set; }    
    [DataMember]
    public decimal TKMChinh { get; set; }
    [DataMember]
    public decimal TKMDon { get; set; }
    [DataMember]
    public decimal TKMGhep { get; set; }
    [DataMember]
    public decimal TKMDay { get; set; }
    [DataMember]
    public decimal TKMRong { get; set; }
    [DataMember]
    public decimal NLTieuThu { get; set; }
    [DataMember]
    public decimal NLTieuChuan { get; set; }    
    [DataMember]
    public decimal DauDCTT { get; set; }
    [DataMember]
    public decimal DauDCTC { get; set; }    
    [DataMember]
    public decimal DauTLTT { get; set; }
    [DataMember]
    public decimal DauTLTC { get; set; }    
    [DataMember]
    public decimal DauGTTT { get; set; }
    [DataMember]
    public decimal DauGTTC { get; set; }
    [DataMember]
    public decimal SLRKDM { get; set; }
    [DataMember]
    public decimal SLRKDN { get; set; }
    
}
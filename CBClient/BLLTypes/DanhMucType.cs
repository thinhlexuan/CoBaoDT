using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLTypes
{
    public partial class NguoiDung:Common.DMNhanVien
    {
        public string TenDVQL { get; set; }
        public short? MaQH { get; set; }
        public short? NL { get; set; }
        public bool Active { get; set; }
    }
    public partial class NhanVien
    {       
        public string MaNV { get; set; }
        public short? MaQH { get; set; }
        public short? NL { get; set; }
        public string MaDV { get; set; }
        public bool Active { get; set; }
    }   
    public class ViewDMNhanVien
    {
        public int NhanVienID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string MaSo { get; set; }
        public string ChucVu { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string MaCT { get; set; }
        public string DVQL { get; set; }
        public string CodeQL { get; set; }
        public short? CapQL { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
    public partial class BangNhatKy
    {        
        public string TenBang { get; set; }
    }
    public partial class NhatKy
    {       
        public long ID { get; set; }
        public string TenBang { get; set; }
        public string NoiDung { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
    }
    public partial class DauMay
    {    
        public int ID { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string MaCTSoHuu { get; set; }
        public string MaCTQuanLy { get; set; }
        public DateTime? NgayHL { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class ViewDauMay
    {       
        public int ID { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string MaCTSoHuu { get; set; }
        public string TenCTSoHuu { get; set; }
        public string MaCTQuanLy { get; set; }
        public string TenCTQuanLy { get; set; }
        public DateTime? NgayHL { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class DMDauMay
    {
        public int Id { get; set; }
        public string DauMaySo { get; set; }
        public string SoHieuMay { get; set; }
        public string PhanLoai { get; set; }
        public decimal? TuTrong { get; set; }
        public int? Tam2DauDam { get; set; }
        public string MaCtsoHuu { get; set; }
        public string MaCtquanLy { get; set; }
        public int? KhoDuong { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
    public partial class DMTaiXe
    {      
        public string TaiXeID { get; set; }
        public string TaiXeName { get; set; }
        public string DonViID { get; set; }
        public string DonViName { get; set; }
    }
    public partial class DMPhoXe
    {       
        public string PhoXeID { get; set; }
        public string PhoXeName { get; set; }
        public string DonViID { get; set; }
        public string DonViName { get; set; }
    }
    public partial class LoaiMay
    {
        public string LoaiMayId { get; set; }
        public string LoaiMayMap { get; set; }
        public string LoaiMayName { get; set; }
        public short KhoDuong { get; set; }
        public decimal TuTrong { get; set; }
        public short MaLuc { get; set; }
        public short NhomMay { get; set; }
        public short SoTT { get; set; }
        public bool? Active { get; set; }
    }
    public partial class HeSoQdnl
    {
        public long ID { get; set; }
        public string MaDv { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal HesoLit { get; set; }
        public decimal HesoKg { get; set; }
        public decimal NhietDo { get; set; }
    }
    public partial class DmdonVi
    {
        public string MaDv { get; set; }
        public string TenDv { get; set; }
        public string MaDvql { get; set; }
        public string MaCt { get; set; }
        public string Dvql { get; set; }
        public string CodeQl { get; set; }
        public short? CapQl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public short? LoaiDv { get; set; }
        public int? IsActive { get; set; }
    }
    public partial class DonViDM
    {       
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string TenTat { get; set; }
        public string DiaChi { get; set; }
        public string Tinh { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string MST { get; set; }
        public string SoTK { get; set; }
        public string NganHang { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string GaDMList { get; set; }
        public string MaCha { get; set; }
    }
    public partial class DonViKT
    {       
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string MaDVCha { get; set; }
        public short SapXep { get; set; }
        public int GaID { get; set; }
    }
    public partial class CongTy
    {       
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public bool Active { get; set; }
    }
    public partial class DMGa
    {
        public int GaId { get; set; }
        public string MaGa { get; set; }
        public string TenGa { get; set; }
        public string MaGaDs { get; set; }
        public string Keyword { get; set; }
        public int? IsActive { get; set; }
    }
    public partial class GaChuyenDon
    { 
        public int GaId { get; set; }
        public string GaName { get; set; }
        public DateTime NgayHL { get; set; }
        public bool Active { get; set; }
        public string GhiChu { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class DmtramNhienLieu
    {
        public int Id { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public string MaDvql { get; set; }
        public byte? IsActive { get; set; }
    }
    public partial class DmLyTrinh
    {       
        public string TuyenId { get; set; }
        public string TuyenName { get; set; }
        public int? GaDiId { get; set; }
        public string GaDiName { get; set; }
        public decimal? GaDiKM { get; set; }
        public int? GaDenId { get; set; }
        public string GaDenName { get; set; }
        public decimal? GaDenKM { get; set; }
        public string Chieu { get; set; }
    }
    public partial class LyTrinh
    {
        public long ID { get; set; }
        public string TuyenID { get; set; }
        public string TuyenName { get; set; }
        public int? GaID { get; set; }
        public string TenGa { get; set; }
        public decimal? Km { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class CongTac
    {
        public short CongTacId { get; set; }
        public string CongTacName { get; set; }
    }
    public partial class LoaiTau
    {        
        public short LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public short CongTacID { get; set; }
    }
    public partial class DMMacTau
    {       
        public string MacTauID { get; set; }
        public short? LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
    }
    public partial class MacTau
    {
        public string MacTauID { get; set; }
        public short? LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public short CongTacID { get; set; }
        public string CongTacName { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class TinhChat
    {
        public short TinhChatId { get; set; }
        public string TinhChatName { get; set; }
    }
    public partial class Tuyen
    {
        public string TuyenID { get; set; }
        public short TuyenMap { get; set; }
        public string TuyenName { get; set; }
        public bool Active { get; set; }
    }
    public partial class TuyenMap
    {
        public short TuyenId { get; set; }
        public string TuyenName { get; set; }
    }   
    public partial class CongLenhSK
    {      
        public long ID { get; set; }
        public string TuyenID { get; set; }
        public string KhuDoan { get; set; }
        public decimal? DocHC { get; set; }
        public decimal? D4H { get; set; }
        public decimal? D5H { get; set; }
        public decimal? D8E { get; set; }
        public decimal? D9E { get; set; }
        public decimal? D10H { get; set; }
        public decimal? D11H { get; set; }
        public decimal? D12E { get; set; }
        public decimal? D13E { get; set; }
        public decimal? D14Er { get; set; }
        public decimal? D18E { get; set; }
        public decimal? D19E { get; set; }
        public decimal? D19Er { get; set; }
        public decimal? D20E { get; set; }
        public string GhiChu { get; set; }
        public int? GaXP { get; set; }
        public decimal? KmXP { get; set; }
        public int? GaKT { get; set; }
        public decimal? KmKT { get; set; }        
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public class MienPhat
    {
        public long CoBaoID { get; set; }
        public string SoCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public decimal TyLe { get; set; }
        public string LyDo { get; set; }
        public string MaDV { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class PhienBan
    {      
        public string ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface ICBService
{
    [OperationContract]
    bool TestService();

    #region Lookup Service
    [OperationContract]
    DataSet GetLookupDS();
    #endregion

    #region CoBao Service
    [OperationContract]
    List<CoBaoInfo> GetCoBaoList(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe);
    [OperationContract]
    DataSet GetCoBaoDS(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe);
    [OperationContract]
    DataSet GetCoBaoCTDS(long cobaoID);
    [OperationContract]
    OperationStatus SaveCoBao(CoBaoInfo cbInfo, DoanThongInfo dtInfo, DataTable cbCT, List<DoanThongCTInfo> dtCT, bool addNew);    
    [OperationContract]
    OperationStatus DeleteCoBao(CoBaoInfo cbInfo, string nguoiTH);
    #endregion

    #region DoanThong Service
    [OperationContract]
    DataSet GetDoanThongDS(int thangDT, int namDT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe);
    [OperationContract]
    DataSet GetDoanThongCTDS(long cobaoID);
    [OperationContract]
    DataSet GetThanhTichDS(long cobaoID);
    [OperationContract]
    string GetTopDoanThong(long cobaoID);
    [OperationContract]
    DataSet GetGioQVDS(int thangDT, int namDT);
    [OperationContract]
    DataSet GetGioDTDS(int thangDT, int namDT);
    [OperationContract]
    DataSet GetVanTocDS(int thangDT, int namDT);
    [OperationContract]
    string CreateTableDB(string tableName, string strCriteria);
    [OperationContract]
    string InsertTableDB(string tableName, DataTable dt);
    [OperationContract]
    OperationStatus DeleteDoanThong(string tableName, int thangDT, int namDT);
    [OperationContract]
    OperationStatus KhoaDT(string tableName, int thangDT, int namDT, bool khoaDT);
    #endregion

    #region Nhan vien Service
    [OperationContract]
    List<NhanVienInfo> GetNhanVienList(string crit, short critType, bool activeOnly);
    [OperationContract]
    NhanVienInfo GetNhanVien(string maNV, string password);
    [OperationContract]
    OperationStatus InsertNhanVien(NhanVienInfo nv);
    [OperationContract]
    OperationStatus UpdateNhanVien(NhanVienInfo nv);
    [OperationContract]
    OperationStatus DeleteNhanVien(string maNV);
    [OperationContract]
    OperationStatus ChangePWD(string maNV, string matKhau);
    #endregion

    #region HeSoQDNL Service
    [OperationContract]
    List<HeSoQDNLInfo> GetHeSoQDNLList(short MaDV);
    [OperationContract]
    HeSoQDNLInfo GetHeSoQDNL(short MaDV, int Thang, int Nam);
    [OperationContract]
    DataSet GetHSQDNLDS(int maDV, string cacThang, int nam);

    [OperationContract]
    OperationStatus InsertHSQDNL(HeSoQDNLInfo hs);
    [OperationContract]
    OperationStatus UpdateHSQDNL(HeSoQDNLInfo hs);
    [OperationContract]
    OperationStatus DeleteHSQDNL(HeSoQDNLInfo hs);
    #endregion

    #region DinhMucNL Service
    [OperationContract]
    List<DinhMucNLInfo> GetDinhMucNLList(string tableName, short MaDV, int Thang, int Nam);
    #endregion

    #region DauMay Service
    [OperationContract]
    List<DMDauMayInfo> GetDMDauMayList(string dauMay, string loaiMay);    
    [OperationContract]
    OperationStatus InsertDMDauMay(DMDauMayInfo dm);
    [OperationContract]
    OperationStatus UpdateDMDauMay(DMDauMayInfo dm);
    [OperationContract]
    OperationStatus DeleteDMDauMay(DMDauMayInfo dm, string nguoiTH);
    #endregion

    #region TaiXe Service
    [OperationContract]
    List<DMTaiXeInfo> GetDMTaiXeList(string taiXe, string chucDanh, string doiMay);
    [OperationContract]
    OperationStatus InsertDMTaiXe(DMTaiXeInfo dm);
    [OperationContract]
    OperationStatus UpdateDMTaiXe(DMTaiXeInfo dm);
    [OperationContract]
    OperationStatus DeleteDMTaiXe(DMTaiXeInfo dm, string nguoiTH);
    #endregion

    #region MacTau Service
    [OperationContract]
    List<DMMacTauInfo> GetDMMacTauList(string macTau, string congTac);
    [OperationContract]
    OperationStatus InsertDMMacTau(DMMacTauInfo dm);
    [OperationContract]
    OperationStatus UpdateDMMacTau(DMMacTauInfo dm);
    [OperationContract]
    OperationStatus DeleteDMMacTau(DMMacTauInfo dm, string nguoiTH);
    #endregion

    #region BaoCao Service
    [OperationContract]
    DataSet GetBCVanDungDS(int thangDT, int namDT, string loaiMay, int tuyen);
       
    [OperationContract]
    DataSet GetBCDauMoDS(int thangDT, int namDT, string loaiDauMo);

    [OperationContract]
    DataSet GetBCTinhLuongDS(int thangDT, int namDT);

    [OperationContract]
    DataSet GetBCGioDonDS(int thangDT, int namDT);    

    [OperationContract]
    DataSet GetBCKTKTDS(string tableName, int thangDT, int namDT, int khoDuong, string loaiMay, int tuyen);

    [OperationContract]
    DataSet GetBCTongHopDS(string tableName, int thangDT, int namDT, string loaiMay);

    [OperationContract]
    DataSet GetBCSPCDDS(string tableName, int thangDT, int namDT, string loaiMay);

    [OperationContract]
    DataSet GetBCTHSPTNDS(string tableName, int thangDT, int namDT);
    #endregion

    #region HeThong Service
    [OperationContract]
    DataSet GetNhatKyDS(DateTime ngayBD, DateTime ngayKT, string tenBang, string nhanVien);    
    #endregion
}


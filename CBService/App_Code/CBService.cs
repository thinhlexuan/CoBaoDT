using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class CBService : ICBService
{
    public bool TestService()
    {
        return true;
    }

    #region Lookup Service    
    public DataSet GetLookupDS()
    {
        LookupDB db = new LookupDB();
        return db.GetLookupDS();
    }   
    #endregion     

    #region CoBao Service
    public List<CoBaoInfo> GetCoBaoList(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        CoBaoDB db = new CoBaoDB();
        return db.GetCoBaoList(loaiTT, ngayBD, ngayKT, loaiMay, shCoBao, shDauMay, shTaiXe);
    }
    public DataSet GetCoBaoDS(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        CoBaoDB db = new CoBaoDB();
        return db.GetCoBaoDS(loaiTT, ngayBD, ngayKT,loaiMay,shCoBao,shDauMay,shTaiXe);
    }
    public DataSet GetCoBaoCTDS(long cobaoID)
    {
        CoBaoDB db = new CoBaoDB();
        return db.GetCoBaoCTDS(cobaoID);
    }
    public OperationStatus SaveCoBao(CoBaoInfo cbInfo, DoanThongInfo dtInfo, DataTable cbCT, List<DoanThongCTInfo> dtCT,bool addNew)
    {
        CoBaoDB db = new CoBaoDB();
        return db.SaveCoBao(cbInfo,dtInfo,cbCT, dtCT,addNew);
    }   
    public OperationStatus DeleteCoBao(CoBaoInfo cbInfo, string nguoiTH)
    {
        CoBaoDB db = new CoBaoDB();
        return db.DeleteCoBao(cbInfo,nguoiTH);
    }
    #endregion

    #region DoanThong Service
    public DataSet GetDoanThongDS(int thangDT, int namDT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetDoanThongDS(thangDT, namDT, loaiMay, shCoBao, shDauMay, shTaiXe);
    }
    public DataSet GetDoanThongCTDS(long cobaoID)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetDoanThongCTDS(cobaoID);
    }
    public DataSet GetThanhTichDS(long cobaoID)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetThanhTichDS(cobaoID);
    }
    public string GetTopDoanThong(long cobaoID)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetTopDoanThong(cobaoID);
    }
    public DataSet GetGioQVDS(int thangDT, int namDT)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetGioQVDS(thangDT, namDT);
    }
    public DataSet GetGioDTDS(int thangDT, int namDT)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetGioDTDS(thangDT, namDT);
    }
    public DataSet GetVanTocDS(int thangDT, int namDT)
    {
        DoanThongDB db = new DoanThongDB();
        return db.GetVanTocDS(thangDT, namDT);
    }
    
    public string CreateTableDB(string tableName, string strCriteria)
    {
        DoanThongDB db = new DoanThongDB();
        return db.CreateTableDB(tableName, strCriteria);
    }
    public string InsertTableDB(string tableName, DataTable dt)
    {
        DoanThongDB db = new DoanThongDB();
        return db.InsertTableDB(tableName, dt);
    }
    public OperationStatus DeleteDoanThong(string tableName, int thangDT, int namDT)
    {
        DoanThongDB db = new DoanThongDB();
        return db.DeleteDoanThong(tableName, thangDT, namDT);
    }
   
    public OperationStatus KhoaDT(string tableName, int thangDT, int namDT,bool khoaDT)
    {
        DoanThongDB db = new DoanThongDB();
        return db.KhoaDT(tableName, thangDT,namDT,khoaDT);
    }
    #endregion

    #region Nhan vien Service
    public List<NhanVienInfo> GetNhanVienList(string crit, short critType, bool activeOnly)
    {
        NhanVienDB db = new NhanVienDB();
        return db.GetNhanVienList(crit, critType, activeOnly);
    }

    public NhanVienInfo GetNhanVien(string maNV, string password)
    {
        NhanVienDB db = new NhanVienDB();
        return db.GetNhanVien(maNV, password);
    }

    public OperationStatus InsertNhanVien(NhanVienInfo nv)
    {
        NhanVienDB db = new NhanVienDB();
        return db.InsertNhanVien(nv);
    }

    public OperationStatus UpdateNhanVien(NhanVienInfo nv)
    {
        NhanVienDB db = new NhanVienDB();
        return db.UpdateNhanVien(nv);
    }

    public OperationStatus DeleteNhanVien(string maNV)
    {
        NhanVienDB db = new NhanVienDB();
        return db.DeleteNhanVien(maNV);
    }
    public OperationStatus ChangePWD(string maNV, string matKhau)
    {
        NhanVienDB db = new NhanVienDB();
        return db.ChangePWD(maNV, matKhau);
    }

    #endregion

    #region HeSoQDNL Service
    public List<HeSoQDNLInfo> GetHeSoQDNLList(short MaDV)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.GetHeSoQDNLList(MaDV);
    }

    public HeSoQDNLInfo GetHeSoQDNL(short MaDV, int Thang,int Nam)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.GetHeSoQDNL(MaDV, Thang,Nam);
    }
    public DataSet GetHSQDNLDS(int maDV, string cacThang, int nam)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.GetHSQDNLDS(maDV, cacThang, nam);
    }
    public OperationStatus InsertHSQDNL(HeSoQDNLInfo hs)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.InsertHSQDNL(hs);
    }

    public OperationStatus UpdateHSQDNL(HeSoQDNLInfo hs)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.UpdateHSQDNL(hs);
    }

    public OperationStatus DeleteHSQDNL(HeSoQDNLInfo hs)
    {
        HeSoQDNLDB db = new HeSoQDNLDB();
        return db.DeleteHSQDNL(hs);
    }
    #endregion

    #region DinhMucNL Service
    public List<DinhMucNLInfo> GetDinhMucNLList(string tableName, short MaDV, int Thang, int Nam)
    {
        DinhMucNLDB db = new DinhMucNLDB();
        return db.GetDinhMucNLList(tableName,MaDV,Thang,Nam);
    }
    #endregion

    #region DauMay Service
    public List<DMDauMayInfo> GetDMDauMayList(string dauMay, string loaiMay)
    {
        DanhMucDB db = new DanhMucDB();
        return db.GetDMDauMayList(dauMay,loaiMay);
    }
    public OperationStatus InsertDMDauMay(DMDauMayInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.InsertDMDauMay(dm);
    }
    public OperationStatus UpdateDMDauMay(DMDauMayInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.UpdateDMDauMay(dm);
    }
    public OperationStatus DeleteDMDauMay(DMDauMayInfo dm, string nguoiTH)
    {
        DanhMucDB db = new DanhMucDB();
        return db.DeleteDMDauMay(dm,nguoiTH);
    }
    #endregion

    #region TaiXe Service
    public List<DMTaiXeInfo> GetDMTaiXeList(string taiXe, string chucDanh, string doiMay)
    {
        DanhMucDB db = new DanhMucDB();
        return db.GetDMTaiXeList(taiXe, chucDanh, doiMay);
    }
    public OperationStatus InsertDMTaiXe(DMTaiXeInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.InsertDMTaiXe(dm);
    }
    public OperationStatus UpdateDMTaiXe(DMTaiXeInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.UpdateDMTaiXe(dm);
    }
    public OperationStatus DeleteDMTaiXe(DMTaiXeInfo dm, string nguoiTH)
    {
        DanhMucDB db = new DanhMucDB();
        return db.DeleteDMTaiXe(dm, nguoiTH);
    }
    #endregion

    #region MacTau Service
    public List<DMMacTauInfo> GetDMMacTauList(string macTau, string congTac)
    {
        DanhMucDB db = new DanhMucDB();
        return db.GetDMMacTauList(macTau, congTac);
    }
    public OperationStatus InsertDMMacTau(DMMacTauInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.InsertDMMacTau(dm);
    }
    public OperationStatus UpdateDMMacTau(DMMacTauInfo dm)
    {
        DanhMucDB db = new DanhMucDB();
        return db.UpdateDMMacTau(dm);
    }
    public OperationStatus DeleteDMMacTau(DMMacTauInfo dm, string nguoiTH)
    {
        DanhMucDB db = new DanhMucDB();
        return db.DeleteDMMacTau(dm, nguoiTH);
    }
    #endregion

    #region BaoCao Service
    public DataSet GetBCVanDungDS(int thangDT, int namDT, string loaiMay, int tuyen)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCVanDungDS(thangDT, namDT, loaiMay,tuyen);
    }

    public DataSet GetBCDauMoDS(int thangDT, int namDT, string loaiDauMo)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCDauMoDS(thangDT, namDT,loaiDauMo);
    }

    public DataSet GetBCTinhLuongDS(int thangDT, int namDT)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCTinhLuongDS(thangDT, namDT);
    }

    public DataSet GetBCGioDonDS(int thangDT, int namDT)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCGioDonDS(thangDT, namDT);
    }
    public DataSet GetBCKTKTDS(string tableName, int thangDT, int namDT, int khoDuong, string loaiMay, int tuyen)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCKTKTDS(tableName, thangDT, namDT, khoDuong, loaiMay, tuyen);
    }

    public DataSet GetBCTongHopDS(string tableName, int thangDT, int namDT, string loaiMay)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCTongHopDS(tableName, thangDT, namDT,loaiMay);
    }
    public DataSet GetBCSPCDDS(string tableName, int thangDT, int namDT, string loaiMay)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCSPCDDS(tableName, thangDT, namDT,loaiMay);
    }
    public DataSet GetBCTHSPTNDS(string tableName, int thangDT, int namDT)
    {
        BaoCaoDB db = new BaoCaoDB();
        return db.GetBCTHSPTNDS(tableName, thangDT, namDT);
    }

    #endregion

    #region HeThong Service
    public DataSet GetNhatKyDS(DateTime ngayBD,DateTime ngayKT, string tenBang, string nhanVien)
    {
        HeThongDB db = new HeThongDB();
        return db.GetNhatKyDS(ngayBD,ngayKT,tenBang,nhanVien);
    }  
    #endregion

}

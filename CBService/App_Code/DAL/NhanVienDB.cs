using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for NhanVienDB
/// </summary>
public class NhanVienDB
{
    public List<NhanVienInfo> GetNhanVienList(string crit, short critType, bool activeOnly)
    {
        List<NhanVienInfo> list = new List<NhanVienInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT NV.MaNV,NV.TenNV,NV.MatKhau,NV.MaQH,QH.TenQH,NV.MaDV,DV.TenDV,DV.MaCha,NV.PhongBan,NV.Active ";
                commandText += "FROM NhanVien NV ";
                commandText += "INNER JOIN QuyenHan QH ON NV.MaQH=QH.MaQH ";
                commandText += "INNER JOIN DonVi DV ON NV.MaDV=DV.MaDV ";
                string whereText = string.Empty;
                if (critType == 1 && !string.IsNullOrEmpty(crit)) //Theo don vi
                    whereText = "WHERE NV.MaDV=@Crit " + (activeOnly ? "AND Active=1" : "");
                else if (critType == 2 && !string.IsNullOrEmpty(crit)) //Theo don vi
                    whereText = "WHERE NV.TenNV LIKE @Crit " + (activeOnly ? "AND Active=1" : "");
                else
                    whereText = (activeOnly ? "WHERE Active=1" : "");
                commandText += whereText;
                if (commandText.Contains('@'))
                    db.AddParameter("@Crit", crit);
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    NhanVienInfo info = new NhanVienInfo();
                    info.MaNV = dr["MaNV"].ToString();
                    info.TenNV = dr["TenNV"].ToString();
                    info.MatKhau = dr["MatKhau"].ToString();
                    info.MaQH = Convert.ToInt16(dr["MaQH"]);
                    info.TenQH = dr["TenQH"].ToString();
                    info.MaDV = Convert.ToInt16(dr["MaDV"]);
                    info.TenDV = dr["TenDV"].ToString();
                    info.MaCha = Convert.ToInt16(dr["MaCha"]);
                    info.PhongBan = dr["PhongBan"].ToString();
                    info.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(info);
                }
            }
        }
        catch
        {
        }
        finally
        {
            if (dr != null && !dr.IsClosed)
                dr.Close();
        }
        return list;
    }

    public NhanVienInfo GetNhanVien(string maNV, string password)
    {
        NhanVienInfo info = null;        
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT NV.MaNV,NV.TenNV,NV.MatKhau,NV.MaQH,QH.TenQH,NV.MaDV,DV.TenDV,DV.MaCha,NV.PhongBan,NV.Active ";
                commandText += "FROM NhanVien NV ";
                commandText += "INNER JOIN QuyenHan QH ON NV.MaQH=QH.MaQH ";
                commandText += "INNER JOIN DonVi DV ON NV.MaDV=DV.MaDV ";
                commandText += "WHERE MaNV=@MaNV ";
                db.AddParameter("@MaNV", maNV);
                if (!string.IsNullOrEmpty(password))
                {
                    commandText += "AND MatKhau=@Password";
                    db.AddParameter("@Password", password);
                }
                dr = db.ExecuteReader(commandText);
                if (dr.Read())
                {
                    info = new NhanVienInfo();
                    info.MaNV = dr["MaNV"].ToString();
                    info.TenNV = dr["TenNV"].ToString();
                    info.MatKhau = dr["MatKhau"].ToString();
                    info.MaQH = Convert.ToInt16(dr["MaQH"]);
                    info.TenQH = dr["TenQH"].ToString();
                    info.MaDV =  Convert.ToInt16(dr["MaDV"]);
                    info.TenDV = dr["TenDV"].ToString();
                    info.MaCha = Convert.ToInt16(dr["MaCha"]);
                    info.PhongBan = dr["PhongBan"].ToString();
                    info.Active = Convert.ToBoolean(dr["Active"]);
                }
            }
        }
        catch
        {
        }
        finally
        {
            if (dr != null && !dr.IsClosed)
                dr.Close();
        }
        return info;
    }

    public OperationStatus InsertNhanVien(NhanVienInfo nv)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "INSERT INTO NhanVien(MaNV,TenNV,MatKhau,MaQH,MaDV,PhongBan,Active) ";
                commandText += "VALUES(@MaNV,@TenNV,@MatKhau,@MaQH,@MaDV,@PhongBan,1) ";
                db.AddParameter("@MaNV", nv.MaNV);
                if (db.ExecuteScalar("SELECT TOP 1 MaNV FROM NhanVien WHERE MaNV=@MaNV") != null)
                {
                    opStatus.IsSuccess = false;
                    opStatus.Message = "Đã có mã nhân viên này";
                }
                else
                {
                    db.AddParameter("@TenNV", nv.TenNV);
                    db.AddParameter("@MatKhau", nv.MatKhau);
                    db.AddParameter("@MaQH", nv.MaQH);
                    db.AddParameter("@MaDV", nv.MaDV);
                    db.AddParameter("@PhongBan", nv.PhongBan);
                    int recsAffected = db.ExecuteNonQuery(commandText);
                    opStatus.IsSuccess = (recsAffected == 1);
                }
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }

    public OperationStatus UpdateNhanVien(NhanVienInfo nv)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE NhanVien SET TenNV=@TenNV,MatKhau=@MatKhau,MaQH=@MaQH,MaDV=@MaDV,PhongBan=@PhongBan,Active=@Active ";
                commandText += "WHERE MaNV=@MaNV";
                db.AddParameter("@MaNV", nv.MaNV);
                db.AddParameter("@TenNV", nv.TenNV);
                db.AddParameter("@MatKhau", nv.MatKhau);
                db.AddParameter("@MaQH", nv.MaQH);
                db.AddParameter("@MaDV", nv.MaDV);
                db.AddParameter("@PhongBan", nv.PhongBan);
                db.AddParameter("@Active", nv.Active);
                int recsAffected = db.ExecuteNonQuery(commandText);
                opStatus.IsSuccess = (recsAffected == 1);
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }

    public OperationStatus DeleteNhanVien(string maNV)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                //string commandText = "SELECT TOP 1 MaNV FROM DuLieu.VanDon WHERE MaNV=@MaNV";
                //db.AddParameter("@MaNV", maNV);
                //object checkMaNV = db.ExecuteScalar(commandText);
                //if (checkMaNV != null)
                //    throw new Exception("Lỗi đã có nhân viên này trong vận đơn.");

                string commandText = "DELETE FROM NhanVien WHERE MaNV=@MaNV";
                int recsAffected = db.ExecuteNonQuery(commandText);
                opStatus.IsSuccess = (recsAffected == 1);
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }

    public OperationStatus ChangePWD(string maNV, string matKhau)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE NhanVien SET MatKhau=@MatKhau WHERE MaNV=@MaNV";
                db.AddParameter("@MaNV", maNV);
                db.AddParameter("@MatKhau", matKhau);
                int recsAffected = db.ExecuteNonQuery(commandText);
                opStatus.IsSuccess = (recsAffected == 1);
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }

}
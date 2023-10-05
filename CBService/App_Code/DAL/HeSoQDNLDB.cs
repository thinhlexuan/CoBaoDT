using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HeSoQDNLDB
/// </summary>
public class HeSoQDNLDB
{
    public List<HeSoQDNLInfo> GetHeSoQDNLList(short MaDV)
    {
        List<HeSoQDNLInfo> list = new List<HeSoQDNLInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT HS.MaDV,DV.TenDV,HS.Thang,HS.Nam,HS.HesoLit,HS.HesoKg,HS.NhietDo ";
                commandText += "FROM HeSoQDNL HS INNER JOIN DonVi DV ON HS.MaDV=DV.MaDV ";               
                if (MaDV > 0)
                {
                    commandText = "WHERE HS.MaDV=@MaDV";
                    db.AddParameter("@MaDV", MaDV);
                }
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    HeSoQDNLInfo info = new HeSoQDNLInfo();
                    info.MaDV = Convert.ToInt16(dr["MaDV"]);
                    info.TenDV = dr["TenDV"].ToString();
                    info.Thang = Convert.ToInt32(dr["Thang"]);
                    info.Nam = Convert.ToInt32(dr["Nam"]);
                    info.HeSoLit = Convert.ToDecimal(dr["HesoLit"]);
                    info.HeSoKg = Convert.ToDecimal(dr["HesoKg"]);
                    info.NhietDo = Convert.ToDecimal(dr["NhietDo"]);
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

    public HeSoQDNLInfo GetHeSoQDNL(short MaDV,int Thang,int Nam)
    {
        HeSoQDNLInfo info = null;
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT HS.MaDV,DV.TenDV,HS.Thang,HS.Nam,HS.HesoLit,HS.HesoKg,HS.NhietDo ";
                commandText += "FROM HeSoQDNL HS INNER JOIN DonVi DV ON HS.MaDV=DV.MaDV "; 
                commandText += "WHERE HS.MaDV=@MaDV AND HS.Thang=@Thang AND HS.Nam=@Nam";
                db.AddParameter("@MaDV", MaDV);
                db.AddParameter("@Thang", Thang);
                db.AddParameter("@Nam", Nam);
                dr = db.ExecuteReader(commandText);
                if (dr.Read())
                {   
                    info = new HeSoQDNLInfo();
                    info.MaDV = Convert.ToInt16(dr["MaDV"]);
                    info.TenDV = dr["TenDV"].ToString();
                    info.Thang = Convert.ToInt32(dr["Thang"]);
                    info.Nam = Convert.ToInt32(dr["Nam"]);
                    info.HeSoLit = Convert.ToDecimal(dr["HesoLit"]);
                    info.HeSoKg = Convert.ToDecimal(dr["HesoKg"]);
                    info.NhietDo = Convert.ToDecimal(dr["NhietDo"]);
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

    public DataSet GetHSQDNLDS(int maDV, string cacThang, int nam)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT hs.MaDV,dv.TenDV,hs.Thang,hs.Nam,hs.HesoLit,hs.HesoKg,hs.NhietDo from HeSoQDNL hs inner join DonVi dv on hs.MaDV=dv.MaDV"
                + " WHERE hs.Thang in (" + cacThang + ") AND hs.Nam=@Nam AND hs.MaDV=@MaDV order by thang,nam";
                db.AddParameter("@MaDV", maDV);
                //db.AddParameter("@Thang", cacThang);
                db.AddParameter("@Nam", nam);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }

    public OperationStatus InsertHSQDNL(HeSoQDNLInfo hs)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "INSERT INTO HeSoQDNL(MaDV,Thang,Nam,HesoLit,HesoKg,NhietDo) ";
                commandText += "VALUES(@MaDV,@Thang,@Nam,@HesoLit,@HesoKg,@NhietDo) ";
                db.AddParameter("@MaDV", hs.MaDV);
                db.AddParameter("@Thang", hs.Thang);
                db.AddParameter("@Nam", hs.Nam);
                db.AddParameter("@HesoLit", hs.HeSoLit);
                db.AddParameter("@HesoKg", hs.HeSoKg);
                db.AddParameter("@NhietDo", hs.NhietDo);
                if (db.ExecuteScalar("SELECT TOP 1 MaDV FROM HeSoQDNL WHERE MaDV=@MaDV AND Thang=@Thang AND Nam=@Nam") != null)
                {
                    opStatus.IsSuccess = false;
                    opStatus.Message = "Đã có hệ số tháng này";
                }
                else
                {

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

    public OperationStatus UpdateHSQDNL(HeSoQDNLInfo hs)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE HeSoQDNL SET HesoLit=@HesoLit,HesoKg=@HesoKg,NhietDo=@NhietDo ";
                commandText += "WHERE MaDV=@MaDV AND Thang=@Thang AND Nam=@Nam";
                db.AddParameter("@MaDV", hs.MaDV);
                db.AddParameter("@Thang", hs.Thang);
                db.AddParameter("@Nam", hs.Nam);
                db.AddParameter("@HesoLit", hs.HeSoLit);
                db.AddParameter("@HesoKg", hs.HeSoKg);
                db.AddParameter("@NhietDo", hs.NhietDo);
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
    
    public OperationStatus DeleteHSQDNL(HeSoQDNLInfo hs)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "DELETE FROM HeSoQDNL WHERE MaDV=@MaDV AND Thang=@Thang AND Nam=@Nam";
                db.AddParameter("@MaDV", hs.MaDV);
                db.AddParameter("@Thang", hs.Thang);
                db.AddParameter("@Nam", hs.Nam);                
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
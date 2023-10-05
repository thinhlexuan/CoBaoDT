using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DinhMucNLDB
/// </summary>
public class DinhMucNLDB
{
    public List<DinhMucNLInfo> GetDinhMucNLList(string tableName, short MaDV, int Thang, int Nam)
    {
        List<DinhMucNLInfo> list = new List<DinhMucNLInfo>();
        IDataReader dr = null;
        DateTime ngayKT = Thang == 12 ? new DateTime(Nam + 1, 1, 1) : new DateTime(Nam, Thang + 1, 1);
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT DM.MaDV,DV.TenDV,DM.MaCT,DM.LoaiMayID,DM.ThoiDB,DM.DVTinh,DM.DMLit15,DM.NgayHL"
                + " FROM " + tableName + " DM INNER JOIN DonVi DV ON DM.MaDV=DV.MaDV WHERE DM.NgayHL<@NgayKT";
                if (MaDV > 0)
                {
                    commandText += " AND DM.MaDV=@MaDV";
                    db.AddParameter("@MaDV", MaDV);
                }
                db.AddParameter("@NgayKT", ngayKT);
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    DinhMucNLInfo info = new DinhMucNLInfo();
                    info.MaDV = Convert.ToInt16(dr["MaDV"]);
                    info.TenDV = dr["TenDV"].ToString();
                    info.MaCT = Convert.ToInt16(dr["MaCT"]);
                    info.LoaiMayID = dr["LoaiMayID"].ToString();
                    info.ThoiDB = dr["ThoiDB"].ToString();
                    info.DVTinh = dr["DVTinh"].ToString();
                    info.DMLit15 = Convert.ToDecimal(dr["DMLit15"]);
                    info.NgayHL = Convert.ToDateTime(dr["NgayHL"]);
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
       
}
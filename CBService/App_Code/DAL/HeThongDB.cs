using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HeThongDB
/// </summary>
public class HeThongDB
{
    public DataSet GetNhatKyDS(DateTime ngayBD, DateTime ngayKT, string tenBang, string nhanVien)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT NK.TenBang,NK.NoiDung,NK.NgayTH,NK.NguoiTH,NV.TenNV FROM NhatKyVI NK INNER JOIN NhanVien NV ON NK.NguoiTH=NV.MaNV";                
                commandText += " WHERE NK.NgayTH>=@NgayBD AND NK.NgayTH<@NgayKT";
                if (tenBang != "ALL")
                {
                    commandText += " AND NK.TenBang=@TenBang";
                    db.AddParameter("@TenBang", tenBang);
                }
                if (!string.IsNullOrWhiteSpace(nhanVien))
                    commandText += " AND NK.NguoiTH LIKE '%" + nhanVien + "%'";
                
                db.AddParameter("@NgayBD", ngayBD);
                db.AddParameter("@NgayKT", ngayKT);                
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
}
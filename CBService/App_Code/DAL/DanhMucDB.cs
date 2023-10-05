using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DanhMucDB
/// </summary>
public class DanhMucDB
{
    #region DauMay
    public List<DMDauMayInfo> GetDMDauMayList(string dauMay, string loaiMay)
    {
        List<DMDauMayInfo> list = new List<DMDauMayInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT DM.DauMayID,DM.LoaiMayID,LM.LoaiMayName,DM.TramID,TR.TramName,DM.GhiChu,DM.Active";
                commandText += " FROM DauMay DM INNER JOIN LoaiMay LM ON DM.LoaiMayID=LM.LoaiMayID";
                commandText += " INNER JOIN Tram TR ON DM.TramID=TR.TramID";
                if (loaiMay != "ALL")
                {
                    commandText += " WHERE DM.LoaiMayID=@LoaiMayID";
                    db.AddParameter("@LoaiMayID", loaiMay);
                    if(!string.IsNullOrWhiteSpace(dauMay))
                    {
                        commandText += " AND DM.DauMayID in ("+dauMay+")";                       
                    }
                }
                else if (!string.IsNullOrWhiteSpace(dauMay))
                {
                    commandText += " WHERE DM.DauMayID in (" + dauMay + ")";                   
                }
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    DMDauMayInfo info = new DMDauMayInfo();
                    info.DauMayID = dr["DauMayID"].ToString();
                    info.LoaiMayID = dr["LoaiMayID"].ToString();
                    info.LoaiMayName = dr["LoaiMayName"].ToString();
                    info.TramID = dr["TramID"].ToString();
                    info.TramName = dr["TramName"].ToString();
                    info.GhiChu = dr["GhiChu"].ToString();
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
    public OperationStatus InsertDMDauMay(DMDauMayInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "INSERT INTO DauMay(DauMayID,LoaiMayID,TramID,GhiChu,Active) ";
                commandText += "VALUES(@DauMayID,@LoaiMayID,@TramID,@GhiChu,@Active) ";
                db.AddParameter("@DauMayID", dm.DauMayID);
                db.AddParameter("@LoaiMayID", dm.LoaiMayID);
                db.AddParameter("@TramID", dm.TramID);
                db.AddParameter("@GhiChu", dm.GhiChu);
                db.AddParameter("@Active", dm.Active);                
                if (db.ExecuteScalar("SELECT TOP 1 DauMayID FROM DauMay WHERE DauMayID=@DauMayID") != null)
                {
                    throw new Exception("Đã có đầu máy này");
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
    public OperationStatus UpdateDMDauMay(DMDauMayInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE DauMay SET LoaiMayID=@LoaiMayID,TramID=@TramID,GhiChu=@GhiChu,Active=@Active";
                commandText += " WHERE DauMayID=@DauMayID";
                db.AddParameter("@DauMayID", dm.DauMayID);
                db.AddParameter("@LoaiMayID", dm.LoaiMayID);
                db.AddParameter("@TramID", dm.TramID);
                db.AddParameter("@GhiChu", dm.GhiChu);
                db.AddParameter("@Active", dm.Active);  
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
    public OperationStatus DeleteDMDauMay(DMDauMayInfo dm,string nguoiTH)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        string strConn = new DBAccess().ConnectionString;
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    string commandText = "SELECT TOP 1 DauMayID FROM CoBaoVI WHERE DauMayID=@DauMayID";                
                    SqlParameter param = new SqlParameter("@DauMayID", dm.DauMayID); cmd.Parameters.Add(param);
                    cmd.CommandText = commandText;
                    if (cmd.ExecuteScalar() != null)
                        throw new Exception("Đã có đầu máy này trong bảng cơ báo");                                           
                    else
                    {
                        commandText = "DELETE FROM DauMay WHERE DauMayID=@DauMayID";
                        cmd.CommandText = commandText;
                        int recsAffected = cmd.ExecuteNonQuery();
                        opStatus.IsSuccess = (recsAffected == 1);
                        //Ghi nhật ký
                        string noiDung = "Xóa đầu máy: " + dm.DauMayID + " loại máy: " + dm.LoaiMayID + "-" + dm.LoaiMayName + " trạm: " + dm.TramID + "-" + dm.TramName;
                        commandText = "INSERT INTO NhatKyVI(TenBang,NoiDung,NgayTH,NguoiTH) VALUES(@TenBang,@NoiDung,@NgayTH,@NguoiTH)";
                        cmd.Parameters.Clear();
                        param = new SqlParameter("@TenBang", "DauMay"); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NoiDung", noiDung); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NgayTH", DateTime.Now); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NguoiTH", nguoiTH); cmd.Parameters.Add(param);
                        cmd.CommandText = commandText;
                        recsAffected = cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    opStatus = OperationStatus.CreateFromException(ex);
                }
            }
        }
        return opStatus;
    }
    #endregion

    #region TaiXe
    public List<DMTaiXeInfo> GetDMTaiXeList(string taiXe, string chucDanh, string doiMay)
    {
        List<DMTaiXeInfo> list = new List<DMTaiXeInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT TaiXeID,TaiXeName,ChucDanh,DoiMay,Active FROM TaiXe";                
                if (doiMay != "ALL")
                {
                    commandText += " WHERE DoiMay=@DoiMay";
                    db.AddParameter("@DoiMay", doiMay);
                    if (chucDanh != "ALL")
                    {
                        commandText += " AND ChucDanh=@ChucDanh";
                        db.AddParameter("@ChucDanh", chucDanh);
                    }
                    if (!string.IsNullOrWhiteSpace(taiXe))
                    {
                        commandText += " AND TaiXeID in (" + taiXe + ")";
                    }
                }
                else if (chucDanh != "ALL")
                {
                    commandText += " WHERE ChucDanh=@ChucDanh";
                    db.AddParameter("@ChucDanh", chucDanh);
                    if (!string.IsNullOrWhiteSpace(taiXe))
                    {
                        commandText += " AND TaiXeID in (" + taiXe + ")";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(taiXe))
                {
                    commandText += " WHERE TaiXeID in (" + taiXe + ")";
                }
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    DMTaiXeInfo info = new DMTaiXeInfo();
                    info.TaiXeID = dr["TaiXeID"].ToString();
                    info.TaiXeName = dr["TaiXeName"].ToString();
                    info.ChucDanh = dr["ChucDanh"].ToString();
                    info.DoiMay = dr["DoiMay"].ToString();                    
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
    public OperationStatus InsertDMTaiXe(DMTaiXeInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "INSERT INTO TaiXe(TaiXeID,TaiXeName,ChucDanh,DoiMay,Active) ";
                commandText += "VALUES(@TaiXeID,@TaiXeName,@ChucDanh,@DoiMay,@Active) ";
                db.AddParameter("@TaiXeID", dm.TaiXeID);
                db.AddParameter("@TaiXeName", dm.TaiXeName);
                db.AddParameter("@ChucDanh", dm.ChucDanh);
                db.AddParameter("@DoiMay", dm.DoiMay);
                db.AddParameter("@Active", dm.Active);
                if (db.ExecuteScalar("SELECT TOP 1 TaiXeID FROM TaiXe WHERE TaiXeID=@TaiXeID") != null)
                {
                    throw new Exception("Đã có tài xế này");
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
    public OperationStatus UpdateDMTaiXe(DMTaiXeInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "UPDATE TaiXe SET TaiXeName=@TaiXeName,ChucDanh=@ChucDanh,DoiMay=@DoiMay,Active=@Active";
                commandText += " WHERE TaiXeID=@TaiXeID";
                db.AddParameter("@TaiXeID", dm.TaiXeID);
                db.AddParameter("@TaiXeName", dm.TaiXeName);
                db.AddParameter("@ChucDanh", dm.ChucDanh);
                db.AddParameter("@DoiMay", dm.DoiMay);
                db.AddParameter("@Active", dm.Active);
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
    public OperationStatus DeleteDMTaiXe(DMTaiXeInfo dm, string nguoiTH)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        string strConn = new DBAccess().ConnectionString;
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    string commandText = "SELECT TOP 1 TaiXe1ID FROM CoBaoVI WHERE TaiXe1ID=@TaiXeID OR PhoXe1ID=@TaiXeID OR TaiXe2ID=@TaiXeID OR PhoXe2ID=@TaiXeID";
                    SqlParameter param = new SqlParameter("@TaiXeID", dm.TaiXeID); cmd.Parameters.Add(param);
                    cmd.CommandText = commandText;
                    if (cmd.ExecuteScalar() != null)
                        throw new Exception("Đã có tài xế này trong bảng cơ báo");
                    else
                    {
                        commandText = "DELETE FROM TaiXe WHERE TaiXeID=@TaiXeID";
                        cmd.CommandText = commandText;
                        int recsAffected = cmd.ExecuteNonQuery();
                        opStatus.IsSuccess = (recsAffected == 1);
                        //Ghi nhật ký
                        string noiDung = "Xóa tài xế: " + dm.TaiXeID + "-" + dm.TaiXeName + " chức danh: " + dm.ChucDanh + " đội máy: " + dm.DoiMay;
                        commandText = "INSERT INTO NhatKyVI(TenBang,NoiDung,NgayTH,NguoiTH) VALUES(@TenBang,@NoiDung,@NgayTH,@NguoiTH)";
                        cmd.Parameters.Clear();
                        param = new SqlParameter("@TenBang", "TaiXe"); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NoiDung", noiDung); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NgayTH", DateTime.Now); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NguoiTH", nguoiTH); cmd.Parameters.Add(param);
                        cmd.CommandText = commandText;
                        recsAffected = cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    opStatus = OperationStatus.CreateFromException(ex);
                }
            }
        }
        return opStatus;
    }
    #endregion

    #region MacTau
    public List<DMMacTauInfo> GetDMMacTauList(string macTau, string congTac)
    {
        List<DMMacTauInfo> list = new List<DMMacTauInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT DM.MacTauID,DM.CongTacID,CT.CongTacName,DM.Active";
                commandText += " FROM MacTauVI DM INNER JOIN CongTac CT ON DM.CongTacID=CT.CongTacID";                
                if (congTac != "0")
                {
                    commandText += " WHERE DM.CongTacID=@CongTacID";
                    db.AddParameter("@CongTacID", congTac);
                    if (!string.IsNullOrWhiteSpace(macTau))
                    {
                        commandText += " AND DM.MacTauID in (" + macTau + ")";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(macTau))
                {
                    commandText += " WHERE DM.MacTauID in (" + macTau + ")";
                }
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    DMMacTauInfo info = new DMMacTauInfo();
                    info.MacTauID = dr["MacTauID"].ToString();
                    info.CongTacID = dr["CongTacID"].ToString();
                    info.CongTacName = dr["CongTacName"].ToString();                    
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
    public OperationStatus InsertDMMacTau(DMMacTauInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "INSERT INTO MacTauVI(MacTauID,CongTacID,Active) ";
                commandText += "VALUES(@MacTauID,@CongTacID,@Active) ";
                db.AddParameter("@MacTauID", dm.MacTauID);
                db.AddParameter("@CongTacID", dm.CongTacID);                
                db.AddParameter("@Active", dm.Active);
                if (db.ExecuteScalar("SELECT TOP 1 MacTauID FROM MacTauVI WHERE MacTauID=@MacTauID") != null)
                {
                    throw new Exception("Đã có mác tầu này");
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
    public OperationStatus UpdateDMMacTau(DMMacTauInfo dm)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE MacTauVI SET CongTacID=@CongTacID,Active=@Active";
                commandText += " WHERE MacTauID=@MacTauID";
                db.AddParameter("@MacTauID", dm.MacTauID);
                db.AddParameter("@CongTacID", dm.CongTacID);
                db.AddParameter("@Active", dm.Active);
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
    public OperationStatus DeleteDMMacTau(DMMacTauInfo dm, string nguoiTH)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        string strConn = new DBAccess().ConnectionString;
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    string commandText = "SELECT TOP 1 MacTauID FROM CoBaoVICT WHERE MacTauID=@MacTauID";
                    SqlParameter param = new SqlParameter("@MacTauID", dm.MacTauID); cmd.Parameters.Add(param);
                    cmd.CommandText = commandText;
                    if (cmd.ExecuteScalar() != null)
                        throw new Exception("Đã có mác tầu này trong bảng cơ báo");
                    else
                    {
                        commandText = "DELETE FROM MacTauVI WHERE MacTauID=@MacTauID";
                        cmd.CommandText = commandText;
                        int recsAffected = cmd.ExecuteNonQuery();
                        opStatus.IsSuccess = (recsAffected == 1);
                        //Ghi nhật ký
                        string noiDung = "Xóa mác tầu: " + dm.MacTauID + " công tác: " + dm.CongTacID + "-" + dm.CongTacName;
                        commandText = "INSERT INTO NhatKyVI(TenBang,NoiDung,NgayTH,NguoiTH) VALUES(@TenBang,@NoiDung,@NgayTH,@NguoiTH)";
                        cmd.Parameters.Clear();
                        param = new SqlParameter("@TenBang", "MacTauVI"); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NoiDung", noiDung); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NgayTH", DateTime.Now); cmd.Parameters.Add(param);
                        param = new SqlParameter("@NguoiTH", nguoiTH); cmd.Parameters.Add(param);
                        cmd.CommandText = commandText;
                        recsAffected = cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    opStatus = OperationStatus.CreateFromException(ex);
                }
            }
        }
        return opStatus;
    }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoanThongDB
/// </summary>
public class DoanThongDB
{
    public DataSet GetDoanThongDS(int thangDT, int namDT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db=new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM View_DoanThong WHERE ThangDT=@ThangDT AND NamDT=@NamDT";
                if (loaiMay != "ALL")
                    commandText += " AND LoaiMayID=@LoaiMay";
                if (!string.IsNullOrWhiteSpace(shCoBao))
                    commandText += " AND SoCB LIKE '%" + shCoBao + "%'";
                if (!string.IsNullOrWhiteSpace(shDauMay))
                    commandText += " AND DauMayID LIKE '%" + shDauMay + "%'";
                if (!string.IsNullOrWhiteSpace(shTaiXe))
                    commandText += " AND TaiXe1ID LIKE '%" + shTaiXe + "%'";
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                db.AddParameter("@LoaiMay", loaiMay);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch 
        {
            ds = null;
        }
        return ds;
    }
    public DataSet GetDoanThongCTDS(long cobaoID)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM DoanThongCT WHERE CoBaoID=@CoBaoID ORDER BY SoTT";
                db.AddParameter("@CoBaoID", cobaoID);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
    public DataSet GetThanhTichDS(long cobaoID)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT TOP 1 * FROM View_ThanhTich WHERE CoBaoGoc=@CoBaoID";
                db.AddParameter("@CoBaoID", cobaoID);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
    public string GetTopDoanThong(long cobaoID)
    {
        string strResult = string.Empty;
        using (DBAccess db = new DBAccess(CommandType.Text))
        {
            IDataReader dr = null;
            try
            {
                string commandText = "SELECT  TOP 1 * FROM View_ThanhTich WHERE CoBaoGoc=@CoBaoID";
                db.AddParameter("@CoBaoID", cobaoID);
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    strResult = dr["socb"].ToString() + "," + Convert.ToDateTime(dr["ngaycb"]).ToString("dd.MM.yyyy")
                    + "," + dr["daumayid"].ToString() + "," + dr["loaimayid"].ToString()
                    + "," + Convert.ToInt32(dr["quayvong"])
                    + "," + Convert.ToInt32(dr["luhanh"])
                    + "," + Convert.ToInt32(dr["donthuan"])
                    + "," + Convert.ToInt32(dr["giodung"])
                    + "," + Convert.ToInt32(dr["giodon"])
                    + "," + Convert.ToDecimal(dr["km"])
                    + "," + Convert.ToDecimal(dr["tkm"])
                    + "," + Convert.ToDecimal(dr["nltieuchuan"])
                    + "," + Convert.ToDecimal(dr["nltieuthu"])
                    + "," + Convert.ToDecimal(dr["nlloilo"]);                    
                }
                dr.Close();
            }
            catch
            {
                dr.Close();
                strResult = string.Empty;
            }
            return strResult;
        }
    }
    public DataSet GetGioQVDS(int thangDT, int namDT)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText="SELECT dt.CoBaoID, dt.SoCB,Count(ct.CoBaoID) as soBG,sum(ct.QuayVong) as GioQV from VieW_DoanThongVI dt INNER JOIN DoanThongCT ct ON dt.CoBaoID=ct.CoBaoID"
                    + " group by dt.CoBaoID, dt.SoCB,dt.ThangDT,dt.NamDT  having (sum(ct.QuayVong)>720 OR sum(ct.QuayVong)<0) AND dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT order by sum(ct.QuayVong) desc";                                
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
    public DataSet GetGioDTDS(int thangDT, int namDT)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT dt.CoBaoID, dt.SoCB,Count(ct.CoBaoID) as soBG,sum(ct.DonThuan) as GioDT from View_DoanThong dt INNER JOIN DoanThongCT ct ON dt.CoBaoID=ct.CoBaoID"
                     + " group by dt.CoBaoID, dt.SoCB,dt.ThangDT,dt.NamDT  having (sum(ct.DonThuan)>600 OR sum(ct.DonThuan)<0) AND dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT order by sum(ct.DonThuan) desc";                         
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
    public DataSet GetVanTocDS(int thangDT, int namDT)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT dt.CoBaoID, dt.SoCB,ct.SoTT,convert(int,(CASE WHEN sum(ct.DonThuan) <>0 THEN sum(ct.KMChinh+ct.KMDon+ct.KMGhep+ct.KMDay)*60/sum(ct.DonThuan) ELSE 0 END)) as VanToc"
                +" from View_DoanThong dt INNER JOIN DoanThongCT ct ON dt.CoBaoID=ct.CoBaoID"
                + " group by dt.CoBaoID, dt.SoCB,ct.SoTT,dt.ThangDT,dt.NamDT  having ((CASE WHEN sum(ct.DonThuan) <>0 THEN sum(ct.KMChinh+ct.KMDon+ct.KMGhep+ct.KMDay)*60/sum(ct.DonThuan) ELSE 0 END)>60"
                + " OR (CASE WHEN sum(ct.DonThuan) <>0 THEN sum(ct.KMChinh+ct.KMDon+ct.KMGhep+ct.KMDay)*60/sum(ct.DonThuan) ELSE 0 END)<0) AND dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT"
                + " order by (CASE WHEN sum(ct.DonThuan) <>0 THEN sum(ct.KMChinh+ct.KMDon+ct.KMGhep+ct.KMDay)*60/sum(ct.DonThuan) ELSE 0 END) desc";
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }       
    public string CreateTableDB(string tableName, string sqlCreate)
    {
        string result = string.Empty;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                //string commandText = "SELECT TOP 1 * FROM "+tableName;
                //if(db.ExecuteScalar(commandText)!=null)
                //{
                //    throw new Exception("Đã có bảng này.");
                //}
                db.ExecuteNonQuery(sqlCreate);
            }            
            result = "OK@";
        }
        catch (Exception ex)
        {
            result = "ER@" + ex.Message;
        }
        return result;
    }
    public string InsertTableDB(string tableName, DataTable dt)
    {
        string result = string.Empty;
        try
        {
            // take note of SqlBulkCopyOptions.KeepIdentity , you may or may not want to use this for your situation.  
            using (var bulkCopy = new SqlBulkCopy(Utils.GetConnectionString(), SqlBulkCopyOptions.KeepIdentity))
            {
                // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, 
                // just pass in which datatable name matches the SQL column name in Column Mappings
                foreach (DataColumn col in dt.Columns)
                {
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }
                bulkCopy.BulkCopyTimeout = 600;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(dt);
            }
            result = "OK@";
        }
        catch (Exception ex)
        {
            result = "ER@" + ex.Message;
        }
        return result;
    }
    public  int fnSaveDoanThong(SqlCommand cmd, DoanThongInfo info, bool addNew)
    {
        string InsertDTText = "INSERT INTO DoanThong(CoBaoID,DungKB,NLTieuThu,DauDCTT,DauTLTT,DauGTTT,ThangDT,NamDT,Createddate,Createdby,CreatedName,Modifydate,Modifyby,ModifyName)"
           + " VALUES(@CoBaoID,@DungKB,@NLTieuThu,@DauDCTT,@DauTLTT,@DauGTTT,@ThangDT,@NamDT,@Createddate,@Createdby,@CreatedName,@Modifydate,@Modifyby,@ModifyName)";

        string UpdateDTText = "UPDATE DoanThong SET DungKB=@DungKB,NLTieuThu=@NLTieuThu,DauDCTT=@DauDCTT,DauTLTT=@DauTLTT,ThangDT=@ThangDT,NamDT=@NamDT"       
        + ",Createddate=@Createddate,Createdby=@Createdby,CreatedName=@CreatedName,Modifydate=@Modifydate,Modifyby=@Modifyby,ModifyName=@ModifyName"
        + " WHERE CoBaoID=@CoBaoID";
        cmd.Parameters.Clear();
        SqlParameter param = new SqlParameter("@CoBaoID", info.CoBaoID); cmd.Parameters.Add(param);        
        param = new SqlParameter("@DungKB", info.DungKB); cmd.Parameters.Add(param);
        param = new SqlParameter("@NLTieuThu", info.NLTieuThu); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauDCTT", info.DauDCTT); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauTLTT", info.DauTLTT); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauGTTT", info.DauGTTT); cmd.Parameters.Add(param);
        param = new SqlParameter("@ThangDT", info.ThangDT); cmd.Parameters.Add(param);
        param = new SqlParameter("@NamDT", info.NamDT); cmd.Parameters.Add(param);       
        param = new SqlParameter("@Createddate", info.Createddate); cmd.Parameters.Add(param);
        param = new SqlParameter("@Createdby", info.Createdby); cmd.Parameters.Add(param);
        param = new SqlParameter("@CreatedName", info.CreatedName); cmd.Parameters.Add(param);
        param = new SqlParameter("@Modifydate", info.Modifydate); cmd.Parameters.Add(param);
        param = new SqlParameter("@Modifyby", info.Modifyby); cmd.Parameters.Add(param);
        param = new SqlParameter("@ModifyName", info.ModifyName); cmd.Parameters.Add(param);

        string commandText = addNew ? InsertDTText : UpdateDTText;
        cmd.CommandText = commandText;
        int recsAffected = cmd.ExecuteNonQuery();
        return recsAffected;
    }    
    public void fnSaveDoanThongCT(SqlCommand cmd, long coBaoID, List<DoanThongCTInfo> dtCT,bool addNew)
    {
        //Delete doan thong ct
        string commandText = "DELETE FROM DoanThongCT WHERE CoBaoID=@CoBaoID";
        cmd.Parameters.Clear();
        SqlParameter param = new SqlParameter("@CoBaoID", coBaoID);
        cmd.Parameters.Add(param);
        cmd.CommandText = commandText;
        int recsAffected=0;
        if(!addNew)
            recsAffected = cmd.ExecuteNonQuery();
        //Inser doan thong ct
        commandText = "INSERT INTO DoanThongCT(CoBaoID,SoTT,NgayXP,MacTauID,CongTyID,CongTyName,CongTacID,CongTacName,TinhChatID,TinhChatName"
         + ",KhuDoanID,DinhMuc,TuyenID,TuyenName,GaXPID,GaXPName,GaKTID,GaKTName,MayGhepID"
         + ",QuayVong,LuHanh,DonThuan,DungDM,DungDN,DungQD,DungXP,DungDD,DungKT,DungKhoDM,DungKhoDN,DonXP,DonDD,DonKT,XeTotal,XeRongTotal"
         + ",KMChinh,KMDon,KMGhep,KMDay,TKMChinh,TKMDon,TKMGhep,TKMDay,TKMRong,NLTieuThu,NLTieuChuan,DauDCTT,DauDCTC,DauGTTT,DauGTTC,DauTLTT,DauTLTC,SLRKDM,SLRKDN)"
         + " VALUES(@CoBaoID,@SoTT,@NgayXP,@MacTauID,@CongTyID,@CongTyName,@CongTacID,@CongTacName,@TinhChatID,@TinhChatName"
         + ",@KhuDoanID,@DinhMuc,@TuyenID,@TuyenName,@GaXPID,@GaXPName,@GaKTID,@GaKTName,@MayGhepID"
         + ",@QuayVong,@LuHanh,@DonThuan,@DungDM,@DungDN,@DungQD,@DungXP,@DungDD,@DungKT,@DungKhoDM,@DungKhoDN,@DonXP,@DonDD,@DonKT,@XeTotal,@XeRongTotal"
         + ",@KMChinh,@KMDon,@KMGhep,@KMDay,@TKMChinh,@TKMDon,@TKMGhep,@TKMDay,@TKMRong,@NLTieuThu,@NLTieuChuan,@DauDCTT,@DauDCTC,@DauGTTT,@DauGTTC,@DauTLTT,@DauTLTC,@SLRKDM,@SLRKDN)";
        if (dtCT != null && dtCT.Count > 0)
        {
            foreach (DoanThongCTInfo dr in dtCT)
            {
                cmd.Parameters.Clear();
                param = new SqlParameter("@CoBaoID", coBaoID); cmd.Parameters.Add(param);
                param = new SqlParameter("@SoTT", dr.SoTT); cmd.Parameters.Add(param);
                param = new SqlParameter("@NgayXP", dr.NgayXP); cmd.Parameters.Add(param);
                param = new SqlParameter("@MacTauID", dr.MacTauID); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTyID", dr.CongTyID); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTyName", dr.CongTyName); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTacID", dr.CongTacID); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTacName", dr.CongTacName); cmd.Parameters.Add(param);
                param = new SqlParameter("@TinhChatID", dr.TinhChatID); cmd.Parameters.Add(param);
                param = new SqlParameter("@TinhChatName", dr.TinhChatName); cmd.Parameters.Add(param);
                param = new SqlParameter("@KhuDoanID", dr.KhuDoanID); cmd.Parameters.Add(param);
                param = new SqlParameter("@DinhMuc", dr.DinhMuc); cmd.Parameters.Add(param);
                param = new SqlParameter("@TuyenID", dr.TuyenID); cmd.Parameters.Add(param);
                param = new SqlParameter("@TuyenName", dr.TuyenName); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaXPID", dr.GaXPID); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaXPName", dr.GaXPName); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaKTID", dr.GaKTID); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaKTName", dr.GaKTName); cmd.Parameters.Add(param);
                param = new SqlParameter("@MayGhepID", dr.MayGhepID); cmd.Parameters.Add(param);
                param = new SqlParameter("@QuayVong", dr.QuayVong); cmd.Parameters.Add(param);
                param = new SqlParameter("@LuHanh", dr.LuHanh); cmd.Parameters.Add(param);
                param = new SqlParameter("@DonThuan", dr.DonThuan); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungDM", dr.DungDM); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungDN", dr.DungDN); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungQD", dr.DungQD); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungXP", dr.DungXP); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungDD", dr.DungDD); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungKT", dr.DungKT); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungKhoDM", dr.DungKhoDM); cmd.Parameters.Add(param);
                param = new SqlParameter("@DungKhoDN", dr.DungKhoDN); cmd.Parameters.Add(param);
                param = new SqlParameter("@DonXP", dr.DonXP); cmd.Parameters.Add(param);
                param = new SqlParameter("@DonDD", dr.DonDD); cmd.Parameters.Add(param);
                param = new SqlParameter("@DonKT", dr.DonKT); cmd.Parameters.Add(param);
                param = new SqlParameter("@XeTotal", dr.XeTotal); cmd.Parameters.Add(param);
                param = new SqlParameter("@XeRongTotal", dr.XeRongTotal); cmd.Parameters.Add(param);
                param = new SqlParameter("@KMChinh", dr.KMChinh); cmd.Parameters.Add(param);
                param = new SqlParameter("@KMDon", dr.KMDon); cmd.Parameters.Add(param);
                param = new SqlParameter("@KMGhep", dr.KMGhep); cmd.Parameters.Add(param);
                param = new SqlParameter("@KMDay", dr.KMDay); cmd.Parameters.Add(param);
                param = new SqlParameter("@TKMChinh", dr.TKMChinh); cmd.Parameters.Add(param);
                param = new SqlParameter("@TKMDon", dr.TKMDon); cmd.Parameters.Add(param);
                param = new SqlParameter("@TKMGhep", dr.TKMGhep); cmd.Parameters.Add(param);
                param = new SqlParameter("@TKMDay", dr.TKMDay); cmd.Parameters.Add(param);
                param = new SqlParameter("@TKMRong", dr.TKMDay); cmd.Parameters.Add(param);
                param = new SqlParameter("@NLTieuThu", dr.NLTieuThu); cmd.Parameters.Add(param);
                param = new SqlParameter("@NLTieuChuan", dr.NLTieuChuan); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauDCTT", dr.DauDCTT); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauDCTC", dr.DauDCTC); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauGTTT", dr.DauGTTT); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauGTTC", dr.DauGTTC); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauTLTT", dr.DauTLTT); cmd.Parameters.Add(param);
                param = new SqlParameter("@DauTLTC", dr.DauTLTC); cmd.Parameters.Add(param);
                param = new SqlParameter("@SLRKDM", dr.SLRKDM); cmd.Parameters.Add(param);
                param = new SqlParameter("@SLRKDN", dr.SLRKDN); cmd.Parameters.Add(param);
                cmd.CommandText = commandText;
                recsAffected = cmd.ExecuteNonQuery();
                if (recsAffected != 1)
                    throw new Exception("Lỗi insert đoạn thống chi tiết thứ: " + dr.SoTT.ToString());
            }
        }
    }
    public OperationStatus DeleteDoanThong(string tableName, int thangDT, int namDT)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "DELETE FROM " + tableName + " WHERE ThangDT=@ThangDT AND NamDT=@NamDT";
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                int recsAffected = db.ExecuteNonQuery(commandText);
                opStatus.IsSuccess = (recsAffected >= 1);
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }
    public OperationStatus KhoaDT(string tableName, int thangDT, int namDT,bool khoaDT)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {

                string commandText = "UPDATE " + tableName + " SET KhoaSo=@KhoaSo WHERE ThangDT=@ThangDT AND NamDT=@NamDT";
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                db.AddParameter("@KhoaSo", khoaDT);
                int recsAffected = db.ExecuteNonQuery(commandText);
                opStatus.IsSuccess = (recsAffected >= 1);
            }
        }
        catch (Exception ex)
        {
            opStatus = OperationStatus.CreateFromException(ex);
        }
        return opStatus;
    }
}
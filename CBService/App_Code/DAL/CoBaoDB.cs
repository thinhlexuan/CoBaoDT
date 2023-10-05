using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CoBaoDB
/// </summary>
public class CoBaoDB
{
    public List<CoBaoInfo> GetCoBaoList(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        List<CoBaoInfo> list = new List<CoBaoInfo>();
        IDataReader dr = null;
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM CoBao";
                if (loaiTT == 0)
                    commandText += " WHERE Modifydate>=@NgayBD AND Modifydate<@NgayKT";
                else
                    commandText += " WHERE NgayCB>=@NgayBD AND NgayCB<@NgayKT";
                if (loaiMay != "ALL")
                    commandText += " AND LoaiMayID=@LoaiMay";
                if (!string.IsNullOrWhiteSpace(shCoBao))
                    commandText += " AND SoCB LIKE '%" + shCoBao + "%'";
                if (!string.IsNullOrWhiteSpace(shDauMay))
                    commandText += " AND DauMayID LIKE '%" + shDauMay + "%'";
                if (!string.IsNullOrWhiteSpace(shTaiXe))
                    commandText += " AND TaiXe1ID LIKE '%" + shTaiXe + "%'";
                db.AddParameter("@NgayBD", ngayBD);
                db.AddParameter("@NgayKT", ngayKT);
                db.AddParameter("@LoaiMay", loaiMay);
                dr = db.ExecuteReader(commandText);
                while (dr.Read())
                {
                    CoBaoInfo info = new CoBaoInfo();
                    info.CoBaoID = Convert.ToInt64(dr["CoBaoID"]);
                    info.CoBaoGoc = Convert.ToInt64(dr["CoBaoGoc"]);
                    info.DauMayID = dr["DauMayID"].ToString();
                    info.LoaiMayID = dr["LoaiMayID"].ToString();
                    info.DvdmID = dr["DvdmID"].ToString();
                    info.DvdmName = dr["DvdmName"].ToString();
                    info.SoCB = dr["SoCB"].ToString();
                    info.DvcbID = dr["DvcbID"].ToString();
                    info.DvcbName = dr["DvcbName"].ToString();
                    info.NgayCB = Convert.ToDateTime(dr["NgayCB"]);                    
                    info.RutGio = Convert.ToInt32(dr["RutGio"]);
                    info.ChatLuong = dr["ChatLuong"].ToString();
                    info.SoLanRaKho = Convert.ToDecimal(dr["SoLanRaKho"]);

                    info.TaiXe1ID = dr["TaiXe1ID"].ToString();
                    info.TaiXe1Name = dr["TaiXe1Name"].ToString();
                    info.PhoXe1ID = dr["PhoXe1ID"].ToString();
                    info.PhoXe1Name = dr["PhoXe1Name"].ToString();
                    info.TaiXe2ID = dr["TaiXe2ID"].ToString();
                    info.TaiXe2Name = dr["TaiXe2Name"].ToString();
                    info.PhoXe2ID = dr["PhoXe2ID"].ToString();
                    info.PhoXe2Name = dr["PhoXe2Name"].ToString();
                    info.TaiXe3ID = dr["TaiXe3ID"].ToString();
                    info.TaiXe3Name = dr["TaiXe3Name"].ToString();
                    info.PhoXe3ID = dr["PhoXe3ID"].ToString();
                    info.PhoXe3Name = dr["PhoXe3Name"].ToString();

                    info.LenBan = Convert.ToDateTime(dr["LenBan"]);
                    info.NhanMay = Convert.ToDateTime(dr["NhanMay"]);
                    info.GiaoMay = Convert.ToDateTime(dr["GiaoMay"]);
                    info.RaKho = Convert.ToDateTime(dr["RaKho"]);
                    info.VaoKho = Convert.ToDateTime(dr["VaoKho"]);
                    info.XuongBan = Convert.ToDateTime(dr["XuongBan"]);

                    info.NLBanTruoc = Convert.ToInt32(dr["NLBanTruoc"]);
                    info.NLThucNhan = Convert.ToInt32(dr["NLThucNhan"]);
                    info.NLLinh = Convert.ToInt32(dr["NLLinh"]);
                    info.TramNLID = dr["TramNLID"].ToString();
                    info.NLTrongDoan = Convert.ToInt32(dr["NLTrongDoan"]);
                    info.NLBanSau = Convert.ToInt32(dr["NLBanSau"]);

                    info.DauDCNhan = Convert.ToDecimal(dr["DauDCNhan"]);
                    info.DauDCLinh = Convert.ToDecimal(dr["DauDCLinh"]);
                    info.TramDCID = dr["TramDCID"].ToString();
                    info.DauDCGiao = Convert.ToDecimal(dr["DauDCGiao"]);
                    info.DauTLNhan = Convert.ToDecimal(dr["DauTLNhan"]);
                    info.DauTLLinh = Convert.ToDecimal(dr["DauTLLinh"]);
                    info.TramTLID = dr["TramTLID"].ToString();
                    info.DauTLGiao = Convert.ToDecimal(dr["DauTLGiao"]);
                    info.DauGTNhan = Convert.ToDecimal(dr["DauGTNhan"]);
                    info.DauGTLinh = Convert.ToDecimal(dr["DauGTLinh"]);
                    info.TramGTID = dr["TramGTID"].ToString();
                    info.DauGTGiao = Convert.ToDecimal(dr["DauGTGiao"]);
                    
                    info.SHDT = dr["SHDT"].ToString();
                    info.MaCB = dr["MaCB"].ToString();
                    info.DonDocDuong = Convert.ToDecimal(dr["DonDocDuong"]);
                    info.DungDocDuong = Convert.ToDecimal(dr["DungDocDuong"]);
                    info.DungNoMay = Convert.ToDecimal(dr["DungNoMay"]);

                    info.Createddate = Convert.ToDateTime(dr["Createddate"]);
                    info.Createdby = dr["Createdby"].ToString();
                    info.CreatedName = dr["CreatedName"].ToString();
                    info.Modifydate = Convert.ToDateTime(dr["Modifydate"]);
                    info.Modifyby = dr["Modifyby"].ToString();
                    info.ModifyName = dr["ModifyName"].ToString();
                    info.TrangThai = dr["TrangThai"].ToString();                   
                    info.KhoaCB = Convert.ToBoolean(dr["KhoaCB"]);

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
    public DataSet GetCoBaoDS(short loaiTT, DateTime ngayBD, DateTime ngayKT, string loaiMay, string shCoBao, string shDauMay, string shTaiXe)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM CoBao";
                if (loaiTT == 0)
                    commandText += " WHERE Modifydate>=@NgayBD AND Modifydate<@NgayKT";
                else
                    commandText += " WHERE NgayCB>=@NgayBD AND NgayCB<@NgayKT";
                if (loaiMay != "ALL")
                    commandText += " AND DauMayID in (SELECT DauMayID FROM DauMay WHERE LoaiMayID=@LoaiMay)";
                if (!string.IsNullOrWhiteSpace(shCoBao))
                    commandText += " AND SoCB LIKE '%" + shCoBao + "%'";
                if (!string.IsNullOrWhiteSpace(shDauMay))
                    commandText += " AND DauMayID LIKE '%" + shDauMay + "%'";
                if (!string.IsNullOrWhiteSpace(shTaiXe))
                    commandText += " AND TaiXe1ID LIKE '%" + shTaiXe + "%'";
                db.AddParameter("@NgayBD", ngayBD);
                db.AddParameter("@NgayKT", ngayKT);
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
    public DataSet GetCoBaoCTDS(long cobaoID)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM CoBaoCT WHERE CoBaoID=@CoBaoID ORDER BY SoTT";
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
    private long GetCoBaoID()
    {
        long intResult = 1;
        using (DBAccess db = new DBAccess(CommandType.Text))
        {
            try
            {
                string commandText = "SELECT TOP 1 CoBaoID FROM CoBao ORDER BY CoBaoID DESC";
                object objResult = db.ExecuteScalar(commandText);
                if (objResult != null)
                    intResult = Convert.ToInt64(objResult.ToString()) + 1;
            }
            catch
            {
                intResult = 1;
            }
            return intResult;
        }
    }   
    private int fnSaveCoBao(SqlCommand cmd, CoBaoInfo info,bool addNew)
    {
        string InsertCBText = "INSERT INTO CoBao(CoBaoID,CoBaoGoc,DauMayID,LoaiMayID,DvdmID,DvdmName,SoCB,DvcbID,DvcbName,NgayCB,RutGio,ChatLuong,SoLanRaKho"
          + ",TaiXe1ID,TaiXe1Name,PhoXe1ID,PhoXe1Name,TaiXe2ID,TaiXe2Name,PhoXe2ID,PhoXe2Name, TaiXe3ID, TaiXe3Name, PhoXe3ID, PhoXe3Name"
          + ",LenBan,NhanMay,RaKho,VaoKho,GiaoMay,XuongBan,NLBanTruoc,NLThucNhan,NLLinh,TramNLID,NLTrongDoan,NLBanSau"
          + ",DauDCNhan,DauDCLinh,TramDCID,DauDCGiao,DauTLNhan,DauTLLinh,TramTLID,DauTLGiao,DauGTNhan,DauGTLinh,TramGTID,DauGTGiao"
          + ",SHDT,MaCB,DonDocDuong,DungDocDuong,DungNoMay,Createddate,Createdby,CreatedName,Modifydate,Modifyby,ModifyName,TrangThai,KhoaCB)"
          + " VALUES(@CoBaoID,@CoBaoGoc,@DauMayID,@LoaiMayID,@DvdmID,@DvdmName,@SoCB,@DvcbID,@DvcbName,@NgayCB,@RutGio,@ChatLuong,@SoLanRaKho"
          + ",@TaiXe1ID,@TaiXe1Name,@PhoXe1ID,@PhoXe1Name,@TaiXe2ID,@TaiXe2Name,@PhoXe2ID,@PhoXe2Name,@TaiXe3ID,@TaiXe3Name,@PhoXe3ID,@PhoXe3Name"
          + ",@LenBan,@NhanMay,@RaKho,@VaoKho,@GiaoMay,@XuongBan,@NLBanTruoc,@NLThucNhan,@NLLinh,@TramNLID,@NLTrongDoan,@NLBanSau"
          + ",@DauDCNhan,@DauDCLinh,@TramDCID,@DauDCGiao,@DauTLNhan,@DauTLLinh,@TramTLID,@DauTLGiao,@DauGTNhan,@DauGTLinh,@TramGTID,@DauGTGiao"
          + ",@SHDT,@MaCB,@DonDocDuong,@DungDocDuong,@DungNoMay,@Createddate,@Createdby,@CreatedName,@Modifydate,@Modifyby,@ModifyName,@TrangThai,@KhoaCB)";

        string UpdateCBText = "UPDATE CoBao SET CoBaoGoc=@CoBaoGoc,DauMayID=@DauMayID,LoaiMayID=@LoaiMayID,DvdmID=@DvdmID,DvdmName=@DvdmName,SoCB=@SoCB"
        + ",DvcbID=@DvcbID,DvcbName=@DvcbName,NgayCB=@NgayCB,RutGio=@RutGio,ChatLuong=@ChatLuong,SoLanRaKho=@SoLanRaKho"
        + ",TaiXe1ID=@TaiXe1ID,TaiXe1Name=@TaiXe1Name,PhoXe1ID=@PhoXe1ID,PhoXe1Name=@PhoXe1Name,TaiXe2ID=@TaiXe2ID,TaiXe2Name=@TaiXe2Name"
        + ",PhoXe2ID=@PhoXe2ID,PhoXe2Name=@PhoXe2Name,TaiXe2ID=@TaiXe2ID,TaiXe2Name=@TaiXe2Name,PhoXe2ID=@PhoXe2ID,PhoXe2Name=@PhoXe2Name"
        + ",LenBan=@LenBan,NhanMay=@NhanMay,RaKho=@RaKho,VaoKho=@VaoKho,GiaoMay=@GiaoMay,XuongBan=@XuongBan"
        + ",NLBanTruoc=@NLBanTruoc,NLThucNhan=@NLThucNhan,NLLinh=@NLLinh,TramNLID=@TramNLID,NLTrongDoan=@NLTrongDoan,NLBanSau=@NLBanSau"
        + ",DauDCNhan=@DauDCNhan,DauDCLinh=@DauDCLinh,TramDCID=@TramDCID,DauDCGiao=@DauDCGiao,DauTLNhan=@DauTLNhan,DauTLLinh=@DauTLLinh,TramTLID=@TramTLID,DauTLGiao=@DauTLGiao"
        + ",DauGTNhan=@DauGTNhan,DauGTLinh=@DauGTLinh,TramGTID=@TramGTID,DauGTGiao=@DauGTGiao,SHDT=@SHDT,MaCB=@MaCB,DonDocDuong=@DonDocDuong,DungDocDuong=@DungDocDuong,DungNoMay=@DungNoMay"
        + ",Createddate=@Createddate,Createdby=@Createdby,CreatedName=@CreatedName,Modifydate=@Modifydate,Modifyby=@Modifyby,ModifyName=@ModifyName,TrangThai=@TrangThai"
        + ",KhoaCB=@KhoaCB WHERE CoBaoID=@CoBaoID";
        cmd.Parameters.Clear();
        SqlParameter param = new SqlParameter("@CoBaoID", info.CoBaoID); cmd.Parameters.Add(param);
        param = new SqlParameter("@CoBaoGoc", info.CoBaoGoc); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauMayID", info.DauMayID); cmd.Parameters.Add(param);
        param = new SqlParameter("@LoaiMayID", info.LoaiMayID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DvdmID", info.DvdmID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DvdmName", info.DvdmName); cmd.Parameters.Add(param);
        param = new SqlParameter("@SoCB", info.SoCB); cmd.Parameters.Add(param);
        param = new SqlParameter("@DvcbID", info.DvcbID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DvcbName", info.DvcbName); cmd.Parameters.Add(param);
        param = new SqlParameter("@NgayCB", info.NgayCB); cmd.Parameters.Add(param);
        param = new SqlParameter("@RutGio", info.RutGio); cmd.Parameters.Add(param);
        param = new SqlParameter("@ChatLuong", info.ChatLuong); cmd.Parameters.Add(param);
        param = new SqlParameter("@SoLanRaKho", info.SoLanRaKho); cmd.Parameters.Add(param);

        param = new SqlParameter("@TaiXe1ID", info.TaiXe1ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@TaiXe1Name", info.TaiXe1Name); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe1ID", info.PhoXe1ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe1Name", info.PhoXe1Name); cmd.Parameters.Add(param);
        param = new SqlParameter("@TaiXe2ID", info.TaiXe2ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@TaiXe2Name", info.TaiXe2Name); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe2ID", info.PhoXe2ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe2Name", info.PhoXe2Name); cmd.Parameters.Add(param);
        param = new SqlParameter("@TaiXe3ID", info.TaiXe3ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@TaiXe3Name", info.TaiXe3Name); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe3ID", info.PhoXe3ID); cmd.Parameters.Add(param);
        param = new SqlParameter("@PhoXe3Name", info.PhoXe3Name); cmd.Parameters.Add(param);

        param = new SqlParameter("@LenBan", info.LenBan); cmd.Parameters.Add(param);
        param = new SqlParameter("@NhanMay", info.NhanMay); cmd.Parameters.Add(param);
        param = new SqlParameter("@RaKho", info.RaKho); cmd.Parameters.Add(param);
        param = new SqlParameter("@VaoKho", info.VaoKho); cmd.Parameters.Add(param);
        param = new SqlParameter("@GiaoMay", info.GiaoMay); cmd.Parameters.Add(param);
        param = new SqlParameter("@XuongBan", info.XuongBan); cmd.Parameters.Add(param);

        param = new SqlParameter("@NLBanTruoc", info.NLBanTruoc); cmd.Parameters.Add(param);
        param = new SqlParameter("@NLThucNhan", info.NLThucNhan); cmd.Parameters.Add(param);
        param = new SqlParameter("@NLLinh", info.NLLinh); cmd.Parameters.Add(param);
        param = new SqlParameter("@TramNLID", info.TramNLID); cmd.Parameters.Add(param);
        param = new SqlParameter("@NLTrongDoan", info.NLTrongDoan); cmd.Parameters.Add(param);
        param = new SqlParameter("@NLBanSau", info.NLBanSau); cmd.Parameters.Add(param);

        param = new SqlParameter("@DauDCNhan", info.DauDCNhan); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauDCLinh", info.DauDCLinh); cmd.Parameters.Add(param);
        param = new SqlParameter("@TramDCID", info.TramDCID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauDCGiao", info.DauDCGiao); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauTLNhan", info.DauTLNhan); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauTLLinh", info.DauTLLinh); cmd.Parameters.Add(param);
        param = new SqlParameter("@TramTLID", info.TramTLID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauTLGiao", info.DauTLGiao); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauGTNhan", info.DauGTNhan); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauGTLinh", info.DauGTLinh); cmd.Parameters.Add(param);
        param = new SqlParameter("@TramGTID", info.TramGTID); cmd.Parameters.Add(param);
        param = new SqlParameter("@DauGTGiao", info.DauGTGiao); cmd.Parameters.Add(param);

        param = new SqlParameter("@SHDT", info.SHDT); cmd.Parameters.Add(param);
        param = new SqlParameter("@MaCB", info.MaCB); cmd.Parameters.Add(param);
        param = new SqlParameter("@DonDocDuong", info.DonDocDuong); cmd.Parameters.Add(param);
        param = new SqlParameter("@DungDocDuong", info.DungDocDuong); cmd.Parameters.Add(param);
        param = new SqlParameter("@DungNoMay", info.DungNoMay); cmd.Parameters.Add(param);

        info.Createddate = addNew ? DateTime.Now : info.Createddate;
        param = new SqlParameter("@Createddate", info.Createddate); cmd.Parameters.Add(param);
        param = new SqlParameter("@Createdby", info.Createdby); cmd.Parameters.Add(param);
        param = new SqlParameter("@CreatedName", info.CreatedName); cmd.Parameters.Add(param); 
        info.Modifydate = addNew? info.Modifydate : DateTime.Now;
        param = new SqlParameter("@Modifydate", info.Modifydate); cmd.Parameters.Add(param);
        param = new SqlParameter("@Modifyby", info.Modifyby); cmd.Parameters.Add(param);
        param = new SqlParameter("@ModifyName", info.ModifyName); cmd.Parameters.Add(param);
        param = new SqlParameter("@TrangThai", info.TrangThai); cmd.Parameters.Add(param);       
        param = new SqlParameter("@KhoaCB", info.KhoaCB); cmd.Parameters.Add(param);

        string commandText = addNew ? InsertCBText : UpdateCBText;
        cmd.CommandText = commandText;
        int recsAffected = cmd.ExecuteNonQuery();        
        return recsAffected;
    }
    private void fnSaveCoBaoCT(SqlCommand cmd, long coBaoID, DataTable cbCT,bool addNew)
    {
        //Delete doan thong ct
        string commandText = "DELETE FROM CoBaoCT WHERE CoBaoID=@CoBaoID";
        cmd.Parameters.Clear();
        SqlParameter param = new SqlParameter("@CoBaoID", coBaoID);
        cmd.Parameters.Add(param);
        cmd.CommandText = commandText;
        int recsAffected = 0;
        if (!addNew)
            recsAffected = cmd.ExecuteNonQuery();
        
        commandText = "INSERT INTO CoBaoCT(CoBaoID,SoTT,NgayXP,GioDen,GioDi,GioDon,MacTauID,CongTyID,CongTyName,CongTacID,CongTacName,GaID,GaName"
        + ",TuyenID,TuyenName,Tan,XeTotal,TanXeRong,XeRongTotal,TinhChatID,TinhChatName,MayGhepID,KmAdd)"
        + " VALUES(@CoBaoID,@SoTT,@NgayXP,@GioDen,@GioDi,@GioDon,@MacTauID,@CongTyID,@CongTyName,@CongTacID,@CongTacName,@GaID,@GaName"
        + ",@TuyenID,@TuyenName,@Tan,@XeTotal,@TanXeRong,@XeRongTotal,@TinhChatID,@TinhChatName,@MayGhepID,@KmAdd)";
        if (cbCT != null && cbCT.Rows.Count > 0)
        {
            foreach (DataRow dr in cbCT.Rows)
            {
                cmd.Parameters.Clear();
                param = new SqlParameter("@CoBaoID", coBaoID); cmd.Parameters.Add(param);
                param = new SqlParameter("@SoTT", dr["SoTT"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@NgayXP", dr["NgayXP"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@GioDen", dr["GioDen"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@GioDi", dr["GioDi"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@GioDon", dr["GioDon"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@MacTauID", dr["MacTauID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTyID", dr["CongTyID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTyName", dr["CongTyName"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTacID", dr["CongTacID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@CongTacName", dr["CongTacName"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaID", dr["GaID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@GaName", dr["GaName"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@TuyenID", dr["TuyenID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@TuyenName", dr["TuyenName"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@Tan", dr["Tan"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@XeTotal", dr["XeTotal"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@TanXeRong", dr["TanXeRong"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@XeRongTotal", dr["XeRongTotal"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@TinhChatID", dr["TinhChatID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@TinhChatName", dr["TinhChatName"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@MayGhepID", dr["MayGhepID"]); cmd.Parameters.Add(param);
                param = new SqlParameter("@KmAdd", dr["KmAdd"]); cmd.Parameters.Add(param);
                cmd.CommandText = commandText;
                recsAffected = cmd.ExecuteNonQuery();
                if (recsAffected != 1)
                    throw new Exception("Lỗi thêm cơ báo chi tiết thứ: " + dr["SoTT"].ToString());
            }
        }
    }
    public OperationStatus SaveCoBao(CoBaoInfo cbInfo,DoanThongInfo dtInfo, DataTable cbCT,List<DoanThongCTInfo> dtCT, bool addNew)
    {
        OperationStatus opStatus = new OperationStatus { IsSuccess = true };
        string strConn = new DBAccess().ConnectionString;
        DoanThongDB db = new DoanThongDB();
        cbInfo.CoBaoID = GetCoBaoID();
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    //Save cơ báo
                    int recsAffected = fnSaveCoBao(cmd, cbInfo,addNew);
                    opStatus.IsSuccess = (recsAffected == 1);
                    //Save co bao ct
                    fnSaveCoBaoCT(cmd, cbInfo.CoBaoID, cbCT,addNew);
                    //Save đoạn thống
                    dtInfo.CoBaoID = cbInfo.CoBaoID;
                    recsAffected = db.fnSaveDoanThong(cmd, dtInfo,addNew);
                    opStatus.IsSuccess = (recsAffected == 1);
                    //Save đoạn thống ct
                    db.fnSaveDoanThongCT(cmd, cbInfo.CoBaoID, dtCT,addNew);
                    opStatus.NewID = cbInfo.CoBaoID;
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
    public OperationStatus DeleteCoBao(CoBaoInfo cbInfo,string nguoiTH)
    {
        long cobaoID = cbInfo.CoBaoID;
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
                    //Delete cơ báo
                    string commandText = "DELETE FROM CoBao WHERE CoBaoID=@CoBaoID";
                    SqlParameter param = new SqlParameter("@CoBaoID", cobaoID);
                    cmd.Parameters.Add(param);
                    cmd.CommandText = commandText;
                    int recsAffected = cmd.ExecuteNonQuery();
                    opStatus.IsSuccess = (recsAffected == 1);
                    //Delete co bao ct
                    commandText = "DELETE FROM CoBaoCT WHERE CoBaoID=@CoBaoID";
                    cmd.CommandText = commandText;
                    recsAffected = cmd.ExecuteNonQuery();
                    //if (recsAffected < 0)
                    //    throw new Exception("Lỗi xóa cơ báo chi tiết.");
                    //Delete đoạn thống
                    commandText = "DELETE FROM DoanThong WHERE CoBaoID=@CoBaoID";
                    cmd.CommandText = commandText;
                    recsAffected = cmd.ExecuteNonQuery();
                    opStatus.IsSuccess = (recsAffected == 1);
                    //Delete đoạn thống ct
                    commandText = "DELETE FROM DoanThongCT WHERE CoBaoID=@CoBaoID";
                    cmd.CommandText = commandText;
                    recsAffected = cmd.ExecuteNonQuery();
                    //Ghi nhật ký
                    string noiDung = "Xóa cơ báo: " + cbInfo.SoCB + " ngày: " + cbInfo.NgayCB.ToString("dd.MM.yyyy") + " đầu máy :" + cbInfo.DauMayID + " tài xế: " + cbInfo.TaiXe1ID + "-" + cbInfo.TaiXe1Name;
                    commandText = "INSERT INTO NhatKy(TenBang,NoiDung,NgayTH,NguoiTH) VALUES(@TenBang,@NoiDung,@NgayTH,@NguoiTH)";
                    cmd.Parameters.Clear();
                    param = new SqlParameter("@TenBang", "CoBao"); cmd.Parameters.Add(param);
                    param = new SqlParameter("@NoiDung", noiDung); cmd.Parameters.Add(param);
                    param = new SqlParameter("@NgayTH", DateTime.Now); cmd.Parameters.Add(param);
                    param = new SqlParameter("@NguoiTH", nguoiTH); cmd.Parameters.Add(param);
                    cmd.CommandText = commandText;
                    recsAffected = cmd.ExecuteNonQuery();

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
}

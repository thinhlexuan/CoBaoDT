using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaoCaoDB
/// </summary>
public class BaoCaoDB
{
    public DataSet GetBCVanDungDS(int thangDT, int namDT, string loaiMay, int tuyen)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT ct.CongTacID,ct.TinhChatID,sum(ct.QuayVong) as GioDM,sum(ct.LuHanh) as GioLH,sum(ct.DonThuan) as GioDT,sum(ct.DungXP) as DGXP,sum(ct.DungDD) as DGDD,sum(ct.DungKT) as DGCC"
                + ",SUM(ct.DungQD) as DGQD,sum(ct.DungDM) as DGDM,sum(ct.DungDN) as DGDN,sum(ct.DungKhoDM) as DGKM,sum(ct.DungKhoDN) as DGKN,sum(ct.DonXP) as DNXP,sum(ct.DonDD) as DNDD,sum(ct.DonKT) as DNCC"
                + ",sum(ct.KMChinh) as KMCH,sum(ct.KMDon) as KMDW,sum(ct.KMGhep) as KMGH,sum(ct.KMDay) as KMDY,sum(ct.TKMChinh) as TKCH,sum(ct.TKMDon) as TKDW,sum(ct.TKMGhep) as TKGH,sum(ct.TKMDay) as TKDY"
                + ",sum(ct.SLRKDM) as SLRKM,sum(ct.SLRKDN) as SLRKN,sum(ct.NLTieuThu) as SLTT,sum(ct.NLTieuChuan) as SLTC"
                + " FROM DoanThongVICT ct INNER JOIN DoanThongVI dt ON ct.CoBaoID=dt.CoBaoID WHERE dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT";
                db.AddParameter("@ThangDT", thangDT);
                db.AddParameter("@NamDT", namDT);
                if (loaiMay != "ALL")
                {
                    commandText += " AND dt.LoaiMayID=@LoaiMay";
                    db.AddParameter("@LoaiMay", loaiMay);
                }
                if (tuyen > 0)
                {
                    commandText += " AND TuyenID IN (SELECT TuyenID FROM Tuyen WHERE TuyenMap=@Tuyen)";
                    db.AddParameter("@Tuyen", tuyen);
                }
                commandText += " GROUP BY ct.CongTacID,ct.TinhChatID ORDER BY ct.CongTacID,ct.TinhChatID";
                ds = db.ExecuteDataSet(commandText);
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }

    public DataSet GetBCDauMoDS(int thangDT, int namDT,string loaiDauMo)
    {        
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = string.Empty;
                if (loaiDauMo == "DC")
                    commandText = "select DauMayID,NgayCB,SoCB,TramDCID as TramID,DauDCLinh as Linh from DoanThongVI where TramDCID<>''";
                else if (loaiDauMo == "TL")
                    commandText = "select DauMayID,NgayCB,SoCB,TramTLID as TramID,DauTLLinh as Linh from DoanThongVI where TramTLID<>''";
                else if (loaiDauMo == "GT")
                    commandText = "select DauMayID,NgayCB,SoCB,TramGTID as TramID,DauGTLinh as Linh from DoanThongVI where TramGTID<>''";
                else
                    commandText = "select DauMayID,NgayCB,SoCB,TramNLID as TramID,NLLinh as Linh from DoanThongVI where TramNLID<>''";
                commandText += "AND ThangDT =@ThangDT AND NamDT=@NamDT order by DauMayID,NgayCB,SoCB";
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

    public DataSet GetBCTinhLuongDS(int thangDT, int namDT)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "select * from"
                + " (select dt.DauMayID,dt.SoCB,dt.NgayCB,dt.TaiXe1ID as TaiXeID,dt.TaiXe1Name as TaiXeName,1 as LT,10 as PT,dt.MaCB,ct.MacTauID"
                + " ,ct.TinhChatID,sum(ct.QuayVong) as GioDM,sum(ct.kmchinh+ct.kmdon+ct.KMGhep+ct.KMDay)+(SUM(ct.DonXP+ct.DonDD+ct.DonKT)*10/60) as KM,sum(ct.NLTieuChuan-ct.NLTieuThu) as NLLoiLo"
                + " from DoanThongVI dt inner join DoanThongVICT ct on dt.CoBaoID=ct.CoBaoID where TaiXe1ID!='' AND dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT"
                + " group by dt.DauMayID,dt.SoCB,dt.NgayCB,dt.TaiXe1ID,dt.TaiXe1Name,dt.MaCB,ct.MacTauID,ct.TinhChatID"
                + " union all"
                + " select dt.DauMayID,dt.SoCB,dt.NgayCB,dt.PhoXe1ID as TaiXeID,dt.PhoXe1Name as TaiXeName,10 as LT,1 as PT"
                + " ,dt.MaCB,'' as MacTauID,'' as TinhChatID,0 as GioDM,0 as KM,0 as NLLoiLo"
                + " from DoanThongVI dt where PhoXe1ID!='' AND dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT"
                + " union all"
                + " select dt.DauMayID,dt.SoCB,dt.NgayCB,dt.TaiXe2ID as TaiXeID,dt.TaiXe2Name as TaiXeName,2 as LT,10 as PT"
                + " ,dt.MaCB,'' as MacTauID,'' as TinhChatID,0 as GioDM,0 as KM,0 as NLLoiLo"
                + " from DoanThongVI dt where TaiXe2ID!='' AND dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT"
                + " union all"
                + " select dt.DauMayID,dt.SoCB,dt.NgayCB,dt.PhoXe2ID as TaiXeID,dt.PhoXe2Name as TaiXeName,10 as LT,2 as PT"
                + " ,dt.MaCB,'' as MacTauID,'' as TinhChatID,0 as GioDM,0 as KM,0 as NLLoiLo"
                + " from DoanThongVI dt where PhoXe2ID!='' AND dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT"
                + ") g order by g.DauMayID,g.SoCB,g.NgayCB,g.LT,g.MacTauID,g.TinhChatID";
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

    public DataSet GetBCGioDonDS(int thangDT, int namDT)
    {        
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT ct.GaXPID,dt.LoaiMayID,sum(ct.DonXP+ct.DonDD+ct.DonKT) as GioDon "
                    + "FROM DoanThongVI dt inner join DoanThongVICT ct on ct.CoBaoID=dt.CoBaoID WHERE dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT "
                    + "group by ct.GAXPID,dt.LoaiMayID having sum(ct.DonXP+ct.DonDD+ct.DonKT)>0 order by ct.GAXPID,dt.LoaiMayID";                
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
    
    public DataSet GetBCKTKTDS(string tableName, int thangDT, int namDT, int khoDuong, string loaiMay, int tuyen)
    {
        int maDV = 0;
        if (tableName == "DoanThongYV")
            maDV = 4;
        if (tableName == "DoanThongHN")
            maDV = 5;
        if (tableName == "DoanThongVI")
            maDV = 6;
         if (tableName == "DoanThongDN")
            maDV = 7;
        if (tableName == "DoanThongSG")
            maDV = 8;
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText=string.Empty;
                if (maDV == 7)
                {
                    commandText = "select mact as CTAC,matc as CTACP,sum(lh+dungxp+dungcc+donxp+doncc+dungdm+dungdn) as GioDM,sum(lh) as GioLH,sum(lh-dondd-dungdd) as GioDT,sum(dungdd+dondd) as GioDungDonDD"
                    + ",sum(dungcc) as GioDungQD,sum(dungcc+doncc) as GioDungDonQD,sum(dungxp) as GioDungDM,sum(dungxp+donxp) as GioDungDonDM ,sum(dungxp+dctlmdm+dctlmdn+dungdd+dungcc) as DungTD"
                    + ",sum(donxp+dondd+doncc) as DonTD,sum(solanrk) as SLRK,CONVERT(DECIMAL(18,2), 0) as KMDW,CONVERT(DECIMAL(18,2), 0) as KMCH,CONVERT(DECIMAL(18,2), 0) as KMPT "
                    + ",sum(KM) as KM,CONVERT(DECIMAL(18,2), 0) as TKCH,sum(tkm) as TanKM "
                    + "FROM DoanThongDN WHERE ThangDT =@ThangDT AND NamDT=@NamDT ";
                }
                else
                {
                    commandText = "select CTAC,CTACP";
                    if (maDV == 8)                        
                        commandText += ",sum(GIDT+DGXP+DGCC+DGDD+DNXP+DNDD+DNCC+DGQD+DGKB) as GioDM";                    
                     else
                        commandText += ",sum(GIQV) as GioDM";
                    commandText += ",sum(GILH) as GioLH,sum(GIDT) as GioDT,sum(DGDD+DNDD) as GioDungDonDD";

                    if (maDV == 8)
                        commandText += ",sum(DGQD) as GioDungQD";
                    else if (maDV == 6)
                        commandText += ",sum(DGDN+DGKN+TGTNN+DGQD) as GioDungQD";
                    else
                        commandText += ",sum(DGDN+DGKN) as GioDungQD";
                    if (maDV == 4|| maDV == 5)
                        commandText += ",sum(DGQD+DGCC+DNCC) as GioDungDonQD";
                    else if (maDV == 6)
                        commandText += ",sum(DGQD+DGCC+DNCC+TGTNN) as GioDungDonQD";
                    else
                        commandText += ",sum(DGCC+DNCC) as GioDungDonQD";
                    if (maDV == 8)
                        commandText += ",sum(DGKB) as GioDungDM";
                    else if (maDV == 6)
                        commandText += ",sum(DGDM+DGKM+TGTNM) as GioDungDM";
                    else
                        commandText += ",sum(DGDM+DGKM) as GioDungDM";
                    commandText += ",sum(DGXP) as GioDungDonDM";
                    if (maDV == 6)
                        commandText += ",sum(DGXP+DGDD+DGCC+DGQD+DGDN+DGKN+DGDM+DGKM+TGTNM+TGTNN) as DungTD";
                    else if (maDV == 8)
                        commandText += ",sum(DGXP+DGDD+DGCC+DGQD+DGDN+DGKN+DGDM+DGKM+DGKB) as DungTD";
                    else
                        commandText += ",sum(DGXP+DGDD+DGCC+DGQD+DGDN+DGKN+DGDM+DGKM) as DungTD";                    
                    commandText += ",sum(DNXP+DNDD+DNCC) as DonTD,sum(SLRK) as SLRK,sum(KMDW) as KMDW,sum(KMCH) as KMCH";
                    if (maDV == 6)
                        commandText += ",sum(KMDW+KMGH+KMDY+KMNG+KMDD) as KMPT";
                    else
                        commandText += ",sum(KMDW+KMGH+KMDY) as KMPT";
                    commandText += ",sum(KMCH+KMDW+KMGH+KMDY) as KM,sum(TKCH) as TKCH,sum(TKCH+TKDW+TKGH+TKDY) as TanKM "
                    + "FROM " + tableName + " WHERE ThangDT =@ThangDT AND NamDT=@NamDT ";
                }
                if (khoDuong > 0)
                {
                    if (loaiMay != "ALL")
                    {
                        if (maDV == 7)
                            commandText += " AND MaDM in (SELECT DauMayID FROM DauMay dm LEFT JOIN LoaiMay lm ON dm.LoaiMayID=lm.LoaiMayID WHERE dm.LoaiMayID=@LoaiMay  AND dm.MaDV=@MaDV AND lm.KhoDuong=@KhoDuong)";
                        else
                            commandText += " AND dmay in (SELECT DauMayID FROM DauMay dm LEFT JOIN LoaiMay lm ON dm.LoaiMayID=lm.LoaiMayID WHERE dm.LoaiMayID=@LoaiMay AND dm.MaDV=@MaDV AND lm.KhoDuong=@KhoDuong)";
                        db.AddParameter("@LoaiMay", loaiMay);                       
                    }
                    else
                    {
                        if (maDV == 7)
                            commandText += " AND MaDM in (SELECT DauMayID FROM DauMay dm LEFT JOIN LoaiMay lm ON dm.LoaiMayID=lm.LoaiMayID WHERE  dm.MaDV=@MaDV AND lm.KhoDuong=@KhoDuong)";
                        else
                            commandText += " AND dmay in (SELECT DauMayID FROM DauMay dm LEFT JOIN LoaiMay lm ON dm.LoaiMayID=lm.LoaiMayID WHERE  dm.MaDV=@MaDV AND lm.KhoDuong=@KhoDuong)";
                    }
                    db.AddParameter("@MaDV", maDV);
                    db.AddParameter("@KhoDuong", khoDuong);
                }
                else
                {
                    if (loaiMay != "ALL")
                    {
                        if (maDV == 7)
                            commandText += " AND MaDM in (SELECT DauMayID FROM DauMay WHERE LoaiMayID=@LoaiMay AND MaDV=@MaDV)";
                        else
                            commandText += " AND dmay in (SELECT DauMayID FROM DauMay WHERE LoaiMayID=@LoaiMay AND MaDV=@MaDV)";
                        db.AddParameter("@LoaiMay", loaiMay);
                        db.AddParameter("@MaDV", maDV);
                    }
                }

                if (tuyen > 0)
                {
                    if (maDV == 4 || maDV == 5)
                    {
                        if (maDV == 4)
                            commandText += "AND MDOAN=@Tuyen ";
                        else
                            commandText += "AND PDOAN=@Tuyen ";
                        db.AddParameter("@Tuyen", tuyen);
                    }
                    else if (maDV == 5 && tuyen == 7)
                    {
                        commandText += "AND KDOAN='GB-LT' ";
                    }
                    else if (tuyen > 1)//Khác tuyến saigon
                        commandText += "AND NgayTH > GETDATE() ";
                }
                 if(maDV==7)
                     commandText += "group by mact,matc order by mact,matc";
                 else
                     commandText += "group by CTAC,CTACP order by CTAC,CTACP";                
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

    public DataSet GetBCTongHopDS(string tableName, int thangDT, int namDT, string loaiMay)
    {
        int maDV = 0;
        if (tableName == "DoanThongYV")
            maDV = 4;
        if (tableName == "DoanThongHN")
            maDV = 5;
        if (tableName == "DoanThongVI")
            maDV = 6;
        if (tableName == "DoanThongDN")
            maDV = 7;
        if (tableName == "DoanThongSG")
            maDV = 8;
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = string.Empty;
                if (maDV == 7)
                {
                    commandText = "select dt.MaCT as CTAC,left(dt.MaTC,1) as CTACP,dt.MaTC as TCHAT,dm.LoaiMayID,sum(dt.lh+dt.dungxp+dt.dungcc+dt.donxp+dt.doncc+dt.dungdm+dt.dungdn) as GioDM,sum(dt.lh) as GioLH"
                    + ",sum(dt.dungxp+dt.dctlmdm+dt.dctlmdn+dt.dungdd+dt.dungcc) as DungTD,sum(dt.donxp+dt.dondd+dt.doncc) as DonTD,CONVERT(DECIMAL(18,2), 0) as KMCH,CONVERT(DECIMAL(18,2), 0) as KMPT,sum(dt.KM) as KM,sum(dt.tkm) as TanKM"
                    + ",SUM(dt.NLTT) as NLTT,sum(dt.NLTC) as NLTC"
                    + " FROM DoanThongDN dt left join DauMay dm on dt.MADM=dm.DauMayID WHERE dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT AND dm.MaDV=@MaDV";

                    if (loaiMay != "ALL")
                    {                       
                         commandText += " AND dm.LoaiMayID=@LoaiMay";                       
                        db.AddParameter("@LoaiMay", loaiMay);
                        
                    }
                }
                else
                {
                    commandText = "select dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID";
                    if (maDV == 8)
                        commandText += ",sum(dt.GIDT+dt.DGXP+dt.DGCC+dt.DGDD+dt.DNXP+dt.DNDD+dt.DNCC+dt.DGQD+dt.DGKB) as GioDM";
                    else
                        commandText += ",sum(dt.GIQV) as GioDM";
                    commandText += ",sum(dt.GILH) as GioLH";
                    if (maDV == 6)
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM+dt.TGTNM+dt.TGTNN) as DungTD";
                    else if (maDV == 8)
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM+dt.DGKB) as DungTD";
                    else
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM) as DungTD";  
                    commandText += ",sum(dt.DNXP+dt.DNDD+dt.DNCC) as DonTD,sum(dt.KMCH) as KMCH";
                    if (maDV == 6)
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY+dt.KMNG+dt.KMDD) as KMPT,sum(dt.KMCH+dt.KMDW+dt.KMGH+dt.KMDY+dt.KMNG+dt.KMDD) as KM";
                    else
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY) as KMPT,sum(dt.KMCH+dt.KMDW+dt.KMGH+dt.KMDY) as KM";
                    commandText += ",sum(dt.TKCH+dt.TKDW+dt.TKGH+dt.TKDY) as TanKM";
                    if (maDV == 4 || maDV == 5)
                        commandText += ",sum(dt.SLTT+dt.SLDL_M) as NLTT";
                    else if (maDV == 8)
                        commandText += ",sum(dt.SLTT+dt.slsd) as NLTT";
                    else
                        commandText += ",SUM(dt.SLTT) as NLTT";
                    commandText += ",sum(dt.SLTC) as NLTC"
                    + " FROM " + tableName + " dt left join DauMay dm on dt.DMAY=dm.DauMayID WHERE dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT AND dm.MaDV=@MaDV";
                    if (loaiMay != "ALL")
                    {
                        commandText += " AND dm.LoaiMayID=@LoaiMay";
                        db.AddParameter("@LoaiMay", loaiMay);

                    }
                }
                if (maDV == 7)
                    commandText += " group by dt.MACT,left(dt.MaTC,1),dt.MaTC,dm.LoaiMayID order by dt.MACT,left(dt.MaTC,1),dt.MaTC,dm.LoaiMayID";
                else
                    commandText += " group by dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID order by dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID";
                db.AddParameter("@MaDV", maDV);
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

    public DataSet GetBCSPCDDS(string tableName, int thangDT, int namDT, string loaiMay)
    {
        int maDV = 0;
        if (tableName == "DoanThongYV")
            maDV = 4;
        if (tableName == "DoanThongHN")
            maDV = 5;
        if (tableName == "DoanThongVI")
            maDV = 6;
        if (tableName == "DoanThongDN")
            maDV = 7;
        if (tableName == "DoanThongSG")
            maDV = 8;
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = string.Empty;
                if (maDV == 7)
                {
                    commandText = "select dt.MaCT as CTAC,left(dt.MaTC,1) as CTACP,dt.MaTC as TCHAT,dt.CTY,dt.mactau,sum(dt.lh+dt.dungxp+dt.dungcc+dt.donxp+dt.doncc+dt.dungdm+dt.dungdn) as GioDM,sum(dt.lh) as GioLH"
                    + ",sum(dt.dungxp+dt.dctlmdm+dt.dctlmdn+dt.dungdd+dt.dungcc) as DungTD,sum(dt.donxp+dt.dondd+dt.doncc) as DonTD,CONVERT(DECIMAL(18,2), 0) as KMCH,CONVERT(DECIMAL(18,2), 0) as KMPT,sum(dt.KM) as KM,sum(dt.tkm) as TanKM"
                    + ",SUM(dt.NLTT) as NLTT"
                    + " FROM DoanThongDN dt WHERE dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT";
                }
                else
                {
                    commandText = "select dt.CTAC,dt.CTACP,dt.TCHAT";
                    if (maDV == 5)
                        commandText += ",dt.ctac as CTY";
                    else if (maDV == 8)
                        commandText += ",mdv as CTY";
                    else
                        commandText += ",dt.CTY";
                    if (maDV == 4 || maDV == 5)
                        commandText += ",dt.mtau as mactau";
                    else
                        commandText += ",dt.tau as mactau";
                    if (maDV == 8)
                        commandText += ",sum(dt.GIDT+dt.DGXP+dt.DGCC+dt.DGDD+dt.DNXP+dt.DNDD+dt.DNCC+dt.DGQD+dt.DGKB) as GioDM";
                    else
                        commandText += ",sum(dt.GIQV) as GioDM";
                    commandText += ",sum(dt.GILH) as GioLH";
                    if (maDV == 6)
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM+dt.TGTNM+dt.TGTNN) as DungTD";
                    else if (maDV == 8)
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM+dt.DGKB) as DungTD";
                    else
                        commandText += ",sum(dt.DGXP+dt.DGDD+dt.DGCC+dt.DGQD+dt.DGDN+dt.DGKN+dt.DGDM+dt.DGKM) as DungTD";  
                    commandText += ",sum(dt.DNXP+dt.DNDD+dt.DNCC) as DonTD,sum(dt.KMCH) as KMCH";
                    if (maDV == 6)
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY+dt.KMNG+dt.KMDD) as KMPT,sum(dt.KMCH+dt.KMDW+dt.KMGH+dt.KMDY+dt.KMNG+dt.KMDD) as KM";
                    else
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY) as KMPT,sum(dt.KMCH+dt.KMDW+dt.KMGH+dt.KMDY) as KM";
                    commandText += ",sum(dt.TKCH+dt.TKDW+dt.TKGH+dt.TKDY) as TanKM";
                    if (maDV == 4 || maDV == 5)
                        commandText += ",sum(dt.SLTT+dt.SLDL_M) as NLTT";
                    else if (maDV == 8)
                        commandText += ",sum(dt.SLTT+dt.slsd) as NLTT";
                    else
                        commandText += ",SUM(dt.SLTT) as NLTT";
                    commandText += " FROM " + tableName + " dt WHERE dt.ThangDT =@ThangDT AND dt.NamDT=@NamDT";
                }

                if (loaiMay != "ALL")
                {
                    if (maDV == 7)
                        commandText += " AND MaDM in (SELECT DauMayID FROM DauMay WHERE LoaiMayID=@LoaiMay AND MaDV=@MaDV)";
                    else
                        commandText += " AND dmay in (SELECT DauMayID FROM DauMay WHERE LoaiMayID=@LoaiMay AND MaDV=@MaDV)";
                    db.AddParameter("@LoaiMay", loaiMay);                   
                }

                if (maDV == 7)
                    commandText += " group by dt.MACT,left(dt.MaTC,1),dt.MaTC,dt.CTY,dt.mactau order by dt.MACT,left(dt.MaTC,1),dt.MaTC,dt.CTY,dt.mactau";
                else
                {
                    commandText += " group by dt.CTAC,dt.CTACP,dt.TCHAT";
                    if (maDV == 8)
                        commandText += ",mdv";
                    else if (maDV == 4 || maDV == 6)
                        commandText += ",dt.CTY";
                    if (maDV == 4 || maDV == 5)
                        commandText += ",dt.mtau";
                    else
                        commandText += ",dt.tau";
                    commandText += " order by dt.CTAC,dt.CTACP,dt.TCHAT";
                   if (maDV == 8)
                        commandText += ",mdv";
                    else if (maDV == 4 || maDV == 6)
                        commandText += ",dt.CTY";
                    if (maDV == 4 || maDV == 5)
                        commandText += ",dt.mtau";
                    else
                        commandText += ",dt.tau";
                }                    
                db.AddParameter("@MaDV", maDV);
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

    public DataSet GetBCTHSPTNDS(string tableName, int thangDT, int namDT)
    {
        int maDV = 0;
        if (tableName == "DoanThongYV")
            maDV = 4;
        if (tableName == "DoanThongHN")
            maDV = 5;
        if (tableName == "DoanThongVI")
            maDV = 6;
        if (tableName == "DoanThongDN")
            maDV = 7;
        if (tableName == "DoanThongSG")
            maDV = 8;
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = string.Empty;
                if (maDV == 7)
                {
                    commandText = "SELECT dt.MaCT as CTAC,left(dt.MaTC,1) as CTACP,dt.MaTC as TCHAT,dm.LoaiMayID"
                    + ",SUM(dt.KM) as KMCH,CONVERT(DECIMAL(18,2), 0) as KMPT, SUM(dt.TKM) as TANKM,SUM(dt.DONDD+dt.DONXP+dt.DONCC) as DonTD"
                    + " FROM DoanThongDN dt left join DauMay dm on dt.MADM=dm.DauMayID WHERE dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT AND dm.MaDV=@MaDV"
                    + " group by dt.MACT,left(dt.MaTC,1),dt.MaTC,dm.LoaiMayID order by dt.MACT,left(dt.MaTC,1),dt.MaTC,dm.LoaiMayID";
                }
                else
                {
                    commandText = "SELECT dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID,SUM(dt.KMCH) as KMCH";
                    if (maDV == 6)
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY+dt.KMNG+dt.KMDD) as KMPT";
                    else
                        commandText += ",sum(dt.KMDW+dt.KMGH+dt.KMDY) as KMPT";                                
                    commandText += ",SUM(dt.TKCH+dt.TKDW+dt.TKGH+dt.TKDY) as TANKM,SUM(dt.DNDD+dt.DNXP+dt.DNCC) as DonTD"
                    + " FROM " + tableName + " dt left join DauMay dm on dt.DMAY=dm.DauMayID WHERE dt.ThangDT=@ThangDT AND dt.NamDT=@NamDT AND dm.MaDV=@MaDV"
                    + " group by dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID order by dt.CTAC,dt.CTACP,dt.TCHAT,dm.LoaiMayID";
                }
                db.AddParameter("@MaDV", maDV);
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
}
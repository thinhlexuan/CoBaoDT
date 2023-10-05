using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LookupDB
/// </summary>
public class LookupDB
{
    public DataSet GetLookupDS()
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBAccess db = new DBAccess(CommandType.Text))
            {
                string commandText = "SELECT * FROM QuyenHan;";
                commandText += "SELECT * FROM DonVi;";
                commandText += "SELECT * FROM LoaiMay WHERE Active=1 ORDER BY SoTT;";
                commandText += "SELECT DauMayID,LoaiMayID FROM DauMay WHERE Active=1 ORDER BY DauMayID;";
                commandText += "SELECT * FROM TaiXe WHERE Active=1 ORDER BY TaiXeID;";
                commandText += "SELECT * FROM Ga WHERE Active=1 ORDER BY GaID;";
                commandText += "SELECT * FROM TinhChat ORDER BY TinhChatID;";
                commandText += "SELECT * FROM Tram ORDER BY TramID;";
                commandText += "SELECT MacTauID,CongTacID FROM MacTauVI WHERE Active=1 ORDER BY MacTauID;";
                commandText += "SELECT * FROM Network ORDER BY NodeID;";
                commandText += "SELECT * FROM LyTrinh ORDER BY TuyenID,Km;";
                commandText += "SELECT * FROM HSTanVI ORDER BY LoaiMayID,NgayHL,TanMin;";
                commandText += "SELECT * FROM KhuDoanVI ORDER BY KhuDoan;";
                commandText += "SELECT * FROM DinhMucDBTVI ORDER BY LoaiMayID,NgayHL;";
                commandText += "SELECT * FROM DinhMucNLVI ORDER BY LoaiMayID,NgayHL;";
                commandText += "SELECT * FROM DinhMucKNVI ORDER BY LoaiMayID,NgayHL;";
                commandText += "SELECT * FROM DinhMucMDVI ORDER BY MayChinhID,MayPhuID,NgayHL;";
                commandText += "SELECT * FROM DinhMucMDKVI ORDER BY LoaiMayID,NgayHL;";
                commandText += "SELECT * FROM HeSoMGVI ORDER BY MayChinhID,MayPhuID,NgayHL;";
                commandText += "SELECT * FROM CongTac ORDER BY CongTacID;";
                commandText += "SELECT DISTINCT TenBang FROM NhatKyVI ORDER BY TenBang;";
                commandText += "SELECT * FROM TuyenMap ORDER BY TuyenID;";
                
                ds = db.ExecuteDataSet(commandText);
                ds.Tables[0].TableName = "QuyenHan";
                ds.Tables[1].TableName = "DonVi";
                ds.Tables[2].TableName = "LoaiMay";
                ds.Tables[3].TableName = "DauMay";
                ds.Tables[4].TableName = "TaiXe";
                ds.Tables[5].TableName = "Ga";
                ds.Tables[6].TableName = "TinhChat";
                ds.Tables[7].TableName = "Tram";
                ds.Tables[8].TableName = "MacTau";
                ds.Tables[9].TableName = "Network";
                ds.Tables[10].TableName = "LyTrinh";
                ds.Tables[11].TableName = "HSTanVI";
                ds.Tables[12].TableName = "KhuDoanVI";
                ds.Tables[13].TableName = "DinhMucDBTVI";
                ds.Tables[14].TableName = "DinhMucNLVI";
                ds.Tables[15].TableName = "DinhMucKNVI";
                ds.Tables[16].TableName = "DinhMucMDVI";
                ds.Tables[17].TableName = "DinhMucMDKVI";
                ds.Tables[18].TableName = "HeSoMGVI";
                ds.Tables[19].TableName = "CongTac";
                ds.Tables[20].TableName = "NhatKyVI";
                ds.Tables[21].TableName = "TuyenMap";
            }
        }
        catch
        {
            ds = null;
        }
        return ds;
    }
}
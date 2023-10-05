using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
    public static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["CoBaoDT"].ConnectionString;
    }

    public static string GetConnectionString(string dbName)
    {
        return ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
    }

    public static void AddParameters(DBAccess db, object obj)
    {
        Type type = obj.GetType();
        foreach (var f in type.GetProperties())
        {
            string paramName = string.Format("@{0}", f.Name);
            object paraValue = f.GetValue(obj, null);
            db.AddParameter(paramName, paraValue);
        }
    }

    public static string Decrypt(string strToDecrypt)
    {
        byte[] bytKey = System.Text.Encoding.UTF8.GetBytes("V^r!x@Z#c$a%M~b&h*K(e)$_");
        byte[] bytIV = System.Text.Encoding.UTF8.GetBytes("r~g^p$%b$");
        TripleDESCryptoServiceProvider objTriplesDES = new TripleDESCryptoServiceProvider();
        try
        {
            byte[] bytInput = Convert.FromBase64String(strToDecrypt);
            using (MemoryStream objOutputStream = new MemoryStream())
            {
                //Encrypt the byte array
                CryptoStream objCryptoStream = new CryptoStream(objOutputStream,
                objTriplesDES.CreateDecryptor(bytKey, bytIV), CryptoStreamMode.Write);
                objCryptoStream.Write(bytInput, 0, bytInput.Length);
                objCryptoStream.FlushFinalBlock();
                //return the byte array as a Base64 string
                return Encoding.UTF8.GetString(objOutputStream.ToArray());
            }
        }
        catch (Exception)
        {
            throw new System.Exception("Cửa vé chưa được kích hoạt.");
        }
    }
}
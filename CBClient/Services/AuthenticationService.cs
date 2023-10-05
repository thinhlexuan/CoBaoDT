using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBClient.Adapter;
using CBClient.BLLTypes;
using CBClient.Common;
using CBClient.Models;

namespace CBClient.Services
{
	public class AuthenticationService
	{
        public static async Task<partnerTCTCoBaoByDateOutput> GetListCoBaoDienTuByDate(string NgayBD, string NgayKT,string SoCoBao,string DauMaySo,short? TrangThai, string Username, string access_token = "")
        {
            try
            {
                Common.TimKiemCoBaoByDateInput input = new Common.TimKiemCoBaoByDateInput();
                input.NgayBD = NgayBD;
                input.NgayKT = NgayKT;
                input.SoCoBao = SoCoBao;
                input.DauMaySo = DauMaySo;
                input.Username = Username;
                input.TrangThai = TrangThai;
                input.PageNumber = 1;
                input.PageSize = 1000;
                var response = await CoBaoService.GetListCoBaoDienTuByDate(input, Username, access_token);
                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<IEnumerable<DMTramFuel>> LayThongTinTramFuel(string Username, string access_token = "")
        {
            try
            {                
                var response = await CoBaoService.LayThongTinTramFuel(Username, access_token);
                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<DMNhanVien> GetNhanVienByUsername(NhanVienInput input,string Username, string access_token = "")
        {
            try
            {
                var response = await CoBaoService.GetNhanVienByUsername(input, Username, access_token);
                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<partnerTCTCoBaoByIDOutput> PartnerTCTGetCoBaoDienTuByID(long? CoBaoID, string Username, string access_token = "")
        {
            try
            {
                Common.TimKiemCoBaoByIDInput input = new Common.TimKiemCoBaoByIDInput();
                input.CoBaoID = CoBaoID;                
                var response = await CoBaoService.PartnerTCTGetCoBaoDienTuByID(input, Username, access_token);
                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<CoBaoResult> PartnerTCTFeedBackThanhTichByID(PartnerThanhTichInput input, string Username, string access_token = "")
        {
            try
            {                               
                var response = await CoBaoService.PartnerTCTFeedBackThanhTichByID(input, Username, access_token);
                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<LoginData> Login(string userName, string password, string deviceID)
        {
            try
            {
                var response = await CoBaoService.Login(new LoginInput
                {
                    UserName = userName,
                    Password = password,
                    GrantType = Configuration.GrantType
                }, userName);

                if (response.StatusCode == AdapterStatus.Succcess && response.Data != null)
                {
                    //region add check role API
                    if (string.IsNullOrEmpty(response.Data.roles))
                    {
                        //"Tài khoản này chưa có quyền truy cập hệ thống giao nhận hàng lẻ"
                        return null;
                    }

                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;

            }
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.Models
{
 public	class Configuration
	{
        //public readonly static string UrlApi = "http://dev2.hanghoa.vnticketonline.vn:12001/";//Dev
        //public readonly static string UrlLogin = "http://dev2.hhauth.vnticketonline.vn:10036/auth/";//Dev
        public readonly static string UrlApi = "http://vtds.vn/";//Pro
        public readonly static string UrlLogin = "http://hhauth.vtds.vn/auth/";//Pro
        //public readonly static string UrlCBApi = "http://192.168.222.242:8000/";//Dev        
        //public readonly static string UrlCBApi = "http://192.168.130.200:8000/";//Pro
        //public readonly static string UrlCBApi = "http://multishop.dsvn.vn/";//DS
        public readonly static string UrlCBApi = "http://cobaodientuapi.vtds.vn/";//Pro
        //public readonly static string UrlCBApi = "http://localhost:8000/";//Local
        public readonly static string UrlTkdm = "http://thongkedm.dsvn.vn/";//Pro
        public readonly static string GrantType = "password";
		public readonly static string User_Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36";
	}
}

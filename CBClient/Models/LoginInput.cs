using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.Models
{
	public class LoginInput
    {
		public string GrantType { get; set; }
		public string ClientID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string DeviceKey { get; set; }
		public string DeviceSecret { get; set; }
		public string ClientSecret { get; set; }

	}
}

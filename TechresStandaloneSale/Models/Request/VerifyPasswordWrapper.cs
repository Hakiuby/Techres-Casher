using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class VerifyPasswordWrapper
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "verify_code")]
        public string VerifyCode { get; set; }
        [JsonProperty(PropertyName = "new_password")]
        public string NewPassword { get; set; }
        public VerifyPasswordWrapper(string username,string verifyCode, string newPassword)
        {
            UserName = username;
            VerifyCode = verifyCode;
            NewPassword = newPassword;
        }
    }
}

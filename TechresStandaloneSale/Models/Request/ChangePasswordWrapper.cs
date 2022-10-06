using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class ChangePasswordWrapper
    {
        [JsonProperty(PropertyName = "old_password")]
        public string OldPassword { get; set; }
        [JsonProperty(PropertyName = "new_password")]
        public string NewPassword { get; set; }
        
        public ChangePasswordWrapper(string oldPassword, string newPassword)
        {
            //this.NewPassword = newPassword;
            //this.OldPassword = oldPassword;
            this.NewPassword = Utils.Utils.Base64Encode(newPassword);
            this.OldPassword = Utils.Utils.Base64Encode(oldPassword); 

        }
    }
}

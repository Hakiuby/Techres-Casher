using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class UploadImagesFoodWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("list")]
        public List<UploadImagesFood> List { get; set; }
        public UploadImagesFoodWrapper( long branchId, List<UploadImagesFood> uploadImagesFoods)
        {
            BranchId = branchId;
            List = uploadImagesFoods == null ? new List<UploadImagesFood>() : uploadImagesFoods;
        }
    }
    public class UploadImagesFood
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("avatar-thump")]
        public string AvatarThump { get; set; }
        public UploadImagesFood ( string code, string avatar, string avatarThump)
        {
            Code = code;
            Avatar = string.IsNullOrEmpty(avatar) ? string.Empty : avatar; 
            AvatarThump = string.IsNullOrEmpty(avatarThump) ? string.Empty: avatarThump;
        }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
   public class UserResponse : BaseResponse
    {
        [JsonProperty("data")]
        public User Data { get; set; }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class BaseResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }


    }
}

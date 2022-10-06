using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{

    public class OrderSession : BaseResponse
    {
        [JsonProperty("data")]
        public CheckSessionData Data { get; set; }
    }
    public class CheckSessionData
    {
        [JsonProperty("type")]
        public long Type { get; set; }
        [JsonProperty("order_session_id")]
        public long OrderSessionId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

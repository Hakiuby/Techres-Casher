
using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class ConfigNode
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("api_chat")]
        public string ApiChat { get; set; }

        [JsonProperty("api_lucky_wheel")]
        public string ApiLuckyWheel { get; set; }

        [JsonProperty("api_ads")]
        public string ApiAds { get; set; }
        [JsonProperty("api_log")]
        public string ApiLog { get; set; }
    }
    public class ConfigNodeResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "data")]
        public ConfigNode Data { get; set; }
    }
}

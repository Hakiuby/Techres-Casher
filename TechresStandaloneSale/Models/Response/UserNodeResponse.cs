using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class UserNodeResponse : BaseResponse
    {
        [JsonProperty("data")]
        public UserNode Data { get; set; }
    }
    public class UserNode
    {
        [JsonProperty("node_access_token")]
        public string NodeAccessToken { get; set; }
    }
}

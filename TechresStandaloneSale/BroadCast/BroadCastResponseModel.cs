using Newtonsoft.Json;

namespace TechresStandaloneSale.BroadCast
{
    public class BroadCastResponseModel
    {
        [JsonProperty(PropertyName = "broad_cast_key")]
        public string BroadCastKey { get; set; }
        [JsonProperty(PropertyName = "ip_address")]
        public string IpAdress { get; set; }
        [JsonProperty(PropertyName = "computer_name")]
        public string ComputerName { get; set; }

    }
}

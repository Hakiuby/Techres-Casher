using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{

    public class CancelReason
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonIgnore]
        public bool IsCheckCancel { get; set; }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class PaymentReasonWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

    }
}

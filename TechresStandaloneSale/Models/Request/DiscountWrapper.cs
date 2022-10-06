using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class DiscountWrapper
    {
        [JsonProperty(PropertyName = "discount_type")]
        public int DiscountType { get; set; }

        [JsonProperty(PropertyName = "discount_percent")]
        public float DiscountPercent { get; set; }

        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }
    }
}

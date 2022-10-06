using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class RejectReasonWrapper
    {
        [JsonProperty("restaurant_brand_id")]
        public long RestaurantBrandId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}

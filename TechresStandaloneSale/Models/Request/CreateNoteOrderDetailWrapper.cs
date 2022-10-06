using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
   public class CreateNoteOrderDetailWrapper
    {
        [JsonProperty("restaurant_brand_id")]
        public int BrandId { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("delete")]
        public long Delete { get; set; }
    }
}

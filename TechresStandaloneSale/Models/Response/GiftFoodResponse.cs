using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class GiftFoodResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GiftFoodData Data { get; set; }
    }
    public class GiftFoodData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("list")]
        public List<GiftFood> List { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }

        [JsonProperty("total_quantity")]
        public decimal TotalQuantity { get; set; }

        [JsonProperty("total_amount")]
        public long TotalAmount { get; set; }

        [JsonProperty("total_time")]
        public long TotalTime { get; set; }
    }
}

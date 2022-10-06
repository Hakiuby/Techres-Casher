using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderResponse : BaseResponse
    {
        [JsonProperty("data")]
        public OrderResponseData Data { get; set; }
    }
    public class OrderResponseData : BaseResponse
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("list")]
        public List<Order> List { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
    }
   
}

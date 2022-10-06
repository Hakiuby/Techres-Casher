using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderDetailResponse : BaseResponse
    {
        [JsonProperty("data")]
        public OrderDetailData Data { get; set; }
    }

    public class OrderDetailData : BaseResponse
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("list")]
        public List<OrderDetail> List { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
    }
}

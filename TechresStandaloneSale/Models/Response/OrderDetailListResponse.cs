using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderDetailListResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<OrderDetail> Data { get; set; }
    }
}

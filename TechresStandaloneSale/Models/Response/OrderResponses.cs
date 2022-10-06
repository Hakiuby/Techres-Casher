using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderResponses : BaseResponse
    {
        [JsonProperty("data")]
        public List<Order> Data { get; set; }
    }
}

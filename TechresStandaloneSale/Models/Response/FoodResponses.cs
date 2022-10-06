using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class FoodResponses : BaseResponse
    {
        [JsonProperty("data")]
        public List<Food> Data { get; set; }
    }
}

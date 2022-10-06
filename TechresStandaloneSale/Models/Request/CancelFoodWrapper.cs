using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class CancelFoodWrapper
    {
        [JsonProperty("order_details")]
        public List<CancelFood> CancelFoods { get; set; }
    }
}

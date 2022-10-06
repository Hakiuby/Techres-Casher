using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class CreateSpecialFoodResponse : BaseResponse
    {
        [JsonProperty("data")]
       public CreateSpecialFoodOrderId Data { get; set; }
    }
    public class CreateSpecialFoodOrderId
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}

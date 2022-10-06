using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class ExtraChargeResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<ExtraCharge> Data { get; set; }
    }
    public class ExtraCharge
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("restaurant_extra_charge_id")]
        public long RestaurantExtraChargeId { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }
    }
    public class AddExtraChargeResponse : BaseResponse
    {
        [JsonProperty("data")]
        public BillResponse Data { get; set; }
    }
}

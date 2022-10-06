using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class FoodComboResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<FoodCombo> Data { get; set; }
    }
    public class FoodCombo
    {
        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("food_id")]
        public long FoodId { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("total_original_amount")]
        public decimal TotalOriginalAmount { get; set; }
    }
}

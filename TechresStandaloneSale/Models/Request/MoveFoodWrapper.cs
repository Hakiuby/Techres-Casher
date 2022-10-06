using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class MoveFoodWrapper
    {
        [JsonProperty("from_order_id")]
        public long OrderId { get; set; }
        [JsonProperty("to_table_id")]
        public int TableId { get; set; }

        [JsonProperty("list_food")]
        public List<FoodData> ListFood { get; set; }

    }

    public class FoodData
    {
        [JsonProperty("order_detail_id")]
        public long OrderDetailId { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }

}

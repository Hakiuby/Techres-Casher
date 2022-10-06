using Newtonsoft.Json;
using System;

namespace TechresStandaloneSale.Models
{
    public class ChartFood
    {

        [JsonProperty("name")]
        public int NumberOrder { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("total_quantity")]
        public decimal TotalQuantity { get; set; }
    }
}

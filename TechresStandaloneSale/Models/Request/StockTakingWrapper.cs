using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class StockTakingWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("is_temporary")]
        public long IsTemporary { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("details")]
        public List<Taking> Details { get; set; }

        public class Taking
        {
            [JsonProperty("id")]
            public long Id { get; set; }
            [JsonProperty("action_type")]
            public long ActionType { get; set; }
            [JsonProperty("material_id")]
            public long MaterialId { get; set; }

            //[JsonProperty("quantity")]
            //public decimal Quantity { get; set; }

            [JsonProperty("real_quantity")]
            public decimal RealQuantity { get; set; }
            [JsonProperty("remain_quantity")]
            public decimal RemainQuantity { get; set; }
        }
    }
}

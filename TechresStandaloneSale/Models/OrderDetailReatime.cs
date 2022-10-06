using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
  public  class OrderDetailReatime
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("old_quantity")]
        public float OldQuantity { get; set; }
        [JsonProperty("quantity")]
        public float Quantity { get; set; }
        [JsonProperty("order_detail_status")]
        public long OrderDetailStatus { get; set; }
        [JsonProperty("is_bbq")]
        public bool IsBbq { get; set; }
        [JsonProperty("is_return")]
        public bool IsReturn { get; set; }
        [JsonProperty("object_data")]
        public string ObjectData{ get; set; }
        [JsonProperty("timetick")]
        public string timetick { get; set; }
        [JsonProperty("action_type")]
        public int ActionType { get; set; }

    }
}

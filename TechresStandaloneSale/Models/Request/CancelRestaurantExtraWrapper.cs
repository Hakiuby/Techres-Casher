using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class CancelRestaurantExtraWrapper
    {
        [JsonProperty("order_extra_charge")]
        public long OrderExtraCharge { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        public CancelRestaurantExtraWrapper(long orderExtraCharge, int quantity, string note)
        {
            OrderExtraCharge = orderExtraCharge;
            Quantity = quantity;
            Note = string.IsNullOrEmpty(note) ? "" : note;
        }
    }
}

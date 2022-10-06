using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class OrderRealTimeObj
    {
        public List<OrderRealTimeLST> orderReals { get; set; }
    }
    public class OrderRealTimeLST
    {
        public OrderRealTime orderRealTimes { get; set; }
    }
   public class  OrderRealTime
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("using_slot")]
        public long UsingSlot { get; set; }
        [JsonProperty("total_order_detail_customer_request")]
        public int TotalOrderDetailCustomerRequest { get; set; }

        [JsonProperty("is_change_amount")]
        public bool IsChangeAmount { get; set; }

        [JsonProperty("is_move_table")]
        public bool IsMoveTable { get; set; }

        [JsonProperty("order_status")]
        public long OrderStatus { get; set; }

        [JsonProperty("timetick")]
        public long Timetick { get; set; }
        [JsonProperty("object_data")]
        public string ObjectData { get; set; }
    }
  
}

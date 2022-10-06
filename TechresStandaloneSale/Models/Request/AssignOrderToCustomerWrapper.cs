using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class AssignOrderToCustomerWrapper
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        [JsonProperty("customer_address")]
        public string CustomerAddress { get; set; }
        public AssignOrderToCustomerWrapper(long orderId, long customerId, string customerName, string customerPhone, string customerAddress)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
        }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class CancelFood
    {
        [JsonProperty("order_detail_id")]
        public long OrderDetailId { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        public CancelFood(long orderDetailId, decimal quantity, string reason)
        {
            OrderDetailId = orderDetailId;
            Quantity = quantity;
            Reason = reason;
        }

    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class ChangeSatusOrderDetailWrapper
    {
        [JsonProperty(PropertyName = "order_detail_id")]
        public string OrderDetailId { get; set; }

        [JsonProperty(PropertyName = "order_detail_status")]
        public int OrderDetailStatus { get; set; }
        [JsonProperty(PropertyName = "is_out_stock")]
        public int IsOutStock { get; set; }
        public ChangeSatusOrderDetailWrapper(string orderDetailId, int orderDetailStatus, int isOutStock)
        {
            this.OrderDetailId = orderDetailId;
            this.OrderDetailStatus = orderDetailStatus;
            this.IsOutStock = isOutStock; 
        }
    }
}

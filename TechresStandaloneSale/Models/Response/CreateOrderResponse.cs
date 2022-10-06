using Newtonsoft.Json;
using System.Windows.Documents;

namespace TechresStandaloneSale.Models.Response
{

    public class CreateOrderResponse : BaseResponse
    {
        [JsonProperty("data")]
        public CreateOrderData Data { get; set; }
    }

    public class CreateOrderData
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

    }
}

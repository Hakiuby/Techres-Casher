using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class IsPrintSendFoodCookWrapper
    {
        [JsonProperty("order_detail_ids")]
        public List<long> OrderDetailIds { get; set; }
      
        public IsPrintSendFoodCookWrapper(List<long> orderDetailIds)
        {
            this.OrderDetailIds = orderDetailIds;
            
        }
    }
}

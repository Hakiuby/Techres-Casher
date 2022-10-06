using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class ChangeApprovedDrinkWrapper
    {
        [JsonProperty(PropertyName = "order_detail_id")]
        public string OrderDetailId { get; set; }

        
        public ChangeApprovedDrinkWrapper(string orderDetailId)
        {
            this.OrderDetailId =orderDetailId;
           
        }
    }
}

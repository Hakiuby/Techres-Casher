using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class ReturnBeerWrapper
    {
        [JsonProperty("order_details")]
        public  List<ReturnBeerOrderDetails> OrderDetails { get; set; }
        public ReturnBeerWrapper(List<ReturnBeerOrderDetails> beerOrderDetails)
        {
            this.OrderDetails = beerOrderDetails; 
          
        }
        public class ReturnBeerOrderDetails
        {
            [JsonProperty("order_detail_id")]
            public long OrderDetailsId { get; set; }
            [JsonProperty("quantity")]
            public long Quantity { get; set; }
            public ReturnBeerOrderDetails(long orderDetaisId, long quantity)
            {
                this.OrderDetailsId = orderDetaisId;
                this.Quantity = quantity;
            }

        }
    }
}


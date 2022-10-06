using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class RestaurantTopUpCardWrapper
    {
        [JsonProperty("restaurant_top_up_card_id")]
        public long RestaurantTopUpCardId { get; set; }

        public RestaurantTopUpCardWrapper(long restaurantTopUpCardId)
        {
            RestaurantTopUpCardId = restaurantTopUpCardId;
        }
    }
}

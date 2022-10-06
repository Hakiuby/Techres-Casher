using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class UpdateFoodKitchenWrapper
    {
        [JsonProperty("restaurant_brand_id")]
        public int RestaurantBrandId { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("is_move_urgently")]
        public int IsMoveUrgently { get; set; }
        [JsonProperty("kitchen_place_id")]
        public int KitchenPlaceId { get; set; }
        [JsonProperty("food_ids")]
        public List<long> List { get; set; }
    }
}

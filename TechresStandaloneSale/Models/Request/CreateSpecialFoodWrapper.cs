using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class CreateSpecialFoodWrapper
    {
        [JsonProperty("food_name")]
        public string FoodName { get; set; }
        [JsonProperty("is_allow_print")]
        public long IsAllowPrint { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("restaurant_kitchen_place_id")]
        public long RestaurantKitchenPlaceId { get; set; }
        [JsonProperty("restaurant_vat_config_id")] 
        public long RestaurantVatConfigId { get; set; }
        public CreateSpecialFoodWrapper(string foodName, bool isAllowPrint, string note, decimal price, decimal quantity, long restaurantKitchenPlaceId, long restaurantVatConfigId)
        {
            FoodName = foodName;
            if(isAllowPrint == false)
            {
                IsAllowPrint = 0; 
            }
            else
            {
                IsAllowPrint = 1;
            }
            //IsAllowPrint = isAllowPrint;
            Note = note;
            Price = price;
            Quantity = quantity;
            RestaurantKitchenPlaceId = restaurantKitchenPlaceId;
            RestaurantVatConfigId = restaurantVatConfigId; 
            
        }
    }
    
}

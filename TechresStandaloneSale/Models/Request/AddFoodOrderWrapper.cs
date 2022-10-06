using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class AddFoodOrderWrapper
    {
        [JsonProperty(PropertyName = "foods")]
        public List<BillResponse> Foods { get; set; }
    }

    public class AddFoodWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_gift")]
        public long IsGift { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("is_cook_on_table")]
        public long IsCookOnTable { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("is_selected")]
        public long IsSelected { get; set; }

        [JsonProperty("addition_foods")]
        public List<AddFoodWrapper> AdditionFoods { get; set; }
        [JsonProperty("buy_one_get_one_foods")]
        public List<AddFoodWrapper>BuyOneGetOneFoods { get; set; }
        public AddFoodWrapper(long id, string foodName, bool isGift, decimal quantity, string note, decimal price, List<AddFoodWrapper> wrappers,List<AddFoodWrapper>wrappersPromotion, long isselected)
        {
            Id = id;
            IsGift = isGift ? 1 : 0; 
            Quantity = quantity;
            IsCookOnTable = 0;
            Note = string.IsNullOrEmpty(note) ? "" : note;
            FoodName = string.IsNullOrEmpty(foodName) ? "" : foodName;
            Price = price;
            AdditionFoods = wrappers != null ? wrappers : new List<AddFoodWrapper>();
            BuyOneGetOneFoods = wrappersPromotion != null ? wrappersPromotion : new List<AddFoodWrapper>();
            IsSelected = isselected; 
        }
    }
}

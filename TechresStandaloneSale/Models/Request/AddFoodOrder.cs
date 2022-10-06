using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class AddFoodOrder
    {
        //[JsonProperty("is_use_point")]
        //public long IsUsePoint { get; set; }
        [JsonProperty("foods")]
        public List<ListFoodWrapper> Foods { get; set;  }
       
    }
    public class ListFoodWrapper
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("addition_foods")] 
        public List<AdditionFoodsNew> AdditionFoods { get; set;  }
        [JsonProperty("buy_one_get_one_foods")] 
        public List<BuyOneGetOneFoods> BuyOneGetOneFoods { get; set; }

        public ListFoodWrapper(long id, long quantity, string note, List<AdditionFoodsNew> additionFood, List<BuyOneGetOneFoods> foodPromotions)
        {
            Id = id;
            Quantity = quantity;
            Note = note;
            AdditionFoods = additionFood;
            BuyOneGetOneFoods = foodPromotions; 
        }
    }
    public class AdditionFoodsNew
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
    }
    public class BuyOneGetOneFoods
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class FoodUsing
    {
        [JsonProperty("food_id")]
        public long FoodId { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("is_gift")]
        public int IsGift { get; set; }
        public FoodUsing(long foodId, decimal quantity, int isGift)
        {
            FoodId = foodId;
            Quantity = quantity;
            IsGift = isGift;
        }
    }
}

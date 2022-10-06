using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class FoodRequest
    {
        [JsonProperty("food_id")]
        public string FoodId { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("is_gift")]
        public int IsGift { get; set; }
        public string TotalAmountString
        {
            get
            {
                if (IsGift ==1) return "🎁";
                else return Utils.Utils.FormatMoney(this.TotalAmount);
            }
            set
            {
                TotalAmountString = value;
            }
        }
        public string PriceString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Price);
            }
            set
            {
                PriceString = value;
            }
        }
    }
}

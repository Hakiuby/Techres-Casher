using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class GiftFood
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("discount_price")]
        public long DiscountPrice { get; set; }

        [JsonProperty("is_gift")]
        public long IsGift { get; set; }

        [JsonProperty("is_cook_on_table")]
        public long IsCookOnTable { get; set; }

        [JsonProperty("order_detail_status")]
        public long OrderDetailStatus { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("total_amount")]
        public long TotalAmount { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("food")]
        public Food Food { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("category_type")]
        public long CategoryType { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("payment_date")]
        public string PaymentDate { get; set; }

        [JsonProperty("order_detail_created_at")]
        public string OrderDetailCreatedAt { get; set; }
        public string PriceString
        {
            get
            {
                return string.Format("{0:0,0}", this.Price);
            }
            set
            {
                PriceString = value;
            }
        }
        public string TotalAmountString
        {
            get
            {
                return string.Format("{0:0,0}", this.TotalAmount);
            }
            set
            {
                TotalAmountString = value;
            }
        }
    }
}

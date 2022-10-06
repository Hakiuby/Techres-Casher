using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class ReportRevenueCashier
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("vat")]
        public decimal VAT { get; set; }

        [JsonProperty("revenue")]
        public decimal Revenue { get; set; }

        [JsonProperty("order")]
        public decimal TotalOrder { get; set; }


        [JsonProperty("gift_food")]
        public decimal GiftFood { get; set; }


        [JsonProperty("real_revenue")]
        public decimal TotalRealRevenue { get; set; }


        public string GiftFoodString
        {
            get
            {
                return string.Format("{0:0,0}", this.GiftFood);
            }
            set
            {
                GiftFoodString = value;
            }
        }
        public string TotalRealRevenueString
        {
            get
            {
                return string.Format("{0:0,0}", this.TotalRealRevenue);
            }
            set
            {
                TotalRealRevenueString = value;
            }
        }

        public string DiscountString
        {
            get
            {
                return string.Format("{0:0,0}", this.Discount);
            }
            set
            {
                DiscountString = value;
            }
        }
        public string VATString
        {
            get
            {
                return string.Format("{0:0,0}", this.VAT);
            }
            set
            {
                VATString = value;
            }
        }
        public string RevenueString
        {
            get
            {
                return string.Format("{0:0,0}", this.Revenue);
            }
            set
            {
                RevenueString = value;
            }
        }
    }


}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class FoodModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("is_allow_print")]
        public long IsAllowPrint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("avatar")]
        public string AvatarFood { get; set; }
        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }
        [JsonProperty("category_id")]
        public long CategoryId { get; set; }
        [JsonProperty("category_type")]
        public long CategoryTypeId { get; set; }

        [JsonProperty("category_type_name")]
        public string CategoryTypeName { get; set; }
        [JsonProperty("is_bbq")]
        public long IsBbq { get; set; }
        [JsonProperty("is_sell_by_weight")]
        public long IsSellByWeight { get; set; }
        [JsonProperty("is_combo")]
        public long IsCombo { get; set; }
        [JsonProperty("time_to_completed")]
        public long TimeToCompleted { get; set; }
        [JsonProperty("original_revenue")]
        public decimal OriginalRevenue { get; set; }

        [JsonProperty("original_revenue_percent")]
        public decimal OriginalRevenuePercent { get; set; }

        [JsonProperty("unit_type")]
        public string UnitType { get; set; }
        [JsonProperty("original_price")]
        public decimal OriginalPrice { get; set; }
        [JsonProperty("addition_foods")]
        public List<AdditionFoodModel> AdditionFoods { get; set; }
        [JsonProperty("food_list_in_promotion_buy_one_get_one")]
        public List<FoodInPromotionOneGetOne> FoodInPromotion;
        [JsonProperty("food_in_combo")]
        public List<FoodInCombo> FoodInCombo;

        [JsonIgnore]
        public bool IsSelected { get; set; }
        public string Avatar
        {
            get
            {
                return string.Format("{0}{1}", Constants.ADS_DOMAIN, this.AvatarFood);
            }
            set
            {
                Avatar = value;
            }
        }
        public string FoodCode
        {
            get
            {
                return string.Format("#{0}", this.Id);
            }
            set
            {
                FoodCode = value;
            }
        }
        public string OriginalRevenueString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.OriginalRevenue);
            }
            set
            {
                OriginalRevenueString = value;
            }
        }


        public string OriginalPriceString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.OriginalPrice);
            }
            set
            {
                OriginalPriceString = value;
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
        public override string ToString()
        {
            return Name;
        }
    }

    public class AdditionFoodModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
    public class FoodInPromotionOneGetOne
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
    public class FoodInCombo
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

}

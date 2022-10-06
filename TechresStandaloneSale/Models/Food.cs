using Newtonsoft.Json;
using System.Collections.Generic;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class Food
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("restaurant_brand")]
        public Brand Brand{ get; set; }
        [JsonProperty("list_branch_kitchen")]
        public List<Branch> ListBranchKitchen { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("restaurant_kitchen_place_id")]
        public long RestaurantKitchenPlaceId { get; set; }      

        [JsonProperty("restaurant_kitchen_place_name")]
        public string RestaurantKitchenPlaceName { get; set; }

        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("category_type")]
        public long CategoryTypeId { get; set; }

        [JsonProperty("category_type_name")]
        public string CategoryTypeName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("original_price")]
        public decimal OriginalPrice { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("point_to_purchase")]
        public long PointToPurchase { get; set; }

        [JsonProperty("original_revenue")]
        public decimal OriginalRevenue { get; set; }

        [JsonProperty("original_revenue_percent")]
        public decimal OriginalRevenuePercent { get; set; }

        [JsonProperty("avatar")]
        public string AvatarFood { get; set; }
        [JsonProperty("avatar_thump")]
        public string AvatarThump { get; set; }


        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("time_to_completed")]
        public long TimeToCompleted { get; set; }

        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("is_bbq")]
        public long IsBbq { get; set; }

        [JsonProperty("unit_type")]
        public string UnitType { get; set; }

        [JsonProperty("is_special_claim_point")]
        public long IsSpecialClaimPoint { get; set; }

        [JsonProperty("is_sell_by_weight")]
        public long IsSellByWeight { get; set; }

        [JsonProperty("is_allow_review")]
        public long IsAllowReview { get; set; }

        [JsonProperty("is_allow_print")]
        public long IsAllowPrint { get; set; }

        [JsonProperty("is_allow_purchase_by_point")]
        public long IsAllowPurchaseByPoint { get; set; }

        [JsonProperty("is_take_away")]
        public long IsTakeAway { get; set; }

        [JsonProperty("is_addition")]
        public long IsAddition { get; set; }
        [JsonProperty("is_best_seller")]
        public long IsBestSeller { get; set; }

        [JsonProperty("food_material_type")]
        public long FoodMaterialType { get; set; }


        [JsonProperty("is_allow_employee_gift")]
        public int IsAllowEmployeeGift { get; set; }

        [JsonProperty("addition_foods")]
        public List<BillResponse> AdditionFoods { get; set; }
        [JsonProperty("is_combo")]
        public long IsCombo { get; set; }
        [JsonProperty("food_in_combo")]
        public List<BillResponse> FoodInCombo;

        [JsonProperty("food_list_in_promotion_buy_one_get_one")]
        public List<BillResponse> FoodInPromotion;

        [JsonIgnore]
        public bool IsSelected { get; set; }
        public string Avatar
        {
            get
            {
                return string.Format("{0}{1}",Constants.ADS_DOMAIN, this.AvatarFood);
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
    public class AdditionFoods
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("vat_percent")]
        public long VatPercent { get; set; }
    }
    public class FoodInPromotionBuyOneGetOne
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

}

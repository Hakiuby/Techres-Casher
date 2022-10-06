using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class ReportFoodResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ReportFoodData Data { get; set; }
    }
    public partial class ReportFoodData
    {
        [JsonProperty("sum_quatity")]
        public decimal SumQuatity { get; set; }

        [JsonProperty("sum_total")]
        public decimal SumTotal { get; set; }

        [JsonProperty("sum_total_original")]
        public decimal SumTotalOriginal { get; set; }

        [JsonProperty("sum_profit")]
        public decimal SumProfit { get; set; }

        [JsonProperty("list")]
        public List<ReportFoodItem> List { get; set; }

        [JsonProperty("sum_main_material")]
        public long SumMainMaterial { get; set; }

        [JsonProperty("sum_spice")]
        public long SumSpice { get; set; }

        [JsonProperty("sum_orther_material")]
        public long SumOrtherMaterial { get; set; }
    }

    public class ReportFoodItem
    {
        //[JsonProperty("name")]
        [JsonProperty("food_name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("unit_name")]
        public string Unit { get; set; }

        [JsonProperty("food_avatar")]
        public string Avatar { get; set; }

        [JsonProperty("food_id")]
        public long Id { get; set; }

        [JsonProperty("food_material_type")]
        public long FoodMaterialType { get; set; }

        [JsonProperty("gift_quantity")]
        public float GiftQuantity { get; set; }

        [JsonProperty("canceled_quantity")]
        public float CanceledQuantity { get; set; }

        [JsonProperty("total_amount")]
        public decimal Total { get; set; }

        [JsonProperty("total_original_amount")]
        public decimal TotalOriginal { get; set; }

        [JsonProperty("profit")]
        public decimal Profit { get; set; }

        [JsonProperty("unit_profit")]
        public decimal UnitProfit { get; set; }

        [JsonProperty("profit_ratio")]
        public decimal ProfitRate { get; set; }

        [JsonProperty("is_sell_by_weight")]
        public long IsSellByWeight { get; set; }

        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("main_material_amount")]
        public decimal MainMaterialAmount { get; set; }

        [JsonProperty("spice_amount")]
        public decimal SpiceAmount { get; set; }

        [JsonProperty("orther_material_amount")]
        public decimal OrtherMaterialAmount { get; set; }

        public long Number { get; set; }
        public string QuantityString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Quantity);
            }
            set
            {
                QuantityString = value;
            }
        }
    }
}

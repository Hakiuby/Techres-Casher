using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class OrderDetailPrintResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<OrderDetailPrint> Data { get; set; }
    }
   public class OrderDetailPrint
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }
        [JsonProperty("old_quantity")]
        public float OldQuantity { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }

        [JsonProperty("food_id")]
        public long FoodId { get; set; }

        [JsonProperty("name")]
        public string FoodName { get; set; }

        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("total_quantity_for_drink")]
        public long TotalQuantityForDrink { get; set; }

        [JsonProperty("return_quantity_for_drink")]
        public long ReturnQuantityForDrink { get; set; }

        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("status")]
        public long OrderDetailStatus { get; set; }

        [JsonProperty("is_cook_on_table")]
        public long IsCookOnTable { get; set; }

        [JsonProperty("is_approved_drink")]
        public long IsApprovedDrink { get; set; }

        [JsonProperty("category_type")]
        public long CategoryType { get; set; }

        [JsonProperty("food_type")]
        public long FoodType { get; set; }

        [JsonProperty("cancel_reason")]
        public string CancelReason { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("printed_quantity")]
        public long PrintedQuantity { get; set; }

        [JsonProperty("restaurant_kitchen_place_id")]
        public long RestaurantKitchenPlaceId { get; set; }


        [JsonProperty("order_detail_additions")]
        public List<OrderDetailAddition> OrderDetailAdditions { get; set; }
        [JsonProperty("order_detail_combo_parent_id")]
        public long OrderDetailComboParnetId { get; set; }

        public string FoodNameString
        {
            get
            {
                if (OrderDetailComboParnetId == 0)
                {
                    return FoodName;
                }
                else
                {
                    return string.Format("{0} - COMBO", FoodName);
                }
            }
            set
            {
                FoodNameString = value;
            }
        }
        public string CreatedAtHour
        {
            get
            {
                //05/06/2019 09:06
                if (!string.IsNullOrEmpty(CreatedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetHourFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return "";
            }
            set
            {
                CreatedAtHour = value;
            }
        }
        public string CreatedAtDay
        {
            get
            {
                //05/06/2019 09:06
                // DateTime date = DateTime.ParseExact(CreatedAt, "yyy-MM-dd HH:mm", null);
                if (!string.IsNullOrEmpty(CreatedAt))
                {
                    DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(CreatedAt);
                    if (dateTime != null)
                    {
                        return Utils.Utils.GetDateFormatVN(dateTime);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return "";
            }
            set
            {
                CreatedAtDay = value;
            }
        }
    }
}

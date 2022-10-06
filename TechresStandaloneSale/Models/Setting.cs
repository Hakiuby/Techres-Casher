using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class Setting : BaseResponse
    {
        [JsonProperty("data")]
        public SettingData Data { get; set; }
    }

    public class SettingData
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }

        [JsonProperty("vat")]
        public long Vat { get; set; }

        [JsonProperty("is_working_offline")]
        public bool IsWorkingOffline { get; set; }

        [JsonProperty("is_enable_checkin")]
        public bool IsEnableCheckin { get; set; }
        [JsonProperty("service_restaurant_level_id")]
        public long ServiceRestaurantLevelId { get; set; }

        [JsonProperty("branch_type")]
        public long BranchType { get; set; }

        [JsonProperty("branch_type_name")]
        public string BranchTypeName { get; set; }

        [JsonProperty("branch_type_option")]
        public long BranchTypeOption { get; set; }

        [JsonProperty("open_time")]
        public string OpenTime { get; set; }

        [JsonProperty("close_time")]
        public string CloseTime { get; set; }

        [JsonProperty("hour_to_take_report")]
        public long HourToTakeReport { get; set; }

        [JsonProperty("api_prefix_path_for_branch_type")]
        public string ApiPrefixPathForBranchType { get; set; }

        [JsonProperty("min_distance_checkin")]
        public long MinDistanceCheckin { get; set; }

        [JsonProperty("is_allow_print_temporary_bill")]
        public bool IsAllowPrintTemporaryBill { get; set; }

        [JsonProperty("is_hide_total_amount_before_complete_bill")]
        public bool IsHideTotalAmountBeforeCompleteBill { get; set; }

        [JsonProperty("is_enable_tms")]
        public bool IsEnableTms { get; set; }
        [JsonProperty("is_have_take_away")]
        public bool IsHaveTakeAway { get; set; }

        [JsonProperty("is_enable_membership_card")]
        public bool IsEnableMembershipCard { get; set; }

        [JsonProperty("is_print_bill_logo")]
        public bool IsPrintBillLogo { get; set; }
        [JsonProperty("is_hide_category_type_food")]
        public bool IsHideCategoryTypeFood { get; set; }
        [JsonProperty("is_hide_category_type_drink")]
        public bool IsHideCategoryTypeDrink { get; set; }
        [JsonProperty("is_hide_category_type_other")]
        public bool IsHideCategoryTypeOther { get; set; }
        [JsonProperty("is_hide_category_type_sea_food")]
        public bool IsHideCategoryTypeSeaFood { get; set; }
        [JsonProperty("branch_info")]
        public BranchInfo BranchInfo { get; set; }
    }
    public class BranchInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set;  }
        [JsonProperty("address")]
        public string Address { get; set; } 
    }
}

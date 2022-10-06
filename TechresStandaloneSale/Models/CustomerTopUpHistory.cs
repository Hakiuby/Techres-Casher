using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class CustomerTopUpHistory
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("point")]
        public long Point { get; set; }
        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("customer_avatar")]
        public string CustomerAvatar { get; set; }
        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }
        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("branch_name")]
        public string BranchName { get; set; }
        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
        [JsonProperty("restaurant_top_up_card_id")]
        public long RestaurantTopUpCardId { get; set; }
        [JsonProperty("bonus_amount")]
        public decimal BonusAmount { get; set; }
        [JsonProperty("bonus_point")]
        public long BonusPoint { get; set; }
        [JsonProperty("total_point")]
        public long TotalPoint { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        public string AmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Amount);
            }
        }
        public string BonusAmountString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.BonusAmount);
            }
        }
    }
}

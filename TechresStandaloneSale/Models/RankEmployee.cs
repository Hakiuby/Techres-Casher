using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class RankEmployee
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("table_number")]
        public long TableNumber { get; set; }
        [JsonProperty("role_id")]
        public long RoleId { get; set; }
        [JsonProperty("role_name")]
        public string RoleName { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("bonus_point")]
        public decimal BonusPoint { get; set; }

        [JsonProperty("total_point")]
        public decimal TotalPoint { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        public string AmountString
        {
            get
            {

                return Utils.Utils.FormatMoney(this.Amount);
            }
            set
            {
                AmountString = value;
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

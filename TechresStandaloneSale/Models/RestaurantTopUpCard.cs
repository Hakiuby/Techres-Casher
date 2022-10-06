using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
   public class RestaurantTopUpCard
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("point")]
        public long Point { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("accumulate_point")]
        public long AccumulatePoint { get; set; }

        [JsonProperty("promotion_point")]
        public long PromotionPoint { get; set; }

        [JsonProperty("alo_point")]
        public long AloPoint { get; set; }

        [JsonProperty("total_all_point")]
        public long TotalAllPoint { get; set; }

        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }

        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("restaurant_avatar")]
        public string RestaurantAvatar { get; set; }

        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }

        [JsonProperty("restaurant_info")]
        public string RestaurantInfo { get; set; }

        [JsonProperty("restaurant_membership_card_name")]
        public string RestaurantMembershipCardName { get; set; }

        [JsonProperty("restaurant_membership_card_color_hex_code")]
        public string RestaurantMembershipCardColorHexCode { get; set; }

        [JsonProperty("restaurant_address")]
        public string RestaurantAddress { get; set; }

        [JsonProperty("restaurant_phone")]
        public string RestaurantPhone { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("branch_count")]
        public long BranchCount { get; set; }

        [JsonProperty("average_rate")]
        public float AverageRate { get; set; }
    }
}

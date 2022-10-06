using Newtonsoft.Json;
using System;

namespace TechresStandaloneSale.Models
{
    public class Restaurant
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("google_store_link")]
        public string GoogleStoreLink { get; set; }

        [JsonProperty("apple_store_link")]
        public string AppleStoreLink { get; set; }

        [JsonProperty("is_enable_auto_update_app")]
        public long IsEnableAutoUpdateApp { get; set; }

        [JsonProperty("expired_date")]
        public DateTime? ExpiredDate { get; set; }

        [JsonProperty("is_party")]
        public int IsParty { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class PrivateAdvertsResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<PrivateAdvertsData> Data { get; set; }
    }
    public class PrivateAdvertsData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }
        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }
        [JsonProperty("restaurant_brand_id")]
        public long RestaurantBrandId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }
        [JsonProperty("media_length_by_second")]
        public long MediaLengthBySecond { get; set; }
        [JsonProperty("is_running")]
        public long IsRunning { get; set; }
        [JsonProperty("media_type")]
        public long MediaType { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

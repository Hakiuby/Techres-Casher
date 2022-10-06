using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class RestaurantExtraResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<RestaurantExtra> Data { get; set; }
    }
    public partial class RestaurantExtra
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("restaurant_id")]
        public int RestaurantId { get; set; }

        [JsonProperty("restaurant_brand_id")]
        public int RestaurantBrandId { get; set; }

        [JsonProperty("restaurant_brand_name")]
        public string RestaurantBrandName { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}

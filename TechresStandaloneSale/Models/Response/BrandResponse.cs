using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class BrandResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Brand> Data { get; set; }
    }
    public class BrandOneResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Brand Data { get; set; }
    }
    public class Brand
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("restaurant_id")]
        public int RestaurantId { get; set; }

        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        public Brand(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Brand()
        {

        }
    }
}

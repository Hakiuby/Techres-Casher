using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class OutOfFood
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("food_id")]
        public int FoodId { get; set; }
        [JsonProperty("restaurant_id")]
        public int RestaurantId { get; set; }
        [JsonProperty("restaurant_brand_id")]
        public int RestaurantBrandId { get; set; }
        [JsonProperty("branch_id")]
        public int BranchId { get; set; }
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("is_out_stock")]
        public int IsOutStock { get; set; }


    }
}

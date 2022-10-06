using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class FoodOutOfSockItem
    {
        [JsonProperty("branch_id")]
        public string BrandId { get; set; }
        [JsonProperty("food_ids")]
        public int FoodIds { get; set; }
        public List<OutOfFood> outOfFoods { get; set; }
        //public FoodOutOfSockItem(int foodid, List<OutOfFood> outs)
        //{
        //    FoodId = foodid;
        //    outOfFoods = outs;
        //}
    }
}

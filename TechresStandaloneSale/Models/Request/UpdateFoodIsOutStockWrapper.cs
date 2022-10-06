using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class UpdateFoodIsOutStockWrapper
    {
        [JsonProperty(PropertyName = "branch_id")]
        public string BranchId { get; set; }
        [JsonProperty(PropertyName ="food_id")]
        public List<Int32> FoodIds { get; set; }

        public UpdateFoodIsOutStockWrapper(int brandid, List<Int32> foodids)
        {
            BranchId = brandid.ToString();
            FoodIds = foodids; 
        }
       
    }
}

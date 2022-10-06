using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class OutOfFoodResponse : BaseResponse
    {
        [JsonProperty("data")]
        public OutOfFoodData Data { get; set; }
    }
    public class OutOfFoodData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
        [JsonProperty("list")]
        public List<OutOfFood> List { get; set; }
    }
}

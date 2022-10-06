using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class ChartResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ChartData Data { get; set; }
    }
    public class ChartData:BaseResponse
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("detail")]
        public List<Chart> Detail { get; set; }
    }
}

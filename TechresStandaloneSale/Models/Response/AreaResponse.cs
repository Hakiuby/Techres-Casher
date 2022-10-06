using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class AreaResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Area> Data { get; set; }
    }

    //public class AreaData
    //{

    //    [JsonProperty("limit")]
    //    public double Limit { get; set; }

    //    [JsonProperty("list")]
    //    public List<Area> Lists { get; set; }

    //    [JsonProperty("total_record")]
    //    public double TotalRecord { get; set; }
    //}
}

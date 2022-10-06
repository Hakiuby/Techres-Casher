using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class MergeTableResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<MergeTable> Data { get; set; }
    }
    //public class MergeTableData
    //{
    //    [JsonProperty("limit")]
    //    public long Limit { get; set; }
    //    [JsonProperty("total_record")]
    //    public long TotalRecord { get; set; }
    //    [JsonProperty("list")]
    //    public List<MergeTable> List { get; set; }

    //}
    public class TableBookingResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<MergeTable> Data { get; set; }
    }
}

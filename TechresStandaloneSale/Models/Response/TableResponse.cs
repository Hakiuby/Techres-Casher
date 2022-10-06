using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class TableResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Table> Data { get; set; }
    }

    public class TableData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
        [JsonProperty("list")]
        public List<Table> ListData { get; set; }

    }
    public class TableTakeAwayResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Table> Data { get; set; }
    }
}

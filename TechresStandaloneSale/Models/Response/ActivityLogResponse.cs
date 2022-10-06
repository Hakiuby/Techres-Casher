using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class ActivityLogResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ActivityLogData Data { get; set; }
    }

    public class ActivityLogData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
        [JsonProperty("list")]
        public List<ActivityLog> ActivityLogDatas { get; set; }

    }
}

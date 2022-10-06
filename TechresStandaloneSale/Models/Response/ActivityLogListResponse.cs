using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class ActivityLogListResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ActivitiLogDataList Data { get; set; }
    }

    public class ActivitiLogDataList
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
        [JsonProperty("list")]
        public List<ActivityLog> ActivityLogs { get; set; }
    }
}

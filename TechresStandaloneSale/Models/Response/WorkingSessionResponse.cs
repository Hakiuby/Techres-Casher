using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class WorkingSessionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public WorkingSessionData Data { get; set; }
    }
    public class WorkingSessionData
    {
        [JsonProperty("limit")]
        public double Limit { get; set; }

        [JsonProperty("list")]
        public List<WorkingSession> Lists { get; set; }

        [JsonProperty("total_record")]
        public double TotalRecord { get; set; }
    }

    public class WorkingSessionResponses : BaseResponse
    {
        [JsonProperty("data")]
        public List<WorkingSession> Data { get; set; }
    }
}

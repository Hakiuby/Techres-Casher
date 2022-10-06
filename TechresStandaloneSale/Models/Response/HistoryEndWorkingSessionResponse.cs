using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class HistoryEndWorkingSessionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public HistoryEndWorkingSessionData Data { get; set; }
    }
    public class HistoryEndWorkingSessionData
    {
        [JsonProperty("limit")]
        public double Limit { get; set; }

        [JsonProperty("list")]
        public List<RevenueFinishWorkingSession> Lists { get; set; }

        [JsonProperty("total_record")]
        public double TotalRecord { get; set; }
    }
    public class HistoryEndWorkingSessionItemResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RevenueFinishWorkingSession Data { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class DebitResponse : BaseResponse
    {
        [JsonProperty("data")]
        public DebitData Data { get; set; }
    }

    public class DebitData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }


        [JsonProperty("list")]
        public List<Debit> List { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
    }
}

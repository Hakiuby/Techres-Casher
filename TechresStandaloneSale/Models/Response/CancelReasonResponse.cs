using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class CancelReasonResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<CancelReason> Data { get; set; }
    }
}

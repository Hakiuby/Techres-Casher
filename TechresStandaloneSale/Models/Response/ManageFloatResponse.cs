using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class ManageFloatResponse : BaseResponse
    {
        [JsonProperty("data")]
        public decimal Data { get; set; }
    }
}

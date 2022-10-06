using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class BranchResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Branch Data { get; set; }
    }
}

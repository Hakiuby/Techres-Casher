using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class BranchesResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Branch> data { get; set; }
    }
}

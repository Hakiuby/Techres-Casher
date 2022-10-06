using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class EmployeeResponses : BaseResponse
    {
        [JsonProperty("data")]
        public EmployeeResponse Data { get; set; }
    }
    public class EmployeeResponse : BaseResponse
    {
        [JsonProperty("list")]
        public List<Employee> List { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
    }
}

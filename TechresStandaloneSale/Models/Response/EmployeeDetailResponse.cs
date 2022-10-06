using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class EmployeeDetailResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Employee Data { get; set; }
    }
}

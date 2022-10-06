using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class ReportEmployeeResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<EmployeeReport> Data { get; set; }
    }
    public partial class EmployeeReport
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("branch")]
        public Branch Branch { get; set; }

        [JsonProperty("number_employees")]
        public float NumberEmployees { get; set; }

        [JsonProperty("number_female_employees")]
        public float NumberFemaleEmployees { get; set; }

        [JsonProperty("number_male_employees")]
        public float NumberMaleEmployees { get; set; }

        [JsonProperty("number_employees_by_role")]
        public List<NumberEmployeesByRole> NumberEmployeesByRole { get; set; }

        [JsonProperty("number_employees_by_age")]
        public List<NumberEmployeesByAge> NumberEmployeesByAge { get; set; }

        public List<NumberEmployeesByGender> NumberEmployeesByGender { get; set; }

    }

    public partial class NumberEmployeesByAge
    {
        public int stt { get; set; }
        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        public string percent { get; set; }
    }

    public partial class NumberEmployeesByRole
    {
        public int stt { get; set; }
        [JsonProperty("role_id")]
        public long RoleId { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        public string percent { get; set; }
    }

    public partial class NumberEmployeesByGender
    {
        public int stt { get; set; }
        public string Gender { get; set; }
        public float Number { get; set; }
        public string percent { get; set; }
    }
}

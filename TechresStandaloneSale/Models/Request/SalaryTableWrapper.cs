using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class SalaryTableWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
        public SalaryTableWrapper(long branchId, string time,long employeeId)
        {
            BranchId = branchId;
            EmployeeId = employeeId;
            Time = time;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class AddvanedSalaryWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("employee_id")]
        public long EmployeeId { get; set; }
        public AddvanedSalaryWrapper(long branchId, long id, long employeeID)
        {
            BranchId = branchId;
            Id = id;
            EmployeeId = employeeID;
        }
    }
}

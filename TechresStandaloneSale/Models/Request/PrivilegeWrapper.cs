using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class PrivilegeWrapper
    {
        [JsonProperty("privileges")]
        public List<long> Privileges { get; set; }
        [JsonProperty("employee_privilege_group_id")]
        public long EmployeePrivilegeGroupId { get; set; }
        
        public PrivilegeWrapper(List<long> privileges, long employeePrivilegeGroupId)
        {
            Privileges = privileges != null ? privileges : new List<long>();
            EmployeePrivilegeGroupId = employeePrivilegeGroupId;
        }
    }
}

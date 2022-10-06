using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class PrivilegeGroupWrapper
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("privilege_ids")]
        public List<long> PrivilegeIds
        {
            get; set;
        }
        public PrivilegeGroupWrapper(string name, List<long> privilegeIds)
        {
            Name = string.IsNullOrEmpty(name) ? string.Empty : name;
            PrivilegeIds = privilegeIds != null ? privilegeIds : new List<long>();
        }
    }
}

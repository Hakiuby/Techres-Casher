using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class BranchIdWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        public BranchIdWrapper(long id)
        {
            BranchId = id;
        }
    }
}

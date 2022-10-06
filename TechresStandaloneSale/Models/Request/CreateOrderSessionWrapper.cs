using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class CreateOrderSessionWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("order_session_id")]
        public long OrderSessionId { get; set; }
        public CreateOrderSessionWrapper(long branchId, long orderSessionId)
        {
            this.BranchId = branchId;   
            this.OrderSessionId = orderSessionId;   
        }
    }
}

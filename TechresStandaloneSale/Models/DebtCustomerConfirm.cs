using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class DebtCustomerConfirm
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("order_ids")]
        public List<long> OrderIds { get; set; }
    }
}

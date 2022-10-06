using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class CustomerDebitWrapper
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }

        public CustomerDebitWrapper(long branchId,long customerId, long OrderId, string Note)
        {
            this.BranchId = branchId;
            this.CustomerId = customerId;
            this.OrderId = OrderId;
            this.Note = Note;
        }
    }
}

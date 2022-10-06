using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class BookingResonCancelResponse
    {
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        [JsonProperty("cancel_reason")]
        public string CancelReson { get; set; }
        public BookingResonCancelResponse(long branchId, string cancelReson)
        {
            this.BranchId = branchId;
            this.CancelReson = cancelReson; 

        }
    }
    
}

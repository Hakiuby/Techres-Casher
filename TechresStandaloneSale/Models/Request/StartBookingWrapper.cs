using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
    public class StartBookingWrapper
    {
        [JsonProperty("booking_id")]
        public long BookingId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }
        public StartBookingWrapper(long bookingId, long branchId)
        {
            BookingId = bookingId;
            BranchId = branchId;
        }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class CustomerSlotNumberWrapper
    {
        [JsonProperty("customer_slot_number")]
        public int CustomerSlotNumber { get; set; }

    }
}

using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class BookingSession : BaseResponse
    {

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("time")]
        public string BookingTime { get; set; }

        [JsonProperty("booking_id")]
        public long BookingId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        [JsonProperty("action_type")]
        public int ActionType { get; set; }
        public string BookingCode
        {
            get
            {
                return string.Format("#{0}", BookingId);
            }
            set
            {
                BookingCode = value;
            }
        }
        public string AmountString
        {
            get
            {
                return string.Format("{0:0,0}", this.Amount);
            }
            set
            {
                AmountString = value;
            }
        }
    }
}

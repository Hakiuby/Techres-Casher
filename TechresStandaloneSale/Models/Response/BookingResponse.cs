using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class BookingResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Booking Data { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class BookingSessionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<BookingSession> Data { get; set; }
    }
}

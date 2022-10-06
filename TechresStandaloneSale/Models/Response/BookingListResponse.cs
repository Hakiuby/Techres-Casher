using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class BookingListResponse : BaseResponse
    {
        [JsonProperty("data")]
        public BookingData Data { get; set; }
    }
    public class BookingData
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }

        [JsonProperty("list")]
        public List<Booking> List { get; set; }
    }

}

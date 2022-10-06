using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class OrderNoteDetailResponse
    {
        [JsonProperty("data")]
        public List<OrderNoteDetailResponseData> Data { get; set; }
    }
    public class OrderNoteDetailResponseData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}

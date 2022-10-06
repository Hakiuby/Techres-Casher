using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class OpenTableByIdResponse: BaseResponse
    {
        [JsonProperty("data")]
        public OpenTableByIdData Data { get; set; }
    }
    public class OpenTableByIdData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("status_text")]
        public string StatusText { get; set; }
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}

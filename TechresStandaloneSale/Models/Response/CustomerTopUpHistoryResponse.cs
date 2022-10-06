using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class CustomerTopUpHistoryResponse : BaseResponse
    {
        [JsonProperty("data")]
        public CustomerTopUpHistoryData Data { get; set; }
    }
    public class CustomerTopUpHistoryData
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("total_record")]
        public long TotalRecord { get; set; }
        [JsonProperty("list")]
        public List<CustomerTopUpHistory> List { get; set; }
    }
}

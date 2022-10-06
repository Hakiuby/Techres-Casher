using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
  public class LongResponse:BaseResponse
    {
        [JsonProperty("data")]
        public long Data { get; set; }
    }
}

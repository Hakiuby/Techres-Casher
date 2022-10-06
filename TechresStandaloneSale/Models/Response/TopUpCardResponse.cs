using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class TopUpCardResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<TopUpCard> Data { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class KitchenResponse:BaseResponse
    {
        [JsonProperty("data")]
        public List<Kitchen> Data { get; set; }
    }
}

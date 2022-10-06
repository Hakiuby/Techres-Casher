using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class CustomerRegisterResponse: BaseResponse
    {
        [JsonProperty("data")]
        public Customer Data { get; set; }
    }
}

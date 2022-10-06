using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
    public class ManageAdditionFeeResponse : BaseResponse
    {
        [JsonProperty("data")]
        public AdditionFee Data { get; set; }
    }
}

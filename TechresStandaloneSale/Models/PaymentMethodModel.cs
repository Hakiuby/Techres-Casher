using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
  public  class PaymentMethodModel
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        public PaymentMethodModel(string method, string unit, string amount)
        {
            Method = method;
            Amount = amount;
            Unit = unit;
        }
    }
}

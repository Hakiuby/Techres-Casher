using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class PaymentMethodWrapper
    {
        [JsonProperty("payment_method")]
        public int PaymentMethod { get; set; }
        public PaymentMethodWrapper(int paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }
    }
}

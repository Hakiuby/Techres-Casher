using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Request
{
   public class UpdateShippingWrapper
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        public UpdateShippingWrapper( string name, string address, string phone, decimal fee)
        {
            Name = string.IsNullOrEmpty(name) ? "" : name;
            Address = string.IsNullOrEmpty(address) ? "" : address;
            Phone = string.IsNullOrEmpty(phone) ? "" : phone;
            Fee = fee;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
  public  class Customer
    {
        [JsonProperty("customer_id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("customer_address")]
        public string Address { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("qr_code")]
        public string QrCode { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}

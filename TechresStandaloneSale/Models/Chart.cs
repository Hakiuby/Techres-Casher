using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class Chart
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        public string ValueString
        {
            get
            {
                return Utils.Utils.FormatMoney(this.Value);
            }
            set
            {
                ValueString = value;
            }
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class OrderDetailsRealTime
    {
        [JsonProperty("action_type")]
        public int ActionType { get; set;  }
        [JsonProperty("timetick")] 
        public long TimeTick { get; set; }
    }
}

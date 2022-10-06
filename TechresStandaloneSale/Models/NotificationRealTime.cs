using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class NotificationRealTime
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("object_id")]
        public long ObjectId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}

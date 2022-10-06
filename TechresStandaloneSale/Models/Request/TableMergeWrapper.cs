using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Request
{
    public class TableMergeWrapper
    {
        [JsonProperty(PropertyName = "table_ids")]
        public List<int> TableIds { get; set; }
    }
}

using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class TableMoveWrapper
    {
        [JsonProperty(PropertyName = "table_id")]
        public long TableId { get; set; }

    }
}

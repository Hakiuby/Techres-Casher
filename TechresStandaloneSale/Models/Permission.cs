using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class Permission
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_selected")]
        public int IsSelected { get; set; }
    }
}

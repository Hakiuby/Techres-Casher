using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class DetailFood
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

}

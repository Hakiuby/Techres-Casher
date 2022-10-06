using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class BasicModel
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }


        public BasicModel(int value, string content)
        {
            Value = value;
            Content = content;
        }
        public override string ToString()
        {
            return Content;
        }
    }
}

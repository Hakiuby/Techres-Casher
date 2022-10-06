using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class ForgetPasswordWrapper
    {
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        public ForgetPasswordWrapper(string username)
        {
            this.username = username;
        }
    }
}

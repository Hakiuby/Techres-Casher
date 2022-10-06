using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class FindCustomerWrapper
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
        public FindCustomerWrapper(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;
        }
    }

}

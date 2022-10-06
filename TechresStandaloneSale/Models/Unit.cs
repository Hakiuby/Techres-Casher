using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{
    public class Unit : BaseResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Prefix
        {
            get
            {
                return string.IsNullOrEmpty(Name) ? "" : Utils.Utils.ConvertUppercaseToLowercase(Utils.Utils.convertToUnSign3(Name));
            }
        }
        public string NormalizeName
        {
            get
            {
                return string.IsNullOrEmpty(Name) ? "" : Utils.Utils.ConvertToUpperAndLower(Name);
            }
        }
    }
}

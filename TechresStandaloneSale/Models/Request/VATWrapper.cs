using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Request
{
    public class VATWrapper
    {

        [JsonProperty(PropertyName = "is_apply_vat")]
        public int IsApplyVAT { get; set; }
    }
}

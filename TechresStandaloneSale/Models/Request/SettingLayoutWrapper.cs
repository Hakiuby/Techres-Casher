using Newtonsoft.Json;
using System.Windows;

namespace TechresStandaloneSale.Models.Request
{
    public class SettingLayoutWrapper
    {
        [JsonProperty(PropertyName = "is_hidden_header_and_footer")]
        public bool IsHiddenHeaderAndFooter { get; set; }
        [JsonProperty(PropertyName = "is_two_layout")]
        public bool IsTwoLayout { get; set; }
    }
}

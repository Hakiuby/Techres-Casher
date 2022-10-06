using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{
    public class CategoryResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Category> Data { get; set; }
    }
}

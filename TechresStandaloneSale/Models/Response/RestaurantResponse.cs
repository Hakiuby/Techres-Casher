using Newtonsoft.Json;

namespace TechresStandaloneSale.Models.Response
{
    public class RestaurantResponse : BaseResponse
    {
        [JsonProperty("data")]
        public Restaurant Data { get; set; }
    }
}

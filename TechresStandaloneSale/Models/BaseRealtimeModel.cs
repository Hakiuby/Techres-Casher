using Newtonsoft.Json;

namespace TechresStandaloneSale.Models
{
    public class BaseRealtimeModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public OrderDetailReatime Value { get; set; }
    }
    public class BaseRealtimeModelOrder
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public OrderRealTime Value { get; set; }
    }
    public class BaseRealtimeModelOrderDetails
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public Order Value { get; set; }
    }
    public class BaseRealtimeModelPrintOrderDetails
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public OrderDetail Value { get; set; }
    }
    public class BaseRealtimeModelNotifications
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public NotificationRealTime Value { get; set; }
    }

}

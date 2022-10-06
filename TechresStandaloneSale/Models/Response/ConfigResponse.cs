using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models.Response
{

    public class ConfigData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("api_domain")]
        public string ApiDomain { get; set; }

        [JsonProperty("chat_domain")]
        public string ChatDomain { get; set; }

        [JsonProperty("ads_domain")]
        public string AdsDomain { get; set; }

        [JsonProperty("realtime_domain")]
        public string RealtimeDomain { get; set; }

        [JsonProperty("system_server")]
        public string SystemServer { get; set; }

        [JsonProperty("current_domain")]
        public string CurrentDomain { get; set; }

        [JsonProperty("install_app_url")]
        public string InstallAppUrl { get; set; }

        [JsonProperty("date_time")]
        public string DateTime { get; set; }

    }

    public class ConfigResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "data")]
        public ConfigData Data { get; set; }
    }
}

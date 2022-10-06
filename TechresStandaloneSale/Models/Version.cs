using Newtonsoft.Json;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Models
{

    public class Version : BaseResponse
    {
        [JsonProperty("data")]
        public VersionData Data { get; set; }
    }
    public class VersionData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("version")]
        public string VersionName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("is_require_update")]
        public long IsRequireUpdate { get; set; }

        [JsonProperty("download_link")]
        public string DownloadLink { get; set; }
    }
}

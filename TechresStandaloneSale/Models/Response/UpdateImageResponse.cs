using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models.Response
{
   public class UpdateImageResponse:BaseResponse
    {
        [JsonProperty("data")]
        public UpdateImage Data { get; set; }
    }
    public class UpdateImage
    {
        [JsonProperty("link_original")]
        public string Link { get; set; }

        [JsonProperty("link_thumb")]
        public string LinkThumbs { get; set; }
    }
}

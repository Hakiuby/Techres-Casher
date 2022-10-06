using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models.Request
{
   public class CallApiWrapper
    {
        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("params")]
        public object Params { get; set; }

        [JsonProperty("http_method")]
        public long HttpMethod { get; set; }

        [JsonProperty("request_url")]
        public string RequestUrl { get; set; }

        [JsonProperty("os_name")]
        public string OsName { get; set; }

        [JsonProperty("is_production_mode")]
        public long IsProductionMode { get; set; }
        public CallApiWrapper() 
        {
            OsName = "WINDOW-TECHRES-SALE";
            //beta
            IsProductionMode = 0;
            //live
            //IsProductionMode = 1;
        }

        public CallApiWrapper(long projectId, RestRequest request)
        {
            ProjectId = projectId;
            if (request.Method == Method.POST)
            {
                var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
                if (body != null)
                {
                    Params = Utils.Utils.AsObject<object>(body.Value.ToString());
                }
                    HttpMethod = 1;
            }
            else
            {
                List<string> paras = new List<string>();
                foreach (Parameter p in request.Parameters)
                {
                    paras.Add(string.Format("\"{0}\":\"{1}\"", p.Name, p.Value));
                 //   Debug.Write(string.Format("\"{0}\"=\"{1}\"&", p.Name, p.Value));
                }
                
                string std ="{"+ Utils.Utils.convertFormListString(paras) +"}";
                object abc = Utils.Utils.AsObject<object>(std);
                Params = abc;
                HttpMethod = 0;
            }
            RequestUrl = projectId ==(int) ProjectIdEnum.ORDER ? Constants.KEY_CALL_API  + request.Resource: request.Resource;
            OsName = "WINDOW-TECHRES-SALE";
        
            IsProductionMode = Constants.IS_MODE_SERVER;
         }
    }
}

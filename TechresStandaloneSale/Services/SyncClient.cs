using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
  public  class SyncClient : BaseClient
    {
        public SyncClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }
        public BaseResponse GetSyncData()
        {
            RestRequest request = new RestRequest(LinkCallApi.API_SYNC_DATA, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);
        }
    }
}

using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;

namespace TechresStandaloneSale.Services
{
    public class VersionClient : BaseClient
    {
        public VersionClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
       : base(cache, serializer, errorLogger) { }
        public Models.Version GetVersionAPI()
        {
            RestRequest request = new RestRequest(LinkCallApi.API_UPDATE_VERSION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH, request);
            return Get<Models.Version>(request, callApiWrapper);
        }
    }
}

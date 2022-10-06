using RestSharp;
using RestSharp.Deserializers;
using System.Diagnostics;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;

namespace TechresStandaloneSale.Services
{
    public class SettingClient : BaseClient
    {
        public SettingClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }
        public Setting GetSetting(long branchId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_SETTING, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH, request); 
            Debug.WriteLine(request);
            return Get<Setting>(request,callApiWrapper);
           
        }
    }
}

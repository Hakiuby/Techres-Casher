using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
   public class AreasClient : BaseClient
    {
        public AreasClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }
        public AreaResponse GetListAreaes(int brandId,long branchId, int Status,long takeAway)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_AREAES, Method.GET);
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("status", Status.ToString());
            request.AddQueryParameter("is_take_away", takeAway.ToString());
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<AreaResponse>(request, callApiWrapper);
        }
    }
}

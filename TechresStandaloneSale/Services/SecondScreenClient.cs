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
    public class SecondScreenClient: BaseClient
    {
        public SecondScreenClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
            : base(cache, serializer, errorLogger) { }

        public PrivateAdvertsResponse GetListRestaurantPrivateAdverts(long RestaurandBrandId, long status, long MediaType)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_LIST_RESTAURANT_PRIVATE_ADVERTS, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", RestaurandBrandId.ToString());
            request.AddQueryParameter("status", status.ToString());
            request.AddQueryParameter("media_type", MediaType.ToString()); 
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            WriteLog.logs(request.ToString());
            return Get<PrivateAdvertsResponse>(request, callApiWrapper);
        }
    }
}

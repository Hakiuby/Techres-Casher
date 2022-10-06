using CommonServiceLocator;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
   public class RestaurantClient : BaseClient
    {
        public RestaurantClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger) : base(cache, serializer, errorLogger) { }
        public RestaurantResponse getRestaurantInfo(long restaurantId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_RESTAURANT_INFO, restaurantId), Method.GET);
            request.AddHeader("Content-type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RestaurantResponse>(request, callApiWrapper);
        }
        
    }
}

using Newtonsoft.Json;
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
  public  class RestaurantExtraClient : BaseClient
    {
        public RestaurantExtraClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
                : base(cache, serializer, errorLogger) { }

        public RestaurantExtraResponse GetRestaurantExtraResponse()
        {
            RestRequest request = new RestRequest(LinkCallApi.API_RESTAURANT_EXTRA_CHANGE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RestaurantExtraResponse>(request, callApiWrapper);
        }
        public AddExtraChargeResponse AddRestaurantExtra(AddRestaurantExtraWrapper wrapper, long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ADD_RESTAURANT_EXTRA, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<AddExtraChargeResponse>(request, callApiWrapper);
        }
        public BaseResponse CancelRestaurantExtra(CancelRestaurantExtraWrapper wrapper, long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CANCEL_RESTAURANT_EXTRA, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
    }
}

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
    public class UserClient : BaseClient
    {
        public UserClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }

        public ConfigResponse GetConfig(string RestaurantName)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CONFIG, Method.GET);
            request.AddQueryParameter("restaurant_name", RestaurantName);
            request.AddQueryParameter("project_id", "net.techres.order.api");
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper wrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH, request);
            return Get<ConfigResponse>(request, wrapper);
        }
        public UserResponse LoginSystem(string Username, string Password, string ApiKey)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_LOGIN, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", ApiKey));
            Console.WriteLine("123121312312313213213"); 
            LoginWrapper wrapper = new LoginWrapper(Username, Password);
            var js = JsonConvert.SerializeObject(wrapper);
            Console.Write(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH, request);
            return Get<UserResponse>(request, ApiKey, callApiWrapper);
        }

    }
}

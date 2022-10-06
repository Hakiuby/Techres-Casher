using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class UserNodeClient : BaseClient
    {
        public UserNodeClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
        : base(cache, serializer, errorLogger) { }
        public ConfigNodeResponse GetConfig()
        {
            RestRequest request = new RestRequest(LinkCallApi.API_NODE_CONFIG, Method.GET);
            request.AddQueryParameter("secret_key", "cHJvamVjdC51c2VyLmtleSZhYmMkXiYl");
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper wrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH_NODE, request);
            return Get<ConfigNodeResponse>(request, wrapper);
        }
        public UserNodeResponse LoginSystemNode(User user, string password, string ApiKey)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_NODE_LOGIN, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            LoginNodeWrapper wrapper = new LoginNodeWrapper(user, password);
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper call = new CallApiWrapper((long)ProjectIdEnum.OAUTH_NODE, request);

            return Get<UserNodeResponse>(request, ApiKey, call);
        }
    }
}

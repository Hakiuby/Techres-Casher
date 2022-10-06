using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Linq;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class BaseClient : RestSharp.RestClient
    {

        public readonly string TOKEN_NAME = "Authorization";
        protected ICacheService _cache;
        protected IErrorLogger _errorLogger;
        private CallApiWrapper wrapper;
        public BaseClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
        {
            _cache = cache;
            _errorLogger = errorLogger;
            AddHandler("application/json", deserializer: serializer);
            AddHandler("text/json", deserializer: serializer);
            Timeout = 30000;
        }
        private void TimeoutCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                LogError(BaseUrl, request, response);
            }
        }

     
        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            if (Properties.Settings.Default.IsOffline)
            {
                RestRequest callApi = new RestRequest(Constants.SERVER_OAUTH_DOMAIN, Method.POST);
                User user = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                //ConfigResponse config = (ConfigResponse)Utils.Utils.GetCacheValue(Constants.CURRENT_CONFIG); 
                if (user != null)
                {
                    if (wrapper.ProjectId == (long)ProjectIdEnum.ORDER || wrapper.ProjectId == (long)ProjectIdEnum.OAUTH)
                    {
                        callApi.AddHeader("Authorization", user.TokenType + " " + user.AccessToken);
                        WriteLog.logs(string.Format("Authorization: {0} {1}", user.TokenType, user.AccessToken));
                    }
                    else if (wrapper.ProjectId == (long)ProjectIdEnum.LOGS)
                    {
                        callApi.AddHeader("Authorization", user.NodeAccessToken);
                        WriteLog.logs(string.Format("Authorization: {0}", user.NodeAccessToken));
                    }
                    else if(wrapper.ProjectId == (long)ProjectIdEnum.REPORT)
                    {
                        callApi.AddHeader("Authorization", user.NodeAccessToken);
                        WriteLog.logs(string.Format("Authorization: {0}", user.NodeAccessToken)); 
                    }
                }
                
                callApi.AddHeader("Content-Type", "application/json");
                var js = JsonConvert.SerializeObject(wrapper);
                WriteLog.logs(js);
                callApi.AddJsonBody(js);
                var response = base.Execute<T>(callApi);
                TimeoutCheck(request, response);
                return response;
            }
            else
            {
                User user = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                if (user != null)
                {
                    if (wrapper.ProjectId == (long)ProjectIdEnum.ORDER || wrapper.ProjectId == (long)ProjectIdEnum.OAUTH)
                    {
                        request.AddHeader("Authorization", user.TokenType + " " + user.AccessToken);
                        WriteLog.logs(string.Format("Authorization: {0} {1}", user.TokenType, user.AccessToken));
                    }
                    else if (wrapper.ProjectId == (long)ProjectIdEnum.LOGS)
                    {
                        //request.AddHeader("Authorization", user.NodeAccessToken);
                        //WriteLog.logs(string.Format("Authorization: {0}", user.NodeAccessToken));

                        request.AddHeader("Authorization", user.TokenType + " " + user.AccessToken);
                        WriteLog.logs(string.Format("Authorization: {0} {1}", user.TokenType, user.AccessToken));
                    }
                }
                request.Resource = string.Format("{0}/{1}", Constants.SERVER_OFFLINE_DOMAIN, request.Resource);
                var response = base.Execute<T>(request);
                TimeoutCheck(request, response);
                return response;
            }
        }


        public T Get<T>(IRestRequest request, CallApiWrapper _wrapper) where T : new()
        {
            try
            {               
                wrapper = _wrapper ==  null? new CallApiWrapper(): _wrapper;

                var response = Execute<T>(request);
                if ( response != null && response.ResponseUri != null)
                {
                    WriteLog.logs(response.ResponseUri.ToString());
                    WriteLog.logs(response.Content);
                }
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    LogError(BaseUrl, request, response);
                }
                return (T)Convert.ChangeType(response.Data, typeof(T));

            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.Message);
                return default;
            }
        }
        
        public T Get<T>(IRestRequest request, string ApiKey, CallApiWrapper _wrapper) where T : new()
        {
            try
            {
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    if (Properties.Settings.Default.IsOffline)
                    {
                        wrapper = _wrapper == null ? new CallApiWrapper() : _wrapper;

                        RestRequest callApi = new RestRequest(Constants.SERVER_OAUTH_DOMAIN, Method.POST);
                        callApi.AddHeader("Authorization", string.Format("Basic {0}", ApiKey));
                        WriteLog.logs(string.Format("Basic {0}", ApiKey));
                        callApi.AddHeader("Content-Type", "application/json");
                        var js = JsonConvert.SerializeObject(wrapper);
                        WriteLog.logs(js);
                        callApi.AddJsonBody(js);
                        var response = base.Execute<T>(callApi);
                        TimeoutCheck(request, response);
                        WriteLog.logs(response.ResponseUri.ToString());
                        WriteLog.logs(response.Content);
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            LogError(BaseUrl, request, response);
                        }
                        return (T)Convert.ChangeType(response.Data, typeof(T));
                    }
                    else
                    {
                        request.AddHeader("Authorization", string.Format("Basic {0}", ApiKey));
                        WriteLog.logs(string.Format("Basic {0}", ApiKey));
                        request.AddHeader("Content-Type", "application/json");
                        request.Resource = string.Format("{0}/{1}", Constants.SERVER_OFFLINE_DOMAIN, request.Resource);
                        var response = base.Execute<T>(request);
                        TimeoutCheck(request, response);
                        WriteLog.logs(response.ResponseUri.ToString());
                        WriteLog.logs(response.Content);
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            LogError(BaseUrl, request, response);
                        }
                        return (T)Convert.ChangeType(response.Data, typeof(T));
                    }
                   
                }
                return default;
            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.ToString());
                //baseController.CreateErrorLog(ex.Message, ex.ToString());
                return default;
            }
        }
     

        public T GetFromCache<T>(string cacheKey) where T : class, new()
        {
            var item = _cache.Get<T>(cacheKey);
            return item;
        }

        public T GetFromCache<T>(IRestRequest request, string cacheKey) where T : class, new()
        {
            var item = _cache.Get<T>(cacheKey);
            if (item == null)
            {
                var response = Execute<T>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _cache.Set(cacheKey, response.Data, 3600);
                    item = response.Data;
                }
                else
                {
                    LogError(BaseUrl, request, response);
                    return default;
                }
            }
            return item;
        }

        private readonly int _minuteDefault = 3600;

        public void SaveToCache(string cacheKey, object item)
        {
            if (!string.IsNullOrEmpty(cacheKey))
            {
                _cache.Set(cacheKey, item, _minuteDefault);
            }
        }

        public void SaveToCache(string cacheKey, object item, int minute)
        {
            if (!string.IsNullOrEmpty(cacheKey) && minute >= 0)
            {
                _cache.Set(cacheKey, item, minute);
            }
        }

        private void LogError(Uri BaseUrl, IRestRequest request, IRestResponse response)
        {
            //Get the values of the parameters passed to the API
            string parameters = string.Join(", ", request.Parameters.Select(x => x.Name.ToString() + "=" + (x?.Value == null ? "NULL" : x.Value)).ToArray());

            //Set up the information message with the URL, the status code, and the parameters.
            string info = "Request to " + request.Resource + " failed with status code " + response.StatusCode + ", parameters: "
            + parameters + ", and content: " + response.Content;

            //Acquire the actual exception
            Exception ex;
            if (response != null && response.ErrorException != null)
            {
                ex = response.ErrorException;
                info = response.Content;
            }
            else
            {
                ex = new Exception(info);
                info = response.Content;
            }

            //Log the exception and info message
            _errorLogger.LogError(ex, info);
        }
    }
}

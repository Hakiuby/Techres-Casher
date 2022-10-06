using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    class PasswordClient : BaseClient
    {
        public PasswordClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
       : base(cache, serializer, errorLogger) { }


        public BaseResponse ChangePassword(string old_password, string new_password, int userId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CHANGE_PASSWORD, userId), Method.POST);
            //ChangePasswordWrapper changePasswordWrapper = new ChangePasswordWrapper(old_password, new_password);
            var js = JsonConvert.SerializeObject(new ChangePasswordWrapper(old_password, new_password)); 
            User user = (User)Utils.Utils.GetCacheValue("CURRENT_USER");
            request.AddHeader("Authorization", "Bearer " + user.AccessToken);
            request.AddHeader("Content-type", "application/json");
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);
        }

        public BaseResponse ForgetPassword (string UserName, string Authorization)
        {

            RestRequest request = new RestRequest(LinkCallApi.API_FORGOT_PASSWORD, Method.POST);
            ForgetPasswordWrapper changePasswordWrapper = new ForgetPasswordWrapper(UserName);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Authorization);
            var js = JsonConvert.SerializeObject(changePasswordWrapper);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.OAUTH, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse VerfirmForgetPassword(VerifyPasswordWrapper verify, string Authorization)
        {

            RestRequest request = new RestRequest(LinkCallApi.API_VERIFY_CHANGE_PASSWORD, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", Authorization);
            var js = JsonConvert.SerializeObject(verify);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
    }
}

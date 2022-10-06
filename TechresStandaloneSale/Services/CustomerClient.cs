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
    class CustomerClient : BaseClient
    {

        public CustomerClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
       : base(cache, serializer, errorLogger) { }

        public CustomerRegisterResponse Register(string frist_name, string last_name, string address, string phone, string birthday)
        {
            CustomerRegisterWrapper registerWrapper = new CustomerRegisterWrapper(frist_name, last_name, address, phone, birthday);
            RestRequest request = new RestRequest(LinkCallApi.API_CUSTOMER_REGISTER, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(registerWrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerRegisterResponse>(request,callApiWrapper);
        }
        public CustomerRegisterResponse FindCustomerByPhone(FindCustomerWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CUSTOMER_SEARCH_BY_PHONE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerRegisterResponse>(request, callApiWrapper);
        }
        #region toan
        public CustomerRegisterResponse FindCustomer(string phone, long branchid)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CUSTOMER_SEARCH_BY_PHONE), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("phone", phone);
            request.AddParameter("branch_id", branchid.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerRegisterResponse>(request, callApiWrapper);
        }
        #endregion
        public CustomerRegisterResponse GetCustomerDetail(long id)
        {
            RestRequest request = new RestRequest( string.Format(LinkCallApi.API_CUSTOMER_PROFILE, id), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerRegisterResponse>(request, callApiWrapper);
        }
        public CustomerDebtResponse GetAllCustomerDebt(int BrandId,long BranchId, decimal OrderId, decimal CustomerId,int IsPaid)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CUSTOMER_DEBT_ALL, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("restaurant_brand_id", BrandId.ToString());
            request.AddParameter("branch_id", BranchId.ToString());
            request.AddParameter("order_id", OrderId.ToString());
            request.AddParameter("customer_id", CustomerId.ToString());
            request.AddParameter("is_paid", IsPaid.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerDebtResponse>(request, callApiWrapper);
        }
        public CustomerDebtResponse CustomerDebtData(decimal CustomerId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CUSTOMER_DEBT_DETAIL, CustomerId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerDebtResponse>(request, callApiWrapper);
        }
        public BaseResponse CustomerConfirmDebt(DebtCustomerConfirm wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CUSTOMER_DEBT_CONFIRM, Method.POST);
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
    }
}

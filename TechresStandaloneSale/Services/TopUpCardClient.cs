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
    public class TopUpCardClient : BaseClient
    {
        public TopUpCardClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }

        public TopUpCardResponse GetListTopUpCardResponse(int Status)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_TOP_UP_CARD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("status", Status.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<TopUpCardResponse>(request,callApiWrapper);
        }
        public RestaurantTopUpCardResponse InputMoneyToCustomer(long customerId, long topUpId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TOP_UP_CARD_CUSTOMER, customerId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new RestaurantTopUpCardWrapper(topUpId));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RestaurantTopUpCardResponse>(request,callApiWrapper);
        }
        public CustomerTopUpHistoryResponse GetListTopUpHistory(int all, long branchId,long employeeId,string fromDate, string toDate,int page,int limit)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TOP_UP_CARD_CUSTOMER, all), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("employee_id", employeeId.ToString());
            request.AddQueryParameter("from", fromDate.ToString());
            request.AddQueryParameter("to", toDate.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CustomerTopUpHistoryResponse>(request,callApiWrapper);
        }
    }
}

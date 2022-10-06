using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Diagnostics;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class WorkingSessionClient : BaseClient
    {
        public WorkingSessionClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }


        public RevenueFinishWorkingSessionResponse GetRevenueFinishWorkingSessionToDay()
        {
            RestRequest request = new RestRequest(LinkCallApi.END_WORKING_SESSION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<RevenueFinishWorkingSessionResponse>(request, callApiWrapper);
        }

        public BaseResponse EndWorkingSession(RevenueFinishWorkingSessionWrapper revenueFinishWorkingSession)
        {
            RestRequest request = new RestRequest(LinkCallApi.CLOSE_SESSION, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(revenueFinishWorkingSession);
            Console.Write(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER,request);

            return Get<BaseResponse>(request, callApiWrapper);
        }
        public HistoryEndWorkingSessionResponse HistoryEndWorkingSession(int brandId,long branchid, string from,string to, int page)
        {
            RestRequest request = new RestRequest(LinkCallApi.HISTORY_END_WORKING_SESSION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchid.ToString());
            request.AddQueryParameter("from", from);
            request.AddQueryParameter("to", to);
            request.AddQueryParameter("page", page.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<HistoryEndWorkingSessionResponse>(request, callApiWrapper);
        }
        public HistoryEndWorkingSessionItemResponse HistoryEndWorkingSessionDetail(long Id,long branchid)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.HISTORY_END_WORKING_SESSION_DETAIL,Id), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchid.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<HistoryEndWorkingSessionItemResponse>(request, callApiWrapper);
        }
        public OrderSession GetCheckSessions()
        {
            RestRequest request = new RestRequest(LinkCallApi.CHECK_SESSION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<OrderSession>(request, callApiWrapper);
        }
        public BaseResponse CreateeteOrderSession(long orderSessionId, long branchId)
        {
            RestRequest request = new RestRequest(LinkCallApi.CREATE_ORDER_SESSION_EMPLOYEE, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new CreateOrderSessionWrapper(branchId, orderSessionId));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);  
        }
        public BaseResponse OpenSession(decimal beforeCash, long workingSessionId)
        {
            RestRequest request = new RestRequest(LinkCallApi.OPEN_SESSION, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new OpenSessionWrapper(beforeCash, workingSessionId));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER,request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
    }
}

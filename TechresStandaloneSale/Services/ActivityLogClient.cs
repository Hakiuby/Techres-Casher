using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    class ActivityLogClient : BaseClient
    {

        public ActivityLogClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger) : base(cache, serializer, errorLogger) { }
        public ActivityLogResponse GetListLog(int page, long employeeId, string fromDate, string toDate, long limt)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ACTIVITY_LOG, Method.GET);
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("employee_id", employeeId.ToString());
            //request.AddQueryParameter("log_type", logType);
            request.AddQueryParameter("from_date", fromDate.ToString());
            request.AddQueryParameter("to_date", toDate.ToString());
            request.AddQueryParameter("limit", limt.ToString());
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.LOGS, request);
            return Get<ActivityLogResponse>(request,callApiWrapper);
        }
        public ActivityLogListResponse GetListActivityOrder(long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ACTIVITY_LOG_ORDER, orderId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("object_id", orderId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.LOGS, request);

            return Get<ActivityLogListResponse>(request, callApiWrapper);
        }

        public ActivityLogListResponse GetListActivity(long orderId,long limit,int page)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ACTIVITY_LOG_ORDER), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("object_id", orderId.ToString());
            request.AddParameter("limit", limit.ToString());
            request.AddParameter("page", page.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.LOGS, request);
            return Get<ActivityLogListResponse>(request, callApiWrapper);
        }

    }
}

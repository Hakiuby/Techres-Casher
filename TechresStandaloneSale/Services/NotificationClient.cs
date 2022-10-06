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
  public  class NotificationClient : BaseClient
    {
        public NotificationClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger) : base(cache, serializer, errorLogger) { }
        public LongResponse GetLongResponse(string TokenNodejs, int notificationType)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_IS_VIEWD_EMPLOYEE_NOTIFICATION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("notification_employee_type", notificationType.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.LOGS, request);
            return Get<LongResponse>(request, callApiWrapper);
        }
        public ActivityLogResponse GetNotificationEmployeeResponse(string TokenNodejs, int page, int notificationType)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_EMPLOYEE_NOTIFICATION, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("notification_employee_type", notificationType.ToString());
            //  request.AddQueryParameter("limit", limit.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.LOGS, request);
            return Get<ActivityLogResponse>(request, callApiWrapper);
        }
    }
}

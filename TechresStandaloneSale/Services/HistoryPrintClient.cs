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
    public class HistoryPrintClient: BaseClient
    {
        public HistoryPrintClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)

            : base(cache, serializer, errorLogger) { }
        //public HistoryPrintResponse GetListFoodSendCook(long orderId, long BranchId)
        //{
        //    RestRequest request = new RestRequest(LinkCallApi.API_ORDER_IS_PRINT, Method.GET);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddQueryParameter("order_id", orderId.ToString());
        //    request.AddQueryParameter("branch_id", BranchId.ToString());

        //    CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
        //    WriteLog.logs(request.ToString());
        //    return Get<HistoryPrintResponse>(request, callApiWrapper); 
        //}
        public HistoryPrintResponse GetListFoodSendCook(long orderId, long BranchId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_HISTORY_PRINT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("order_id", orderId.ToString());
            request.AddQueryParameter("branch_id", BranchId.ToString());

            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            WriteLog.logs(request.ToString());
            return Get<HistoryPrintResponse>(request, callApiWrapper);
        }
    }
}

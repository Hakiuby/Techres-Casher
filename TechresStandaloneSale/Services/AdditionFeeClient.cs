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
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
   public class AdditionFeeClient : BaseClient
    {
        public AdditionFeeClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }
         public BaseResponse PaidAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_PAID_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse ConfirmAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CONFIRM_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request,callApiWrapper);
        }
        public BaseResponse CancelAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CANCEL_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request,callApiWrapper);
        }
        public BaseResponse CancelPaymentAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CANCEL_PAYMENT_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request,callApiWrapper);
        }
        public BaseResponse RefundAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_REFUND_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request,callApiWrapper);
        }
        public BaseResponse ApproveAdditionFee(long Id, long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_APPROVE_PAYMENT_ADDITION_FEE, Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new ConfirmAdditionFeeWrapper(branchId, Id));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request, callApiWrapper);
        }
      
        public ManageAdditionFeeResponse AddAdditionFee(AdditionFeeWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ADD_ADDITION_FEE, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ManageAdditionFeeResponse>(request, callApiWrapper);
        }
        public ManageAdditionFeeResponse UpdateAdditionFee(UpdatePaymentSlipWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UPDATE_ADDITION_FEE,wrapper.Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<ManageAdditionFeeResponse>(request, callApiWrapper);
        }
       
        public AdditionFeeOneResponse GetAdditionFeeDetail(long AdditionFeeId , long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ADDITION_FEES_DETAIL, AdditionFeeId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<AdditionFeeOneResponse>(request, callApiWrapper);
        }
        public AdditionFeeResponse GetListAdditionFee(int page = 1,int brandId =-1, long branchId = -1, long orderSessionId = -1, long isTakeAuto = -1, string FromDate="",string ToDate="", int type = -1,int isCountToRevenue = 1,long restaurantBudgetId =-1, long employeeId=-1, string ids = "")
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ADDITION_FEES, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("restaurant_budget_id", restaurantBudgetId.ToString());
            request.AddQueryParameter("from", FromDate);
            request.AddQueryParameter("to", ToDate);
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("is_count_to_revenue", isCountToRevenue.ToString());
            request.AddQueryParameter("is_take_auto_generated", isTakeAuto.ToString());
            request.AddQueryParameter("order_session_id", orderSessionId.ToString());
            request.AddQueryParameter("employee_id", employeeId.ToString());
            request.AddQueryParameter("addition_fee_ids", ids);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<AdditionFeeResponse>(request, callApiWrapper);
        }
     
    }
}

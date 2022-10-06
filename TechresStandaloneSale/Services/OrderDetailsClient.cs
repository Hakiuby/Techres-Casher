using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{

   public class OrderDetailsClient : BaseClient
    {

        private readonly User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public OrderDetailsClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
: base(cache, serializer, errorLogger) { }

        public SingleOrderDetailResponse GetOrderDetail(long id)    
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_ORDER + id.ToString(), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<SingleOrderDetailResponse>(request, callApiWrapper);
        }
        public SingleOrderDetailResponse GetOrderDetailExtraCharge(long id)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_ORDER_EXTRA_CHARGE + id.ToString(), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<SingleOrderDetailResponse>(request, callApiWrapper);
        }
        public OrderDetailResponse GetOrderDetails(long branchId,string OrderDetailStatus, string is_approved, string FromDate, string ToDate, string orderMethod, int restaurantKitchenPlaceId, string categoryTypes)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_ORDERS_DETAIL, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("order_detail_status", OrderDetailStatus);
            request.AddQueryParameter("category_types", categoryTypes);
            request.AddQueryParameter("is_approved", is_approved);
            request.AddQueryParameter("from_date", FromDate);
            request.AddQueryParameter("to_date", ToDate);
            request.AddQueryParameter("order_methods", orderMethod);
            request.AddQueryParameter("restaurant_kitchen_place_id", restaurantKitchenPlaceId.ToString());
            request.AddQueryParameter("limit", "400"); 
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<OrderDetailResponse>(request, callApiWrapper);
        }
        public BaseResponse ChangeStatus(string orderId, ChangeSatusOrderDetailWrapper wrapper)
        {

            string query = string.Format(LinkCallApi.API_CHANGE_STATUS, orderId);
            RestRequest request = new RestRequest(query, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse ApprovedDrinkOther(string orderId, ChangeApprovedDrinkWrapper wrapper)
        {
            string query = string.Format(LinkCallApi.API_APPROVED_DRINK_OTHER, orderId);
            RestRequest request = new RestRequest(query, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public OrderDetailListResponse GetOrderDetailsPrint(long orderId)
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_ORDER_DETAIL_IS_PRINT), orderId), Method.GET); 
            //RestRequest request = new RestRequest(LinkCallApi.API_ORDER_DETAIL_IS_PRINT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddQueryParameter("order_id", orderId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<OrderDetailListResponse>(request, callApiWrapper);
        }
        public OrderNoteDetailResponse GetOrderDetailNote(int brandid,long branchId )
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_ORDER_DETAIL_NOTE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandid.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderNoteDetailResponse>(request, callApiWrapper);
        }
        public BaseResponse CreateNoteOrderDetail(CreateNoteOrderDetailWrapper wrapper, long branchid, int brandId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_MANAGE_ORDER_DETAIL_NOTE, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchid.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER,request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
    }
}

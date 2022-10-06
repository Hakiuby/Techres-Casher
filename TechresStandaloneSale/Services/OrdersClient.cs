using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class OrdersClient : BaseClient
    {
        public OrdersClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }

        public OrderResponse GetListOrder(string orderStatus, int page, int pageNumber, long branchId)
        {
            RestRequest request = new RestRequest(LinkCallApi.GET_ALL_ORDERS, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("page_number", pageNumber.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("order_status", orderStatus);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderResponse>(request,callApiWrapper);
        }
        public OrderResponse GetListOrderHistory(int ReportType, string FromDate, int FromHour, string ToDate, int ToHour, string OrderStatus, int Page, long Limit, long branchId,int brandId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ORDER_HISTORY, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("report_type", ReportType.ToString());
            request.AddQueryParameter("from_date", FromDate);
            request.AddQueryParameter("from_hour", FromHour.ToString());
            request.AddQueryParameter("to_date", ToDate);
            request.AddQueryParameter("to_hour", ToHour.ToString());
            request.AddQueryParameter("order_status", OrderStatus);
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("page", Page.ToString());
            request.AddQueryParameter("limit", Limit.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderResponse>(request,callApiWrapper);
        }
        public OrderDetailListResponse GetOrderDetailCustomerById(long OrderId, long branchId)
        {
            string query = string.Format(LinkCallApi.GET_ORDER_DETAIL_CUSTOMER, OrderId);
            RestRequest request = new RestRequest(query, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderDetailListResponse>(request, callApiWrapper);
        }
        public OrderItemResponse GetOrderById(long OrderId, long branchId, int isTakeCancelledFood, long is_print_bill)
        {
            string query = string.Format(LinkCallApi.GET_ORDER_BY_ID, OrderId);
            RestRequest request = new RestRequest(query, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("is_take_cancelled_food", isTakeCancelledFood.ToString());
            request.AddQueryParameter("is_print_bill", is_print_bill.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse GetOrderAddionalVAT(long OrderId, int paymentMethod)
        {
            string query = string.Format(LinkCallApi.GET_ORDER_ADDTIONAL_VAT, OrderId);
            RestRequest request = new RestRequest(query, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(new PaymentMethodWrapper(paymentMethod));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse GetOrderByTableId(long tableId, long branchId, int isTakeCancelledFood)
        {
            string query = string.Format(LinkCallApi.GET_ORDER_BY_TABLE_ID, tableId);
            RestRequest request = new RestRequest(query, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("is_take_cancelled_food", isTakeCancelledFood.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse AssignOrderToCustomer(AssignOrderToCustomerWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ASSIGN_ORDER_TO_CUSTOMER, wrapper.OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }

        public OrderItemResponse UnAssignOrderToCustomer(AssignOrderToCustomerWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UN_ASSIGN_ORDER_TO_CUSTOMER, wrapper.OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse AssignOrderToPromotion(AssignOrderToPromotionWrapper wrapper, long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ASSIGN_ORDER_TO_PROMOTION, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse UnAssignOrderToPromotion(long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UN_ASSIGN_ORDER_TO_PROMOTION, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }

        public BaseResponse MakePayment(long orderId, CompleteOrderWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_MAKE_PAYMENNT, orderId.ToString()), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse MakePaymentWaiting(long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_MAKE_PAYMENT_WAITING, orderId.ToString()), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse ConfirmOrders(long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ORDER_CONFIRM, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse CancelOrders(long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ORDER_CANCEL, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public OpenTableByIdResponse OpenTableById(long tableId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TABLE_OPEN_BY_ID, tableId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OpenTableByIdResponse>(request, callApiWrapper);
        }
        public CreateOrderResponse CreateOrder(CreateOrderWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CREATE_ORDER, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CreateOrderResponse>(request, callApiWrapper);
        }
        public OrderItemResponse UpdateShipping(UpdateShippingWrapper wrapper, long orderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UPDATE_SHIPPING_ORDER, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }
        public OrderItemResponse AddFoodOrder(long OrderId, AddFoodOrderWrapper wrapper) // add  món trong menu
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ADD_FOOD_ORDER, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request,callApiWrapper);
        }

        public BaseResponse ReturnBeerById(ReturnBeerWrapper wrapper,string prefix,long orderId, long id , long branchid)
        {
            string url = string.Format((LinkCallApi.API_RETURN_BEER_MULTI), orderId);
            RestRequest request = new RestRequest(url, Method.POST);
            request.Resource = string.Format(request.Resource + "?branch_id=" + branchid);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js); 
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper); 
        }
        public OrderItemResponse GiftFoodOrder(long OrderId, AddFoodOrderWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_GIFT_FOOD_ORDER, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderItemResponse>(request, callApiWrapper);
        }

        public CreateSpecialFoodResponse CreateSpecialFood(long OrderId,long BranchId, CreateSpecialFoodWrapper wrapper) // tạo món ăn khác và order
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_CREATE_SPECIAL_FOOD),OrderId,BranchId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CreateSpecialFoodResponse>(request, callApiWrapper);
        }

        public BaseResponse CompleteOrder(long OrderId, CompleteOrderWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_COMPLETE_WITHOUT_RP, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse ApplyDiscount(long OrderId, DiscountWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_APPLY_DISCOUNT, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,  callApiWrapper);
        }
        public BaseResponse ApplyVAT(long OrderId, int isApplyVAT)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_APPLY_VAT, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            VATWrapper wrapper = new VATWrapper();
            wrapper.IsApplyVAT = isApplyVAT;
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse ApplyReturnDeposit(long OrderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_APPLY_RETURN_BOOKING, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse UpdateCustomerSlotNumber(long OrderId, int slot)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UPDATE_CUSTOMER_SLOT_NUMBER, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CustomerSlotNumberWrapper wrapper = new CustomerSlotNumberWrapper();
            wrapper.CustomerSlotNumber = slot;
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse MakePaymentWaitingCompleteNoRP(long orderId)
        {

            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_WAITING_COMPLETE_NO_RP, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);
        }
        public ManageFloatResponse TmpRevenue(long branchId)
        {
            RestRequest request = new RestRequest(LinkCallApi.TMP_REVENUE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ManageFloatResponse>(request, callApiWrapper);
        }
        public DebitResponse GetHistoryDebit(string from, string to,int brandId, long branchId, long employeeId, int page)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ORDER_HISOTRY_DEBIT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("employee_id", employeeId.ToString());
            request.AddQueryParameter("from", from);
            request.AddQueryParameter("to", to);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<DebitResponse>(request, callApiWrapper);
        }
        public CancelReasonResponse GetCancelReasons(long brandId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ORDER_CANCEL_REASON, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CancelReasonResponse>(request, callApiWrapper);
        }
        public BaseResponse UpdateDebitOrderEmployee(long OrderId, DebitEmployeeWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ORDER_DEBIT, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse UpdateDebitOrderCustomer(long OrderId, CustomerDebitWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ORDER_DEBIT_CUSTOMER, OrderId, wrapper.CustomerId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public BaseResponse UpdateOrderIsPrintSendCook(IsPrintSendFoodCookWrapper wrapper, long orderId)
        {
            string url = string.Format((LinkCallApi.API_ORDER_IS_PRINT),orderId);

            RestRequest request = new RestRequest(url,Method.POST);
            request.Resource = string.Format(request.Resource + "?order_id=" + orderId);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            WriteLog.logs(callApiWrapper.ToString());
            return Get<BaseResponse>(request, callApiWrapper);
        }
        //public BaseResponse PostOrderDetailPrint(long orderid)
        //{
        //    RestRequest request = new RestRequest(LinkCallApi.API_ORDER_IS_PRINT, Method.POST);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddQueryParameter("order_id", orderid.ToString());

        //    CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
        //    return Get<OrderDetailPrintResponse>(request, callApiWrapper);
        //}
        public OrderDetailPrintResponse GetOrderDetailPrint(long orderid)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ORDER_IS_PRINT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("order_id", orderid.ToString());

            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderDetailPrintResponse>(request, callApiWrapper);
        }

        public BaseResponse CancelFood(CancelFoodWrapper wrapper, long OrderId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ORDER_CANCEL_FOOD, OrderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }

        public BaseResponse CancelFood_2(CancelFoodWrapper wrapper, string prefix, long orderId, long id, long branchid)
        {
            string url = string.Format((LinkCallApi.API_ORDER_CANCEL_FOOD_V2), orderId);
            RestRequest request = new RestRequest(url, Method.POST);
            request.Resource = string.Format(request.Resource + "?branch_id=" + branchid);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        public PaymentReasonResponse GetListPaymentReason(long Status, int isCost, int isSystemAutoGenerate)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ORDER_ALL_PAYMENT_REASON, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("status", Status.ToString());
            request.AddQueryParameter("is_cost", isCost.ToString());
            request.AddQueryParameter("is_system_auto_generate", isSystemAutoGenerate.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<PaymentReasonResponse>(request, callApiWrapper);
        }


    }
}

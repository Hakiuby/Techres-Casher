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
    public class ReportClient:BaseClient
    {
        public ReportClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }
       
        public RevenueAreaReportResponse GetRevenueArea(int brandId, string FromDate, long branchId,  int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_REVENUE_AREA, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RevenueAreaReportResponse>(request,callApiWrapper);
        }
        public RevenueSysthesisReportResponse GetRevenueSysthesis(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYSTHESIS_REPORT_REVENUE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("report_type", type.ToString())
                
                
                
                ;
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("time", FromDate.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RevenueSysthesisReportResponse>(request,callApiWrapper);
        }
      
        //ReportStatisticAge
        public ReportEmployeeResponse GetStatistic(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.STATISTIC_REPORT_AGE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate.ToString());
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ReportEmployeeResponse>(request,callApiWrapper);
        }
        public ReportFoodResponse GetReportFood(int brandId, long branchId, string FromDate, int type, int categoryId, string categoryTypeIds, int isGroupbyCategory, int orderMethod,int isCanceledOrderDetail,int justTakeSpecicalFoods, int isGift)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_FOOD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("category_id", categoryId.ToString());
            request.AddQueryParameter("category_type", categoryTypeIds.ToString());
            request.AddQueryParameter("is_groupby_category", isGroupbyCategory.ToString());
            request.AddQueryParameter("order_method", orderMethod.ToString());
            request.AddQueryParameter("is_canceled_order_detail", isCanceledOrderDetail.ToString());
            request.AddQueryParameter("food_material_type", "-1");
            request.AddQueryParameter("just_take_specical_foods", justTakeSpecicalFoods.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("is_gift", isGift.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ReportFoodResponse>(request,callApiWrapper);
        }
        public ReportFoodResponse GetReportCancelFoodV2(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_REPORT_ORDER_CANCEL_FOOD), brandId, branchId, type, FromDate), Method.GET); 
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.REPORT, request);
            return Get<ReportFoodResponse>(request, callApiWrapper); 
        }
        public ReportFoodResponse GetReportFoodV2(int brandId, long branchId, string FromDate, int type)
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_REPORT_ORDER_FOOD), branchId, brandId, type, FromDate,-1),Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.REPORT, request);
            return Get<ReportFoodResponse>(request, callApiWrapper);
        }

        public ReportFoodResponse GetFoodV2(long branchId,int brandId,int type,string FromDate,string category,int categoryId,int isGift, int isCombo, int isCancel,int isTakeaway,int isGoods)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_FOOD_V2, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("brand_id", brandId.ToString());
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("category", category.ToString());
            request.AddQueryParameter("category_id", categoryId.ToString());
            request.AddQueryParameter("is_gift", isGift.ToString());
            request.AddQueryParameter("is_combo", isCombo.ToString());
            request.AddQueryParameter("is_cancel", isCancel.ToString());
            request.AddQueryParameter("is_take_away", isTakeaway.ToString());
            request.AddQueryParameter("is_goods", isGoods.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.REPORT, request);
            return Get<ReportFoodResponse>(request, callApiWrapper);
        }

        public ReportFoodResponse GetExtraFoodV2(int brandId, long branchId, string FromDate, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_EXTRA_FOOD_V2, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("brand_id", brandId.ToString());
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("time", FromDate);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.REPORT, request);
            return Get<ReportFoodResponse>(request, callApiWrapper);
        }
        public ReportFoodResponse GetGiftfFoodV2(int brandId, long branchId, string FromDate, int type)
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_REPORT_ORDER_GIFT_FOOD), branchId, brandId, type, FromDate),Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.REPORT, request);
            return Get<ReportFoodResponse>(request, callApiWrapper); 
        }
        public ExtraChargeResponse GetExtraChargeReport(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_EXTRA_CHARGE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ExtraChargeResponse>(request,callApiWrapper);
        }

       
        public ChartResponse GetSynthesisReportVAT(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_VAT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ChartResponse>(request,callApiWrapper);
        }
        public GiftFoodResponse GetSynthesisReportGiftFood(int brandId, string FromDate, long branchId, int type, int page, int limit)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_GIFT_FOOD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate.ToString());
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<GiftFoodResponse>(request,callApiWrapper);
        }
        public FoodComboResponse GetSynthesisReportCombo(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_FOOD_COMBO, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate.ToString());
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<FoodComboResponse>(request,callApiWrapper);
        }


        public ChartResponse GetSynthesisReportDiscount(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_DISCOUNT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ChartResponse>(request,callApiWrapper);
        }

        public SysthesisReportOrdersResponse GetSynthesisReportOrder(int brandId, string FromDate, long branchId, int type,int page)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_ORDER, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<SysthesisReportOrdersResponse>(request,callApiWrapper);
        }
        public ChartResponse GetSynthesisReportCustomer(int brandId, string FromDate, long branchId, int type)
        {
            RestRequest request = new RestRequest(LinkCallApi.SYNTHESIS_REPORT_CUSTOMER, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("time", FromDate);
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<ChartResponse>(request,callApiWrapper);
        }

        public OrderResponses OrderHistory(long OrderSessionId)
        {
            RestRequest request = new RestRequest(LinkCallApi.ORDER_SESSION_ORDER_HISTORY, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("order_session_id", OrderSessionId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<OrderResponses>(request,callApiWrapper);
        }

        public BookingSessionResponse BookingHistory(int action, long OrderSessionId)
        {
            RestRequest request = new RestRequest(LinkCallApi.ORDER_SESSION_BOOKING_HISTORY, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("order_session_id", OrderSessionId.ToString());
            request.AddQueryParameter("type", action.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingSessionResponse>(request,callApiWrapper);
        }
    
        public EmployeeMainAdminResponse GetEmployeeMainAdminResponse(int brandId, string time, long branchId, long type, long limit)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_REPORT_REVENUE_RANK_BY_EMPLOYEE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            request.AddQueryParameter("time", time.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeMainAdminResponse>(request,callApiWrapper);
        }
        public RevenuePaymentMethodResponse GetRevenuePaymentMethodResponse(int brandId, string time, long branchId, long type)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_REVENUE_BY_PAYMENT_METHOD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("report_type", type.ToString());
            request.AddQueryParameter("time", time.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<RevenuePaymentMethodResponse>(request,callApiWrapper);
        }
        public FormServiceResponse GetFormServiceResponse(int brandId, string time, long branchId, long type)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_REPORT_FORM_SERVICE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("type", type.ToString());
            request.AddQueryParameter("time", time.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<FormServiceResponse>(request,callApiWrapper);
        }
    }
}

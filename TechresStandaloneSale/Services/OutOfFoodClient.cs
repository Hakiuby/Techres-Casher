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
    public class OutOfFoodClient : BaseClient
    {
        public OutOfFoodClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)

            : base(cache, serializer, errorLogger) { }

        public OutOfFoodResponse GetOutOfFoodWorking(long branchid, long isoutstock, string keysearch, int kitchenid)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_FOOD_FOR_BRANCH_KITCHEN, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchid.ToString());
            request.AddQueryParameter("is_out_stock", isoutstock.ToString());
            request.AddQueryParameter("key_search", keysearch.ToString());
            request.AddQueryParameter("restaurant_kitchen_place_id", kitchenid.ToString());

            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            WriteLog.logs(request.ToString());
            return Get<OutOfFoodResponse>(request, callApiWrapper);


        }
        public OutOfFoodResponse GetOutOfFoodNotWorking(long branchid, long isoutstock, string keysearch, int kitchenid)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_GET_FOOD_FOR_BRANCH_KITCHEN, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchid.ToString());
            request.AddQueryParameter("is_out_stock", isoutstock.ToString());
            request.AddQueryParameter("key_search", keysearch.ToString());
            request.AddQueryParameter("restaurant_kitchen_place_id", kitchenid.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            WriteLog.logs(request.ToString());
            return Get<OutOfFoodResponse>(request, callApiWrapper);
        }
        public BaseResponse UpdateFoodOutOfStock(int brandid, List<int> foodids)
        {
            //string query = string.Format(LinkCallApi.API_UPDATE_FOOD_IS_OUT_STOCK_BRACH_KITCHEN, Method.POST);
            RestRequest request = new RestRequest(LinkCallApi.API_UPDATE_FOOD_IS_OUT_STOCK_BRACH_KITCHEN, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            WriteLog.logs(request.ToString());
            var js = JsonConvert.SerializeObject(new UpdateFoodIsOutStockWrapper(brandid, foodids));
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
        

    }
}

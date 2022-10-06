using Dragablz.Dockablz;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class FoodsClient : BaseClient
    {
        public FoodsClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
                : base(cache, serializer, errorLogger) { }

        public FoodResponses GetAllFoods(int RestaurantBrandId,long branchId, int status, long categoryId, long categoryTypeId, long isTakeAWay, long isAddition, long isSpecialGift,long isBestseller,long isCombo, long kitchenId, long isCountMaterial, long isSellByWeight)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_FOOD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", RestaurantBrandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("status", status.ToString());
            request.AddQueryParameter("category_type", categoryTypeId.ToString());
            request.AddQueryParameter("category_id", categoryId.ToString());
            request.AddQueryParameter("is_take_away", isTakeAWay.ToString());
            request.AddQueryParameter("is_addition", isAddition.ToString());
            request.AddQueryParameter("is_special_gift", isSpecialGift.ToString());
            request.AddQueryParameter("is_bestseller", isBestseller.ToString());
            request.AddQueryParameter("is_combo", isCombo.ToString());
            request.AddQueryParameter("kitchen_id", kitchenId.ToString());
            request.AddQueryParameter("is_count_material", isCountMaterial.ToString());
            request.AddQueryParameter("is_sell_by_weight", isSellByWeight.ToString());
            request.AddQueryParameter("limit", int.MaxValue.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<FoodResponses>(request,callApiWrapper);
        }


        public FoodResponses GetFoodsUSing(int categoryType, long branchId, long categoryId, long isTakeAway,int isCombo, long isSpecialGift, long isSellByWeight, int status)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_FOOD, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("category_type", categoryType.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("category_id", categoryId.ToString());
            request.AddQueryParameter("is_take_away", isTakeAway.ToString());
            request.AddQueryParameter("is_combo", isCombo.ToString());
            request.AddQueryParameter("is_sell_by_weight", isSellByWeight.ToString());
            request.AddQueryParameter("is_special_gift", isSpecialGift.ToString());
            request.AddQueryParameter("status", status.ToString()); 
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<FoodResponses>(request, callApiWrapper);
        }
        //public ResponseFood GetFoodsUSing1(int categoryType, long branchId, long categoryId, long isTakeAway, int isCombo, long isSpecialGift, long isSellByWeight, int status)
        //{
        //    RestRequest request = new RestRequest(LinkCallApi.API_FOOD, Method.GET);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddQueryParameter("category_type", categoryType.ToString());
        //    request.AddQueryParameter("branch_id", branchId.ToString());
        //    request.AddQueryParameter("category_id", categoryId.ToString());
        //    request.AddQueryParameter("is_take_away", isTakeAway.ToString());
        //    request.AddQueryParameter("is_combo", isCombo.ToString());
        //    request.AddQueryParameter("is_sell_by_weight", isSellByWeight.ToString());
        //    request.AddQueryParameter("is_special_gift", isSpecialGift.ToString());
        //    request.AddQueryParameter("status", status.ToString());
        //    CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
        //    return Get<ResponseFood>(request, callApiWrapper);
        //}
        public BaseResponse UpdateKitchen(UpdateFoodKitchenWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_FOOD_UPDATE_KITCHEN, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            Console.Write(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request, callApiWrapper);
        }
       
       
    }
}

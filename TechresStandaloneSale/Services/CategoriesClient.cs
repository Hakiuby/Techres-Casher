using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    class CategoriesClient : BaseClient
    {


        public CategoriesClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
         : base(cache, serializer, errorLogger) { }

        public CategoryResponse GetCategoryByBranch(long branchId, string categoryTypes)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CATEGORY_BY_BRANCH_KITCHEN, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("category_types", categoryTypes);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CategoryResponse>(request,callApiWrapper);
        }
        public CategoryResponse GetAllCategory( int status, string categoryType, int restaurantBrandId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CATEGORY_ALL, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("status", status.ToString());
            request.AddQueryParameter("category_types", categoryType);
            request.AddQueryParameter("restaurant_brand_id", restaurantBrandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<CategoryResponse>(request,callApiWrapper);
        }

    }
}

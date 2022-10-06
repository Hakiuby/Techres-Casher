using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class BranchClient : BaseClient
    {
        public BranchClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }

        public BranchesResponse GetAllBranchesResponse(int isEnableMemberShipCard =-1,int restaurantBrandId =-1 )
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ALL_BRANCHES, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("is_enable_membership_card", isEnableMemberShipCard.ToString());
            request.AddQueryParameter("restaurant_brand_id", restaurantBrandId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BranchesResponse>(request,callApiWrapper);
        }
        public BranchResponse getBranchInfo(long branchId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_BRANCH_INFO,branchId), Method.GET);
            request.AddHeader("Content-type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BranchResponse>(request,callApiWrapper);
        }
    }
}

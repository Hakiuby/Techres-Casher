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
   public class PromotionClient : BaseClient
    {
        public PromotionClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }



        //public PromotionResponse GetPromotionApplying (long branchId, long customerId)
        //{
        //    RestRequest request = new RestRequest(LinkCallApi.API_PROMOTION_APPLY, Method.GET);
        //    request.AddQueryParameter("branch_id", branchId.ToString());
        //    request.AddQueryParameter("customer_id", customerId.ToString());
        //    request.AddHeader("Content-Type", "application/json");
        //    CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
        //    return Get<PromotionResponse>(request,callApiWrapper);
        //}

        public PromotionItemResponse GetPromotionDetail(long promotionId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_PROMOTION_DETAIL, promotionId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<PromotionItemResponse>(request,callApiWrapper);
        }
    }
}

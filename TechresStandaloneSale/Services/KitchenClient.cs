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
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
   public class KitchenClient : BaseClient
    {
        public KitchenClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger) : base(cache, serializer, errorLogger) { }

        public KitchenResponse GetKitchenResponses(int brandId = -1,long branchId =-1, int status=-1, int isBarKitChen =-1)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_KITCHEN_LIST, Method.GET);
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("status", status.ToString());
            request.AddQueryParameter("type", isBarKitChen.ToString());
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<KitchenResponse>(request, callApiWrapper);
        }
        public KitchenResponse UpdateRestaurantKitchenPlate(UpdatePrinterKitchenWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_RESTAURANT_KITCHEN_PLACE_PRINTER, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            Console.Write(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<KitchenResponse>(request,callApiWrapper  );
        }
    }
}

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
    public class BrandClient : BaseClient
    {
        public BrandClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
     : base(cache, serializer, errorLogger) { }

        public BrandResponse GetAllBrand( int status = -1)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ALL_BRAND, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("status", status.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BrandResponse>(request,callApiWrapper);
        }
    
    }
}

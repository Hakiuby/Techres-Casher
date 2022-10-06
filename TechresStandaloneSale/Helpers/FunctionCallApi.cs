using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.Helpers
{
   public class FunctionCallApi : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public List<Branch> GetAllBranches(int restaurantBrandId, bool isRemoveAll)
        {
            List<Branch> branches = new List<Branch>();
            BranchClient client = new BranchClient(this, this, this);
            BranchesResponse response = client.GetAllBranchesResponse(Constants.ALL, restaurantBrandId);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.data != null)
            {
                branches = response.data;
                if (!isRemoveAll)
                {
                    Branch item = new Branch();
                    item.Name = MessageValue.MESSAGE_ALL_BRANCH;
                    item.Id = Constants.ALL;
                    branches.Insert(0, item);
                }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_BRANCH, branches, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
            }
            return branches;
        }
        public List<Brand> GetAllBrands(bool isRemoveAll)
        {
            List<Brand> brands = new List<Brand>();
            BrandClient client = new BrandClient(this, this, this);
            BrandResponse response = client.GetAllBrand();
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data.Count > 0)
            {
                brands = response.Data;
                if (!isRemoveAll)
                {
                    Brand item = new Brand();
                    item.Name = MessageValue.MESSAGE_ALL_BRAND;
                    item.Id = Constants.ALL;
                    brands.Insert(0, item);
                }
               
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_BRAND, brands, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
            }
            return brands;
        }
        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                if (jsonResponse.status == 200)
                {
                    T check = jsonResponse.ToObject<T>();
                    if (check != null)
                    {
                        return check;
                    }
                }
                else
                {
                    NotificationMessage.Error(jsonResponse.message);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                NotificationMessage.Warning(MessageValue.FORBIDDEN);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
            else
            {
                NotificationMessage.Error(response.ErrorMessage);
            }
            return default(T);
        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }


        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }


    }
}

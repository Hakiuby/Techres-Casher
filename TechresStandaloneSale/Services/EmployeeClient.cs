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
    public class EmployeeClient : BaseClient
    {
        public EmployeeClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
       : base(cache, serializer, errorLogger) { }

        public WorkingSessionResponses GetWorkingSessionByEmployee(long employeeId, long branchId)
        {
            RestRequest request = new RestRequest( string.Format(LinkCallApi.API_WORKING_SESSION_BY_EMPLOYEE, employeeId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<WorkingSessionResponses>(request, callApiWrapper);
        }
      

        public EmployeeAdvancedSalaryResponse GetEmployeeAdvancedSalaryResponses(int BrandId,long branchId, int status)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_EMPLOYEE_ADVANCED_SALARY_LIST, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", BrandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("status", status.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeAdvancedSalaryResponse>(request, callApiWrapper);
        }
        public EmployeeResponses GetAllEmployeesResponses(int brandId,long branchId, int page, int limmit, int status ,int is_only_take_owner, int is_take_myself)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_EMPLOYEE_LIST, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("status", status.ToString());
            request.AddQueryParameter("limit", limmit.ToString());
            //request.AddQueryParameter("is_include_restaurant_manager", isIncludeRestaurantManager.ToString());
            request.AddQueryParameter("is_only_take_owner ", is_only_take_owner.ToString());
            request.AddQueryParameter("is_take_myself", is_take_myself.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeResponses>(request, callApiWrapper);
        }

        public EmployeeResponses GetAllEmployeesDebitResponses()
        {
            RestRequest request = new RestRequest(LinkCallApi.API_EMPLOYEE_LIST_DEBT, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeResponses>(request, callApiWrapper);
        }

        public BaseResponse PayEmployeeAdvancedSalary(AddvanedSalaryWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_EMPLOYEE_ADVANCED_SALARY_PAY, wrapper.Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeAdvancedSalaryData>(request, callApiWrapper);
        }
        public EmployeeAdvancedSalaryData CancelEmployeeAdvancedSalary(AddvanedSalaryWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_EMPLOYEE_ADVANCED_SALARY_CANCEL, wrapper.Id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeAdvancedSalaryData>(request, callApiWrapper);
        }
     
        public EmployeeDetailResponse GetDetailEmployee(long employeeId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_EMPLOYEES_MANAGE, employeeId), Method.GET);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<EmployeeDetailResponse>(request, callApiWrapper);
        }
    }
}

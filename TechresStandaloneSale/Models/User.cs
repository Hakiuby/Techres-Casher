using Newtonsoft.Json;
using System.Collections.Generic;

namespace TechresStandaloneSale.Models
{
    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("employee_role_id")]
        public long EmployeeRoleId { get; set; }

        [JsonProperty("employee_role_name")]
        public string EmployeeRoleName { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("restaurant_id")]
        public long RestaurantId { get; set; }
        [JsonProperty("restaurant_brand_id")]
        public int RestaurantBrandId { get; set; }

        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty("gender")]
        public long Gender { get; set; }
        public string NodeAccessToken {get;set;}
        // nhận biết thuộc quyền quản lý của nhà hàng/ thương hiệu/ chi nhánh
        public int UserManagerId { get; set; }
    }
}

using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Models
{
    public class Employee
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("point")]
        public long Point { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("normalize_name")]
        public string NormalizeName { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("employee_role_id")]
        public long RoleId { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [JsonProperty("employee_rank_id")]
        public long EmployeeRankId { get; set; }

        [JsonProperty("employee_rank_name")]
        public string EmployeeRankName { get; set; }

        [JsonProperty("avatar")]
        public string AvatarImage { get; set; }

        [JsonProperty("point_used")]
        public long PointUsed { get; set; }

        [JsonProperty("total_point")]
        public decimal TotalPoint { get; set; }

        [JsonProperty("qr_code")]
        public string QrCode { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }
        [JsonProperty("passport")]
        public string Passport { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("birthplace")]
        public string Birthplace { get; set; }

        [JsonProperty("salary_level_id")]
        public long SalaryLevelId { get; set; }
        [JsonProperty("salary_level")]
        public string SalaryLevelName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("restaurant_name")]
        public string RestaurantName { get; set; }

        [JsonProperty("id_in_timekeeper")]
        public string IdInTimeKeeper { get; set; }

        [JsonProperty("area_id")]
        public int AreaId { get; set; }

        [JsonProperty("area_name")]
        public string AreaName { get; set; }

        [JsonProperty("working_session_id")]
        public int WorkingSessionId { get; set; }
        [JsonProperty("working_session_time")]
        public string WorkingSessionTime { get; set; }
        [JsonProperty("branch_id")]
        public long BranchId { get; set; }

        [JsonProperty("manage_areas")]
        public List<Area> ManageAreas { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }
        public bool IsSelectAll { get; set; }
    
        public Brush CheckAllColor
        {
            get
            {
                if (IsSelectAll)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GREEN_GG_COLOR));
                }
                else
                {
                    return new SolidColorBrush(Colors.White);

                }

            }
            set
            {
                CheckAllColor = value;
            }
        }
        public string NameRoleNameString
        {
            get
            {
                return string.Format("{0} - {1}", Name, RoleName);
            }
            set
            {
                NameRoleNameString = value;
            }
        }
      
        public string SalaryLevelNameString
        {
            get
            {
                return string.IsNullOrEmpty(SalaryLevelName) ? "" : SalaryLevelName;
            }
            set
            {
                SalaryLevelName = value;
            }
        }
        public string StatusString
        {
            get
            {
                return Status == Constants.STATUS ? "Đang hoạt động" : "Không hoạt động";
            }
            set
            {
                StatusString = value;
            }
        }
        public string EmployeeRankNameString
        {
            get
            {
                return string.IsNullOrEmpty(EmployeeRankName) ? "" : EmployeeRankName;
            }
            set
            {
                EmployeeRankName = value;
            }
        }



        public string WorkingSessionTimeString
        {
            get
            {
                return string.IsNullOrEmpty(WorkingSessionTime) ? "" : WorkingSessionTime;
            }
            set
            {
                WorkingSessionTime = value;
            }
        }
        public string AreaNameString
        {
            get
            {
                return string.IsNullOrEmpty(AreaName) ? "" : AreaName;
            }
            set
            {
                AreaName = value;
            }
        }
        public string Avatar
        {
            get
            {
                return Constants.ADS_DOMAIN + AvatarImage;

            }
            set
            {
                Avatar = value;
            }
        }
        public string RoleString
        {
            get
            {
                return Role(this.RoleId);
            }
            set
            {
                RoleString = value;
            }
        }
        public string GenderString
        {
            get
            {
                return Gender == 0 ? "Nữ" : "Nam";
            }
            set
            {
                GenderString = value;
            }
        }
        public string Role(long roleId)
        {
            switch (roleId)
            {
                case 1:
                    return ("Chủ nhà hàng");
                case 2:
                    return ("Quản lý");
                case 3:
                    return ("Thu ngân");
                case 4:
                    return ("Phục vụ");
                case 5:
                    return ("Quản lý kho");
                case 6:
                    return ("Đầu bếp");
                case 7:
                    return ("Quản lý kho bia");
                case 8:
                    return ("Nhân viên kinh doanh");
                default:
                    return ("Không biết");
            }
        }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, RoleName);
        }
    }


}

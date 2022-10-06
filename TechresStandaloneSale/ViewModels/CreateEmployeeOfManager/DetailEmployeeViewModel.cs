using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.CreateEmployeeOfManager
{
    public class DetailEmployeeViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; } }

        private string _Rank;
        public string Rank { get => _Rank; set { _Rank = value; } }

        private string _Level;
        public string Level { get => _Level; set { _Level = value; } }

        private string _Areaes;
        public string Areaes { get => _Areaes; set { _Areaes = value; } }


        private string _TimeAttendanceId;
        public string TimeAttendanceId { get => _TimeAttendanceId; set { _TimeAttendanceId = value; } }

        private string _Birthday;
        public string Birthday { get => _Birthday; set { _Birthday = value; } }

        private string _BirthdayPlace;
        public string BirthdayPlace { get => _BirthdayPlace; set { _BirthdayPlace = value; } }

        private string _Passport;
        public string Passport { get => _Passport; set { _Passport = value; } }

        private string _WorkingSession;
        public string WorkingSession { get => _WorkingSession; set { _WorkingSession = value; } }


        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; } }



        private string _Status;
        public string Status { get => _Status; set { _Status = value; } }

        private string _BranchName;
        public string BranchName { get => _BranchName; set { _BranchName = value; } }
        private string _ManageArea;
        public string ManageArea { get => _ManageArea; set { _ManageArea = value; } }

        private string _Avatar;
        public string Avatar { get => _Avatar; set { _Avatar = value; } }
        public DetailEmployeeViewModel(long employeeId = 0)
        {
            GetDetailEmployee(employeeId);
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public void GetDetailEmployee(long id)
        {
            EmployeeClient client = new EmployeeClient(this, this, this);
            EmployeeDetailResponse detail = client.GetDetailEmployee(id);
            if (detail != null && detail.Data != null)
            {
                Name = detail.Data.Name;
                Phone = detail.Data.Phone;
                Email = detail.Data.Email;
                Address = detail.Data.Address;
                TimeAttendanceId = detail.Data.IdInTimeKeeper;
                Role = detail.Data.RoleName;
                Rank = detail.Data.EmployeeRankName;
                WorkingSession = detail.Data.WorkingSessionTime;
                Level = detail.Data.SalaryLevelName;
                Areaes = detail.Data.AreaName;
                Avatar = string.IsNullOrEmpty(detail.Data.Avatar) ? "../Resources/Images/logo.png" : detail.Data.Avatar;
                if (detail.Data.Gender == 0)
                    Gender = "Nữ";
                else
                    Gender = "Nam";
                Birthday = detail.Data.Birthday;
                if (detail.Data.Status == 1) Status = "Hoạt động";
                else
                {
                    Status = "Không hoạt động";
                }
                BirthdayPlace = detail.Data.Birthplace;
                Passport = detail.Data.Passport;
                BranchName = detail.Data.BranchName;
                ManageArea = "";
                if (detail.Data.ManageAreas != null && detail.Data.ManageAreas.Count > 0)
                {
                    foreach (Area a in detail.Data.ManageAreas)
                    {
                        ManageArea = ManageArea + a.Name + ",";
                    }
                }
            }

        }
        public void LogError(Exception ex, string infoMessage)
        {
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

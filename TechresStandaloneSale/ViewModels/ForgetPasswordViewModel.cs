using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ForgetPasswordViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private string _EmployeeCode;
        public string EmployeeCode { get => _EmployeeCode; set { _EmployeeCode = value; } }
        private string _RestaurantName; 
        public string RestaurantName { get => _RestaurantName; set { _RestaurantName = value; } }
        public bool isForgetPassword;
        public ForgetPasswordViewModel()
        {
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                isForgetPassword = false;
                p.Close();
            });

            //AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    UserClient usersClient = new UserClient(this, this, this);
            //    //ConfigResponse config = usersClient.GetConfig(Properties.Settings.Default.RestaurantDomainAPI);
            //    ConfigResponse config = usersClient.GetConfig(RestaurantName);
            //    if (config!= null && config.Status == (int)ResponseEnum.OK)
            //    {
            //        string Authorization = string.Format("{0} {1}", config.Data.Type, config.Data.ApiKey);
            //        PasswordClient client = new PasswordClient(this, this, this);
            //        BaseResponse response = client.ForgetPassword(EmployeeCode, Authorization);
            //        if (response.Status == (int)ResponseEnum.OK)
            //        {
            //            p.Hide();
            //            VerifyForgotPasswordWindow verify = new VerifyForgotPasswordWindow();
            //            verify.DataContext = new VerifyForgotPasswordViewModel(EmployeeCode, Authorization);
            //            verify.ShowDialog();


            //        }
            //    }
                
            //});
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

        public void LogError(Exception ex, string infoMessage)
        {
        }
    }
}

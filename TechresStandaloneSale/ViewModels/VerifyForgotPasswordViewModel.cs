using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class VerifyForgotPasswordViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        public ICommand PasswordNewChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public string _EmployeeCode;
        public string EmployeeCode
        {
            get
            {
                return _EmployeeCode;
            }
            set
            {
                _EmployeeCode = value;
                OnPropertyChanged("EmployeeCode");
            }
        }
        public string _VerifyCode;
        public string VerifyCode
        {
            get
            {
                return _VerifyCode;
            }
            set
            {
                _VerifyCode = value;
                OnPropertyChanged("VerifyCode");
            }
        }
        public string PasswordNew;
        public string ConfirmPassword;

        public bool isVerify;
        public VerifyForgotPasswordViewModel(string employeeCode = "", string Authorization = "")
        {
            EmployeeCode = employeeCode;
            PasswordNewChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PasswordNew = p.Password;
            });
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ConfirmPassword = p.Password;
            });

            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (PasswordNew != ConfirmPassword)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CONFIRM_PASSWORD);
                    return;
                }
                if (string.IsNullOrEmpty(VerifyCode) || IsNumber(VerifyCode))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_VERIFY_CODE);
                    return;
                }
                PasswordClient client = new PasswordClient(this, this, this);
                VerifyPasswordWrapper verify = new VerifyPasswordWrapper(employeeCode, VerifyCode, PasswordNew);
                BaseResponse response = client.VerfirmForgetPassword(verify, Authorization);
                if (response.Status == (int)ResponseEnum.OK)
                {
                    p.Hide();
                    NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                }
                else
                {
                    NotificationMessage.Error(response.Message);
                }
            });

        }
        public bool IsNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^(\+[0-9])$");
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

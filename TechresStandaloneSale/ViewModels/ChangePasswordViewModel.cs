using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        PasswordClient passwordClient;
        
        public bool IsChange = false;

        private string _Password;
        public string Password { get => _Password; set { _Password = value; } }

        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; } }

        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; } }

        public ICommand PasswordCommand { get; set; }
        public ICommand NewPasswordCommand { get; set; }
        public ICommand ConfirmPasswordCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ChangePasswordViewModel()
        {
            User user = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

            passwordClient = new PasswordClient(this, this, this);

            PasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _Password = p.Password; });

            NewPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _NewPassword = p.Password; });

            ConfirmPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _ConfirmPassword = p.Password; });

            CloseCommand = new RelayCommand<ChangePasswordPopup>((t) => { return true; }, t =>
                {
                    IsChange = false;
                    t.Close();
                });

            AcceptCommand = new RelayCommand<ChangePasswordPopup>((t) => { return true; }, t => { Accept(t); });

            void Accept(ChangePasswordPopup t)
            {

                #region toàn

                if (t == null) return;
                
                if(String.IsNullOrEmpty(_Password))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_CHANGE_PASSWORD_OLD_PASSWORD);
                }
                else if(String.IsNullOrWhiteSpace(_NewPassword))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_CHANGE_PASSWORD_NEW_PASSWORD);    
                }
                if(_NewPassword.Length < 4)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_NEW_PASSWORD);
                }    
                else
                {
                    if (_NewPassword != _ConfirmPassword)
                    {
                        NotificationMessage.Error(MessageValue.MESSAGE_NOT_PASSWORD);
                    }
                    else
                    {
                        User userCurrent = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                        BaseResponse changePass = passwordClient.ChangePassword(_Password, _NewPassword, (int)userCurrent.Id);
                        if (changePass != null)
                        {
                            if (changePass.Status == 200)
                            {
                                NotificationMessage.Infomation(MessageValue.MESSAGE_CHANGE_PASSWORD_SUCCESS);
                                t.Close();
                                //Process.Start(Application.ResourceAssembly.Location);
                                //Application.Current.Shutdown();
                                IsChange = true;
                            }
                            else
                            {
                                NotificationMessage.Infomation(MessageValue.MESSAGE_NOT_OLD_PASSWORD);
                            }
                        }
                       
                    }
                }
                #endregion
            }
        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object item, int minutes)
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

        public void LogError(Exception ex, string infoMessage)
        {
            // throw new NotImplementedException();
        }
    }
}

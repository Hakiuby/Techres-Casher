using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.CreateEmployeeOfManager
{
    class UserInfomationViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        private BitmapImage _Avatar { get; set; }
        public BitmapImage Avatar { get => _Avatar; set { _Avatar = value; OnPropertyChanged("Avatar"); } }

        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged("Username"); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged("Password"); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged("Name"); } }
        public UserInfomationViewModel(string Name, string Username, string Password, BitmapImage Avatar)
        {
            this.Avatar = Avatar;
            this.Username = Username;
            this.Password = Password;
            this.Name = Name;
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
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


using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Dialogs
{
    public class ConfirmMoveTableViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }
        private string _TableContent1 { get; set; }
        public string TableContent1 { get => _TableContent1; set { _TableContent1 = value; OnPropertyChanged("TableContent1"); } }
        private string _TableContent2 { get; set; }
        public string TableContent2 { get => _TableContent2; set { _TableContent2 = value; OnPropertyChanged("TableContent2"); } }

        public bool isConfirm;
        public ConfirmMoveTableViewModel(string tableName1,string tableName2)
        {
            TableContent1 = tableName1;
            TableContent2 = tableName2;
             YesCommand = new RelayCommand<ConfirmMoveTableWindow>((t) => { return true; }, t =>
            {
                NotificationMessage.Infomation(string.Format(("Chuyển thành công bàn {0} qua bàn {1}"), TableContent1, TableContent2));
                isConfirm = true;
                t.Close();
            });
            NoCommand = new RelayCommand<ConfirmMoveTableWindow>((t) => { return true; }, t =>
            {
                isConfirm = false;
                t.Close();
            });
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

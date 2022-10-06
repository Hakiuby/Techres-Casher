using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;

namespace TechresStandaloneSale.ViewModels
{
    public class ResonCancelBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public bool isDone; 
        public string _ResonNote; 
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set;  }
        public string ResonNote
        {
            get
            {
                return _ResonNote; 
            }
            set
            {
                _ResonNote = value;
                OnPropertyChanged("ResonNote"); 
            }
        }

        public ResonCancelBookingViewModel()
        {  
            isDone = false;

            AddCommand = new RelayCommand<Window>((p) => { return true; },
                p =>
                {
                    if (string.IsNullOrEmpty(ResonNote))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_RESON_CANCEL_BOOKING);
                        return;
                    }
                    isDone = true;
                    p.Close();
                });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => 
            {
                isDone = false; 
                p.Close();  
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

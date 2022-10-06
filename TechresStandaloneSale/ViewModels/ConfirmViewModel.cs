using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ConfirmViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        public ICommand NoCommand { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public string _ContentConfirm;
        public string ContentConfirm
        {
            get
            {
                return _ContentConfirm;
            }
            set
            {
                _ContentConfirm = value;
                OnPropertyChanged("ContentConfirm");
            }
        }

        public string _TitleContent;
        public string TitleContent
        {
            get
            {
                return _TitleContent;
            }
            set
            {
                _TitleContent = value;
                OnPropertyChanged("TitleContent");
            }
        }

        public string _NoContent;
        public string NoContent
        {
            get
            {
                return _NoContent;
            }
            set
            {
                _NoContent = value;
                OnPropertyChanged("NoContent");
            }
        }

        public string _YesContent;
        public string YesContent
        {
            get
            {
                return _YesContent;
            }
            set
            {
                _YesContent = value;
                OnPropertyChanged("YesContent");
            }
        }
        public bool isConfirm;
        public bool isNoConfirm;
        public bool isStock; 
        public ConfirmViewModel(string contentConfirm = "", string title = "", string noContent = "", string yesContent = "")
        {
            ContentConfirm = contentConfirm;
            TitleContent = title;
            NoContent = noContent;
            YesContent = yesContent;
            YesCommand = new RelayCommand<ConfirmDeleteWindow>((t) => { return true; }, t =>
            {
                isConfirm = true;
                t.Close();
            });
            NoCommand = new RelayCommand<ConfirmDeleteWindow>((t) => { return true; }, t =>
            {
                isConfirm = false;
                isStock = true; 
                t.Close();
            });
            CloseCommand = new RelayCommand<ConfirmDeleteWindow>((t) => { return true; }, t =>
            {
                isConfirm = false;
                isNoConfirm = true;
                t.Close();
            });
            NewCommand = new RelayCommand<ConfirmDeleteWindow>((t) => { return true; }, t => {
                isConfirm = true; 
                
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

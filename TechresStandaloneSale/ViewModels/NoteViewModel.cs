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
    public class NoteViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public string _Note;
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
                OnPropertyChanged("Note");
            }
        }
        public ICommand AddCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public string note = "";
        public NoteViewModel()
        {
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                note = Note;
                p.Close();
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                note = "";
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

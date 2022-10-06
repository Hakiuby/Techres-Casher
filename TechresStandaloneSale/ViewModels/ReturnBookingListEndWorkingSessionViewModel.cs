using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ReturnBookingListEndWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<BookingSession> _BookingItems = new ObservableCollection<BookingSession>();
        public ObservableCollection<BookingSession> BookingItems
        {
            get
            {
                return _BookingItems;
            }
            set
            {
                _BookingItems = value;
                OnPropertyChanged("BookingItems");
            }
        }
        private bool _DialogHostOpen;
        public bool DialogHostOpen
        {
            get
            {
                return _DialogHostOpen;
            }
            set
            {
                _DialogHostOpen = value;

                OnPropertyChanged("DialogHostOpen");
            }
        }
        public ReturnBookingListEndWorkingSessionViewModel(long sessionId)
        {
            Application.Current.Dispatcher.Invoke(async delegate
            {
                DialogHostOpen = true;
                ReportClient reportClient = new ReportClient(this, this, this);
                BookingSessionResponse bookings = await System.Threading.Tasks.Task.Run(()=> reportClient.BookingHistory(Constants.ACTION_BOOKING_RETURN_DEPOSIT, sessionId));
                if (bookings != null && bookings.Status == (int)ResponseEnum.OK)
                {
                    bookings.Data.ForEach(BookingItems.Add);
                    DialogHostOpen = false;
                }
                else
                    DialogHostOpen = false;
            });
        }
        public void LogError(Exception ex, string infoMessage)
        {
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
    }
}


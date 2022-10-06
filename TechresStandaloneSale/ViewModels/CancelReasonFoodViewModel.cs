using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class CancelReasonFoodViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<CancelReason> _ListCancelReason;
        public ObservableCollection<CancelReason> ListCancelReason
        {
            get
            {
                return _ListCancelReason;
            }
            set
            {
                _ListCancelReason = value;
                OnPropertyChanged("ListCancelReason");

            }
        }

        private Visibility _CancelFoodVisibility;
        public Visibility CancelFoodVisibility
        {
            get
            {
                return _CancelFoodVisibility;
            }
            set
            {
                _CancelFoodVisibility = value;
                OnPropertyChanged("CancelFoodVisibility");
            }
        }
        private string _CancelReasonText;
        public string CancelReasonText
        {
            get
            {
                return _CancelReasonText;
            }
            set
            {
                _CancelReasonText = value;
                OnPropertyChanged("CancelReasonText");
            }
        }
        public ICommand AddCommand { get; set; }

        public ICommand IsCheckCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public string CancelReasonString;
        public void GetDetail()
        {
            if (ListCancelReason == null)
            {
                ListCancelReason = new ObservableCollection<CancelReason>();
            }
            else
            {
                ListCancelReason.Clear();
            }
            OrdersClient ordersClient = new OrdersClient(this, this, this);
            CancelReasonResponse cancelReasons = ordersClient.GetCancelReasons(currentUser.RestaurantBrandId);
            if (cancelReasons != null && cancelReasons.Status == (int)ResponseEnum.OK)
            {
                cancelReasons.Data.ForEach(ListCancelReason.Add);
            }
            CancelFoodVisibility = Visibility.Collapsed;

        }
        public CancelReasonFoodViewModel()
        {
            GetDetail();
            IsCheckCommand = new RelayCommand<CancelReason>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    if (p.Id == Constants.CANCEL_REASON_OTHER)
                    {
                        CancelFoodVisibility = Visibility.Visible;
                    }
                    else
                    {
                        CancelFoodVisibility = Visibility.Collapsed;
                    }
                    if(p.IsCheckCancel)
                    {
                        foreach (CancelReason reason in ListCancelReason.ToList())
                        {
                            if (reason.Id != p.Id)
                                reason.IsCheckCancel = false;
                            int index = ListCancelReason.IndexOf(reason);
                            ListCancelReason.Remove(reason);
                            ListCancelReason.Insert(index, reason);
                        }    
                    }    
                }

            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                CancelReason cancel = ListCancelReason.Where(x => x.IsCheckCancel).FirstOrDefault();
                if (cancel != null)
                {

                    if (cancel.Id == Constants.CANCEL_REASON_OTHER)
                    {
                        CancelReasonString = string.IsNullOrEmpty(CancelReasonText) ? "" : CancelReasonText;
                    }
                    else
                    {
                        CancelReasonString = cancel.Content;
                    }
                    p.Close();
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CANCEL_REASON);
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
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

using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;

namespace TechresStandaloneSale.ViewModels
{
   public class HistoryInputMoneyCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public bool _DialogHostOpen;
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
        private DateTime _DateTimeFromInput;
        public DateTime DateTimeFromInput
        {
            get
            {
                return _DateTimeFromInput;
            }
            set
            {
                _DateTimeFromInput = value;
                OnPropertyChanged("DateTimeFromInput");
            }
        }
        private DateTime _DateTimeToInput;
        public DateTime DateTimeToInput
        {
            get
            {
                return _DateTimeToInput;
            }
            set
            {
                _DateTimeToInput = value;
                OnPropertyChanged("DateTimeToInput");
            }
        }
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
        private ObservableCollection<CustomerTopUpHistory> _CustomerTopUpHistoryList = new ObservableCollection<CustomerTopUpHistory>();
        public ObservableCollection<CustomerTopUpHistory> CustomerTopUpHistoryList { get => _CustomerTopUpHistoryList; set { _CustomerTopUpHistoryList = value; OnPropertyChanged("CustomerTopUpHistory"); } }
        public ICommand AddCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private int CurrentPage =1;
        private int TotalPage =1;
        public void GetDetail(string FromDate,string ToDate,int Page)
        {
            if (CustomerTopUpHistoryList != null)
            {
                CustomerTopUpHistoryList.Clear();
            }
            else
            {
                CustomerTopUpHistoryList = new ObservableCollection<CustomerTopUpHistory>();
            }
            int limit = 50;
            TopUpCardClient client = new TopUpCardClient(this, this, this);
            CustomerTopUpHistoryResponse response = client.GetListTopUpHistory(Constants.ALL, currentUser.BranchId, currentUser.Id, FromDate, ToDate, Page, limit);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                response.Data.List.ForEach(CustomerTopUpHistoryList.Add);
                if (response.Data.TotalRecord % response.Data.Limit != 0)
                {
                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit) + 1;
                }
                else
                {
                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit);
                }
                PageContent = CurrentPage + "/" + (TotalPage == 0?1: TotalPage);
            }
        }
        public HistoryInputMoneyCustomerViewModel()
        {
            DateTimeFromInput = DateTime.Now;
            DateTimeToInput = DateTime.Now;
            GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
            AddCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                InputMoneyCustomerWindow window = new InputMoneyCustomerWindow();
                window.DataContext = new InputMoneyCustomerViewModel();
                window.ShowDialog();
                GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                Constants.CURRENT_BRANCH_ID = currentUser.BranchId;
                GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                //if(DateTimeToInput > DateTimeFromInput)
                //{
                //    NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                //    return;
                //}
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
            });
            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if(CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                }               
            });
            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if(CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                }                
            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                GetDetail(Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
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

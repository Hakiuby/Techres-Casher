using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ActivityOrderViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }

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

        private int _TotalPage;
        public int TotalPage { get => _TotalPage; set { _TotalPage = value; OnPropertyChanged("TotalPage"); UpdateEnableState(); } }
        private int _CurrentPage;
        public int CurrentPage { get => _CurrentPage; set { _CurrentPage = value; OnPropertyChanged("CurrentPage"); UpdateEnableState(); } }


        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }

        private bool _isNextEnabled;
        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged("IsNextEnabled"); }
        }
        private bool _isPreviousEnabled;
        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set { _isPreviousEnabled = value; OnPropertyChanged("IsPreviousEnabled"); }
        }
        private bool _isFirstEnabled;
        public bool IsFirstEnabled
        {
            get { return _isFirstEnabled; }
            set { _isFirstEnabled = value; OnPropertyChanged("IsFirstEnabled"); }
        }
        private bool _isLastEnabled;
        public bool IsLastEnabled
        {
            get { return _isLastEnabled; }
            set { _isLastEnabled = value; OnPropertyChanged("IsLastEnabled"); }
        }

        private void UpdateEnableState()
        {
            IsFirstEnabled = CurrentPage > 1;
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < TotalPage;
            IsLastEnabled = CurrentPage < TotalPage;
        }

        private ObservableCollection<ActivityLog> _ListActivity ;
        public ObservableCollection<ActivityLog> ListActivity
        {
            get
            {
                return _ListActivity;
            }
            set
            {
                _ListActivity = value;
                OnPropertyChanged("ListActivity");
            }
        }
        User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void GetListLogData(long orderId, int page)
        {
            CurrentPage = page;
            TotalPage = 1;
            DialogHostOpen = true;
            if (ListActivity!= null)
            {
                ListActivity.Clear();
            }
            else
            {
                ListActivity = new ObservableCollection<ActivityLog>();
            }
            ActivityLogClient client = new ActivityLogClient(this, this, this);
            ActivityLogListResponse activityLogResponse =  client.GetListActivity(orderId, (long)LimitEnum.DEFAULT,page);
            if (activityLogResponse != null && activityLogResponse.Status == (int)ResponseEnum.OK && activityLogResponse.Data != null && activityLogResponse.Data.ActivityLogs != null)
            {
                activityLogResponse.Data.ActivityLogs.ForEach(ListActivity.Add);
                if (activityLogResponse.Data.TotalRecord % activityLogResponse.Data.Limit != 0)
                {
                    TotalPage = (int)(activityLogResponse.Data.TotalRecord / activityLogResponse.Data.Limit) + 1;
                    UpdateEnableState();
                }
                else
                {
                    TotalPage = (int)(activityLogResponse.Data.TotalRecord / activityLogResponse.Data.Limit);
                    UpdateEnableState();
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
            PageContent = CurrentPage + "/" + (TotalPage == 0 ? 1 : TotalPage);
        }

        public ActivityOrderViewModel(long orderId)
        {
            if (currentUser != null)
            {
                GetListLogData(orderId, 1);

                PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if (ListActivity != null)
                        ListActivity.Clear();
                    GetListLogData(orderId, CurrentPage);

                });
                PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = TotalPage;
                    if (ListActivity != null)
                        ListActivity.Clear();
                    GetListLogData(orderId, CurrentPage);
                });
                PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (CurrentPage > 1)
                    {
                        CurrentPage = CurrentPage - 1;
                        if (ListActivity != null) ListActivity.Clear();
                        GetListLogData(orderId, CurrentPage);
                    }
                });

                PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (CurrentPage < TotalPage)
                    {
                        CurrentPage = CurrentPage + 1;
                        if (ListActivity != null) ListActivity.Clear();
                        GetListLogData(orderId, CurrentPage);
                    }
                });

            }
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

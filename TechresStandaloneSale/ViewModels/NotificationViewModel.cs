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

namespace TechresStandaloneSale.ViewModels
{
   public class NotificationViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand NextPageCommand { get; set; }

        private ObservableCollection<ActivityLog> _NotificationList = new ObservableCollection<ActivityLog>();
        public ObservableCollection<ActivityLog> NotificationList
        {
            get
            {
                return _NotificationList;
            }
            set
            {
                _NotificationList = value;
                OnPropertyChanged("NotificationList");
            }
        }
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
        private string _ContentTitle { get; set; }
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private int CurrentPage;

        private int TotalPage;
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
        private DateTime _DateTimeFromInput { get; set; }
        public DateTime DateTimeFromInput { get => _DateTimeFromInput; set { _DateTimeFromInput = value; OnPropertyChanged("DateTimeFromInput"); } }
        private DateTime _DateTimeToInput { get; set; }
        public DateTime DateTimeToInput { get => _DateTimeToInput; set { _DateTimeToInput = value; OnPropertyChanged("DateTimeToInput"); } }
        private DateTime _DatetimeDisplayDateEnd { get; set; }
        public DateTime DatetimeDisplayDateEnd { get => _DatetimeDisplayDateEnd; set { _DatetimeDisplayDateEnd = value; OnPropertyChanged("DatetimeDisplayDateEnd"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public async void GetListLogData(int page, int NotificationEmployeeType)
        {
            CurrentPage = page;
            TotalPage = 1;
            DialogHostOpen = true;
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_HISTORY_LOG_USER_CONTENT_TITLE, 0);
            NotificationClient client = new NotificationClient(this, this, this);
            ActivityLogResponse activityLogResponse = await System.Threading.Tasks.Task.Run(() => client.GetNotificationEmployeeResponse(currentUser.NodeAccessToken, CurrentPage, NotificationEmployeeType));
            if (activityLogResponse != null && activityLogResponse.Status == (int)ResponseEnum.OK && activityLogResponse.Data != null && activityLogResponse.Data.ActivityLogDatas != null)
            {
                if (CurrentPage == 1)
                {
                    if (NotificationList != null)
                    {
                        NotificationList.Clear();
                    }
                    else
                    {
                        NotificationList = new ObservableCollection<ActivityLog>();
                    }
                }
               
                activityLogResponse.Data.ActivityLogDatas.ForEach(NotificationList.Add);
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_NOTIFICATION_USER_CONTENT_TITLE, activityLogResponse.Data.TotalRecord);
                if (activityLogResponse.Data.TotalRecord % activityLogResponse.Data.Limit != 0)
                {
                    TotalPage = (int)(activityLogResponse.Data.TotalRecord / activityLogResponse.Data.Limit) + 1;
                }
                else
                {
                    TotalPage = (int)(activityLogResponse.Data.TotalRecord / activityLogResponse.Data.Limit);
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;

            PageContent = CurrentPage + "/" + (TotalPage == 0 ? 1 : TotalPage);
        }
        public NotificationViewModel(int NotificationEmployeeType)
        {
            if (currentUser != null)
            {
                GetListLogData(Constants.OFFSET, NotificationEmployeeType);
                NextPageCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (CurrentPage < TotalPage)
                    {
                        CurrentPage = CurrentPage + 1;
                        GetListLogData(CurrentPage, NotificationEmployeeType);
                    }
                });

               
            }

        }

        public void LogError(Exception ex, string infoMessage)
        {
            WriteLog.logs(infoMessage);
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

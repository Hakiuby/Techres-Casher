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
    public class ActivityLogViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedFromCommand { get; set; }

        private ObservableCollection<ActivityLog> logList;
        public ObservableCollection<ActivityLog> LogList
        {
            get
            {
                return logList;
            }
            set
            {
                logList = value;
                OnPropertyChanged("LogList");
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

        #region Đạt
        private int _TotalPage;
        public int TotalPage { get => _TotalPage; set { _TotalPage = value; OnPropertyChanged("TotalPage"); UpdateEnableState(); } }
        private int _CurrentPage;
        public int CurrentPage { get => _CurrentPage; set { _CurrentPage = value; OnPropertyChanged("CurrentPage"); UpdateEnableState(); } }
        #endregion

        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }

        #region Đạt
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
        private DateTime _DatePickerDisplayDateEnd { get; set; }
        public DateTime DatePickerDisplayDateEnd { get => _DatePickerDisplayDateEnd; set { _DatePickerDisplayDateEnd = value; OnPropertyChanged("DatePickerDisplayDateEnd"); } }
        #endregion

        private DateTime _DateTimeFromInput { get; set; }
        public DateTime DateTimeFromInput { get => _DateTimeFromInput; set { _DateTimeFromInput = value; OnPropertyChanged("DateTimeFromInput"); } }
        private DateTime _DateTimeToInput { get; set; }
        public DateTime DateTimeToInput { get => _DateTimeToInput; set { _DateTimeToInput = value; OnPropertyChanged("DateTimeToInput"); } }
        private DateTime _DatetimeDisplayDateEnd { get; set; }
        public DateTime DatetimeDisplayDateEnd { get => _DatetimeDisplayDateEnd; set { _DatetimeDisplayDateEnd = value; OnPropertyChanged("DatetimeDisplayDateEnd"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public async void GetListLogData(int page,  DateTime fromDate, DateTime toDate)
        {
            CurrentPage = page;
            TotalPage = 1;
            DialogHostOpen = true;
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_HISTORY_LOG_USER_CONTENT_TITLE, 0);
            ActivityLogClient client = new ActivityLogClient(this, this, this);
            ActivityLogResponse activityLogResponse = await System.Threading.Tasks.Task.Run(()=> client.GetListLog(page, currentUser.Id,Utils.Utils.GetDateFormatVN(fromDate), Utils.Utils.GetDateFormatVN(toDate), (long)LimitEnum.FIFTY));
            if (activityLogResponse != null && activityLogResponse.Status == (int)ResponseEnum.OK && activityLogResponse.Data != null && activityLogResponse.Data.ActivityLogDatas!=null)
            {
                if (LogList != null)
                {
                    LogList.Clear();
                }
                else
                {
                    LogList = new ObservableCollection<ActivityLog>();
                }
                activityLogResponse.Data.ActivityLogDatas.ForEach(LogList.Add);
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_HISTORY_LOG_USER_CONTENT_TITLE, activityLogResponse.Data.TotalRecord);
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
        public ActivityLogViewModel()
        {
            if (currentUser != null)
            {
                DatePickerDisplayDateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTimeFromInput = DateTime.Now;
                DateTimeToInput = DateTime.Now;
                GetListLogData(Constants.OFFSET,  DateTimeFromInput, DateTimeToInput);

                SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if (LogList != null) LogList.Clear();
                    if (DateTimeToInput.DayOfYear < DateTimeFromInput.DayOfYear)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListLogData(CurrentPage, DateTimeFromInput, DateTimeToInput);
                });
                SelectionChangedFromCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if (LogList != null) LogList.Clear();
                    if ( DateTimeFromInput.DayOfYear > DateTimeToInput.DayOfYear)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListLogData(CurrentPage, DateTimeFromInput, DateTimeToInput);
                });
                PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if (LogList != null) LogList.Clear();
                    GetListLogData(CurrentPage, DateTimeFromInput, DateTimeToInput);
                });

                PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {

                    if (CurrentPage > 1)
                    {
                        CurrentPage = CurrentPage - 1;
                        if (LogList != null) LogList.Clear();
                        GetListLogData(CurrentPage,  DateTimeFromInput, DateTimeToInput);
                    }
                });

                PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (CurrentPage < TotalPage)
                    {
                        CurrentPage = CurrentPage + 1;
                        if (LogList != null) LogList.Clear();
                        GetListLogData(CurrentPage,  DateTimeFromInput, DateTimeToInput);
                    }
                      
                });

                PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = TotalPage;
                    if (LogList != null) LogList.Clear();
                    GetListLogData(CurrentPage,  DateTimeFromInput, DateTimeToInput);
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

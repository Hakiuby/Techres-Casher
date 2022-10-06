using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
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
    public class OpenWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand AddCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        

        private string _FromTime;
        public string FromTime
        {
            get
            {
                return _FromTime;
            }
            set
            {
                _FromTime = value;

                OnPropertyChanged("FromTime");
            }
        }

        private string _ToTime;
        public string ToTime
        {
            get
            {
                return _ToTime;
            }
            set
            {
                _ToTime = value;

                OnPropertyChanged("ToTime");
            }
        }
        private string _InputMoney;
        public string InputMoney
        {
            get
            {
                return _InputMoney;
            }
            set
            {
                _InputMoney = value;

                OnPropertyChanged("InputMoney");
            }
        }
        private ObservableCollection<WorkingSession> _WorkingSessionList = new ObservableCollection<WorkingSession>();
        public ObservableCollection<WorkingSession> WorkingSessionList
        {
            get
            {
                return _WorkingSessionList;
            }
            set
            {
                _WorkingSessionList = value;
                OnPropertyChanged("WorkingSession");
            }
        }
        private WorkingSession _WorkingSessionItem { get; set; }
        public WorkingSession WorkingSessionItem { get => _WorkingSessionItem; set { _WorkingSessionItem = value; OnPropertyChanged("WorkingSessionItem"); } }

        public bool isClose; // Đạt : kiểm tra không bị trùng với CloseCommand
        public bool isSuccess;

        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void GetWorkingSession()
        {
            if (WorkingSessionList!= null)
            {
                WorkingSessionList.Clear();
            }
            else
            {
                WorkingSessionList = new ObservableCollection<WorkingSession>();
            }
            EmployeeClient client = new EmployeeClient(this, this, this);
            WorkingSessionResponses response = client.GetWorkingSessionByEmployee(currentUser.Id, currentUser.BranchId);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.ForEach(WorkingSessionList.Add); 
            }
        }
        public void GetDetail(decimal money)
        {
            GetWorkingSession();

            WorkingSessionItem = WorkingSessionList!= null && WorkingSessionList.Count>0? WorkingSessionList[0] : null;
            InputMoney = Utils.Utils.FormatMoney(money);
            FromTime = "00:00";
            ToTime = "00:00";
            if (WorkingSessionItem!= null)
            {
                FromTime = WorkingSessionItem.FromHour;
                ToTime = WorkingSessionItem.ToHour;
            }
        }
        public OpenWorkingSessionViewModel(decimal money )
        {
            GetDetail(money);
            isSuccess = false;
            isClose = false;
            SelectionChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (WorkingSessionItem != null)
                {
                    FromTime = WorkingSessionItem.FromHour;
                    ToTime = WorkingSessionItem.ToHour;
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                #region Đạt
                isClose = true;
                Process.Start(Application.ResourceAssembly.Location);
                Environment.Exit(0);
                //Application.Current.Shutdown();
                #endregion
                //Process.Start(Application.ResourceAssembly.Location);
                //Application.Current.Shutdown();
            });
          AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                decimal put;
                if (!string.IsNullOrEmpty(InputMoney))
                {
                    put = decimal.Parse(InputMoney.TrimStart(','));
                }
                else
                {
                    put = 0;
                }
                WorkingSessionClient client = new WorkingSessionClient(this, this, this);
                BaseResponse response = client.OpenSession(put, WorkingSessionItem!= null ? WorkingSessionItem.Id: 0);
                if (response!= null && response.Status == (int)ResponseEnum.OK)
                {
                    isSuccess = true;
                    p.Close();
                }
            });
        }

        #region Đạt
        public void openWorkingSessionWindow_Closing(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
            if (isSuccess == false)
            {
                if (!isClose)
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    //Application.Current.Shutdown();
                    Environment.Exit(0);
                }
            }
        }
        #endregion

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

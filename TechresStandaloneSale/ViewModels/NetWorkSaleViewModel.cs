using Nest;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TechresStandaloneSale.BroadCast;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{

    public class NetWorkSaleViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _ComputerName;
        public string ComputerName { get => _ComputerName; set { _ComputerName = value; OnPropertyChanged("ComputerName"); } }

        private string _IpAddress;
        public string IpAddress { get => _IpAddress; set { _IpAddress = value; OnPropertyChanged("IpAddress"); } }
        private bool _IsOkReload;
        public bool IsOkReload { get => _IsOkReload; set { _IsOkReload = value; OnPropertyChanged("IsOkReload"); } }
        private bool _SaveBtnEnable;
        public bool SaveBtnEnable { get => _SaveBtnEnable; set { _SaveBtnEnable = value; OnPropertyChanged("SaveBtnEnable"); } }
        private Visibility _IPLocalVisibility;
        public Visibility IPLocalVisibility { get => _IPLocalVisibility; set { _IPLocalVisibility = value; OnPropertyChanged("IPLocalVisibility"); } }
        private Visibility _ResetingVisibility;
        public Visibility ResetingVisibility { get => _ResetingVisibility; set { _ResetingVisibility = value; OnPropertyChanged("ResetingVisibility"); } }
        private Visibility _ReloadIconVisibility;
        public Visibility ReloadIconVisibility { get => _ReloadIconVisibility; set { _ReloadIconVisibility = value; OnPropertyChanged("ReloadIconVisibility"); } }
        public ICommand AddCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public bool IsConfirm = true;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            IsOkReload = true;
            ReloadIconVisibility = Visibility.Visible;
            ReloadIconVisibility = Visibility.Visible;
            ResetingVisibility = Visibility.Collapsed;
            string tmp1 = (string)Utils.Utils.GetCacheValue(Constants.CACHE_BROAD_CAST_CLIENT);
            if (!string.IsNullOrEmpty(tmp1))
            {
                BroadCastResponseModel responseModel = JsonConvert.DeserializeObject<BroadCastResponseModel>(tmp1);
                if (responseModel != null)
                {
                    ComputerName = responseModel.ComputerName;
                    IpAddress = responseModel.IpAdress;
                    IPLocalVisibility = Visibility.Visible;
                    SaveBtnEnable = true;
                }
            }
            dispatcherTimer.Stop();
        }
        public NetWorkSaleViewModel()
        {
            try 
            {
                SaveBtnEnable = false;
                IsOkReload = true;
                ResetingVisibility = Visibility.Collapsed;
                ReloadIconVisibility = Visibility.Visible;
                IPLocalVisibility = Visibility.Collapsed;
                Application.Current.Dispatcher.Invoke((Action) async delegate
                {
                     await Task.Run(() =>
                    {
                        BroadCast.BroadCastClient.StartListener();
                    });
                });
                System.Threading.Thread.Sleep(1000);
                string tmp = (string)Utils.Utils.GetCacheValue(Constants.CACHE_BROAD_CAST_CLIENT);
                if (!string.IsNullOrEmpty(tmp))
                {
                    BroadCastResponseModel responseModel = JsonConvert.DeserializeObject<BroadCastResponseModel>(tmp);
                    if (responseModel != null)
                    {
                        ComputerName = responseModel.ComputerName;
                        IpAddress = responseModel.IpAdress;
                        IPLocalVisibility = Visibility.Visible;
                        SaveBtnEnable = true;
                    }
                }
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    p.Close();
                    IsConfirm = false;
                    BroadCastClient.StopListener();
                });
                AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if (SaveBtnEnable)
                    {
                        p.Close();
                        IsConfirm = true;
                    }
                });
                RefreshCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ResetingVisibility = Visibility.Visible;
                    ReloadIconVisibility = Visibility.Hidden;
                    IPLocalVisibility = Visibility.Collapsed;
                    IsOkReload = false;
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                    dispatcherTimer.Start();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
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
        public void Set(string cacheKey, object item, int minutes)
        {

        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

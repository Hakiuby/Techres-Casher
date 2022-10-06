using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Services;
using System;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Interface;
using RestSharp.Deserializers;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Views;
using System.Windows.Controls;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.UserControlView.ReportGiftFood;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.UserControlView.CreateOrder;
using TechresStandaloneSale.Views.Dialogs;
using System.Threading.Tasks;
using TechresStandaloneSale.UserControlView.ReportRevenue;
using TechresStandaloneSale.UserControlView.ReportFood;
using TechresStandaloneSale.ViewModels.ReportFood;
using TechresStandaloneSale.ViewModels.ReportGiftFood;
using TechresStandaloneSale.UserControlView.ReportCustomerSlot;
using TechresStandaloneSale.ViewModels.ReportCustomerSlot;
using System.Diagnostics;
using System.IO;
using TechresStandaloneSale.UserControlView.ReportEmployee;
using TechresStandaloneSale.ViewModels.ReportEmployee;
using TechresStandaloneSale.ViewModels.SettingS;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using TechresStandaloneSale.Views.CreateEmployeeOfManager;
using TechresStandaloneSale.ViewModels.CreateEmployeeOfManager;
using SocketIOClient.Newtonsoft.Json;
using System.Net;

namespace TechresStandaloneSale.ViewModels
{
    public class MainViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public bool Isloaded = false;

        public ICommand SupportCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public ICommand FoodBranchListCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SyncDataCommand { get; set; }
        public ICommand QRCodeRegistrationCustomerCommand { get; set; }
        public ICommand ExpenseCommand { get; set; }
        public ICommand ExpenseCashierCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ItemFinishClick { get; set; }
        public ICommand GoHomeCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand ViewAllCommand { get; set; }
        public ICommand ReportRevenueCommand { get; set; }
        public ICommand ReportGiftFoodCommand { get; set; }
        public ICommand BatenderCommand { get; set; }
        public ICommand ManageHistoryFoodCommand { get; set; }
        public ICommand FoodTakeAwayCommand { get; set; }
        public ICommand ReportCookCommand { get; set; }
        //public ICommand EmployeeCommand { get; set; }
        //public ICommand EmployeeRoleCommand { get; set; }
        //public ICommand CategoryCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand HistoryLogCommand { get; set; }
        public ICommand BookingCommand { get; set; }
        public ICommand OrderingListCommand { get; set; }
        public ICommand BtnAddOrderCommand { get; set; }
        public ICommand PermissionRoleCommand { get; set; }
        public ICommand PermissionEmployeeCommand { get; set; }
        public ICommand WorkingSessionCommand { get; set; }
        public ICommand SalaryLevelCommand { get; set; }
        public ICommand ReportFinishWorkingSessionCommand { get; set; }
        public ICommand HistoryEndWorkingSessionCommand { get; set; }
        public ICommand HistoryDebitCommand { get; set; }
        public ICommand ListCustomerDebitCommand { get; set; }
        public ICommand QRCodeBranchCommand { get; set; }
        //public ICommand ReportCustomerSlotCommand { get; set; }
        public ICommand FoodListCommand { get; set; }
        //public ICommand EditPriceCommand { get; set; }
        //public ICommand AreaCommand { get; set; }
        //public ICommand TableCommand { get; set; }
        public ICommand NoteListCommand { get; set; }
        public ICommand PaymentReasonCommand { get; set; }
        public ICommand RejectReasonCommand { get; set; }
        public ICommand ReportBalanceMoneyCommand { get; set; }
        public ICommand UnitFoodCommand { get; set; }
        public ICommand MoveListFoodCommand { get; set; }
        public ICommand ReportEmployeeCommand { get; set; }
        public ICommand EmployeeAdvancedSalaryLevelCommand { get; set; }
        public ICommand BtnAddOrderTakeAwayCommand { get; set; }
        public ICommand InputMoneyToCardCommand { get; set; }
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
        #region Dat
        //private Brush _color1;
        //public Brush Color1
        //{
        //    get { return _color1; }
        //    set { _color1 = value; OnPropertyChanged("Color1"); }
        //}
        //private Brush _color2;
        //public Brush Color2
        //{
        //    get { return _color2; }
        //    set { _color2 = value; OnPropertyChanged("Color2"); }
        //}
        #endregion
        private Visibility _FoodListBranchVisibility;
        public Visibility FoodListBranchVisibility
        {
            get
            {
                return _FoodListBranchVisibility;
            }
            set
            {
                _FoodListBranchVisibility = value;

                OnPropertyChanged("FoodListBranchVisibility");
            }
        }
        private Visibility _SyncDataVisibility;
        public Visibility SyncDataVisibility
        {
            get
            {
                return _SyncDataVisibility;
            }
            set
            {
                _SyncDataVisibility = value;

                OnPropertyChanged("SyncDataVisibility");
            }
        }

        private Visibility _QRCodeRegistrationCustomerVisibility;
        public Visibility QRCodeRegistrationCustomerVisibility
        {
            get
            {
                return _QRCodeRegistrationCustomerVisibility;
            }
            set
            {
                _QRCodeRegistrationCustomerVisibility = value;

                OnPropertyChanged("QRCodeRegistrationCustomerVisibility");
            }
        }
        private Visibility _ExpenseCashierVisibility;
        public Visibility ExpenseCashierVisibility
        {
            get
            {
                return _ExpenseCashierVisibility;
            }
            set
            {
                _ExpenseCashierVisibility = value;

                OnPropertyChanged("ExpenseCashierVisibility");
            }
        }

        private Visibility _EmployeeAdvancedSalaryVisibility;
        public Visibility EmployeeAdvancedSalaryVisibility
        {
            get
            {
                return _EmployeeAdvancedSalaryVisibility;
            }
            set
            {
                _EmployeeAdvancedSalaryVisibility = value;

                OnPropertyChanged("EmployeeAdvancedSalaryVisibility");
            }
        }
        private Visibility _UnitFoodVisibility;
        public Visibility UnitFoodVisibility
        {
            get
            {
                return _UnitFoodVisibility;
            }
            set
            {
                _UnitFoodVisibility = value;

                OnPropertyChanged("UnitFoodVisibility");
            }

        }
        private Visibility _EditPriceVisibility;
        public Visibility EditPriceVisibility
        {
            get
            {
                return _EditPriceVisibility;
            }
            set
            {
                _EditPriceVisibility = value;

                OnPropertyChanged("EditPriceVisibility");
            }

        }
        private Visibility _BatenderVisibility;
        public Visibility BatenderVisibility
        {
            get
            {
                return _BatenderVisibility;
            }
            set
            {
                _BatenderVisibility = value;

                OnPropertyChanged("BatenderVisibility");
            }

        }
        private Visibility _ReportBalanceMoneyVisibility;
        public Visibility ReportBalanceMoneyVisibility
        {
            get
            {
                return _ReportBalanceMoneyVisibility;
            }
            set
            {
                _ReportBalanceMoneyVisibility = value;

                OnPropertyChanged("ReportBalanceMoneyVisibility");
            }

        }
        private Visibility _HistoryDebitVisibility;
        public Visibility HistoryDebitVisibility
        {
            get
            {
                return _HistoryDebitVisibility;
            }
            set
            {
                _HistoryDebitVisibility = value;

                OnPropertyChanged("HistoryDebitVisibility");
            }

        }
        private Visibility _ExpenseVisibility;
        public Visibility ExpenseVisibility
        {
            get
            {
                return _ExpenseVisibility;
            }
            set
            {
                _ExpenseVisibility = value;

                OnPropertyChanged("ExpenseVisibility");
            }
        }
        private Visibility _ManageHistoryFood;
        public Visibility ManageHistoryFood
        {
            get
            {
                return _ManageHistoryFood;
            }
            set
            {
                _ManageHistoryFood = value;
                OnPropertyChanged("ManageHistoryFood");
            }
        }

        private Visibility _InputMoneyToCardVisibility;
        public Visibility InputMoneyToCardVisibility
        {
            get
            {
                return _InputMoneyToCardVisibility;
            }
            set
            {
                _InputMoneyToCardVisibility = value;

                OnPropertyChanged("InputMoneyToCardVisibility");
            }
        }
        private Visibility _EmployeeVisibility;
        public Visibility EmployeeVisibility
        {
            get
            {
                return _EmployeeVisibility;
            }
            set
            {
                _EmployeeVisibility = value;

                OnPropertyChanged("EmployeeVisibility");
            }
        }
        private Visibility _WorkingSessionVisibility;
        public Visibility WorkingSessionVisibility
        {
            get
            {
                return _WorkingSessionVisibility;
            }
            set
            {
                _WorkingSessionVisibility = value;

                OnPropertyChanged("WorkingSessionVisibility");
            }
        }
        private Visibility _BookingVisibility;
        public Visibility BookingVisibility
        {
            get
            {
                return _BookingVisibility;
            }
            set
            {
                _BookingVisibility = value;

                OnPropertyChanged("BookingVisibility");
            }
        }


        private Visibility _OrderVisibility;
        public Visibility OrderVisibility
        {
            get
            {
                return _OrderVisibility;
            }
            set
            {
                _OrderVisibility = value;

                OnPropertyChanged("OrderVisibility");
            }
        }

        private Visibility _ReportRevenueWorkingSessionVisibility;
        public Visibility ReportRevenueWorkingSessionVisibility
        {
            get
            {
                return _ReportRevenueWorkingSessionVisibility;
            }
            set
            {
                _ReportRevenueWorkingSessionVisibility = value;

                OnPropertyChanged("ReportRevenueWorkingSessionVisibility");
            }
        }
        private Visibility _ReportFoodVisibility;
        public Visibility ReportFoodVisibility
        {
            get
            {
                return _ReportFoodVisibility;
            }
            set
            {
                _ReportFoodVisibility = value;

                OnPropertyChanged("ReportFoodVisibility");
            }
        }
        private Visibility _ReportRevenueVisibility;
        public Visibility ReportRevenueVisibility
        {
            get
            {
                return _ReportRevenueVisibility;
            }
            set
            {
                _ReportRevenueVisibility = value;

                OnPropertyChanged("ReportRevenueVisibility");
            }
        }




        private Visibility _ReportGiftFoodVisibility;
        public Visibility ReportGiftFoodVisibility
        {
            get
            {
                return _ReportGiftFoodVisibility;
            }
            set
            {
                _ReportGiftFoodVisibility = value;

                OnPropertyChanged("ReportGiftFoodVisibility");
            }
        }


        private Visibility _ReportHistoryLogVisibility;
        public Visibility ReportHistoryLogVisibility
        {
            get
            {
                return _ReportHistoryLogVisibility;
            }
            set
            {
                _ReportHistoryLogVisibility = value;

                OnPropertyChanged("ReportHistoryLogVisibility");
            }
        }

        private Visibility _CategoryVisibility;
        public Visibility CategoryVisibility
        {
            get
            {
                return _CategoryVisibility;
            }
            set
            {
                _CategoryVisibility = value;

                OnPropertyChanged("CategoryVisibility");
            }
        }
        private Visibility _SettingVisibility;
        public Visibility SettingVisibility
        {
            get
            {
                return _SettingVisibility;
            }
            set
            {
                _SettingVisibility = value;

                OnPropertyChanged("SettingVisibility");
            }
        }
        private Visibility _AddOrderVisibility;
        public Visibility AddOrderVisibility
        {
            get
            {
                return _AddOrderVisibility;
            }
            set
            {
                _AddOrderVisibility = value;

                OnPropertyChanged("AddOrderVisibility");
            }
        }
        private Visibility _AddOrderTakeAwayVisibility;
        public Visibility AddOrderTakeAwayVisibility
        {
            get
            {
                return _AddOrderTakeAwayVisibility;
            }
            set
            {
                _AddOrderTakeAwayVisibility = value;

                OnPropertyChanged("AddOrderTakeAwayVisibility");
            }
        }

        private Visibility _HistoryEndWorkingSessionVisibility;
        public Visibility HistoryEndWorkingSessionVisibility
        {
            get
            {
                return _HistoryEndWorkingSessionVisibility;
            }
            set
            {
                _HistoryEndWorkingSessionVisibility = value;

                OnPropertyChanged("HistoryEndWorkingSessionVisibility");
            }
        }

        private Visibility _QrCodeBranchVisibility;
        public Visibility QrCodeBranchVisibility
        {
            get
            {
                return _QrCodeBranchVisibility;
            }
            set
            {
                _QrCodeBranchVisibility = value;

                OnPropertyChanged("QrCodeBranchVisibility");
            }
        }

        private Visibility _ReportCustomerSlotVisibility;
        public Visibility ReportCustomerSlotVisibility
        {
            get
            {
                return _ReportCustomerSlotVisibility;
            }
            set
            {
                _ReportCustomerSlotVisibility = value;

                OnPropertyChanged("ReportCustomerSlotVisibility");
            }
        }

        private Visibility _FoodListVisibility;
        public Visibility FoodListVisibility
        {
            get
            {
                return _FoodListVisibility;
            }
            set
            {
                _FoodListVisibility = value;

                OnPropertyChanged("FoodListVisibility");
            }
        }

        private Visibility _TableVisibility;
        public Visibility TableVisibility
        {
            get
            {
                return _TableVisibility;
            }
            set
            {
                _TableVisibility = value;

                OnPropertyChanged("TableVisibility");
            }
        }
        private Visibility _AreaVisibility;
        public Visibility AreaVisibility
        {
            get
            {
                return _AreaVisibility;
            }
            set
            {
                _AreaVisibility = value;

                OnPropertyChanged("AreaVisibility");
            }
        }
        private Visibility _FoodTakeAwayVisibility;
        public Visibility FoodTakeAwayVisibility
        {
            get
            {
                return _FoodTakeAwayVisibility;
            }
            set
            {
                _FoodTakeAwayVisibility = value;

                OnPropertyChanged("FoodTakeAwayVisibility");
            }
        }
        private Visibility _MoveFoodKitchenVisibility;
        public Visibility MoveFoodKitchenVisibility
        {
            get
            {
                return _MoveFoodKitchenVisibility;
            }
            set
            {
                _MoveFoodKitchenVisibility = value;

                OnPropertyChanged("MoveFoodKitchenVisibility");
            }
        }

        private Visibility _NoteFoodVisibility;
        public Visibility NoteFoodVisibility
        {
            get
            {
                return _NoteFoodVisibility;
            }
            set
            {
                _NoteFoodVisibility = value;

                OnPropertyChanged("NoteFoodVisibility");
            }
        }
        private Visibility _PaymentReasonListVisibility;
        public Visibility PaymentReasonListVisibility
        {
            get
            {
                return _PaymentReasonListVisibility;
            }
            set
            {
                _PaymentReasonListVisibility = value;

                OnPropertyChanged("PaymentReasonListVisibility");
            }
        }

        private Visibility _RejectReasonVisibility;
        public Visibility RejectReasonVisibility
        {
            get
            {
                return _RejectReasonVisibility;
            }
            set
            {
                _RejectReasonVisibility = value;

                OnPropertyChanged("RejectReasonVisibility");
            }
        }

        //private Visibility _ReportEmployeeVisibility;
        //public Visibility ReportEmployeeVisibility
        //{
        //    get
        //    {
        //        return _ReportEmployeeVisibility;
        //    }
        //    set
        //    {
        //        _ReportEmployeeVisibility = value;

        //        OnPropertyChanged("ReportEmployeeVisibility");
        //    }
        //}
        private Visibility _HeaderAndFooterVisibility;
        public Visibility HeaderAndFooterVisibility
        {
            get
            {
                return _HeaderAndFooterVisibility;
            }
            set
            {
                _HeaderAndFooterVisibility = value;

                OnPropertyChanged("HeaderAndFooterVisibility");
            }
        }

        private ContentControl _MainContentControl;

        public User currentUser;
        private bool _showReport;
        public bool ShowReport { get => _showReport; set { _showReport = value; OnPropertyChanged("ShowReport"); } }

        private string _VersionTitle;
        public string VersionTitle { get => _VersionTitle; set { _VersionTitle = value; OnPropertyChanged("VersionTitle"); } }
        private string _ContentRestaurant { get; set; }
        public string ContentRestaurant { get => _ContentRestaurant; set { _ContentRestaurant = value; OnPropertyChanged("ContentRestaurant"); } }

        private string _EmployeeName { get; set; }
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged("EmployeeName"); } }
        private bool _NotificationIsOpen { get; set; }
        public bool NotificationIsOpen { get => _NotificationIsOpen; set { _NotificationIsOpen = value; OnPropertyChanged("NotificationIsOpen"); } }
        private string _NotificationCountNotView { get; set; }
        public string NotificationCountNotView { get => _NotificationCountNotView; set { _NotificationCountNotView = value; OnPropertyChanged("NotificationCountNotView"); } }
        private Visibility _NotificationCountViewVisibility { get; set; }
        public Visibility NotificationCountViewVisibility { get => _NotificationCountViewVisibility; set { _NotificationCountViewVisibility = value; OnPropertyChanged("NotificationCountViewVisibility"); } }

        private Brush _AddOrderBackground { get; set; }
        public Brush AddOrderBackground { get => _AddOrderBackground; set { _AddOrderBackground = value; OnPropertyChanged("AddOrderBackground"); } }
        private Brush _AddOrderTakeAwayBackground { get; set; }
        public Brush AddOrderTakeAwayBackground { get => _AddOrderTakeAwayBackground; set { _AddOrderTakeAwayBackground = value; OnPropertyChanged("AddOrderTakeAwayBackground"); } }
        private Brush _AddOrderForeground { get; set; }
        public Brush AddOrderForeground { get => _AddOrderForeground; set { _AddOrderForeground = value; OnPropertyChanged("AddOrderForeground"); } }
        private Brush _AddOrderTakeAwayForeground { get; set; }
        public Brush AddOrderTakeAwayForeground { get => _AddOrderTakeAwayForeground; set { _AddOrderTakeAwayForeground = value; OnPropertyChanged("AddOrderTakeAwayForeground"); } }
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
        public SettingData currentSetting;
        public Restaurant currentRestaurant;
        private Kitchen currentKitchen;
        public Branch currentBranch;
        public DeviceClient deviceClient = new DeviceClient();
        public List<FoodMenuItem> AllFood = new List<FoodMenuItem>();

        public void RealTimeUpdateVersion(Window p)
        {
            if (Utils.Utils.CheckInternet())
            {
                string LinkSocketWaiting = string.Format("update_app_windows");
                SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketWaiting);
                SocketIOManager.Instance.socket.On(LinkSocketWaiting, (data) =>
                {
                    VersionClient client = new VersionClient(this, this, this);
                    Models.Version v = client.GetVersionAPI();
                    if (v != null && v.Status == (int)ResponseEnum.OK && v.Data != null)
                    {
                        if (!v.Data.VersionName.Contains(Properties.Settings.Default.VERSION))
                        {
                            UpdateVersion version = new UpdateVersion();
                            version.VersionName.Text = v.Data.VersionName;
                            version.DownloadUrl.Text = v.Data.DownloadLink;
                            version.ContentNotification.Text = MessageValue.MESSAFE_CHECK_VERSION;
                            version.ShowDialog();
                            p.Close();
                        }
                    }
                });
            }
        }
        public async void GetRestaurantDetail(long restaurantId)
        {
            RestaurantClient client = new RestaurantClient(this, this, this);
            RestaurantResponse restaurantResponse = await Task.Run(() => client.getRestaurantInfo(restaurantId));
            if (restaurantResponse != null && restaurantResponse.Status == (int)ResponseEnum.OK && restaurantResponse.Data != null)
            {
                currentRestaurant = restaurantResponse.Data;
                Utils.Utils.AddCacheValue(Constants.CURRENT_RESTAURANT, restaurantResponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
            }
            else
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
        }
        public async void GetBranchDetail(long branchId)
        {
            BranchClient client = new BranchClient(this, this, this);
            BranchResponse response = await Task.Run(() => client.getBranchInfo(branchId));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                currentBranch = response.Data;
                Utils.Utils.AddCacheValue(Constants.CURRENT_BRANCH, response.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
            }
            else
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
        }
        private long NotificationCount = 0;
        public void CountNotification()
        {
            NotificationCount++;
            NotificationCountViewVisibility = Visibility.Visible;
            NotificationCountNotView = Utils.Utils.FormatMoney(NotificationCount);
            if (NotificationCount == 0)
            {
                NotificationCountViewVisibility = Visibility.Collapsed;
            }
        }
        private int NotificationEmployeeType = 0;
        public async void GetCountViewNotification()
        {
            NotificationCountViewVisibility = Visibility.Collapsed;
            NotificationClient client = new NotificationClient(this, this, this);
            LongResponse response = await Task.Run(() => client.GetLongResponse(currentUser.NodeAccessToken, NotificationEmployeeType));
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                if (response.Data > 0)
                {
                    NotificationCountViewVisibility = Visibility.Visible;
                    NotificationCountNotView = Utils.Utils.FormatMoney(response.Data);
                    NotificationCount = response.Data;
                }
            }
            else
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
        }

        private void GetNotificationEmployeeType()
        {
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                NotificationEmployeeType = 1;
            }
            else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
            {
                NotificationEmployeeType = 2;
            }
            else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
            {
                NotificationEmployeeType = 3;
            }
            else
            {
                NotificationEmployeeType = 0;
            }
        }
        public void GetNotificationList()
        {
            if (NotificationList != null)
            {
                NotificationList.Clear();
            }
            else
            {
                NotificationList = new ObservableCollection<ActivityLog>();
            }
            NotificationClient client = new NotificationClient(this, this, this);
            ActivityLogResponse response = client.GetNotificationEmployeeResponse(currentUser.NodeAccessToken, 1, NotificationEmployeeType);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                response.Data.ActivityLogDatas.ForEach(NotificationList.Add);
                if (NotificationCount > 20)
                {
                    NotificationCount = NotificationCount - 20;
                    CountNotification();
                }
                else
                {
                    NotificationCount = -1;
                    CountNotification();
                }

            }
            else
            {
                NotificationMessage.Error(MessageValue.INTERNAL_SERVER_ERROR);
            }
        }

        public void SetVisibilityHeaderAndFooter()
        {
            SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
            if (setting.IsHiddenHeaderAndFooter)
            {
                HeaderAndFooterVisibility = Visibility.Collapsed;
            }
            else
            {
                HeaderAndFooterVisibility = Visibility.Visible;
            }
        }

        public void RealTimeNotification()
        {
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                string LinkSocket = string.Format("restaurants/{0}/branches/{1}/notify_winapp_cashier", currentUser.RestaurantId, currentUser.BranchId);

                WriteLog.logs(LinkSocket);
                SocketIOManager.Instance.socket.On(LinkSocket, (data) =>
                {
                    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        if (data == null) return;
                        string JsonString = data.ToString();
                        WriteLog.logs(JsonString);
                        CountNotification();

                        //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                        SocketIOManager.Instance.socket.JsonSerializer = new NewtonsoftJsonSerializer();
                        NotificationRealTime notification = data.GetValue<NotificationRealTime>(0);
                        NotificationMessage.ShowInfomation(notification.Title, notification.Content, notification.CreatedAt);
                    }), DispatcherPriority.ContextIdle);
                });
            }
            else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
            {
                string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                SocketIOManager.Instance.socket.On(LinkSocketNotification, (data) =>
                {
                    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        if (data == null) return;
                        string JsonString = data.ToString();
                        WriteLog.logs(JsonString);
                        //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                        //NotificationRealTime notification = jsonResponse.ToObject<Models.NotificationRealTime>();
                        CountNotification();
                        NotificationRealTime notification = data.GetValue<Models.NotificationRealTime>();
                        NotificationMessage.ShowInfomation(notification.Title, notification.Content, notification.CreatedAt);
                    }), DispatcherPriority.ContextIdle);
                });
            }
            else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
            {
                string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                SocketIOManager.Instance.socket.On(LinkSocketNotification, (data) =>
                {
                    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        if (data == null) return;
                        string JsonString = data.ToString();
                        WriteLog.logs(JsonString);
                        CountNotification();
                        //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                        //NotificationRealTime notification = jsonResponse.ToObject<Models.NotificationRealTime>();
                        NotificationRealTime notification = data.GetValue<Models.NotificationRealTime>();
                        NotificationMessage.ShowInfomation(notification.Title, notification.Content, notification.CreatedAt);
                    }), DispatcherPriority.ContextIdle);
                });
            }


        }
        public void RealTime()
        {
            if (Constants.IS_NETWORK_ONLINE)
            {
                RealTimeNotification();
            }

        }
        #region Dat
        //public void MainMenu_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    var converter = new System.Windows.Media.BrushConverter();
        //    Color1 = (Brush)converter.ConvertFromString("#bee6fd");
        //}

        //public void MainMenu_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    var converter = new System.Windows.Media.BrushConverter();
        //    Color1 = (Brush)converter.ConvertFromString("#ffa233");
        //}

        //public void MenuUser_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    var converter = new System.Windows.Media.BrushConverter();
        //    Color2= (Brush)converter.ConvertFromString("#bee6fd");
        //}

        //public void MenuUser_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    var converter = new System.Windows.Media.BrushConverter();
        //    Color2 = (Brush)converter.ConvertFromString("#ffa233");
        //}
        #endregion
        #region Phan Viet Ha
        private ObservableCollection<PrivateAdvertsData> _LstBanner;
        public ObservableCollection<PrivateAdvertsData> LstBanner
        {
            get => _LstBanner; set { _LstBanner = value; OnPropertyChanged("LstBanner"); }
        }
        public void GetListBanner()
        {

        }

        #endregion
        public MainViewModel()
        {
            //NotificationMessage.notification = new ToastViewModel();
            DialogHostOpen = true;
            initVisibility();
            SetVisibilityHeaderAndFooter();
            VersionTitle = string.Format(MessageValue.MESSAGE_FROM_VERSION, Properties.Settings.Default.VERSION);
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
            currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
            ConfigData config = (ConfigData)Utils.Utils.GetCacheValue(Constants.CURRENT_CONFIG);
            if (currentUser != null)
            {
                GetNotificationEmployeeType();
                EmployeeName = currentUser.Name;
                // GetRestaurantKitchen(currentUser.BranchId);
                Application.Current.Dispatcher.Invoke((Action)async delegate
                {
                    await Task.Run(() => GetRestaurantDetail(currentUser.RestaurantId));
                    await Task.Run(() => GetBranchDetail(currentUser.BranchId));
                    await Task.Run(() => GetCountViewNotification());
                });
                if (string.IsNullOrEmpty(config.SystemServer) || config.SystemServer.ToLower().Contains("live"))
                {
                    ContentRestaurant = string.Format("{0} ({1})", currentUser.Name, currentUser.Username);
                }
                else
                {
                    ContentRestaurant = string.Format("{0} ({1}) - {2}  ", currentUser.Name, currentUser.Username, config.SystemServer);
                }


                if (currentSetting == null)
                {
                    WriteLog.logs("currentSetting null *** ");
                }
                else
                {
                    if (!currentSetting.IsWorkingOffline)
                    {
                        initVisibilityPermission();
                        SyncDataVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        initVisibilityLocal();
                        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                        {
                            SyncDataVisibility = Visibility.Visible;
                        }
                        else
                        {
                            SyncDataVisibility = Visibility.Collapsed;
                        }
                    }
                }
                NotificationCountNotView = "0";
            }
            DialogHostOpen = false;

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (Constants.IS_FIRST_LOGIN)
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_MANAGER), currentUser.Permissions))
                    {
                        Utils.Utils.GetBrands(false);
                        Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, false);
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BRAND_MANAGER), currentUser.Permissions))
                    {
                        Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, false);
                    }
                    Constants.IS_FIRST_LOGIN = false;
                }
                if (currentSetting.BranchType != (int)BranchTypeEnum.LARGE && currentSetting.BranchType < (long)BranchTypeEnum.LARGE)
                {
                    GetAllFoodList();
                }
                initUserControll(p);
                RealTimeUpdateVersion(p);
                AddOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                AddOrderTakeAwayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                AddOrderForeground = new SolidColorBrush(Colors.White);
                AddOrderTakeAwayForeground = new SolidColorBrush(Colors.White);

            });
            MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.WindowState = WindowState.Minimized;
            });
            ItemFinishClick = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_END_WORKING_SESSION;
                    string Title = MessageValue.MESSAGE_END_WORKING_SESSION_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        WorkingSessionClient workingSessionClient = new WorkingSessionClient(this, this, this);
                        OrderSession checkSession = workingSessionClient.GetCheckSessions();
                        if (checkSession != null && checkSession.Status == (int)ResponseEnum.OK)
                        {
                            if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_EXPIRED)
                            {
                                ConfirmDeleteWindow confirmWindow = new ConfirmDeleteWindow();
                                string content = MessageValue.MESSAGE_NEED_OPEN_WORKING_SECSION;
                                string title = MessageValue.MESSAGE_NOTIFICATION;
                                string yesContent = MessageValue.MESSAGE_YES_CONTENT;
                                string noContent = MessageValue.MESSAGE_NO_CONTENT;
                                confirmWindow.DataContext = new ConfirmViewModel(content, title, noContent, yesContent);
                                confirmWindow.ShowDialog();
                                var yesConfirm = confirmWindow.DataContext as ConfirmViewModel;
                                if (yesConfirm.isConfirm)
                                {
                                    System.Windows.Application.Current.Shutdown();
                                }

                                //foreach (Process Proc in Process.GetProcesses())
                                //{
                                //    if (Proc.ProcessName.Equals(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)) Proc.Kill();
                                //}

                            } // CA ĐÃ HẾT HẠN - 3
                            else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_NOT_OPEN)
                            {
                                ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                                endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                                endWorkingSessionWindow.ShowDialog();
                                var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                if (end.isSuccess)
                                {
                                    OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                    openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(0);
                                    openWorkingSessionWindow.ShowDialog();
                                    var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                    if (open.isSuccess)
                                    {
                                        ShowReport = true;
                                        RealTime();
                                        _MainContentControl.Content = new HomeUserControl();
                                        //DownloadIcon();
                                    }
                                }
                                //GoHome(p);
                            } // CHƯA MỞ CA -- 1
                            else if(checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_OPENED)
                            {
                                ConfirmDeleteWindow confirmWindow = new ConfirmDeleteWindow();
                                string content = MessageValue.MESSAGE_SESSION_OPENED;
                                string title = MessageValue.MESSAGE_NOTIFICATION;
                                string yesContent = MessageValue.MESSAGE_YES_CONTENT;
                                string noContent = MessageValue.MESSAGE_NO_CONTENT;
                                confirmWindow.DataContext = new ConfirmViewModel(content, title, noContent, yesContent);
                                confirmWindow.ShowDialog();
                                var yesConfirm = confirmWindow.DataContext as ConfirmViewModel;
                                if (yesConfirm.isConfirm)
                                {
                                    ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                                    endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                                    endWorkingSessionWindow.ShowDialog();
                                    var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                    if (end.isSuccess)
                                    {
                                        OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                        openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(0);
                                        openWorkingSessionWindow.ShowDialog();
                                        var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                        if (open.isSuccess)
                                        {
                                            ShowReport = true;
                                            RealTime();
                                            _MainContentControl.Content = new HomeUserControl();
                                            //DownloadIcon();
                                        }
                                    }
                                }
                                else
                                {
                                    WorkingSessionClient client = new WorkingSessionClient(this, this, this);
                                    BaseResponse response = client.CreateeteOrderSession(checkSession.Data.OrderSessionId, currentUser.BranchId);
                                    if(response.Status == (int)ResponseEnum.OK)
                                    {
                                        ShowReport = true;
                                        RealTime();
                                        _MainContentControl.Content = new HomeUserControl();
                                    }
                                }
                            } //ĐÃ MỞ CA  -- 2
                            else
                            {
                                ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                                endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                                endWorkingSessionWindow.ShowDialog();
                                var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                if (end.isSuccess)
                                {
                                    Process.Start(Application.ResourceAssembly.Location);
                                    Application.Current.Shutdown();
                                    //#region Đạt
                                    //Environment.Exit(0);
                                    //#endregion
                                }
                                // DownloadIcon();
                            } // VÀ LẠI CA CHÍNH -- 4
                        }
                    }
                    else
                    {
                        if (!confirm.isNoConfirm)
                        {
                            foreach (Process Proc in Process.GetProcesses())
                            {
                                if (Proc.ProcessName.Equals(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)) Proc.Kill();
                            }
                            System.Windows.Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_END;
                    string Title = MessageValue.MESSAGE_NOTIFICATION;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        foreach (Process Proc in Process.GetProcesses())
                        {
                            if (Proc.ProcessName.Equals(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)) Proc.Kill();
                        }
                        System.Windows.Application.Current.Shutdown();
                    }
                }
            });
            ManageHistoryFoodCommand = new RelayCommand<MainWindow>((p) => { return true; }, p => {
                if(currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM)
                {
                    p.ContentCt.Focus();
                    _MainContentControl = p.FindName("ContentCt") as ContentControl;
                    HistoryPrinManagerWindow create = new HistoryPrinManagerWindow();
                    create.DataContext = new HistoryPrintManagerViewModel();
                    create.ShowDialog();
                    //_MainContentControl.Content = create;
                }
            });
            ChangePasswordCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                ChangePasswordPopup change = new ChangePasswordPopup();
                change.DataContext = new ChangePasswordViewModel();
                change.ShowDialog();
                var changePW = change.DataContext as ChangePasswordViewModel;
                if (changePW.IsChange)
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                    //t.Close();
                    //LoginWindow loginWindow = new LoginWindow();
                    //loginWindow.DataContext = new LoginViewModel();
                    //loginWindow.ShowDialog();
                }
                //TablesClient client = new TablesClient(this, this, this);

                //for (int i =1; i<=45; i++)
                //{
                //    CreateTableWrapper createTableWrapper = new CreateTableWrapper("D"+i, 323, 10, 1, 14);
                //    ManageFloatResponse table = client.CreateTable(createTableWrapper);
                //}

            });
            GoHomeCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                AddOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                AddOrderTakeAwayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                AddOrderForeground = new SolidColorBrush(Colors.White);
                AddOrderTakeAwayForeground = new SolidColorBrush(Colors.White);
                DialogHostOpen = true;
                GoHome(t);
                DialogHostOpen = false;
                MainSecondViewModel.ShowMainSecondBanner();
            });
            InputMoneyToCardCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                HistoryInputMoneyCustomerUserControl historyInputMoneyCustomerUserControl = new HistoryInputMoneyCustomerUserControl();
                historyInputMoneyCustomerUserControl.DataContext = new HistoryInputMoneyCustomerViewModel();
                _MainContentControl.Content = historyInputMoneyCustomerUserControl;

                MainSecondViewModel.ShowMainSecondBanner();
            });
            ReportFinishWorkingSessionCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    #region Tạm đóng
                    //RevenueFinishWorkingSessionUserControl control = new RevenueFinishWorkingSessionUserControl();
                    //control.DataContext = new RevenueFinishWorkingSessionViewModel();
                    //if (t != null)
                    //{
                    //    _MainContentControl = t.FindName("ContentCt") as ContentControl;
                    //    if (_MainContentControl != null)
                    //    {
                    //        _MainContentControl.Content = control;
                    //    }
                    //}
                    #endregion
                    WorkingSessionClient workingSessionClient = new WorkingSessionClient(this, this, this);
                    OrderSession checkSession = workingSessionClient.GetCheckSessions();
                    if (checkSession != null && checkSession.Status == (int)ResponseEnum.OK)
                    {
                        if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_EXPIRED)
                        {
                            Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();

                        } // // CA ĐÃ HẾT HẠN  -- 3
                        else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_OPENER)
                        {
                            ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                            endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                            endWorkingSessionWindow.ShowDialog();
                            var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                            if (end.isSuccess)
                            {
                                OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(0);
                                openWorkingSessionWindow.ShowDialog();
                                var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                if (open.isSuccess)
                                {
                                    ShowReport = true;
                                    RealTime();
                                    _MainContentControl.Content = new HomeUserControl();
                                    //DownloadIcon();
                                }
                            }
                        } // VÀ LẠI CA CHÍNH -- 4
                    }
                        //
                        MainSecondViewModel.ShowMainSecondBanner();
                }
            });
            SyncDataCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_SYNC_DATA_BOOKING;
                    string Title = MessageValue.MESSAGE_SYNC_DATA_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        DialogHostOpen = true;
                        SyncClient syncClient = new SyncClient(this, this, this);
                        BaseResponse baseResponse = syncClient.GetSyncData();
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {

                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);

                        }
                        DialogHostOpen = false;
                    }
                }
            });

            ReportRevenueCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ReportRevenueUserControl tmp = new ReportRevenueUserControl();
                tmp.DataContext = new ReportRevenueViewModel();
                _MainContentControl.Content = tmp;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            ReportCookCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ReportFoodUserControl tmp = new ReportFoodUserControl();
                tmp.DataContext = new ReportFoodViewModel();
                _MainContentControl.Content = tmp;
                MainSecondViewModel.ShowMainSecondBanner();
            });

            ReportGiftFoodCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ReportGiftFoodUC tmp = new ReportGiftFoodUC();
                tmp.DataContext = new ReportGiftFoodViewModel();
                _MainContentControl.Content = tmp;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });





            BtnAddOrderCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || currentSetting.BranchType == (int)BranchTypeEnum.SMALL)
                {
                    AddOrderBackground = new SolidColorBrush(Colors.White);
                    AddOrderTakeAwayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    AddOrderForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    AddOrderTakeAwayForeground = new SolidColorBrush(Colors.White);
                    p.ContentCt.Focus();
                    _MainContentControl = p.FindName("ContentCt") as ContentControl;
                    CreateOrderUserControl create = new CreateOrderUserControl();
                    create.DataContext = new CreateOrderViewModel();
                    _MainContentControl.Content = create;
                }
                else
                {
                    CallLog();
                    return;
                }
                MainSecondViewModel.ShowCreateOrderSecond();
            });
            BtnAddOrderTakeAwayCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || currentSetting.BranchType == (int)BranchTypeEnum.SMALL)
                {
                    AddOrderTakeAwayBackground = new SolidColorBrush(Colors.White);
                    AddOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    AddOrderTakeAwayForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    AddOrderForeground = new SolidColorBrush(Colors.White);
                    p.ContentCt.Focus();
                    _MainContentControl = p.FindName("ContentCt") as ContentControl;
                    CreateOrderTakeAwayUserControl create = new CreateOrderTakeAwayUserControl();
                    create.DataContext = new CreateOrderTakeAwayViewModel();
                    _MainContentControl.Content = create;
                }
                else
                {
                    CallLog();
                    return;
                }
                MainSecondViewModel.ShowCreateOrderSecond();
            });
            LogoutCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_END_WORKING_SESSION;
                    string Title = MessageValue.MESSAGE_END_WORKING_SESSION_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        WorkingSessionClient workingSessionClient = new WorkingSessionClient(this, this, this);
                        OrderSession checkSession = workingSessionClient.GetCheckSessions();
                        if (checkSession != null && checkSession.Status == (int)ResponseEnum.OK)
                        {
                            if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_EXPIRED)
                            {
                                Process.Start(Application.ResourceAssembly.Location);
                                Application.Current.Shutdown();

                            }
                            else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_OPENER)
                            {
                                ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                                endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                                endWorkingSessionWindow.ShowDialog();
                                var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                if (end.isSuccess)
                                {
                                    Process.Start(Application.ResourceAssembly.Location);
                                    Application.Current.Shutdown();
                                    //#region Đạt
                                    //Environment.Exit(0);
                                    //#endregion
                                }
                            }

                        }
                    }
                    else
                    {
                        if (!confirm.isNoConfirm)
                        {
                            Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                            //#region Đạt
                            //Environment.Exit(0);
                            //#endregion
                        }
                    }
                }
                else
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_LOG_OUT;
                    string Title = MessageValue.MESSAGE_NOTIFICATION;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        //LoginWindow window = new LoginWindow();
                        //window.Show();
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();

                    }

                }
            });
            SettingCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                p.ContentCt.Focus();
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                SettingUserControl settingUC = new SettingUserControl();
                settingUC.DataContext = new SettingViewModel();
                _MainContentControl.Content = settingUC;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            HistoryLogCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                HistoryLogUserControl tmp = new HistoryLogUserControl();
                ActivityLogViewModel ac = new ActivityLogViewModel();
                tmp.DataContext = ac;
                _MainContentControl.Content = tmp;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });


            EmployeeAdvancedSalaryLevelCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                EmployeeAdvancedSalaryUserControl uc = new EmployeeAdvancedSalaryUserControl();
                uc.DataContext = new EmployeeAdcancedSalaryViewModel();
                _MainContentControl.Content = uc;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            BookingCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ListBookingUC listBookingUC = new ListBookingUC();
                listBookingUC.DataContext = new BookingViewModel();
                _MainContentControl.Content = listBookingUC;
            });

            OrderingListCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;

                OrderListUserControlDetailAdmin orderListUserControlDetailAdmin = new OrderListUserControlDetailAdmin();
                orderListUserControlDetailAdmin.DataContext = new OrderListDetailAdminViewModel();
                _MainContentControl.Content = orderListUserControlDetailAdmin;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            HistoryEndWorkingSessionCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                HistoryEndWorkingSessionUserControl workingSessionUserControl = new HistoryEndWorkingSessionUserControl();
                workingSessionUserControl.DataContext = new HistoryEndWorkingSessionViewModel();
                _MainContentControl.Content = workingSessionUserControl;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });
            ViewAllCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                NotificationUserControl workingSessionUserControl = new NotificationUserControl();
                workingSessionUserControl.DataContext = new NotificationViewModel(NotificationEmployeeType);
                _MainContentControl.Content = workingSessionUserControl;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            NoteListCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                NoteListUC tmp = new NoteListUC();
                tmp.DataContext = new NoteListViewModel();
                _MainContentControl.Content = tmp;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            HistoryDebitCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                HistoryDebitUserControl control = new HistoryDebitUserControl();
                control.DataContext = new HistoryDebitViewModel();
                _MainContentControl.Content = control;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            ListCustomerDebitCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ListDebtCustomerUserControl control = new ListDebtCustomerUserControl();
                control.DataContext = new ListDebtCustomerViewModel();
                _MainContentControl.Content = control;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            ReportEmployeeCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                p.ContentCt.Focus();
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ReportEmployeeUserControl control = new ReportEmployeeUserControl();
                control.DataContext = new ReportEmployeeViewModel();
                _MainContentControl.Content = control;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            QRCodeBranchCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            {
                p.ContentCt.Focus();
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                QRCodeBranchCheckInUserControl qR = new QRCodeBranchCheckInUserControl();
                qR.DataContext = new QRCodeBranchCheckInViewModel();
                _MainContentControl.Content = qR;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });
            //ReportCustomerSlotCommand = new RelayCommand<MainWindow>((p) => { return true; }, p =>
            //{
            //    p.ContentCt.Focus();
            //    _MainContentControl = p.FindName("ContentCt") as ContentControl;
            //    ReportCustomerSlotUserControl tmp = new ReportCustomerSlotUserControl();
            //    tmp.DataContext = new ReportCustomerSlotViewmodel();
            //    _MainContentControl.Content = tmp;
            //    //
            //    MainSecondViewModel.ShowMainSecondBanner();
            //});


            NotificationCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (NotificationIsOpen || NotificationList.Count > 0)
                {
                    NotificationIsOpen = false;
                    NotificationList.Clear();
                }
                else
                {
                    GetNotificationList();
                    NotificationIsOpen = true;
                }
            });
            BatenderCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                BatenderUC control = new BatenderUC();
                _MainContentControl.Content = control;
                MainSecondViewModel.ShowMainSecondBanner();
            });

            MoveListFoodCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                MoveListFoodsUserControl uc = new MoveListFoodsUserControl();
                uc.DataContext = new MoveListFoodsViewModel();
                _MainContentControl.Content = uc;
            });
            ExpenseCashierCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                ExpenseCashierUserControl uc = new ExpenseCashierUserControl();
                uc.DataContext = new ExpenseCashierViewModel();
                _MainContentControl.Content = uc;
                //
                MainSecondViewModel.ShowMainSecondBanner();
            });

            QRCodeRegistrationCustomerCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                QRCodeRegistrationCustomerUserControl uc = new QRCodeRegistrationCustomerUserControl();
                uc.DataContext = new QRCodeRegistrationCustomerViewModel();
                _MainContentControl.Content = uc;
            });
            HelpCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                string target = "http://help.techres.vn";
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
            });

            ProfileCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                DetailEmployeeWindow popup = new DetailEmployeeWindow();
                popup.DataContext = new DetailEmployeeViewModel(currentUser.Id);
                popup.ShowDialog();
            });

            SupportCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

            });
        }
        public async void DownloadImageLogo(string imagelogo)
        {
            DownloadManager downloadManager = new DownloadManager();
            await Task.Run(() => downloadManager.DownloadFile(imagelogo, Environment.CurrentDirectory + @"\logo_branch.png"));
        }
        //public void DownloadIcon()
        //{
        //    if (currentSetting.IsPrintBillLogo)
        //    {
        //        if (!string.IsNullOrEmpty(currentBranch.ImageLogo))
        //        {
        //            if (File.Exists(Environment.CurrentDirectory + @"\logo_branch.png"))
        //            {
        //                File.Delete(Environment.CurrentDirectory + @"\logo_branch.png");
        //            }
        //            DownloadImageLogo(string.Format("{0}{1}", Constants.ADS_DOMAIN, currentBranch.ImageLogo));
        //        }
        //    }
        //}
        public void GoHome(Window p)
        {
            if (currentUser != null)
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                //SetVisibilityHeaderAndFooter();
                if (_MainContentControl != null)
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        _MainContentControl.Content = new HomeUserControl();
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                    {
                        CookUserControll cookUser = new CookUserControll();
                        _MainContentControl.Content = cookUser;
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                    {
                        BatenderUC control = new BatenderUC();
                        _MainContentControl.Content = control;
                    }
                }
            }
        }

        public async void GetAllFoodList()
        {
            FoodsClient client = new FoodsClient(this, this, this);
            FoodResponses response = await Task.Run(() => client.GetFoodsUSing(Constants.ALL, currentUser.BranchId, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, 1));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach (Food food in response.Data)
                {
                    AllFood.Add(AddFoodMenu(food));
                }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, AllFood, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                //     MainSecondViewModel.FoodList = AllFood;
                Constants.IS_FIRST_FOOD = false;
                ////
                //   MainSecondViewModel.ShowMainSecondBanner();
            }
        }
        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
        public FoodMenuItem AddFoodMenu(Food f)
        {
            string priceFood = Utils.Utils.FormatShortCurrency(f);
            //if (f.Price % 1000 == 0)
            //{
            //    priceFood = f.Price / 1000 + "K";
            //}
            //else
            //{
            //    if (f.Price > 1000)
            //    {
            //        priceFood = f.Price / 1000 + "K" + (f.Price / 1000) / 100;
            //    }
            //    else
            //    {
            //        priceFood = f.Price.ToString();
            //    }
            //}
            //try
            //{
            //if(f.AvatarFood == "/public/resource/avatar_default/default.jpg")
            //{
            //    return new FoodMenuItem(f.Id, new BitmapImage(new Uri(@"C:/Users/DELL/Desktop/code51/TechresStandaloneSale/Resources/Images/fast-food.png")), priceFood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code, f.FoodInCombo, f.IsSellByWeight, f.FoodInPromotion);
            //}
            //else
            //{
            BitmapImage bitmap = new BitmapImage();
            if (RemoteFileExists(f.Avatar) == false)
            {
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/food-default.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                bitmap = new BitmapImage(new Uri(f.Avatar, UriKind.RelativeOrAbsolute));
            }
                return new FoodMenuItem(f.Id, bitmap, priceFood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code, f.FoodInCombo, f.IsSellByWeight, f.FoodInPromotion,f.IsAllowEmployeeGift);
            //}
            //}
            //catch (Exception ex)
            //{
            //    WriteLog.logs(ex.ToString());
            //    f.Avatar = "logo.ico";
            //    return new FoodMenuItem(f.Id, new BitmapImage(new Uri(f.Avatar, UriKind.RelativeOrAbsolute)), priceFood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code);
            //}
        }
        public void initUserControll(Window p)
        {

            if (currentUser != null)
            {
                _MainContentControl = p.FindName("ContentCt") as ContentControl;
                if (_MainContentControl != null)
                {
                    // After login check permission is Cashier
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        WorkingSessionClient workingSessionClient = new WorkingSessionClient(this, this, this);
                        OrderSession checkSession = workingSessionClient.GetCheckSessions();
                        if (checkSession != null && checkSession.Status == (int)ResponseEnum.OK)
                        {
                            if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_EXPIRED)
                            {
                                ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport();
                                endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(true);
                                endWorkingSessionWindow.ShowDialog();
                                var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                if (end.isSuccess)
                                {
                                    ShowReport = true;
                                    RealTime();
                                    _MainContentControl.Content = new HomeUserControl();
                                    //DownloadIcon();
                                }
                            } // CA ĐÃ HẾT HẠN  -- 3
                            else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_NOT_OPEN)
                            {
                                OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(0);
                                openWorkingSessionWindow.ShowDialog();
                                var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                if (open.isSuccess)
                                {
                                    ShowReport = true;
                                    RealTime();
                                    _MainContentControl.Content = new HomeUserControl();
                                    //DownloadIcon();
                                }
                            } // // CHƯA MỞ CA -- 1
                            else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_OPENER)
                            {
                                ShowReport = true;
                                RealTime();
                                _MainContentControl.Content = new HomeUserControl();
                                // DownloadIcon();
                            }  // VÀ LẠI CA CHÍNH -- 4
                            else if (checkSession.Data.Type == (int)OrderSessionStatusEnum.SESSION_OPENED)
                            {
                                ConfirmDeleteWindow confirmWindow = new ConfirmDeleteWindow();
                                string content = MessageValue.MESSAGE_SESSION_OPENED;
                                string title = MessageValue.MESSAGE_NOTIFICATION;
                                string yesContent = MessageValue.MESSAGE_YES_CONTENT;
                                string noContent = MessageValue.MESSAGE_NO_CONTENT;
                                confirmWindow.DataContext = new ConfirmViewModel(content, title, noContent, yesContent);
                                confirmWindow.ShowDialog();
                                var yesConfirm = confirmWindow.DataContext as ConfirmViewModel;
                                if (yesConfirm.isConfirm)
                                {
                                    ShiftActivityReport endWorkingSessionWindow = new ShiftActivityReport(); // CHANGE
                                    endWorkingSessionWindow.DataContext = new EndWorkingSessionViewModel(false);
                                    endWorkingSessionWindow.ShowDialog();
                                    var end = endWorkingSessionWindow.DataContext as EndWorkingSessionViewModel;
                                    if (end.isSuccess)
                                    {
                                        OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                        openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(0);
                                        openWorkingSessionWindow.ShowDialog();
                                        var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                        if (open.isSuccess)
                                        {
                                            ShowReport = true;
                                            RealTime();
                                            _MainContentControl.Content = new HomeUserControl();
                                            //DownloadIcon();
                                        }
                                    }
                                }
                                else
                                {
                                    WorkingSessionClient client = new WorkingSessionClient(this, this, this);
                                    BaseResponse response = client.CreateeteOrderSession(checkSession.Data.OrderSessionId, currentUser.BranchId);
                                    if (response.Status == (int)ResponseEnum.OK)
                                    {
                                        ShowReport = true;
                                        RealTime();
                                        _MainContentControl.Content = new HomeUserControl();
                                    }
                                }
                            }
                        }
                    }
                    // After login check permission is bếp nấu or bếp nướng
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                    {

                        ChooseKitchenWindow chooseKitchenWindow = new ChooseKitchenWindow();
                        chooseKitchenWindow.DataContext = new ChooseKitchenViewModel();
                        chooseKitchenWindow.ShowDialog();
                        var chooseKitchen = chooseKitchenWindow.DataContext as ChooseKitchenViewModel;
                        if (chooseKitchen != null && chooseKitchen.IsConfrim)
                        {
                            Kitchen kitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
                            currentKitchen = kitchen;
                            RealTime();
                            ContentRestaurant = string.Concat(ContentRestaurant, "--", kitchen.Name);
                            CookUserControll cookUser = new CookUserControll();
                            _MainContentControl.Content = cookUser;
                        }
                    }
                    // After login check permission is Kho bia / Bar
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                    {
                        // Properties.Settings.Default.CurrentKitchen = 0;
                        ChooseKitchenWindow chooseKitchenWindow = new ChooseKitchenWindow();
                        chooseKitchenWindow.DataContext = new ChooseKitchenViewModel();
                        chooseKitchenWindow.ShowDialog();
                        var chooseKitchen = chooseKitchenWindow.DataContext as ChooseKitchenViewModel;
                        if (chooseKitchen != null && chooseKitchen.IsConfrim)
                        {
                            RealTime();
                            BatenderUC control = new BatenderUC();
                            _MainContentControl.Content = control;
                        }
                    }
                    else
                    {
                        CallLog();
                    }
                }
            }
        }
        public void initVisibilityLocal()
        {
            // Restaurant Level 1
            if (currentSetting.BranchType == (int)BranchTypeEnum.SMALL)
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    ExpenseVisibility = Visibility.Visible;
                    SettingVisibility = Visibility.Visible;
                    AddOrderVisibility = Visibility.Visible;
                    AddOrderTakeAwayVisibility = Visibility.Visible;
                    BookingVisibility = Visibility.Visible;
                    MoveFoodKitchenVisibility = Visibility.Visible;
                }
                else
                {

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK) }, currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT) }, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;

                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions))
                    {
                        ExpenseCashierVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                    {
                        AddOrderVisibility = Visibility.Visible;
                        AddOrderTakeAwayVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ACTION_ON_FOOD_AND_TABLE), currentUser.Permissions))
                    {
                        MoveFoodKitchenVisibility = Visibility.Visible;
                    }
                }
            }
            // Restaurant Level 2
            else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM)
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    if (currentRestaurant != null && currentRestaurant.IsParty == Constants.STATUS)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    }
                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    ReportGiftFoodVisibility = Visibility.Visible;

                    BookingVisibility = Visibility.Visible;

                    OrderVisibility = Visibility.Visible;
                    HistoryEndWorkingSessionVisibility = Visibility.Visible;

                    ExpenseVisibility = Visibility.Visible;

                    SettingVisibility = Visibility.Visible;
                    if(currentSetting.BranchTypeOption == (int)BranchTypeOption.OPTIONTWO)
                    {
                        AddOrderVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        AddOrderVisibility = Visibility.Visible;
                    }

                    AddOrderTakeAwayVisibility = Visibility.Visible;
                    QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    BookingVisibility = Visibility.Visible;
                }
                else
                {
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                    {
                        AddOrderVisibility = Visibility.Visible;
                        AddOrderTakeAwayVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK) }, currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT) }, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;

                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        OrderVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_SESSION_MANAGER), currentUser.Permissions))
                    {
                        HistoryEndWorkingSessionVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions) && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ExpenseCashierVisibility = Visibility.Visible;
                    }
                }
            }
            // Restaurant Level 3
            else // BranchTypeEnum.LARGE  if (currentSetting.BranchType == (int)BranchTypeEnum.LARGE)
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    if (currentSetting.IsEnableTms)
                    {
                        if (currentSetting.IsEnableCheckin)
                        {
                            QrCodeBranchVisibility = Visibility.Visible;
                        }

                    }
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                        InputMoneyToCardVisibility = Visibility.Collapsed;
                    }
                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    ReportGiftFoodVisibility = Visibility.Visible;
                    BookingVisibility = Visibility.Visible;
                    OrderVisibility = Visibility.Visible;
                    HistoryEndWorkingSessionVisibility = Visibility.Visible;

                    ExpenseCashierVisibility = Visibility.Collapsed;
                    ExpenseVisibility = Visibility.Visible;
                    ReportHistoryLogVisibility = Visibility.Visible;
                    SettingVisibility = Visibility.Visible;
                    EmployeeAdvancedSalaryVisibility = Visibility.Visible;
                    BookingVisibility = Visibility.Visible;
                    MoveFoodKitchenVisibility = Visibility.Visible;
                }
                else
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.TOPUP_CUSTOMER_CARD), currentUser.Permissions))
                    {
                        InputMoneyToCardVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), currentUser.Permissions)
                        || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK), currentUser.Permissions)
                        || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT) }, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;

                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        OrderVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_SESSION_MANAGER), currentUser.Permissions))
                    {
                        HistoryEndWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions) && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ExpenseCashierVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.PAID_EMPLOYEE_ADVANCE_SALARY), currentUser.Permissions))
                    {
                        EmployeeAdvancedSalaryVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ACTION_ON_FOOD_AND_TABLE), currentUser.Permissions))
                    {
                        MoveFoodKitchenVisibility = Visibility.Visible;
                    }
                }


            }//BranchTypeEnum.LARGE
        }
        public void initVisibility()
        {
            WorkingSessionVisibility = Visibility.Collapsed;
            InputMoneyToCardVisibility = Visibility.Collapsed;
            EmployeeVisibility = Visibility.Collapsed;

            ReportRevenueWorkingSessionVisibility = Visibility.Collapsed;
            ReportFoodVisibility = Visibility.Collapsed;
            ReportRevenueVisibility = Visibility.Collapsed;
            HistoryDebitVisibility = Visibility.Collapsed;
            ReportGiftFoodVisibility = Visibility.Collapsed;
            ReportCustomerSlotVisibility = Visibility.Collapsed;
            //ReportEmployeeVisibility = Visibility.Collapsed;

            NoteFoodVisibility = Visibility.Collapsed;
            RejectReasonVisibility = Visibility.Collapsed;
            CategoryVisibility = Visibility.Collapsed;
            FoodListVisibility = Visibility.Collapsed;
            UnitFoodVisibility = Visibility.Collapsed;
            EditPriceVisibility = Visibility.Collapsed;
            FoodTakeAwayVisibility = Visibility.Collapsed;

            MoveFoodKitchenVisibility = Visibility.Collapsed;

            BookingVisibility = Visibility.Collapsed;

            OrderVisibility = Visibility.Collapsed;
            EmployeeAdvancedSalaryVisibility = Visibility.Collapsed;
            HistoryEndWorkingSessionVisibility = Visibility.Collapsed;
            QrCodeBranchVisibility = Visibility.Collapsed;
            ExpenseCashierVisibility = Visibility.Collapsed;

            ExpenseVisibility = Visibility.Collapsed;
            ReportBalanceMoneyVisibility = Visibility.Collapsed;
            PaymentReasonListVisibility = Visibility.Collapsed;
            ReportHistoryLogVisibility = Visibility.Visible;

            AreaVisibility = Visibility.Collapsed;
            TableVisibility = Visibility.Collapsed;
            SettingVisibility = Visibility.Visible;

            BatenderVisibility = Visibility.Collapsed;
            AddOrderVisibility = Visibility.Collapsed;
            AddOrderTakeAwayVisibility = Visibility.Collapsed;
            QRCodeRegistrationCustomerVisibility = Visibility.Collapsed;
            FoodListBranchVisibility = Visibility.Collapsed;


        }
        public void initVisibilityPermission()
        {
            // currentRestaurant.IsParty = 1;
            if (currentSetting.BranchType == (int)BranchTypeEnum.SMALL)
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    //ReportEmployeeVisibility = Visibility.Visible;

                    NoteFoodVisibility = Visibility.Visible;
                    RejectReasonVisibility = Visibility.Visible;
                    CategoryVisibility = Visibility.Visible;
                    FoodListVisibility = Visibility.Visible;
                    UnitFoodVisibility = Visibility.Visible;
                    EditPriceVisibility = Visibility.Visible;
                    FoodTakeAwayVisibility = Visibility.Visible;
                    MoveFoodKitchenVisibility = Visibility.Visible;
                    WorkingSessionVisibility = Visibility.Visible;

                    ExpenseVisibility = Visibility.Visible;
                    ReportBalanceMoneyVisibility = Visibility.Visible;
                    PaymentReasonListVisibility = Visibility.Visible;

                    AreaVisibility = Visibility.Visible;
                    TableVisibility = Visibility.Visible;
                    SettingVisibility = Visibility.Visible;

                    AddOrderVisibility = Visibility.Visible;
                    AddOrderTakeAwayVisibility = Visibility.Visible;

                    MoveFoodKitchenVisibility = Visibility.Visible;
                    HistoryDebitVisibility = Visibility.Visible;
                }
                else
                {
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK) }, currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }
                    //if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REPORT), currentUser.Permissions))
                    //{
                    //    ReportEmployeeVisibility = Visibility.Visible;
                    //}
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_CUSTOMER_SLOT), currentUser.Permissions))
                    {
                        ReportCustomerSlotVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_REVENUE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REVENUE_REPORT) }, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_BRAND_FOOD_MANAGER), currentUser.Permissions))
                    {
                        CategoryVisibility = Visibility.Visible;
                        FoodListVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        UnitFoodVisibility = Visibility.Visible;
                        RejectReasonVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                        FoodTakeAwayVisibility = Visibility.Visible;
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.FOOD_MANAGER), currentUser.Permissions))
                    {
                        FoodListBranchVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions))
                    {
                        PaymentReasonListVisibility = Visibility.Visible;
                        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                        {
                            ExpenseCashierVisibility = Visibility.Visible;

                        }
                        else
                        {
                            ExpenseVisibility = Visibility.Visible;
                            ReportBalanceMoneyVisibility = Visibility.Visible;
                        }
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_TABLE_MANAGER), currentUser.Permissions))
                    {
                        AreaVisibility = Visibility.Visible;
                        TableVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                    {
                        AddOrderVisibility = Visibility.Visible;
                        AddOrderTakeAwayVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ACTION_ON_FOOD_AND_TABLE), currentUser.Permissions))
                    {
                        MoveFoodKitchenVisibility = Visibility.Visible;
                    }
                }
            }
            else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM)
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    if (currentRestaurant != null && currentRestaurant.IsParty == Constants.STATUS)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                        InputMoneyToCardVisibility = Visibility.Visible;
                    }
                    WorkingSessionVisibility = Visibility.Visible;
                    EmployeeVisibility = Visibility.Visible;

                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    HistoryDebitVisibility = Visibility.Visible;
                    ReportGiftFoodVisibility = Visibility.Visible;
                    ReportCustomerSlotVisibility = Visibility.Visible;
                    //ReportEmployeeVisibility = Visibility.Visible;


                    NoteFoodVisibility = Visibility.Visible;
                    RejectReasonVisibility = Visibility.Visible;
                    CategoryVisibility = Visibility.Visible;
                    FoodListVisibility = Visibility.Visible;
                    UnitFoodVisibility = Visibility.Visible;
                    EditPriceVisibility = Visibility.Visible;
                    FoodTakeAwayVisibility = Visibility.Visible;

                    BookingVisibility = Visibility.Visible;

                    OrderVisibility = Visibility.Visible;
                    HistoryEndWorkingSessionVisibility = Visibility.Visible;
                    ExpenseCashierVisibility = Visibility.Collapsed;

                    ExpenseVisibility = Visibility.Visible;
                    ReportBalanceMoneyVisibility = Visibility.Visible;
                    PaymentReasonListVisibility = Visibility.Visible;

                    AreaVisibility = Visibility.Visible;
                    TableVisibility = Visibility.Visible;
                    SettingVisibility = Visibility.Visible;

                    BatenderVisibility = Visibility.Collapsed;
                    //AddOrderVisibility = Visibility.Visible;
                    if (currentSetting.BranchTypeOption == (int)BranchTypeOption.OPTIONTWO)
                    {
                        AddOrderVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        AddOrderVisibility = Visibility.Visible;
                    }
                    AddOrderTakeAwayVisibility = Visibility.Visible;
                    QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    MoveFoodKitchenVisibility = Visibility.Visible;
                }
                else
                {
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.TOPUP_CUSTOMER_CARD), currentUser.Permissions))
                    {
                        InputMoneyToCardVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                    {
                        //AddOrderVisibility = Visibility.Visible;
                        if (currentSetting.BranchTypeOption == (int)BranchTypeOption.OPTIONTWO)
                        {
                            AddOrderVisibility = Visibility.Collapsed;
                        }
                        else
                        {
                            AddOrderVisibility = Visibility.Visible;
                        }
                        AddOrderTakeAwayVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.WORKING_SESSION_MANAGER), currentUser.Permissions))
                    {
                        WorkingSessionVisibility = Visibility.Visible;
                    }


                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_MANAGER), currentUser.Permissions))
                    {
                        EmployeeVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.SALARY_MANAGER), currentUser.Permissions))
                    {
                        HistoryDebitVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK) }, currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }
                    //if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REPORT), currentUser.Permissions))
                    //{
                    //    ReportEmployeeVisibility = Visibility.Visible;
                    //}
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_CUSTOMER_SLOT), currentUser.Permissions))
                    {
                        ReportCustomerSlotVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_REVENUE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REVENUE_REPORT) }, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_BRAND_FOOD_MANAGER), currentUser.Permissions))
                    {
                        CategoryVisibility = Visibility.Visible;
                        FoodListVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        UnitFoodVisibility = Visibility.Visible;
                        RejectReasonVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                        FoodTakeAwayVisibility = Visibility.Visible;
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.FOOD_MANAGER), currentUser.Permissions))
                    {
                        FoodListBranchVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        OrderVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_SESSION_MANAGER), currentUser.Permissions))
                    {
                        HistoryEndWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions) && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ExpenseCashierVisibility = Visibility.Visible;
                        PaymentReasonListVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions))
                    {
                        if (!Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                        {
                            ExpenseVisibility = Visibility.Visible;
                            ReportBalanceMoneyVisibility = Visibility.Visible;
                            PaymentReasonListVisibility = Visibility.Visible;
                        }
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_TABLE_MANAGER), currentUser.Permissions))
                    {
                        AreaVisibility = Visibility.Visible;
                        TableVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ACTION_ON_FOOD_AND_TABLE), currentUser.Permissions))
                    {
                        MoveFoodKitchenVisibility = Visibility.Visible;
                    }
                }
            }
            // Account Login Level 3
            else if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE  )
            {
                // Check if is Cashier login app
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    if (currentSetting.IsEnableTms)
                    {
                        if (currentSetting.IsEnableCheckin)
                        {
                            QrCodeBranchVisibility = Visibility.Visible;
                        }
                    }
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                        //InputMoneyToCardVisibility = Visibility.Visible;
                    }
                    WorkingSessionVisibility = Visibility.Visible;

                    EmployeeVisibility = Visibility.Visible;

                    ReportFoodVisibility = Visibility.Visible;
                    ReportRevenueVisibility = Visibility.Visible;
                    HistoryDebitVisibility = Visibility.Visible;
                    ReportGiftFoodVisibility = Visibility.Visible;
                    ReportCustomerSlotVisibility = Visibility.Visible;
                    //ReportEmployeeVisibility = Visibility.Visible;

                    ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    NoteFoodVisibility = Visibility.Visible;
                    RejectReasonVisibility = Visibility.Visible;
                    CategoryVisibility = Visibility.Visible;
                    FoodListVisibility = Visibility.Visible;
                    UnitFoodVisibility = Visibility.Visible;
                    EditPriceVisibility = Visibility.Visible;
                    FoodTakeAwayVisibility = Visibility.Visible;

                    BookingVisibility = Visibility.Visible;

                    OrderVisibility = Visibility.Visible;
                    EmployeeAdvancedSalaryVisibility = Visibility.Visible;
                    HistoryEndWorkingSessionVisibility = Visibility.Visible;

                    ExpenseCashierVisibility = Visibility.Visible;

                    ExpenseVisibility = Visibility.Visible;
                    ReportBalanceMoneyVisibility = Visibility.Visible;
                    PaymentReasonListVisibility = Visibility.Visible;

                    ReportHistoryLogVisibility = Visibility.Visible;

                    AreaVisibility = Visibility.Visible;
                    TableVisibility = Visibility.Visible;
                    SettingVisibility = Visibility.Visible;
                    MoveFoodKitchenVisibility = Visibility.Visible;
                    ManageHistoryFood = Visibility.Collapsed;
                }
                else
                {
                    if (currentSetting.IsEnableMembershipCard)
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.TOPUP_CUSTOMER_CARD), currentUser.Permissions))
                    {
                        //InputMoneyToCardVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.WORKING_SESSION_MANAGER), currentUser.Permissions))
                    {
                        WorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Collapsed;//dat
                        ManageHistoryFood = Visibility.Collapsed;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                    {
                        QRCodeRegistrationCustomerVisibility = Visibility.Collapsed;//dat
                        ManageHistoryFood = Visibility.Collapsed;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_MANAGER), currentUser.Permissions))
                    {
                        EmployeeVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.SALARY_MANAGER), currentUser.Permissions))
                    {
                        HistoryDebitVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK) }, currentUser.Permissions))
                    {
                        ReportFoodVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                    {
                        ReportGiftFoodVisibility = Visibility.Visible;
                    }
                    //if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    //{
                    //    ReportEmployeeVisibility = Visibility.Visible;
                    //}
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ReportRevenueWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_CUSTOMER_SLOT), currentUser.Permissions))
                    {
                        ReportCustomerSlotVisibility = Visibility.Visible;
                    }

                    // list permission can access function revenue report on menu
                    List<string> permission_can_access_function_report_revenue = new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_REVENUE_REPORT),
                        Enum.GetName(typeof(TechresEnum), TechresEnum.EMPLOYEE_REVENUE_REPORT) };

                    if (Utils.Utils.CheckListPermissionsEmployee(permission_can_access_function_report_revenue, currentUser.Permissions))
                    {
                        ReportRevenueVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_BRAND_FOOD_MANAGER), currentUser.Permissions))
                    {
                        CategoryVisibility = Visibility.Visible;
                        FoodListVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        UnitFoodVisibility = Visibility.Visible;
                        RejectReasonVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                        FoodTakeAwayVisibility = Visibility.Visible;
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.FOOD_MANAGER), currentUser.Permissions))
                    {
                        FoodListBranchVisibility = Visibility.Visible;
                        EditPriceVisibility = Visibility.Visible;
                        NoteFoodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                    {
                        BookingVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.PAID_EMPLOYEE_ADVANCE_SALARY), currentUser.Permissions))
                    {
                        EmployeeAdvancedSalaryVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        OrderVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_SESSION_MANAGER), currentUser.Permissions))
                    {
                        HistoryEndWorkingSessionVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions) && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        ExpenseCashierVisibility = Visibility.Visible;
                        PaymentReasonListVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_MANAGER), currentUser.Permissions))
                    {
                        if (!Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                        {
                            ExpenseVisibility = Visibility.Visible;
                            ReportBalanceMoneyVisibility = Visibility.Visible;
                            PaymentReasonListVisibility = Visibility.Visible;
                        }

                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.AREA_TABLE_MANAGER), currentUser.Permissions))
                    {
                        AreaVisibility = Visibility.Visible;
                        TableVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ACTION_ON_FOOD_AND_TABLE), currentUser.Permissions))
                    {
                        MoveFoodKitchenVisibility = Visibility.Visible;
                    }
                }
            }
        }
        public void CallLog()
        {
            NotificationMessage.Warning(MessageValue.FORBIDDEN);
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
        public void LogError(Exception ex, string infoMessage)
        {
            // throw new NotImplementedException();
        }
    }
}

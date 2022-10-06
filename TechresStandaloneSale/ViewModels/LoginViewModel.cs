using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Quobject.SocketIoClientDotNet.Client;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Template;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class LoginViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private readonly double MAX_EXPIRE_CACHE = 1;
        public List<FoodMenuItem> AllFood = new List<FoodMenuItem>();
        public bool IsLogin { get; set; }

        private ImageBrush _WorkingOnlineBackground;
        public ImageBrush WorkingOnlineBackground { get => _WorkingOnlineBackground; set { _WorkingOnlineBackground = value; OnPropertyChanged("WorkingOnlineBackground"); } }
        private Brush _WorkingOnlineForeground;
        public Brush WorkingOnlineForeground { get => _WorkingOnlineForeground; set { _WorkingOnlineForeground = value; OnPropertyChanged("WorkingOnlineForeground"); } }
        private ImageBrush _WorkingOfflineBackground;
        public ImageBrush WorkingOfflineBackground { get => _WorkingOfflineBackground; set { _WorkingOfflineBackground = value; OnPropertyChanged("WorkingOfflineBackground"); } }
        private Brush _WorkingOfflineForeground;
        public Brush WorkingOfflineForeground { get => _WorkingOfflineForeground; set { _WorkingOfflineForeground = value; OnPropertyChanged("WorkingOfflineForeground"); } }
        private BitmapImage _WorkingOnlineIcon;
        public BitmapImage WorkingOnlineIcon { get => _WorkingOnlineIcon; set { _WorkingOnlineIcon = value; OnPropertyChanged("WorkingOnlineIcon"); } }
        private BitmapImage _WorkingOfflineIcon;
        public BitmapImage WorkingOfflineIcon { get => _WorkingOfflineIcon; set { _WorkingOfflineIcon = value; OnPropertyChanged("WorkingOfflineIcon"); } }
        private string _RestaurantAddress;
        public string RestaurantAddress { get => _RestaurantAddress; set { _RestaurantAddress = value; OnPropertyChanged("RestaurantAddress"); } }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged("UserName"); } }
        private string _RestaurantName;
        public string RestaurantName { get => _RestaurantName; set { _RestaurantName = value; OnPropertyChanged("RestaurantName"); } }
        private string _VersionTitle;
        public string VersionTitle { get => _VersionTitle; set { _VersionTitle = value; OnPropertyChanged("VersionTitle"); } }

        public bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged("Password"); } }
        private bool _IsSavePassword;
        public bool IsSavePassword { get => _IsSavePassword; set { _IsSavePassword = value; OnPropertyChanged("IsSavePassword"); } }
        private Visibility _WorkingOnlineVisibility;
        public Visibility WorkingOnlineVisibility { get => _WorkingOnlineVisibility; set { _WorkingOnlineVisibility = value; OnPropertyChanged("WorkingOnlineVisibility"); } }
        private Visibility _WorkingOfflineVisibility;
        public Visibility WorkingOfflineVisibility { get => _WorkingOfflineVisibility; set { _WorkingOfflineVisibility = value; OnPropertyChanged("WorkingOfflineVisibility"); } }


        public bool IsClose = false;
        public ICommand LoginCommanded { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand EnterLoginCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ForgetPasswordCommand { get; set; }
        public ICommand ClosedAppCommand { get; set; }
        public ICommand WorkingOfflineCommand { get; set; }
        public ICommand WorkingOnlineCommand { get; set; }
        public ICommand SearchServerCommand { get; set; }

        public ICommand ForgotCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        public bool isNetWorkOnline = true;

        public void CheckRememberUser(LoginWindow p)
        {
            VersionTitle = string.Format("Phiên bản {0}", Properties.Settings.Default.VERSION);


            if (Properties.Settings.Default.IsOffline)
            {
                isNetWorkOnline = true;
                RestaurantName = Properties.Settings.Default.RestaurantDomainAPI;
                WorkingOfflineVisibility = Visibility.Collapsed;
                WorkingOnlineVisibility = Visibility.Visible;
                WorkingOfflineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-btn-offline-login.png")));
                WorkingOfflineForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                WorkingOfflineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-restaurant-active.png", UriKind.RelativeOrAbsolute));

                WorkingOnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg_btn_online_login.png")));
                WorkingOnlineForeground = new SolidColorBrush(Colors.White);
                WorkingOnlineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-online.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                isNetWorkOnline = false;
                RestaurantAddress = Properties.Settings.Default.RestaurantDomainAPI;
                WorkingOfflineVisibility = Visibility.Visible;
                WorkingOnlineVisibility = Visibility.Collapsed;
                WorkingOfflineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg_btn_online_login.png")));
                WorkingOfflineForeground = new SolidColorBrush(Colors.White);
                WorkingOfflineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-restaurant-white.png", UriKind.RelativeOrAbsolute));

                WorkingOnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-btn-offline-login.png")));
                WorkingOnlineForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                WorkingOnlineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-online-active.png", UriKind.RelativeOrAbsolute));
            }
            UserName = Properties.Settings.Default.UserNameMind;

            if (Properties.Settings.Default.Mind)
            {
                Password = Properties.Settings.Default.PasswordMind;
                p.PasswordBox.Password = Password;
                IsSavePassword = true;
            }
            else
            {
                Password = string.Empty;
                p.PasswordBox.Password = string.Empty;
                IsSavePassword = false;
            }

        }

        public LoginViewModel()
        {
            try
            {
                //NotificationMessage.notification = new ToastViewModel();
                LoadedWindowCommand = new RelayCommand<LoginWindow>((p) => { return true; }, (p) =>
                {
                    CheckRememberUser(p);
                });
                PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
                {
                    _Password = p.Password;
                });
                WorkingOnlineCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    isNetWorkOnline = true;
                    WorkingOnlineVisibility = Visibility.Visible;
                    WorkingOfflineVisibility = Visibility.Collapsed;
                    WorkingOfflineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-btn-offline-login.png")));
                    WorkingOfflineForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                    WorkingOfflineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-restaurant-active.png", UriKind.RelativeOrAbsolute));

                    WorkingOnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg_btn_online_login.png")));
                    WorkingOnlineForeground = new SolidColorBrush(Colors.White);
                    WorkingOnlineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-online.png", UriKind.RelativeOrAbsolute));
                });
                WorkingOfflineCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    isNetWorkOnline = false;
                    WorkingOfflineVisibility = Visibility.Visible;
                    WorkingOnlineVisibility = Visibility.Collapsed;
                    WorkingOfflineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg_btn_online_login.png")));
                    WorkingOfflineForeground = new SolidColorBrush(Colors.White);
                    WorkingOfflineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-restaurant-white.png", UriKind.RelativeOrAbsolute));

                    WorkingOnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-btn-offline-login.png")));
                    WorkingOnlineForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                    WorkingOnlineIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-online-active.png", UriKind.RelativeOrAbsolute));
                });
                LoginCommanded = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    IsClose = true; Login(p);
                });
                EnterLoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { IsClose = true; Login(p); });
                CloseWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
                {

                    foreach (Process Proc in Process.GetProcesses())
                    {
                        if (Proc.ProcessName.Equals(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)) Proc.Kill();
                    }
                    System.Windows.Application.Current.Shutdown();
                });
                ClosedAppCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
                {
                    if (!IsClose)
                    {
                        foreach (Process Proc in Process.GetProcesses())
                        {
                            if (Proc.ProcessName.Equals(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)) Proc.Kill();
                        }
                        System.Windows.Application.Current.Shutdown();
                    }
                });
                MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
                {
                    p.WindowState = WindowState.Minimized;
                });
                SearchServerCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
                {
                    NetWorkSaleWindow netWorkSaleWindow = new NetWorkSaleWindow();
                    netWorkSaleWindow.DataContext = new NetWorkSaleViewModel();
                    netWorkSaleWindow.ShowDialog();
                    var tmp = netWorkSaleWindow.DataContext as NetWorkSaleViewModel;
                    if (tmp != null && tmp.IsConfirm)
                    {
                        RestaurantAddress = tmp.IpAddress;
                    }
                });
                ForgetPasswordCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    ForgetPasswordWindow window = new ForgetPasswordWindow();
                    window.DataContext = new ForgetPasswordViewModel();
                    window.ShowDialog();
                });
                ForgotCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                    NotificationForgotPassword notificationForgot = new NotificationForgotPassword();
                    notificationForgot.ShowDialog();
                    notificationForgot.DataContext = new LoginViewModel(); 
                });
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                    p.Close(); 
                }); 
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public async void Login(Window p)
        {
   
            if (isNetWorkOnline)
            {
                Properties.Settings.Default.IsOffline = isNetWorkOnline;
                Properties.Settings.Default.Save();
                if (!Utils.Utils.CheckInternet())
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_CONNECT_FAIL);
                }
                else if (string.IsNullOrEmpty(RestaurantName))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_RESTAURANT_NAME_NOT);
                }
                else if (string.IsNullOrEmpty(UserName))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_USER_NAME_NOT);
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_PASSWORD_NOT);
                }
                else if(Password.Length < 4)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_ERROR_PASSWORD);
                }
                else if (UserName.Length < 8)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_FROM_lOGIN_LENGTH_USERNAME);
                }
                else
                {
                    UserClient userClient = new UserClient(this, this, this);
                    ConfigResponse configResponse = userClient.GetConfig(RestaurantName);
                    if (configResponse != null && configResponse.Status == (int)ResponseEnum.OK)
                    {
                        
                        Constants.SERVER_REALTIME = string.IsNullOrEmpty(configResponse.Data.RealtimeDomain) ? Constants.SERVER_REALTIME : configResponse.Data.RealtimeDomain;
                        Constants.ADS_DOMAIN = configResponse.Data.AdsDomain;
                        Utils.Utils.AddCacheValue(Constants.CURRENT_CONFIG, configResponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                        UserResponse currentUser = userClient.LoginSystem(UserName, Password, configResponse.Data.ApiKey);
                        if (currentUser != null && currentUser.Status == (int)ResponseEnum.OK && currentUser.Data != null)
                        {
                            Constants.SERVER_REALTIME = string.Format("{0}/restaurants_{1}_branches_{2}", Constants.SERVER_REALTIME, currentUser.Data.RestaurantId, currentUser.Data.BranchId);
                            //     Constants.SERVER_REALTIME = string.Format("{0}/restaurants_{1}_branches_{2}","http://172.16.1.31", currentUser.Data.RestaurantId, currentUser.Data.BranchId);
                            currentUser.Data.NodeAccessToken = GetNodeAccessToken(currentUser.Data, Password);
                            currentUser.Data = Utils.Utils.SetUserManager(currentUser.Data);
                            Utils.Utils.AddCacheValue(Constants.CURRENT_USER, currentUser.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                            if (IsSavePassword)
                            {
                                Properties.Settings.Default.Mind = IsSavePassword;
                                Properties.Settings.Default.RestaurantDomainAPI = RestaurantName;
                                Properties.Settings.Default.UserNameMind = UserName;
                                Properties.Settings.Default.PasswordMind = Password;
                                Properties.Settings.Default.Save();

                            }
                            else
                            {
                                Properties.Settings.Default.Mind = IsSavePassword;
                                Properties.Settings.Default.RestaurantDomainAPI = RestaurantName;
                                Properties.Settings.Default.UserNameMind = UserName;
                                Properties.Settings.Default.PasswordMind = "";
                                Properties.Settings.Default.Save();
                            }

                            SettingClient settingClient = new SettingClient(this, this, this);
                            Setting setting = settingClient.GetSetting(currentUser.Data.BranchId);
                            if (setting != null && setting.Status == (int)ResponseEnum.OK && setting.Data != null)
                            {
                                Constants.KEY_CALL_API = setting.Data.ApiPrefixPathForBranchType;
                                Constants.IS_NETWORK_ONLINE = setting.Data.IsWorkingOffline ? false : true;
                                Utils.Utils.AddCacheValue(Constants.CURRENT_SETTING, setting.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                            }
                            VersionClient client = new VersionClient(this, this, this);
                            Models.Version v = client.GetVersionAPI();
                            if (v != null && v.Status == (int)ResponseEnum.OK && v.Data != null)
                            {
                                if (!v.Data.VersionName.Contains(Properties.Settings.Default.VERSION))
                                {
                                    UpdateVersion version = new UpdateVersion();
                                    version.VersionName.Text = v.Data.VersionName;
                                    version.DownloadUrl.Text = v.Data.DownloadLink;
                                    version.ContentNotification.Text = v.Data.Message;
                                    version.ShowDialog();
                                    p.Close();
                                }
                                else
                                {
                                    //if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS) }, currentUser.Data.Permissions))
                                    //if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() {Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS) }, currentUser.Data.Permissions))

                                    if (Utils.Utils.CheckRoleAccessWindowsApplication(currentUser.Data.Permissions))
                                    {
                                        DialogHostOpen = true;
                                        MainWindow mainWindow = new MainWindow();
                                        mainWindow.DataContext = await Task.Run(() => new MainViewModel());
                                        //p.Hide();
                                        DialogHostOpen = false;
                                        mainWindow.Show();
                                        IsLogin = true;
                                        DialogHostOpen = false;
                                        p.Close();
                                       
                                    }
                                    else
                                    {
                                        NotificationMessage.Warning(MessageValue.FORBIDDEN);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_USER);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_CONFIG);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_SETTING);
                                        Constants.IS_FIRST_LOGIN = true;
                                    }
                                }
                            }
                            else
                            {
                                if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS) }, currentUser.Data.Permissions))
                                {
                                    MainWindow mainWindow = new MainWindow();
                                    mainWindow.DataContext = await Task.Run(() => new MainViewModel());
                                    p.Hide();
                                    mainWindow.Show();
                                    IsLogin = true;
                                    p.Close();
                                }
                                else
                                {
                                    NotificationMessage.Warning(MessageValue.FORBIDDEN);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_USER);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_CONFIG);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_SETTING);
                                    Constants.IS_FIRST_LOGIN = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Properties.Settings.Default.IsOffline = isNetWorkOnline;
                Properties.Settings.Default.Save();
                if (string.IsNullOrEmpty(RestaurantAddress))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_RESTAURANT_ADDRESS_NOT);
                }
                else if (string.IsNullOrEmpty(UserName))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_USER_NAME_NOT);
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_PASSWORD_NOT);
                }
                else
                {
                    Constants.SERVER_OFFLINE_DOMAIN = string.Format("http://{0}", RestaurantAddress);
                    UserClient userClientOff = new UserClient(this, this, this);
                    ConfigResponse configResponse = userClientOff.GetConfig(RestaurantAddress);
                    if (configResponse != null && configResponse.Status == (int)ResponseEnum.OK)
                    {
                        Constants.WEBSOCKET_DOMAIN = configResponse.Data.RealtimeDomain;
                        Constants.ADS_DOMAIN = configResponse.Data.AdsDomain;
                        Utils.Utils.AddCacheValue(Constants.CURRENT_CONFIG, configResponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                        Constants.SERVER_REALTIME = string.IsNullOrEmpty(configResponse.Data.RealtimeDomain) ? Constants.SERVER_REALTIME : configResponse.Data.RealtimeDomain;
                        UserResponse currentUser = userClientOff.LoginSystem(UserName, Password, configResponse.Data.ApiKey);
                        if (currentUser != null && currentUser.Status == (int)ResponseEnum.OK && currentUser.Data != null)
                        {
                            Properties.Settings.Default.IsOffline = isNetWorkOnline;
                            if (IsSavePassword)
                            {
                                Properties.Settings.Default.Mind = IsSavePassword;
                                Properties.Settings.Default.RestaurantDomainAPI = RestaurantAddress;
                                Properties.Settings.Default.UserNameMind = UserName;
                                Properties.Settings.Default.PasswordMind = Password;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.Mind = IsSavePassword;
                                Properties.Settings.Default.RestaurantDomainAPI = RestaurantAddress;
                                Properties.Settings.Default.UserNameMind = UserName;
                                Properties.Settings.Default.PasswordMind = "";
                                Properties.Settings.Default.Save();
                            }
                            //currentUser.Data.NodeAccessToken = GetNodeAccessToken(currentUser.Data, Password);
                            Utils.Utils.AddCacheValue(Constants.CURRENT_USER, currentUser.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                            VersionClient client = new VersionClient(this, this, this);
                            Models.Version v = client.GetVersionAPI();
                            if (v != null && v.Status == (int)ResponseEnum.OK && v.Data != null)
                            {
                                SettingClient settingClient = new SettingClient(this, this, this);
                                Setting setting = settingClient.GetSetting(currentUser.Data.BranchId);
                                if (setting != null && setting.Status == (int)ResponseEnum.OK && setting.Data != null)
                                {
                                    Constants.SERVER_OFFLINE_DOMAIN = Constants.SERVER_OFFLINE_DOMAIN + setting.Data.ApiPrefixPathForBranchType;
                                    Utils.Utils.AddCacheValue(Constants.CURRENT_SETTING, setting.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                                }
                                if (!v.Data.VersionName.Contains(Properties.Settings.Default.VERSION))
                                {
                                    UpdateVersion version = new UpdateVersion();
                                    version.VersionName.Text = v.Data.VersionName;
                                    version.DownloadUrl.Text = v.Data.DownloadLink;
                                    version.ContentNotification.Text = v.Data.Message;
                                    version.ShowDialog();
                                    p.Close();
                                }
                                else
                                {
                                    if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS) }, currentUser.Data.Permissions))
                                    {
                                        MainWindow mainWindow = new MainWindow();
                                        mainWindow.DataContext = await Task.Run(() => new MainViewModel());
                                        p.Hide();
                                        mainWindow.Show();
                                        IsLogin = true;
                                        p.Close();
                                    }
                                    else
                                    {
                                        NotificationMessage.Warning(MessageValue.FORBIDDEN);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_USER);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_CONFIG);
                                        Utils.Utils.DeleteCacheValue(Constants.CURRENT_SETTING);
                                        Constants.IS_FIRST_LOGIN = true;
                                    }
                                }
                            }
                            else
                            {
                                SettingClient settingClient = new SettingClient(this, this, this);
                                Setting setting = settingClient.GetSetting(currentUser.Data.BranchId);
                                if (setting != null && setting.Status == (int)ResponseEnum.OK && setting.Data != null)
                                {
                                    Constants.SERVER_OFFLINE_DOMAIN = Constants.SERVER_OFFLINE_DOMAIN + setting.Data.ApiPrefixPathForBranchType;
                                    Utils.Utils.AddCacheValue(Constants.CURRENT_SETTING, setting.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                                }
                                if (Utils.Utils.CheckListPermissionsEmployee(new List<string>() { Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_BBQ_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS) }, currentUser.Data.Permissions))
                                {
                                    MainWindow mainWindow = new MainWindow();
                                    mainWindow.DataContext = await Task.Run(() => new MainViewModel());
                                    p.Hide();
                                    mainWindow.Show();
                                    IsLogin = true;
                                    p.Close();
                                }
                                else
                                {
                                    NotificationMessage.Warning(MessageValue.FORBIDDEN);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_USER);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_CONFIG);
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_SETTING);
                                    Constants.IS_FIRST_LOGIN = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private string GetNodeAccessToken(User user, string password)
        {
            UserNodeClient userClient = new UserNodeClient(this, this, this);
            ConfigNodeResponse configResponse = userClient.GetConfig();
            if (configResponse != null && configResponse.Status == (int)HttpStatusCode.OK)
            {
                UserNodeResponse userNodeResponse = userClient.LoginSystemNode(user, password, configResponse.Data.ApiKey);
                if (userNodeResponse != null && userNodeResponse.Status == (int)HttpStatusCode.OK)
                {
                    return userNodeResponse.Data.NodeAccessToken;
                    //  Utils.Utils.AddCacheValue(Constants.CURRENT_USER_CHART, userNodeResponse.Data, DateTimeOffset.Now.AddDays(MAX_EXPIRE_CACHE));
                }
            }
            return string.Empty;
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
        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

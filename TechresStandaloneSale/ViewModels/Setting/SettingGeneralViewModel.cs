using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.SettingS
{
    public class SettingGeneralViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private bool _IsCheckHidden { get; set; }
        public bool IsCheckHidden { get => _IsCheckHidden; set { _IsCheckHidden = value; OnPropertyChanged("IsCheckHidden"); } }
        private bool _IsCheckTwoLayout { get; set; }
        public bool IsCheckTwoLayout { get => _IsCheckTwoLayout; set { _IsCheckTwoLayout = value; OnPropertyChanged("IsCheckTwoLayout"); } }
        private Visibility _TwoLayoutVisibility { get; set; }
        public Visibility TwoLayoutVisibility { get => _TwoLayoutVisibility; set { _TwoLayoutVisibility = value; OnPropertyChanged("TwoLayoutVisibility"); } }
        public ICommand SaveCommand { get; set; }
        public DeviceClient deviceClient = new DeviceClient();
        private ContentControl _MainContentControl;
       // private bool CurrentIsCheckTwoLayout;
        public SettingGeneralViewModel()
        {
            try
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
                if (secondaryScreen != null)
                    TwoLayoutVisibility = Visibility.Visible;
                else
                    TwoLayoutVisibility = Visibility.Collapsed;
                SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
                IsCheckHidden = setting.IsHiddenHeaderAndFooter;
                //IsCheckHidden = false;
                IsCheckTwoLayout = setting.IsTwoLayout;

                SaveCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    setting.IsHiddenHeaderAndFooter = IsCheckHidden;
                    //NotificationMessage ni = new NotificationMessage();
                    if (IsCheckTwoLayout  != setting.IsTwoLayout)
                    {
                        setting.IsTwoLayout = IsCheckTwoLayout;
                        deviceClient.SaveSettingLayout(setting);
                        if (secondaryScreen != null && setting.IsTwoLayout == true)
                        {
                            var workingArea = secondaryScreen.Bounds;
                            MainSecondViewModel.mainSecondWindow = new MainSecondWindow();
                            MainSecondViewModel.mainSecondWindow.Left = workingArea.Left;
                            MainSecondViewModel.mainSecondWindow.Top = workingArea.Top;
                            MainSecondViewModel.mainSecondWindow.Width = workingArea.Width;
                            MainSecondViewModel.mainSecondWindow.Height = workingArea.Height;
                            _MainContentControl = MainSecondViewModel.mainSecondWindow.FindName("ContentMain") as ContentControl;
                            FoodsBannerUserControl tmp = new FoodsBannerUserControl();
                            _MainContentControl.Content = tmp;
                            MainSecondViewModel.mainSecondWindow.Show();

                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                            return;
                        }
                        else
                        {
                            MainSecondViewModel.CloseMainSecondDefault();
                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                            return;
                        }
                    }
                    else
                    {
                        NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS_LAYOUT_SETTING);
                    }                  
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        FrameworkElement GetWindowParent(System.Windows.Controls.UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }

            return parent;
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

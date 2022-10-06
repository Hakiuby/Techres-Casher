using DevExpress.Mvvm.Native;
using Microsoft.Toolkit.Extensions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Loading;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.UserControlView.SettingS;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace TechresStandaloneSale.ViewModels.SettingS 
{
    public class SettingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public User currentUser;
        private Visibility _printVisibility;
        public Visibility PrintVisibility
        {
            get { return _printVisibility; }
            set { _printVisibility = value; OnPropertyChanged("PrintVisibility"); }
        }
        private Visibility _SencondaySceenVisibility; // Dat
        public Visibility SencondaySceenVisibility
        {
            get { return _SencondaySceenVisibility; }
            set { _SencondaySceenVisibility = value; OnPropertyChanged("SencondaySceenVisibility"); }
        }
        private Brush _GeneralSettingBackground;
        public Brush GeneralSettingBackground { get => _GeneralSettingBackground; set { _GeneralSettingBackground = value; OnPropertyChanged("GeneralSettingBackground"); } }
        private Brush _GeneralSettingForeground;
        public Brush GeneralSettingForeground { get => _GeneralSettingForeground; set { _GeneralSettingForeground = value; OnPropertyChanged("GeneralSettingForeground"); } }
        private Brush _PrintConfigBackground;
        public Brush PrintConfigBackground { get => _PrintConfigBackground; set { _PrintConfigBackground = value; OnPropertyChanged("PrintConfigBackground"); } }
        private Brush _PrintConfigForeground;
        public Brush PrintConfigForeground { get => _PrintConfigForeground; set { _PrintConfigForeground = value; OnPropertyChanged("PrintConfigForeground"); } }
        private Brush _SencondaySceenBackground;
        public Brush SencondaySceenBackground { get => _SencondaySceenBackground; set { _SencondaySceenBackground = value; OnPropertyChanged("SencondaySceenBackground"); } }

        private Brush _SencondaySceenForeground;
        public Brush SencondaySceenForeground { get => _SencondaySceenForeground; set { _SencondaySceenForeground = value; OnPropertyChanged("SencondaySceenForeground"); } }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand PrintConfigWindowCommand { get; set; }
        public ICommand GeneralSettingWindowCommand { get; set; }

        public ICommand SencondaySceenWindowCommand { get; set; }
        private static ContentControl _MainContentControl;
        public SettingViewModel()
        {
            #region Dat
            SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
            if ((int)BranchTypeEnum.MEDIUM == currentSetting.BranchType)
            {
                if (currentSetting.BranchTypeOption == 3)
                {
                    SencondaySceenVisibility = Visibility.Visible;
                }
                else
                {
                    SencondaySceenVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                SencondaySceenVisibility = Visibility.Collapsed;
            }
            #endregion
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
            {
                if(Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
                {
                    PrintVisibility = Visibility.Visible;
                }
                else
                {
                    PrintVisibility = Visibility.Hidden;
                }
            }
            else
            {
                PrintVisibility = Visibility.Visible;
            }
            LoadedWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                GeneralSettingBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                GeneralSettingForeground = new SolidColorBrush(Colors.White);
                PrintConfigBackground = new SolidColorBrush(Colors.White);
                PrintConfigForeground = new SolidColorBrush(Colors.Black);
                SencondaySceenBackground = new SolidColorBrush(Colors.White);
                SencondaySceenForeground = new SolidColorBrush(Colors.Black);

                _MainContentControl = p.FindName("ContentSetting") as ContentControl;
                GeneralSettingUserControl generalSettingUserControl = new GeneralSettingUserControl();
                generalSettingUserControl.DataContext = new SettingGeneralViewModel();
                _MainContentControl.Content = generalSettingUserControl;
            });
            GeneralSettingWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                GeneralSettingBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                GeneralSettingForeground = new SolidColorBrush(Colors.White);
                PrintConfigBackground = new SolidColorBrush(Colors.White);
                PrintConfigForeground = new SolidColorBrush(Colors.Black);
                SencondaySceenBackground = new SolidColorBrush(Colors.White);
                SencondaySceenForeground = new SolidColorBrush(Colors.Black);

                _MainContentControl = p.FindName("ContentSetting") as ContentControl;
                GeneralSettingUserControl generalSettingUserControl = new GeneralSettingUserControl();
                generalSettingUserControl.DataContext = new SettingGeneralViewModel();
                _MainContentControl.Content = generalSettingUserControl;
            });
            PrintConfigWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                GeneralSettingBackground = new SolidColorBrush(Colors.White);
                GeneralSettingForeground = new SolidColorBrush(Colors.Black);
                SencondaySceenBackground = new SolidColorBrush(Colors.White);
                SencondaySceenForeground = new SolidColorBrush(Colors.Black);
                PrintConfigBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                PrintConfigForeground = new SolidColorBrush(Colors.White);
            
                _MainContentControl = p.FindName("ContentSetting") as ContentControl;
                PrintSettingUserControl printSettingUserControl = new PrintSettingUserControl();
                printSettingUserControl.DataContext = new PrintConfigViewModel();
                _MainContentControl.Content = printSettingUserControl;
            });

            SencondaySceenWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, p => {

                GeneralSettingBackground = new SolidColorBrush(Colors.White);
                GeneralSettingForeground = new SolidColorBrush(Colors.Black);
                PrintConfigBackground = new SolidColorBrush(Colors.White);
                PrintConfigForeground = new SolidColorBrush(Colors.Black);
                SencondaySceenBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                SencondaySceenForeground = new SolidColorBrush(Colors.White);

                _MainContentControl = p.FindName("ContentSetting") as ContentControl;
                SencondaryScreenUserControl printSettingUserControl = new SencondaryScreenUserControl();
            //    printSettingUserControl.DataContext = new PrintConfigViewModel();
                _MainContentControl.Content = printSettingUserControl;

            });
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

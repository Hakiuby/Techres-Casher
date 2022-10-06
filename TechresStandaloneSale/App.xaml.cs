using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Ikc5.TypeLibrary.Logging;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.ViewModels;
using TechresStandaloneSale.Views;
using Unity;
using Unity.Resolution;
using Application = System.Windows.Application;

namespace TechresStandaloneSale
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            DeviceClient deviceClient = new DeviceClient();
            SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
            if (setting == null)
            {
                setting = new SettingLayoutWrapper();
                setting.IsHiddenHeaderAndFooter = false;
                setting.IsTwoLayout = false;
                deviceClient.SaveSettingLayout(setting);
            }
            var secondaryScreen = Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();        
            if (secondaryScreen != null && setting.IsTwoLayout)
            {
                MainSecondViewModel.ShowMainSecondDefault();
            }
        }
    }
}

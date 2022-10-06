using Newtonsoft.Json;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models.Request;

namespace TechresStandaloneSale.Services
{
    public class DeviceClient
    {
        public void SaveConfigs(DeviceConfigWrapper wrapper)
        {
            var js = JsonConvert.SerializeObject(wrapper);

            Utils.Utils.save_Setting(Constants.SETTING_PRINT, js);

        }
        public void SaveSettingLayout(SettingLayoutWrapper setting)
        {
            var js = JsonConvert.SerializeObject(setting);
            Utils.Utils.save_SettingLayout(Constants.SETTING_LAYOUT, js);
        }
        public SettingLayoutWrapper ReadCurrentSettingLayout()
        {
            string line = Properties.Settings.Default.ConfigSettingLayout;
            SettingLayoutWrapper setting = JsonConvert.DeserializeObject<SettingLayoutWrapper>(line);
            if (setting != null)
                return setting;
            else
                return null;
        }
        public DeviceConfigWrapper RealConfigs()
        {
            // Read the stream to a string, and write the string to the console.
            string line = Properties.Settings.Default.ConfigPrint;
            DeviceConfigWrapper device = JsonConvert.DeserializeObject<DeviceConfigWrapper>(line);
            if (device != null)
            {
                return device;
            }
            else
            {
                return null;
            }


        }


    }
}

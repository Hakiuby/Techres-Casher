using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Template;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
   public class QRCodeRegistrationCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _RestaurantName { get; set; }
        public string RestaurantName { get => _RestaurantName; set { _RestaurantName = value; OnPropertyChanged("RestaurantName"); } }

        private BitmapImage _QrCode { get; set; }
        public BitmapImage QrCode { get => _QrCode; set { _QrCode = value; OnPropertyChanged("QrCode"); } }

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);

        public Restaurant currentRestaurant = (Restaurant)Utils.Utils.GetCacheValue(Constants.CURRENT_RESTAURANT);
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand PrintCommand { get; set; }
     
        public QRCodeRegistrationCustomerViewModel()
        {
            if (currentRestaurant!= null)
            {
                RestaurantName = string.Format("{0}", currentRestaurant.Name);
                User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                string qrCode = string.Format(MessageValue.MESSAGE_QR_CODE_REGISTRATION_CUSTOMER, currentRestaurant.RestaurantName.ToLower().Trim(' '), currentRestaurant.Id, 1);

                MemoryStream ms = new MemoryStream();
                string qrcode = new QRCodeHelper(qrCode).GenerateUrl();
                using (WebClient webClient = new WebClient())
                { 
                    using (Stream stream = webClient.OpenRead(qrcode))
                    {
                        using (Bitmap bitmap = new Bitmap(stream))
                        {
                            stream.Flush();
                            stream.Close();
                            bitmap.Save(ms, ImageFormat.Png);
                        }
                    }
                }
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                QrCode = bi;

            }

            PrintCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                DeviceClient deviceClient = new DeviceClient();
                DeviceConfigWrapper device = deviceClient.RealConfigs();
                if (device != null && device.IsEmployeeQRCode && !string.IsNullOrEmpty(device.EmployeeQrCodePrinter))
                {
                    if (!string.IsNullOrEmpty(device.QrCodePrinter))
                    {
                        dialog.PrintQueue = new PrintQueue(new PrintServer(), device.EmployeeQrCodePrinter);
                    }
                    MemoryStream ms = new MemoryStream();
                    string qrcode = new QRCodeHelper(string.Format(MessageValue.MESSAGE_QR_CODE_REGISTRATION_CUSTOMER, currentRestaurant.Name.ToLower().Trim(' '), currentRestaurant.Id)).GenerateUrl();
                    using (WebClient webClient = new WebClient())
                    {
                        using (Stream stream = webClient.OpenRead(qrcode))
                        {
                            using (Bitmap bitmap = new Bitmap(stream))
                            {
                                stream.Flush();
                                stream.Close();
                                bitmap.Save(ms, ImageFormat.Png);
                            }
                        }
                    }
                    if(device.EmployeeQrCodeSize == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                    {
                        PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                        print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                        print.ImageQrCode.Source = bi;
                        print.RestaurantName.Text = string.Format("{0} - {1}", currentRestaurant.Name, currentRestaurant.Name);
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                    else
                    {
                        PrintQrCodeCheckIn58MM print = new PrintQrCodeCheckIn58MM();
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                        print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                        print.ImageQrCode.Source = bi;
                        print.RestaurantName.Text = string.Format("{0} - {1}", currentRestaurant.Name, currentRestaurant.Name);
                        FlowDocument doc = print.InfoDocument;
                        doc.PagePadding = new Thickness(0);
                        doc.ColumnWidth = dialog.PrintableAreaWidth;
                        doc.PageHeight = dialog.PrintableAreaHeight;
                        IDocumentPaginatorSource idSource = doc;
                        dialog.PrintDocument(idSource.DocumentPaginator, "");
                    }
                }
            });
        }

        public void LogError(Exception ex, string infoMessage)
        {

            //throw new NotImplementedException();
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
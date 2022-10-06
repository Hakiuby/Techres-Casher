using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
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
    public class QRCodeBranchCheckInViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
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
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private long BranchId;
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand PrintCommand { get; set; }
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
        public void GetAllBranches()
        {
            if (BranchList == null)
            {
                BranchList = new ObservableCollection<Branch>();
            }
            else
            {
                BranchList.Clear();
            }

            List<Branch> branches = (List<Branch>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_BRANCH);
            if (branches != null)
            {
                branches.ForEach(BranchList.Add);
                BranchList.RemoveAt(0);
            }
        }
        //public ICommand SaveCommand { get; set; }
        public QRCodeBranchCheckInViewModel()
        {
            if (currentSetting.IsEnableCheckin)
            {
                if (currentUser.UserManagerId == (int)UserManagerEnum.RESTAURANT)
                {

                    BrandVisibility = Visibility.Visible;
                    if (BrandList == null)
                    {
                        BrandList = new ObservableCollection<Brand>();
                    }
                    else
                    {
                        BrandList.Clear();
                    }
                    BrandList = Utils.Utils.GetBrands(true);
                    BrandItem = BrandList.Where(x => x.Id == currentUser.RestaurantBrandId).FirstOrDefault();
                    BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                    if (BranchList == null)
                    {
                        BranchList = new ObservableCollection<Branch>();
                    }
                    else
                    {
                        BranchList.Clear();
                    }
                    BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, true);
                    BranchItem = BranchList.Where(x => x.Id == currentUser.BranchId).FirstOrDefault();
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                }
                else if (currentUser.UserManagerId == (int)UserManagerEnum.BRAND)
                {
                    BranchVisibility = Visibility.Visible;
                    if (BranchList == null)
                    {
                        BranchList = new ObservableCollection<Branch>();
                    }
                    else
                    {
                        BranchList.Clear();
                    }
                    BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, true);
                    BranchItem = BranchList.Where(x => x.Id == currentUser.BranchId).FirstOrDefault();
                    BrandId = currentUser.RestaurantBrandId;
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                }
                else
                {
                    BrandVisibility = Visibility.Collapsed;
                    BranchVisibility = Visibility.Collapsed;
                    BrandId = currentUser.RestaurantBrandId;
                    BranchId = currentUser.BranchId;
                }

                if (BranchItem!= null)
                {
                    RestaurantName = string.Format("{0}", BranchItem.Name);

                    MemoryStream ms = new MemoryStream();
                    string qrcode = new QRCodeHelper(BranchItem.QrCodeCheckin).GenerateUrl();
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
                else
                {
                    if (currentBranch!= null)
                    {
                        RestaurantName = string.Format("{0}", currentBranch.Name);

                        MemoryStream ms = new MemoryStream();
                        string qrcode = new QRCodeHelper(currentBranch.QrCodeCheckin).GenerateUrl();
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
                   
                }
               
            }
            SelectionChangedBrandCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BrandItem != null && BrandId != (BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id))
                {
                    if (BranchList == null)
                    {
                        BranchList = new ObservableCollection<Branch>();
                    }
                    else
                    {
                        BranchList.Clear();
                    }
                    BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                    BranchList = Utils.Utils.GetBranchs(BrandId, true);
                    if (BranchList.Count > 0) BranchItem = BranchList[0];                    
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (BranchItem != null)
                {
                    RestaurantName = string.Format("{0}", BranchItem.Name);

                    MemoryStream ms = new MemoryStream();
                    string qrcode = new QRCodeHelper(BranchItem.QrCodeCheckin).GenerateUrl();
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
            });


           PrintCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                    if (currentSetting.IsEnableCheckin)
                    {
                        if (BranchItem!= null)
                        {
                            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                            DeviceClient deviceClient = new DeviceClient();
                            DeviceConfigWrapper device = deviceClient.RealConfigs();
                        if (device != null && device.IsBranchQRCode && !string.IsNullOrEmpty(device.BranchQrCodePrinter))
                        {
                            if (!string.IsNullOrEmpty(device.BranchQrCodePrinter))
                            {
                                Application.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    dialog.PrintQueue = new PrintQueue(new PrintServer(), device.BranchQrCodePrinter);
                                });
                            }
                            MemoryStream ms = new MemoryStream();
                            string qrcode = new QRCodeHelper(BranchItem.QrCodeCheckin).GenerateUrl();
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
                            if (device.BranchQrCodeSize == 80)
                            {
                                PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                                ms.Position = 0;
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.EndInit();
                                print.Title.Text = MessageValue.MESSAGE_FROM_CHECK_IN_QR;
                                print.ImageQrCode.Source = bi;
                                print.RestaurantName.Text = string.Format("{0}", BranchItem.Name);
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
                                print.Title.Text = MessageValue.MESSAGE_FROM_CHECK_IN_QR;
                                print.ImageQrCode.Source = bi;
                                print.RestaurantName.Text = string.Format("{0}", BranchItem.Name);
                                FlowDocument doc = print.InfoDocument;
                                doc.PagePadding = new Thickness(0);
                                doc.ColumnWidth = dialog.PrintableAreaWidth;
                                doc.PageHeight = dialog.PrintableAreaHeight;
                                IDocumentPaginatorSource idSource = doc;
                                dialog.PrintDocument(idSource.DocumentPaginator, "");
                            }
                        }
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

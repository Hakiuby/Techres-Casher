using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    public class PreviewPrintViewModels : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _PrintPosition;
        public string PrintPosition
        {
            get {   return _PrintPosition;  }
            set {   _PrintPosition = value; OnPropertyChanged("PrintPosition"); }
        }
        private string _PrintName;
        public string PrintName
        {
            get {   return _PrintName;  }
            set {   _PrintName = value; OnPropertyChanged("PrintName"); }    
        }
        private string _PaperSize;
        public string PaperSize
        {
            get { return _PaperSize; }
            set { _PaperSize = value; OnPropertyChanged("PaperSize"); }
        }
        private float _LeftMargin;
        public float LeftMargin
        {
            get { return _LeftMargin; }
            set { _LeftMargin = value; OnPropertyChanged("LeftMargin"); }
        }
        private float _RightMargin;
        public float RightMargin
        {
            get { return _RightMargin; }
            set { _RightMargin = value; OnPropertyChanged("RightMargin"); }
        }
        private float _TopMargin;
        public float TopMargin
        {
            get { return _TopMargin; }
            set { _TopMargin = value; OnPropertyChanged("TopMargin"); }
        }
        private float _BottomMargin;
      //  private object deviceClient;

        public float BottomMargin
        {
            get { return _BottomMargin; }
            set { _BottomMargin = value; OnPropertyChanged("BottomMargin"); }
        }

        public ICommand PrintTestCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public PreviewPrintViewModels(string EmployeeRoleName, string PrintItem,int type,int size)
        {
            PrintPosition = EmployeeRoleName;
            PrintName = PrintItem;
            if(size == 80)
            {
                PaperSize = MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80;
            }
            else
            {
                PaperSize = MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_58;
            }
            LeftMargin = MessageValue.MESSAGE_FROM_SETTING_PRINT_MARGIN;
            RightMargin = MessageValue.MESSAGE_FROM_SETTING_PRINT_MARGIN;
            TopMargin = MessageValue.MESSAGE_FROM_SETTING_PRINT_MARGIN;
            BottomMargin = MessageValue.MESSAGE_FROM_SETTING_PRINT_MARGIN;

            PrintTestCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                switch (type)
                {
                    case 1:
                        {
                            Models.BillResponse temp = new Models.BillResponse
                            {
                                Quantity = 2,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_1,
                                UnitPrice = 100000,
                                TotalPrice = 200000,
                            };
                            Models.BillResponse temp2 = new Models.BillResponse
                            {
                                Quantity = 3,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_2,
                                UnitPrice = 150000,
                                TotalPrice = 450000,
                            };
                            List<Models.BillResponse> temp3 = new List<Models.BillResponse>();
                            temp3.Add(temp);
                            temp3.Add(temp2);
                            OrderBillData test = new OrderBillData
                            {
                                Id = 1,
                                TableName = MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST,
                                UpdatedAt = MessageValue.MESSAGE_FROM_SETTING_PRINT_TITLE_TEST,
                                EmployeeName = MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST,
                                CashierName = MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST,
                                CustomerSlotNumber = 2,
                                CreatedAt = Utils.Utils.GetDateFormatVN(DateTime.Now),
                                Foods = temp3,
                                DiscountAmount = 50000,
                                Amount = 650000,
                                TotalAmount = 600000,
                            };
                            OrderItemResponse PrintTest = new OrderItemResponse
                            {
                                Data = test,
                            };
                            string mode = MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES;
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
                            pt.Print(mode, PrintTest, PrintItem,size, currentBranch);
                            break;
                        }
                    case 2:
                        {
                            Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 2,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_SEAFOOD_TEST_1,
                            };
                            Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 3,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_SEAFOOD_TEST_2,
                            };
                            List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                            temp3.Add(temp);
                            temp3.Add(temp2);
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            pt.PrintFoodCook(PrintItem, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, size);
                            break;
                        }
                    case 3:
                        {
                            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                            DeviceClient deviceClient = new DeviceClient();
                            DeviceConfigWrapper device = deviceClient.RealConfigs();
                            if (device != null && device.IsQRCode && !string.IsNullOrEmpty(device.QrCodePrinter))
                            {
                                if (!string.IsNullOrEmpty(PrintItem))
                                {
                                    dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintItem);
                                }
                                MemoryStream ms = new MemoryStream();
                                string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
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
                                if(size == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                                {
                                    PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                                    ms.Position = 0;
                                    BitmapImage bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.StreamSource = ms;
                                    bi.EndInit();
                                    print.ImageQrCode.Source = bi;
                                    print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
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
                                    print.ImageQrCode.Source = bi;
                                    print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                                    FlowDocument doc = print.InfoDocument;
                                    doc.PagePadding = new Thickness(0);
                                    doc.ColumnWidth = dialog.PrintableAreaWidth;
                                    doc.PageHeight = dialog.PrintableAreaHeight;
                                    IDocumentPaginatorSource idSource = doc;
                                    dialog.PrintDocument(idSource.DocumentPaginator, "");
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 2,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_DRINK_TEST_1,
                            };
                            Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 3,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_DRINK_TEST_2,
                            };
                            List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                            temp3.Add(temp);
                            temp3.Add(temp2);
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            pt.PrintFoodCook(PrintItem, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, size);
                            break;
                        }
                    case 5:
                        {
                            Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 2,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_1,
                            };
                            Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 3,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_BBQ_TEST_2,
                            };
                            List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                            temp3.Add(temp);
                            temp3.Add(temp2);
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            pt.PrintFoodCook(PrintItem, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, size);
                            break;
                        }
                    case 7:
                        {
                            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
                            DeviceClient deviceClient = new DeviceClient();
                            DeviceConfigWrapper device = deviceClient.RealConfigs();
                                MemoryStream ms = new MemoryStream();
                                string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
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
                                if(size == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                            {
                                PrintQrCodeCheckIn print = new PrintQrCodeCheckIn();
                                ms.Position = 0;
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.EndInit();
                                print.ImageQrCode.Source = bi;
                                print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                                print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
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
                                print.ImageQrCode.Source = bi;
                                print.Title.Text = MessageValue.MESSAGE_FROM_CREATE_CUSTOMER_QR;
                                print.RestaurantName.Text = string.Format("{0}", MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES);
                                FlowDocument doc = print.InfoDocument;
                                doc.PagePadding = new Thickness(0);
                                doc.ColumnWidth = dialog.PrintableAreaWidth;
                                doc.PageHeight = dialog.PrintableAreaHeight;
                                IDocumentPaginatorSource idSource = doc;
                                dialog.PrintDocument(idSource.DocumentPaginator, "");
                            }   
                            break;
                        }
                    case 8:
                        {
                            MemoryStream ms = new MemoryStream();
                            string qrcode = new QRCodeHelper(MessageValue.MESSAGE_FROM_SETTING_PRINT_TECHRES).GenerateUrl();
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
                            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

                            DeviceClient deviceClient = new DeviceClient();
                            DeviceConfigWrapper device = deviceClient.RealConfigs();
                                dialog.PrintQueue = new PrintQueue(new PrintServer(), PrintItem);
                            if(size == MessageValue.MESSAGE_FROM_SETTING_PRINT_PAPER_SIZE_80_VALUE)
                            {
                                PrintQrCode print = new PrintQrCode();
                                ms.Position = 0;

                                print.ImageQrCode.Source = bi;
                                print.restaurantName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.EmployeeName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.Username.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.Password.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                FlowDocument doc = print.InfoDocument;
                                doc.PagePadding = new Thickness(0);

                                doc.ColumnWidth = dialog.PrintableAreaWidth;
                                doc.PageHeight = dialog.PrintableAreaHeight;

                                IDocumentPaginatorSource idSource = doc;
                                dialog.PrintDocument(idSource.DocumentPaginator, "");
                            }
                            else
                            {
                                PrintQrCode58MM print = new PrintQrCode58MM();
                                ms.Position = 0;

                                print.ImageQrCode.Source = bi;
                                print.restaurantName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.EmployeeName.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.Username.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                print.Password.Text = MessageValue.MESSAGE_FROM_SETTING_PRINT_RESTAURANT_NAME;
                                FlowDocument doc = print.InfoDocument;
                                doc.PagePadding = new Thickness(0);

                                doc.ColumnWidth = dialog.PrintableAreaWidth;
                                doc.PageHeight = dialog.PrintableAreaHeight;

                                IDocumentPaginatorSource idSource = doc;
                                dialog.PrintDocument(idSource.DocumentPaginator, "");
                            }   
                            break;
                        }
                    default:
                        {
                            Models.OrderDetailPrint temp = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 2,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_COOK_TEST_1,
                            };
                            Models.OrderDetailPrint temp2 = new Models.OrderDetailPrint
                            {
                                Id = 1,
                                Quantity = 3,
                                FoodName = MessageValue.MESSAGE_FROM_SETTING_PRINT_COOK_TEST_2,
                            };
                            List<Models.OrderDetailPrint> temp3 = new List<Models.OrderDetailPrint>();
                            temp3.Add(temp);
                            temp3.Add(temp2);
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            pt.PrintFoodCook(PrintItem, temp3, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_TABLE_ID_TEST, MessageValue.MESSAGE_FROM_SETTING_PRINT_CASHIER_TEST, size);
                            break;
                        }
                }

            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
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

        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views;

namespace TechresStandaloneSale.ViewModels
{
    public static class MainSecondViewModel
    {
        private static List<PrivateAdvertsResponse> _LstBanner;
        public static List<PrivateAdvertsResponse> LstBanner;
        public static List<string> validextentions = new List<string> { "png", "jpg", "gif" };

        public static MainSecondWindow mainSecondWindow = new MainSecondWindow();
        private static ContentControl _MainContentControl;
        private static CreateOrderSecondUserControl Content;
        public static List<FoodMenuItem> FoodList;
        public static List<FoodMenuItem> FoodOrderList;
        public static string TotalAmount;
        public static decimal Discount = 0;
        public static decimal Vat = 0;
        public static int IsGift;
        public static decimal GiftAmount = 0;
        public static decimal TotalGiftAmount = 0;
        private static bool IsShowCreateOrder = false;
        private static bool IsShowBanner = false;
        private static DeviceClient deviceClient = new DeviceClient();
        private static SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
        public static void ShowMainSecondDefault()
        {

            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
            if (secondaryScreen != null && setting.IsTwoLayout)
            {               
                var workingArea = secondaryScreen.Bounds;
                mainSecondWindow.Left = workingArea.Left;
                mainSecondWindow.Top = workingArea.Top;
                mainSecondWindow.Width = workingArea.Width;
                mainSecondWindow.Height = workingArea.Height;
                //mainSecondWindow.DataContext = new MainSecondViewModel();
                _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                BasicSecondUserControl tmp = new BasicSecondUserControl();
                _MainContentControl.Content = tmp;
                //Constants.window = t;
                mainSecondWindow.Show();
            }
        }
        public static void GetBannerForKitchenBrandId()
        {
            if (LstBanner != null)
            {
                LstBanner.Clear(); 
            }
            else
            {
                LstBanner = new List<PrivateAdvertsResponse>(); 
            }
        }
        public static void CloseMainSecondDefault()
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
            if (secondaryScreen != null && setting.IsTwoLayout)
            {               
                mainSecondWindow.Close();
            }
        }
        public static void ShowMainSecondBanner()
        {
            if (IsShowBanner == false)
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
                if (secondaryScreen != null && setting.IsTwoLayout)
                {
                    var workingArea = secondaryScreen.Bounds;
                    mainSecondWindow.Left = workingArea.Left;
                    mainSecondWindow.Top = workingArea.Top;
                    mainSecondWindow.Width = workingArea.Width;
                    mainSecondWindow.Height = workingArea.Height;
                    //mainSecondWindow.DataContext = new MainSecondViewModel();
                    _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                    FoodsBannerUserControl tmp = new FoodsBannerUserControl();
                    _MainContentControl.Content = tmp;
                    //tmp.DataContext = new FoodsBannerViewModel();
                    IsShowBanner = true;
                    IsShowCreateOrder = false;
                }
            }            
        }
        public static void ShowCreateOrderSecond()
        {
            if (IsShowCreateOrder == false)
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
                if (secondaryScreen != null && setting.IsTwoLayout)
                {
                    _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                    CreateOrderSecondUserControl tmp = new CreateOrderSecondUserControl();
                    Content = tmp;
                    _MainContentControl.Content = tmp;
                    //tmp.ListFood.ItemsSource = FoodList;
                    tmp.TotalAmount.Content = TotalAmount;
                    IsShowCreateOrder = true;
                    IsShowBanner = false;
                }
            }
            
        }
        public static void ShowCreateOrderSecond(ObservableCollection<BillResponse> FoodOrderList)
        {
            if (IsShowCreateOrder == false)
            {
                var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
                if (secondaryScreen != null && setting.IsTwoLayout)
                {
                    _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                    CreateOrderSecondUserControl tmp = new CreateOrderSecondUserControl();
                    Content = tmp;
                    _MainContentControl.Content = tmp;
                    Content.lvFoodPending.ItemsSource = FoodOrderList;
                    tmp.TotalAmount.Content = TotalAmount;
                    IsShowCreateOrder = true;
                    IsShowBanner = false;
                }
            }

        }

        public static void ShowOrderFood(ObservableCollection<BillResponse> FoodOrderList)
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
            if (secondaryScreen != null && setting.IsTwoLayout)
            {
                //CreateOrderSecondViewModel.ShowcreateOrderSecond(AllFood);

                //MainSecondViewModel mainSecondViewModel = new MainSecondViewModel();
                //mainSecondWindow.DataContext = mainSecondViewModelAllFood);
                //mainSecondWindow.DataContext = mainSecondViewModel;
                _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                //CreateOrderSecondUserControl tmp = new CreateOrderSecondUserControl();
                _MainContentControl.Content = Content;
               // Content.ListFood.ItemsSource = FoodList;
                Content.lvFoodPending.ItemsSource = FoodOrderList;
                decimal amount = (decimal)FoodOrderList.Where(x => x.IsGift == 0).Sum(x => x.TotalPriceInlcudeAdditionFoods);
                Content.TotalMoney.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(FoodOrderList.Sum(x => x.TotalPriceInlcudeAdditionFoods)));
                if (Vat != 0)
                {
                    //Content.VatPercent.Content = String.Format("{0}%", Vat * 100);
                    Content.Vat.Content = String.Format("{0}đ", Utils.Utils.FormatMoney((amount - amount * Discount) * Vat));
                }
                else
                {
                    //Content.VatPercent.Content = String.Format("{0}%", 0);
                    Content.Vat.Content = String.Format("{0}đ", 0);
                }
                if (Discount != 0)
                {
                    //Content.DiscountPercent.Content = String.Format("{0}%", Discount * 100);
                    Content.Discount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(amount * Discount));
                }
                else
                {
                    //Content.DiscountPercent.Content = String.Format("{0}%", 0);
                    Content.Discount.Content = String.Format("{0}đ", 0);
                }
                if (GiftAmount != 0)
                {
                    TotalGiftAmount = TotalGiftAmount + GiftAmount;
                    Content.TotalGiftAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalGiftAmount));
                    GiftAmount = 0;
                }
                else
                {
                    Content.TotalGiftAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalGiftAmount));
                }
                Content.TotalAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalAmount));
                //Content.Discount.Content = Discount;
                //mainSecondWindow.Show();
            }
        }
        public static void ShowOrderFood1(ObservableCollection<BillResponse> FoodOrderList)
        {
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
            if (secondaryScreen != null && setting.IsTwoLayout)
            {
                //CreateOrderSecondViewModel.ShowcreateOrderSecond(AllFood);

                //MainSecondViewModel mainSecondViewModel = new MainSecondViewModel();
                //mainSecondWindow.DataContext = mainSecondViewModelAllFood);
                //mainSecondWindow.DataContext = mainSecondViewModel;
                _MainContentControl = mainSecondWindow.FindName("ContentMain") as ContentControl;
                //CreateOrderSecondUserControl tmp = new CreateOrderSecondUserControl();
                _MainContentControl.Content = Content;
                // Content.ListFood.ItemsSource = FoodList;
                Content.lvFoodPending.ItemsSource = FoodOrderList;
                decimal amount = (decimal)FoodOrderList.Where(x => x.IsGift == 0).Sum(x => x.TotalPriceInlcudeAdditionFoods);
                Content.TotalMoney.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(FoodOrderList.Sum(x => x.TotalPriceInlcudeAdditionFoods)));
                if (Vat != 0)
                {
                    //Content.VatPercent.Content = String.Format("{0}%", Vat * 100);
                    Content.Vat.Content = String.Format("{0}đ", Utils.Utils.FormatMoney((amount - amount * Discount) * Vat));
                }
                else
                {
                    //Content.VatPercent.Content = String.Format("{0}%", 0);
                    Content.Vat.Content = String.Format("{0}đ", 0);
                }
                if (Discount != 0)
                {
                    //Content.DiscountPercent.Content = String.Format("{0}%", Discount * 100);
                    Content.Discount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(amount * Discount));
                }
                else
                {
                    //Content.DiscountPercent.Content = String.Format("{0}%", 0);
                    Content.Discount.Content = String.Format("{0}đ", 0);
                }
                if (GiftAmount != 0)
                {
                    TotalGiftAmount = TotalGiftAmount + GiftAmount;
                    Content.TotalGiftAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalGiftAmount));
                    GiftAmount = 0;
                }
                else
                {
                    Content.TotalGiftAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalGiftAmount));
                }
                //Content.TotalAmount.Content = String.Format("{0}đ", Utils.Utils.FormatMoney(TotalAmount));
                //Content.Discount.Content = Discount;
                //mainSecondWindow.Show();
            }
        }
    }
}

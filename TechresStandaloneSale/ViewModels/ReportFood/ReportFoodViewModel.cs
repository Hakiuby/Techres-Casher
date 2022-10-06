using DevExpress.Mvvm.Native;
using LiveCharts;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView.ReportRevenue;

namespace TechresStandaloneSale.ViewModels.ReportFood
{
   public class ReportFoodViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand LoadedUCCommand { get; set; }
        public ICommand ReportFoodCommand { get; set; }
        public ICommand ReportDrinkAndOtherdCommand { get; set; }
        public ICommand ReportGiftFoodCommand { get; set; }
        public ICommand ReportComboCommand { get; set; }
        public ICommand ReportCategoryCommand { get; set; }
        public ICommand ReportExtralCommand { get; set; }
        public ICommand ReportOtherMenuCommand { get; set; }
        public ICommand ReportFoodTopCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedTimeCommand { get; set; }
        public ICommand SelectionDayChangedCommand { get; set; }
        public ICommand SelectionWeekChangedCommand { get; set; }
        public ICommand SelectionMonthChangedCommand { get; set; }
        public ICommand SelectionYearChangedCommand { get; set; }
        public ICommand SelectionThreeYearChangedCommand { get; set; }
        public ICommand PrintExcelCommand { get; set; }

        private Visibility _UnitVisibility { get; set; }
        public Visibility UnitVisibility { get => _UnitVisibility; set { _UnitVisibility = value; OnPropertyChanged("UnitVisibility"); } }

        private Brush _ReportFoodBackground { get; set; }
        public Brush ReportFoodBackground { get => _ReportFoodBackground; set { _ReportFoodBackground = value; OnPropertyChanged("ReportFoodBackground"); } }
        private Brush _ReportDrinkAndOtherBackground { get; set; }
        public Brush ReportDrinkAndOtherBackground { get => _ReportDrinkAndOtherBackground; set { _ReportDrinkAndOtherBackground = value; OnPropertyChanged("ReportDrinkAndOtherBackground"); } }

        private Brush _ReportGiftFoodBackground { get; set; }
        public Brush ReportGiftFoodBackground { get => _ReportGiftFoodBackground; set { _ReportGiftFoodBackground = value; OnPropertyChanged("ReportGiftFoodBackground"); } }

        private Brush _ReportComboBackground { get; set; }
        public Brush ReportComboBackground { get => _ReportComboBackground; set { _ReportComboBackground = value; OnPropertyChanged("ReportComboBackground"); } }

        private Brush _ReportCategoryBackground { get; set; }
        public Brush ReportCategoryBackground { get => _ReportCategoryBackground; set { _ReportCategoryBackground = value; OnPropertyChanged("ReportCategoryBackground"); } }

        private Brush _ReportExtralBackground { get; set; }
        public Brush ReportExtralBackground { get => _ReportExtralBackground; set { _ReportExtralBackground = value; OnPropertyChanged("ReportExtralBackground"); } }

        private Brush _ReportOtherMenuBackground { get; set; }
        public Brush ReportOtherMenuBackground { get => _ReportOtherMenuBackground; set { _ReportOtherMenuBackground = value; OnPropertyChanged("ReportOtherMenuBackground"); } }

        private Brush _ReportFoodTopBackground { get; set; }
        public Brush ReportFoodTopBackground { get => _ReportFoodTopBackground; set { _ReportFoodTopBackground = value; OnPropertyChanged("ReportFoodTopBackground"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<BasicModel> _TimesList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> TimesList { get => _TimesList; set { _TimesList = value; OnPropertyChanged("TimesList"); } }
        private BasicModel _TimeItem { get; set; }
        public BasicModel TimeItem { get => _TimeItem; set { _TimeItem = value; OnPropertyChanged("TimeItem"); } }
        public bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }
        private DateTime _DisplayDateStart { get; set; }
        public DateTime DisplayDateStart { get => _DisplayDateStart; set { _DisplayDateStart = value; OnPropertyChanged("DisplayDateStart"); } }
        private DateTime _DisplayDateEnd { get; set; }
        public DateTime DisplayDateEnd { get => _DisplayDateEnd; set { _DisplayDateEnd = value; OnPropertyChanged("DisplayDateEnd"); } }
        private DateTime _DateTimeInput { get; set; }
        public DateTime DateTimeInput { get => _DateTimeInput; set { _DateTimeInput = value; OnPropertyChanged("DateTimeInput"); } }
        private BasicModel _MonthItem { get; set; }
        public BasicModel MonthItem { get => _MonthItem; set { _MonthItem = value; OnPropertyChanged("MonthItem"); } }
        private BasicModel _YearItem { get; set; }
        public BasicModel YearItem { get => _YearItem; set { _YearItem = value; OnPropertyChanged("YearItem"); } }
        private ObservableCollection<BasicModel> _MonthList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> MonthList { get => _MonthList; set { _MonthList = value; OnPropertyChanged("MonthList"); } }
        private ObservableCollection<BasicModel> _YearList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> YearList { get => _YearList; set { _YearList = value; OnPropertyChanged("YearList"); } }
        private ObservableCollection<ReportFoodItem> _FoodList = new ObservableCollection<ReportFoodItem>();
        public ObservableCollection<ReportFoodItem> FoodList { get => _FoodList; set { _FoodList = value; OnPropertyChanged("FoodList"); } }
        private Visibility _FoodVisibility { get; set; }
        public Visibility FoodVisibility { get => _FoodVisibility; set { _FoodVisibility = value; OnPropertyChanged("FoodVisibility"); } }
        private Visibility _DrinkOtherVisibility { get; set; }
        public Visibility DrinkOtherVisibility { get => _DrinkOtherVisibility; set { _DrinkOtherVisibility = value; OnPropertyChanged("DrinkOtherVisibility"); } }
        private Visibility _GiftFoodVisibility { get; set; }
        public Visibility GiftFoodVisibility { get => _GiftFoodVisibility; set { _GiftFoodVisibility = value; OnPropertyChanged("GiftFoodVisibility"); } }
        private Visibility _ComboVisibility { get; set; }
        public Visibility ComboVisibility { get => _ComboVisibility; set { _ComboVisibility = value; OnPropertyChanged("ComboVisibility"); } }
        private Visibility _CategoryVisibility { get; set; }
        public Visibility CategoryVisibility { get => _CategoryVisibility; set { _CategoryVisibility = value; OnPropertyChanged("CategoryVisibility"); } }

        private Visibility _ExtralVisibility { get; set; }
        public Visibility ExtralVisibility { get => _ExtralVisibility; set { _ExtralVisibility = value; OnPropertyChanged("ExtralVisibility"); } }

        private Visibility _OtherMenuVisibility { get; set; }
        public Visibility OtherMenuVisibility { get => _OtherMenuVisibility; set { _OtherMenuVisibility = value; OnPropertyChanged("OtherMenuVisibility"); } }

        private Visibility _FoodTopVisibility { get; set; }
        public Visibility FoodTopVisibility { get => _FoodTopVisibility; set { _FoodTopVisibility = value; OnPropertyChanged("FoodTopVisibility"); } }

        private string _MessageFoodName { get; set; }
        public string MessageFoodName { get => _MessageFoodName; set { _MessageFoodName = value; OnPropertyChanged("MessageFoodName"); } }

        #region toan
        private int _widthButton;
        public int widthButton { get => _widthButton; set { _widthButton = value; OnPropertyChanged("widthButton"); } }

        private int _fontsizeButton;
        public int fontsizeButton { get => _fontsizeButton; set { _fontsizeButton = value; OnPropertyChanged("fontsizeButton"); } }

        #endregion
        private long _TotalDish { get; set; }
        public long TotalDish { get => _TotalDish; set { _TotalDish = value; OnPropertyChanged("TotalDish"); } }
        private string _ToTalQuantity { get; set; }
        public string ToTalQuantity { get => _ToTalQuantity; set { _ToTalQuantity = value; OnPropertyChanged("ToTalQuantity"); } }
        private ReportFoodButtonEnum CurrentButton;
        private int BrandId;
        private string FormDate = Utils.Utils.GetDateFormatVN(DateTime.Now);
        private long BranchId;
        private int Type;
        public async void GetData(int brandId, string formDate, long branchId, int type, ReportFoodButtonEnum currentButton)
        {
            CurrentButton = currentButton;
            BrandId = brandId;
            FormDate = formDate;
            BranchId = branchId;
            Type = type;
            DialogHostOpen = true;

            switch (currentButton)
            {
                case ReportFoodButtonEnum.FOOD:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, Constants.STATUS_CATEGORY_TYPE_COOK, 0, -1, 0, 0, -1));
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportFoodV2(brandId, branchId, formDate, type));
                        //ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, "1", -1, -1, 1, 0, -1, 0));
                        ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, "1", -1, -1, -1, 0, -1, 0));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.List.Count();

                                for (int i =0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = response.Data.List[i];
                                    item.Number = i+1;
                                    FoodList.Add(item);
                                }
                                //response.Data.List.ForEach(FoodList.Add);
                                //ToTalQuantity =  Utils.Utils.FormatMoney(response.Data.SumQuatity);
                                ToTalQuantity = Utils.Utils.FormatMoney(response.Data.List.Sum(x=>x.Quantity)); // Đạt
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.DRINK_OTHER:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                       // ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER, 0, -1, 0, 0,-1));

                       // List<long> statuses = new List<long>() { 2, 3 };

                        ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER, -1, -1, -1, 0, -1, -1));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            try
                            {
                                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                                {


                                    TotalDish = response.Data.List.Count();
                                    for (int i = 0; i < TotalDish; i++)
                                    {
                                        ReportFoodItem item = response.Data.List[i];
                                        item.Number = i + 1;
                                        FoodList.Add(item);
                                    }
                                    ToTalQuantity = Utils.Utils.FormatMoney(response.Data.List.Sum(t => t.Quantity));
                                    DialogHostOpen = false;



                                }
                                else
                                    DialogHostOpen = false;
                            }
                            catch (System.Exception ex)
                            {
                                WriteLog.logs(ex.ToString());
                            }
                        });
                        break;
                    }
                case ReportFoodButtonEnum.GIFT_FOOD:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, Constants.STATUS_CATEGORY_TYPE_COOK, 0, -1, 0, 0,1));
                        ReportFoodResponse response = await Task.Run(() => client.GetGiftfFoodV2(brandId, branchId, formDate, type));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.List.Count();

                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = response.Data.List[i];
                                    item.Number = i + 1;
                                   // item.Quantity = item.GiftQuantity;
                                    FoodList.Add(item);
                                }
                                ToTalQuantity = Utils.Utils.FormatMoney(FoodList.Sum(x=>x.Quantity));
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.COMBO:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, "1", -1, 0, 1, 0, -1, 0));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.List.Count();

                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = response.Data.List[i];
                                    {
                                        item.Number = i + 1;
                                        FoodList.Add(item);
                                    };                                
                                }

                                ToTalQuantity = Utils.Utils.FormatMoney(response.Data.List.Sum(x=>x.Quantity));
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.CATEGORY:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, string.Empty, 1, -1, 0, 0,-1));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                List<ReportFoodItem> items = response.Data.List.Where(x => x.Quantity > 0).ToList();
                                TotalDish = items.Count();

                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = items[i];
                                    item.Number = i + 1;
                                    FoodList.Add(item);
                                }
                                ToTalQuantity = Utils.Utils.FormatMoney(response.Data.SumQuatity);
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.EXTRAL:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        ExtraChargeResponse response = await Task.Run(() => client.GetExtraChargeReport(brandId, formDate, branchId,  type));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.Count();

                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = new ReportFoodItem()
                                    {
                                        Name = response.Data[i].Name,
                                        Quantity = response.Data[i].Quantity

                                    };
                                    item.Number = i + 1;
                                    FoodList.Add(item);
                                }

                                ToTalQuantity = Utils.Utils.FormatMoney(response.Data.Sum(x=>x.Quantity));
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.OTHER_MENU:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, string.Empty, 0, -1, 0, 1,-1));
                        ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, "1", 0, -1, 0, 0, 0, -1));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.List.Where(x => x.CategoryId == 0).Count();
                                var list = response.Data.List.Where(x => x.CategoryId == 0).ToList();
                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = list[i];
                                    item.Number = i + 1;
                                    item.Unit = "Phần";
                                    FoodList.Add(item);
                                }
                                ToTalQuantity = Utils.Utils.FormatMoney(list.Sum(x=>x.Quantity));
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportFoodButtonEnum.CANCEL_FOOD:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportFood(brandId, branchId, formDate, type, Constants.ALL, string.Empty, 0, -1, 1, -1,-1));
                        //ReportFoodResponse response = await Task.Run(() => client.GetReportCancelFoodV2(brandId, formDate, branchId, type));
                        ReportFoodResponse response = await Task.Run(() => client.GetFoodV2(branchId, brandId, type, formDate, "1", -1, -1, -1, 1, -1, -1));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                TotalDish = response.Data.List.Count();

                                for (int i = 0; i < TotalDish; i++)
                                {
                                    ReportFoodItem item = response.Data.List[i];
                                    item.Number = i + 1;
                                  //  item.Quantity = item.CanceledQuantity;
                                    FoodList.Add(item);
                                }
                                ToTalQuantity = Utils.Utils.FormatMoney(response.Data.List.Sum(t => t.Quantity));
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
            }

        }
        public void GetAllTimes()
        {
            if (TimesList == null)
            {
                TimesList = new ObservableCollection<BasicModel>();
            }
            else
            {
                TimesList.Clear();
            }
            BasicModel Date = new BasicModel((int)ReportTypeEnum.HOURS_IN_DAY, MessageValue.MESSAGE_FROM_DATE_TIME);
            BasicModel Week = new BasicModel((int)ReportTypeEnum.DAYS_IN_WEEK, MessageValue.MESSAGE_FROM_WEEK);
            BasicModel Month = new BasicModel((int)ReportTypeEnum.DAYS_IN_MONTH, MessageValue.MESSAGE_FROM_MONTH_AGO);
            BasicModel Quater = new BasicModel((int)ReportTypeEnum.NEAREST_THREE_MONTHS, MessageValue.MESSAGE_FROM_QUARTER);
            BasicModel Year = new BasicModel((int)ReportTypeEnum.MONTHS_IN_YEAR, MessageValue.MESSAGE_FROM_YEAR);
            BasicModel ThreeYear = new BasicModel((int)ReportTypeEnum.MONTHS_IN_THREE_YEARS, MessageValue.MESSAGE_FROM_THREE_YEAR);
            BasicModel ManyYear = new BasicModel((int)ReportTypeEnum.ALL_YEAR, MessageValue.MESSAGE_FROM_MANY_YEAR);
            TimesList.Add(Date);
            TimesList.Add(Week);
            TimesList.Add(Month);
            TimesList.Add(Quater);
            TimesList.Add(Year);
            TimesList.Add(ThreeYear);
            //TimesList.Add(ManyYear);
        }
        public string WeekOfYear(string date)
        {
            DateTime inputDate = DateTime.Parse(date.Trim());
            var d = inputDate;
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            int weekNum = cul.Calendar.GetWeekOfYear(
            d,
            CalendarWeekRule.FirstFullWeek,
            DayOfWeek.Monday);

            int year = weekNum == 52 && d.Month == 1 ? d.Year - 1 : d.Year;
            if (weekNum < 10)
            {
                return string.Format("0{0}/{1}", weekNum, year);
            }
            else
                return string.Format("{0}/{1}", weekNum, year);
        }
        public string MounthOfYear(DateTime dateTime)
        {
            string currentMounth = dateTime.Month.ToString();
            string currentYear = dateTime.Year.ToString();
            if (dateTime.Month < 9)
            {
                return string.Format("0{0}/{1}", currentMounth, currentYear);
            }
            else
                return string.Format("{0}/{1}", currentMounth, currentYear);
        }
        public string YearOfYear (DateTime dateTime)
        {
            return dateTime.Year.ToString();
        }
        public void ResetColorBG(ReportFoodButtonEnum currentButton)
        {
            TotalDish = 0;
            ToTalQuantity = "0";
            if (FoodList == null)
            {
                FoodList = new ObservableCollection<ReportFoodItem>();
            }
            else
            {
                FoodList.Clear();
            }
            CurrentButton = currentButton;
            switch (currentButton)
            {
                case ReportFoodButtonEnum.FOOD:
                    {
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_FOOD;
                        UnitVisibility = Visibility.Visible;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.DRINK_OTHER:
                    {
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_DRIND_AND_OTHER;
                        UnitVisibility = Visibility.Visible;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.GIFT_FOOD:
                    {
                        UnitVisibility = Visibility.Visible;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_GIFT_FOOD;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.COMBO:
                    {
                        UnitVisibility = Visibility.Visible;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_COMBO;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.CATEGORY:
                    {
                        UnitVisibility = Visibility.Collapsed;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_CATEGORY;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.EXTRAL:
                    {
                        UnitVisibility = Visibility.Visible;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_EXTRAL;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.OTHER_MENU:
                    {
                        UnitVisibility = Visibility.Visible;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_OTHER_MENU;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportFoodButtonEnum.CANCEL_FOOD:
                    {
                        UnitVisibility = Visibility.Visible;
                        MessageFoodName = MessageValue.MESSAGE_FROM_REPORT_FOOD_TOP;
                        ReportFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDrinkAndOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportGiftFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportComboBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportCategoryBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportExtralBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOtherMenuBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportFoodTopBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        break;
                    }
            }

        }
        public ContentControl _MainUserControl;
        public ContentControl _SearchUserControl;
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void init()
        {
            if (currentUser != null)
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
                    BrandList = Utils.Utils.GetBrands(false);
                    BrandItem = BrandList.Where(x => x.Id == Constants.ALL).FirstOrDefault();
                    BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                    BranchId = Constants.ALL;
                    if (BranchList == null)
                    {
                        BranchList = new ObservableCollection<Branch>();
                    }
                    else
                    {
                        BranchList.Clear();
                    }
                    BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, false);
                    BranchItem = BranchList.Where(x => x.Id == Constants.ALL).FirstOrDefault();

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
                    BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, false);
                    BranchItem = BranchList.Where(x => x.Id == Constants.ALL).FirstOrDefault();
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
                GetAllTimes();
                TimeItem = TimesList[0];
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    DateTime dateTime = DateTime.Now;
                    DisplayDateStart = new DateTime(2010, 1, 1);
                    DisplayDateEnd = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                    MonthList.Clear();
                    YearList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddYears(-1))
                    {
                        YearList.Add(new BasicModel(i.Year, string.Format("{0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                else
                {
                    CheckDateTimeReportPermission();
                }
            }
        }
        public void CheckDateTimeReportPermission()
        {
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_MONTHS), currentUser.Permissions)
           || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_ONE_YEARS), currentUser.Permissions)
           || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_YEARS), currentUser.Permissions)
           || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_MANY_YEARS), currentUser.Permissions))
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_MONTHS), currentUser.Permissions))
                {
                    DateTime dateTime = DateTime.Now;
                    if (dateTime.Month < 3)
                    {
                        DisplayDateStart = new DateTime(dateTime.Year, 1, 1);
                    }
                    else
                    {
                        DisplayDateStart = new DateTime(dateTime.Year, dateTime.Month - 2, 1);
                    }
                    DisplayDateEnd = Utils.Utils.GetLastDayOfMonth(dateTime);
                    MonthList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                }
                else
                {
                    TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.NEAREST_THREE_MONTHS).FirstOrDefault());
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_ONE_YEARS), currentUser.Permissions))
                {
                    DateTime dateTime = DateTime.Now;
                    DisplayDateStart = new DateTime(dateTime.Year, 1, 1);
                    DisplayDateEnd = new DateTime(dateTime.Year, 12, 31);
                    MonthList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                    YearList.Clear();
                    YearList.Add(new BasicModel(DateTime.Now.Year, string.Format("{0}", DateTime.Now.Year)));
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                else
                {
                    TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR).FirstOrDefault());
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_YEARS), currentUser.Permissions))
                {
                    DateTime dateTime = DateTime.Now;
                    DisplayDateStart = new DateTime(dateTime.Year - 2, 1, 1);
                    DisplayDateEnd = new DateTime(dateTime.Year, 12, 31);
                    MonthList.Clear();
                    YearList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddYears(-1))
                    {
                        YearList.Add(new BasicModel(i.Year, string.Format("{0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                else
                {
                    TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS).FirstOrDefault());
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_MANY_YEARS), currentUser.Permissions))
                {
                    DateTime dateTime = DateTime.Now;
                    DisplayDateStart = new DateTime(2010, 1, 1);
                    DisplayDateEnd = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                    MonthList.Clear();
                    YearList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddYears(-1))
                    {
                        YearList.Add(new BasicModel(i.Year, string.Format("{0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                else
                {
                    TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.ALL_YEAR).FirstOrDefault());
                }
            }
            else
            {
                TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.NEAREST_THREE_MONTHS).FirstOrDefault());
                TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR).FirstOrDefault());
                TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS).FirstOrDefault());
                TimesList.Remove(TimesList.Where(x => x.Value == (int)ReportTypeEnum.ALL_YEAR).FirstOrDefault());

                DateTime dateTime = DateTime.Now;
                DisplayDateStart = new DateTime(dateTime.Year, dateTime.Month, 1);
                DisplayDateEnd = Utils.Utils.GetLastDayOfMonth(dateTime);
                MonthList.Clear();
                for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                {
                    MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                }
                MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
            }
        }
        public void SelectionChanged()
        {
            DialogHostOpen = false;
            ResetColorBG(CurrentButton);
            //GetData(BrandId, FormDate, BranchId, Type, CurrentButton);
            //GetData(BrandId, Utils.Utils.GetDateFormatVN(DateTimeInput), BranchId, Type, CurrentButton);
            GetData(BrandId, FormDate, BranchId, Type, CurrentButton);
        }
        public void CheckPermission()
        {
            FoodVisibility = Visibility.Collapsed;
            DrinkOtherVisibility = Visibility.Collapsed;
            OtherMenuVisibility = Visibility.Collapsed;
            GiftFoodVisibility = Visibility.Collapsed;
            ComboVisibility = Visibility.Collapsed;
            CategoryVisibility = Visibility.Collapsed;
            ExtralVisibility = Visibility.Collapsed;
            FoodTopVisibility = Visibility.Collapsed;

            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
             {
                FoodVisibility = Visibility.Visible;
                DrinkOtherVisibility = Visibility.Visible;
                OtherMenuVisibility = Visibility.Visible;
                GiftFoodVisibility = Visibility.Visible;
                ComboVisibility = Visibility.Visible;
                CategoryVisibility = Visibility.Visible;
                ExtralVisibility = Visibility.Visible;
                FoodTopVisibility = Visibility.Visible;
            }
            else
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), currentUser.Permissions))
                {
                    FoodVisibility = Visibility.Visible;
                    OtherMenuVisibility = Visibility.Visible;
                    ComboVisibility = Visibility.Visible;
                    CategoryVisibility = Visibility.Visible;
                    ExtralVisibility = Visibility.Visible;
                    FoodTopVisibility = Visibility.Visible;
                }
                 if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK), currentUser.Permissions))
                {
                    DrinkOtherVisibility = Visibility.Visible;
                }
                 if(Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_GIFT_FOOD), currentUser.Permissions))
                {
                    GiftFoodVisibility = Visibility.Visible;
                }

            }
        }

        public ReportFoodViewModel()
        {
            LoadedUCCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                #region toan
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    
                    widthButton = 440;
                    fontsizeButton = 18;

                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366)
                {
                    widthButton = 320;
                    fontsizeButton = 17;
                }
                else
                {
                    widthButton = 230;
                    fontsizeButton = 13;

                }
                #endregion
                DialogHostOpen = true;
                init();
                Type = (int)ReportTypeEnum.HOURS_IN_DAY;
                _SearchUserControl = p.FindName("SearchControl") as ContentControl;
                _SearchUserControl.Content = new SearchDayUC();
                DateTimeInput = DateTime.Now;


                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_FOOD), currentUser.Permissions)
                || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    ResetColorBG(ReportFoodButtonEnum.FOOD);
                    GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.FOOD);
                }
                else if(Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DRINK), currentUser.Permissions))
                {
                    ResetColorBG(ReportFoodButtonEnum.DRINK_OTHER);
                    GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.DRINK_OTHER);
                }
                else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.GIFT_FOOD), currentUser.Permissions))
                {
                    ResetColorBG(ReportFoodButtonEnum.GIFT_FOOD);
                    GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.GIFT_FOOD);
                }
                CheckPermission();
            });
            ReportFoodCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.FOOD);

                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.FOOD);
            });
            ReportDrinkAndOtherdCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.DRINK_OTHER);

                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.DRINK_OTHER);
            });
            ReportGiftFoodCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.GIFT_FOOD);
                ResetColorBG(ReportFoodButtonEnum.GIFT_FOOD);
            });
            ReportComboCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.COMBO);

                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.COMBO);
            });
            ReportCategoryCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.CATEGORY);

                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.CATEGORY);
            });
            ReportExtralCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.EXTRAL);

                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.EXTRAL);

            });
            ReportOtherMenuCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                ResetColorBG(ReportFoodButtonEnum.OTHER_MENU);
                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.OTHER_MENU);

            });
            ReportFoodTopCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                GetData(BrandId, FormDate, BranchId, Type, ReportFoodButtonEnum.CANCEL_FOOD);

                ResetColorBG(ReportFoodButtonEnum.CANCEL_FOOD);

            });
            SelectionChangedBrandCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BrandItem != null && BrandId != (BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id))
                {
                    if (BrandItem.Id != Constants.ALL)
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
                        BranchList = Utils.Utils.GetBranchs(BrandId, false);
                        BranchItem = BranchList.Where(x => x.Id == Constants.ALL).FirstOrDefault();
                        BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                        BranchVisibility = Visibility.Visible;
                    }
                    else
                    {
                        BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                        BranchVisibility = Visibility.Collapsed;
                    }
                    SelectionChanged();

                }
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                    SelectionChanged();
                }
            });
            SelectionChangedTimeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (TimeItem.Value == (int)ReportTypeEnum.HOURS_IN_DAY) // Theo Ngay
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchDayUC();
                    // text = string.Format("NGÀY {0}", datetime);
                    FormDate = Utils.Utils.GetDateFormatVN(DateTimeInput);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_WEEK) // Theo Tuan
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchWeekUC();
                    //text = string.Format("TUẦN");
                    string dateInput = DateTimeInput.ToString();
                    FormDate = WeekOfYear(dateInput);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_MONTH) // Theo Thang
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchMonthUC();
                    FormDate = MounthOfYear(DateTimeInput);
                    //DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                    // text = string.Format("THÁNG {0}/{1}", date.Month, date.Year);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.NEAREST_THREE_MONTHS) // Theo 3 Thang
                {
                    _SearchUserControl.Visibility = Visibility.Collapsed;
                    // text = string.Format("3 THÁNG GẦN NHẤT");
                    FormDate = MounthOfYear(DateTimeInput);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR) // Theo Nam
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchYearUC();
                    FormDate = YearOfYear(DateTimeInput);
                    //DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                  //  text = string.Format("NĂM {0}", date.Year);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS) // Theo 3 nam
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchThreeYearUC();
                    FormDate = YearOfYear(DateTimeInput);
                   // DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                   // text = string.Format("3 NĂM {0} - {1} - {2} ", date.Year - 2, date.Year - 1, date.Year);
                }
                else if (TimeItem.Value == (int)ReportTypeEnum.ALL_YEAR)
                {
                    _SearchUserControl.Visibility = Visibility.Collapsed;
                    //text = string.Format("TẤT CẢ CÁC NĂM");
                }
                Type = TimeItem == null ? (int)ReportTypeEnum.HOURS_IN_DAY : TimeItem.Value;

                //if (Type == (int)ReportTypeEnum.HOURS_IN_DAY)
                //{
                //    FormDate = Utils.Utils.GetDateFormatVN(DateTimeInput);
                //}
                //else if (Type == (int)ReportTypeEnum.DAYS_IN_MONTH)
                //{
                //    FormDate = FormDate.Substring(3, 7);
                //}
                //if (Type == (int)ReportTypeEnum.DAYS_IN_WEEK)
                //{
                //    FormDate = Utils.Utils.GetDateFormatVN(DateTimeInput);
                //}
                SelectionChanged();
            });
            SelectionDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
               if ((TimeItem.Value == (int)ReportTypeEnum.HOURS_IN_DAY))
                {
                    string strDate = Utils.Utils.GetDateFormatVN(DateTimeInput);
                    FormDate = strDate;
                    SelectionChanged();
                }
            });
            SelectionWeekChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                //string strDate = Utils.Utils.GetWeek(DateTime.Parse(p.Text)).ToString();
                //string strDate = Utils.Utils.GetWeek(DateTimeInput).ToString();
                //FormDate = strDate;
                if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_WEEK) // Dat
                {
                    string dateInput = DateTimeInput.ToString();
                    FormDate = WeekOfYear(dateInput);
                    SelectionChanged();
                }
            });
            SelectionMonthChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_MONTH) // Dat
                {
                    string FromDate = MonthItem.Value.ToString();
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    string strDate;
                    if (month < 9) // Dat
                    {
                        strDate = String.Format("0{0}/{1}", month, year);
                    }
                    else
                    {
                        strDate = String.Format("{0}/{1}", month, year);
                    }
                    //string strDate = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    FormDate = strDate;
                    SelectionChanged();
                }
            });
            SelectionYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if(TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR) // Dat
                {
                    string strDate = YearItem.Value.ToString();
                    //string strDate = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                    FormDate = strDate;
                    SelectionChanged();
                }
            });
            SelectionThreeYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if(TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS) // Dat
                {
                    string strDate = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                    FormDate = strDate;
                    SelectionChanged();
                }
            });

            PrintExcelCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = MessageFoodName;
                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel|*.xlsx |Excel 2003|*.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_EXPORT_EXCEL_NOT_FILE_PART);

                    return;
                }

                try
                {
                    using (ExcelPackage exp = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        exp.Workbook.Properties.Author = Constants.MESSAGE_EXCEL_AUTHOR;

                        // đặt tiêu đề cho file
                        exp.Workbook.Properties.Title = MessageFoodName;

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_AREA_LIST_TITLE_SHEET);

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_AREA_LIST_TITLE_SHEET;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                        // Tạo danh sách các column header
                        List<string> arrColumnHeader;
                        if (CurrentButton == ReportFoodButtonEnum.CATEGORY)
                        {
                            arrColumnHeader = new List<string>() { MessageValue.MESSAGE_EXPORT_EXCEL_ORDER_VIEW_STT,
                                                MessageFoodName,
                                              MessageValue.MESSAGE_FROM_AMOUNT};
                        }
                        else
                        {
                            arrColumnHeader = new List<string>() { MessageValue.MESSAGE_EXPORT_EXCEL_ORDER_VIEW_STT,
                                                MessageFoodName,
                                                MessageValue.MESSAGE_FROM_FOOD_UNIT,
                                              MessageValue.MESSAGE_FROM_AMOUNT};
                        }
                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        ws.Cells[1, 1].Value = MessageFoodName;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE_HEARDER;
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            ws.Cells.AutoFitColumns();

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }
                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in FoodList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell
                            ws.Cells[rowIndex, colIndex].Value = item.Number;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Name;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                            if (CurrentButton != ReportFoodButtonEnum.CATEGORY)
                            {
                                ws.Cells[rowIndex, colIndex].Value = item.Unit;
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            }

                            ws.Cells[rowIndex, colIndex].Value = item.Quantity;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            ws.Cells.AutoFitColumns();
                        }
                        //Lưu file lại
                        Byte[] bin = exp.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    NotificationMessage.Infomation(MessageValue.MESSAGE_EXPORT_EXCEL_SUCCESS, filePath);
                }
                catch (Exception ex)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_EXPORT_EXCEL_FAIL);
                    WriteLog.logs(ex.Message);
                    //  MessageBox.Show("Có lỗi khi lưu file!");
                }
            });
        }



        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }
        public void Set(string cacheKey, object item, int minutes)
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
        public void LogError(Exception ex, string infoMessage)
        {
            //
        }
    }
}

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
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.ReportGiftFood 
{
    class ReportGiftFoodViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<GiftFood> _ListData = new ObservableCollection<GiftFood>();
        public ObservableCollection<GiftFood> ListData { get => _ListData; set { _ListData = value; OnPropertyChanged("CouponEditPrinceData"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }

        private ObservableCollection<BasicModel> _MonthList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> MonthList { get => _MonthList; set { _MonthList = value; OnPropertyChanged("MonthList"); } }
        private ObservableCollection<BasicModel> _YearList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> YearList { get => _YearList; set { _YearList = value; OnPropertyChanged("YearList"); } }
        private DateTime _DisplayDateStart { get; set; }
        public DateTime DisplayDateStart { get => _DisplayDateStart; set { _DisplayDateStart = value; OnPropertyChanged("DisplayDateStart"); } }
        private DateTime _DateTimeInput { get; set; }
        public DateTime DateTimeInput { get => _DateTimeInput; set { _DateTimeInput = value; OnPropertyChanged("DateTimeInput"); } }
        private DateTime _DisplayDateEnd { get; set; }
        public DateTime DisplayDateEnd { get => _DisplayDateEnd; set { _DisplayDateEnd = value; OnPropertyChanged("DisplayDateEnd"); } }
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private string _ReportContent { get; set; }
        public string ReportContent { get => _ReportContent; set { _ReportContent = value; OnPropertyChanged("ReportContent"); } }
        private string _TotalTime { get; set; }
        public string TotalTime { get => _TotalTime; set { _TotalTime = value; OnPropertyChanged("TotalTime"); } }
        private string _TotalQuantity { get; set; }
        public string TotalQuantity { get => _TotalQuantity; set { _TotalQuantity = value; OnPropertyChanged("TotalQuantity"); } }
        private string _TotalRevenue { get; set; }
        public string TotalRevenue { get => _TotalRevenue; set { _TotalRevenue = value; OnPropertyChanged("TotalRevenue"); } }
        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private Visibility _BtnDayVisibility { get; set; }
        public Visibility BtnDayVisibility { get => _BtnDayVisibility; set { _BtnDayVisibility = value; OnPropertyChanged("BtnDayVisibility"); } }
        private Visibility _BtnWeekVisibility { get; set; }
        public Visibility BtnWeekVisibility { get => _BtnWeekVisibility; set { _BtnWeekVisibility = value; OnPropertyChanged("BtnWeekVisibility"); } }
        private Visibility _BtnMonthVisibility { get; set; }
        public Visibility BtnMonthVisibility { get => _BtnMonthVisibility; set { _BtnMonthVisibility = value; OnPropertyChanged("BtnMonthVisibility"); } }
        private Visibility _BtnQuarterVisibility { get; set; }
        public Visibility BtnQuarterVisibility { get => _BtnQuarterVisibility; set { _BtnQuarterVisibility = value; OnPropertyChanged("BtnQuarterVisibility"); } }
        private Visibility _BtnYearVisibility { get; set; }
        public Visibility BtnYearVisibility { get => _BtnYearVisibility; set { _BtnYearVisibility = value; OnPropertyChanged("BtnYearVisibility"); } }
        private Visibility _SearchVisibility { get; set; }
        public Visibility SearchVisibility { get => _SearchVisibility; set { _SearchVisibility = value; OnPropertyChanged("SearchVisibility"); } }
        private Visibility _BtnThreeYearVisibility { get; set; }
        public Visibility BtnThreeYearVisibility { get => _BtnThreeYearVisibility; set { _BtnThreeYearVisibility = value; OnPropertyChanged("BtnThreeYearVisibility"); } }
        private Visibility _BtnManyYearVisibility { get; set; }
        public Visibility BtnManyYearVisibility { get => _BtnManyYearVisibility; set { _BtnManyYearVisibility = value; OnPropertyChanged("BtnManyYearVisibility"); } }
        private Brush _BtnDayBackground { get; set; }
        public Brush BtnDayBackground { get => _BtnDayBackground; set { _BtnDayBackground = value; OnPropertyChanged("BtnDayBackground"); } }
        private Brush _BtnWeekBackground { get; set; }
        public Brush BtnWeekBackground { get => _BtnWeekBackground; set { _BtnWeekBackground = value; OnPropertyChanged("BtnWeekBackground"); } }
        private Brush _BtnMonthBackground { get; set; }
        public Brush BtnMonthBackground { get => _BtnMonthBackground; set { _BtnMonthBackground = value; OnPropertyChanged("BtnMonthBackground"); } }
        private Brush _BtnQuarterBackground { get; set; }
        public Brush BtnQuarterBackground { get => _BtnQuarterBackground; set { _BtnQuarterBackground = value; OnPropertyChanged("BtnQuarterBackground"); } }
        private Brush _BtnYearBackground { get; set; }
        public Brush BtnYearBackground { get => _BtnYearBackground; set { _BtnYearBackground = value; OnPropertyChanged("BtnYearBackground"); } }
        private Brush _BtnThreeYearBackground { get; set; }
        public Brush BtnThreeYearBackground { get => _BtnThreeYearBackground; set { _BtnThreeYearBackground = value; OnPropertyChanged("BtnThreeYearBackground"); } }
        private Brush _BtnManyYearBackground { get; set; }
        public Brush BtnManyYearBackground { get => _BtnManyYearBackground; set { _BtnManyYearBackground = value; OnPropertyChanged("BtnManyYearBackground"); } }
        private BasicModel _MonthItem { get; set; }
        public BasicModel MonthItem { get => _MonthItem; set { _MonthItem = value; OnPropertyChanged("MonthItem"); } }
        private BasicModel _YearItem { get; set; }
        public BasicModel YearItem { get => _YearItem; set { _YearItem = value; OnPropertyChanged("YearItem"); } }
        public Func<double, string> YFormatter { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PrintExcelCommand { get; set; }
        public ICommand BtnDayCommand { get; set; }
        public ICommand BtnWeekCommand { get; set; }
        public ICommand BtnMonthCommand { get; set; }
        public ICommand BtnQuarterCommand { get; set; }
        public ICommand BtnYearCommand { get; set; }
        public ICommand BtnThreeYearCommand { get; set; }
        public ICommand BtnManyYearCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand SelectionDayChangedCommand { get; set; }
        public ICommand SelectionWeekChangedCommand { get; set; }
        public ICommand SelectionMonthChangedCommand { get; set; }
        public ICommand SelectionYearChangedCommand { get; set; }
        public ICommand SelectionThreeYearChangedCommand { get; set; }
        public ICommand LoadedUCCommand { get; set; }
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }

        #region Dat
        private int _TotalPage;
        public int TotalPage { get => _TotalPage; set { _TotalPage = value; OnPropertyChanged("TotalPage"); UpdateEnableState(); } }
        private int _CurrentPage;
        public int CurrentPage { get => _CurrentPage; set { _CurrentPage = value; OnPropertyChanged("CurrentPage"); UpdateEnableState(); } }

        private bool _isNextEnabled;
        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged("IsNextEnabled"); }
        }
        private bool _isPreviousEnabled;
        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set { _isPreviousEnabled = value; OnPropertyChanged("IsPreviousEnabled"); }
        }
        private bool _isFirstEnabled;
        public bool IsFirstEnabled
        {
            get { return _isFirstEnabled; }
            set { _isFirstEnabled = value; OnPropertyChanged("IsFirstEnabled"); }
        }
        private bool _isLastEnabled;
        public bool IsLastEnabled
        {
            get { return _isLastEnabled; }
            set { _isLastEnabled = value; OnPropertyChanged("IsLastEnabled"); }
        }
        private void UpdateEnableState()
        {
            IsFirstEnabled = CurrentPage > 1;
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < TotalPage;
            IsLastEnabled = CurrentPage < TotalPage;
        }
        #endregion

        public ContentControl _MainUserControl;
        public long BranchId = 0;
        public int BrandId = 0;
        public int TypeReport = 0;
        private int Button = 0;
        public string FormDateString = "";
        public async void GetData(int brandId, string formDate, long branchId, int type, int page)
        {
            DialogHostOpen = true;
            ReportClient client = new ReportClient(this, this, this);
            GiftFoodResponse response = await Task.Run(()=> client.GetSynthesisReportGiftFood(brandId, formDate, branchId, type, page, (int)LimitEnum.DEFAULT));
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                {
                    if (ListData != null)
                    {
                        ListData.Clear();
                    }
                    else
                    {
                        ListData = new ObservableCollection<GiftFood>();
                    }
                    CurrentPage = page;
                    response.Data.List.ForEach(ListData.Add);
                    TotalRevenue = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                    TotalTime = response.Data.TotalTime.ToString();
                    TotalQuantity = response.Data.TotalQuantity.ToString();
                    if (response.Data.TotalRecord > 0)
                    {
                        if (response.Data.TotalRecord % response.Data.Limit != 0)
                        {
                            TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit) + 1;
                        }
                        else
                        {
                            TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit);
                        }
                        PageContent = string.Format("{0}/{1}", CurrentPage, TotalPage);
                    }
                    else
                    {
                        PageContent = string.Format("{0}/{1}", 1, 1);
                    }
                    DialogHostOpen = false;
                }
                else
                    DialogHostOpen = false;
            });
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
                    BtnDayVisibility = Visibility.Visible;
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnQuarterVisibility = Visibility.Visible;
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
                    MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_ONE_YEARS), currentUser.Permissions))
                {
                    BtnDayVisibility = Visibility.Visible;
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnQuarterVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
                    DateTime dateTime = DateTime.Now;
                    DisplayDateStart = new DateTime(dateTime.Year, 1, 1);
                    DisplayDateEnd = new DateTime(dateTime.Year, 12, 31);
                    MonthList.Clear();
                    for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                    {
                        MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
                    YearList.Clear();
                    YearList.Add(new BasicModel(DateTime.Now.Year, string.Format("Năm {0}", DateTime.Now.Year)));
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_YEARS), currentUser.Permissions))
                {
                    BtnDayVisibility = Visibility.Visible;
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnQuarterVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
                    BtnThreeYearVisibility = Visibility.Visible;
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
                        YearList.Add(new BasicModel(i.Year, string.Format("Năm {0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_MANY_YEARS), currentUser.Permissions))
                {
                    BtnDayVisibility = Visibility.Visible;
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnQuarterVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
                    BtnThreeYearVisibility = Visibility.Visible;
                    BtnManyYearVisibility = Visibility.Visible;
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
                        YearList.Add(new BasicModel(i.Year, string.Format("Năm {0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
            }
            else
            {
                BtnDayVisibility = Visibility.Visible;
                BtnWeekVisibility = Visibility.Visible;
                BtnMonthVisibility = Visibility.Visible;
                DateTime dateTime = DateTime.Now;
                DisplayDateStart = new DateTime(dateTime.Year, dateTime.Month, 1);
                DisplayDateEnd = Utils.Utils.GetLastDayOfMonth(dateTime);
                MonthList.Clear();
                for (DateTime i = DisplayDateEnd; i >= DisplayDateStart; i = i.AddMonths(-1))
                {
                    MonthList.Add(new BasicModel(int.Parse(string.Format("{0}{1}", i.Year, i.Month)), string.Format("Tháng {0}- Năm {1}", i.Month, i.Year)));
                }
                MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
            }
            BtnThreeYearVisibility = Visibility.Collapsed;
            BtnManyYearVisibility = Visibility.Collapsed;
        }
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
                DateTimeInput = DateTime.Now;
                FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Now);
                TypeReport = (int)ReportTypeEnum.HOURS_IN_DAY;
                GetData(BrandId,FormDateString, BranchId, TypeReport, Constants.OFFSET);
                Button = TypeReport;
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG NGÀY {0}", FormDateString);

                BtnDayVisibility = Visibility.Collapsed;
                BtnWeekVisibility = Visibility.Collapsed;
                BtnMonthVisibility = Visibility.Collapsed;
                BtnQuarterVisibility = Visibility.Collapsed;
                BtnYearVisibility = Visibility.Collapsed;
                BtnThreeYearVisibility = Visibility.Collapsed;
                BtnManyYearVisibility = Visibility.Collapsed;
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    BtnDayVisibility = Visibility.Visible;
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnQuarterVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
                    BtnThreeYearVisibility = Visibility.Collapsed;
                    BtnManyYearVisibility = Visibility.Collapsed;
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
                        YearList.Add(new BasicModel(i.Year, string.Format("Năm {0}", i.Year)));
                    }
                    MonthItem = MonthList.Where(x => x.Value == DateTime.Now.Month).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                }
                else
                {
                    CheckDateTimeReportPermission();
                }
                var bc = new System.Windows.Media.BrushConverter();
                BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
            }
        }
        public SettingData currentSetting;
        public ReportGiftFoodViewModel()
        {
            LoadedUCCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
                if (currentSetting.IsWorkingOffline)
                {
                    BtnMonthVisibility = Visibility.Collapsed;
                    BtnQuarterVisibility = Visibility.Collapsed;
                    BtnYearVisibility = Visibility.Collapsed;
                    BtnThreeYearVisibility = Visibility.Collapsed;
                    BtnManyYearVisibility = Visibility.Collapsed;
                }
                SearchVisibility = Visibility.Visible;
                _MainUserControl = p.FindName("SearchControl") as ContentControl;
                _MainUserControl.Content = new SearchDayUC();
                init();
            });
            PrintExcelCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = ReportContent;
                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = " Excel |*.xlsx| Excel 2003 |*.xls";

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
                        exp.Workbook.Properties.Title = ReportContent;
                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_CATEGORY);
                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];
                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_CATEGORY;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;
                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_TIME,
                                               MessageValue.MESSAGE_FROM_LIST_ORDER_CODE,
                                               MessageValue.MESSAGE_FROM_LIST_TABLE, MessageValue.MESSAGE_FROM_GIFT_EMPLOYEE,
                                               MessageValue.MESSAGE_FROM_GIFT_FOOD_STRING_NAME,
                                               MessageValue.MESSAGE_FROM_CREAT_BOOKING_PICRE,
                                               MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_SLOT,
                                               MessageValue.MESSAGE_FROM_CREAT_BOOKING_BY_MONEY
                         };
                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();
                        ws.Cells[1, 1].Value = ReportContent;
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
                        foreach (var item in ListData)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;
                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex].Value = item.PaymentDate;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.OrderId;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.TableName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Employee.Name;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Food.Name;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Price;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Quantity;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.TotalAmount;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            ws.Cells.AutoFitColumns();
                        }
                        rowIndex++;
                        ws.Cells[rowIndex, 1].Style.Font.Bold = true;
                        ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_CREAT_BOOKING_TOTAL;
                        ws.Cells[rowIndex, 5].Style.Font.Bold = true;
                        ws.Cells[rowIndex, 5].Value = TotalTime;
                        ws.Cells[rowIndex, 7].Style.Font.Bold = true;
                        ws.Cells[rowIndex, 7].Value = TotalQuantity;
                        ws.Cells[rowIndex, 8].Style.Font.Bold = true;
                        ws.Cells[rowIndex, 8].Value = decimal.Parse(TotalRevenue);
                        ws.Cells[rowIndex, 8].Style.Numberformat.Format = "#,###,##0";

                        ws.Cells.AutoFitColumns();
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
                }
            });

            // Chọn Thương Hiệu 
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
                    if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                    {
                        //string time = Utils.Utils.GetDateFormatVN(new DateTime(int.Parse(FormDateString), 01, 01));
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else 
                    {
                        GetData(BrandId, FormDateString, BranchId,TypeReport, CurrentPage); 
                    }
                }
            });
            // Chọn Chi Nhánh 
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;

                    if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                    {
                        //string time = Utils.Utils.GetDateFormatVN(new DateTime(int.Parse(FormDateString), 01, 01));
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                }
                
            });
            BtnDayCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.HOURS_IN_DAY;
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Visible;
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchDayUC();
                    FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Now);
                    DateTimeInput = DateTime.Now;
                    GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    Button = TypeReport;

                    ReportContent = string.Format("DANH SÁCH MÓN TẶNG NGÀY {0}", FormDateString);
                    var bc = new System.Windows.Media.BrushConverter();
                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }               
            });
            BtnWeekCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.DAYS_IN_WEEK;
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Visible;
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchWeekUC();
                    FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Now);

                    GetData(BrandId, FormDateString, BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;

                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG TUẦN TỪ NGÀY {0} ĐẾN {1}",
                      Utils.Utils.GetDateFormatVN(Utils.Utils.GetFirstDayOfWeek(DateTime.Now)),
                       Utils.Utils.GetDateFormatVN(Utils.Utils.LastDayOfWeek(DateTime.Now)));
                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }               
            });
            BtnMonthCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.DAYS_IN_MONTH;
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Visible;
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchMonthUC();
                    int month = int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month));
                    MonthItem = MonthList.Where(x => x.Value == month).FirstOrDefault();

                    FormDateString = MonthItem.Value.ToString();

                    int yearChoice = int.Parse(FormDateString.Substring(0, 4));
                    int monthChoice = int.Parse(FormDateString.Substring(4));
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(yearChoice, monthChoice, 01));
                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;

                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG THÁNG {0}/{1}", DateTime.Now.Month, DateTime.Now.Year);
                    var bc = new System.Windows.Media.BrushConverter();
                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }               
            });
            BtnQuarterCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                
                TypeReport = (int)ReportTypeEnum.NEAREST_THREE_MONTHS;
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Collapsed;

                    int month = int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month));
                    MonthItem = MonthList.Where(x => x.Value == month).FirstOrDefault();
                    FormDateString = MonthItem.Value.ToString();
                    int yearChoice = int.Parse(FormDateString.Substring(0, 4));
                    int monthChoice = int.Parse(FormDateString.Substring(4));
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(yearChoice, monthChoice, 01));

                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;

                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG 3 THÁNG GẦN NHẤT");

                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }               
            });
            BtnYearCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.MONTHS_IN_YEAR;
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Visible;
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchYearUC();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                        
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));

                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;

                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG NĂM {0}", YearItem.Value);

                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }
                
            });
            BtnThreeYearCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS;
                if (Button != TypeReport)
                {
                    if (!Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    {
                        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_MANY_YEARS), currentUser.Permissions)
                    && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_THREE_YEARS), currentUser.Permissions))
                        {
                            SearchVisibility = Visibility.Visible;
                            _MainUserControl = p.FindName("SearchControl") as ContentControl;
                            _MainUserControl.Content = new SearchThreeYearUC();
                        }
                        else
                        {
                            SearchVisibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        SearchVisibility = Visibility.Visible;
                        _MainUserControl = p.FindName("SearchControl") as ContentControl;
                        _MainUserControl.Content = new SearchThreeYearUC();
                    }
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                    FormDateString = YearItem.Value.ToString();
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(1, 1, YearItem.Value));
                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;
                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG 3 NĂM {0} - {1} - {2} ", YearItem.Value - 2, YearItem.Value - 1, YearItem.Value);

                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }                              
            });
            BtnManyYearCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                
                TypeReport = (int)ReportTypeEnum.ALL_YEAR;
                //  YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();
                // FormDateString = YearItem.Value.ToString();
                if (Button != TypeReport)
                {
                    SearchVisibility = Visibility.Collapsed;
                    GetData(BrandId, "", BranchId, TypeReport, Constants.OFFSET);
                    Button = TypeReport;

                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("DANH SÁCH  DOANH THU CỦA TẤT CẢ CÁC NĂM");

                    BtnDayBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnQuarterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnThreeYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));

                    BtnManyYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                }
                
            });
            SelectionDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetDateFormatVN(DateTimeInput);
                if (strDate != FormDateString && TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                {
                    FormDateString = Utils.Utils.GetDateFormatVN(p.SelectedDate.GetValueOrDefault());
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG NGÀY {0}", FormDateString);
                    GetData(BrandId, FormDateString, BranchId, TypeReport, Constants.OFFSET);
                }                               
            });
            SelectionWeekChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetWeek(DateTime.Now).ToString();
                if (FormDateString != strDate && TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                {
                    FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Parse(p.Text)).ToString();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG TUẦN TỪ NGÀY {0} ĐẾN {1}",
                       Utils.Utils.GetDateFormatVN(Utils.Utils.GetFirstDayOfWeek(DateTime.Parse(p.Text))),
                       Utils.Utils.GetDateFormatVN(Utils.Utils.LastDayOfWeek(DateTime.Parse(p.Text))));
                    GetData(BrandId, FormDateString, BranchId, TypeReport, Constants.OFFSET);
                }                    
            });
            SelectionMonthChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (MonthItem != null && FormDateString != MonthItem.Value.ToString() && TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                {
                    FormDateString = MonthItem.Value.ToString();
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG THÁNG {0}/{1}", month, year);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                }                   
            });
            SelectionYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (YearItem != null && FormDateString != YearItem.Value.ToString())
                {
                    FormDateString = YearItem.Value.ToString();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG NĂM {0}", FormDateString);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                }                    
            });
            SelectionThreeYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (YearItem != null && FormDateString != YearItem.Value.ToString())
                {
                    FormDateString = YearItem.Value.ToString();
                    ReportContent = string.Format("DANH SÁCH  MÓN TẶNG 3 NĂM {0} - {1} - {2} ", YearItem.Value - 2, YearItem.Value - 1, YearItem.Value);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                    GetData(BrandId, time, BranchId, TypeReport, Constants.OFFSET);
                }                   
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                {
                    GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                {
                    GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                {
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                {
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                {
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(int.Parse(FormDateString), 01, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                }
            });

            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                    {
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(int.Parse(FormDateString), 01, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                }

            });

            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                    {
                        GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                    {
                        string FromDate = FormDateString;
                        int year = int.Parse(FromDate.Substring(0, 4));
                        FromDate = FromDate.Remove(0, 4);
                        int month = int.Parse(FromDate);
                        string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                    else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                    {
                        string time =FormDateString;
                        GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                    }
                }

            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                if (TypeReport == (int)ReportTypeEnum.HOURS_IN_DAY)
                {
                    GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                {
                    GetData(BrandId, FormDateString, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.DAYS_IN_MONTH)
                {
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                {
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
                }
                else if (TypeReport == (int)ReportTypeEnum.MONTHS_IN_YEAR || TypeReport == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                {
                    string time = Utils.Utils.GetDateFormatVN(new DateTime(int.Parse(FormDateString), 01, 01));
                    GetData(BrandId, time, BranchId, TypeReport, CurrentPage);
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

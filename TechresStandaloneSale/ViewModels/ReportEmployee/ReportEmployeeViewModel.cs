using DevExpress.Mvvm.Native;
using LiveCharts;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts.Wpf;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView.ReportRevenue;
using TechresStandaloneSale.Views.Dialogs;
using static TechresStandaloneSale.Models.Response.EmployeeReport;
using System.Threading.Tasks;

namespace TechresStandaloneSale.ViewModels.ReportEmployee
{
    public class ReportEmployeeViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        private ObservableCollection<NumberEmployeesByAge> _ListAgeData = new ObservableCollection<NumberEmployeesByAge>();
        public ObservableCollection<NumberEmployeesByAge> ListAgeData { get => _ListAgeData; set { _ListAgeData = value; OnPropertyChanged("ListAgeData"); } }
        private ObservableCollection<NumberEmployeesByRole> _ListRoleData = new ObservableCollection<NumberEmployeesByRole>();
        public ObservableCollection<NumberEmployeesByRole> ListRoleData { get => _ListRoleData; set { _ListRoleData = value; OnPropertyChanged("ListRoleData"); } }
        private ObservableCollection<NumberEmployeesByGender> _ListGenderData = new ObservableCollection<NumberEmployeesByGender>();
        public ObservableCollection<NumberEmployeesByGender> ListGenderData { get => _ListGenderData; set { _ListGenderData = value; OnPropertyChanged("ListGenderData"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }

        public float _NumberEmployees { get; set; }
        public float NumberEmployees { get => _NumberEmployees; set { _NumberEmployees = value; OnPropertyChanged("NumberEmployees"); } }
        public float _NumberFemaleEmployees { get; set; }
        public float NumberFemaleEmployees { get => _NumberFemaleEmployees; set { _NumberFemaleEmployees = value; OnPropertyChanged("NumberFemaleEmployees"); } }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }

        private ObservableCollection<BasicModel> _MonthList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> MonthList { get => _MonthList; set { _MonthList = value; OnPropertyChanged("MonthList"); } }

        private ObservableCollection<BasicModel> _YearList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> YearList { get => _YearList; set { _YearList = value; OnPropertyChanged("YearList"); } }
        private DateTime _DateTimeInput { get; set; }
        public DateTime DateTimeInput { get => _DateTimeInput; set { _DateTimeInput = value; OnPropertyChanged("DateTimeInput"); } }
        private DateTime _DisplayDateStart { get; set; }
        public DateTime DisplayDateStart { get => _DisplayDateStart; set { _DisplayDateStart = value; OnPropertyChanged("DisplayDateStart"); } }

        private DateTime _DisplayDateEnd { get; set; }
        public DateTime DisplayDateEnd { get => _DisplayDateEnd; set { _DisplayDateEnd = value; OnPropertyChanged("DisplayDateEnd"); } }

        private string _ReportContent { get; set; }
        public string ReportContent { get => _ReportContent; set { _ReportContent = value; OnPropertyChanged("ReportContent"); } }

        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
       
        private SeriesCollection _DataChartValue;
        public SeriesCollection DataChartValue { get => _DataChartValue; set { _DataChartValue = value; OnPropertyChanged("DataChartValue"); } }
        private SeriesCollection _DataChartRoleValue;
        public SeriesCollection DataChartRoleValue { get => _DataChartRoleValue; set { _DataChartRoleValue = value; OnPropertyChanged("DataChartRoleValue"); } }
        private SeriesCollection _DataChartGenderValue;
        public SeriesCollection DataChartGenderValue { get => _DataChartGenderValue; set { _DataChartGenderValue = value; OnPropertyChanged("DataChartGenderValue"); } }

        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        
        private Visibility _BtnWeekVisibility { get; set; }
        public Visibility BtnWeekVisibility { get => _BtnWeekVisibility; set { _BtnWeekVisibility = value; OnPropertyChanged("BtnWeekVisibility"); } }
        private Visibility _BtnMonthVisibility { get; set; }
        public Visibility BtnMonthVisibility { get => _BtnMonthVisibility; set { _BtnMonthVisibility = value; OnPropertyChanged("BtnMonthVisibility"); } }
        private Visibility _BtnQuarterVisibility { get; set; }
        public Visibility BtnQuarterVisibility { get => _BtnQuarterVisibility; set { _BtnQuarterVisibility = value; OnPropertyChanged("BtnQuarterVisibility"); } }
        private Visibility _BtnYearVisibility { get; set; }
        public Visibility BtnYearVisibility { get => _BtnYearVisibility; set { _BtnYearVisibility = value; OnPropertyChanged("BtnYearVisibility"); } }
        
        private Brush _BtnWeekForeground { get; set; }
        public Brush BtnWeekForeground { get => _BtnWeekForeground; set { _BtnWeekForeground = value; OnPropertyChanged("BtnWeekForeground"); } }

        private Brush _BtnMonthForeground { get; set; }
        public Brush BtnMonthForeground { get => _BtnMonthForeground; set { _BtnMonthForeground = value; OnPropertyChanged("BtnMonthForeground"); } }

        private Brush _BtnYearForeground { get; set; }
        public Brush BtnYearForeground { get => _BtnYearForeground; set { _BtnYearForeground = value; OnPropertyChanged("BtnYearForeground"); } }
        
        private Brush _BtnWeekBackground { get; set; }
        public Brush BtnWeekBackground { get => _BtnWeekBackground; set { _BtnWeekBackground = value; OnPropertyChanged("BtnWeekBackground"); } }

        private Brush _BtnMonthBackground { get; set; }
        public Brush BtnMonthBackground { get => _BtnMonthBackground; set { _BtnMonthBackground = value; OnPropertyChanged("BtnMonthBackground"); } }

        private Brush _BtnQuarterBackground { get; set; }
        public Brush BtnQuarterBackground { get => _BtnQuarterBackground; set { _BtnQuarterBackground = value; OnPropertyChanged("BtnQuarterBackground"); } }

        private Brush _BtnYearBackground { get; set; }
        public Brush BtnYearBackground { get => _BtnYearBackground; set { _BtnYearBackground = value; OnPropertyChanged("BtnYearBackground"); } }

        private Brush _BtnWeekBorderBrush { get; set; }
        public Brush BtnWeekBorderBrush { get => _BtnWeekBorderBrush; set { _BtnWeekBorderBrush = value; OnPropertyChanged("BtnWeekBorderBrush"); } }

        private Brush _BtnMonthBorderBrush { get; set; }
        public Brush BtnMonthBorderBrush { get => _BtnMonthBorderBrush; set { _BtnMonthBorderBrush = value; OnPropertyChanged("BtnMonthBorderBrush"); } }

        private Brush _BtnYearBorderBrush { get; set; }
        public Brush BtnYearBorderBrush { get => _BtnYearBorderBrush; set { _BtnYearBorderBrush = value; OnPropertyChanged("BtnYearBorderBrush"); } }
        
        private BasicModel _MonthItem { get; set; }
        public BasicModel MonthItem { get => _MonthItem; set { _MonthItem = value; OnPropertyChanged("MonthItem"); } }

        private BasicModel _YearItem { get; set; }
        public BasicModel YearItem { get => _YearItem; set { _YearItem = value; OnPropertyChanged("YearItem"); } }
        public Func<double, string> YFormatter { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
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
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand BtnWeekCommand { get; set; }
        public ICommand BtnMonthCommand { get; set; }
        public ICommand BtnYearCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionWeekChangedCommand { get; set; }
        public ICommand SelectionMonthChangedCommand { get; set; }
        public ICommand SelectionYearChangedCommand { get; set; }
        public ICommand LoadedUCCommand { get; set; }

        public ContentControl _MainUserControl;
        public int BrandId = 0;
        public long BranchId = 0;
        private int Button = 0;
        public int TypeReport = 0;
        public string FormDateString = "";
        public string time = "";
        public  void GetData(int brandId, string formDate, long branchId, int type)
        {           
            DialogHostOpen = true;
            ReportClient client = new ReportClient(this, this, this);
            ReportEmployeeResponse response = client.GetStatistic(brandId, formDate, branchId, type);
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Count > 0)
                {
                    if (ListAgeData == null) { ListAgeData = new ObservableCollection<NumberEmployeesByAge>(); } else { ListAgeData.Clear(); }
                    if (ListRoleData == null) { ListRoleData = new ObservableCollection<NumberEmployeesByRole>(); } else { ListRoleData.Clear(); }
                    if (ListGenderData == null) { ListGenderData = new ObservableCollection<NumberEmployeesByGender>(); } else { ListGenderData.Clear(); }
                    if (DataChartValue == null) { DataChartValue = new SeriesCollection(); } else { DataChartValue.Clear(); }
                    if (DataChartRoleValue == null) { DataChartRoleValue = new SeriesCollection(); } else { DataChartRoleValue.Clear(); }
                    if (DataChartGenderValue == null) { DataChartGenderValue = new SeriesCollection(); } else { DataChartGenderValue.Clear(); }
                    EmployeeReport Response = response.Data[0];
                    for (int i = 0; i < Response.NumberEmployeesByAge.Count(); i++)
                    {
                        ListAgeData.Add(new NumberEmployeesByAge
                        {
                            stt = i + 1,
                            Age = Response.NumberEmployeesByAge[i].Age,
                            Number = Response.NumberEmployeesByAge[i].Number,
                            percent = (Response.NumberEmployeesByAge[i].Number / Response.NumberEmployees).ToString("0.00%"),
                        });
                    }
                    for (int i = 0; i < Response.NumberEmployeesByRole.Count(); i++)
                    {
                        ListRoleData.Add(new NumberEmployeesByRole
                        {
                            stt = i + 1,
                            RoleName = Response.NumberEmployeesByRole[i].RoleName,
                            RoleId = Response.NumberEmployeesByRole[i].RoleId,
                            Number = Response.NumberEmployeesByRole[i].Number,
                            percent = (Response.NumberEmployeesByRole[i].Number / Response.NumberEmployees).ToString("0.00%"),
                        });
                    }
                    foreach (var v in Response.NumberEmployeesByAge)
                    {
                        Func<ChartPoint, string> labelPoint = chartPoint =>
                            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                        DataChartValue.Add(new PieSeries
                        {
                            Title = "Tuổi " + v.Age.ToString(),
                            Values = new ChartValues<double> { (double)v.Number },
                            DataLabels = true,
                            //LabelPoint = labelPoint
                        });
                    }
                    foreach (var v in Response.NumberEmployeesByRole)
                    {
                        Func<ChartPoint, string> labelPoint = chartPoint =>
                            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                        DataChartRoleValue.Add(new PieSeries
                        {
                            Title = v.RoleName.ToString(),
                            Values = new ChartValues<double> { (double)v.Number },
                            DataLabels = true,
                            //LabelPoint = labelPoint
                        });
                    }
                    {
                        Func<ChartPoint, string> labelPoint = chartPoint =>
                            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                        DataChartGenderValue.Add(new PieSeries
                        {
                            Title = "Nam",
                            Values = new ChartValues<double> { (double)response.Data[0].NumberMaleEmployees },
                            DataLabels = true,
                            LabelPoint = labelPoint
                        });
                        DataChartGenderValue.Add(new PieSeries
                        {
                            Title = "Nữ",
                            Values = new ChartValues<double> { (double)response.Data[0].NumberFemaleEmployees },
                            DataLabels = true,
                            LabelPoint = labelPoint
                        });
                    }
                    NumberEmployees = response.Data[0].NumberEmployees;
                    //NumberFemaleEmployees = response.ConfigData[0].NumberMaleEmployees;
                    ListGenderData.Add(new NumberEmployeesByGender
                    {
                        stt = 1,
                        Gender = "Nữ",
                        Number = response.Data[0].NumberFemaleEmployees,
                        //percent = (float)Math.Round((double)(100 * response.ConfigData[0].NumberFemaleEmployees) / Response.NumberEmployees),
                        percent = (Response.NumberFemaleEmployees / Response.NumberEmployees).ToString("0.00%"),
                    });
                    ListGenderData.Add(new NumberEmployeesByGender
                    {
                        stt = 2,
                        Gender = "Nam",
                        Number = response.Data[0].NumberMaleEmployees,
                        //percent = (float)Math.Round((double)(100 * response.ConfigData[0].NumberMaleEmployees) / Response.NumberEmployees),
                        percent = (Response.NumberMaleEmployees / Response.NumberEmployees).ToString("0.00%"),
                    });
                    DialogHostOpen = false;
                }
                else
                    DialogHostOpen = false;
            });
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
                    BranchVisibility = Visibility.Collapsed;

                }
                else if (currentUser.UserManagerId == (int)UserManagerEnum.BRAND)
                {
                    BranchVisibility = Visibility.Visible;
                    BrandVisibility = Visibility.Collapsed;
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
                TypeReport = (int)ReportTypeEnum.DAYS_IN_WEEK;
                FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Now);
                time = Utils.Utils.GetDateFormatVN(DateTime.Now);
                GetData(BrandId,time, BranchId, TypeReport);
                Button = TypeReport;
                int week = Utils.Utils.GetWeek(DateTime.Now);
                //var bc = new System.Windows.Media.BrushConverter();
                ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO TUẦN TỪ NGÀY {0} ĐẾN {1}", Utils.Utils.GetDateFormatVN(Utils.Utils.FirstDateOfWeek(DateTime.Now.Year, week)), Utils.Utils.GetDateFormatVN(Utils.Utils.LastDateOfWeek(DateTime.Now.Year, week)));
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {

                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
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
                    MonthItem = MonthItem = MonthList.Where(x => x.Value == int.Parse(string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month))).FirstOrDefault();
                    YearItem = YearList.Where(x => x.Value == DateTime.Now.Year).FirstOrDefault();

                }
                else
                {
                    CheckDateTimeReportPermission();
                }

                var bc = new System.Windows.Media.BrushConverter();

                BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
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
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
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
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
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
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;
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
                    BtnWeekVisibility = Visibility.Visible;
                    BtnMonthVisibility = Visibility.Visible;
                    BtnYearVisibility = Visibility.Visible;

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
        }
        public ReportEmployeeViewModel()
        {
            LoadedUCCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                _MainUserControl = p.FindName("SearchControl") as ContentControl;
                _MainUserControl.Content = new SearchWeekUC();
                init();
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
                    GetData(BrandId, FormDateString, BranchId, TypeReport);
                }
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id) )
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                    GetData(BrandId, FormDateString, BranchId, TypeReport);
                }
            });

            BtnWeekCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.DAYS_IN_WEEK;
                if (Button != TypeReport)
                {
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchWeekUC();
                    Button = TypeReport;
                    FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Now);
                    GetData(BrandId, FormDateString, BranchId, TypeReport);
                    int week = Utils.Utils.GetWeek(DateTime.Now);
                    var bc = new System.Windows.Media.BrushConverter();
                    ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO TUẦN TỪ NGÀY {0} ĐẾN {1}", Utils.Utils.GetDateFormatVN(Utils.Utils.FirstDateOfWeek(DateTime.Now.Year, week)), Utils.Utils.GetDateFormatVN(Utils.Utils.LastDateOfWeek(DateTime.Now.Year, week)));


                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }                
            });
            BtnMonthCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {      
                TypeReport = (int)ReportTypeEnum.DAYS_IN_MONTH;
                if (Button != TypeReport)
                {
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchMonthUC();
                    int yearChoice = int.Parse(MonthItem.Value.ToString().Substring(0, 4));
                    int monthChoice = int.Parse(MonthItem.Value.ToString().Substring(4));
                    FormDateString = MonthItem.Value.ToString();
                    time = Utils.Utils.GetDateFormatVN(new DateTime(yearChoice, monthChoice, 01));
                    Button = TypeReport;
                    GetData(BrandId, time, BranchId, TypeReport);
                    ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO THÁNG {0}/{1}", monthChoice, yearChoice);
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                }                
            });
            BtnYearCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                TypeReport = (int)ReportTypeEnum.MONTHS_IN_YEAR;
                
                if(Button != TypeReport)
                {
                    _MainUserControl = p.FindName("SearchControl") as ContentControl;
                    _MainUserControl.Content = new SearchYearUC();
                    Button = TypeReport;
                    FormDateString = YearItem.Value.ToString();
                    time = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                    GetData(BrandId, time, BranchId, TypeReport);
                    ReportContent = string.Format("BÁO CÁO ĐƠN HÀNG NĂM THEO NĂM {0}", YearItem.Value);
                    BtnWeekBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnMonthBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                    BtnYearBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                }                
            });

            SelectionWeekChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetDateFormatVN(DateTime.Parse(p.Text)).ToString();
                if (FormDateString != strDate && TypeReport == (int)ReportTypeEnum.DAYS_IN_WEEK)
                {
                    FormDateString = Utils.Utils.GetDateFormatVN(DateTime.Parse(p.Text)).ToString();
                    DateTime form = Utils.Utils.GetFirstDayOfWeek(DateTime.Parse(p.Text));
                    DateTime to = Utils.Utils.LastDayOfWeek(DateTime.Parse(p.Text));
                    ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO TUẦN TỪ NGÀY {0} ĐẾN {1}", Utils.Utils.GetDateFormatVN(form), Utils.Utils.GetDateFormatVN(to));
                   GetData(BrandId, FormDateString, BranchId, TypeReport);
                }                    
            });
            SelectionMonthChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (MonthItem != null && FormDateString != MonthItem.Value.ToString())
                {
                    FormDateString = MonthItem.Value.ToString();
                    string FromDate = FormDateString;
                    int year = int.Parse(FromDate.Substring(0, 4));
                    FromDate = FromDate.Remove(0, 4);
                    int month = int.Parse(FromDate);
                    ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO THÁNG {0}/{1}", month, year);
                    DateTime From = new DateTime(year, month, 01);
                    time = Utils.Utils.GetDateFormatVN(From);
                   GetData(BrandId, time, BranchId, TypeReport);
                }                    
            });
            SelectionYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (YearItem != null && FormDateString != YearItem.Value.ToString())
                {
                    FormDateString = YearItem.Value.ToString();
                    DateTime From = new DateTime(int.Parse(FormDateString), 01, 01);
                    ReportContent = string.Format("BÁO CÁO THỐNG KÊ NHÂN SỰ THEO NĂM {0}", YearItem.Value);
                    time = Utils.Utils.GetDateFormatVN(From);
                   GetData(BrandId, time, BranchId, TypeReport);
                }                    
            });
        }
        public void LogError(Exception ex, string infoMessage)
        {
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
            return default;
        }
    }

}

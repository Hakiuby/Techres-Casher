using DevExpress.Mvvm.Native;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using TechresStandaloneSale.UserControlView.ReportDiscount;
using TechresStandaloneSale.UserControlView.ReportOrder;
using TechresStandaloneSale.UserControlView.ReportRevenue;
using TechresStandaloneSale.UserControlView.ReportRevenueArea;
using TechresStandaloneSale.UserControlView.ReportRevenueOrderMethod;
using TechresStandaloneSale.UserControlView.ReportRevenuePaymentMethod;
using TechresStandaloneSale.UserControlView.ReportRevenueRankEmployee;
using TechresStandaloneSale.UserControlView.ReportSysthesisRevenue;
using TechresStandaloneSale.UserControlView.ReportVAT;

namespace TechresStandaloneSale.ViewModels
{
    public class ReportRevenueViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand LoadedUCCommand { get; set; }
        public ICommand ReportSysthesisRevenueCommand { get; set; }
        public ICommand ReportPaymentMethodCommand { get; set; }
        public ICommand ReportRevenueRankEmployeeCommand { get; set; }
        public ICommand ReportRevenueAreaCommand { get; set; }
        public ICommand ReportVATCommand { get; set; }
        public ICommand ReportDiscountCommand { get; set; }
        public ICommand ReportOrderCommand { get; set; }
        public ICommand ReportOrderMethodCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedTimeCommand { get; set; }
        public ICommand SelectionDayChangedCommand { get; set; }
        public ICommand SelectionWeekChangedCommand { get; set; }
        public ICommand SelectionMonthChangedCommand { get; set; }
        public ICommand SelectionYearChangedCommand { get; set; }
        public ICommand SelectionThreeYearChangedCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        private ObservableCollection<Chart> _ListData = new ObservableCollection<Chart>();
        public ObservableCollection<Chart> ListData { get => _ListData; set { _ListData = value; OnPropertyChanged("ListData"); } }
        private ObservableCollection<Order> _OrderList = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged("OrderList"); } }

        private ObservableCollection<RevenueSysthesisReportData> _RevenueSysthesisReport = new ObservableCollection<RevenueSysthesisReportData>();
        public ObservableCollection<RevenueSysthesisReportData> RevenueSysthesisReport { get => _RevenueSysthesisReport; set { _RevenueSysthesisReport = value; OnPropertyChanged("RevenueSysthesisReport"); } }
        private Brush _ReportSysthesisRevenueBG { get; set; }
        public Brush ReportSysthesisRevenueBG { get => _ReportSysthesisRevenueBG; set { _ReportSysthesisRevenueBG = value; OnPropertyChanged("ReportSysthesisRevenueBG"); } }
        private Brush _ReportPaymentMethodBG { get; set; }
        public Brush ReportPaymentMethodBG { get => _ReportPaymentMethodBG; set { _ReportPaymentMethodBG = value; OnPropertyChanged("ReportPaymentMethodBG"); } }
        private Brush _ReportRevenueRankEmployeeBG { get; set; }
        public Brush ReportRevenueRankEmployeeBG { get => _ReportRevenueRankEmployeeBG; set { _ReportRevenueRankEmployeeBG = value; OnPropertyChanged("ReportRevenueRankEmployeeBG"); } }
        private Brush _ReportRevenueAreaBG { get; set; }
        public Brush ReportRevenueAreaBG { get => _ReportRevenueAreaBG; set { _ReportRevenueAreaBG = value; OnPropertyChanged("ReportRevenueAreaBG"); } }
        private Brush _ReportVATBG { get; set; }
        public Brush ReportVATBG { get => _ReportVATBG; set { _ReportVATBG = value; OnPropertyChanged("ReportVATBG"); } }
        private Brush _ReportDiscountBG { get; set; }
        public Brush ReportDiscountBG { get => _ReportDiscountBG; set { _ReportDiscountBG = value; OnPropertyChanged("ReportDiscountBG"); } }
        private Brush _ReportOrderBG { get; set; }
        public Brush ReportOrderBG { get => _ReportOrderBG; set { _ReportOrderBG = value; OnPropertyChanged("ReportOrderBG"); } }
        private Brush _ReportOrderMethodBG { get; set; }
        public Brush ReportOrderMethodBG { get => _ReportOrderMethodBG; set { _ReportOrderMethodBG = value; OnPropertyChanged("ReportOrderMethodBG"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private ObservableCollection<BasicModel> _TimesList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> TimesList { get => _TimesList; set { _TimesList = value; OnPropertyChanged("TimesList"); } }
        private BasicModel _TimeItem { get; set; }
        public BasicModel TimeItem { get => _TimeItem; set { _TimeItem = value; OnPropertyChanged("TimeItem"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private Visibility _DoubleChartVisibility { get; set; }
        public Visibility DoubleChartVisibility { get => _DoubleChartVisibility; set { _DoubleChartVisibility = value; OnPropertyChanged("DoubleChartVisibility"); } }
        private Visibility _OneChartVisibility { get; set; }
        public Visibility OneChartVisibility
        { get => _OneChartVisibility; set { _OneChartVisibility = value; OnPropertyChanged("OneChartVisibility"); } }
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
        private string _STTChoice { get; set; }
        public string STTChoice { get => _STTChoice; set { _STTChoice = value; OnPropertyChanged("STTChoice"); } }
        private string _SumOder { get; set; }
        public string SumOder { get => _SumOder; set { _SumOder = value; OnPropertyChanged("SumOder"); } }
        private string _SumRevenue { get; set; }
        public string SumRevenue { get => _SumRevenue; set { _SumRevenue = value; OnPropertyChanged("SumRevenue"); } }
        private string _SumGift { get; set; }
        public string SumGift { get => _SumGift; set { _SumGift = value; OnPropertyChanged("SumGift"); } }
        private string _SumDiscount { get; set; }
        public string SumDiscount { get => _SumDiscount; set { _SumDiscount = value; OnPropertyChanged("SumDiscount"); } }
        private string _SumVAT { get; set; }
        public string SumVAT { get => _SumVAT; set { _SumVAT = value; OnPropertyChanged("SumVAT"); } }
        private string _SumRevenuePay { get; set; }
        public string SumRevenuePay { get => _SumRevenuePay; set { _SumRevenuePay = value; OnPropertyChanged("SumRevenuePay"); } }
        private string _SumBankAmount { get; set; }
        public string SumBankAmount { get => _SumBankAmount; set { _SumBankAmount = value; OnPropertyChanged("SumBankAmount"); } }
        private string _SumCastAmount { get; set; }
        public string SumCastAmount { get => _SumCastAmount; set { _SumCastAmount = value; OnPropertyChanged("SumCastAmount"); } }
        private string _SumTransferAmount { get; set; }
        public string SumTransferAmount { get => _SumTransferAmount; set { _SumTransferAmount = value; OnPropertyChanged("SumTransferAmount"); } }
        private string _SumMembershipPointUsed { get; set; }
        public string SumMembershipPointUsed { get => _SumMembershipPointUsed; set { _SumMembershipPointUsed = value; OnPropertyChanged("SumMembershipPointUsed"); } }
        private string _SumMembershipAloPointUsed { get; set; }
        public string SumMembershipAloPointUsed { get => _SumMembershipAloPointUsed; set { _SumMembershipAloPointUsed = value; OnPropertyChanged("SumMembershipAloPointUsed"); } }
        private string _SumMembershipPromotionPointUsed { get; set; }
        public string SumMembershipPromotionPointUsed { get => _SumMembershipPromotionPointUsed; set { _SumMembershipPromotionPointUsed = value; OnPropertyChanged("SumMembershipPromotionPointUsed"); } }
        private string _SumMembershipAccumulatePointUsed { get; set; }
        public string SumMembershipAccumulatePointUsed { get => _SumMembershipAccumulatePointUsed; set { _SumMembershipAccumulatePointUsed = value; OnPropertyChanged("SumMembershipAccumulatePointUsed"); } }
        private string _SumMembershipTotalPointUsed { get; set; }
        public string SumMembershipTotalPointUsed { get => _SumMembershipTotalPointUsed; set { _SumMembershipTotalPointUsed = value; OnPropertyChanged("SumMembershipTotalPointUsed"); } }
        private string _SumSysthesis { get; set; }
        public string SumSysthesis { get => _SumSysthesis; set { _SumSysthesis = value; OnPropertyChanged("SumSysthesis"); } }
        private string _ReportContent { get; set; }
        public string ReportContent { get => _ReportContent; set { _ReportContent = value; OnPropertyChanged("ReportContent"); } }
        private string _TotalVAT { get; set; }
        public string TotalVAT { get => _TotalVAT; set { _TotalVAT = value; OnPropertyChanged("TotalVAT"); } }

        public Func<double, string> YFormatter { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public Func<double, string> Formatter { get; set; }
        public ChartValues<string> _LabelData = new ChartValues<string>();
        public ChartValues<string> LabelData
        {
            get { return _LabelData; }
            set { _LabelData = value; OnPropertyChanged("LabelData"); }
        }
        public ChartValues<decimal> _DataChartValue = new ChartValues<decimal>();
        public ChartValues<decimal> DataChartValue
        {
            get { return _DataChartValue; }
            set { _DataChartValue = value; OnPropertyChanged("DataChartValue"); }
        }
        private double _xPointer;
        public double XPointer
        {
            get { return _xPointer; }
            set
            {
                _xPointer = value;
                OnPropertyChanged("XPointer");
            }
        }
        private double _yPointer;
        public double YPointer
        {
            get { return _yPointer; }
            set
            {
                _yPointer = value;
                OnPropertyChanged("YPointer");
            }
        }
        private ObservableCollection<PaymentMethodModel> _TotalRevenuePaymentMethod = new ObservableCollection<PaymentMethodModel>();
        public ObservableCollection<PaymentMethodModel> TotalRevenuePaymentMethod { get => _TotalRevenuePaymentMethod; set { _TotalRevenuePaymentMethod = value; OnPropertyChanged("TotalRevenuePaymentMethod"); } }
        private ObservableCollection<FormService> _ToltalRevenueOrderMethod = new ObservableCollection<FormService>();
        public ObservableCollection<FormService> ToltalRevenueOrderMethod { get => _ToltalRevenueOrderMethod; set { _ToltalRevenueOrderMethod = value; OnPropertyChanged("ToltalRevenueOrderMethod"); } }

        private SeriesCollection _DataPieChartValue;
        public SeriesCollection DataPieChartValue { get => _DataPieChartValue; set { _DataPieChartValue = value; OnPropertyChanged("DataPieChartValue"); } }

        private ObservableCollection<EmployeeMainAdmin> _EmployeeMainAdmin = new ObservableCollection<EmployeeMainAdmin>();
        public ObservableCollection<EmployeeMainAdmin> RevenueReportRankEmployee { get => _EmployeeMainAdmin; set { _EmployeeMainAdmin = value; OnPropertyChanged("EmployeeMainAdmin"); } }

        private ObservableCollection<ReportRevenueAreas> _RevenueAreaReport = new ObservableCollection<ReportRevenueAreas>();
        public ObservableCollection<ReportRevenueAreas> RevenueAreaReport { get => _RevenueAreaReport; set { _RevenueAreaReport = value; OnPropertyChanged("RevenueAreaReport"); } }
        private string _TotalAmount { get; set; }
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }

        private string _TotalOrder { get; set; }
        public string TotalOrder { get => _TotalOrder; set { _TotalOrder = value; OnPropertyChanged("TotalOrder"); } }

        private string _TotalDiscount { get; set; }
        public string TotalDiscount { get => _TotalDiscount; set { _TotalDiscount = value; OnPropertyChanged("TotalDiscount"); } }
        private string _TotalRevenue { get; set; }
        public string TotalRevenue { get => _TotalRevenue; set { _TotalRevenue = value; OnPropertyChanged("TotalRevenue"); } }
        private long _TotalArea { get; set; }
        public long TotalArea { get => _TotalArea; set { _TotalArea = value; OnPropertyChanged("TotalArea"); } }
        #region toan

        private int _widthButton;
        public int widthButton { get => _widthButton; set { _widthButton = value; OnPropertyChanged("widthButton"); } }


        private int _fontsizeButton;
        public int fontsizeButton { get => _fontsizeButton; set { _fontsizeButton = value; OnPropertyChanged("fontsizeButton"); } }
        
        #endregion
        private Visibility _OrderVisibility { get; set; }
        public Visibility OrderVisibility { get => _OrderVisibility; set { _OrderVisibility = value; OnPropertyChanged("OrderVisibility"); } }
        private Visibility _OrderMethodVisibility { get; set; }
        public Visibility OrderMethodVisibility { get => _OrderMethodVisibility; set { _OrderMethodVisibility = value; OnPropertyChanged("OrderMethodVisibility"); } }

        private Visibility _DiscountVisibility { get; set; }
        public Visibility DiscountVisibility { get => _DiscountVisibility; set { _DiscountVisibility = value; OnPropertyChanged("DiscountVisibility"); } }

        private Visibility _VatVisibility { get; set; }
        public Visibility VatVisibility { get => _VatVisibility; set { _VatVisibility = value; OnPropertyChanged("VatVisibility"); } }

        private Visibility _RevenueAreaVisibility { get; set; }
        public Visibility RevenueAreaVisibility { get => _RevenueAreaVisibility; set { _RevenueAreaVisibility = value; OnPropertyChanged("RevenueAreaVisibility"); } }

        private Visibility _RankEmployeeVisibility { get; set; }
        public Visibility RankEmployeeVisibility { get => _RankEmployeeVisibility; set { _RankEmployeeVisibility = value; OnPropertyChanged("RankEmployeeVisibility"); } }

        private Visibility _PaymentMethodVisibility { get; set; }
        public Visibility PaymentMethodVisibility { get => _PaymentMethodVisibility; set { _PaymentMethodVisibility = value; OnPropertyChanged("PaymentMethodVisibility"); } }

        private Visibility _RevenueVisibility { get; set; }
        public Visibility RevenueVisibility { get => _RevenueVisibility; set { _RevenueVisibility = value; OnPropertyChanged("RevenueVisibility"); } }

        private string _Total { get; set; }
        public string Total { get => _Total; set { _Total = value; OnPropertyChanged("Total"); } }
        public ContentControl _MainUserControl;
        public ContentControl _SearchUserControl;
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        public long BranchId = 0;
        public int BrandId = 0;
        public int type = 0;
        private int CurrentPage;
        private int TotalPage;
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
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
        private ReportSysthesisRevenueUC reportSysthesisRevenueUC;
        private ReportOrderUC reportOrderUC;
        private ReportRevenueAreaUC reportRevenueAreaUC;
        private ReportRevenueOrderMethodUC reportRevenueOrderMethodUC;
        private ReportRevenuePaymentMethodUC reportRevenuePaymentMethodUC;
        private ReportRevenueRankEmployeeUC reportRevenueRankEmployeeUC;
        private ReportVATUC reportVATUC;
        private ReportDiscountUC reportDiscountUC;
       
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
            //TimesList.Add(Week);            
            if (Properties.Settings.Default.IsOffline)
            {
                TimesList.Add(Month);
                TimesList.Add(Quater);
                TimesList.Add(Year);
                TimesList.Add(ThreeYear);
                TimesList.Add(ManyYear);
            }
        }
        public void ResetContent(ReportRevenueButtonEnum currentButton, string text)
        {
            switch (currentButton)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                        ReportContent = "BÁO CÁO DOANH THU ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        ReportContent = "BÁO CÁO DOANH THU THEO HÌNH THỨC THANH TOÁN ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        ReportContent = string.Format("TOP 10 NHÂN VIÊN CÓ DOANH THU CAO NHẤT ");
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        ReportContent = "BÁO CÁO DOANH THU THEO KHU VỰC ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        ReportContent = "BÁO CÁO VAT ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        ReportContent = "BÁO CÁO GIẢM GIÁ ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        ReportContent = "BÁO CÁO DOANH THU THEO ĐƠN HÀNG ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        ReportContent = "BÁO CÁO DOANH THU THEO PHƯƠNG THỨC PHỤC VỤ ";
                        ReportContent = string.Concat(ReportContent, text);
                        break;
                    }
            }
        }
        public void ResetColorBG(ReportRevenueButtonEnum currentButton)
        {
            switch (currentButton)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        ReportSysthesisRevenueBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportPaymentMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueRankEmployeeBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportRevenueAreaBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportVATBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportDiscountBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.WAVE_COLOR));
                        ReportOrderMethodBG = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                        break;
                    }
            }
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
                    OrderVisibility = Visibility.Visible;
                    RevenueAreaVisibility = Visibility.Visible;
                    RevenueVisibility = Visibility.Visible;
                    RankEmployeeVisibility = Visibility.Visible;
                    VatVisibility = Visibility.Visible;
                    DiscountVisibility = Visibility.Visible;
                    OrderMethodVisibility = Visibility.Visible;
                    PaymentMethodVisibility = Visibility.Visible;
                }
                else
                {
                    CheckDateTimeReportPermission();

                    OrderVisibility = Visibility.Collapsed;
                    RevenueAreaVisibility = Visibility.Collapsed;
                    RevenueVisibility = Visibility.Collapsed;
                    RankEmployeeVisibility = Visibility.Collapsed;
                    VatVisibility = Visibility.Collapsed;
                    DiscountVisibility = Visibility.Collapsed;
                    OrderMethodVisibility = Visibility.Collapsed;
                    PaymentMethodVisibility = Visibility.Collapsed;
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        OrderVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE), currentUser.Permissions))
                    {
                        RevenueAreaVisibility = Visibility.Visible;
                        RevenueVisibility = Visibility.Visible;
                        RankEmployeeVisibility = Visibility.Visible;
                        OrderMethodVisibility = Visibility.Visible;
                        PaymentMethodVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT), currentUser.Permissions))
                    {
                        DiscountVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT), currentUser.Permissions))
                    {
                        VatVisibility = Visibility.Visible;
                    }
                }


                //GetReportContent();                
            }
        }

        public void GetData(int brandId, string FromDate, long branchId, int type, int page, ReportRevenueButtonEnum currentButton)
        {
            switch (currentButton)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        RevenueSysthesisReportResponse response = client.GetRevenueSysthesis(brandId, FromDate, branchId, type);
                        if (response != null && response.Status == (int)ResponseEnum.OK && response.Data.Detail != null)
                        {
                            if (RevenueSysthesisReport == null)
                            {
                                RevenueSysthesisReport = new ObservableCollection<RevenueSysthesisReportData>();
                            }
                            else
                            {
                                RevenueSysthesisReport.Clear();
                            }
                            SumOder = Utils.Utils.FormatMoney(response.Data.TotalOrder);
                            SumRevenue = Utils.Utils.FormatMoney(response.Data.TotalSaleRevenue);
                            SumGift = Utils.Utils.FormatMoney(response.Data.TotalGift);
                            SumDiscount = Utils.Utils.FormatMoney(response.Data.TotalDiscount);
                            SumVAT = Utils.Utils.FormatMoney(response.Data.TotalVat);
                            SumRevenuePay = Utils.Utils.FormatMoney(response.Data.TotalRevenue);
                            SumBankAmount = Utils.Utils.FormatMoney(response.Data.BankAmount);
                            SumCastAmount = Utils.Utils.FormatMoney(response.Data.CashAmount);
                            SumTransferAmount = Utils.Utils.FormatMoney(response.Data.TransferAmount);                            
                            SumSysthesis = Utils.Utils.FormatMoney(response.Data.RevenueExcludeVat);

                            SumMembershipPointUsed = Utils.Utils.FormatMoney(response.Data.MembershipPointUsed);
                            SumMembershipAloPointUsed = Utils.Utils.FormatMoney(response.Data.MembershipAloPointUsed);
                            SumMembershipPromotionPointUsed = Utils.Utils.FormatMoney(response.Data.MembershipPromotionPointUsed);
                            SumMembershipAccumulatePointUsed = Utils.Utils.FormatMoney(response.Data.MembershipAccumulatePointUsed);
                            SumMembershipTotalPointUsed = Utils.Utils.FormatMoney(response.Data.MembershipTotalPointUsed);

                            DataChartValue = new ChartValues<decimal>();
                            LabelData.Clear();
                            if (type == (int)ReportTypeEnum.HOURS_IN_DAY)
                            {
                                ReportContent = string.Format("BÁO CÁO DOANH THU NGÀY {0}", FromDate);
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "GIỜ";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail)
                                {
                                    if (c.TotalRevenue > 0)
                                    {
                                        int index = int.Parse(c.Index);
                                        if (index == 23)
                                        {
                                            c.IndexString = string.Format("{0}:00 - {1}:00", c.Index, 0);
                                        }
                                        else
                                        {
                                            c.IndexString = string.Format("{0}:00 - {1}:00", index, index + 1);
                                        }
                                        RevenueSysthesisReport.Add(c);
                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("{0}:00", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.DAYS_IN_WEEK)
                            {
                                ReportContent = string.Format("BÁO CÁO DOANH THU TUẦN ");
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "THỨ";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail)
                                {
                                    if (c.TotalRevenue > 0)
                                    {

                                        if (c.Index.Contains("1"))
                                        {
                                            RevenueSysthesisReport.Add(new RevenueSysthesisReportData
                                            {
                                                TotalDiscount = c.TotalDiscount,
                                                TotalGift = c.TotalGift,
                                                TotalRevenue = c.TotalRevenue,
                                                TotalOrder = c.TotalOrder,
                                                RevenueExcludeVat = c.RevenueExcludeVat,
                                                TotalSaleRevenue = c.TotalSaleRevenue,
                                                TotalVat = c.TotalVat,
                                                BankAmount = c.BankAmount,
                                                CashAmount = c.CashAmount,
                                                Index = "CN",
                                            });
                                        }
                                        else
                                        {
                                            c.IndexString = string.Format("Thứ {0}", c.Index);
                                            RevenueSysthesisReport.Add(c);
                                        }

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Thứ {0}", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.DAYS_IN_MONTH)
                            {
                                DateTime date = Utils.Utils.GetStringFormatDate(FromDate);
                                ReportContent = string.Format("BÁO CÁO DOANH THU THÁNG {0}/{1}", date.Month, date.Year);
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "NGÀY";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail.Where(x => x.TotalRevenue > 0).ToList())
                                {

                                    if (c.TotalRevenue > 0)
                                    {
                                        c.IndexString = string.Format("Ngày {0}", c.Index);
                                        RevenueSysthesisReport.Add(c);

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Ngày {0}", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                            {
                                ReportContent = string.Format("BÁO CÁO DOANH THU 3 THÁNG GẦN NHẤT");
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "THÁNG";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail.Where(x => x.TotalRevenue > 0).ToList())
                                {

                                    if (c.TotalRevenue > 0)
                                    {
                                        c.IndexString = string.Format("Tháng {0}", c.Index);
                                        RevenueSysthesisReport.Add(c);

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Tháng {0}", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.MONTHS_IN_YEAR)
                            {
                                DateTime date = Utils.Utils.GetStringFormatDate(FromDate);
                                ReportContent = string.Format("BÁO CÁO DOANH THU NĂM {0}", date.Year);
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "THÁNG";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail.Where(x => x.TotalRevenue > 0).ToList())
                                {

                                    if (c.TotalRevenue > 0)
                                    {
                                        c.IndexString = string.Format("Tháng {0}", c.Index);
                                        RevenueSysthesisReport.Add(c);

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Tháng {0}", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                            {
                                DateTime date = Utils.Utils.GetStringFormatDate(FromDate);
                                ReportContent = string.Format("BÁO CÁO DOANH THU 3 NĂM {0} - {1} - {2} ", date.Year - 2, date.Year - 1, date.Year);
                                OneChartVisibility = Visibility.Visible;
                                DoubleChartVisibility = Visibility.Collapsed;
                                STTChoice = "THÁNG - NĂM";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail.Where(x => x.TotalRevenue > 0).ToList())
                                {

                                    if (c.TotalRevenue > 0)
                                    {
                                        c.IndexString = string.Format("Tháng {0}", c.Index);
                                        RevenueSysthesisReport.Add(c);

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Tháng {0}", c.Index));
                                }
                            }
                            else if (type == (int)ReportTypeEnum.ALL_YEAR)
                            {
                                ReportContent = string.Format("BÁO CÁO DOANH THU CỦA TẤT CẢ CÁC NĂM");
                                OneChartVisibility = Visibility.Collapsed;
                                DoubleChartVisibility = Visibility.Visible;
                                STTChoice = "NĂM";
                                foreach (RevenueSysthesisReportData c in response.Data.Detail.Where(x => x.TotalRevenue > 0).ToList())
                                {
                                    if (c.TotalRevenue > 0)
                                    {
                                        c.IndexString = string.Format("Năm {0}", c.Index);
                                        RevenueSysthesisReport.Add(c);

                                    }
                                    DataChartValue.Add(c.RevenueExcludeVat);
                                    LabelData.Add(string.Format("Năm {0}", c.Index));
                                }
                            }
                            YFormatter = value => Utils.Utils.FormatMoney(value);
                            XPointer = -5;
                            YPointer = 0;
                            PointLabel = chartPoint => Utils.Utils.FormatMoney(chartPoint.Y);
                            Formatter = x => Utils.Utils.FormatMoney(x);
                            DialogHostOpen = false;
                        }
                        else DialogHostOpen = false;
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        RevenuePaymentMethodResponse reportResponse = client.GetRevenuePaymentMethodResponse(brandId, FromDate, branchId, type);
                        if (reportResponse != null && reportResponse.Status == (int)ResponseEnum.OK && reportResponse.Data != null)
                        {
                            if (TotalRevenuePaymentMethod == null)
                            {
                                TotalRevenuePaymentMethod = new ObservableCollection<PaymentMethodModel>();
                            }
                            else
                            {
                                TotalRevenuePaymentMethod.Clear();
                            }
                            Total = Utils.Utils.FormatMoney(reportResponse.Data.CashAmount + reportResponse.Data.BankAmount + reportResponse.Data.TransferAmount + reportResponse.Data.TotalPointUsedAmount);
                           
                                DataPieChartValue = new SeriesCollection();
                            DataChartValue = new ChartValues<decimal>();
                            LabelData.Clear();

                            DataChartValue.Add(reportResponse.Data.CashAmount);
                            LabelData.Add(MessageValue.MESSAGE_FROM_CASH_AMOUNT);
                            Func<ChartPoint, string> labelPoint = chartPoint =>
                                string.Format("{0:P}", chartPoint.Participation);
                            PieSeries pieSeries = new PieSeries
                            {
                                Title = MessageValue.MESSAGE_FROM_CASH_AMOUNT,
                                Values = new ChartValues<double> { (double)reportResponse.Data.CashAmount },
                                DataLabels = true,
                                LabelPoint = labelPoint,
                            };
                            DataPieChartValue.Add(pieSeries);


                            TotalRevenuePaymentMethod.Add(new PaymentMethodModel(MessageValue.MESSAGE_FROM_CASH_AMOUNT, "VNĐ", Utils.Utils.FormatMoney(reportResponse.Data.CashAmount)));

                            DataChartValue.Add(reportResponse.Data.BankAmount);
                            LabelData.Add(MessageValue.MESSAGE_FROM_BANK_AMOUNT);
                            DataPieChartValue.Add(new PieSeries
                            {
                                Title = MessageValue.MESSAGE_FROM_BANK_AMOUNT,
                                Values = new ChartValues<double> { (double)reportResponse.Data.BankAmount },
                                DataLabels = true,
                                LabelPoint = labelPoint,
                            });
                            TotalRevenuePaymentMethod.Add(new PaymentMethodModel(MessageValue.MESSAGE_FROM_BANK_AMOUNT, "VNĐ", Utils.Utils.FormatMoney(reportResponse.Data.BankAmount)));

                            DataChartValue.Add(reportResponse.Data.TransferAmount);
                            LabelData.Add(MessageValue.MESSAGE_FROM_TRANSFER_AMOUNT);
                            DataPieChartValue.Add(new PieSeries
                            {
                                Title = MessageValue.MESSAGE_FROM_TRANSFER_AMOUNT,
                                Values = new ChartValues<double> { (double)reportResponse.Data.TransferAmount },
                                DataLabels = true,
                                LabelPoint = labelPoint,
                            });
                            TotalRevenuePaymentMethod.Add(new PaymentMethodModel(MessageValue.MESSAGE_FROM_TRANSFER_AMOUNT, "VNĐ", Utils.Utils.FormatMoney(reportResponse.Data.TransferAmount)));

                            DataChartValue.Add(reportResponse.Data.TotalPointUsedAmount);
                            LabelData.Add(MessageValue.MESSAGE_FROM_POINT_USED);
                            DataPieChartValue.Add(new PieSeries
                            {
                                Title = MessageValue.MESSAGE_FROM_POINT_USED,
                                Values = new ChartValues<double> { (double)reportResponse.Data.TotalPointUsedAmount },
                                DataLabels = true,
                                LabelPoint = labelPoint,
                            });
                            TotalRevenuePaymentMethod.Add(new PaymentMethodModel(MessageValue.MESSAGE_FROM_POINT_USED, reportResponse.Data.TotalPointUsed + MessageValue.MESSAGE_FROM_POINT, Utils.Utils.FormatMoney(reportResponse.Data.TotalPointUsedAmount)));

                            Formatter = value => Utils.Utils.FormatMoney(value);
                            XPointer = -5;
                            YPointer = -5;
                            PointLabel = chartPoint => Utils.Utils.FormatMoney(chartPoint.Y);
                            DialogHostOpen = false;
                        }
                        else{
                            DialogHostOpen = false;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        EmployeeMainAdminResponse reportResponse = client.GetEmployeeMainAdminResponse(brandId, FromDate, branchId, type, (int)LimitEnum.ZERO);
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (reportResponse != null && reportResponse.Status == (int)ResponseEnum.OK && reportResponse.Data != null)
                            {
                                if (RevenueReportRankEmployee == null)
                                {
                                    RevenueReportRankEmployee = new ObservableCollection<EmployeeMainAdmin>();
                                }
                                else if (RevenueReportRankEmployee.Count > 0) { RevenueReportRankEmployee.Clear(); }
                                if (DataPieChartValue == null) { DataPieChartValue = new SeriesCollection(); } else { DataPieChartValue.Clear(); }
                                DataChartValue = new ChartValues<decimal>();
                                LabelData.Clear();
                                int i = 1;
                                foreach (EmployeeMainAdmin c in reportResponse.Data.List)
                                {
                                    RevenueReportRankEmployee.Add(c);
                                    if (i <= 10)
                                    {
                                        i++;
                                        DataChartValue.Add(c.Revenue);
                                        LabelData.Add(c.Employee.Name);
                                        Func<ChartPoint, string> labelPoint = chartPoint =>
                                        string.Format("{0:P}", chartPoint.Participation);
                                        DataPieChartValue.Add(new PieSeries
                                        {
                                            Title = c.Employee.Name,
                                            Values = new ChartValues<double> { (double)c.Revenue },
                                            DataLabels = true,
                                            LabelPoint = labelPoint,
                                        });
                                    }
                                }
                                Formatter = value => Utils.Utils.FormatMoney(value);
                                XPointer = -5;
                                YPointer = -5;
                                PointLabel = chartPoint => Utils.Utils.FormatMoney(chartPoint.Y);
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        RevenueAreaReportResponse response = client.GetRevenueArea(brandId, FromDate, branchId, type);
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data.ReportData != null)
                            {
                                if (type == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                                {
                                    OneChartVisibility = Visibility.Visible;
                                    DoubleChartVisibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                }
                                if (RevenueAreaReport == null)
                                {
                                    RevenueAreaReport = new ObservableCollection<ReportRevenueAreas>();
                                }
                                else if (RevenueAreaReport.Count > 0) { RevenueAreaReport.Clear(); }
                                int i = 1;
                                DataChartValue = new ChartValues<decimal>();
                                LabelData.Clear();
                                TotalArea = RevenueAreaReport.Count();
                                TotalRevenue = Utils.Utils.FormatMoney(response.Data.TotalRevenue);
                                foreach (ReportRevenueAreas c in response.Data.ReportData)
                                {
                                    if (c.Revenue > 0)
                                    {
                                        c.No = i;
                                        i++;
                                        RevenueAreaReport.Add(c);
                                    }
                                    DataChartValue.Add(c.Revenue);
                                    LabelData.Add(string.Format("{0}", c.AreaName));
                                }
                                YFormatter = value => Utils.Utils.FormatMoney(value);
                                XPointer = -5;
                                YPointer = -5;
                                PointLabel = chartPoint => Utils.Utils.FormatMoney(chartPoint.Y);
                                Formatter = x => Utils.Utils.FormatMoney(x);
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        ChartResponse response = client.GetSynthesisReportVAT(brandId, FromDate, branchId, type);
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                if (ListData == null)
                                {
                                    ListData = new ObservableCollection<Chart>();
                                }
                                else
                                {
                                    ListData.Clear();
                                }
                                if (DataChartValue == null)
                                {
                                    DataChartValue = new ChartValues<decimal>();
                                }
                                else
                                {
                                    DataChartValue.Clear();
                                }
                                if (LabelData == null)
                                {
                                    LabelData = new ChartValues<string>();
                                }
                                else
                                {
                                    LabelData.Clear();
                                }
                                TotalVAT = Utils.Utils.FormatMoney(response.Data.Total);
                                if (type == (int)ReportTypeEnum.HOURS_IN_DAY)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("{0}:00", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.DAYS_IN_WEEK)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Ngày {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.DAYS_IN_MONTH)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Ngày {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.MONTHS_IN_YEAR)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {

                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                                {
                                    OneChartVisibility = Visibility.Visible;
                                    DoubleChartVisibility = Visibility.Collapsed;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.ALL_YEAR)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Năm {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.ALL_MONTHS)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail)
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                    }
                                }
                                YFormatter = value => value.ToString("N0");
                                XPointer = -5;
                                YPointer = -5;
                                PointLabel = chartPoint => chartPoint.Y != 0 ? Utils.Utils.FormatMoney(chartPoint.Y) : "";
                                Formatter = x => Utils.Utils.FormatMoney(x);

                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        ChartResponse response = client.GetSynthesisReportDiscount(brandId, FromDate, branchId, type);
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                if (ListData == null)
                                {
                                    ListData = new ObservableCollection<Chart>();
                                }
                                else if (ListData.Count > 0) { ListData.Clear(); }
                                DataChartValue = new ChartValues<decimal>();
                                Total = Utils.Utils.FormatMoney(response.Data.Total);
                                //Total = string.Format("{0:0,0}", response.Data.Total) + " VNĐ";
                                LabelData.Clear();
                                if (type == (int)ReportTypeEnum.HOURS_IN_DAY)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("{0}:00", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("{0}:00", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.DAYS_IN_WEEK)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Ngày {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Ngày {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.DAYS_IN_MONTH)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Ngày {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Ngày {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Tháng {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.MONTHS_IN_YEAR)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Tháng {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                                {
                                    OneChartVisibility = Visibility.Visible;
                                    DoubleChartVisibility = Visibility.Collapsed;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Tháng {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Tháng {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.ALL_YEAR)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Năm {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Năm {0}", c.CreatedAt));
                                    }
                                }
                                else if (type == (int)ReportTypeEnum.ALL_MONTHS)
                                {
                                    OneChartVisibility = Visibility.Collapsed;
                                    DoubleChartVisibility = Visibility.Visible;
                                    foreach (Chart c in response.Data.Detail.Where(x => x.Value > 0).ToList())
                                    {
                                        c.CreatedAt = string.Format("Năm {0}", c.CreatedAt);
                                        if (c.Value > 0)
                                        {
                                            ListData.Add(c);
                                        }
                                        DataChartValue.Add(c.Value);
                                        LabelData.Add(string.Format("Năm {0}", c.CreatedAt));
                                    }
                                }
                                YFormatter = value => value.ToString("N0");
                                XPointer = -5;
                                YPointer = -5;
                                PointLabel = chartPoint => chartPoint.Y != 0 ? Utils.Utils.FormatMoney(chartPoint.Y) : "";
                                Formatter = x => Utils.Utils.FormatMoney(x);

                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        SysthesisReportOrdersResponse response = client.GetSynthesisReportOrder(brandId, FromDate, branchId, type, page);
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                            {
                                if (OrderList == null)
                                {
                                    OrderList = new ObservableCollection<Order>();
                                }
                                else if (OrderList.Count > 0) { OrderList.Clear(); }
                                response.Data.Orders.ForEach(OrderList.Add);
                                CurrentPage = page;
                                TotalOrder = response.Data.TotalRecord.ToString();
                                TotalVAT = Utils.Utils.FormatMoney(response.Data.VatAmount);
                                TotalDiscount = Utils.Utils.FormatMoney(response.Data.DiscountAmount);
                                TotalRevenue = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                                if (response.Data.TotalRecord % response.Data.Limit != 0)
                                {
                                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit) + 1;
                                }
                                else
                                {
                                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit);
                                }
                                PageContent = CurrentPage + "/" + (TotalPage==0 ?1: TotalPage);
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        });
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        ReportClient client = new ReportClient(this, this, this);
                        FormServiceResponse reportResponse = client.GetFormServiceResponse(brandId, FromDate, branchId, type);
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (reportResponse != null && reportResponse.Status == (int)ResponseEnum.OK && reportResponse.Data != null)
                            {
                                TotalOrder = Utils.Utils.FormatMoney(reportResponse.Data.TotalNumberOrder);
                                TotalAmount = Utils.Utils.FormatMoney(reportResponse.Data.SumAmount);
                                if (ToltalRevenueOrderMethod == null)
                                {
                                    ToltalRevenueOrderMethod = new ObservableCollection<FormService>();
                                }
                                else if (ToltalRevenueOrderMethod.Count > 0) { ToltalRevenueOrderMethod.Clear(); }
                                DataPieChartValue = new SeriesCollection();
                                DataChartValue = new ChartValues<decimal>();
                                LabelData.Clear();
                                int i = 1;
                                foreach (FormService c in reportResponse.Data.Details)
                                {
                                    ToltalRevenueOrderMethod.Add(c);
                                    if (i <= 10)
                                    {
                                        i++;
                                        DataChartValue.Add(c.TotalAmount);
                                        LabelData.Add(c.Name);
                                        Func<ChartPoint, string> labelPoint = chartPoint =>
                                        string.Format("{0:P}", chartPoint.Participation);
                                        DataPieChartValue.Add(new PieSeries
                                        {
                                            Title = c.Name,
                                            Values = new ChartValues<double> { (double)c.RevenueRate },
                                            DataLabels = true,
                                            LabelPoint = labelPoint,
                                        });
                                    }
                                }
                                Formatter = value => Utils.Utils.FormatMoney(value);
                                XPointer = -5;
                                YPointer = -5;
                                PointLabel = chartPoint => Utils.Utils.FormatMoney(chartPoint.Y);
                                DialogHostOpen = false;
                            }
                            else { DialogHostOpen = false; }
                        });
                        break;
                    }
            }

        }
        private string GetReportContent()
        {
           
            if (TimeItem.Value == (int)ReportTypeEnum.HOURS_IN_DAY)
            {
                _SearchUserControl.Visibility = Visibility.Visible;
                _SearchUserControl.Content = new SearchDayUC();
                return string.Format("NGÀY {0}", datetime);
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_WEEK)
            {
                _SearchUserControl.Visibility = Visibility.Visible;
                _SearchUserControl.Content = new SearchWeekUC();
                return string.Format("TUẦN");
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_MONTH)
            {
                _SearchUserControl.Visibility = Visibility.Visible;
                _SearchUserControl.Content = new SearchMonthUC();
                DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                return string.Format("THÁNG {0}/{1}", date.Month, date.Year);
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
            {

                _SearchUserControl.Visibility = Visibility.Collapsed;
                return string.Format("3 THÁNG GẦN NHẤT");
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR)
            {
                _SearchUserControl.Visibility = Visibility.Visible;
                _SearchUserControl.Content = new SearchYearUC();
                DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                return string.Format("NĂM {0}", date.Year);
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
            {
                _SearchUserControl.Visibility = Visibility.Visible;
                _SearchUserControl.Content = new SearchThreeYearUC();
                DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                return string.Format("3 NĂM {0} - {1} - {2} ", date.Year - 2, date.Year - 1, date.Year);
            }
            else if (TimeItem.Value == (int)ReportTypeEnum.ALL_YEAR)
            {
                _SearchUserControl.Visibility = Visibility.Collapsed;
                return string.Format("TẤT CẢ CÁC NĂM");
            }
            else
            {
                return string.Empty;
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
        private ReportRevenueButtonEnum ButtonReport;
        private string datetime = Utils.Utils.GetDateFormatVN(DateTime.Now);
        public bool isLoad = false;
        public ReportRevenueViewModel()
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
                    widthButton = 280;
                    fontsizeButton = 17;
                }
                else
                {
                    widthButton = 200;
                    fontsizeButton = 13;

                }
                #endregion
                if (!isLoad)
                {
                    DialogHostOpen = true;
                    type = (int)ReportTypeEnum.HOURS_IN_DAY;
                    _SearchUserControl = p.FindName("SearchControl") as ContentControl;
                    CurrentPage = 1;
                    DateTimeInput = DateTime.Now;
                    _MainUserControl = p.FindName("ReportControl") as ContentControl;
                    init();
                    if (currentSetting.IsWorkingOffline)
                    {
                        RankEmployeeVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        RankEmployeeVisibility = Visibility.Visible;
                    }

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_REVENUE), currentUser.Permissions)||
                    Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    {
                        if (reportSysthesisRevenueUC == null)
                        {
                            reportSysthesisRevenueUC = new ReportSysthesisRevenueUC();
                        }
                        _MainUserControl.Content = reportSysthesisRevenueUC;
                        ButtonReport = ReportRevenueButtonEnum.REVENUE;
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                       
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_VAT), currentUser.Permissions))
                    {
                        ButtonReport = ReportRevenueButtonEnum.VAT;
                        if (reportVATUC == null)
                        {
                            reportVATUC = new ReportVATUC();
                        }
                        _MainUserControl.Content = reportVATUC;
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.REPORT_DISCOUNT), currentUser.Permissions))
                    {
                        ButtonReport = ReportRevenueButtonEnum.DISCOUNT;

                        if (reportDiscountUC == null)
                        {
                            reportDiscountUC = new ReportDiscountUC();
                        }
                        _MainUserControl.Content = reportDiscountUC;
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                    }else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDERS_MANAGER), currentUser.Permissions))
                    {
                        ButtonReport = ReportRevenueButtonEnum.ORDER;
                        if (reportOrderUC == null)
                        {
                            reportOrderUC = new ReportOrderUC();
                        }
                        _MainUserControl.Content = reportOrderUC;
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                    }
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    isLoad = true;
                }
              
            });
            ReportSysthesisRevenueCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.REVENUE)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.REVENUE;
                    if (reportSysthesisRevenueUC == null)
                    {
                        reportSysthesisRevenueUC = new ReportSysthesisRevenueUC();
                    }
                    _MainUserControl.Content = reportSysthesisRevenueUC;
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportPaymentMethodCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.PAYMENT_METHOD)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.PAYMENT_METHOD;
                
                    if (reportRevenuePaymentMethodUC == null)
                    {
                        reportRevenuePaymentMethodUC = new ReportRevenuePaymentMethodUC();
                    }
                    _MainUserControl.Content = reportRevenuePaymentMethodUC;
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportRevenueRankEmployeeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.RANK_EMPLOYEE)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.RANK_EMPLOYEE;
                    if (reportRevenueRankEmployeeUC == null)
                    {
                        reportRevenueRankEmployeeUC = new ReportRevenueRankEmployeeUC();
                    }
                    _MainUserControl.Content = reportRevenueRankEmployeeUC;
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportRevenueAreaCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.AREA)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.AREA;
                    if (reportRevenueAreaUC == null)
                    {
                        reportRevenueAreaUC = new ReportRevenueAreaUC();
                    }
                    _MainUserControl.Content = reportRevenueAreaUC;
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }

            });
            ReportVATCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.VAT)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.VAT;
                   
                    if (reportVATUC == null)
                    {
                        reportVATUC = new ReportVATUC();
                    }
                    _MainUserControl.Content = reportVATUC;
                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportDiscountCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.DISCOUNT)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.DISCOUNT;
                   
                    if (reportDiscountUC == null)
                    {
                        reportDiscountUC = new ReportDiscountUC();
                    }
                    _MainUserControl.Content = reportDiscountUC;

                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportOrderMethodCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.ORDER_METHOD)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.ORDER_METHOD;
                
                    if (reportRevenueOrderMethodUC == null)
                    {
                        reportRevenueOrderMethodUC = new ReportRevenueOrderMethodUC();
                    }
                    _MainUserControl.Content = reportRevenueOrderMethodUC;

                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            ReportOrderCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (ButtonReport != ReportRevenueButtonEnum.ORDER)
                {
                    DialogHostOpen = true;
                    ButtonReport = ReportRevenueButtonEnum.ORDER;
                    if (reportOrderUC == null)
                    {
                        reportOrderUC = new ReportOrderUC();
                    }
                    _MainUserControl.Content = reportOrderUC;

                    ResetColorBG(ButtonReport);
                    ResetContent(ButtonReport, GetReportContent());
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
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
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    DialogHostOpen = false;
                });
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                    SelectionChanged();
                }
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    DialogHostOpen = false;
                });
            });
            SelectionChangedTimeCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (TimeItem!= null)
                {
                    if (TimeItem.Value == (int)ReportTypeEnum.HOURS_IN_DAY)
                    {
                        _SearchUserControl.Visibility = Visibility.Visible;
                        _SearchUserControl.Content = new SearchDayUC();
                        // text = string.Format("NGÀY {0}", datetime);

                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_WEEK)
                    {
                        _SearchUserControl.Visibility = Visibility.Visible;
                        _SearchUserControl.Content = new SearchWeekUC();
                        //text = string.Format("TUẦN");
                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.DAYS_IN_MONTH)
                    {
                        _SearchUserControl.Visibility = Visibility.Visible;
                        _SearchUserControl.Content = new SearchMonthUC();
                        //DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                        // text = string.Format("THÁNG {0}/{1}", date.Month, date.Year);
                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.NEAREST_THREE_MONTHS)
                    {
                        _SearchUserControl.Visibility = Visibility.Collapsed;
                        // text = string.Format("3 THÁNG GẦN NHẤT");
                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_YEAR)
                    {
                        _SearchUserControl.Visibility = Visibility.Visible;
                        _SearchUserControl.Content = new SearchYearUC();
                        //DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                        //  text = string.Format("NĂM {0}", date.Year);
                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.MONTHS_IN_THREE_YEARS)
                    {
                        _SearchUserControl.Visibility = Visibility.Visible;
                        _SearchUserControl.Content = new SearchThreeYearUC();
                        // DateTime date = Utils.Utils.GetStringFormatDate(datetime);
                        // text = string.Format("3 NĂM {0} - {1} - {2} ", date.Year - 2, date.Year - 1, date.Year);
                    }
                    else if (TimeItem.Value == (int)ReportTypeEnum.ALL_YEAR)
                    {
                        _SearchUserControl.Visibility = Visibility.Collapsed;
                        //text = string.Format("TẤT CẢ CÁC NĂM");
                    }
                }
                else
                {
                    _SearchUserControl.Visibility = Visibility.Visible;
                    _SearchUserControl.Content = new SearchDayUC();
                    // text = string.Format("NGÀY {0}", datetime);

                }

                type = TimeItem != null ? TimeItem.Value : (int)ReportTypeEnum.HOURS_IN_DAY;
                SelectionChanged();
            });
            SelectionDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetDateFormatVN(DateTimeInput);

                if (!datetime.Contains(strDate))
                {
                    datetime = strDate;
                    SelectionChanged();
                }
              
            });
            SelectionWeekChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetWeek(DateTime.Parse(p.Text)).ToString();
                if (!datetime.Contains(strDate))
                {
                    datetime = strDate;
                    SelectionChanged();
                }
            });
            SelectionMonthChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {

                string FromDate = MonthItem.Value.ToString();
                int year = int.Parse(FromDate.Substring(0, 4));
                FromDate = FromDate.Remove(0, 4);
                int month = int.Parse(FromDate);
                string strDate = Utils.Utils.GetDateFormatVN(new DateTime(year, month, 01));
                if (!datetime.Contains(strDate))
                {
                    datetime = strDate;
                    SelectionChanged();
                }
            });
            SelectionYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                if (!datetime.Contains(strDate))
                {
                    datetime = strDate;
                    SelectionChanged();
                }
            });
            SelectionThreeYearChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                string strDate = Utils.Utils.GetDateFormatVN(new DateTime(YearItem.Value, 01, 01));
                if (!datetime.Contains(strDate))
                {
                    datetime = strDate;
                    SelectionChanged();
                }
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                if (ListData != null) ListData.Clear();
                GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
            });
            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    if (ListData != null) ListData.Clear();
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    if (ListData != null) ListData.Clear();
                    GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                }
            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                if (ListData != null) ListData.Clear();
                GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
            });
        }

        
        private void SelectionChanged()
        {
            switch (ButtonReport)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                       // ButtonReport = ReportRevenueButtonEnum.REVENUE;
                        //_MainUserControl.Content = new ReportSysthesisRevenueUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.AREA;
                        //_MainUserControl.Content = new ReportRevenueAreaUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.DISCOUNT;
                        //_MainUserControl.Content = new ReportDiscountUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.ORDER;
                        //_MainUserControl.Content = new ReportOrderUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.ORDER_METHOD;
                        //_MainUserControl.Content = new ReportRevenueOrderMethodUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.PAYMENT_METHOD;
                        //_MainUserControl.Content = new ReportRevenuePaymentMethodUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.RANK_EMPLOYEE;
                        //_MainUserControl.Content = new ReportRevenueRankEmployeeUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        //ButtonReport = ReportRevenueButtonEnum.VAT;
                        //_MainUserControl.Content = new ReportVATUC();
                        //ResetColorBG(ButtonReport);
                        //ResetContent(ButtonReport, GetReportContent());
                        GetData(BrandId, datetime, BranchId, type, CurrentPage, ButtonReport);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
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

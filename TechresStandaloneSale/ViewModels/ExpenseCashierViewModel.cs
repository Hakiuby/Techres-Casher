using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.ViewModels.Warehouse;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ExpenseCashierViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        public ICommand AddPaymentSlipCommand { get; set; }
        public ICommand AddReceiptsCommand { get; set; }
        public ICommand AdditionFeeCancelCommand { get; set; }
        public ICommand AdditionFeeApproveCommand { get; set; }
        public ICommand AdditionFeeReturnCommand { get; set; }
        public ICommand AdditionFeeViewCommand { get; set; }
        public ICommand AdditionFeeEditCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedFromCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        
        public ICommand TypeCouponSelectionChangedCommand { get; set; }
        private DateTime _DateTimeDisplayDateStart { get; set; }
        public DateTime DateTimeDisplayDateStart { get => _DateTimeDisplayDateStart; set { _DateTimeDisplayDateStart = value; OnPropertyChanged("DateTimeDisplayDateStart"); } }
        private DateTime _DateTimeDisplayDateEnd { get; set; }
        public DateTime DateTimeDisplayDateEnd { get => _DateTimeDisplayDateEnd; set { _DateTimeDisplayDateEnd = value; OnPropertyChanged("DateTimeDisplayDateEnd"); } }
        private DateTime _DateTimeFrom { get; set; }
        public DateTime DateTimeFrom { get => _DateTimeFrom; set { _DateTimeFrom = value; OnPropertyChanged("DateTimeFrom"); } }
        private DateTime _DateTimeInput { get; set; }
        public DateTime DateTimeInput { get => _DateTimeInput; set { _DateTimeInput = value; OnPropertyChanged("DateTimeInput"); } }
        private ObservableCollection<BasicModel> _TypeCouponList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> TypeCouponList { get => _TypeCouponList; set { _TypeCouponList = value; OnPropertyChanged("TypeCouponList"); } }
        private BasicModel _TypeCouponItem;
        public BasicModel TypeCouponItem { get => _TypeCouponItem; set { _TypeCouponItem = value; OnPropertyChanged("TypeCouponItem"); } }
        private ObservableCollection<AdditionFee> _AdditionFeeList = new ObservableCollection<AdditionFee>();
        public ObservableCollection<AdditionFee> AdditionFeeList { get => _AdditionFeeList; set { _AdditionFeeList = value; OnPropertyChanged("AdditionFeeList"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }

        private BasicModel _TypeItem;
        public BasicModel TypeItem { get => _TypeItem; set { _TypeItem = value; OnPropertyChanged("TypeItem"); } }
        private string _TitleContent { get; set; }
        public string TitleContent { get => _TitleContent; set { _TitleContent = value; OnPropertyChanged("TitleContent"); } }

        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }

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

        private int BrandId;
        private long BranchId;
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
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        private Visibility _BranchVisibility;

        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        public void GetTypeCoupon()
        {
            if (TypeCouponList == null)
            {
                TypeCouponList = new ObservableCollection<BasicModel>();
            }
            else
            {
                TypeCouponList.Clear();
            }
            TypeCouponList.Add(new BasicModel(Constants.ALL, MessageValue.MESSAGE_ALL));
            TypeCouponList.Add(new BasicModel(Constants.NOT_STATUS, MessageValue.MESSAGE_FROM_EXPENSE_WINDOW_RECEIPTS_COUPON));
            TypeCouponList.Add(new BasicModel(Constants.STATUS, MessageValue.MESSAGE_FROM_EXPENSE_WINDOW_OUT_COUPON));
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
        public async void GetListData(int page,int brandId, long branchId, string FromDate, string ToDate, int type, int isTake, int isCountToRevenue, long restaurantBudgetId)
        {
            if (AdditionFeeList == null)
            {
                AdditionFeeList = new ObservableCollection<AdditionFee>();
            }
            else
            {
                AdditionFeeList.Clear();
            }
            CurrentPage = page;
            DialogHostOpen = true;
            AdditionFeeClient warehouseClient = new AdditionFeeClient(this, this, this);
            AdditionFeeResponse response = await Task.Run(() => warehouseClient.GetListAdditionFee(page, brandId, branchId, Constants.ALL, isTake, FromDate, ToDate, type, isCountToRevenue, restaurantBudgetId, currentUser.Id));
            if (response != null && response.Data != null && response.Data.List != null && response.Data.List.Count > 0)
            {
                response.Data.List.ForEach(AdditionFeeList.Add);
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
            {
                PageContent = string.Format("{0}/{1}", 1, 1);
                DialogHostOpen = false;
            }
        }
        public ExpenseCashierViewModel()
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
            DateTimeFrom = DateTime.Now;
            DateTimeInput = DateTime.Now;
            DateTimeDisplayDateEnd = DateTime.Now;
            GetTypeCoupon();
            TypeCouponItem = TypeCouponList.Where(x => x.Value == (int)WarehouseTypeEnum.ALL).FirstOrDefault();
            GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            SelectionChangedBrandCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
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
                    //if (BranchList.Count > 0) BranchItem = BranchList[0];
                    if (AdditionFeeList == null)
                    {
                        AdditionFeeList = new ObservableCollection<AdditionFee>();
                    }
                    else
                    {
                        AdditionFeeList.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
               // DialogHostOpen = true;
                if(DateTimeInput < DateTimeFrom)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                    return;
                }
                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            });
            SelectionChangedFromCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                // DialogHostOpen = true;
                if (DateTimeFrom > DateTimeInput)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                    return;
                }
                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            });
            ExportExcelCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_LIST;
                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel|*.xlsx|Excel 2003|*.xls";

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
                        exp.Workbook.Properties.Title = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_TITLE;

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_COUPON);

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_COUPON;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_CODE,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_OBJECT_NAME,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_REASON_PAYMENT,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_DATE,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_CREATE_AT,
                                                MessageValue.MESSAGE_FROM_MONEY_NUNMBER,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_TYPE_COUPON,
                                                MessageValue.MESSAGE_FROM_LIST_STATUS,
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_TITLE2;
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
                        foreach (var item in AdditionFeeList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.Code;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            ws.Cells[rowIndex, colIndex].Value = item.ObjectName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.AdditionFeeReasonName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Date;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.CreatedAt;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Amount;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.TypeName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.StatusText;
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
                catch (Exception EE)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_EXPORT_EXCEL_FAIL);
                    WriteLog.logs(EE.Message);
                    //  MessageBox.Show("Có lỗi khi lưu file!");
                }
            });

            AddPaymentSlipCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                ManagePaymentSlipWindow managePaymentSlipWindow = new ManagePaymentSlipWindow();
                managePaymentSlipWindow.DataContext = new ManagePaymentSlipViewModel(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : currentUser.BranchId);
                managePaymentSlipWindow.ShowDialog();
                var manage = managePaymentSlipWindow.DataContext as ManagePaymentSlipViewModel;
                if (manage != null && manage.isCreate == true)
                {
                    if (manage.AdditionFeeUpdate != null)
                    {
                        AdditionFeeList.Insert(0, manage.AdditionFeeUpdate);
                    }
                }
            });
            AddReceiptsCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                ManageReceiptWindow window = new ManageReceiptWindow();
                window.DataContext = new ManageReceiptViewModel(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId);
                window.ShowDialog();
                var manage = window.DataContext as ManageReceiptViewModel;
                if (manage != null && manage.isCreate == true)
                {
                    AdditionFeeList.Add(manage.AdditionFeeUpdate);
                }
            });
            AdditionFeeViewCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
            {
                ExpenseReceiptViewWindow window = new ExpenseReceiptViewWindow();
                window.DataContext = new ExpenseReceiptViewViewModel(p.Branch.Id, p.Id);
                window.ShowDialog();
            });
            AdditionFeeReturnCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    if (p.Type == (int)AdditionFeeEnum.OUT)
                    {
                        if (p.Status == (int)AdditionFeeStatusEnum.CANCEL_PAYMENT)
                        {
                            AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                            BaseResponse baseResponse = AdditionFeeClient.RefundAdditionFee(p.Id, p.Branch.Id);
                            if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                            {
                                p.Status = (int)AdditionFeeStatusEnum.CANCEL_PAYMENT_REFUNDED;
                                p.StatusText = "Đã truy thu do chi sai";
                                int index = AdditionFeeList.IndexOf(p);
                                AdditionFeeList.Remove(p);
                                AdditionFeeList.Insert(index, p);
                            }
                        }
                    }
                }
            });
            AdditionFeeCancelCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
           {
               if (p != null)
               {
                   if (p.Type == (int)AdditionFeeEnum.OUT)
                   {
                       if (p.Status == (int)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT || p.Status == (int)AdditionFeeStatusEnum.WAITING_PAYMENT)
                       {
                           AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                           BaseResponse baseResponse = AdditionFeeClient.CancelAdditionFee(p.Id, p.Branch.Id);
                           if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                           {
                               p.Status = (int)AdditionFeeStatusEnum.CANCEL;
                               p.StatusText = "Đã hủy";
                               int index = AdditionFeeList.IndexOf(p);
                               AdditionFeeList.Remove(p);
                               AdditionFeeList.Insert(index, p);

                           }
                       }
                       else if (p.Status == (int)AdditionFeeStatusEnum.PAID)
                       {
                           AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                           BaseResponse baseResponse = AdditionFeeClient.CancelPaymentAdditionFee(p.Id, p.Branch.Id);
                           if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                           {
                               p.Status = (int)AdditionFeeStatusEnum.CANCEL_PAYMENT;
                               p.StatusText = "Hủy do chi sai";
                               int index = AdditionFeeList.IndexOf(p);
                               AdditionFeeList.Remove(p);
                               AdditionFeeList.Insert(index, p);
                           }
                       }
                   }
               }
           });

            AdditionFeeApproveCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
            {
                if (p != null && p.Type == (int)AdditionFeeEnum.OUT)
                {
                    if (p.Status == (long)AdditionFeeStatusEnum.WAITTING_APPROVE_PAYMENT)
                    {
                        AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                        BaseResponse baseResponse = AdditionFeeClient.ApproveAdditionFee(p.Id, p.Branch.Id);
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            p.Status = (int)AdditionFeeStatusEnum.WAITING_PAYMENT;
                            p.StatusText = "Chờ chi";
                            int index = AdditionFeeList.IndexOf(p);
                            AdditionFeeList.Remove(p);
                            AdditionFeeList.Insert(index, p);
                        }
                    }
                    else if (p.Status == (long)AdditionFeeStatusEnum.WAITING_PAYMENT)
                    {

                        AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                        BaseResponse baseResponse = AdditionFeeClient.PaidAdditionFee(p.Id, p.Branch.Id);
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            p.Status = (int)AdditionFeeStatusEnum.PAID;
                            p.StatusText = "Chờ xác nhận";
                            int index = AdditionFeeList.IndexOf(p);
                            AdditionFeeList.Remove(p);
                            AdditionFeeList.Insert(index, p);
                        }
                    }
                    else if (p.Status == (long)AdditionFeeStatusEnum.PAID)
                    {
                        AdditionFeeClient AdditionFeeClient = new AdditionFeeClient(this, this, this);
                        BaseResponse baseResponse = AdditionFeeClient.ConfirmAdditionFee(p.Id, p.Branch.Id);
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            p.Status = (int)AdditionFeeStatusEnum.CONFIRMED;
                            p.StatusText = "Hoàn tất";
                            int index = AdditionFeeList.IndexOf(p);
                            AdditionFeeList.Remove(p);
                            AdditionFeeList.Insert(index, p);
                        }
                    }
                }
            });
            AdditionFeeEditCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    if (p.Type == (int)AdditionFeeEnum.OUT)
                    {
                        ManagePaymentSlipWindow managePaymentSlipWindow = new ManagePaymentSlipWindow();
                        managePaymentSlipWindow.DataContext = new ManagePaymentSlipViewModel(p);
                        managePaymentSlipWindow.ShowDialog();
                        var manage = managePaymentSlipWindow.DataContext as ManagePaymentSlipViewModel;
                        if (manage != null && manage.isCreate)
                        {
                            if (manage.AdditionFeeUpdate != null)
                            {
                                int index = AdditionFeeList.IndexOf(p);
                                AdditionFeeList.Remove(p);
                                AdditionFeeList.Insert(index, manage.AdditionFeeUpdate);
                            }
                            else
                            {
                                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);
                            }
                        }
                    }
                    else if (p.Type == (int)AdditionFeeEnum.IN)
                    {
                        ManageReceiptWindow managePaymentSlipWindow = new ManageReceiptWindow();
                        managePaymentSlipWindow.DataContext = new ManageReceiptViewModel(p);
                        managePaymentSlipWindow.ShowDialog();
                        var manage = managePaymentSlipWindow.DataContext as ManageReceiptViewModel;
                        if (manage != null && manage.isCreate)
                        {
                            if (manage.AdditionFeeUpdate != null)
                            {
                                int index = AdditionFeeList.IndexOf(p);
                                AdditionFeeList.Remove(p);
                                AdditionFeeList.Insert(index, manage.AdditionFeeUpdate);
                            }
                            else
                            {
                                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);
                            }
                        }
                    }

                }
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);

            });

            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);

                }

            });

            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);

                }

            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL);

            });
        }
        public ExpenseCashierViewModel(RestaurantBudget restaurantBudget)
        {

            DateTimeInput = DateTimeDisplayDateEnd;
            GetTypeCoupon();
            TypeCouponItem = TypeCouponList.Where(x => x.Value == (int)Constants.ALL).FirstOrDefault();
            GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, restaurantBudget.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, (int)WarehouseTypeEnum.ALL, 1, restaurantBudget.Id);
            TypeCouponSelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : Constants.ALL, (int)WarehouseTypeEnum.ALL, Constants.ALL, restaurantBudget.Id);
            });
            SelectionChangedBrandCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
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
                    //if (BranchList.Count > 0) BranchItem = BranchList[0];
                    if (AdditionFeeList == null)
                    {
                        AdditionFeeList = new ObservableCollection<AdditionFee>();
                    }
                    else
                    {
                        AdditionFeeList.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                //DialogHostOpen = true;
                if (DateTimeInput < DateTimeFrom)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                    return;
                }
                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            });
            SelectionChangedFromCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                // DialogHostOpen = true;
                if (DateTimeFrom > DateTimeInput)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                    return;
                }
                GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), TypeCouponItem != null ? TypeCouponItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            });
            ExportExcelCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_LIST;
                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_EXPORT_EXCEL_NOT_FILE_PART);
                    return;
                }

                try
                {
                    using (ExcelPackage exp = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        exp.Workbook.Properties.Author = Constants.MESSAGE_EXCEL_AUTHOR;

                        // đặt tiêu đề cho file
                        exp.Workbook.Properties.Title = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_TITLE;

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_COUPON);

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_COUPON;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                 MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_CODE,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_OBJECT_NAME,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_REASON_PAYMENT,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_DATE,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_CREATE_AT,
                                                MessageValue.MESSAGE_FROM_MONEY_NUNMBER,
                                                MessageValue.MESSAGE_FROM_REPORT_ADDITION_FEE_VIEW_DETAIL_TYPE_COUPON,
                                                MessageValue.MESSAGE_FROM_LIST_STATUS,
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = MessageValue.MESSAGE_FROM_EXPENSE_VIEWMODEL_EXCEL_TITLE2;
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
                        foreach (var item in AdditionFeeList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.Code;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            ws.Cells[rowIndex, colIndex].Value = item.ObjectName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.AdditionFeeReasonName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Date;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.CreatedAt;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                           ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Amount;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.TypeName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                          ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                          ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                          ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.StatusText;
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
                catch (Exception EE)
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_EXPORT_EXCEL_FAIL);
                    WriteLog.logs(EE.Message);
                    //  MessageBox.Show("Có lỗi khi lưu file!");
                }
            });

            AdditionFeeViewCommand = new RelayCommand<AdditionFee>((p) => { return true; }, p =>
            {
                ExpenseReceiptViewWindow window = new ExpenseReceiptViewWindow();
                window.DataContext = new ExpenseReceiptViewViewModel(p.Branch.Id, p.Id);
                window.ShowDialog();
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeItem != null ? TypeItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, restaurantBudget.Id);

            });

            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    GetListData(Constants.OFFSET, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeItem != null ? TypeItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, restaurantBudget.Id);

                }

            });

            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeItem != null ? TypeItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, restaurantBudget.Id);

                }

            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                GetListData(CurrentPage, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFrom), Utils.Utils.GetDateFormatVN(DateTimeInput), Constants.ALL, TypeItem != null ? TypeItem.Value : (int)WarehouseTypeEnum.ALL, Constants.ALL, restaurantBudget.Id);

            });
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
    }
}

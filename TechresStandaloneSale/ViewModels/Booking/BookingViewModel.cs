using DevExpress.Mvvm.Native;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.ViewModels.Booking;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class BookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<Models.Booking> _BookingItems = new ObservableCollection<Models.Booking>();
        public ObservableCollection<Models.Booking> BookingItems
        {
            get
            {
                return _BookingItems;
            }
            set
            {
                _BookingItems = value;
                OnPropertyChanged("BookingItems");
            }
        }
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
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private DateTime _DateTimeFrom { get; set; }
        public DateTime DateTimeFrom { get => _DateTimeFrom; set { _DateTimeFrom = value; OnPropertyChanged("DateTimeFrom"); } }
        private DateTime _DateTimeTo { get; set; }
        public DateTime DateTimeTo { get => _DateTimeTo; set { _DateTimeTo = value; OnPropertyChanged("DateTimeTo"); } }

        private string _ContentTitle { get; set; }
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        public ICommand AddBookingCommand { get; set; }
        public ICommand EditBookingCommand { get; set; }
        public ICommand BtnViewBooking { get; set; }
        public ICommand BtnConfirmBooking { get; set; }
        public ICommand BtnPrintBooking { get; set; }

        public ICommand BtnConfirmDebt { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionFormDayChangedCommand { get; set; }
        public ICommand SelectionToDayChangedCommand { get; set; }

        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
        #region Đạt
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

        //private bool _isCheckEnable;
        //public bool isCheckEnable { get => _isCheckEnable; set { _isCheckEnable = value; OnPropertyChanged("isCheckEnable"); } }
        #endregion

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        public TechresStandaloneSale.Models.Booking booking;
        //private int TotalPage;
        //private int CurrentPage;
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
                branches.Where(x=>x.Id != Constants.ALL).ForEach(BranchList.Add);
            }
        }
        public void GetAllBrands()
        {
            if (BrandList == null)
            {
                BrandList = new ObservableCollection<Brand>();
            }
            else
            {
                BrandList.Clear();
            }
            List<Brand> branches = (List<Brand>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_BRAND);
            if (branches != null)
            {
                branches.ForEach(BrandList.Add);
            }
        }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private int BrandId;
        private long BranchId;
        public BookingViewModel()
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
                //if(booking.BookingStatus == 2 && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions)
                //   || booking.BookingStatus == 9 && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BOOKING_MANAGER), currentUser.Permissions))
                //{
                //    isCheckEnable = true;
                //}
                //else
                //{
                //    isCheckEnable = false;
                //}
                DateTimeFrom = DateTime.Now;
                DateTimeTo = DateTime.Now;
                CurrentPage = 1;
                GetListBooking(BrandId,BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);
                AddBookingCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
               {
                   if (Constants.IS_NETWORK_ONLINE)
                   {
                       BookingWindowPopup booking = new BookingWindowPopup();
                       booking.DataContext = new ManageBookingViewModel(BrandItem != null && BrandItem.Id != Constants.ALL ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null && BranchItem.Id != Constants.ALL ? BranchItem.Id : currentUser.BranchId);
                       booking.ShowDialog();
                       var model = booking.DataContext as ManageBookingViewModel;
                       if (model.isCreate)
                       {
                           if (DateTimeFrom <= model.BookingTime)
                               if ((BranchItem != null && (BranchItem.Id == model.BookingUpdate.Branch.Id || BranchItem.Id == Constants.ALL)) || BranchItem == null)
                                   BookingItems.Insert(0, model.BookingUpdate);
                       }
                   }
                   else
                   {
                       NotificationMessage.Error(MessageValue.WORKING_ONLINE_REQUIRED);
                   }

               });

                EditBookingCommand = new RelayCommand<Models.Booking>((p) => { return true; }, p =>
               {
                   BookingWindowPopup booking = new BookingWindowPopup();
                   booking.DataContext = new ManageBookingViewModel(p);
                   booking.ShowDialog();
                   var model = booking.DataContext as ManageBookingViewModel;
                   if (model.isCreate)
                   {
                       if (DateTimeFrom <= model.BookingTime)
                           GetListBooking(BrandId, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);
                       //GetListBooking(BranchItem != null ? BranchItem.Id : currentUser.BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now);
                   }
               });
                ExportCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string filePath = "";
                    // tạo SaveFileDialog để lưu file excel
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.FileName = string.Format(MessageValue.MESSAGE_FROM_BOOKING_LIST, BookingItems.Count);
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
                        NotificationMessage.Infomation(MessageValue.MESSAGE_EXPORT_EXCEL_NOT_FILE_PART);

                        return;
                    }

                    try
                    {
                        using (ExcelPackage exp = new ExcelPackage())
                        {
                            // đặt tên người tạo file
                            exp.Workbook.Properties.Author = Constants.MESSAGE_EXCEL_AUTHOR;

                            // đặt tiêu đề cho file
                            exp.Workbook.Properties.Title = string.Format(MessageValue.MESSAGE_FROM_BOOKING_LIST, BookingItems.Count);

                            //Tạo một sheet để làm việc trên đó
                            exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_BOOKING_LIST);

                            // lấy sheet vừa add ra để thao tác
                            ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                            // đặt tên cho sheet
                            ws.Name = MessageValue.MESSAGE_FROM_BOOKING_LIST;
                            // fontsize mặc định cho cả sheet
                            ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                            // font family mặc định cho cả sheet
                            ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;
                            // Tạo danh sách các column header
                            string[] arrColumnHeader = {
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_CODE,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_CUSTOMER_NAME,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_CUSTOMER_PHONE_NUMBER,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_TIME_SELECTION,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_TABLE_SELECTION,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_AMOUNT_OF_PEOPLE,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_LIST_EATING,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_OTHER_REQUIREMENTS,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_NOTE,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_DEPOSITS,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_PAYMENTS_OF_DEPOSITS,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_TYPE_OF_TABLE,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_EMPLOYEE_NAME,
                                                MessageValue.MESSAGE_FROM_LIST_BOOKING_STATUS,
                };

                            // lấy ra số lượng cột cần dùng dựa vào số lượng header
                            var countColHeader = arrColumnHeader.Count();

                            // merge các column lại từ column 1 đến số column header
                            // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                            ws.Cells[1, 1].Value = string.Format(MessageValue.MESSAGE_FROM_BOOKING_LIST, BookingItems.Count);
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
                            foreach (var item in BookingItems)
                            {
                                // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                colIndex = 1;

                                // rowIndex tương ứng từng dòng dữ liệu
                                rowIndex++;
                                //gán giá trị cho từng cell
                                ws.Cells[rowIndex, colIndex++].Value = item.BookingCode;

                                // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                                ws.Cells[rowIndex, colIndex++].Value = item.CustomerName;
                                ws.Cells[rowIndex, colIndex++].Value = item.CustomerPhone;
                                ws.Cells[rowIndex, colIndex++].Value = item.BookingTime;
                                ws.Cells[rowIndex, colIndex++].Value = item.TableString;
                                ws.Cells[rowIndex, colIndex++].Value = item.NumberSlot;
                                ws.Cells[rowIndex, colIndex++].Value = item.FoodString;
                                ws.Cells[rowIndex, colIndex++].Value = item.OrtherRequirements;
                                ws.Cells[rowIndex, colIndex++].Value = item.Note;
                                ws.Cells[rowIndex, colIndex++].Value = item.DepositString;
                                ws.Cells[rowIndex, colIndex++].Value = item.ReturnDepositString;
                                ws.Cells[rowIndex, colIndex++].Value = item.BookingTypeName;
                                ws.Cells[rowIndex, colIndex++].Value = item.Employee.Name;
                                ws.Cells[rowIndex, colIndex++].Value = item.BookingStatusName;
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
                        WriteLog.logs(EE.Message);
                        NotificationMessage.Infomation(MessageValue.MESSAGE_EXPORT_EXCEL_FAIL);
                    }

                });

                BtnPrintBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    PrintBill(p);
                });

                BtnConfirmDebt = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    ViewBookingWindow view = new ViewBookingWindow();
                    view.DataContext = new ViewBookingViewModel(p);
                    view.ShowDialog();
                });

                BtnViewBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    ViewBookingWindow view = new ViewBookingWindow();
                    view.DataContext = new ViewBookingViewModel(p);
                    view.ShowDialog();
                });
                BtnConfirmBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    if (p.BookingStatus == (int)BookingStatusEnum.WAITING_CONFIRM)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CONFIRM_BOOKING;
                        string Title = MessageValue.MESSAGE_CONFIRM_BOOKING_TITLE;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            if((p.DepositAmount == 0 || p.DepositAmount > 0 && p.IsDepositConfirmed == 1))
                            {
                                BookingClient bookingClient = new BookingClient(this, this, this);
                                BookingResponse response = bookingClient.ConfirmBooking(p.Id);
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    int index = BookingItems.IndexOf(p);
                                    BookingItems.Remove(p);
                                    BookingItems.Insert(index, response.Data);
                                }
                            }
                            else
                            {
                                NotificationMessage.Error("Đơn đặt bàn có cọc yêu cầu xác nhận hoặc hủy cọc để tiếp tục!");
                            }
                        }
                    }
                    else if (p.BookingStatus == (int)BookingStatusEnum.CONFIMED)
                    {
                        if ((p.DepositAmount == 0 || p.DepositAmount > 0 && p.IsDepositConfirmed == 1))
                        {
                            MergeTableWindow tableWindow = new MergeTableWindow();
                            MergeTableViewModel tableViewModel = new MergeTableViewModel(MessageValue.MESSAGE_FROM_ORDER_CHOOSE_TABLE_TITLE, p.Branch.Id, p.Id);
                            tableWindow.DataContext = tableViewModel;
                            tableWindow.ShowDialog();
                            if (tableViewModel.BookingTables != null && tableViewModel.BookingTables.Count > 0)
                            {
                                BookingClient bookingClient = new BookingClient(this, this, this);
                                BookingResponse response = bookingClient.ArrangeTableBooking(new ArrangeTableWrapper(p.Branch.Id, (int)tableViewModel.CurrentAreaId, tableViewModel.BookingTables), p.Id);
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    int index = BookingItems.IndexOf(p);
                                    BookingItems.Remove(p);
                                    BookingItems.Insert(index, response.Data);
                                }
                            }
                        }
                        else
                        {
                            NotificationMessage.Error("Đơn đặt bàn có cọc yêu cầu xác nhận hoặc hủy cọc để tiếp tục!");
                        }
                    }
                    else if (p.BookingStatus == (int)BookingStatusEnum.ARRANGED_TABLE)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_SETUP_BOOKING;
                        string Title = MessageValue.MESSAGE_SETUP_BOOKING_TITLE;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            BookingClient bookingClient = new BookingClient(this, this, this);
                            BookingResponse response = bookingClient.SetupBooking(new StartBookingWrapper(p.Id, p.Branch.Id));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                int index = BookingItems.IndexOf(p);
                                BookingItems.Remove(p);
                                BookingItems.Insert(index, response.Data);
                            }
                        }
                    }
                    else if (p.BookingStatus == (int)BookingStatusEnum.SET_UP)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_START_BOOKING;
                        string Title = MessageValue.MESSAGE_START_BOOKING_TITLE;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            BookingClient bookingClient = new BookingClient(this, this, this);
                            BookingResponse response = bookingClient.StartBooking(new StartBookingWrapper(p.Id, p.Branch.Id));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                int index = BookingItems.IndexOf(p);
                                BookingItems.Remove(p);
                                BookingItems.Insert(index, response.Data);
                            }
                        }
                    }
                });
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
                        if (BookingItems == null)
                        {
                            BookingItems = new ObservableCollection<Models.Booking>();
                        }
                        else
                        {
                            BookingItems.Clear();
                        }
                    }
                    DialogHostOpen = false;
                });
                SelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
                {
                    DialogHostOpen = true;
                    if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                    {
                        BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                        CurrentPage = 1;
                        GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);

                    }
                });
                SelectionFormDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if(DateTimeFrom > DateTimeTo)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);

                });

                SelectionToDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;
                    if (DateTimeTo < DateTimeFrom)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);

                });

                PageDoubleLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                {
                    CurrentPage = 1;
                    GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);


                });

                PageLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                {
                    if (CurrentPage > 1)
                    {
                        CurrentPage = CurrentPage - 1;
                        GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);

                    }

                });

                PageRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                {
                    if (CurrentPage < TotalPage)
                    {
                        CurrentPage = CurrentPage + 1;
                        GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);

                    }

                });
                PageDoubleRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                {
                    CurrentPage = TotalPage;
                    GetListBooking(BrandId, BranchId, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);
                });
                CancelCommand = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, (p) => {
                    //booking = new Models.Booking();

                    ResonCancelBookingWindow cancelWindow = new ResonCancelBookingWindow();
                    cancelWindow.DataContext = new ResonCancelBookingViewModel();
                    cancelWindow.ShowDialog();
                    var modelCancel = cancelWindow.DataContext as ResonCancelBookingViewModel;
                    if (modelCancel.isDone)
                    {
                        BookingClient bookingClient = new BookingClient(this, this, this);
                        BookingResponse response = bookingClient.CancelBookingReson(p.Id, new BookingResonCancelResponse(currentUser.BranchId, modelCancel.ResonNote));
                        if(response != null)
                        {
                            if(response.Status == (int)ResponseEnum.OK)
                            {
                                NotificationMessage.Infomation(MessageValue.MESSAGE_FROM_NOTIFICATION_CANCEL_RESON_BOOKING);
                                GetListBooking(BrandId, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeTo != null ? DateTimeTo : DateTime.Now, CurrentPage);
                            }
                        }
                    }
                });

            }
        }
        public void PrintBill(Models.Booking booking)
        {
            DeviceClient deviceClient = new DeviceClient();
            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
            DeviceConfigWrapper device = deviceClient.RealConfigs();
            if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
            {
                Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
                pt.PrintBooking(MessageValue.MESSAGE_FROM_BOOKING_TITLE, booking, device.CasherPrinter, device.CasherSize, currentBranch, false);
            }
        }
        public async void GetListBooking(int brandId,long BranchId, DateTime FormDate, DateTime ToDate, int page)
        {
            if (BookingItems == null)
            {
                BookingItems = new ObservableCollection<Models.Booking>();
            }
            else
            {
                BookingItems.Clear();
            }
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_BOOKING_LIST, 0);
            DialogHostOpen = true;
            List<long> statuses = new List<long>() { (long)BookingStatusEnum.ARRANGED_TABLE, (long)BookingStatusEnum.CANCEL, (long)BookingStatusEnum.COMPLETED,
                                                                                               (long)BookingStatusEnum.CONFIMED, (long)BookingStatusEnum.EXPIRED, (long)BookingStatusEnum.SET_UP,
                                                   (long)BookingStatusEnum.UNKONW, (long)BookingStatusEnum.WAITING_COMPLETE, (long)BookingStatusEnum.WAITING_CONFIRM};
            BookingClient bookingClient = new BookingClient(this, this, this);
             BookingListResponse response = await System.Threading.Tasks.Task.Run(() => bookingClient.GetListBooking(brandId,BranchId, Utils.Utils.GetDateFormatVN(FormDate), Utils.Utils.GetDateFormatVN(ToDate),
                                                                                        Utils.Utils.convertFormListLong(statuses), Constants.ALL, Constants.ALL, page, (long)LimitEnum.ONE_HUNDRED));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.List.ForEach(BookingItems.Add);
                //MessageBox.Show(response.Data.List[81].ReturnDepositString);
                if (response.Data.TotalRecord > 0)
                {
                    if (response.Data.TotalRecord % response.Data.Limit != 0)
                    {
                        TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit) + 1;
                        UpdateEnableState();
                    }
                    else
                    {
                        TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit);
                        UpdateEnableState();
                    }
                    //CurrentPage = 1;
                    PageContent = string.Format("{0}/{1}", CurrentPage, TotalPage);
                }
                else
                {
                    PageContent = string.Format("{0}/{1}", 1, 1);
                }
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_BOOKING_LIST, response.Data.TotalRecord);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }

        public void LogError(Exception ex, string infoMessage)
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

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
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class OrderListDetailAdminViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand BtnViewOrder { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand BtnPrintOrder { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand SelectionFormDayChangedCommand { get; set; }
        public ICommand SelectionToDayChangedCommand { get; set; }
        public ICommand BtnHistoryActivityOrder { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleRight { get; set; }

        private ObservableCollection<Order> _ItemsOrder = new ObservableCollection<Order>();
        public ObservableCollection<Order> ItemsOrder { get => _ItemsOrder; set { _ItemsOrder = value; OnPropertyChanged("ItemsOrder"); } }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }

        private Visibility _BranchVisibility;
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }

        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private string _ContentTitle;
        public string ContentTitle
        {
            get
            {
                return _ContentTitle;
            }
            set
            {
                _ContentTitle = value;
                OnPropertyChanged("ContentTitle");
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
        #region toan

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
        private DateTime _DatePickerDisplayDateEnd { get; set; }
        public DateTime DatePickerDisplayDateEnd { get => _DatePickerDisplayDateEnd; set { _DatePickerDisplayDateEnd = value; OnPropertyChanged("DatePickerDisplayDateEnd"); } }
        private DateTime _DateTimeFrom { get; set; }
        public DateTime DateTimeFrom { get => _DateTimeFrom; set { _DateTimeFrom = value; OnPropertyChanged("DateTimeFrom"); } }
        private DateTime _DateTimeTo { get; set; }
        public DateTime DateTimeTo { get => _DateTimeTo; set { _DateTimeTo = value; OnPropertyChanged("DateTimeTo"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public List<Branch> branches = new List<Branch>();
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }


        private DateTime FormDate;
        private DateTime ToDate;
        private long BranchId;
        public async void GetListOrder(int brandId,long branchId,DateTime fromDate, DateTime toDate, int Page)
        {         
            DialogHostOpen = true;
            OrdersClient client = new OrdersClient(this, this, this);
            string FromDateString = Utils.Utils.GetDateFormatVN(fromDate);
            string ToDateString = Utils.Utils.GetDateFormatVN(toDate);
            int FromHour = -1;
            int ToHour = -1;
            string OrderStatus = "";
            CurrentPage = Page;
            TotalPage = 1;
            OrderResponse order = await System.Threading.Tasks.Task.Run(() 
                => client.GetListOrderHistory(Constants.TYPE_ORDER_ALL_HISTORY, FromDateString,FromHour,ToDateString,ToHour,OrderStatus,Page,(long)LimitEnum.ONE_HUNDRED, branchId, brandId));
            if (order != null && order.Status == (int)ResponseEnum.OK && order.Data != null)
            {
                if (ItemsOrder == null)
                {
                    ItemsOrder = new ObservableCollection<Order>();
                }
                else
                {
                    ItemsOrder.Clear();
                }
                FormDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                ToDate = new DateTime(toDate.Year, toDate.Month, toDate.Day,0,0,0);
                BranchId = branchId;
                order.Data.List.ForEach(ItemsOrder.Add);
                if (order.Data.TotalRecord > 0)
                {
                    if (order.Data.TotalRecord % order.Data.Limit != 0)
                    {
                        TotalPage = (int)(order.Data.TotalRecord / order.Data.Limit) + 1;
                    }
                    else
                    {
                        TotalPage = (int)(order.Data.TotalRecord / order.Data.Limit);
                    }
                    //CurrentPage = 1;
                    PageContent = CurrentPage + "/" + (TotalPage == 0 ? 1 : TotalPage);
                }
                else
                {
                    PageContent = string.Format("{0}/{1}", 1, 1);
                }
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_ORDER_LIST, ItemsOrder.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
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

             branches = (List<Branch>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_BRANCH);
            if (branches != null)
            {
                branches.ForEach(BranchList.Add);
                BranchList.RemoveAt(0);
            }
        }

        public OrderListDetailAdminViewModel()
        {
            LoadedWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
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
                DatePickerDisplayDateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                CurrentPage = 1;
                GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId,  DateTimeFrom != null ? DateTimeFrom : DateTime.Now, DateTimeFrom != null ? DateTimeFrom : DateTime.Now, Constants.OFFSET); // ha sua 
               
            });
            SelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, p =>
            {
                if (BranchId != (BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID) || FormDate != DateTimeFrom || ToDate != DateTimeTo)
                {
                    if (DateTimeFrom != null && DateTimeTo != null)
                    {
                        GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId, DateTimeFrom, DateTimeTo, Constants.OFFSET); // ha sua 
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
                }
                DialogHostOpen = false;
            });
            ExportCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = string.Format(MessageValue.MESSAGE_FROM_ORDER_LIST, ItemsOrder.Count);
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
                        exp.Workbook.Properties.Title = string.Format(MessageValue.MESSAGE_FROM_ORDER_LIST, ItemsOrder.Count);

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_ORDER_LIST);

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_ORDER_LIST;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;
                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_CODE,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_TABLE,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_EMPLOYEE,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_PROVISIONAL,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_VAT,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_DISCOUNT,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_TOTAL_MONEY,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_DATE_CREATED,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_FINISHED,
                                                MessageValue.MESSAGE_FROM_ORDER_LIST_STATUS,
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = string.Format(MessageValue.MESSAGE_FROM_ORDER_LIST, ItemsOrder.Count);
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
                        foreach (var item in ItemsOrder)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.OrderCode;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
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
                            ws.Cells[rowIndex, colIndex].Value = item.Amount;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Vat;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.DiscountPercent;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
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

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.CreatedAt;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.UpdatedAt;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex++].Value = item.OrderStatusString;
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
                    WriteLog.logs(EE.Message);
                    NotificationMessage.Error(MessageValue.MESSAGE_EXPORT_EXCEL_FAIL);
                }

            });
            SelectionFormDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (BranchId != (BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID) || FormDate != DateTimeFrom || ToDate != DateTimeTo )
                {
                    if (string.IsNullOrEmpty(p.Text))
                    {
                        DateTimeFrom = DateTime.Now;
                    }
                    else
                    {
                        DateTimeFrom = DateTime.Parse(p.Text);
                    }
                    if (DateTimeFrom > DateTimeTo)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId, DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 
                }
            });
            SelectionToDayChangedCommand = new RelayCommand<DatePicker>((p) => { return true; }, p =>
            {
                if (BranchId != (BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID) || FormDate != DateTimeFrom || ToDate != DateTimeTo)
                 {
                    if (string.IsNullOrEmpty(p.Text))
                    {
                        DateTimeTo = DateTime.Now;
                    }
                    else
                    {
                        DateTimeTo = DateTime.Parse(p.Text);
                    }
                    if(DateTimeTo < DateTimeFrom)
                    {                     
                        NotificationMessage.Warning(MessageValue.MESSAGE_DATETIME_TO_ERROR);
                        return;
                    }
                    GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId, DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 
                }
            });
            BtnViewOrder = new RelayCommand<Order>((p) => { return true; }, p =>
            {
                OrderViewWindow popup = new OrderViewWindow();
                popup.DataContext = new OrderViewViewModel(p.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID);
                popup.ShowDialog();
            });
            BtnPrintOrder = new RelayCommand<Order>((p) => { return true; }, p =>
            {

            });
            BtnHistoryActivityOrder = new RelayCommand<Order>((p) => { return true; }, p =>
            {
                ActivityWindow activityWindow = new ActivityWindow();
                activityWindow.DataContext = new ActivityOrderViewModel(p.Id);
                activityWindow.Show();
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = 1;
                if (ItemsOrder != null)
                    ItemsOrder.Clear();
                GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId,  DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 

            });

            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    if (ItemsOrder != null)
                        ItemsOrder.Clear();
                    GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id :currentUser.BranchId,  DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 
                }

            });

            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = CurrentPage + 1;
                    if (ItemsOrder != null)
                        ItemsOrder.Clear();
                    GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId,  DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 
                }

            });
            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = TotalPage;
                if (ItemsOrder != null)
                    ItemsOrder.Clear();
                GetListOrder(BrandItem != null ? BrandItem.Id : (int)Constants.CURRENT_BRAND_ID,BranchItem != null ? BranchItem.Id : currentUser.BranchId,  DateTimeFrom, DateTimeTo, CurrentPage); // ha sua 

            });
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

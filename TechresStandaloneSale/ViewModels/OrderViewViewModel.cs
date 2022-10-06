using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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
    public class OrderViewViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        private ObservableCollection<Models.BillResponse> itemBills;
        public ObservableCollection<Models.BillResponse> ItemBills
        {
            get
            {
                return itemBills;
            }
            set
            {
                itemBills = value;
                OnPropertyChanged("ItemBills");
            }
        }
        private string _OrderCode { get; set; }
        public string OrderCode { get => _OrderCode; set { _OrderCode = value; } }
        private string _TotalAmount { get; set; }
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; } }
        private string _DepositAmount { get; set; }
        public string DepositAmount { get => _DepositAmount; set { _DepositAmount = value; } }
        private string _Amount { get; set; }
        public string Amount { get => _Amount; set { _Amount = value; } }
        private string _TimeContent { get; set; }
        public string TimeContent { get => _TimeContent; set { _TimeContent = value; } }
        private string _DiscountAmount { get; set; }
        public string DiscountAmount { get => _DiscountAmount; set { _DiscountAmount = value; } }
        private string _TableName { get; set; }
        public string TableName { get => _TableName; set { _TableName = value; } }
        private string _VAT { get; set; }
        public string VAT { get => _VAT; set { _VAT = value; } }
        private long _AloPoint { get; set; }
        public long AloPoint { get => _AloPoint; set { _AloPoint = value; } }
        private long _AccumulatePoint { get; set; }
        public long AccumulatePoint { get => _AccumulatePoint; set { _AccumulatePoint = value; } }
        private long _PromotionPoint { get; set; }
        public long PromotionPoint { get => _PromotionPoint; set { _PromotionPoint = value; } }
        private long _PointAdded { get; set; }
        public long PointAdded { get => _PointAdded; set { _PointAdded = value; } }
        private string _EmployeeName { get; set; }
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; } }
        private string _EmployeePayment { get; set; }
        public string EmployeePayment { get => _EmployeePayment; set { _EmployeePayment = value; } }
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged("CustomerName"); } }

        #region toan
        private string _CustomerPhone { get; set; }
        public string CustomerPhone { get => _CustomerPhone; set { _CustomerPhone = value; } }
      
        private string _CustomerAddress { get; set; }
        public string CustomerAddress { get => _CustomerAddress; set { _CustomerAddress = value; } }
        #endregion
        private string _EmployeeGiveQrCode { get; set; }
        public string EmployeeGiveQrCode { get => _EmployeeGiveQrCode; set { _EmployeeGiveQrCode = value; } }
        private long _CustomerSlot { get; set; }
        public long CustomerSlot { get => _CustomerSlot; set { _CustomerSlot = value; } }
        private string _ImageStatus { get; set; }
        public string ImageStatus { get => _ImageStatus; set { _ImageStatus = value; } }
        private string _DiscountTitle { get; set; }
        public string DiscountTitle { get => _DiscountTitle; set { _DiscountTitle = value; } }
        private string _VATTitle { get; set; }
        public string VATTitle { get => _VATTitle; set { _VATTitle = value; } }
        private Visibility _OrderStatusVisibility { get; set; }
        public Visibility OrderStatusVisibility { get => _OrderStatusVisibility; set { _OrderStatusVisibility = value; } }
        private string _OrderStatus { get; set; }
        public string OrderStatus { get => _OrderStatus; set { _OrderStatus = value; } }
        private Visibility _ImageStatusVisibility { get; set; }
        public Visibility ImageStatusVisibility { get => _ImageStatusVisibility; set { _ImageStatusVisibility = value; } }
        public ICommand BtnViewOrderDetail { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        public ICommand BtnPrintOrder { get; set; }

        private Restaurant currentRestaurant = (Restaurant)Utils.Utils.GetCacheValue(Constants.CURRENT_RESTAURANT);
        public DeviceClient deviceClient = new DeviceClient();
        public User currentUser;
        private Branch currentBranch;

        public void GetOrderView(long orderId, long branchId)
        {
            BranchClient branchClient = new BranchClient(this, this, this);
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            BranchResponse branchResponse = branchClient.getBranchInfo(branchId >= 0 ? branchId : currentUser.BranchId);
            if (branchResponse != null && branchResponse.Status == (int)ResponseEnum.OK)
            {
                currentBranch = branchResponse.Data;
            }

            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrdersClient ordersClient = new OrdersClient(this, this, this);
            OrderItemResponse response = ordersClient.GetOrderById(orderId, currentBranch.Id, Constants.STATUS, Constants.NOT_STATUS);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                if (ItemBills == null)
                {
                    ItemBills = new ObservableCollection<BillResponse>();
                }
                else
                {
                    ItemBills.Clear();
                }
                if (response.Data.Foods != null && response.Data.Foods.Count > 0)
                {
                    response.Data.Foods.ForEach(ItemBills.Add);
                }

                OrderCode = "#" + response.Data.Id;
                
                TotalAmount = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                TableName = (response.Data.TableMergeListName != null && response.Data.TableMergeListName.Count > 0) ? string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_PLUS, response.Data.TableName, String.Join(", ", response.Data.TableMergeListName.Select(p => p.ToString()).ToArray())) : response.Data.TableName;
                VATTitle = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VAT);
                VAT = Utils.Utils.FormatMoney(response.Data.VatAmount);
                DepositAmount = Utils.Utils.FormatMoney(response.Data.BookingDepositAmount); 
                DiscountTitle = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_DISCOUNT, response.Data.DiscountPercent);
                DiscountAmount = Utils.Utils.FormatMoney(response.Data.DiscountAmount);
                Amount = Utils.Utils.FormatMoney(response.Data.Amount);
                AloPoint = response.Data.MembershipAloPointUsed;
                AccumulatePoint = response.Data.MembershipAccumulatePointUsed;
                PromotionPoint = response.Data.MembershipPromotionPointUsed;
                PointAdded = response.Data.MembershipPointUsed;
                DateTime createAt = Utils.Utils.GetStringFormatDateTimeHour(response.Data.CreatedAt);
                DateTime payment = Utils.Utils.GetStringFormatDateTimeHour(response.Data.PaymentDate);
                TimeContent = string.Format("{0} {1} - {2}", Utils.Utils.GetDateFormatVN(payment), Utils.Utils.GetHourFormatVN(createAt), Utils.Utils.GetHourFormatVN(payment));
                if (response.Data.CustomerId > 0)
                {
                    CustomerName = response.Data.CustomerName; 
                    CustomerPhone = response.Data.CustomerPhone;// toan
                    CustomerAddress = response.Data.CustomerAddress;//toan
                    EmployeeGiveQrCode = response.Data.EmployeeGiveQrCodeName;
                }
                #region toan
                if (response.Data.TableId == 0)
                {
                    CustomerName = response.Data.ShippingReceiverName;    
                    CustomerPhone = response.Data.ShippingPhone;
                    CustomerAddress = response.Data.ShippingAddress;
                }
                #endregion
                EmployeeName = response.Data.EmployeeName;
                EmployeePayment = currentUser.Name;
                
                CustomerSlot = response.Data.CustomerSlotNumber;
                if (response.Data.OrderStatus == (int)OrderStatusEnum.DONE)
                {
                    ImageStatus = "pack://application:,,,/TechresStandaloneSale;component/Resources/Images/moc_thu_tien.png";
                    ImageStatusVisibility = Visibility.Visible;
                    OrderStatusVisibility = Visibility.Collapsed;
                }
                else
                {
                    switch (response.Data.OrderStatus)
                    {
                        case (int)OrderStatusEnum.OPENING:
                            {
                                OrderStatus = MessageValue.MESSAGE_ORDER_OPENING;
                                break;
                            }
                        case (int)OrderStatusEnum.WAITING_PAYMENT:
                            {
                                OrderStatus = MessageValue.MESSAGE_ORDER_PAYMENT;
                                break;
                            }
                        case (int)OrderStatusEnum.DONE:
                            {
                                OrderStatus = MessageValue.MESSAGE_ORDER_DONE_ENUM;
                                break;
                            }
                        case (int)OrderStatusEnum.MERGED:
                            {
                                OrderStatus = MessageValue.MESSAGE_ORDER_MERGED;
                                break;
                            }
                        case (int)OrderStatusEnum.WAITING_COMPLETE:
                            {
                                OrderStatus = MessageValue.MESSAGE_ORDER_COMPLETE;
                                break;
                            }
                        case (int)OrderStatusEnum.DEBIT:
                            {
                                  if (response.Data.CustomerDebtId > 0)
                                {
                                    OrderStatus = string.Format("{0} - Khách hàng: {1}", MessageValue.MESSAGE_ORDER_DEBIT, response.Data.CustomerDebtName) ;

                                }
                                else if (response.Data.EmployeeDebtId>0)
                                {
                                    OrderStatus = string.Format("{0} - Nhân viên: {1}", MessageValue.MESSAGE_ORDER_DEBIT, response.Data.EmployeeDebtName);
                                }
                                break;
                            }
                        default:
                            {
                                OrderStatus = "Bàn trống";
                                break;
                            }
                    }
                }

            }
        }
        public void PrintBill(string Title, long orderId)
        {
            if (orderId <= 0)
            {
                NotificationMessage.Error(MessageValue.MESSAGE_PRINT_FAIL);
            }
            else
            {
                OrdersClient ordersClient = new OrdersClient(this, this, this);
                OrderItemResponse response = ordersClient.GetOrderById(orderId, currentBranch != null ? currentBranch.Id : currentUser.BranchId, Constants.NOT_STATUS, Constants.STATUS);
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
                {
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                    {
                        pt.Print(Title, response, device.CasherPrinter, device.CasherSize, currentBranch);


                    }
                }
            }
        }
        public OrderViewViewModel(long orderid, long branchId)
        {
            try
            {
                GetOrderView(orderid, branchId);

                ExportExcelCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {

                    string filePath = "";
                    // tạo SaveFileDialog để lưu file excel
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.FileName = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW, OrderCode);
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
                        Branch branchTMP = null;
                        BranchClient branchClient = new BranchClient(this, this, this);
                        BranchResponse branchesResponse = branchClient.getBranchInfo(branchId);
                        if (branchesResponse != null && branchesResponse.Status == (int)ResponseEnum.OK)
                        {
                            branchTMP = branchesResponse.Data;
                        }
                        using (ExcelPackage exp = new ExcelPackage())
                        {

                            // đặt tên người tạo file
                            exp.Workbook.Properties.Author = Constants.MESSAGE_EXCEL_AUTHOR;

                            // đặt tiêu đề cho file
                            exp.Workbook.Properties.Title = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW, OrderCode);

                            //Tạo một sheet để làm việc trên đó
                            exp.Workbook.Worksheets.Add(OrderCode);

                            // lấy sheet vừa add ra để thao tác
                            ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                            // đặt tên cho sheet
                            ws.Name = OrderCode;
                            // fontsize mặc định cho cả sheet
                            ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                            // font family mặc định cho cả sheet
                            ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                            // Tạo danh sách các column header
                            string[] arrColumnHeader = {
                                                MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_NAME_OF_FOOD,
                                                MessageValue.MESSAGE_FROM_FOOD_UNIT,
                                                MessageValue.MESSAGE_FROM_LIST_QUANTITY,
                                                MessageValue.MESSAGE_FROM_LIST_UNIT_AMOUNT,
                                                MessageValue.MESSAGE_FROM_LIST_TOTAL_AMOUNT,
                };
                            var countColHeader = arrColumnHeader.Count();


                            ws.Cells[1, 1].Value = currentBranch.Name;

                            ws.Cells[1, 1, 1, countColHeader].Merge = true;
                            // in đậm
                            ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                            // căn giữa
                            ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[2, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_TITLE;
                            ws.Cells[2, 1].Style.Font.Size = 20;
                            ws.Cells[2, 1, 2, countColHeader].Merge = true;
                            // in đậm
                            ws.Cells[2, 1, 2, countColHeader].Style.Font.Bold = true;
                            // căn giữa
                            ws.Cells[2, 1, 2, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[3, 1].Value = currentBranch.Address;

                            ws.Cells[3, 1, 3, countColHeader].Merge = true;

                            ws.Cells[3, 1, 3, countColHeader].Style.WrapText = true;
                            // in đậm
                            ws.Cells[3, 1, 3, countColHeader].Style.Font.Bold = true;
                            // căn giữa
                            ws.Cells[3, 1, 3, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            //dòng thứ 4
                            ws.Cells[4, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_TABLE + " " + string.Format("{0} - {1}", TableName, OrderCode);
                            ws.Cells[4, 1, 4, 2].Merge = true;
                            ws.Cells[4, 3].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_CUSTOMER_SLOT + " " + CustomerSlot;
                            ws.Cells[4, 3, 4, 4].Merge = true;
                            // dòng thứ 5
                            ws.Cells[5, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_CASHIER + " " + EmployeePayment;
                            ws.Cells[5, 1, 5, countColHeader].Merge = true;

                            ws.Cells[6, 1].Value = MessageValue.MESSAGE_FROM_EMPLOYEE_FILTER + " " + EmployeeName;
                            ws.Cells[6, 1, 6, countColHeader].Merge = true;

                            ws.Cells[7, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_DATE_PAYMENT + " " + TimeContent;
                            ws.Cells[7, 1, 7, countColHeader].Merge = true;


                            int colIndex = 1;
                            int rowIndex = 8;

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

                            foreach (var item in ItemBills)
                            {
                                if (item.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK && item.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL)
                                {
                                    colIndex = 1;
                                    rowIndex++;
                                    ws.Cells[rowIndex, colIndex].Value = item.FoodName;
                                    ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    colIndex++;
                                    ws.Cells[rowIndex, colIndex].Value = item.FoodUnit;
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

                                    ws.Cells[rowIndex, colIndex].Value = item.UnitPrice;
                                    ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,###";
                                    ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    colIndex++;
                                    if (item.IsGift == 1)
                                        ws.Cells[rowIndex, colIndex].Value = "0";
                                    else
                                        ws.Cells[rowIndex, colIndex].Value = item.TotalPrice;
                                    ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,###";
                                    ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                         ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                    ws.Cells.AutoFitColumns();
                                }
                            }
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_TO_MONEY;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);

                            ws.Cells[rowIndex, 5].Value = Amount;
                            ws.Cells[rowIndex, 5].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            ws.Cells[rowIndex, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_DISCOUNT;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            ws.Cells[rowIndex, 5].Value = DiscountAmount;
                            ws.Cells[rowIndex, 5].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            ws.Cells[rowIndex, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_VAT;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            ws.Cells[rowIndex, 5].Value = VAT;
                            ws.Cells[rowIndex, 5].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            ws.Cells[rowIndex, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_AMOUNT;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 1, rowIndex, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 1, rowIndex, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            ws.Cells[rowIndex, 5].Value = TotalAmount;
                            ws.Cells[rowIndex, 5].Style.Border.Bottom.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Top.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Left.Style =
                               ws.Cells[rowIndex, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            ws.Cells[rowIndex, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_THANKS;

                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Merge = true;
                            // in đậm
                            //    ws.Cells[rowIndex, 1, 1, countColHeader].Style.Font.Bold = true;
                            // căn giữa
                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            rowIndex++;

                            string branchPhone = branchTMP != null ? branchTMP.Phone : "";
                            ws.Cells[rowIndex, 1].Value = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_PHONE, branchPhone);

                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Merge = true;
                            // in đậm
                            //    ws.Cells[rowIndex, 1, 1, countColHeader].Style.Font.Bold = true;
                            // căn giữa
                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            rowIndex++;
                            ws.Cells[rowIndex, 1].Value = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VIEW_PR_TECHRES);

                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Merge = true;
                            // in đậm
                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.Font.Italic = true;
                            // căn giữa
                            ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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
                    }

                });
                BtnViewOrderDetail = new RelayCommand<Models.BillResponse>((p) => { return true; }, p =>
               {
                   if (p.IsExtraCharge == Constants.NOT_STATUS)
                   {
                       OrderDetailViewWindow window = new OrderDetailViewWindow();
                       window.DataContext = new OrderDetailViewViewModel(p.Id, p.IsExtraCharge);
                       window.ShowDialog();

                   }
                   else
                   {
                       OrderDetailExtraChargeViewWindow window = new OrderDetailExtraChargeViewWindow();
                       window.DataContext = new OrderDetailViewViewModel(p.Id, p.IsExtraCharge);
                       window.ShowDialog();
                   };
               });
                BtnPrintOrder = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string Title = MessageValue.MESSAGE_PRINT_BILL_TITLE;
                    string contentConfirm = MessageValue.MESSAGE_PRINT_BILL;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        string TitleBILL = "HÓA ĐƠN THANH TOÁN";

                        PrintBill(TitleBILL, orderid);
                    }
                });
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public OrderViewViewModel() { }
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

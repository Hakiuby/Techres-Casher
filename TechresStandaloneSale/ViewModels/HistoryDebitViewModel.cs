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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class HistoryDebitViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<Debit> _HistoryDebitLists = new ObservableCollection<Debit>();
        public ObservableCollection<Debit> HistoryDebitLists { get => _HistoryDebitLists; set { _HistoryDebitLists = value; OnPropertyChanged("HistoryDebitLists"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }

        private Visibility _BranchVisibility;
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }

        private int BrandId;
        private long BranchId;
        private DateTime _DateTimeFromInput;
        public DateTime DateTimeFromInput { get => _DateTimeFromInput; set { _DateTimeFromInput = value; OnPropertyChanged("DateTimeFromInput"); } }
        private DateTime _DateTimeToInput;
        public DateTime DateTimeToInput { get => _DateTimeToInput; set { _DateTimeToInput = value; OnPropertyChanged("DateTimeToInput"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private int CurrentPage;
        public long TotalRecord; 
        private int TotalPage;
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
        private string _TitleContent { get; set; }
        public string TitleContent { get => _TitleContent; set { _TitleContent = value; OnPropertyChanged("TitleContent"); } }
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
        private long _Count; 
        public long Count { get => _Count; set { _Count = value; OnPropertyChanged("Count"); } }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand ExportCommand { get; set; }
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
        public async void GetData(int brandId,long branchId, string from, string to, int page)
        {
            if (HistoryDebitLists == null)
            {
                HistoryDebitLists = new ObservableCollection<Debit>();
            }
            else
            {
                HistoryDebitLists.Clear();
            }
            DialogHostOpen = true;
            OrdersClient orderClient = new OrdersClient(this, this, this);
            DebitResponse response = await System.Threading.Tasks.Task.Run(()=> orderClient.GetHistoryDebit(from, to, brandId, branchId, Constants.ALL, page));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                TotalRecord = response.Data.TotalRecord; 
                response.Data.List.ForEach(HistoryDebitLists.Add);
                if (response.Data.TotalRecord % response.Data.Limit != 0)
                {
                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit) + 1;
                }
                else
                {
                    TotalPage = (int)(response.Data.TotalRecord / response.Data.Limit);
                }
                PageContent = CurrentPage + "/" + TotalPage;
                DialogHostOpen = false;
                TitleContent = string.Format("LỊCH SỬ GHI NỢ ĐƠN HÀNG CỦA NHÂN VIÊN ({0})", response.Data.TotalRecord);
            }
            else
                DialogHostOpen = false;
        }
        public HistoryDebitViewModel()
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
                BranchVisibility =   Visibility.Collapsed;
                BrandId = currentUser.RestaurantBrandId;
                BranchId = currentUser.BranchId;
                //BranchItem = BranchList.Where(x => x.Id == currentUser.BranchId).FirstOrDefault();
            }
            CurrentPage = Constants.OFFSET;
            DateTimeFromInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTimeToInput = DateTime.Now;
            
            //TitleContent = string.Format("LỊCH SỬ GHI NỢ ĐƠN HÀNG CỦA NHÂN VIÊN ({0})", Count);
            GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
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
                    if (HistoryDebitLists == null)
                    {
                        HistoryDebitLists = new ObservableCollection<Debit>();
                    }
                    else
                    {
                        HistoryDebitLists.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                //if(BranchItem == null)
                //{

                //}
                //if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                //{
                //    GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id,BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                //}
                //if ( BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                //{
                //    GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                //}
                GetData(currentUser.RestaurantBrandId, currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage); 

            });
            ExportCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {

                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = TitleContent;
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
                        exp.Workbook.Properties.Title = TitleContent;

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_DEBIT_SHEET_NAME);

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = MessageValue.MESSAGE_FROM_DEBIT_SHEET_NAME;
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                               MessageValue.MESSAGE_FROM_LIST_DAY,
                                                 MessageValue.MESSAGE_FROM_LIST_EMPLOYEES_NAME,
                                                 MessageValue.MESSAGE_FROM_LIST_ORDER_CODE,
                                               MessageValue.MESSAGE_FROM_LIST_TABLE,
                                                  MessageValue.MESSAGE_FROM_MONEY_NUNMBER
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = TitleContent;
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

                        // lấy ra danh sách UserInfo từ ItemSource của DataGrid


                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in HistoryDebitLists)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.DebtTime;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            ws.Cells[rowIndex, colIndex++].Value = item.Employee.Name;
                            ws.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                            ws.Cells[rowIndex, colIndex++].Value = item.TableName;
                            ws.Cells[rowIndex, colIndex++].Value = item.DebitAmountString;
                            //    ws.Cells[rowIndex, colIndex++].Value = item.Time;
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
            PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
            CurrentPage = 1;

            GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
             });

            PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {

                CurrentPage = CurrentPage - 1;

                GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
            });

            PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CurrentPage = CurrentPage + 1;

                GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
            });

            PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {

                CurrentPage = TotalPage;

                GetData(BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
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
            return default(T);
        }

    }
}
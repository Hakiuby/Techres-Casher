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
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class HistoryEndWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public DateTime currentDatetime;
        private ObservableCollection<RevenueFinishWorkingSession> _HistoryEndWorkingSessionLists = new ObservableCollection<RevenueFinishWorkingSession>();
        public ObservableCollection<RevenueFinishWorkingSession> HistoryEndWorkingSessionLists { get => _HistoryEndWorkingSessionLists; set { _HistoryEndWorkingSessionLists = value; OnPropertyChanged("HistoryEndWorkingSessionLists"); } }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }

        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private long BranchId;
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }

        private DateTime _DateTimeFromInput;
        public DateTime DateTimeFromInput
        {
            get
            {
                return _DateTimeFromInput;
            }
            set
            {
                _DateTimeFromInput = value;
                OnPropertyChanged("DateTimeFromInput");
            }
        }
        private DateTime _DateTimeToInput;
        public DateTime DateTimeToInput
        {
            get
            {
                return _DateTimeToInput;
            }
            set
            {
                _DateTimeToInput = value;
                OnPropertyChanged("DateTimeToInput");
            }
        }
        private string _TitleContent;
        public string TitleContent
        {
            get
            {
                return _TitleContent;
            }
            set
            {
                _TitleContent = value;
                OnPropertyChanged("TitleContent");
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
        public SettingData curentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public ICommand SelectionChangedCommand { get; set; }

        public ICommand SelectionDateChangedCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        
        private int CurrentPage;

        private int TotalPage;
        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
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
        public async void GetHistoryEndWorkingSession(int brandId,long branchId, string fromDate,string toDate, int page)
        {
            if (HistoryEndWorkingSessionLists != null)
            {
                HistoryEndWorkingSessionLists.Clear();
            }
            else
            {
                HistoryEndWorkingSessionLists = new ObservableCollection<RevenueFinishWorkingSession>();
            }
            DialogHostOpen = true;
            WorkingSessionClient reportClient = new WorkingSessionClient(this, this, this);
            HistoryEndWorkingSessionResponse historyEndWorkingSession = await System.Threading.Tasks.Task.Run(()=> reportClient.HistoryEndWorkingSession(brandId, branchId,fromDate,toDate, page));
            if (historyEndWorkingSession != null && historyEndWorkingSession.Status == (int)ResponseEnum.OK && historyEndWorkingSession.Data != null)
            {
                TitleContent = string.Format(MessageValue.MESSAGE_FROM_END_WORKING_SESION, historyEndWorkingSession.Data.Lists.Count);
                historyEndWorkingSession.Data.Lists.ForEach(HistoryEndWorkingSessionLists.Add);
                if (historyEndWorkingSession.Data.TotalRecord % historyEndWorkingSession.Data.Limit != 0)
                {
                    TotalPage = (int)(historyEndWorkingSession.Data.TotalRecord / historyEndWorkingSession.Data.Limit) + 1;
                }
                else
                {
                    TotalPage = (int)(historyEndWorkingSession.Data.TotalRecord / historyEndWorkingSession.Data.Limit);
                }
                PageContent = CurrentPage + "/" + TotalPage;
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public HistoryEndWorkingSessionViewModel()
        {
            if (curentSetting != null && currentUser != null)
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

                CurrentPage = Constants.OFFSET;
                DateTimeFromInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTimeToInput = DateTime.Now;
                currentDatetime = DateTimeFromInput;
                TitleContent = string.Format(MessageValue.MESSAGE_FROM_END_WORKING_SESION, 0);
                GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);

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
                        if (HistoryEndWorkingSessionLists != null)
                        {
                            HistoryEndWorkingSessionLists.Clear();
                        }
                        else
                        {
                            HistoryEndWorkingSessionLists = new ObservableCollection<RevenueFinishWorkingSession>();
                        }
                    }
                    DialogHostOpen = false;
                });
                //SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                //{
                //    DialogHostOpen = true;
                //    if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                //    {
                //        BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                //        GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);

                //    }
                //});

                SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p => {
                    CurrentPage = 1;
                    DialogHostOpen = true;
                    //int brandId = unchecked((int)currentUser.BranchId); 
                    GetHistoryEndWorkingSession(currentUser.RestaurantBrandId,currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage); 
                
                });  
                ViewCommand = new RelayCommand<RevenueFinishWorkingSession>((p) => { return true; }, p =>
                {
                    if (p != null)
                    {
                        HistoryEndWorkingSessionViewWindow window = new HistoryEndWorkingSessionViewWindow();
                        window.DataContext = new RevenueFinishWorkingSessionViewModel(p.Id, BranchItem != null ? BranchItem.Id : currentUser.BranchId,p.OpenEmployeeName);
                        window.ShowDialog();                        
                    }
                });
               
                PageDoubleLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1;

                    GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                });

                PageLeft = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {

                    CurrentPage = CurrentPage - 1;
                    if (CurrentPage > 0)
                    {
                        GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                    }
                    else
                    {
                        CurrentPage = 1;
                        GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                    }
                });

                PageRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = CurrentPage + 1;
                    if (CurrentPage <= TotalPage)
                    {
                        GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                    }
                    else
                    {
                        CurrentPage = TotalPage;
                        GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
                    }
                });

                PageDoubleRight = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {

                    CurrentPage = TotalPage;

                    GetHistoryEndWorkingSession(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, Utils.Utils.GetDateFormatVN(DateTimeFromInput), Utils.Utils.GetDateFormatVN(DateTimeToInput), CurrentPage);
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
                            exp.Workbook.Worksheets.Add(MessageValue.MESSAGE_FROM_END_WORKING_SESION_SHEET_NAME);

                            // lấy sheet vừa add ra để thao tác
                            ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                            // đặt tên cho sheet
                            ws.Name = MessageValue.MESSAGE_FROM_END_WORKING_SESION_SHEET_NAME;
                            // fontsize mặc định cho cả sheet
                            ws.Cells.Style.Font.Size = Constants.MESSAGE_EXCEL_FONT_SIZE;
                            // font family mặc định cho cả sheet
                            ws.Cells.Style.Font.Name = Constants.MESSAGE_EXCEL_FONT_NAME;

                            // Tạo danh sách các column header
                            string[] arrColumnHeader = {
                                               MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_CODE,
                                                 MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_EMPLOYEE_NAME_OPEN,
                                                 MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_EMPLOYEE_NAME_CLOSE,
                                               MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_TIME_OPEN,
                                                  MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_TIME_CLOSE,
                                               MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_OPEN,
                                                  MessageValue.MESSAGE_FROM_END_WORKING_SESION_TOTAL_MONEY_REVENUE,
                                                   MessageValue.MESSAGE_FROM_END_WORKING_SESION_TOTAL_MONEY_EXPENDITURE,
                                                   MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_SYSTEM,
                                                 MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_ADDITION_FEE_IN,
                                                 MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_ADDITION_FEE_OUT,
                                               MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_CLOSE,
                                                  MessageValue.MESSAGE_FROM_END_WORKING_SESION_LIST_MONEY_CURRENT_CLOSE
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
                            foreach (var item in HistoryEndWorkingSessionLists)
                            {
                                // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                colIndex = 1;

                                // rowIndex tương ứng từng dòng dữ liệu
                                rowIndex++;

                                //gán giá trị cho từng cell                      
                                ws.Cells[rowIndex, colIndex++].Value = item.Code;

                                // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                                ws.Cells[rowIndex, colIndex].Value = item.OpenEmployeeName;
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.CloseEmployeeName;
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.OpenTime;
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.CloseTime;
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.BeforeCash;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.TotalInAmount;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.TotalOutAmount;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.CashAmount;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.TotalCash;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                colIndex++;
                                ws.Cells[rowIndex, colIndex].Value = item.RealAmount;
                                ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
                                ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                     ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

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
            }

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

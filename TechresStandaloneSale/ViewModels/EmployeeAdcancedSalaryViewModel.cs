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
using System.Text;
using System.Threading.Tasks;
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
using TechresStandaloneSale.Views.CreateEmployeeOfManager;
using TechresStandaloneSale.Views.Dialogs;
namespace TechresStandaloneSale.ViewModels
{
   public class EmployeeAdcancedSalaryViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand AddCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand BtnDoneCommand { get; set; }
        public ICommand BtnCancelCommand { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleLeft { get; set; }

        private ObservableCollection<EmployeeAdvancedSalary> _EmployeeAdvancedSalaryList = new ObservableCollection<EmployeeAdvancedSalary>();
        public ObservableCollection<EmployeeAdvancedSalary> EmployeeAdvancedSalaryList { get => _EmployeeAdvancedSalaryList; set { _EmployeeAdvancedSalaryList = value; OnPropertyChanged("EmployeeAdvancedSalaryList"); } }
      
       
        private string _ContentTitle;
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private Visibility _BranchVisibility;
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private long BranchId;
        private BasicModel _StatusItem;
        public BasicModel StatusItem { get => _StatusItem; set { _StatusItem = value; OnPropertyChanged("StatusItem"); } }

        private ObservableCollection<BasicModel> _StatusList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> StatusList { get => _StatusList; set { _StatusList = value; OnPropertyChanged("StatusList"); } }
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
        public EmployeeAdcancedSalaryViewModel()
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
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_SALARY_LIST,0);
            GetEmployeeAdcancedSalaryStatus();
            GetEmployeeAdcancedSalaryList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : currentUser.BranchId, StatusItem != null ? StatusItem.Value : (int)EmployeeAdvancedSalaryEnum.APPROVED);
            ExportCommand = new RelayCommand<UserControl>((t) => { return true; }, p =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = "Danh sách nhân viên ứng lương";
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
                        exp.Workbook.Properties.Author = "AloApp Company";

                        // đặt tiêu đề cho file
                        exp.Workbook.Properties.Title = "DANH SÁCH NHÂN VIÊN ỨNG LƯƠNG";

                        //Tạo một sheet để làm việc trên đó
                        exp.Workbook.Worksheets.Add("Nhân viên");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = exp.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = "Nhân viên";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "ID",
                                                "Tên nhân viên",
                                                "Số tiền",
                                                "Lý do ứng",
                                                "Bộ phận",
                                                "Tổng lương hiện tại",
                                                 "Trạng thái",
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN ỨNG LƯƠNG";
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
                        foreach (var item in EmployeeAdvancedSalaryList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.Id;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
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
                            ws.Cells[rowIndex, colIndex].Value = item.Reason;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.Employee.RoleName;
                            ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Top.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Left.Style =
                                 ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            colIndex++;
                            ws.Cells[rowIndex, colIndex].Value = item.TotalSalary;
                            ws.Cells[rowIndex, colIndex].Style.Numberformat.Format = "#,###,##0";
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
                    if (BranchList.Count > 0) BranchItem = BranchList[0];
                    if (EmployeeAdvancedSalaryList == null)
                    {
                        EmployeeAdvancedSalaryList = new ObservableCollection<EmployeeAdvancedSalary>();
                    }
                    else
                    {
                        EmployeeAdvancedSalaryList.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                    GetEmployeeAdcancedSalaryList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, StatusItem != null ? StatusItem.Value : (int)EmployeeAdvancedSalaryEnum.APPROVED);

                }
                else if(BrandItem == null)
                {
                    GetEmployeeAdcancedSalaryList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, StatusItem != null ? StatusItem.Value : (int)EmployeeAdvancedSalaryEnum.APPROVED);
                }
            });
            BtnDoneCommand = new RelayCommand<EmployeeAdvancedSalary>((p) => { return true; }, (p) =>
            {
                if (p!= null)
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string Title = MessageValue.MESSAGE_EMPLOYEE_ADVANCED_SALARY_PAY_TITLE;
                    string contentConfirm = string.Format(MessageValue.MESSAGE_EMPLOYEE_ADVANCED_SALARY_PAY, p.Employee.Name);
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        EmployeeClient client = new EmployeeClient(this, this, this);
                        BaseResponse employee = client.PayEmployeeAdvancedSalary(new AddvanedSalaryWrapper(p.BranchId, p.Id, currentUser.Id));
                        if (employee != null && employee.Status == (int)ResponseEnum.OK )
                        {
                            p.Status =(int)EmployeeAdvancedSalaryEnum.PAID;
                            p.StatusText = "Đã chi tiền";
                            EmployeeAdvancedSalaryList.Remove(p);
                            EmployeeAdvancedSalaryList.Add(p);
                            DeviceClient deviceClient = new DeviceClient();
                            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                            DeviceConfigWrapper device = deviceClient.RealConfigs();
                            if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                            {
                               pt.PrintTmpSalaryEmployee(p, device.CasherPrinter, device.CasherSize);
                            }
                        }
                        else
                        {
                            GetEmployeeAdcancedSalaryList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : Constants.CURRENT_BRANCH_ID, StatusItem != null ? StatusItem.Value: (int)EmployeeAdvancedSalaryEnum.APPROVED); 
                        }
                    }
                }
                
            });

            BtnCancelCommand = new RelayCommand<EmployeeAdvancedSalary>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string Title = MessageValue.MESSAGE_EMPLOYEE_ADVANCED_SALARY_CANCEL_TITLE;
                    string contentConfirm = string.Format(MessageValue.MESSAGE_EMPLOYEE_ADVANCED_SALARY_CANCEL, p.Employee.Name);
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        EmployeeClient client = new EmployeeClient(this, this, this);
                        EmployeeAdvancedSalaryData employee = client.CancelEmployeeAdvancedSalary(new AddvanedSalaryWrapper(p.BranchId, p.Id, currentUser.Id));
                        if (employee != null && employee.Status == (int)ResponseEnum.OK && employee.Data != null)
                        {
                            EmployeeAdvancedSalaryList.Remove(p);
                            EmployeeAdvancedSalaryList.Add(employee.Data);
                        }
                    }
                }
            });
       

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
            }
        }
        public void GetEmployeeAdcancedSalaryStatus()
        {
            if (StatusList == null)
            {
                StatusList = new ObservableCollection<BasicModel>();
            }
            else
            {
                StatusList.Clear();
            }
            StatusList.Add(new BasicModel((int)EmployeeAdvancedSalaryEnum.ALL, MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_ALL));
            StatusList.Add(new BasicModel((int)EmployeeAdvancedSalaryEnum.PENDING, MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_PENDING));
            StatusList.Add(new BasicModel((int)EmployeeAdvancedSalaryEnum.APPROVED, MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_APPROVED));
            StatusList.Add(new BasicModel((int)EmployeeAdvancedSalaryEnum.PAID, MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_PAID));
            StatusList.Add(new BasicModel((int)EmployeeAdvancedSalaryEnum.REJECTED, MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_REJECTED));
            StatusItem = StatusList[2];

        }

        public async void GetEmployeeAdcancedSalaryList(int BrandId,long branchId, int status)
        {
            if (EmployeeAdvancedSalaryList == null)
            {
                EmployeeAdvancedSalaryList = new ObservableCollection<EmployeeAdvancedSalary>();
            }
            else
            {
                EmployeeAdvancedSalaryList.Clear();
            }
            //CurrentPage = page;
            DialogHostOpen = true;
            EmployeeClient client = new EmployeeClient(this, this, this);
            EmployeeAdvancedSalaryResponse response = await Task.Run(()=> client.GetEmployeeAdvancedSalaryResponses(BrandId,branchId, status));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_EMPLOYEE_ADVANCED_SALARY_LIST, response.Data.Count);
                response.Data.ForEach(EmployeeAdvancedSalaryList.Add);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
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

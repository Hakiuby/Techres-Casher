using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.ViewModels.CreateEmployeeOfManager;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.CreateEmployeeOfManager;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class DebitOrderEmployeeViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand AddEmployeeCommad { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EnterTextBoxEmployeeCommand { get; set; }
        public ICommand DownTextBoxEmployeeCommand { get; set; }
        public ICommand UpTextBoxEmployeeCommand { get; set; }
        public ICommand BtnAddEmployeeCommand { get; set; }
        public ICommand EnterEmployeeCommand { get; set; }
        private bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }
        private ObservableCollection<Employee> _EmployeeList = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged("EmployeeList"); } }

        private string _CodeOrder;
        public string CodeOrder { get => _CodeOrder; set { _CodeOrder = value; OnPropertyChanged("CodeOrder"); } }


        private string _TotalAmount;
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }


        private Employee _EmployeeItem;
        public Employee EmployeeItem { get => _EmployeeItem; set { _EmployeeItem = value; OnPropertyChanged("EmployeeItem"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private int _EmployeeIndex;
        public int EmployeeIndex { get => _EmployeeIndex; set { _EmployeeIndex = value; OnPropertyChanged("EmployeeIndex"); } }
        private bool _IsOpenPopupEmployee;
        public bool IsOpenPopupEmployee { get => _IsOpenPopupEmployee; set { _IsOpenPopupEmployee = value; OnPropertyChanged("IsOpenPopupEmployee"); } }

        private string _EmployeeText;
        public string EmployeeText { get => _EmployeeText; set { _EmployeeText = value; OnPropertyChanged("EmployeeText"); } }
        public Employee employee;
        #region Đạt
        public bool isSuccess;
        #endregion

        public DebitOrderEmployeeViewModel(CardOrderItem cardOrderItem)
        {
            if (currentUser != null)
            {
                isSuccess = false; // Đạt
                GetEmployeeList(currentUser.RestaurantBrandId, currentUser.BranchId);
                CodeOrder = string.Format("{0} - {1}", cardOrderItem.OrderCode, cardOrderItem.TableName);
                TotalAmount = string.Format("{0}VND", cardOrderItem.CashAmountString);
                EmployeeItem = EmployeeList.Where(x => x.Id == cardOrderItem.EmployeeId).FirstOrDefault();
                DownTextBoxEmployeeCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    try
                    {
                        if (EmployeeIndex < EmployeeList.Count())
                        {
                            EmployeeIndex++;
                            // EmployeeItem = EmployeeList[EmployeeIndex];
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                UpTextBoxEmployeeCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    try
                    {
                        if (EmployeeIndex > 0)
                        {
                            EmployeeIndex--;
                            //  EmployeeItem = EmployeeList[EmployeeIndex];
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                BtnAddEmployeeCommand = new RelayCommand<Employee>((p) => { return true; }, p =>
                {
                    try
                    {
                        if (p != null)
                        {
                            employee = p;
                            EmployeeText = p.NameRoleNameString;
                            IsOpenPopupEmployee = false;
                            // IsOpenPopup = true;
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                EnterTextBoxEmployeeCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    try
                    {
                        if (EmployeeItem != null)
                        {
                            employee = EmployeeItem;
                            EmployeeText = EmployeeItem.NameRoleNameString;
                            IsOpenPopupEmployee = false;
                            //   IsOpenPopup = true;
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                EnterEmployeeCommand = new RelayCommand<Popup>((p) => { return true; }, p =>
                {
                    try
                    {
                        if (EmployeeItem != null)
                        {
                            employee = EmployeeItem;
                            EmployeeText = EmployeeItem.NameRoleNameString;
                            IsOpenPopupEmployee = false;
                            //    IsOpenPopup = true;
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                CloseCommand = new RelayCommand<DebitOrderEmployeeWindow>((p) => { return true; }, (p) =>
                {
                    p.Close();
                });

                AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                      
                        if (employee == null)
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_NOT_SELECT_EMPLOYEE);
                        }
                        else
                        {
                            DebitEmployeeWrapper wrapper = new DebitEmployeeWrapper(employee.Id, cardOrderItem.OrderId, string.IsNullOrEmpty(Note) ? "" : Note, currentUser.BranchId);
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            DialogHostOpen = true;
                            // BaseResponse response = ordersClient.UpdateDebitOrderEmployee(cardOrderItem.OrderId, wrapper);
                            BaseResponse response = await Task.Run(() => ordersClient.UpdateDebitOrderEmployee(cardOrderItem.OrderId, wrapper)); 
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                p.Close();
                                string title = "NỢ BILL NHÂN VIÊN";
                                PrintBill(title, cardOrderItem.OrderId);
                                NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                                DialogHostOpen = false;
                                isSuccess = true; // Đạt
                            }

                        }
                    }); 
                });
            }
        }
        public void GetEmployeeList(int brandId,long branchId)
        {   
            if (EmployeeList == null)
            {
                EmployeeList = new ObservableCollection<Employee>();
            }
            else
            {
                EmployeeList.Clear();
            }
            EmployeeClient client = new EmployeeClient(this, this, this);
            EmployeeResponses employees = client.GetAllEmployeesDebitResponses();
            if (employees != null)
            {
                employees.Data.List.ForEach(EmployeeList.Add);
            }
        }
        public DeviceClient deviceClient = new DeviceClient();
       // public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void PrintBill(string Title, long orderId)
        {
            if (orderId <= 0)
            {
                NotificationMessage.Error(MessageValue.MESSAGE_PRINT_FAIL);
            }
            else
            {
                OrdersClient ordersClient = new OrdersClient(this, this, this);
                OrderItemResponse response = ordersClient.GetOrderById(orderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.STATUS);
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
                {
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                    {
                        Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
                        pt.Print(Title, response, device.CasherPrinter, device.CasherSize, currentBranch);
                    }
                }
            }
        }

        //public void GetDetail(long orderId = 0, long branchId)
        //{
        //    OrdersClient client = new OrdersClient(this, this, this);
        //    OrderItemResponse order = client.GetOrderById(orderId, currentUser.);
        //    if (order != null && order.Status == (int)ResponseEnum.OK && order.WarehouseSessionData != null)
        //    {
        //        CodeOrder = string.Format("{0} - {1}", order.WarehouseSessionData.OrderCode, order.WarehouseSessionData.TableName);
        //        TotalAmount = string.Format("{0}VND", order.WarehouseSessionData.TotalAmountString);
        //        EmployeeItem = EmployeeList.Where(x => x.Id == order.WarehouseSessionData.Employee.Id).FirstOrDefault();
        //    }
        //}
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

        public void LogError(Exception ex, string infoMessage)
        {
        }

    }
}

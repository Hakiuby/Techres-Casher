using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using TechresStandaloneSale.UserControlView.ComboBox;
using TechresStandaloneSale.ViewModels.Booking;
using TechresStandaloneSale.ViewModels.CreateEmployeeOfManager;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.CreateEmployeeOfManager;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Warehouse
{
    public class ManagePaymentSlipViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        private ObservableCollection<WarehouseSession> _MaterialWarehouseList = new ObservableCollection<WarehouseSession>();
        public ObservableCollection<WarehouseSession> MaterialWarehouseList
        {
            get
            {
                return _MaterialWarehouseList;
            }
            set
            {
                _MaterialWarehouseList = value;
                OnPropertyChanged("MaterialWarehouseList");
            }
        }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private ObservableCollection<PaymentReason> _CategoryNoteList = new ObservableCollection<PaymentReason>();
        public ObservableCollection<PaymentReason> CategoryNoteList { get => _CategoryNoteList; set { _CategoryNoteList = value; OnPropertyChanged("CategoryNoteList"); } }

        private PaymentReason _CategoryNoteItem;
        public PaymentReason CategoryNoteItem { get => _CategoryNoteItem; set { _CategoryNoteItem = value; OnPropertyChanged("CategoryNoteItem"); } }

        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }

        private Visibility _BranchVisibility;
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }

        private bool _IsAllItemsSelected;
        public bool IsAllItemsSelected { get => _IsAllItemsSelected; set { _IsAllItemsSelected = value; OnPropertyChanged("IsAllItemsSelected"); } }

        private long _WarehouseSessionQuantity;
        public long WarehouseSessionQuantity { get => _WarehouseSessionQuantity; set { _WarehouseSessionQuantity = value; OnPropertyChanged("WarehouseSessionQuantity"); } }
        private string _WarehouseSessionToTalAmount;
        public string WarehouseSessionToTalAmount { get => _WarehouseSessionToTalAmount; set { _WarehouseSessionToTalAmount = value; OnPropertyChanged("WarehouseSessionToTalAmount"); } }


        private DateTime _TradingDay;
        public DateTime TradingDay { get => _TradingDay; set { _TradingDay = value; OnPropertyChanged("TradingDay"); } }
        private TimeSpan _TradingTime;
        public TimeSpan TradingTime { get => _TradingTime; set { _TradingTime = value; OnPropertyChanged("TradingTime"); } }


        private DateTime _TradingDayDisplayDateEnd;
        public DateTime TradingDayDisplayDateEnd { get => _TradingDayDisplayDateEnd; set { _TradingDayDisplayDateEnd = value; OnPropertyChanged("TradingDayDisplayDateEnd"); } }

        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }

        private ObservableCollection<BasicModel> _PaymentTypeList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> PaymentTypeList { get => _PaymentTypeList; set { _PaymentTypeList = value; OnPropertyChanged("PaymentTypeList"); } }
        private ObservableCollection<BasicModel> _ObjectTypeList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> ObjectTypeList { get => _ObjectTypeList; set { _ObjectTypeList = value; OnPropertyChanged("ObjectTypeList"); } }
        private BasicModel _PaymentTypeItem;
        public BasicModel PaymentTypeItem { get => _PaymentTypeItem; set { _PaymentTypeItem = value; OnPropertyChanged("PaymentTypeItem"); } }

        private BasicModel _ObjectTypeItem;
        public BasicModel ObjectTypeItem { get => _ObjectTypeItem; set { _ObjectTypeItem = value; OnPropertyChanged("ObjectTypeItem"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        private ObservableCollection<Employee> _EmployeeList = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged("EmployeeList"); } }

        private ObservableCollection<Supplier> _SupplierList = new ObservableCollection<Supplier>();
        public ObservableCollection<Supplier> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged("SupplierList"); } }

        private ObservableCollection<Customer> _CustomerList = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged("CustomerList"); } }
        private Customer _CustomerItem;
        public Customer CustomerItem { get => _CustomerItem; set { _CustomerItem = value; OnPropertyChanged("CustomerItem"); } }

        private Employee _EmployeeItem;
        public Employee EmployeeItem { get => _EmployeeItem; set { _EmployeeItem = value; OnPropertyChanged("EmployeeItem"); } }

        private Supplier _SupplierItem;
        public Supplier SupplierItem { get => _SupplierItem; set { _SupplierItem = value; OnPropertyChanged("SupplierItem"); } }
        private string _TextOther;
        public string TextOther { get => _TextOther; set { _TextOther = value; OnPropertyChanged("TextOther"); } }
        private int _SupplierIndex;
        public int SupplierIndex { get => _SupplierIndex; set { _SupplierIndex = value; OnPropertyChanged("SupplierIndex"); } }
        private bool _IsOpenPopupSupplier;
        public bool IsOpenPopupSupplier { get => _IsOpenPopupSupplier; set { _IsOpenPopupSupplier = value; OnPropertyChanged("IsOpenPopupSupplier"); } }

        private string _SupplierText;
        public string SupplierText { get => _SupplierText; set { _SupplierText = value; OnPropertyChanged("SupplierText"); } }

        private int _CustomerIndex;
        public int CustomerIndex { get => _CustomerIndex; set { _CustomerIndex = value; OnPropertyChanged("CustomerIndex"); } }
        private bool _IsOpenPopupCustomer;
        public bool IsOpenPopupCustomer { get => _IsOpenPopupCustomer; set { _IsOpenPopupCustomer = value; OnPropertyChanged("IsOpenPopupCustomer"); } }

        private string _CustomerText;
        public string CustomerText { get => _CustomerText; set { _CustomerText = value; OnPropertyChanged("CustomerText"); } }

        private int _EmployeeIndex;
        public int EmployeeIndex { get => _EmployeeIndex; set { _EmployeeIndex = value; OnPropertyChanged("EmployeeIndex"); } }
        private bool _IsOpenPopupEmployee;
        public bool IsOpenPopupEmployee { get => _IsOpenPopupEmployee; set { _IsOpenPopupEmployee = value; OnPropertyChanged("IsOpenPopupEmployee"); } }

        private string _EmployeeText;
        public string EmployeeText { get => _EmployeeText; set { _EmployeeText = value; OnPropertyChanged("EmployeeText"); } }
        private bool _IsCheckAccounting;
        public bool IsCheckAccounting { get => _IsCheckAccounting; set { _IsCheckAccounting = value; OnPropertyChanged("IsCheckAccounting"); } }
        private Visibility _ObjectTypeVisibility;
        public Visibility ObjectTypeVisibility { get => _ObjectTypeVisibility; set { _ObjectTypeVisibility = value; OnPropertyChanged("ObjectTypeVisibility"); } }
      
        private string _TextContent;
        public string TextContent { get => _TextContent; set { _TextContent = value; OnPropertyChanged("TextContent"); } }
        private Visibility _AddVisibility;
        public Visibility AddVisibility { get => _AddVisibility; set { _AddVisibility = value; OnPropertyChanged("AddVisibility"); } }
        private Visibility _DateTimeVisibility;
        public Visibility DateTimeVisibility { get => _DateTimeVisibility; set { _DateTimeVisibility = value; OnPropertyChanged("DateTimeVisibility"); } }

        private string _CustomerInfor { get; set; }
        public string CustomerInfor { get => _CustomerInfor; set { _CustomerInfor = value; OnPropertyChanged("CustomerInfor"); } }
        #region Đạt
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged("CustomerName"); } }
        private string _CustomerFirstName { get; set; }
        public string CustomerFirstName { get => _CustomerFirstName; set { _CustomerFirstName = value; OnPropertyChanged("CustomerFirstName"); } }
        private string _CustomerLastName { get; set; }
        public string CustomerLastName { get => _CustomerLastName; set { _CustomerLastName = value; OnPropertyChanged("CustomerLastName"); } }
        #endregion
        public ICommand CancelCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ICommand AddCategoryNoteCommand { get; set; }
        public ICommand SelectionObjectChangedCommand { get; set; }
        public ICommand SelectionPaymentReasonChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand AddCustomerCommad { get; set; }
        public ICommand AddEmployeeCommad { get; set; }
        public ICommand AddSupplierCommad { get; set; }
        public ICommand ByCusByPhone { get; set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand SupplierChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand EnterTextBoxSupplierCommand { get; set; }
        public ICommand DownTextBoxSupplierCommand { get; set; }
        public ICommand UpTextBoxSupplierCommand { get; set; }
        public ICommand BtnAddSupplierCommand { get; set; }
        public ICommand EnterSupplierCommand { get; set; }
        public ICommand EnterTextBoxEmployeeCommand { get; set; }
        public ICommand DownTextBoxEmployeeCommand { get; set; }
        public ICommand UpTextBoxEmployeeCommand { get; set; }
        public ICommand BtnAddEmployeeCommand { get; set; }
        public ICommand EnterEmployeeCommand { get; set; }
        public ICommand EnterTextBoxCustomerCommand { get; set; }
        public ICommand DownTextBoxCustomerCommand { get; set; }
        public ICommand UpTextBoxCustomerCommand { get; set; }
        public ICommand BtnAddCustomerCommand { get; set; }
        public ICommand EnterCustomerCommand { get; set; }
        public ICommand IsCheckAllSelected { get; set; }
        public ICommand IsCheckCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }


        private ContentControl _MainContentControl;
        public Customer customer;
        public Employee employee;
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
      
        public void GetListPaymentReason()
        {
            if (CategoryNoteList != null)
                CategoryNoteList.Clear();
            else
                CategoryNoteList = new ObservableCollection<PaymentReason>();
            OrdersClient client = new OrdersClient(this, this, this);
            PaymentReasonResponse response = client.GetListPaymentReason(Constants.STATUS, Constants.STATUS, 0);
            if (response != null && response.Data != null && response.Data != null)
            {
                response.Data.OrderByDescending(x => x.Id).ForEach(CategoryNoteList.Add);
            }
        }
        public void GetObjectType()
        {
            if (ObjectTypeList == null)
            {
                ObjectTypeList = new ObservableCollection<BasicModel>();
            }
            else
            {
                ObjectTypeList.Clear();
            }
            ObjectTypeList.Add(new BasicModel((int)ExpenseTypeEnum.ORTHER, MessageValue.MESSAGE_FROM_OTHER_2));   
            if (Constants.IS_NETWORK_ONLINE)
                //ObjectTypeList.Add(new BasicModel((int)ExpenseTypeEnum.SUPPLIER, MessageValue.MESSAGE_FROM_SUPPLIER_FILTER));
           // ObjectTypeList.Add(new BasicModel((int)ExpenseTypeEnum.CUSTOMER, MessageValue.MESSAGE_BOOKING_CUSTOMER));
            ObjectTypeList.Add(new BasicModel((int)ExpenseTypeEnum.EMPLOYEE, MessageValue.MESSAGE_BOOKING_EMPLOYEE));

        }
        public void GetPaymentType()
        {
            if (PaymentTypeList == null)
            {
                PaymentTypeList = new ObservableCollection<BasicModel>();
            }
            else
            {
                PaymentTypeList.Clear();
            }
            PaymentTypeList.Add(new BasicModel((int)PaymentMethodEnum.CASH, MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_MONEY));
            PaymentTypeList.Add(new BasicModel((int)PaymentMethodEnum.TRANSFER, MessageValue.MESSAGE_FROM_END_WORKING_SESION_TRANSFER_ORDER));
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
            EmployeeResponses employees = client.GetAllEmployeesResponses(brandId,branchId, Constants.OFFSET, Constants.ALL, Constants.STATUS, (int)StatusEnum.YES, Constants.NOT_STATUS);
            if (employees != null)
            {
                employees.Data.List.ForEach(EmployeeList.Add);
            }
        }
        public void GetDetail(long branchId)
        {
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.RESTAURANT_MANAGER), currentUser.Permissions))
            {
                BranchVisibility = Visibility.Visible;
                GetAllBranches();
                BranchItem = BranchList.Where(x => x.Id == branchId).FirstOrDefault();
            }
            else
            {
                BranchVisibility = Visibility.Collapsed;
            }
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                DateTimeVisibility = Visibility.Hidden;
            }
            else
            {
                DateTimeVisibility = Visibility.Visible;
            }
                GetListPaymentReason();
            GetPaymentType();
            GetObjectType();
            ObjectTypeVisibility = Visibility.Visible;
            if (ObjectTypeList != null && ObjectTypeList.Count > 0)
            {
                ObjectTypeItem = ObjectTypeList[0];
            }
            if (CategoryNoteList != null && CategoryNoteList.Count > 0)
            {
                CategoryNoteItem = CategoryNoteList[0];
            }
            if (PaymentTypeList != null && PaymentTypeList.Count > 0)
            {
                PaymentTypeItem = PaymentTypeList[0];
            }
            AddVisibility = Visibility.Collapsed;
            IsCheckAccounting = true;
            TradingDay = DateTime.Now;
            TradingTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TradingDayDisplayDateEnd = DateTime.Now;
            WarehouseSessionQuantity = 0;
            WarehouseSessionToTalAmount = "0";
            
        }
        public void GetDetail(AdditionFee a)
        {
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADDITION_FEE_APPROVEMENT), currentUser.Permissions))
            {
                AddVisibility = Visibility.Visible;
            }
            else
            {
                AddVisibility = Visibility.Visible;
            }
            if (MaterialWarehouseList != null)
            {
                MaterialWarehouseList.Clear();
            }
            else
            {
                MaterialWarehouseList = new ObservableCollection<WarehouseSession>();
            }
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
            {
                DateTimeVisibility = Visibility.Hidden;
            }
            else
            {
                DateTimeVisibility = Visibility.Collapsed;
            }
            AdditionFeeClient client = new AdditionFeeClient(this, this, this);
            AdditionFeeOneResponse response = client.GetAdditionFeeDetail(a.Id, a.Branch.Id);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {

                BranchVisibility = Visibility.Collapsed;
                ObjectTypeVisibility = Visibility.Collapsed;

                GetListPaymentReason();
                GetPaymentType();
                GetObjectType();
                if (ObjectTypeList != null && ObjectTypeList.Count > 0)
                {
                    ObjectTypeItem = ObjectTypeList.Where(x => x.Value == response.Data.ObjectTypeId).FirstOrDefault();
                }
                if (CategoryNoteList != null && CategoryNoteList.Count > 0)
                {
                    CategoryNoteItem = CategoryNoteList.Where(x => x.Id == response.Data.ResonId).FirstOrDefault();
                }
                if (PaymentTypeList != null && PaymentTypeList.Count > 0)
                {
                    PaymentTypeItem = PaymentTypeList.Where(x => x.Value == response.Data.PaymentMethodId).FirstOrDefault();
                }
                TextContent = string.Format("{0} - {1}", response.Data.ObjectType, response.Data.ObjectName);
                IsCheckAccounting = response.Data.IsCountToRevenue == Constants.STATUS ? true : false;
                TradingDay = Utils.Utils.GetStringFormatDate(response.Data.Date);
                TradingTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TradingDayDisplayDateEnd = TradingDayDisplayDateEnd = DateTime.Now;
                WarehouseSessionQuantity = response.Data.Supplier_order_ids.Count;
                //WarehouseClient client = new WarehouseClient(this, this, this);
                WarehouseSessionToTalAmount = response.Data.AmountString;
                Note = response.Data.Note;
               
            }
        }
        public long CustomerId;

        public bool isCreate = false;
        public AdditionFee AdditionFeeUpdate;
        public void FindCustomerByPhone()
        {
            CustomerClient client = new CustomerClient(this, this, this);
            FindCustomerWrapper wrapper = new FindCustomerWrapper(string.IsNullOrEmpty(CustomerInfor) ? "" : CustomerInfor, string.IsNullOrEmpty(CustomerText) ? "" : CustomerText);
            CustomerRegisterResponse response = client.FindCustomerByPhone(wrapper);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                customer.Id = response.Data.Id;
                customer.Name = response.Data.Name;
                CustomerInfor = String.Format("{0} - {1}", response.Data.Name, response.Data.Phone);
                CustomerText = response.Data.Phone;
                #region Đạt
                CustomerName = response.Data.Name;
                CustomerLastName = response.Data.FirstName;
                CustomerFirstName = response.Data.LastName;
                #endregion
            }
        }
        public ManagePaymentSlipViewModel(int brandId,long branchID)
        {
            
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                GetDetail(branchID);
                if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.CUSTOMER)
                {
                    _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                    _MainContentControl.Content = new ComboBoxCustomer();
                }
                else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.EMPLOYEE)
                {
                    _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                    employee = new Employee();
                    GetEmployeeList(brandId,BranchItem != null ? BranchItem.Id : branchID);
                    _MainContentControl.Content = new ComboBoxEmployee();
                }
                else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.ORTHER)
                {
                    _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                    _MainContentControl.Content = new TextboxOther();
                }
                isCreate = false;
            });
            SelectionChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

                GetEmployeeList(brandId, BranchItem != null ? BranchItem.Id : branchID);
            });
            SelectionObjectChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (ObjectTypeItem != null)
                {
                    if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.CUSTOMER)
                    {
                        if (MaterialWarehouseList != null)
                        {
                            MaterialWarehouseList.Clear();
                        }
                        else
                        {
                            MaterialWarehouseList = new ObservableCollection<WarehouseSession>();
                        }
                        _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                        customer = new Customer();
                       //GetCustomer();
                        //  WarehouseSessionVisibility = Visibility.Collapsed;
                        _MainContentControl.Content = new ComboBoxCustomer();
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.SUPPLIER)
                    {
                        IsAllItemsSelected = false;
                        _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                        _MainContentControl.Content = new ComboBoxSupplier();
                        CustomerInfor = null; // Đạt
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.EMPLOYEE)
                    {
                        if (MaterialWarehouseList != null)
                        {
                            MaterialWarehouseList.Clear();
                        }
                        else
                        {
                            MaterialWarehouseList = new ObservableCollection<WarehouseSession>();
                        }
                        _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                        employee = new Employee();
                        //  WarehouseSessionVisibility = Visibility.Collapsed;
                        GetEmployeeList(brandId,BranchItem != null ? BranchItem.Id : branchID);
                        _MainContentControl.Content = new ComboBoxEmployee();
                        CustomerInfor = null; // Đạt
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.ORTHER)
                    {
                        if (MaterialWarehouseList != null)
                        {
                            MaterialWarehouseList.Clear();
                        }
                        else
                        {
                            MaterialWarehouseList = new ObservableCollection<WarehouseSession>();
                        }
                        _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                        //    WarehouseSessionVisibility = Visibility.Collapsed;
                        _MainContentControl.Content = new TextboxOther();
                        CustomerInfor = null; // Đạt
                    }
                }

            });
            IsCheckCommand = new RelayCommand<WarehouseSession>((p) => { return true; }, p =>
            {
                WarehouseSessionQuantity = MaterialWarehouseList.Where(x => x.isCheck).Count();
                WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(MaterialWarehouseList.Where(x => x.isCheck).Sum(x => x.TotalAmount));
                //if (p != null)
                //{
                //    if (p.isCheck)
                //    {
                //        p.isCheck = true;
                //        int index = MaterialWarehouseList.IndexOf(p);
                //        MaterialWarehouseList.RemoveAt(index);
                //        MaterialWarehouseList.Insert(index, p);
                //        WarehouseSessionQuantity = MaterialWarehouseList.Where(x => x.isCheck).Count();
                //        WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(MaterialWarehouseList.Where(x => x.isCheck).Sum(x => x.TotalAmount));
                //    }
                //    else
                //    {
                //        p.isCheck = false;
                //        int index = MaterialWarehouseList.IndexOf(p);
                //        MaterialWarehouseList.RemoveAt(index);
                //        MaterialWarehouseList.Insert(index, p);
                //        WarehouseSessionQuantity = MaterialWarehouseList.Where(x => x.isCheck).Count();
                //        WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(MaterialWarehouseList.Where(x => x.isCheck).Sum(x => x.TotalAmount));
                //    }

                //}
            });
            IsCheckAllSelected = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked.Value)
                {
                    List<WarehouseSession> warehouse = new List<WarehouseSession>();
                    MaterialWarehouseList.ForEach(warehouse.Add);
                    for (int i = 0; i < MaterialWarehouseList.Count; i++)
                    {
                        warehouse[i].isCheck = true;
                        MaterialWarehouseList.RemoveAt(i);
                        MaterialWarehouseList.Insert(i, warehouse[i]);
                    }
                    warehouse.Clear();
                    WarehouseSessionQuantity = MaterialWarehouseList.Count();
                    decimal s = MaterialWarehouseList.Sum(x => Math.Round(x.TotalAmount, Constants.ROUND_ZERO));
                    WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(s);
                }
                else
                {
                    List<WarehouseSession> warehouse = new List<WarehouseSession>();
                    MaterialWarehouseList.ForEach(warehouse.Add);
                    for (int i = 0; i < MaterialWarehouseList.Count; i++)
                    {
                        warehouse[i].isCheck = false;
                        MaterialWarehouseList.RemoveAt(i);
                        MaterialWarehouseList.Insert(i, warehouse[i]);
                    }
                    warehouse.Clear();
                    WarehouseSessionQuantity = 0;
                    WarehouseSessionToTalAmount = "0";
                }

            });
            DownTextBoxSupplierCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                try
                {
                    if (SupplierIndex < SupplierList.Count())
                    {
                        SupplierIndex++;
                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
            UpTextBoxSupplierCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                try
                {
                    if (SupplierIndex > 0)
                    {
                        SupplierIndex--;
                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
            BtnAddSupplierCommand = new RelayCommand<Supplier>((p) => { return true; }, p =>
            {
                try
                {
                    if (p != null)
                    {
                        SupplierText = p.Name;
                        IsOpenPopupSupplier = false;
                    }
                    //if (SupplierItem != null)
                    //{
                    //    ChangeSupplier(SupplierItem);
                    //    SupplierText = SupplierItem.Name;
                    //    IsOpenPopupSupplier = false;
                    //    //   IsOpenPopup = true;
                    //}
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
            EnterTextBoxSupplierCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                try
                {
                    if (SupplierItem != null)
                    {
                        SupplierText = SupplierItem.Name;
                        IsOpenPopupSupplier = false;
                        //   IsOpenPopup = true;
                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
            EnterSupplierCommand = new RelayCommand<Popup>((p) => { return true; }, p =>
            {
                try
                {
                    if (SupplierItem != null)
                    {
                        SupplierText = SupplierItem.Name;
                        IsOpenPopupSupplier = false;
                        //    IsOpenPopup = true;
                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
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
                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });
            EnterTextBoxCustomerCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
            {
                try
                {
                    //if (CustomerItem != null)
                    //{
                    //    customer = CustomerItem;
                    //    CustomerText = CustomerItem.Name;
                    //    IsOpenPopupCustomer = false;
                    //    //   IsOpenPopup = true;
                    //}
                    if (string.IsNullOrEmpty(CustomerText))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_FROM_NOTIFICATION_CUSTOMER_TAG);
                    }
                    else
                    {
                        CustomerClient customerClient = new CustomerClient(this, this, this);
                        CustomerRegisterResponse response = customerClient.FindCustomerByPhone(new FindCustomerWrapper("", CustomerText));
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            customer = response.Data;
                            #region Đạt
                            CustomerInfor = String.Format("{0} - {1}", response.Data.Name, response.Data.Phone);
                            CustomerName = response.Data.Name;
                            CustomerLastName = response.Data.FirstName;
                            CustomerFirstName = response.Data.LastName;
                            #endregion
                        }

                    }
                }
                catch (Exception e)
                {
                    WriteLog.logs("Lỗi là:" + e.Message);
                }
            });           
            ByCusByPhone = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                FindCustomerByPhone();
            });
            AddCustomerCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {                
                ManageCustomerWindow window = new ManageCustomerWindow();
                window.DataContext = new ManageCustomerViewModel(CustomerLastName, CustomerFirstName, CustomerText); // Đạt
                window.ShowDialog();
                ManageCustomerViewModel createCustomerVM = window.DataContext as ManageCustomerViewModel;
                if (createCustomerVM.IsCreate == true)
                {
                    customer.Id = createCustomerVM.CustomerUpdate.Id;
                   // customer.Name = createCustomerVM.CustomerUpdate.Name;
                    CustomerInfor = String.Format("{0} - {1}", createCustomerVM.CustomerUpdate.Name, createCustomerVM.CustomerUpdate.Phone);
                    CustomerText = createCustomerVM.CustomerUpdate.Phone;
                    #region Đạt
                    CustomerName = createCustomerVM.CustomerUpdate.Name;
                    CustomerLastName = createCustomerVM.FirstName;
                    CustomerFirstName = createCustomerVM.LastName;
                    #endregion
                }
            });

            TextChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (CustomerText == "")
                {
                    CustomerName = "";
                    CustomerLastName = "";
                    CustomerFirstName = "";
                }
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
            CompleteCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                List<long> WarehouseSessionIds = new List<long>();
                long objectId = 0;
                long objectType = 0;
                string objectName = "";
                decimal amount = Utils.Utils.FormatMoneyDecimal(WarehouseSessionToTalAmount);
                if (ObjectTypeItem != null)
                {
                    if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.CUSTOMER)
                    {
                        //if (customer != null)
                        //{
                        //    objectType = (int)ExpenseTypeEnum.CUSTOMER;
                        //    objectId = customer.Id;
                        //    objectName = customer.Name;
                        //}
                        //else
                        //{
                        //    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SELECT_CUSTOMER);
                        //    return;
                        //}
                        if (string.IsNullOrEmpty(CustomerInfor))
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SELECT_CUSTOMER);
                            return;
                        }
                        else
                        {
                            objectType = (int)ExpenseTypeEnum.CUSTOMER;
                            objectId = customer.Id;
                            objectName = customer.Name;
                        }
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.EMPLOYEE)
                    {                      
                        //if (employee != null)
                        //{
                        //    objectType = (int)ExpenseTypeEnum.EMPLOYEE;
                        //    objectId = employee.Id;
                        //    objectName = employee.Name;

                        //}
                        //else
                        //{
                        //    NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_NOT_SELECT_EMPLOYEE);
                        //    return;
                        //}
                        if (EmployeeIndex < 0 || string.IsNullOrEmpty(EmployeeText))
                        {
                            EmployeeIndex = -1;
                            NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_NOT_SELECT_EMPLOYEE);
                            return;
                        }
                        else if(employee.NameRoleNameString != EmployeeText)
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_ERROR_EMPLOYEE);
                            return;
                        }    
                        else
                        {
                            objectType = (int)ExpenseTypeEnum.EMPLOYEE;
                            objectId = employee.Id;
                            objectName = employee.Name;
                        }
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.SUPPLIER)
                    {
                        if (SupplierItem != null)
                        {
                            objectType = (int)ExpenseTypeEnum.SUPPLIER;
                            objectId = SupplierItem.Id;
                            objectName = SupplierItem.Name;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SUPPLIER);
                            return;
                        }
                        List<WarehouseSession> warehouseSessions = MaterialWarehouseList.Where(x => x.isCheck).ToList();
                        if (warehouseSessions != null || warehouseSessions.Count > 0)
                        {
                            warehouseSessions.Select(x => x.Id).ForEach(WarehouseSessionIds.Add);
                        }

                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.ORTHER)
                    {
                        if (!string.IsNullOrEmpty(TextOther))
                        {
                            objectType = (int)ExpenseTypeEnum.ORTHER;
                            objectId = 0;
                            objectName = TextOther;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_OTHER);
                            return;
                        }
                    }
                }           
                if (amount == 0)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_FROM_MANAGE_PAYMENT_SLIP);
                    return;
                }
                if (CategoryNoteItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CATEGORY_NOTE);
                    return;
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    AdditionFeeWrapper wrapper = new AdditionFeeWrapper(0,
                    BranchItem != null ? BranchItem.Id : currentUser.BranchId,
                    CategoryNoteItem.Id,
                    string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')),
                     "",
                     IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS,
                      string.IsNullOrEmpty(Note) ? "" : Note, objectId, objectName, objectType,
                      PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, (int)AdditionFeeEnum.OUT,
                      WarehouseSessionIds);
                    wrapper.IsPaid = 1;
                    AdditionFeeClient client = new AdditionFeeClient(this, this, this);                    
                    ManageAdditionFeeResponse response = client.AddAdditionFee(wrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                    {
                        isCreate = true;
                        AdditionFeeUpdate = response.Data;
                        p.Close();
                    }
                }
                else
                {
                    AdditionFeeWrapper wrapper = new AdditionFeeWrapper(0,BranchItem != null ? BranchItem.Id : currentUser.BranchId,CategoryNoteItem.Id,
                  string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')),
                    Utils.Utils.GetDateFormatVN(TradingDay),
                   IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS,
                    string.IsNullOrEmpty(Note) ? "" : Note, objectId, objectName, objectType,
                    PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, (int)AdditionFeeEnum.OUT,
                    WarehouseSessionIds);
                    wrapper.IsPaid = 1;
                    AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                    ManageAdditionFeeResponse response = client.AddAdditionFee(wrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                    {
                        isCreate = true;
                        AdditionFeeUpdate = response.Data;
                        p.Close();
                    }
                }
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                List<long> WarehouseSessionIds = new List<long>();
                long objectId = 0;
                long objectType = 0;
                string objectName = "";
                List<WarehouseSession> warehouseSessions = new List<WarehouseSession>();
                if (ObjectTypeItem != null)
                {
                    if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.CUSTOMER)
                    {
                        if (customer != null)
                        {
                            objectType = (int)ExpenseTypeEnum.CUSTOMER;
                            objectId = customer.Id;
                            objectName = customer.Name;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SELECT_CUSTOMER);
                            return;
                        }
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.EMPLOYEE)
                    {
                        if (employee != null)
                        {
                            objectType = (int)ExpenseTypeEnum.EMPLOYEE;
                            objectId = employee.Id;
                            objectName = employee.Name;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_NOT_SELECT_EMPLOYEE);
                            return;
                        }
                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.SUPPLIER)
                    {
                        if (SupplierItem != null)
                        {
                            objectType = (int)ExpenseTypeEnum.SUPPLIER;
                            objectId = SupplierItem.Id;
                            objectName = SupplierItem.Name;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SUPPLIER);
                            return;
                        }
                       warehouseSessions = MaterialWarehouseList.Where(x => x.isCheck).ToList();
                        if (warehouseSessions != null || warehouseSessions.Count > 0)
                        {
                            warehouseSessions.Select(x => x.Id).ForEach(WarehouseSessionIds.Add);
                        }

                    }
                    else if (ObjectTypeItem.Value == (int)ExpenseTypeEnum.ORTHER)
                    {
                        if (!string.IsNullOrEmpty(TextOther))
                        {
                            objectType = (int)ExpenseTypeEnum.ORTHER;
                            objectId = 0;
                            objectName = TextOther;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_OTHER);
                            return;
                        }
                    }

                }
                if (CategoryNoteItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CATEGORY_NOTE);
                    return;
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                {
                    AdditionFeeWrapper wrapper = new AdditionFeeWrapper(0,
                    BranchItem != null ? BranchItem.Id : currentUser.BranchId,
                    CategoryNoteItem.Id,
                    string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')),
                     Utils.Utils.GetDateFormatVN(TradingDay),
                     IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS,
                      string.IsNullOrEmpty(Note) ? "" : Note, objectId, objectName, objectType,
                      PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, (int)AdditionFeeEnum.OUT,
                      WarehouseSessionIds);
                    wrapper.IsPaid = 0;
                    AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                    ManageAdditionFeeResponse response = client.AddAdditionFee(wrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                    {
                        isCreate = true;
                        AdditionFeeUpdate = response.Data;
                        p.Close();
                    }
                }
                else
                {
                    AdditionFeeWrapper wrapper = new AdditionFeeWrapper(0,BranchItem != null ? BranchItem.Id : currentUser.BranchId,CategoryNoteItem.Id,
                    string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')),
                    Utils.Utils.GetDateFormatVN(TradingDay),IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS,string.IsNullOrEmpty(Note) ? "" : Note, 
                    objectId, objectName, objectType,PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, (int)AdditionFeeEnum.OUT,WarehouseSessionIds);
                    wrapper.IsPaid = 1;
                    AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                    ManageAdditionFeeResponse response = client.AddAdditionFee(wrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                    {
                        isCreate = true;
                        AdditionFeeUpdate = response.Data;
                        p.Close();
                    }
                }
            });
        }
        public ManagePaymentSlipViewModel(AdditionFee fee)
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                GetDetail(fee);
                _MainContentControl = p.FindName("ContentPersion") as ContentControl;
                _MainContentControl.Content = new TextBlockShow();
                isCreate = false;
            });
            IsCheckCommand = new RelayCommand<WarehouseSession>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    if (!p.isCheck)
                    {
                        p.isCheck = true;
                        int index = MaterialWarehouseList.IndexOf(p);
                        MaterialWarehouseList.RemoveAt(index);
                        MaterialWarehouseList.Insert(index, p);
                        WarehouseSessionQuantity = MaterialWarehouseList.Where(x => x.isCheck).Count();
                        WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(MaterialWarehouseList.Where(x => x.isCheck).Sum(x => x.TotalAmount));
                    }
                    else
                    {
                        p.isCheck = false;
                        int index = MaterialWarehouseList.IndexOf(p);
                        MaterialWarehouseList.RemoveAt(index);
                        MaterialWarehouseList.Insert(index, p);
                        WarehouseSessionQuantity = MaterialWarehouseList.Where(x => x.isCheck).Count();
                        WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(MaterialWarehouseList.Where(x => x.isCheck).Sum(x => x.TotalAmount));
                    }

                }
            });
            IsCheckAllSelected = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked.Value)
                {
                    List<WarehouseSession> warehouse = new List<WarehouseSession>();
                    MaterialWarehouseList.ForEach(warehouse.Add);
                    for (int i = 0; i < MaterialWarehouseList.Count; i++)
                    {
                        warehouse[i].isCheck = true;
                        MaterialWarehouseList.RemoveAt(i);
                        MaterialWarehouseList.Insert(i, warehouse[i]);
                    }
                    warehouse.Clear();
                    WarehouseSessionQuantity = MaterialWarehouseList.Count();
                    decimal s = MaterialWarehouseList.Sum(x => Math.Round(x.TotalAmount, Constants.ROUND_ZERO));
                    WarehouseSessionToTalAmount = Utils.Utils.FormatMoney(s);
                }
                else
                {
                    List<WarehouseSession> warehouse = new List<WarehouseSession>();
                    MaterialWarehouseList.ForEach(warehouse.Add);
                    for (int i = 0; i < MaterialWarehouseList.Count; i++)
                    {
                        warehouse[i].isCheck = false;
                        MaterialWarehouseList.RemoveAt(i);
                        MaterialWarehouseList.Insert(i, warehouse[i]);
                    }
                    warehouse.Clear();
                    WarehouseSessionQuantity = 0;
                    WarehouseSessionToTalAmount = "0";
                }

            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
            });
            CompleteCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (CategoryNoteItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CATEGORY_NOTE);
                    return;
                }
                decimal amount = Utils.Utils.FormatMoneyDecimal(WarehouseSessionToTalAmount);
                if (amount == 0)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_FROM_MANAGE_PAYMENT_SLIP);
                    return;
                }
                else
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        UpdatePaymentSlipWrapper wrapper = new UpdatePaymentSlipWrapper(fee.Branch.Id, fee.Id, IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS, PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, CategoryNoteItem.Id, string.IsNullOrEmpty(Note) ? "" : Note, Utils.Utils.GetDateFormatVN(DateTime.Now), string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')),fee.ObjectName);
                        AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                        ManageAdditionFeeResponse response = client.UpdateAdditionFee(wrapper);
                        //if (response != null && response.Status == (int)ResponseEnum.OK)
                        //{
                        //    BaseResponse baseResponse = client.PaidAdditionFee(response.Data.Id, response.Data.Branch.Id);
                        //    if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        //    {
                        //        isCreate = true;
                        //        p.Close();
                        //    }
                        //}
                        isCreate = true;
                        p.Close(); 
                    }
                    else
                    {
                        UpdatePaymentSlipWrapper wrapper = new UpdatePaymentSlipWrapper(fee.Branch.Id, fee.Id, IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS, PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, CategoryNoteItem.Id, string.IsNullOrEmpty(Note) ? "" : Note, string.Format("{0} {1}", Utils.Utils.GetDateFormatVN(TradingDay), DateTime.Now.TimeOfDay), string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')), fee.ObjectName);
                        AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                        ManageAdditionFeeResponse response = client.UpdateAdditionFee(wrapper);
                        //if (response != null && response.Status == (int)ResponseEnum.OK)
                        //{
                        //    BaseResponse baseResponse = client.ConfirmAdditionFee(response.Data.Id, response.Data.Branch.Id);
                        //    if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        //    {
                        //        isCreate = true;
                        //        response.Data.StatusText = MessageValue.MESSAGE_FROM_STATUS_TEXT_COMPLETED;
                        //        response.Data.Status = (int)AdditionFeeStatusEnum.PAID;
                        //        AdditionFeeUpdate = response.Data;
                        //        p.Close();
                        //    }
                        //}
                        isCreate = true;
                        p.Close(); 
                    }
                }
            });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

                if (CategoryNoteItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CATEGORY_NOTE);
                    return;
                }
                else
                {
                    UpdatePaymentSlipWrapper wrapper = new UpdatePaymentSlipWrapper(fee.Branch.Id, fee.Id, IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS, PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, CategoryNoteItem.Id, string.IsNullOrEmpty(Note) ? "" : Note, Utils.Utils.GetDateFormatVN(TradingDay), string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')), fee.ObjectName);
                    AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                    ManageAdditionFeeResponse response = client.UpdateAdditionFee(wrapper);
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CANCEL_ADDITION_FEE_CONTENT_OUT;
                        string Title = MessageValue.MESSAGE_CANCEL_ADDITION_FEE_OUT;
                        string YesContent = MessageValue.MESSAGE_CO_CONTENT;
                        string NoContent = MessageValue.MESSAGE_KHONG_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            BaseResponse baseResponse = client.CancelAdditionFee(response.Data.Id, response.Data.Branch.Id);
                            if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                            {
                                isCreate = true;
                                response.Data.StatusText = MessageValue.MESSAGE_FROM_STATUS_TEXT_CANCEL;
                                response.Data.Status = (int)AdditionFeeStatusEnum.CANCEL;
                                AdditionFeeUpdate = response.Data;
                                p.Close();
                            }
                        }
                    }
                }
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

                if (CategoryNoteItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CATEGORY_NOTE);
                    return;
                }
                else
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {

                        UpdatePaymentSlipWrapper wrapper = new UpdatePaymentSlipWrapper(fee.Branch.Id, fee.Id, IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS, PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, CategoryNoteItem.Id, string.IsNullOrEmpty(Note) ? "" : Note, "", string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')), fee.ObjectName);
                        AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                        ManageAdditionFeeResponse response = client.UpdateAdditionFee(wrapper);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            isCreate = true;
                            AdditionFeeUpdate = response.Data;
                            p.Close();
                        }
                    }
                    else
                    {
                        UpdatePaymentSlipWrapper wrapper = new UpdatePaymentSlipWrapper(fee.Branch.Id, fee.Id, IsCheckAccounting ? Constants.STATUS : Constants.NOT_STATUS, PaymentTypeItem != null ? PaymentTypeItem.Value : (int)PaymentMethodEnum.CASH, CategoryNoteItem.Id, string.IsNullOrEmpty(Note) ? "" : Note, Utils.Utils.GetDateFormatVN(TradingDay), string.IsNullOrEmpty(WarehouseSessionToTalAmount) ? 0 : decimal.Parse(WarehouseSessionToTalAmount.Trim(',')), fee.ObjectName);
                        AdditionFeeClient client = new AdditionFeeClient(this, this, this);
                        ManageAdditionFeeResponse response = client.UpdateAdditionFee(wrapper);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            isCreate = true;
                            AdditionFeeUpdate = response.Data;
                            p.Close();
                        }
                    }
                }
            });
        }


        //public void GetCustomer()
        //{
        //    if (CustomerList != null)
        //    {
        //        CustomerList.Clear();
        //    }
        //    else
        //    {
        //        CustomerList = new ObservableCollection<Customer>();
        //    }
        //    CustomerClient client = new CustomerClient(this, this, this);
        //    CustomerResponse response = client.GetAllCustomer();
        //    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
        //    {
        //        response.Data.ForEach(CustomerList.Add);
        //    }
        //}

        public void LogError(Exception ex, string infoMessage)
        {
            WriteLog.logs(infoMessage);
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

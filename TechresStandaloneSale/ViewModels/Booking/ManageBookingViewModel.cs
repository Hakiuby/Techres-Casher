using DevExpress.Mvvm.Native;
using MaterialDesignThemes.Wpf;
using Nest;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
    public class ManageBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
       
        public bool SetTablePermission = true;
        //public long CurrentTableId = 0;
        private string _FoodNameList { get; set; }
        public string FoodNameList
        {
            get
            {
                return _FoodNameList;
            }
            set
            {
                _FoodNameList = value;
                OnPropertyChanged("FoodNameList");
            }
        }

        private ObservableCollection<Employee> _EmployeeList = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged("EmployeeList"); } }
        private ObservableCollection<BasicModel> _BookingFormList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> BookingFormList { get => _BookingFormList; set { _BookingFormList = value; OnPropertyChanged("BookingFormList"); } }
        private ObservableCollection<BasicModel> _PaymentMethodList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> PaymentMethodList { get => _PaymentMethodList; set { _PaymentMethodList = value; OnPropertyChanged("PaymentMethodList"); } }
        private ObservableCollection<string> _TimeList = new ObservableCollection<string>();
        public ObservableCollection<string> TimeList { get => _TimeList; set { _TimeList = value; OnPropertyChanged("TimeList"); } }

        private string _TimeItem { get; set; }
        public string TimeItem { get => _TimeItem; set { _TimeItem = value; OnPropertyChanged("TimeItem"); } }
        private Visibility _ReviceDepositVisibility { get; set; }
        public Visibility ReviceDepositVisibility { get => _ReviceDepositVisibility; set { _ReviceDepositVisibility = value; OnPropertyChanged("ReviceDepositVisibility"); } }
        private Visibility _ReturnDepositVisibility { get; set; }
        public Visibility ReturnDepositVisibility { get => _ReturnDepositVisibility; set { _ReturnDepositVisibility = value; OnPropertyChanged("ReturnDepositVisibility"); } }
        private string _ReviceDepositContent { get; set; }
        public string ReviceDepositContent { get => _ReviceDepositContent; set { _ReviceDepositContent = value; OnPropertyChanged("ReviceDepositContent"); } }

        private string _CustomerAddress; 
        public string CustomerAddress { get => _CustomerAddress; set { _CustomerAddress = value; OnPropertyChanged("CustomerAddres"); } }
        private string _CustomerBirth; 
        public string CustomersBirth { get => _CustomerBirth; set { _CustomerBirth = value; OnPropertyChanged("CustomersBirth");  } }
             
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged("CustomerName"); } }
        private string _Amount { get; set; }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }

        private string _CustomerPhone { get; set; }
        public string CustomerPhone { get => _CustomerPhone; set { _CustomerPhone = value; OnPropertyChanged("CustomerPhone"); } }

        private BasicModel _BookingFormItem;
        public BasicModel BookingFormItem { get => _BookingFormItem; set { _BookingFormItem = value; OnPropertyChanged("BookingFormItem"); } }

        private Employee _EmployeeItem;
        public Employee EmployeeItem { get => _EmployeeItem; set { _EmployeeItem = value; OnPropertyChanged("EmployeeItem"); } }

        private BasicModel _PaymentMethodItem;
        public BasicModel PaymentMethodItem { get => _PaymentMethodItem; set { _PaymentMethodItem = value; OnPropertyChanged("PaymentMethodItem"); } }

        private DateTime _BookingTime;
        public DateTime BookingTime { get => _BookingTime; set { _BookingTime = value; OnPropertyChanged("BookingTime"); } }

        private DateTime _DatePickerDisplayDateStart;
        public DateTime DatePickerDisplayDateStart { get => _DatePickerDisplayDateStart; set { _DatePickerDisplayDateStart = value; OnPropertyChanged("DatePickerDisplayDateStart"); } }
       
        private string _OtherRequest;
        public string OtherRequest { get => _OtherRequest; set { _OtherRequest = value; OnPropertyChanged("OtherRequest"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        private string _TotalAmount;
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }

        private long _FoodQuantity;
        public long FoodQuantity { get => _FoodQuantity; set { _FoodQuantity = value; OnPropertyChanged("FoodQuantity"); } }

        private decimal _TotalQuantity;
        public decimal TotalQuantity { get => _TotalQuantity; set { _TotalQuantity = value; OnPropertyChanged("TotalQuantity"); } }

        private long _QuantityPeople;
        public long QuantityPeople { get => _QuantityPeople; set { _QuantityPeople = value; OnPropertyChanged("QuantityPeople"); } }
        private Visibility _EmployeeVisibility;
        public Visibility EmployeeVisibility { get => _EmployeeVisibility; set { _EmployeeVisibility = value; OnPropertyChanged("EmployeeVisibility"); } }
        private Visibility _BookingTypeVisibility;
        public Visibility BookingTypeVisibility { get => _BookingTypeVisibility; set { _BookingTypeVisibility = value; OnPropertyChanged("BookingTypeVisibility"); } }
        private Visibility _DepositVisibility;
        public Visibility DepositVisibility { get => _DepositVisibility; set { _DepositVisibility = value; OnPropertyChanged("DepositVisibility"); } }

        #region toan
        private Visibility _searchVisibility;
        public Visibility searchVisibility { get => _searchVisibility; set { _searchVisibility = value; OnPropertyChanged("searchVisibility"); } }

        private bool _CustomerPhoneEnabled;
        public bool CustomerPhoneEnabled { get => _CustomerPhoneEnabled; set { _CustomerPhoneEnabled = value; OnPropertyChanged("CustomerPhoneEnabled"); } }
        #endregion
        public ICommand AddCustomerCommand { get; set; }
        public ICommand ByCusByPhone { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ChooseFoodCommand { get; set; }
        public ICommand SubQuantityCommand { get; set; }
        public ICommand AddQuantityCommand { get; set; }
        public ICommand SelectionChangedEmployeeCommand { get; set; }
        public ICommand SeeDiagramCommand { get; set; }
        public ICommand ChooseFoodGiftCommand { get; set; }
        public ICommand ReviceDepositCommand { get; set; }
        public ICommand ReturnDepositCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        public long CurrentTableId = 0;
        public Customer currentCustomer; // Dat

        public string tableContent;
        public string TableContent
        {
            get
            {
                return tableContent;
            }
            set
            {
                tableContent = value;
                OnPropertyChanged("TableContent");
            }
        }
        public int TableStatus = 0;
        private List<BillResponse> FoodRequest = new List<BillResponse>();

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public bool isCreate = false;

        public Models.Booking BookingUpdate = new Models.Booking();

        public void SetTableByOrder()
        {
            TableOrderWindow table = new TableOrderWindow();
            table.DataContext = new TableViewModel((int)CurrentTableId, MessageValue.MESSAGE_FROM_ORDER_CHOOSE_TABLE_TITLE,false);//toan
            table.ShowDialog();
            var tableViewModel = table.DataContext as TableViewModel; 
            if(tableViewModel != null && tableViewModel.table != null)
            {
                if(tableViewModel.table.TableStatus == (int)TableStatusEnum.EMPTY)
                {
                     CurrentTableId = tableViewModel.table.Id;
                    TableContent = tableViewModel.table.Name;
                    TableStatus = tableViewModel.table.TableStatus;
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_TABLE_NOT_EMPTY);
                }
            }
        }
        public void GetTime()
        {
            if (TimeList == null)
            {
                TimeList = new ObservableCollection<string>();
            }
            else
            {
                TimeList.Clear();
            }
            for (int i = 0; i < 30 * 2 * 24; i += 30)
            {
                TimeList.Add(DateTime.Today.AddMinutes(i).ToString("HH:mm"));
            }
        }
        public void GetBookingForm()
        {
            if (BookingFormList == null)
            {
                BookingFormList = new ObservableCollection<BasicModel>();
            }
            else
            {
                BookingFormList.Clear();
            }
            BookingFormList.Add(new BasicModel((int)BookingTypeEnum.CUSTOMER, MessageValue.MESSAGE_BOOKING_CUSTOMER));
            BookingFormList.Add(new BasicModel((int)BookingTypeEnum.EMPLOYEE, MessageValue.MESSAGE_BOOKING_EMPLOYEE));

        }
        public void GetPaymentMethodList()
        {
            if (PaymentMethodList == null)
            {
                PaymentMethodList = new ObservableCollection<BasicModel>();
            }
            else
            {
                PaymentMethodList.Clear();
            }
            PaymentMethodList.Add(new BasicModel(1, MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_MONEY));
            PaymentMethodList.Add(new BasicModel(2, MessageValue.MESSAGE_FROM_END_WORKING_SESION_BANK_MONEY));
            PaymentMethodList.Add(new BasicModel(6, MessageValue.MESSAGE_FROM_END_WORKING_SESION_TRANSFER_ORDER));
            PaymentMethodItem = PaymentMethodList[0];
        }
        public void GetDetail()
        {
            GetBookingForm();
            GetPaymentMethodList();
            BookingFormItem = BookingFormList.Where(x => x.Value == (int)BookingTypeEnum.CUSTOMER).FirstOrDefault();
            EmployeeVisibility = Visibility.Collapsed;
            GetTime();
            if(DateTime.Now.Hour < 22)
            {
                BookingTime = DateTime.Now;
                TimeItem = new DateTime(new TimeSpan(DateTime.Now.Hour + 2, DateTime.Now.Minute > 0 ? 30 : 0, 0).Ticks).ToString("HH:mm");

            }
            else
            {
                BookingTime = DateTime.Now.AddDays(1);
                TimeItem = new DateTime(new TimeSpan(DateTime.Now.Hour - 22, DateTime.Now.Minute > 0 ? 30 : 0, 0).Ticks).ToString("HH:mm");
            }            
            QuantityPeople = 0;
            TotalAmount = "0";
            Amount = "0";
            DatePickerDisplayDateStart = DateTime.Now;
            ReviceDepositVisibility = Visibility.Collapsed;
            ReturnDepositVisibility = Visibility.Collapsed;
        }
        public void GetDetail(Models.Booking booking)
        {
            #region Dat
            currentCustomer = new Customer();
            currentCustomer.Name = booking.CustomerName;
            currentCustomer.LastName = booking.CustomerLastName;
            currentCustomer.FirstName = booking.CustomerFirstName;
            currentCustomer.Phone = booking.CustomerPhone;
            #endregion
            DatePickerDisplayDateStart = DateTime.Now;
            GetBookingForm();
            GetTime();
            GetPaymentMethodList();
            GetAllEmployees(currentUser.RestaurantBrandId, booking.Branch.Id);
            EmployeeVisibility = Visibility.Collapsed;
            DepositVisibility = Visibility.Collapsed;
            DateTime dateTime = Utils.Utils.GetStringFormatDateTimeHour(booking.BookingTime);
            TimeItem = dateTime.ToString("HH:mm");
            BookingTime = Utils.Utils.GetStringFormatDateTimeHour(booking.BookingTime);
            QuantityPeople = booking.NumberSlot;
            TotalAmount = booking.TotalAmountString;
            customerId = booking.CustomerId;
            CustomerName = booking.CustomerName;
            customerFristName = booking.CustomerFirstName;
            customerLastName = booking.CustomerLastName;
            CustomerPhone = booking.CustomerPhone;
            
            BillResponse orderFood;
            foreach (FoodRequest f in booking.Foods)
            {
                 orderFood = new BillResponse();
                orderFood.FoodId = f.Id;
                orderFood.FoodName = f.Name;
                orderFood.Quantity = f.Quantity;
                orderFood.Price = f.Price;
                orderFood.UnitPrice = f.Price;
                orderFood.IsGift = f.IsGift;
                //    orderFood.Amount = f.TotalAmount;
                FoodRequest.Add(orderFood);
            }
            FoodNameList = string.Format("Gọi món: {0} \nTặng món: {1}", String.Concat(FoodRequest.Where(x => x.IsGift == 0).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))), String.Concat(FoodRequest.Where(x => x.IsGift == 1).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))));

            OtherRequest = booking.OrtherRequirements;
            Note = booking.Note;
            TotalAmount = booking.TotalAmountString;
            FoodQuantity = booking.Foods.Count();
            TotalQuantity = booking.Foods.Sum(x => x.Quantity);
            BookingTypeVisibility = Visibility.Collapsed;
            if (booking.BookingType == (int)BookingTypeEnum.EMPLOYEE)
            {
                EmployeeVisibility = Visibility.Visible;
                EmployeeItem = EmployeeList.Where(x=>x.Id==booking.Employee.Id).FirstOrDefault();
                BookingFormItem = BookingFormList.Where(x => x.Value == (int)BookingTypeEnum.EMPLOYEE).FirstOrDefault();
            }
            else
            {
                EmployeeVisibility = Visibility.Hidden;
                BookingFormItem = BookingFormList.Where(x => x.Value == (int)BookingTypeEnum.CUSTOMER).FirstOrDefault();
            }
            ReviceDepositVisibility = Visibility.Collapsed;
            ReturnDepositVisibility = Visibility.Collapsed;
            if (booking.BookingStatus == (int)BookingStatusEnum.ARRANGED_TABLE || 
                booking.BookingStatus == (int)BookingStatusEnum.CONFIMED || 
                booking.BookingStatus == (int)BookingStatusEnum.WAITING_CONFIRM)
            {
                ReviceDepositVisibility = Visibility.Visible;
            }
                if (booking.DepositAmount == 0)
            {
                ReviceDepositContent = MessageValue.MESSAGE_FROM_BOOKING_RECEVICE_DEPOSIT_CAPSLK;
            }
            else
            {
                ReviceDepositContent = MessageValue.MESSAGE_FROM_BOOKING_EDIT_RECEVICE_DEPOSIT;
                ReturnDepositVisibility = Visibility.Visible;
            }
        }
        public long customerId = 0;
        private string customerFristName =  string.Empty;
        private string customerLastName = string.Empty;
        public ManageBookingViewModel(int brandId,long branchId)
        {
            if (currentUser != null)
            {
                searchVisibility = Visibility.Visible;
                CustomerPhoneEnabled = true;
                GetDetail();
                BookingTypeVisibility = Visibility.Visible;
                DepositVisibility = Visibility.Visible;
                SelectionChangedEmployeeCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if (BookingFormItem != null)
                    {
                        if (BookingFormItem.Value == (int)BookingTypeEnum.CUSTOMER)
                        {
                            EmployeeVisibility = Visibility.Collapsed;
                        }
                        else if (BookingFormItem.Value == (int)BookingTypeEnum.EMPLOYEE)
                        {
                            EmployeeVisibility = Visibility.Visible;
                            GetAllEmployees(currentUser.RestaurantBrandId, branchId);
                        }
                    }
                });
                AddQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if(QuantityPeople != 999)
                    QuantityPeople++;
                });
                SubQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if (QuantityPeople > 0) QuantityPeople--;
                });
                OrderCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    decimal amount = Utils.Utils.FormatMoneyDecimal(Amount);
                    BookingClient cLient = new BookingClient(this, this, this);
                    CustomerClient client = new CustomerClient(this, this, this);
                    #region Tạm đóng
                    //CustomerRegisterResponse responseCustomner = client.Register(customerFristName, customerLastName, CustomerAddress, CustomerPhone, CustomersBirth);
                    #endregion
                    #region Validate 
                    if (BookingTime == null || TimeItem == null)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_NOT_FAIL_TIME_BOOKING);
                        return;
                    }
                    else if (string.IsNullOrEmpty(CustomerPhone) || !Utils.Utils.IsNumber(CustomerPhone))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_PHONE_ERROR);
                        return;
                    } 
                    else if (QuantityPeople <= 0)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_SLOT_CUSTOMER);
                        return;
                    }
                    else if (string.IsNullOrEmpty(Amount))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_FROM_MANAGE_BOOKING_EMPTY_AMOUNT);
                    }
                    else if (string.IsNullOrEmpty(CustomerName))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_EMPTY_CUSTOMER);
                    }
                    #endregion
                    else if (amount == 0 || amount >= 1000 && amount <= 1000000000)
                     {
                        List<FoodUsing> ListFoodRequest = new List<FoodUsing>();
                        foreach (BillResponse o in FoodRequest)
                        {
                            FoodUsing food = new FoodUsing(o.FoodId, o.Quantity, o.IsGift);
                            ListFoodRequest.Add(food);
                        }
                        //string Time = Utils.Utils.GetDateHourSecondsFormatVN(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TimeArrived, MinuteArrived, 0));
                        string Time = Utils.Utils.GetDateFormatVN(BookingTime.Date).ToString() + " " + TimeItem + ":00";
                        CreateBookingWrapper wrapper = new CreateBookingWrapper(branchId, customerId, CustomerName, CustomerPhone, string.IsNullOrEmpty(OtherRequest) ? "" : OtherRequest, string.IsNullOrEmpty(Note) ? "" : Note, QuantityPeople,
                         ListFoodRequest, Time, BookingFormItem != null ? BookingFormItem.Value : (int)BookingTypeEnum.CUSTOMER, EmployeeItem != null ? EmployeeItem.Id : 0, customerFristName, customerLastName, amount);
                        BookingResponse bookingResponse = cLient.CreateBooking(wrapper);
                        if (bookingResponse != null && bookingResponse.Status == (int)ResponseEnum.OK)
                        {
                            //if (!string.IsNullOrEmpty(Amount))
                            //{
                            //    decimal depost = decimal.Parse(Amount.Trim(','));
                            //    if (depost > 0)
                            //    {
                            //        BookingResponse response = cLient.ReceiveDeposit(new ReceiveDepositWrapper(bookingResponse.Data.Id, bookingResponse.Data.Branch.Id, depost, PaymentMethodItem != null ? PaymentMethodItem.Value : (int)PaymentMethodEnum.CASH));
                            //        if (response != null && response.Status == (int)ResponseEnum.OK)
                            //        {
                            //            isCreate = true;
                            //            BookingUpdate = response.Data;
                            //            t.Close();
                            //        }
                            //    }
                            //    else
                            //    {
                                    isCreate = true;
                                    BookingUpdate = bookingResponse.Data;
                                    t.Close();
                            //    }
                               
                            //}
                          
                        }
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MRSSAGE_WRONG_INPUT);

                    }
                });
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if (p != null)
                    {
                        p.Close();
                    }
                });
                AddCustomerCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ManageCustomerWindow window = new ManageCustomerWindow();
                    window.DataContext = new ManageCustomerViewModel(customerFristName, customerLastName, CustomerPhone);
                    window.ShowDialog();
                    ManageCustomerViewModel createCustomerVM = window.DataContext as ManageCustomerViewModel;
                    if (createCustomerVM.IsCreate == true)
                    {
                        CustomerName = createCustomerVM.CustomerUpdate.Name;
                        CustomerPhone = createCustomerVM.CustomerUpdate.Phone;
                        customerId = createCustomerVM.CustomerUpdate.Id;
                        customerLastName = createCustomerVM.LastName;
                        customerFristName = createCustomerVM.FirstName;
                        //CustomerAddress = createCustomerVM.Address;
                        //CustomersBirth = Utils.Utils.GetDateFormatVN(createCustomerVM.Birthday); 
                        //Cus

                    }
                });
                ByCusByPhone = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    FindCustomerByPhone();
                });
                TextChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if(CustomerPhone == "")
                    {
                        CustomerName = "";
                        customerLastName = "";
                        customerFristName = "";
                    }
                });
                ChooseFoodCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    List<BillResponse> BookingFoodList = new List<BillResponse>();
                    BookingFoodList = FoodRequest.Where(x=>x.IsGift ==0).ToList();
                    OrderFoodBookingWindow order = new OrderFoodBookingWindow();
                    order.DataContext = new ChooseFoodBookingViewModel(brandId,branchId,BookingFoodList, false);
                    order.ShowDialog();
                    var model = order.DataContext as ChooseFoodBookingViewModel;
                    if (model != null && model.FoodOrderList != null && model.FoodOrderList.Count > 0 && model.isConfirm)
                    {
                        List<BillResponse> tmp = FoodRequest.Where(x => x.IsGift == 0).ToList();
                        if (tmp!= null && tmp.Count > 0)
                        {
                            foreach(BillResponse b in model.FoodOrderList)
                            {
                                BillResponse bill = tmp.Where(x => x.FoodId == b.FoodId).FirstOrDefault();
                                if (bill != null)
                                {
                                    bill.Quantity = b.Quantity;
                                    FoodRequest.Remove(bill);
                                    FoodRequest.Add(bill);
                                }
                                else
                                {
                                    FoodRequest.Add(b);
                                }

                            }
                        }
                        else
                        {
                            model.FoodOrderList.ForEach(FoodRequest.Add);

                        }
                        FoodNameList =string.Format("Gọi món: {0} \nTặng món: {1}", String.Concat(FoodRequest.Where(x=>x.IsGift == 0).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))), String.Concat(FoodRequest.Where(x => x.IsGift == 1).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))));
                    }
                });
                ChooseFoodGiftCommand =  new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    List<BillResponse> BookingFoodList = new List<BillResponse>();
                    BookingFoodList = FoodRequest.Where(x => x.IsGift == 1).ToList();
                    OrderFoodBookingWindow order = new OrderFoodBookingWindow();
                    order.DataContext = new ChooseFoodBookingViewModel(brandId,branchId, BookingFoodList, true);
                    order.ShowDialog();
                    var model = order.DataContext as ChooseFoodBookingViewModel;
                    if (model != null && model.FoodOrderList != null && model.FoodOrderList.Count > 0 && model.isConfirm)
                    {
                        List<BillResponse> tmp = FoodRequest.Where(x => x.IsGift == 1).ToList();
                        if (tmp != null && tmp.Count > 0)
                        {
                            foreach (BillResponse b in model.FoodOrderList)
                            {
                                BillResponse bill = tmp.Where(x => x.FoodId == b.FoodId).FirstOrDefault();
                                if (bill != null)
                                {
                                    bill.Quantity = b.Quantity;
                                    FoodRequest.Remove(bill);
                                    FoodRequest.Add(bill);
                                }
                                else
                                {
                                    FoodRequest.Add(b);
                                }

                            }
                        }
                        else
                        {
                            model.FoodOrderList.ForEach(FoodRequest.Add);

                        }
                        FoodNameList = string.Format("Gọi món: {0} \nTặng món: {1}", String.Concat(FoodRequest.Where(x => x.IsGift == 0).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))), String.Concat(FoodRequest.Where(x => x.IsGift == 1).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))));

                    }
                });
                SeeDiagramCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    TableDiagramWindow window = new TableDiagramWindow();
                    window.DataContext = new TableDiagramViewModel(MessageValue.MESSAGE_FROM_AREA_VIEW);
                    window.ShowDialog();
                });
                //SeeDiagramCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                //{
                //    TableOrderWindow table = new TableOrderWindow();
                //    table.DataContext = new TableViewModel(MessageValue.MESSAGE_FROM_ORDER_CHOOSE_TABLE_TITLE, false);
                //    table.ShowDialog();
                //});
            }

        }
        public ManageBookingViewModel(Models.Booking booking)
        {
            if (currentUser != null)
            {
                searchVisibility = Visibility.Collapsed;
                CustomerPhoneEnabled = false;
                GetDetail(booking);
                if (booking.BookingStatus == 2 || booking.BookingStatus == 9)
                {
                    ReviceDepositVisibility = Visibility.Collapsed;
                    ReturnDepositVisibility = Visibility.Collapsed;
                }

                ReturnDepositCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ReturnDepositBookingWindow window = new ReturnDepositBookingWindow();
                    window.DataContext = new ReturnDepositBookingViewModel(booking);
                    window.ShowDialog();
                    var model = window.DataContext as ReturnDepositBookingViewModel;
                    if (model.isSuccess)
                    {
                        BookingUpdate = model.CurrentBooking;
                    }

                });
                ReviceDepositCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ReceviceDepositBookingWindow window = new ReceviceDepositBookingWindow();
                    window.DataContext = new ReceviceDepositBookingViewModel(booking);
                    window.ShowDialog();
                    var model = window.DataContext as ReceviceDepositBookingViewModel;
                    if (model.isSuccess)
                    {
                        BookingUpdate = model.CurrentBooking;
                    }
                });

                SelectionChangedEmployeeCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if (BookingFormItem != null)
                    {
                        if (BookingFormItem.Value == (int)BookingTypeEnum.CUSTOMER)
                        {
                            EmployeeVisibility = Visibility.Collapsed;
                        }
                        else if (BookingFormItem.Value == (int)BookingTypeEnum.EMPLOYEE)
                        {
                            EmployeeVisibility = Visibility.Visible;
                            GetAllEmployees(currentUser.RestaurantBrandId,booking.Branch.Id);
                        }
                    }
                });
                AddQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if (QuantityPeople != 999)
                        QuantityPeople++;
                });
                SubQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    if (QuantityPeople > 0) QuantityPeople--;
                });
                OrderCommand = new RelayCommand<Window>((t) => { return true; }, t =>
                {
                    BookingClient cLient = new BookingClient(this, this, this);
                    if (BookingTime == null)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_NOT_FAIL_TIME_BOOKING);
                        return;
                    }
                    else if (string.IsNullOrEmpty(CustomerPhone) || !Utils.Utils.IsNumber(CustomerPhone))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_PHONE_ERROR);
                        return;
                    }
                    else if (QuantityPeople <= 0)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_SLOT_CUSTOMER);
                        return;
                    }
                    else if (string.IsNullOrEmpty(CustomerName))
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_BOOKING_EMPTY_CUSTOMER);
                    }
                    else
                    {
                        decimal amount = Utils.Utils.FormatMoneyDecimal(Amount);
                        amount = BookingUpdate.DepositAmount;
                        List<FoodUsing> ListFoodRequest = new List<FoodUsing>();
                        foreach (BillResponse f in FoodRequest)
                        {
                            FoodUsing food = new FoodUsing(f.FoodId, f.Quantity, f.IsGift);
                            ListFoodRequest.Add(food);
                        }
                        string Time = Utils.Utils.GetDateFormatVN(BookingTime.Date).ToString() + " " + TimeItem + ":00";
                        EditBookingWrapper wrapper = new EditBookingWrapper(booking.Id, booking.Branch.Id, string.IsNullOrEmpty(OtherRequest) ? "" : OtherRequest, string.IsNullOrEmpty(Note) ? "" : Note, QuantityPeople,
                            ListFoodRequest, Time, customerId, CustomerName, CustomerPhone, customerFristName, customerLastName, amount);
                        BookingResponse bookingResponse = cLient.UpdateBooking(wrapper, booking.Id);
                        if (bookingResponse != null && bookingResponse.Status == (int)ResponseEnum.OK)
                        {
                            isCreate = true;
                            BookingUpdate = bookingResponse.Data;
                            t.Close();
                        }
                    }
                });
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if (p != null)
                    {
                        p.Close();
                    }
                });
                TextChangedCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if (CustomerPhone == "")
                    {
                        CustomerName = "";
                        customerLastName = "";
                        customerFristName = "";
                    }
                });
                AddCustomerCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ManageCustomerWindow window = new ManageCustomerWindow();
                    window.DataContext = new ManageCustomerViewModel(customerFristName, customerLastName, CustomerPhone);
                    window.ShowDialog();
                    ManageCustomerViewModel createCustomerVM = window.DataContext as ManageCustomerViewModel;
                    if (createCustomerVM.IsCreate == true)
                    {
                        CustomerName = createCustomerVM.CustomerUpdate.Name;
                        CustomerPhone = createCustomerVM.CustomerUpdate.Phone;
                        customerId = createCustomerVM.CustomerUpdate.Id;
                        customerLastName = createCustomerVM.LastName;
                        customerFristName = createCustomerVM.FirstName;
                    }
                });
                ByCusByPhone = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    FindCustomerByPhone();
                });
                ChooseFoodCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    List<BillResponse> BookingFoodList = new List<BillResponse>();
                    BookingFoodList = FoodRequest.Where(x => x.IsGift == 0).ToList();
                    OrderFoodBookingWindow order = new OrderFoodBookingWindow();
                    order.DataContext = new ChooseFoodBookingViewModel(currentUser.RestaurantBrandId,booking.Branch.Id, BookingFoodList, false);
                    order.ShowDialog();
                    var model = order.DataContext as ChooseFoodBookingViewModel;
                    if (model != null && model.FoodOrderList != null && model.FoodOrderList.Count > 0 && model.isConfirm)
                    {
                        List<BillResponse> tmp = FoodRequest.Where(x => x.IsGift == 0).ToList();
                        if (tmp != null && tmp.Count > 0)
                        {
                            foreach (BillResponse b in model.FoodOrderList)
                            {
                                BillResponse bill = tmp.Where(x => x.FoodId == b.FoodId).FirstOrDefault();
                                if (bill != null)
                                {
                                    bill.Quantity = b.Quantity;
                                    FoodRequest.Remove(bill);
                                    FoodRequest.Add(bill);
                                }
                                else
                                {
                                    FoodRequest.Add(b);
                                }

                            }
                        }
                        else
                        {
                            model.FoodOrderList.ForEach(FoodRequest.Add);

                        }
                        FoodNameList = string.Format("Gọi món: {0} \nTặng món: {1}", String.Concat(FoodRequest.Where(x => x.IsGift == 0).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))), String.Concat(FoodRequest.Where(x => x.IsGift == 1).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))));

                    }
                });
                ChooseFoodGiftCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    List<BillResponse> BookingFoodList = new List<BillResponse>();
                    BookingFoodList = FoodRequest.Where(x => x.IsGift == 1).ToList();
                    OrderFoodBookingWindow order = new OrderFoodBookingWindow();
                    order.DataContext = new ChooseFoodBookingViewModel(currentUser.RestaurantBrandId,booking.Branch.Id, BookingFoodList, true);
                    order.ShowDialog();
                    var model = order.DataContext as ChooseFoodBookingViewModel;
                    if (model != null && model.FoodOrderList != null && model.FoodOrderList.Count > 0 && model.isConfirm)
                    {
                        List<BillResponse> tmp = FoodRequest.Where(x => x.IsGift == 1).ToList();
                        if (tmp != null && tmp.Count > 0)
                        {
                            foreach (BillResponse b in model.FoodOrderList)
                            {
                                BillResponse bill = tmp.Where(x => x.FoodId == b.FoodId).FirstOrDefault();
                                if (bill != null)
                                {
                                    bill.Quantity = b.Quantity;
                                    FoodRequest.Remove(bill);
                                    FoodRequest.Add(bill);
                                }
                                else
                                {
                                    FoodRequest.Add(b);
                                }

                            }
                        }
                        else
                        {
                            model.FoodOrderList.ForEach(FoodRequest.Add);

                        }
                        FoodNameList = string.Format("Gọi món: {0} \nTặng món: {1}", String.Concat(FoodRequest.Where(x => x.IsGift == 0).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))), String.Concat(FoodRequest.Where(x => x.IsGift == 1).Select(o => string.Format("{0} ({1} {2}),", o.FoodName, o.Quantity, o.UnitType))));

                    }
                });
                #region Dat
                SeeDiagramCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    TableDiagramWindow window = new TableDiagramWindow();
                    window.DataContext = new TableDiagramViewModel(MessageValue.MESSAGE_FROM_AREA_VIEW);
                    window.ShowDialog();
                });
                #endregion
            }
        }
        public void FindCustomerByPhone()
        {
            CustomerClient client = new CustomerClient(this, this, this);
            //FindCustomerWrapper wrapper = new FindCustomerWrapper(string.IsNullOrEmpty(CustomerName) ? "" : CustomerName, string.IsNullOrEmpty(CustomerPhone) ? "" : CustomerPhone);
            CustomerRegisterResponse response = client.FindCustomer(CustomerPhone, currentUser.BranchId);//toan
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                currentCustomer = response.Data; // Dat
                CustomerName = response.Data.Name;
                CustomerPhone = response.Data.Phone;
                customerId = response.Data.Id;
                customerLastName = response.Data.LastName;
                customerFristName = response.Data.FirstName;
            }
            else
            {
                NotificationMessage.Error("Số điện thoại này chưa đăng kí thẻ thành viên!!! Vui lòng đăng kí để sử dụng dịch vụ");
                return;
            }      
        }

        public void GetAllEmployees(int brandId,long branchId)
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
            EmployeeResponses employee = client.GetAllEmployeesResponses(brandId,branchId, Constants.OFFSET, (int)LimitEnum.ALL, Constants.STATUS, (int) StatusEnum.NOT, Constants.NOT_STATUS);
            if (employee != null && employee.Status == (int)ResponseEnum.OK && employee.Data != null)
            {
                employee.Data.List.ForEach(EmployeeList.Add);
            }
        }

        public FrameworkElement GetWindowParent(System.Windows.Controls.UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }
            return parent;
        }
        public void LogError(Exception ex, string infoMessage)
        {
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

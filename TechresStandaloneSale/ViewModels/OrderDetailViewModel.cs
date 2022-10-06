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
using System.Windows.Threading;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.ViewModels.Booking;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _ConvertNameAdditionFood; 
        public string ConvertNameAdditionFood
        {
            get => _ConvertNameAdditionFood; 
            set
            {
                _ConvertNameAdditionFood = value;
                OnPropertyChanged("_ConvertNameAdditionFood"); 
            }
        }
        public ICommand PaymentClick { get; set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand CancelCustomerCommand { get; set; }
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand CheckedVATCommand { get; set; }
        public ICommand CheckedBookingCommand { get; set; }
        public ICommand SearchCustomerPhoneCommand { get; set; }
        public ICommand PrintTempBill { get; set; }
        public ICommand BtnViewOrderDetail { get; set; }
        public ICommand BillAndPayment { get; set; }
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand EnterTextBoxCustomerCommand { get; set; }
        public ICommand DownTextBoxCustomerCommand { get; set; }
        public ICommand UpTextBoxCustomerCommand { get; set; }
        public ICommand BtnAddCustomerCommand { get; set; }
        public ICommand EnterCustomerCommand { get; set; }
        public ICommand CheckPromotionCommand { get; set; }
        public ICommand UnCheckPromotionCommand { get; set; }
        public ICommand BackUC { get; set; }
        public ICommand CancelPromotionCommand { get; set; }
        public ICommand PrintBillCommand { get; set; }

        private string _CustomerText;
        public string CustomerText { get => _CustomerText; set { _CustomerText = value; OnPropertyChanged("CustomerText"); } }
        private string _PromotionContent;
        public string PromotionContent { get => _PromotionContent; set { _PromotionContent = value; OnPropertyChanged("PromotionContent"); } }
        private string _PromotionNote;
        public string PromotionNote { get => _PromotionNote; set { _PromotionNote = value; OnPropertyChanged("PromotionNote"); } }

        private string _PromotionAmount;
        public string PromotionAmount { get => _PromotionAmount; set { _PromotionAmount = value; OnPropertyChanged("PromotionAmount"); } }
        private Visibility _PromotionVisibility;
        public Visibility PromotionVisibility { get => _PromotionVisibility; set { _PromotionVisibility = value; OnPropertyChanged("PromotionVisibility"); } }
        private Visibility _BookingVisibility;
        public Visibility BookingVisibility { get => _BookingVisibility; set { _BookingVisibility = value; OnPropertyChanged("BookingVisibility"); } }
        private Visibility _VatVisibility;
        public Visibility VatVisibility { get => _VatVisibility; set { _VatVisibility = value; OnPropertyChanged("VatVisibility"); } }

        private Visibility _PaymentVisibility;
        public Visibility PaymentVisibility
        {
            get { return _PaymentVisibility; }
            set { _PaymentVisibility = value; OnPropertyChanged("PaymentVisibility"); }
        }
        private Visibility _ConfirmPaymentVisibility;
        public Visibility ConfirmPaymentVisibility
        {
            get { return _ConfirmPaymentVisibility; }
            set { _ConfirmPaymentVisibility = value; OnPropertyChanged("ConfirmPaymentVisibility"); }
        }
        private Visibility _PrintVisibility;
        public Visibility PrintVisibility
        {
            get { return _PrintVisibility; }
            set { _PrintVisibility = value; OnPropertyChanged("PrintVisibility"); }
        }
        private bool _IsVat { get; set; }
        public bool IsVat { get => _IsVat; set { _IsVat = value; OnPropertyChanged("IsVat"); } }
        private bool _IsBooking { get; set; }
        public bool IsBooking { get => _IsBooking; set { _IsBooking = value; OnPropertyChanged("IsBooking"); } }
        private bool _IsEnablePayment { get; set; }
        public bool IsEnablePayment { get => _IsEnablePayment; set { _IsEnablePayment = value; OnPropertyChanged("IsEnablePayment"); } }
        private bool _IsEnableConfirmPayment { get; set; }
        public bool IsEnableConfirmPayment { get => _IsEnableConfirmPayment; set { _IsEnableConfirmPayment = value; OnPropertyChanged("IsEnableConfirmPayment"); } }
        private bool _IsEnablePrint { get; set; }
        public bool IsEnablePrint { get => _IsEnablePrint; set { _IsEnablePrint = value; OnPropertyChanged("IsEnablePrint"); } }
        private string _OrderCode { get; set; }
        public string OrderCode { get => _OrderCode; set { _OrderCode = value; OnPropertyChanged("OrderCode"); } }
        private string _CreateTime { get; set; }
        public string CreateTime { get => _CreateTime; set { _CreateTime = value; OnPropertyChanged("CreateTime"); } }
        private string _CustomerPhone { get; set; }
        public string CustomerPhone { get => _CustomerPhone; set { _CustomerPhone = value; OnPropertyChanged("CustomerPhone"); } }
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged("CustomerName"); } }
        private string _CustomerAddress { get; set; }
        public string CustomerAddress { get => _CustomerAddress; set { _CustomerAddress = value; OnPropertyChanged("CustomerAddress"); } }
        private string _TotalAmount { get; set; }
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }

        private string _Amount { get; set; }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }

        private string _PaidAmount { get; set; }
        public string PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged("PaidAmount"); } }
        private string _VAT { get; set; }
        public string VAT { get => _VAT; set { _VAT = value; OnPropertyChanged("VAT"); } }
        private string _VATAmount { get; set; }
        public string VATAmount { get => _VATAmount; set { _VATAmount = value; OnPropertyChanged("VATAmount"); } }
        private string _ReturnDepositAmount { get; set; }
        public string ReturnDepositAmount { get => _ReturnDepositAmount; set { _ReturnDepositAmount = value; OnPropertyChanged("ReturnDepositAmount"); } }
        private decimal DepositAmount;
        private decimal TotalPaymentAmount;
        private string _Point { get; set; }
        public string Point { get => _Point; set { _Point = value; OnPropertyChanged("Point"); } }
        private string _AloPoint { get; set; }
        public string AloPoint { get => _AloPoint; set { _AloPoint = value; OnPropertyChanged("AloPoint"); } }
        private string _AccumulatePoint { get; set; }
        public string AccumulatePoint { get => _AccumulatePoint; set { _AccumulatePoint = value; OnPropertyChanged("AccumulatePoint"); } }
        private string _PromotionPoint { get; set; }
        public string PromotionPoint { get => _PromotionPoint; set { _PromotionPoint = value; OnPropertyChanged("PromotionPoint"); } }
        private string _AddPoint { get; set; }
        public string AddPoint { get => _AddPoint; set { _AddPoint = value; OnPropertyChanged("AddPoint"); } }
        private string _DiscountAmount { get; set; }
        public string DiscountAmount { get => _DiscountAmount; set { _DiscountAmount = value; OnPropertyChanged("DiscountAmount"); } }

        private static ContentControl _MainContentControl;
        public ObservableCollection<Models.BillResponse> bills;
        public ObservableCollection<Models.BillResponse> Bills
        {
            get
            {
                return bills;
            }
            set
            {
                bills = value;
                OnPropertyChanged("Bills");
            }
        }

        public ObservableCollection<Promotion> _PromotionList = new ObservableCollection<Promotion>();
        public ObservableCollection<Promotion> PromotionList
        {
            get
            {
                return _PromotionList;
            }
            set
            {
                _PromotionList = value;
                OnPropertyChanged("PromotionList");
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
        public Visibility _CancelCustomerVisibility;
        public Visibility CancelCustomerVisibility
        {
            get
            {
                return _CancelCustomerVisibility;
            }
            set
            {
                _CancelCustomerVisibility = value;

                OnPropertyChanged("CancelCustomerVisibility");
            }
        }
        public Visibility _SaveCustomerVisibility;
        public Visibility SaveCustomerVisibility
        {
            get
            {
                return _SaveCustomerVisibility;
            }
            set
            {
                _SaveCustomerVisibility = value;

                OnPropertyChanged("SaveCustomerVisibility");
            }
        }
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        public long orderStatus;
        public long promotionId;
        public async void GetDetail(long OrderId)
        {
            OrdersClient client = new OrdersClient(this, this, this);
            if (OrderId > 0)
            {
                DialogHostOpen = true;
                OrderItemResponse response = await Task.Run(() => client.GetOrderById(OrderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.NOT_STATUS));
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
                {
                    SetOrderDetail(response.Data);
                }
                DialogHostOpen = false;
            }
        }
        public void RealTime(long OrderId)
        {
            string LinkSocket = string.Format("restaurants/{0}/branches/{1}/orders/{2}", currentUser.RestaurantId, currentUser.BranchId, OrderId);
            SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocket);
            SocketIOManager.Instance.socket.On(LinkSocket, (data) =>
            {
                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    if (data == null) return;
                    GetDetail(OrderId);

                }), DispatcherPriority.ContextIdle);
            });
        }

        //public void GetPromotion(long branchId, long customerId)
        //{
        //    if (PromotionList == null)
        //    {
        //        PromotionList = new ObservableCollection<Promotion>();
        //    }
        //    else
        //    {
        //        PromotionList.Clear();
        //    }
        //    PromotionClient client = new PromotionClient(this, this, this);
        //    //PromotionResponse response = client.GetPromotionApplying(branchId, customerId);
        //    if (response != null && response.Status == (int)ResponseEnum.OK)
        //    {
        //        response.Data.ForEach(PromotionList.Add);
        //    }
        //}

        public Customer customer;

        public void SetOrderDetail(OrderBillData orderBillData)
        {
            if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM)
            {
                IsEnablePayment = true;
                IsEnableConfirmPayment = true;
                IsEnablePrint = true;
                PaymentVisibility = Visibility.Visible;
                ConfirmPaymentVisibility = Visibility.Visible;
                PrintVisibility = Visibility.Visible;
                if (orderBillData.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE)
                {
                    IsEnablePayment = true;
                    IsEnablePrint = true;
                    PaymentVisibility = Visibility.Visible;
                    ConfirmPaymentVisibility = Visibility.Collapsed;
                    PrintVisibility = Visibility.Visible;
                }
            }
            else
            {
                if (orderBillData.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                {
                    IsEnablePayment = true;
                    IsEnableConfirmPayment = true;
                    IsEnablePrint = true;
                    PaymentVisibility = Visibility.Visible;
                    ConfirmPaymentVisibility = Visibility.Visible;
                    PrintVisibility = Visibility.Visible;
                }
                else if (orderBillData.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE)
                {
                    IsEnablePayment = true;
                    IsEnablePrint = true;
                    PaymentVisibility = Visibility.Visible;
                    //ConfirmPaymentVisibility = Visibility.Visible;
                    PrintVisibility = Visibility.Visible;
                }
            }

            if ( orderBillData.CustomerId > 0)
            {
                //CustomerText = orderBillData.Customer.Name;
                CancelCustomerVisibility = Visibility.Visible;
                SaveCustomerVisibility = Visibility.Collapsed;
                CustomerName = orderBillData.CustomerName;
                CustomerPhone = orderBillData.CustomerPhone;
                CustomerAddress = orderBillData.CustomerAddress;
                //GetPromotion(currentUser.BranchId, orderBillData.CustomerId);
            }
            else
            {
                CustomerText = "";
                CustomerName = string.Empty;
                CustomerPhone = string.Empty;
                CustomerAddress = string.Empty;
                CancelCustomerVisibility = Visibility.Collapsed;
                SaveCustomerVisibility = Visibility.Collapsed;
                if (PromotionList == null)
                {
                    PromotionList = new ObservableCollection<Promotion>();
                }
                else
                {
                    PromotionList.Clear();
                }
            }
            PromotionVisibility = Visibility.Collapsed;

            if (orderBillData.RestaurantPromotionVoucherId > 0)
            {
                PromotionVisibility = Visibility.Visible;
                PromotionAmount = Utils.Utils.FormatMoney(orderBillData.DiscountPromotionAmount);
                PromotionClient promotionClient = new PromotionClient(this, this, this);

                PromotionItemResponse promotionItemResponse = promotionClient.GetPromotionDetail(orderBillData.RestaurantPromotionVoucherId);
                if (promotionItemResponse != null && promotionItemResponse.Status == (int)ResponseEnum.OK)
                {
                    PromotionContent = promotionItemResponse.Data.Name;
                    PromotionNote = promotionItemResponse.Data.Description;
                }
            }
           // OrderCode = orderBillData.TableMergeListName.Count > 0 ? string.Format("#{0} - {1}[{2}]", orderBillData.Id, orderBillData.TableName, Utils.Utils.convertFormListString(orderBillData.TableMergeListName)) : string.Format("#{0} - {1}", orderBillData.Id, orderBillData.TableName);
            CreateTime = orderBillData.CreatedAt;
            orderStatus = orderBillData.OrderStatus;
            if (Bills != null)
            {
                Bills.Clear();
            }
            else
            {
                Bills = new ObservableCollection<BillResponse>();
            }
            //foreach (BillResponse f in orderBillData.Foods)
            //{
            //    if(f.OrderDetailAdditions != null && f.OrderDetailAdditions.Count > 0)
            //    {

            //    }
            //}
            orderBillData.Foods.ForEach(Bills.Add);
            Amount = Utils.Utils.FormatMoney(orderBillData.Amount);
            if (orderBillData.VatAmount > 0)// Dat
            // if(orderBillData.Vat > 0)
            {
                IsVat = true;
                VAT = string.Format("{0}%", orderBillData.Vat);
                VATAmount = Utils.Utils.FormatMoney(orderBillData.VatAmount);
            }
            else
            {
                IsVat = false;
                VAT = string.Format("{0}%", currentSetting.Vat);
                VATAmount = Utils.Utils.FormatMoney(0);
            }
            TotalAmount = Utils.Utils.FormatMoney(orderBillData.TotalAmount);
            PaidAmount = Utils.Utils.FormatMoney(orderBillData.TotalAmount);
            if (orderBillData.BookingDepositAmount > 0 && (orderBillData.OrderStatus == 1 || orderBillData.OrderStatus == 4))
            {
                if (orderBillData.IsReturnDeposit == (int)StatusEnum.YES)
                {
                    IsBooking = true;
                    ReturnDepositAmount = Utils.Utils.FormatMoney(orderBillData.BookingDepositAmount);
                    TotalAmount = Utils.Utils.FormatMoney(orderBillData.TotalAmount - orderBillData.BookingDepositAmount);
                    PaidAmount = Utils.Utils.FormatMoney(orderBillData.TotalAmount - orderBillData.BookingDepositAmount);
                }
                else
                {
                    IsBooking = false;
                }
            }
            else
            {
                BookingVisibility = Visibility.Collapsed;
            }
            VatVisibility = Visibility.Visible;
            if (orderBillData.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || orderBillData.OrderStatus == (int)OrderStatusEnum.DELIVERING)
            {
                VatVisibility = Visibility.Collapsed;
            }
            TotalPaymentAmount = orderBillData.TotalAmount;
            DepositAmount = orderBillData.BookingDepositAmount;
            DiscountAmount = Utils.Utils.FormatMoney(orderBillData.DiscountAmount);
            Point = Utils.Utils.FormatMoney(orderBillData.MembershipTotalPointUsed);
            AloPoint = Utils.Utils.FormatMoney(orderBillData.MembershipAloPointUsed);
            AccumulatePoint = Utils.Utils.FormatMoney(orderBillData.MembershipAccumulatePointUsed);
            PromotionPoint = Utils.Utils.FormatMoney(orderBillData.MembershipPromotionPointUsed);
            AddPoint = Utils.Utils.FormatMoney(orderBillData.MembershipPointAdded);

          
            //customer = orderBillData.Customer;
        }
       
        public OrderDetailViewModel(long OrderId)
        {
            
            try
            {
                LoadedUserControlCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                    IsEnablePayment = false;
                    IsEnableConfirmPayment = false;
                    IsEnablePrint = false;
                    PaymentVisibility = Visibility.Visible;
                    ConfirmPaymentVisibility = Visibility.Visible;
                    PrintVisibility = Visibility.Visible;
                    GetDetail(OrderId);
                    RealTime(OrderId);

                });
                CheckedVATCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        if(IsVat)
                        {

                            ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                            string contentConfirm = MessageValue.MESSAGE_CONFIRM_VAT;
                            string Title = MessageValue.MESSAGE_CONFIRM_VAT_TITLE;
                            string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                            string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                            confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                            confirmDeleteWindow.ShowDialog();
                            var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                            if (confirm.isConfirm)
                            {
                                DialogHostOpen = true;
                                System.Threading.Thread.Sleep(1000);
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                BaseResponse baseResponse = await Task.Run(() => ordersClient.ApplyVAT(OrderId, IsVat ? Constants.STATUS : Constants.NOT_STATUS));
                                if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                {
                                    GetDetail(OrderId);
                                    IsVat = true;
                                }
                                DialogHostOpen = false;
                            }
                            else
                            {
                                IsVat = !IsVat;
                            }
                        }
                        else
                        {
                            ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                            string contentConfirm = MessageValue.MESSAGE_CONFIRM_NOT_VAT;
                            string Title = MessageValue.MESSAGE_CONFIRM_NOT_VAT_TITLE;
                            string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                            string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                            confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                            confirmDeleteWindow.ShowDialog();
                            var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                            if (confirm.isConfirm)
                            {
                                DialogHostOpen = true;
                                System.Threading.Thread.Sleep(1000);
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                BaseResponse baseResponse = await Task.Run(() => ordersClient.ApplyVAT(OrderId, IsVat ? Constants.STATUS : Constants.NOT_STATUS));
                                if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                {
                                    GetDetail(OrderId);
                                    IsVat = false;
                                }
                                DialogHostOpen = false;
                            }
                            else
                            {
                                IsVat = !IsVat;
                            }
                        }
                        
                    });
                });
                CheckedBookingCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        DialogHostOpen = true;
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = await Task.Run(() => ordersClient.ApplyReturnDeposit(OrderId));
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            GetDetail(OrderId);
                        }
                        DialogHostOpen = false;
                    });
                });
                EnterTextBoxCustomerCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    try
                    {
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
                                CustomerName = response.Data.Name;
                                CustomerPhone = response.Data.Phone;
                                CustomerAddress = response.Data.Address;
                                CancelCustomerVisibility = Visibility.Visible;
                                SaveCustomerVisibility = Visibility.Visible;
                                OrdersClient client = new OrdersClient(this, this, this);
                                OrderItemResponse orderItemResponse = client.AssignOrderToCustomer(new AssignOrderToCustomerWrapper(OrderId, customer.Id, customer.Name, customer.Phone, customer.Address));
                                if (orderItemResponse != null && orderItemResponse.Status == (int)ResponseEnum.OK)
                                {
                                    SetOrderDetail(orderItemResponse.Data);
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        WriteLog.logs("Lỗi là:" + e.Message);
                    }
                });
                AddCustomerCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    ManageCustomerWindow window = new ManageCustomerWindow();
                    window.DataContext = new ManageCustomerViewModel();
                    window.ShowDialog();
                    ManageCustomerViewModel createCustomerVM = window.DataContext as ManageCustomerViewModel;
                    if (createCustomerVM.IsCreate == true)
                    {
                        customer = createCustomerVM.CustomerUpdate;
                        CustomerName = createCustomerVM.CustomerUpdate.Name;
                        CustomerPhone = createCustomerVM.CustomerUpdate.Phone;
                        CustomerAddress = createCustomerVM.CustomerUpdate.Address;
                        CancelCustomerVisibility = Visibility.Visible;
                        SaveCustomerVisibility = Visibility.Visible;

                        OrdersClient client = new OrdersClient(this, this, this);
                        OrderItemResponse response = client.AssignOrderToCustomer(new AssignOrderToCustomerWrapper(OrderId, customer.Id, customer.Name, customer.Phone, customer.Address));
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            SetOrderDetail(response.Data);
                        }
                    }
                });
                CheckPromotionCommand = new RelayCommand<Promotion>((p) => { return true; }, p =>
                {
                    DialogHostOpen = true;
                    OrdersClient client = new OrdersClient(this, this, this);
                    OrderItemResponse orderItemResponse = client.AssignOrderToPromotion(new AssignOrderToPromotionWrapper(currentUser.BranchId, p.RestaurantPromotionVoucherId), OrderId);
                    if (orderItemResponse != null && orderItemResponse.Status == (int)ResponseEnum.OK)
                    {
                        SetOrderDetail(orderItemResponse.Data);
                    }
                    else
                    {
                        p.IsApplyPromotion = false;
                        int index = PromotionList.IndexOf(p);
                        PromotionList.Remove(p);
                        PromotionList.Insert(index, p);
                    }
                    DialogHostOpen = false;

                });
                CancelPromotionCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(MessageValue.MESSAGE_CANCEL_PROMOTION_CONTENT, MessageValue.MESSAGE_CANCEL_PROMOTION, MessageValue.MESSAGE_KHONG_CONTENT, MessageValue.MESSAGE_CO_CONTENT);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        DialogHostOpen = true;
                        OrdersClient client = new OrdersClient(this, this, this);
                        OrderItemResponse orderItemResponse = client.UnAssignOrderToPromotion(OrderId);
                        if (orderItemResponse != null && orderItemResponse.Status == (int)ResponseEnum.OK)
                        {
                            SetOrderDetail(orderItemResponse.Data);
                        }
                        DialogHostOpen = false;
                    }
                });
                CancelCustomerCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(MessageValue.MESSAGE_CANCEL_CUSTOMER_CONTENT, MessageValue.MESSAGE_CANCEL_CUSTOMER, MessageValue.MESSAGE_KHONG_CONTENT, MessageValue.MESSAGE_CO_CONTENT);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        DialogHostOpen = true;
                        CustomerText = "";
                        CancelCustomerVisibility = Visibility.Collapsed;
                        SaveCustomerVisibility = Visibility.Collapsed;
                        OrdersClient client = new OrdersClient(this, this, this);
                        OrderBillData orderBillData = new OrderBillData();
                        OrderItemResponse response = client.UnAssignOrderToCustomer(new AssignOrderToCustomerWrapper(OrderId, customer.Id, customer.Name, customer.Phone, customer.Address));
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            SetOrderDetail(response.Data);
                        }
                        DialogHostOpen = false;
                    }

                });
                BackUC = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                    _MainContentControl.Content = new HomeUserControl();
                    string LinkSocket = string.Format("restaurants/{0}/branches/{1}/orders/{2}", currentUser.RestaurantId, currentUser.BranchId, OrderId);
                    SocketIOManager.Instance.socket.EmitAsync("leave_room", LinkSocket);
                });
                PaymentClick = new RelayCommand<OrderDetailUserControl>((p) => { return true; }, p =>
                {
                    if ((int)BranchTypeEnum.LARGE == currentSetting.BranchType || currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)//toan
                    {
                        if (orderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || orderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                        {
                            DialogHostOpen = true;
                            OrdersClient client = new OrdersClient(this, this, this);
                            OrderItemResponse orderBillResponse = client.GetOrderById(OrderId, currentUser.BranchId, Constants.STATUS, Constants.STATUS);
                            if (orderBillResponse != null && orderBillResponse.Status == (int)ResponseEnum.OK && orderBillResponse.Data != null)
                            {
                                DialogHostOpen = false;
                                PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                                paymentWindow.DataContext = new PaymentOrderViewModel(orderBillResponse.Data.Id, orderBillResponse.Data.TotalAmount, orderBillResponse.Data.Amount, orderBillResponse.Data.Vat,
                                            orderBillResponse.Data.VatAmount, orderBillResponse.Data.DiscountAmount, orderBillResponse.Data.DiscountPercent, orderBillResponse.Data.MembershipTotalPointUsed, orderBillResponse.Data.ShippingFee, orderBillResponse.Data.BookingDepositAmount, orderBillResponse.Data.OrderStatus, orderBillResponse.Data.IsReturnDeposit == 0 ? 0 : orderBillResponse.Data.BookingDepositPaymentMethod);
                                paymentWindow.ShowDialog();

                                var baseComplete = paymentWindow.DataContext as PaymentOrderViewModel;
                                if (baseComplete.isSUCCESS == true)
                                {
                                    FrameworkElement window = GetWindowParent(p);
                                    var w = window as Window;
                                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                                    _MainContentControl.Content = new HomeUserControl();
                                }
                                //FrameworkElement window = GetWindowParent(p);
                                //var w = window as Window;
                                //_MainContentControl = w.FindName("ContentCt") as ContentControl;
                                //_MainContentControl.Content = new HomeUserControl();
                                //_MainContentControl.DataContext = new HomeViewModel();
                            }
                            DialogHostOpen = false;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_ORDER_NOT_DONE);
                        }
                    }                 
                    #region toan
                    else if ((int)BranchTypeEnum.MEDIUM == currentSetting.BranchType)
                    {
                        if ((int)BranchTypeOption.OPTIONTWO == currentSetting.BranchTypeOption)
                        {
                            DialogHostOpen = true;
                            OrdersClient client = new OrdersClient(this, this, this);
                            OrderItemResponse orderBillResponse = client.GetOrderById(OrderId, currentUser.BranchId, Constants.STATUS, Constants.STATUS);
                            if (orderBillResponse != null && orderBillResponse.Status == (int)ResponseEnum.OK && orderBillResponse.Data != null)
                            {
                                DialogHostOpen = false;
                                PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                                paymentWindow.DataContext = new PaymentOrderViewModel(orderBillResponse.Data.Id, orderBillResponse.Data.TotalAmount, orderBillResponse.Data.Amount, orderBillResponse.Data.Vat,
                                            orderBillResponse.Data.VatAmount, orderBillResponse.Data.DiscountAmount, orderBillResponse.Data.DiscountPercent, orderBillResponse.Data.MembershipTotalPointUsed, orderBillResponse.Data.ShippingFee, orderBillResponse.Data.BookingDepositAmount, orderBillResponse.Data.OrderStatus, orderBillResponse.Data.IsReturnDeposit == 0 ? 0 : orderBillResponse.Data.BookingDepositPaymentMethod);
                                paymentWindow.ShowDialog();

                                var baseComplete = paymentWindow.DataContext as PaymentOrderViewModel;
                                if (baseComplete.isSUCCESS == true)
                                {
                                    FrameworkElement window = GetWindowParent(p);
                                    var w = window as Window;
                                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                                    _MainContentControl.Content = new HomeUserControl();
                                }
                            }
                            DialogHostOpen = false;
                        }
                        else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                        {
                            if (orderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || orderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                            {
                                DialogHostOpen = true;
                                OrdersClient client = new OrdersClient(this, this, this);
                                OrderItemResponse orderBillResponse = client.GetOrderById(OrderId, currentUser.BranchId, Constants.STATUS, Constants.STATUS);
                                if (orderBillResponse != null && orderBillResponse.Status == (int)ResponseEnum.OK && orderBillResponse.Data != null)
                                {
                                    DialogHostOpen = false;
                                    PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                                    paymentWindow.DataContext = new PaymentOrderViewModel(orderBillResponse.Data.Id, orderBillResponse.Data.TotalAmount, orderBillResponse.Data.Amount, orderBillResponse.Data.Vat,
                                                orderBillResponse.Data.VatAmount, orderBillResponse.Data.DiscountAmount, orderBillResponse.Data.DiscountPercent, orderBillResponse.Data.MembershipTotalPointUsed, orderBillResponse.Data.ShippingFee, orderBillResponse.Data.BookingDepositAmount, orderBillResponse.Data.OrderStatus, orderBillResponse.Data.IsReturnDeposit == 0 ? 0 : orderBillResponse.Data.BookingDepositPaymentMethod);
                                    paymentWindow.ShowDialog();

                                    var baseComplete = paymentWindow.DataContext as PaymentOrderViewModel;
                                    if (baseComplete.isSUCCESS == true)
                                    {
                                        FrameworkElement window = GetWindowParent(p);
                                        var w = window as Window;
                                        _MainContentControl = w.FindName("ContentCt") as ContentControl;
                                        _MainContentControl.Content = new HomeUserControl();
                                    }
                                }
                                DialogHostOpen = false;
                            }
                            else
                            {
                                NotificationMessage.Warning(MessageValue.MESSAGE_ORDER_NOT_DONE);
                            }
                        }
                    }
                    else if ((int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                    {
                        if (orderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || orderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                        {
                            DialogHostOpen = true;
                            OrdersClient client = new OrdersClient(this, this, this);
                            OrderItemResponse orderBillResponse = client.GetOrderById(OrderId, currentUser.BranchId, Constants.STATUS, Constants.STATUS);
                            if (orderBillResponse != null && orderBillResponse.Status == (int)ResponseEnum.OK && orderBillResponse.Data != null)
                            {
                                DialogHostOpen = false;
                                PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                                paymentWindow.DataContext = new PaymentOrderViewModel(orderBillResponse.Data.Id, orderBillResponse.Data.TotalAmount, orderBillResponse.Data.Amount, orderBillResponse.Data.Vat,
                                            orderBillResponse.Data.VatAmount, orderBillResponse.Data.DiscountAmount, orderBillResponse.Data.DiscountPercent, orderBillResponse.Data.MembershipTotalPointUsed, orderBillResponse.Data.ShippingFee, orderBillResponse.Data.BookingDepositAmount, orderBillResponse.Data.OrderStatus, orderBillResponse.Data.IsReturnDeposit == 0 ? 0 : orderBillResponse.Data.BookingDepositPaymentMethod);
                                paymentWindow.ShowDialog();

                                var baseComplete = paymentWindow.DataContext as PaymentOrderViewModel;
                                if (baseComplete.isSUCCESS == true)
                                {
                                    FrameworkElement window = GetWindowParent(p);
                                    var w = window as Window;
                                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                                    _MainContentControl.Content = new HomeUserControl();
                                }
                            }
                            DialogHostOpen = false;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_ORDER_NOT_DONE);
                        }
                    }
                    #endregion
                });
                BillAndPayment = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        DialogHostOpen = true;
                        decimal cashAmount = Utils.Utils.FormatMoneyDecimal(TotalAmount);
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = await Task.Run(() => ordersClient.MakePayment(OrderId, new CompleteOrderWrapper(0, cashAmount, 0, 0)));
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            string Title = "HÓA ĐƠN THANH TOÁN";
                            PrintBill(Title, OrderId);
                            FrameworkElement window = GetWindowParent(p);
                            var w = window as Window;
                            _MainContentControl = w.FindName("ContentCt") as ContentControl;
                            _MainContentControl.Content = new HomeUserControl();
                            //     _MainContentControl.DataContext = new HomeViewModel();
                        }
                        DialogHostOpen = false;
                    });
                });
                PrintBillCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    string Title = "HÓA ĐƠN THANH TOÁN";
                    PrintBill(Title, OrderId);
                });
                PrintTempBill = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        DialogHostOpen = true;
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = await Task.Run(() => ordersClient.MakePaymentWaiting(OrderId));
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            #region Đạt
                            GetDetail(OrderId);
                            ConfirmPaymentVisibility = Visibility.Collapsed;
                            #endregion
                            string Title = "HÓA ĐƠN THANH TOÁN";
                            PrintBill(Title, OrderId);
                        }
                        DialogHostOpen = false;
                    });
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
                    }

                });
            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.Message);
                //Console.Write(ex.Message);
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
                OrderItemResponse response = ordersClient.GetOrderById(orderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.STATUS);
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
                {
                    DeviceClient deviceClient = new DeviceClient();
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
            Console.Write(ex.Message);
            Console.Write(infoMessage);
        }

        FrameworkElement GetWindowParent(System.Windows.Controls.UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }

            return parent;
        }
    }
}

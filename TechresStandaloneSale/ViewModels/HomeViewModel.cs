using FFImageLoading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.UserControlView.CreateOrder;
using TechresStandaloneSale.ViewModels.Booking;
using TechresStandaloneSale.ViewModels.Dialogs;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;
using static TechresStandaloneSale.Helpers.PrintBill;

namespace TechresStandaloneSale.ViewModels
{
    public class HomeViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        //private ImageBrush _BookingForeground { get; set; }
        //public ImageBrush BookingForeground { get => _BookingForeground; set { _BookingForeground = value; OnPropertyChanged("BookingForeground"); } }

        //private ImageBrush _BookingBorderBrush { get; set; }
        //public ImageBrush BookingBorderBrush { get => _BookingBorderBrush; set { _BookingBorderBrush = value; OnPropertyChanged("BookingBorderBrush"); } }

        private ImageBrush _BookingBackground { get; set; }
        public ImageBrush BookingBackground { get => _BookingBackground; set { _BookingBackground = value; OnPropertyChanged("BookingBackground"); } }


        private ImageBrush _OnlineBackground { get; set; }
        public ImageBrush OnlineBackground { get => _OnlineBackground; set { _OnlineBackground = value; OnPropertyChanged("OnlineBackground"); } }

        private ImageBrush _PendingBackground { get; set; }
        public ImageBrush PendingBackground { get => _PendingBackground; set { _PendingBackground = value; OnPropertyChanged("PendingBackground"); } }

        private ImageBrush _HistoryBackground { get; set; }
        public ImageBrush HistoryBackground { get => _HistoryBackground; set { _HistoryBackground = value; OnPropertyChanged("HistoryBackground"); } }

        private Visibility _TextBoxSearch; 
        public Visibility TextBoxSearch
        {
            get => _TextBoxSearch; 
            set
            {
                _TextBoxSearch = value;
                OnPropertyChanged("TextBoxSearch"); 

            }
        }
        private Visibility _DateTimeVisibility { get; set; }
        public Visibility DateTimeVisibility { get => _DateTimeVisibility; set { _DateTimeVisibility = value; OnPropertyChanged("DateTimeVisibility"); } }
        private Visibility _RefreshVisibility { get; set; }
        public Visibility RefreshVisibility { get => _RefreshVisibility; set { _RefreshVisibility = value; OnPropertyChanged("RefreshVisibility"); } }
        private Visibility _CustomerDebitVisibility { get; set; }
        public Visibility CustomerDebitVisibility { get => _CustomerDebitVisibility; set { _CustomerDebitVisibility = value; OnPropertyChanged("CustomerDebitVisibility"); } }

        private DateTime _DatetimeInput;
        public DateTime DatetimeInput { get => _DatetimeInput; set { _DatetimeInput = value; OnPropertyChanged("DatetimeInput"); } }
        private DateTime _DatetimeDisplayDateEnd;
        public DateTime DatetimeDisplayDateEnd { get => _DatetimeDisplayDateEnd; set { _DatetimeDisplayDateEnd = value; OnPropertyChanged("DatetimeDisplayDateEnd"); } }
        private ObservableCollection<CardOrderItem> _Views = new ObservableCollection<CardOrderItem>();
        public ObservableCollection<CardOrderItem> Views
        {
            get
            {
                return _Views;
            }
            set
            {
                _Views = value;
                OnPropertyChanged("Views");
            }
        }
        private ObservableCollection<Order> itemsOrderDone;
        public ObservableCollection<Order> ItemsOrderDone
        {
            get
            {
                return itemsOrderDone;
            }
            set
            {
                itemsOrderDone = value;
                OnPropertyChanged("ItemsOrderDone");
            }
        }
        private ObservableCollection<Order> _OrderOnlineList;
        public ObservableCollection<Order> OrderOnlineList
        {
            get
            {
                return _OrderOnlineList;
            }
            set
            {
                _OrderOnlineList = value;
                OnPropertyChanged("OrderOnlineList");
            }
        }
        private ObservableCollection<Models.Booking> _OrderBookingList = new ObservableCollection<Models.Booking>();
        public ObservableCollection<Models.Booking> OrderBookingList
        {
            get
            {
                return _OrderBookingList;
            }
            set
            {
                _OrderBookingList = value;
                OnPropertyChanged("OrderBookingList");
            }
        }
        private ObservableCollection<Models.BillResponse> _OrderDetailOnlineList;
        public ObservableCollection<Models.BillResponse> OrderDetailOnlineList
        {
            get
            {
                return _OrderDetailOnlineList;
            }
            set
            {
                _OrderDetailOnlineList = value;
                OnPropertyChanged("OrderDetailOnlineList");
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
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged("CustomerName"); } }

        private string _CustomerAddress { get; set; }
        public string CustomerAddress { get => _CustomerAddress; set { _CustomerAddress = value; OnPropertyChanged("CustomerAddress"); } }

        private string _CustomerPhone { get; set; }
        public string CustomerPhone { get => _CustomerPhone; set { _CustomerPhone = value; OnPropertyChanged("CustomerPhone"); } }


        private string _ShipperAmount { get; set; }
        public string ShipperAmount { get => _ShipperAmount; set { _ShipperAmount = value; OnPropertyChanged("ShipperAmount"); } }
        private string _Note { get; set; }
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }
        private string _TotalOrderOnline { get; set; }
        public string TotalOrderOnline { get => _TotalOrderOnline; set { _TotalOrderOnline = value; OnPropertyChanged("TotalOrderOnline"); } }
        private string _OrderAmount { get; set; }
        public string OrderAmount { get => _OrderAmount; set { _OrderAmount = value; OnPropertyChanged("OrderAmount"); } }

        private string _PageContent { get; set; }
        public string PageContent { get => _PageContent; set { _PageContent = value; OnPropertyChanged("PageContent"); } }
        private string _RevenueTmp { get; set; }
        public string RevenueTmp { get => _RevenueTmp; set { _RevenueTmp = value; OnPropertyChanged("RevenueTmp"); } }

        private Visibility _SendFoodCookVisibility { get; set; }
        public Visibility SendFoodCookVisibility
        {
            get => _SendFoodCookVisibility; set { _SendFoodCookVisibility = value; OnPropertyChanged("SendFoodCookVisibility"); }
        }
        private Visibility _RevenueTmpVisibility;
        public Visibility RevenueTmpVisibility
        {
            get
            {
                return _RevenueTmpVisibility;
            }
            set
            {
                _RevenueTmpVisibility = value;
                OnPropertyChanged("RevenueTmpVisibility");
            }
        }
        private Order _OrderOnlineItem { get; set; }
        public Order OrderOnlineItem { get => _OrderOnlineItem; set { _OrderOnlineItem = value; OnPropertyChanged("OrderOnlineItem"); } }
        private bool _IsVat { get; set; }
        public bool IsVat { get => _IsVat; set { _IsVat = value; OnPropertyChanged("IsVat"); } }
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
        private Visibility _CustomerVisibility { get; set; }
        public Visibility CustomerVisibility { get => _CustomerVisibility; set { _CustomerVisibility = value; OnPropertyChanged("CustomerVisibility"); } }
        #region Dat
        private int _TotalPage;
        public int TotalPage { get => _TotalPage; set { _TotalPage = value; OnPropertyChanged("TotalPage"); UpdateEnableState(); } }
        private int _CurrentPage;
        public int CurrentPage { get => _CurrentPage; set { _CurrentPage = value; OnPropertyChanged("CurrentPage"); UpdateEnableState(); } }
        private bool _isNextEnabled;
        public bool IsNextEnabled
        {
            get => _isNextEnabled; set { _isNextEnabled = value; OnPropertyChanged("IsNextEnabled"); }
        }
        private bool _isPreviousEnabled;
        public bool IsPreviousEnabled
        {
            get => _isPreviousEnabled; set { _isPreviousEnabled = value; OnPropertyChanged("IsPreviousEnabled"); }
        }
        private bool _isFirstEnabled;
        public bool IsFirstEnabled
        {
            get => _isFirstEnabled; set { _isFirstEnabled = value; OnPropertyChanged("IsFirstEnabled"); }
        }
        private bool _isLastEnabled;
        public bool IsLastEnabled
        {
            get => _isLastEnabled; set { _isLastEnabled = value; OnPropertyChanged("IsLastEnabled"); }
        }
        private void UpdateEnableState()
        {
            IsFirstEnabled = CurrentPage > 1;
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < TotalPage;
            IsLastEnabled = CurrentPage < TotalPage;
        }
        #endregion Dat
        public ICommand SelectionDateChangedCommand { get; set; }
        public ICommand BtnOpening { get; set; }
        public ICommand BtnDone { get; set; }
        public ICommand BtnBooking { get; set; }
        public ICommand PageDoubleLeft { get; set; }
        public ICommand PageRight { get; set; }
        public ICommand PageLeft { get; set; }
        public ICommand PageDoubleRight { get; set; }
        public ICommand BtnCard { get; set; }
        public ICommand BtnList { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CustomerDebitCommand { get; set; }
        public ICommand LoadedUCCommand { get; set; }
        public ICommand CancelTableCommand { get; set; }
        public ICommand MoveFoodCommand { get; set; }
        public ICommand CustomerSlotCommand { get; set; }
        public ICommand MergeTableCommand { get; set; }
        public ICommand MoveTableCommand { get; set; }
        public ICommand HistoryActivityCommand { get; set; }
        public ICommand MouseSaveClick { get; set; }
        public ICommand MousePrintClick { get; set; }
        public ICommand MouseEnterCommand { get; set; }
        public ICommand MouseDoneClick { get; set; }
        public ICommand DebitCommand { get; set; }
        public ICommand SendFoodCookCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand BtnViewOrder { get; set; }
        public ICommand BtnPrintOrder { get; set; }
        public ICommand BtnHistoryActivityOrder { get; set; }
        public ICommand ChangeOrderOnlineCommand { get; set; }
        public ICommand BtnOnline { get; set; }
        public ICommand CheckedVATCommand { get; set; }
        public ICommand BtnActiveVatCommand { get; set; }
        public ICommand EditBookingCommand { get; set; }
        public ICommand BtnViewBooking { get; set; }
        public ICommand BtnConfirmBooking { get; set; }
        public ICommand BtnPrintBooking { get; set; }

        private static ContentControl _ContentOrder;
        public long orderId;
        private OrdersClient ordersClient;
        public User currentUser;
        public CardOrderItem cardOrderItem;
        public ContentControl _MainContentControl;
        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        public DeviceClient deviceClient = new DeviceClient();
        public DateTime currentDatetime;
        private bool isChange;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public long currentOrderOnlineId = 0;
        public long CurrentButton = 0;
        //private int TotalPage;
        //private int CurrentPage;
        private List<Kitchen> CurrentKitchen = new List<Kitchen>();

        private Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
        public void AddAndUpdateOrder(Order order)
        {
            if (order.OrderStatus != (int)OrderStatusEnum.DONE
                && order.OrderStatus != (int)OrderStatusEnum.DEBIT
                && order.OrderStatus != (int)OrderStatusEnum.MERGED
                 && order.OrderStatus != (int)OrderStatusEnum.CANCELLED
                  && order.OrderStatus != (int)OrderStatusEnum.PENDING)
            {
                cardOrderItem = new CardOrderItem();
                CardOrderItem orderItem = Views.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (orderItem != null)
                {
                    if (order.OrderStatus == orderItem.OrderStatus)
                    {
                        cardOrderItem.OrderId = order.Id;
                        cardOrderItem.OrderStatus = order.OrderStatus;
                        cardOrderItem.CustomerSlot = order.CustomerSlotNumber;
                        cardOrderItem.TotalOrderDetailCustomerRequest = order.TotalOrderDetailCustomerRequest;
                        cardOrderItem.TableName = order.TableName;
                        cardOrderItem.TableId = order.TableId;
                        cardOrderItem.MegerTableObject = order.TableMergeListName;
                        cardOrderItem.CashAmount = order.TotalAmount;
                        cardOrderItem.UsingTime = order.UsingTimeMinutesString;
                        cardOrderItem.IsOnline = order.IsOnline == Constants.STATUS ? true : false;
                        cardOrderItem.Amount = order.Amount;
                        cardOrderItem.Vat = (float)order.Vat;
                        cardOrderItem.VatAmount = order.VatAmount;
                        cardOrderItem.DiscountPercent = (float)order.DiscountPercent;
                        cardOrderItem.DiscountAmount = order.DiscountAmount;
                        cardOrderItem.PointOrder = order.MembershipTotalPointUsed;
                        //cardOrderItem.ShippingFee = order.ShippingFee;
                        cardOrderItem.ShippingFee = orderItem.ShippingFee; // Dat
                        cardOrderItem.IsTakeAway = order.IsTakeAway == Constants.STATUS ? true : false;
                        cardOrderItem.BookingDepositAmount = orderItem.BookingDepositAmount;
                        cardOrderItem.BookingInforId = order.BookingInforId;
                        cardOrderItem.IsReturnDeposit = order.IsReturnDeposit == 1 ? true : false;
                        cardOrderItem.TotalOrderDetailCustomerRequest = order.TotalOrderDetailCustomerRequest;
                        cardOrderItem.BookingDepositPaymentMethod = order.BookingDepositPaymentMethod;
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (order.OrderStatus == (int)OrderStatusEnum.OPENING)
                            {
                                if (cardOrderItem.CashAmount != 0)
                                {
                                    cardOrderItem.BtnDoneIsEnabled = false;
                                }
                                else
                                {
                                    cardOrderItem.BtnDoneIsEnabled = true;
                                }
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }
                        }
                        else
                        {
                            if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }
                        }
                        int index = Views.IndexOf(orderItem);
                        Views.Remove(orderItem);
                        Views.Insert(index, cardOrderItem);
                        Views.Select(std => std.OrderId).Distinct();
                    }
                    else
                    {
                        cardOrderItem.OrderId = order.Id;
                        cardOrderItem.OrderStatus = order.OrderStatus;
                        cardOrderItem.CustomerSlot = order.CustomerSlotNumber;
                        cardOrderItem.TotalOrderDetailCustomerRequest = order.TotalOrderDetailCustomerRequest;
                        cardOrderItem.TableName = order.TableName;
                        cardOrderItem.TableId = order.TableId;
                        cardOrderItem.MegerTableObject = order.TableMergeListName;
                        cardOrderItem.CashAmount = order.TotalAmount;
                        cardOrderItem.UsingTime = order.UsingTimeMinutesString;
                        cardOrderItem.IsOnline = order.IsOnline == Constants.STATUS ? true : false;
                        cardOrderItem.Amount = order.Amount;
                        cardOrderItem.Vat = (float)order.Vat;
                        cardOrderItem.VatAmount = order.VatAmount;
                        cardOrderItem.DiscountPercent = (float)order.DiscountPercent;
                        cardOrderItem.DiscountAmount = order.DiscountAmount;
                        cardOrderItem.PointOrder = order.MembershipTotalPointUsed;
                        cardOrderItem.ShippingFee = order.ShippingFee;
                        cardOrderItem.IsTakeAway = order.IsTakeAway == Constants.STATUS ? true : false;
                        cardOrderItem.BookingDepositAmount = order.BookingDepositAmount;
                        cardOrderItem.BookingInforId = order.BookingInforId;
                        cardOrderItem.IsReturnDeposit = order.IsReturnDeposit == 1 ? true : false;
                        cardOrderItem.BookingDepositPaymentMethod = order.BookingDepositPaymentMethod;
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (order.OrderStatus == (int)OrderStatusEnum.OPENING)
                            {
                                if (cardOrderItem.CashAmount != 0)
                                {
                                    cardOrderItem.BtnDoneIsEnabled = false;
                                }
                                else
                                {
                                    cardOrderItem.BtnDoneIsEnabled = true;
                                }
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                            {
                                if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                                {
                                    cardOrderItem.BtnDoneIsEnabled = true;
                                    cardOrderItem.BtnPrintIsEnabled = true;
                                    cardOrderItem.BtnSaveIsEnabled = false;
                                }
                                else
                                {
                                    cardOrderItem.BtnDoneIsEnabled = true;
                                    cardOrderItem.BtnPrintIsEnabled = true;
                                    cardOrderItem.BtnSaveIsEnabled = true;
                                }
                            }
                        }
                        else
                        {
                            //cardOrderItem.BtnDoneIsEnabled = true;
                            //cardOrderItem.BtnPrintIsEnabled = true;
                            //cardOrderItem.BtnSaveIsEnabled = true;
                            if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }
                        }
                        if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        {
                            int index = Views.IndexOf(orderItem);
                            Views.Remove(orderItem);
                            Views.Insert(0, cardOrderItem);
                            Views.Select(std => std.OrderId).Distinct();
                        }
                        else if (order.OrderStatus == (int)OrderStatusEnum.OPENING)
                        {
                            int index = Views.IndexOf(orderItem);
                            Views.RemoveAt(index);
                            Views.Add(cardOrderItem);
                            Views.Select(std => std.OrderId).Distinct();
                        }
                        else if (order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                        {

                            int index = Views.IndexOf(orderItem);
                            Views.Remove(orderItem);
                            Views.Add(cardOrderItem);
                            List<CardOrderItem> cards = Views.OrderByDescending(x => x.OrderStatus).ToList();
                            if (Views != null)
                            {
                                Views.Clear();
                            }
                            else
                            {
                                Views = new ObservableCollection<CardOrderItem>();
                            }
                            cards.ForEach(Views.Add);
                            Views.Select(std => std.OrderId).Distinct();
                        }
                    }
                }
                else
                {
                    cardOrderItem.OrderId = order.Id;
                    cardOrderItem.OrderStatus = order.OrderStatus;
                    cardOrderItem.CustomerSlot = order.CustomerSlotNumber;
                    cardOrderItem.TotalOrderDetailCustomerRequest = order.TotalOrderDetailCustomerRequest;
                    cardOrderItem.TableName = order.TableName;
                    cardOrderItem.TableId = order.TableId;
                    cardOrderItem.MegerTableObject = order.TableMergeListName;
                    cardOrderItem.CashAmount = order.TotalAmount;
                    cardOrderItem.UsingTime = order.UsingTimeMinutesString;
                    cardOrderItem.IsOnline = order.IsOnline == Constants.STATUS ? true : false;
                    cardOrderItem.Amount = order.Amount;
                    cardOrderItem.Vat = (float)order.Vat;
                    cardOrderItem.VatAmount = order.VatAmount;
                    cardOrderItem.DiscountPercent = (float)order.DiscountPercent;
                    cardOrderItem.DiscountAmount = order.DiscountAmount;
                    cardOrderItem.PointOrder = order.MembershipTotalPointUsed;
                    cardOrderItem.ShippingFee = order.ShippingFee;
                    cardOrderItem.IsTakeAway = order.IsTakeAway == Constants.STATUS ? true : false;
                    cardOrderItem.BookingDepositAmount = order.BookingDepositAmount;
                    cardOrderItem.BookingInforId = order.BookingInforId;
                    cardOrderItem.IsReturnDeposit = order.IsReturnDeposit == 1 ? true : false;
                    cardOrderItem.BookingDepositPaymentMethod = order.BookingDepositPaymentMethod;
                    if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                    {
                        if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        {
                            cardOrderItem.BtnDoneIsEnabled = true;
                            cardOrderItem.BtnPrintIsEnabled = true;
                            cardOrderItem.BtnSaveIsEnabled = false;
                        }
                        else if (order.OrderStatus == (int)OrderStatusEnum.OPENING)
                        {
                            if (cardOrderItem.CashAmount != 0)
                            {
                                cardOrderItem.BtnDoneIsEnabled = false;
                            }
                            else
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                            }
                            cardOrderItem.BtnPrintIsEnabled = false;
                            cardOrderItem.BtnSaveIsEnabled = false;
                        }
                        else if (order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                        {
                            if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }
                        }
                    }
                    else
                    {
                        //cardOrderItem.BtnDoneIsEnabled = true;
                        //cardOrderItem.BtnPrintIsEnabled = true;
                        //cardOrderItem.BtnSaveIsEnabled = true;
                        if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        {
                            cardOrderItem.BtnDoneIsEnabled = true;
                            cardOrderItem.BtnPrintIsEnabled = true;
                            cardOrderItem.BtnSaveIsEnabled = false;
                        }
                        else
                        {
                            cardOrderItem.BtnDoneIsEnabled = true;
                            cardOrderItem.BtnPrintIsEnabled = true;
                            cardOrderItem.BtnSaveIsEnabled = true;
                        }
                    }
                    if (order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                    {
                        if (orderItem != null)
                        {
                            Views.Remove(orderItem);
                        }
                        Views.Insert(0, cardOrderItem);
                        Views.Select(std => std.OrderId).Distinct();
                    }
                    else if (order.OrderStatus == (int)OrderStatusEnum.OPENING)
                    {
                        Views.Add(cardOrderItem);
                        Views.Select(std => std.OrderId).Distinct();
                    }
                    else if (order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                    {

                        int index = Views.IndexOf(orderItem);
                        Views.Remove(orderItem);
                        Views.Add(cardOrderItem);
                        List<CardOrderItem> cards = Views.OrderByDescending(x => x.OrderStatus).ToList();
                        if (Views != null)
                        {
                            Views.Clear();
                        }
                        else
                        {
                            Views = new ObservableCollection<CardOrderItem>();
                        }
                        cards.ForEach(Views.Add);
                        Views.Select(std => std.OrderId).Distinct();
                    }
                }
            }
            else if (order.OrderStatus == (int)OrderStatusEnum.PENDING)
            {
                if (OrderOnlineList != null)
                {
                    Order orderItem = OrderOnlineList.Where(x => x.Id == order.Id).FirstOrDefault();
                    if (orderItem != null)
                    {
                        int index = OrderOnlineList.IndexOf(orderItem);
                        OrderOnlineList.Remove(orderItem);
                        OrderOnlineList.Insert(index, order);
                        OrderOnlineItem = OrderOnlineList.Where(x => x.Id == currentOrderOnlineId).FirstOrDefault();
                        currentOrderOnlineId = OrderOnlineItem != null ? OrderOnlineItem.Id : OrderOnlineList[0].Id;
                        GetOrderDetailOnline(currentOrderOnlineId);
                        CustomerVisibility = Visibility.Visible;
                    }
                    else
                    {
                        OrderOnlineList.Add(order);
                        OrderOnlineItem = OrderOnlineList.Where(x => x.Id == order.Id).FirstOrDefault();
                        currentOrderOnlineId = OrderOnlineItem != null ? OrderOnlineItem.Id : OrderOnlineList[0].Id;
                        GetOrderDetailOnline(currentOrderOnlineId);
                        CustomerVisibility = Visibility.Visible;
                    }
                }

                else
                {
                    OrderOnlineList = new ObservableCollection<Order>();
                    OrderOnlineList.Add(order);
                    OrderOnlineItem = OrderOnlineList.Where(x => x.Id == order.Id).FirstOrDefault();
                    currentOrderOnlineId = OrderOnlineItem != null ? OrderOnlineItem.Id : OrderOnlineList[0].Id;
                    GetOrderDetailOnline(currentOrderOnlineId);
                    CustomerVisibility = Visibility.Visible;
                }
            }
            else
            {
                CardOrderItem orderItem = Views.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (orderItem != null)
                {
                    Views.Remove(orderItem);
                    Views.Select(std => std.OrderId).Distinct();
                }
            }
        }
        public void RealTime()
        {
            if (Constants.IS_NETWORK_ONLINE)
            {
                RealTimeOnline();
                RealTimePrintSeaFoodOnline();
                if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM)
                {
                    RealTimePrintCook();
                }
            }
            else
            {
                RealTimeOffline();
            }
        }
        public void RealTimeOnline  ()
        {
            //string LinkRoomOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
            //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkRoomOrder);
            string LinkSocket = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
            WriteLog.logs(LinkSocket);
            
            SocketIOManager.Instance.socket.On(LinkSocket, (data) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (data == null)
                    {
                        Console.WriteLine("data null");
                        return;
                    }
                    //string JsonString = data.ToString();
                    //WriteLog.logs(JsonString);
                    //OrderRealTime orderRealTime = data.GetValue<Models.OrderRealTime>(0);
                    //Console.WriteLine(data.GetValue<Models.OrderRealTime>().ToString());
                    //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                    //OrderRealTime orderRealTime = jsonResponse.ToObject<Models.OrderRealTime>();
                    //WriteLog.logs(data.SocketIO.ToString()); 
                    string str = data.ToString();
                    WriteLog.logs("REALTIME LOGS : " + str); 
                    //JObject json = JObject.Parse(str);
                    SocketIOManager.Instance.socket.JsonSerializer = new NewtonsoftJsonSerializer();
                    OrderRealTime orderRealTime = data.GetValue<Models.OrderRealTime>(0);    
                    
                    WriteLog.logs(data.ToString());
                    try
                    {
                        if (orderRealTime != null)
                        {
                            if (orderRealTime.ObjectData != null)
                            {
                                dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderRealTime.ObjectData);
                                Order o = jsonResponseObject.ToObject<Order>();
                                if (o != null)
                                {
                                    o.TotalOrderDetailCustomerRequest = orderRealTime.TotalOrderDetailCustomerRequest;
                                    AddAndUpdateOrder(o);
                                }
                            }
                            if (orderRealTime.IsMoveTable)
                            {
                                TotalAmountTmp(); 
                            }
                            if (orderRealTime.IsChangeAmount)
                            {
                                TotalAmountTmp(); 

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.Write(ex.StackTrace);
                    }
                });
            });
        }
        public async void RealTimeOffline()
        {
            try
            {
                //if (Constants.CLIENT_WEB_SOCKET == null || Constants.CLIENT_WEB_SOCKET.State == WebSocketState.Closed || Constants.CLIENT_WEB_SOCKET.State == WebSocketState.Aborted)
                if (Constants.CLIENT_WEB_SOCKET == null || Constants.CLIENT_WEB_SOCKET.State != WebSocketState.Open)
                {
                    TimeSpan timeSpan = new TimeSpan(12, 0, 0);
                    var timeOut = new CancellationTokenSource(timeSpan).Token;
                    Constants.CLIENT_WEB_SOCKET = new ClientWebSocket();
                    await Constants.CLIENT_WEB_SOCKET.ConnectAsync(new Uri(Constants.WEBSOCKET_DOMAIN), timeOut);
                    await Task.WhenAll(Receive(Constants.CLIENT_WEB_SOCKET));
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

        }
        private async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer;
            while (webSocket.State == WebSocketState.Open)
            {
                buffer = new byte[2048];
                TimeSpan timeSpan = new TimeSpan(12, 0, 0);
                var timeOut = new CancellationTokenSource(timeSpan).Token;
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), timeOut);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    try
                    {
                        // WriteLog.logs(buffer.Length);
                        string data = Encoding.UTF8.GetString(buffer, 0, buffer.Length).TrimEnd('\0');
                        if (data == null) return;
                        WriteLog.logs(data);
                        string LinkSocketOrders = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                        string LinkSocketPrint = string.Format("restaurants/{0}/branches/{1}/print", currentUser.RestaurantId, currentUser.BranchId);
                        string LinkSeaFood = string.Format("restaurants/{0}/branches/{1}/order_detail_sea_food_only", currentUser.RestaurantId, currentUser.BranchId);
                        string LinkSocketNotifications = string.Format("restaurants/{0}/branches/{1}/notify_winapp_cashier", currentUser.RestaurantId, currentUser.BranchId);
                        if (data.Contains(LinkSocketOrders))
                        {
                            BaseRealtimeModelOrder baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModelOrder>(data);
                            if (baseRealtimeModelOrder != null && baseRealtimeModelOrder.Key.Equals(LinkSocketOrders))
                            {
                                OrderRealTime orderRealTime = baseRealtimeModelOrder.Value;
                                if (orderRealTime != null)
                                {
                                    if (orderRealTime.ObjectData != null)
                                    {
                                        dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderRealTime.ObjectData);
                                        Order o = jsonResponseObject.ToObject<Order>();
                                        if (o != null)
                                        {

                                            AddAndUpdateOrder(o);
                                        }
                                    }
                                    if (orderRealTime.IsMoveTable || orderRealTime.IsChangeAmount)
                                    {
                                        TotalAmountTmp();
                                    }
                                }
                            }
                        }
                        else if (data.Contains(LinkSeaFood))
                        {
                            BaseRealtimeModelPrintOrderDetails baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModelPrintOrderDetails>(data);
                            if (baseRealtimeModelOrder != null)
                            {
                                Models.OrderDetail orderRealTime = baseRealtimeModelOrder.Value;
                                if (orderRealTime != null)
                                {
                                    PrintSeaFood(orderRealTime);
                                }
                            }
                        }
                        else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM && data.Contains(LinkSocketPrint))
                        {
                            BaseRealtimeModelOrderDetails baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModelOrderDetails>(data);
                            if (baseRealtimeModelOrder != null)
                            {
                                Order order = baseRealtimeModelOrder.Value;
                                if (order != null)
                                { 
                                    PrintCook(order.Id);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.Message);
                    }
                }
            }
        }
        public void RealTimePrintCook()
        {
            //string LinkRoomPrint = string.Format("restaurants/{0}/branches/{1}/print", currentUser.RestaurantId, currentUser.BranchId);
            //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkRoomPrint);

            string LinkSocket = string.Format("restaurants/{0}/branches/{1}/print", currentUser.RestaurantId, currentUser.BranchId);
            //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocket);
            SocketIOManager.Instance.socket.On(LinkSocket, (data) =>
            {

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    if (data == null) return;
                    string JsonString = data.ToString();
                    WriteLog.logs(JsonString);
                    //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                    //Order order = jsonResponse.ToObject<Models.Order>();
                    Order order = data.GetValue<Models.Order>(0);
                    try
                    {
                        if (order != null)
                        {
                            PrintCook(order.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.StackTrace);
                    }
                }), DispatcherPriority.ContextIdle);
            });
        }
        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    DialogHostOpen = false;
                    if (CurrentButton == (long)HomeButtonEnum.OPENING)
                    {
                        GetOrders();
                        TotalAmountTmp();
                    }
                    else if (CurrentButton == (long)HomeButtonEnum.DONE)
                    {
                        GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                    }
                    else if (CurrentButton == (long)HomeButtonEnum.ONLINE)                    {
                        GetOrderListOnline();
                    }
                    else if (CurrentButton == (long)HomeButtonEnum.BOOKING)
                    {

                    }
                });
            }
            else
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    DialogHostOpen = true;
                });
            }
        }
        public void ChangeNetWork()
        {
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }
        //public void TotalAmountTmpLinq(long orderId, decimal tmpOrderId)
        //{
        //    try
        //    {
        //        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
        //        {
        //            RevenueTmpVisibility = Visibility.Visible;

        //            CardOrderItem item = Views.Where(x => x.OrderId == orderId).FirstOrDefault();
        //            decimal tmpRevenue = Convert.ToDecimal(item.Amount);
        //            tmpRevenue = Convert.ToDecimal(RevenueTmp) - tmpRevenue + tmpOrderId;
        //            RevenueTmp = Utils.Utils.FormatMoney(tmpRevenue); 
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog.logs(ex.Message); 
        //    }
        //}
        public async void TotalAmountTmp()
        {
            try
            {
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    RevenueTmpVisibility = Visibility.Visible;
                    ordersClient = new OrdersClient(this, this, this);
                    ManageFloatResponse tmp = await Task.Run(() => ordersClient.TmpRevenue(currentUser.BranchId));
                    if (tmp != null && tmp.Status == (int)ResponseEnum.OK)
                    {
                        RevenueTmp = Utils.Utils.FormatMoney(tmp.Data);
                    }

                }
                else
                {
                    RevenueTmpVisibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.Message);
            }
        }
        public async void GetOrders()
        {
            if (Views != null)
            {
                Views.Clear();
            }
            else
            {
                Views = new ObservableCollection<CardOrderItem>();
            }
            ordersClient = new OrdersClient(this, this, this);
            DialogHostOpen = true;
            OrderResponse response = await Task.Run(() => ordersClient.GetListOrder(Constants.STATUS_OPENING_WAITING_PAYMENT_AND_WAITING_COMPLETED, 1, int.MaxValue, currentUser.BranchId));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.List != null)
            {
                foreach (Order o in response.Data.List)
                {
                    CardOrderItem card = Views.Where(x => x.OrderId == o.Id).FirstOrDefault();
                    if (card == null)
                    {
                        cardOrderItem = new CardOrderItem();
                        cardOrderItem.OrderId = o.Id;
                        cardOrderItem.OrderStatus = o.OrderStatus;
                        cardOrderItem.CustomerSlot = o.CustomerSlotNumber;
                        cardOrderItem.TableName = o.TableName;
                        cardOrderItem.TableId = o.TableId;
                        cardOrderItem.TotalOrderDetailCustomerRequest = o.TotalOrderDetailCustomerRequest;
                        cardOrderItem.MegerTableObject = o.TableMergeListName;
                        cardOrderItem.CashAmount = o.TotalAmount;
                        cardOrderItem.UsingTime = o.UsingTimeMinutesString;
                        cardOrderItem.UsingTimeMinutes = o.UsingTimeMinutes;
                        cardOrderItem.IsTakeAway = o.IsTakeAway == Constants.STATUS ? true : false;
                        cardOrderItem.IsOnline = o.IsOnline == Constants.STATUS ? true : false;
                        cardOrderItem.Amount = o.Amount;
                        cardOrderItem.Vat = (float)o.Vat;
                        cardOrderItem.VatAmount = o.VatAmount;
                        cardOrderItem.DiscountPercent = (float)o.DiscountPercent;
                        cardOrderItem.DiscountAmount = o.DiscountAmount;
                        cardOrderItem.PointOrder = o.MembershipTotalPointUsed;
                        cardOrderItem.ShippingFee = o.ShippingFee;
                        cardOrderItem.IsReturnDeposit = o.IsReturnDeposit == Constants.STATUS ? true : false;
                        cardOrderItem.BookingInforId = o.BookingInforId;
                        cardOrderItem.BookingDepositAmount = o.BookingDepositAmount;
                        cardOrderItem.BookingDepositPaymentMethod = o.BookingDepositPaymentMethod;
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            if (o.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || o.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (o.OrderStatus == (int)OrderStatusEnum.OPENING)
                            {
                                if (cardOrderItem.CashAmount != 0)
                                {
                                    cardOrderItem.BtnDoneIsEnabled = false;
                                }
                                else
                                {
                                    cardOrderItem.BtnDoneIsEnabled = true;
                                }
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else if (o.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }
                        }
                        else
                        {
                            if (o.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || o.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = false;
                            }
                            else
                            {
                                cardOrderItem.BtnDoneIsEnabled = true;
                                cardOrderItem.BtnPrintIsEnabled = true;
                                cardOrderItem.BtnSaveIsEnabled = true;
                            }

                        }
                        Views.Add(cardOrderItem);
                    }
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public async void PrintCook(long orderId)
        {
            OrderDetailPrintResponse orderDetailPrintResponse = await Task.Run(() => ordersClient.GetOrderDetailPrint(orderId));
            if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
            {
                if (CurrentKitchen != null && CurrentKitchen.Count > 0)
                {
                    List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                    foreach (Kitchen k in CurrentKitchen)
                    {
                        List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL && x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList()); // Thay đổi tăng 
                        if (result != null && result.Count > 0)
                        {
                            pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName, string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)k.PrinterPaperSize);
                        }
                        List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity < x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL && x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK))).ToList(); // Thay đổi giảm 
                        if (resultUpdate != null && resultUpdate.Count > 0)
                        {
                            foreach (OrderDetailPrint od in resultUpdate)
                            { 
                                pt.PrintFoodMove(od, k.PrinterName, od.EmployeeName, od.TableName, (int)k.PrinterPaperSize);
                            }
                        }
                        List<OrderDetailPrint> resultCancel = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && (x.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK))).ToList();
                        if (resultCancel != null && resultCancel.Count > 0)
                        {
                            foreach (OrderDetailPrint od in resultCancel)
                            {
                                pt.PrintFoodCancel(od, k.PrinterName, (int)k.PrinterPaperSize);
                            }
                        }

                    }
                }
                //await Task.Run(() => ordersClient.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.Select(x => x.Id).ToList()),orderId));
                 ordersClient.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.Select(x => x.Id).ToList()), orderId);
                //await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(FoodOrderList.Select(x => x.Id).ToList(), orderId)));
                //await Task.Run(() => ordersClient.PostOrderDetailPrint(orderId)); // HA SUA 
            }
        }
        public async void GetOrderListDone(string dateTime, int Page)
        {
            if (ItemsOrderDone != null)
            {
                ItemsOrderDone.Clear();
            }
            else
            {
                ItemsOrderDone = new ObservableCollection<Order>();
            }
            DialogHostOpen = true;
            OrdersClient ordersClient = new OrdersClient(this, this, this);
            int ReportType = Constants.TYPE_ORDER_DONE_HISTORY;
            string ToDateString = dateTime;
            int FromHour = -1;
            int ToHour = -1;
            string OrderStatus = "2,5";
            //int Limit = 1000;
            int Limit = 50;
            OrderResponse order = await Task.Run(() => ordersClient.GetListOrderHistory(ReportType, dateTime, FromHour, ToDateString, ToHour, OrderStatus, Page, Limit, currentUser.BranchId,currentUser.RestaurantBrandId));
            if (order != null && order.Status == (int)ResponseEnum.OK)
            {
                order.Data.List.ForEach(ItemsOrderDone.Add);
                if (order.Data.TotalRecord > 0)
                {
                    if (order.Data.TotalRecord % order.Data.Limit != 0)
                    {
                        TotalPage = (int)(order.Data.TotalRecord / order.Data.Limit) + 1;
                        UpdateEnableState(); // Dat
                    }
                    else
                    {
                        TotalPage = (int)(order.Data.TotalRecord / order.Data.Limit);
                        UpdateEnableState(); // Dat
                    }
                    //CurrentPage = 1;
                    PageContent = string.Format("{0}/{1}", CurrentPage, TotalPage);
                }
                else
                {
                    PageContent = string.Format("{0}/{1}", 1, 1);
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public async void GetOrderListOnline()
        {
            if (OrderOnlineList != null)
            {
                OrderOnlineList.Clear();
            }
            else
            {
                OrderOnlineList = new ObservableCollection<Order>();
            }
            DialogHostOpen = true;
            OrdersClient ordersClient = new OrdersClient(this, this, this);
            OrderResponse order = await Task.Run(() => ordersClient.GetListOrder(string.Format("{0}", (int)OrderStatusEnum.PENDING), 1, int.MaxValue, currentUser.BranchId));
            if (order != null && order.Status == (int)ResponseEnum.OK)
            {
                order.Data.List.ForEach(OrderOnlineList.Add);
                if (OrderOnlineList != null && OrderOnlineList.Count > 0)
                {
                    OrderOnlineItem = OrderOnlineList[0];
                    GetOrderDetailOnline(OrderOnlineItem.Id);
                    CustomerVisibility = Visibility.Visible;
                }
                else
                {
                    CustomerVisibility = Visibility.Collapsed;
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
            TotalOrderOnline = string.Format(MessageValue.MESSAGE_FROM_TOTAL_ORDER_ONLINE, OrderOnlineList.Count);
        }
        //đặt trước
        public async void GetOrderListBooking(string date, int Page)
        {
            DialogHostOpen = true;
            if (OrderBookingList != null)
            {
                OrderBookingList.Clear();
            }
            else
            {
                OrderBookingList = new ObservableCollection<Models.Booking>();
            }
            List<long> statuses = new List<long>() { (long)BookingStatusEnum.ARRANGED_TABLE, (long)BookingStatusEnum.CANCEL, (long)BookingStatusEnum.COMPLETED,
                                                                                               (long)BookingStatusEnum.CONFIMED, (long)BookingStatusEnum.EXPIRED, (long)BookingStatusEnum.SET_UP,
                                                   (long)BookingStatusEnum.UNKONW, (long)BookingStatusEnum.WAITING_COMPLETE, (long)BookingStatusEnum.WAITING_CONFIRM};
            BookingClient bookingClient = new BookingClient(this, this, this);
            BookingListResponse response = await Task.Run(() => bookingClient.GetListBooking(currentUser.RestaurantBrandId, currentUser.BranchId, date, date, Utils.Utils.convertFormListLong(statuses), Constants.NOT_STATUS, Constants.NOT_STATUS, Page, (long)LimitEnum.DEFAULT));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.List.ForEach(OrderBookingList.Add);
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
                DialogHostOpen = false;
            }
            else
            {
                DialogHostOpen = false;
            }
            //TotalOrderOnline = string.Format(MessageValue.MESSAGE_FROM_TOTAL_ORDER_ONLINE, OrderOnlineList.Count);
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RefreshVisibility = Visibility.Visible;
            dispatcherTimer.Stop();
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
        FrameworkElement GetWindowParent(Window p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }

            return parent;
        }
        public async void GetOrderDetailOnline(long orderId)
        {

            if (orderId > 0)
            {
                if (OrderDetailOnlineList != null)
                {
                    OrderDetailOnlineList.Clear();
                }
                else
                {
                    OrderDetailOnlineList = new ObservableCollection<BillResponse>();
                }
                OrdersClient client = new OrdersClient(this, this, this);
                OrderItemResponse response = await Task.Run(() => client.GetOrderById(orderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.NOT_STATUS));
                if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
                {
                    CustomerAddress = response.Data.ShippingAddress;
                    CustomerName = response.Data.ShippingReceiverName;
                    CustomerPhone = response.Data.ShippingPhone;
                    Note = response.Data.Note;
                    ShipperAmount = Utils.Utils.FormatMoney(response.Data.ShippingFee);
                    Amount = Utils.Utils.FormatMoney(response.Data.Amount);
                    if (response.Data.Vat > 0)
                    {
                        IsVat = true;
                        VAT = string.Format("{0}%", response.Data.Vat);
                        VATAmount = Utils.Utils.FormatMoney(response.Data.VatAmount);
                    }
                    else
                    {
                        IsVat = false;
                        VAT = string.Format("{0}%", currentSetting.Vat);
                        VATAmount = Utils.Utils.FormatMoney(0);
                    }
                    TotalAmount = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                    PaidAmount = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                    response.Data.Foods.ForEach(OrderDetailOnlineList.Add);
                }
            }
        }
        public async void PrintSeaFood(Models.OrderDetail orderDetail)
        {
            DeviceConfigWrapper device = deviceClient.RealConfigs();
            if (device != null)
            {
                if ((device.IsFishtank) && !string.IsNullOrEmpty(device.FishtankPrinter))
                {
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    SingleOrderDetailResponse orderDetailResponse = await Task.Run(() => client.GetOrderDetail(orderDetail.Id));
                    if (orderDetailResponse != null && orderDetailResponse.Data != null)
                    {

                        if (orderDetailResponse.Data.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                        {
                            if (orderDetail.OldQuantity == 0)
                            {

                                PrintText print = new PrintText();
                                print.PrintSeaFood(device.FishtankPrinter, orderDetailResponse, device.FishtankSize);
                            }
                            else
                            {
                                if (orderDetail.Quantity > 0)
                                {
                                    orderDetailResponse.Data.Quantity = orderDetail.Quantity;
                                    PrintText print = new PrintText();
                                    print.PrintSeaFood(device.FishtankPrinter, orderDetailResponse, device.FishtankSize);
                                }
                                else if (orderDetail.Quantity < 0)
                                {
                                    PrintText print = new PrintText();
                                    print.PrintSeaFoodMove(orderDetailResponse, device.FishtankPrinter, orderDetail.OldQuantity, orderDetail.Quantity, device.FishtankSize);
                                }
                            }
                        }
                        else if (orderDetailResponse.Data.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL)
                        {
                            PrintText print = new PrintText();
                            print.PrintFoodCancel(orderDetailResponse, device.FishtankPrinter, device.FishtankSize);
                        }

                    }

                }
            }

        }
        public void RealTimePrintSeaFoodOnline()
        {
            //string LinkRoomWaiting = string.Format("restaurants/{0}/branches/{1}/order_detail_sea_food_only", currentUser.RestaurantId, currentUser.BranchId);
            //SocketIOManager.Instance.socket.EmitAsync("join_room", LinkRoomWaiting);

            string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/order_detail_sea_food_only", currentUser.RestaurantId, currentUser.BranchId);
            SocketIOManager.Instance.socket.On(LinkSocketWaiting, (data) =>
            {
                if (data == null) return;
                //string JsonString = data.ToString();
                //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
                //Models.OrderDetail orderDetail = jsonResponse.ToObject<Models.OrderDetail>();

                OrderDetail orderDetail = data.GetValue<Models.OrderDetail>(0);
                Application.Current.Dispatcher.Invoke((Action)delegate
               {
                   PrintSeaFood(orderDetail);
               });
            });
        }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        public void GetAllBranches()
        {
            if (BranchList == null)
            {
                BranchList = new ObservableCollection<Branch>();
            }
            else if (BranchList.Count > 0)
            {
                BranchList.Clear();
            }
            BranchClient client = new BranchClient(this, this, this);
            BranchesResponse response = client.GetAllBranchesResponse();
            if (response != null && response.Status == (int)ResponseEnum.OK && response.data != null)
            {
                Branch item = new Branch();
                item.Name = MessageValue.MESSAGE_ALL_BRANCH;
                item.Id = Constants.ALL;
                BranchList.Add(item);
                response.data.ForEach(BranchList.Add);
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_BRANCH, BranchList.ToList(), DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
            }
        }
        public void GetRestaurantKitchen(int brandId, long branchId)
        {
            KitchenClient client = new KitchenClient(this, this, this);
            KitchenResponse response = client.GetKitchenResponses(brandId,(int)branchId, Constants.STATUS);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                CurrentKitchen = response.Data;
                Properties.Settings.Default.RestaurantKitchenPlacePrint = Utils.Utils.AsJsonList<Kitchen>(response.Data);
                Properties.Settings.Default.Save();
            }
        }
        public HomeViewModel()
        {
            try
            {
                // gọi realtime
                currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                if (currentSetting.BranchType == (long)BranchTypeEnum.SMALL && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    RealTime();
                    GetOrders();
                    TotalAmountTmp();
                    GetRestaurantKitchen(currentUser.RestaurantBrandId, currentUser.BranchId);
                }
                else
                {
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                    {
                        RealTime();
                        GetOrders();
                        TotalAmountTmp();
                        if (currentSetting.BranchType != (long)BranchTypeEnum.LARGE && currentSetting.BranchType < (long)BranchTypeEnum.LARGE)
                        {
                            GetRestaurantKitchen(currentUser.RestaurantBrandId, currentUser.BranchId);
                        }
                    }
                }

                // load lại dữ liệu khi vào màn hình
                LoadedUCCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    RealTime();
                    CurrentKitchen = Utils.Utils.AsObjectList<Kitchen>(Properties.Settings.Default.RestaurantKitchenPlacePrint);
                    TextBoxSearch = Visibility.Visible;
                    DatetimeInput = DateTime.Now;
                    currentDatetime = DatetimeInput;
                    DatetimeDisplayDateEnd = DateTime.Now;
                    DateTimeVisibility = Visibility.Collapsed;
                    RefreshVisibility = Visibility.Visible;
                    _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                    _ContentOrder.Content = new CardOrderUserControl(); ;
                    PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                    OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                    HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                    BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                    //   isFrist = true;
                    CurrentButton = (long)HomeButtonEnum.OPENING;
                    ChangeNetWork();
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                });
                RefreshCommand = new RelayCommand<Button>((t) => { return true; }, t =>
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        CurrentButton = (long)HomeButtonEnum.OPENING;
                        DateTimeVisibility = Visibility.Collapsed;

                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));

                        OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));

                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                        BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        _ContentOrder = t.FindName("ContentOrder") as ContentControl;
                        CardOrderUserControl cardOrderUserControl = new CardOrderUserControl();
                        if (currentSetting.BranchType == (long)BranchTypeEnum.SMALL && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                        {
                            GetOrders();
                            TotalAmountTmp();
                        }
                        else
                        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                        {
                            GetOrders();
                            TotalAmountTmp();
                        }
                        _ContentOrder.Content = cardOrderUserControl;

                        if (!Constants.IS_NETWORK_ONLINE)
                        {
                            RealTimeOffline();
                        }
                        else
                        {
                            RefreshVisibility = Visibility.Collapsed;
                            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                            dispatcherTimer.Interval = new TimeSpan(0, 3, 0);
                            dispatcherTimer.Start();
                        }

                    });

                });
                SelectionDateChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    CurrentPage = 1; // Dat
                    if (currentDatetime != DatetimeInput || !isChange)
                    {
                        if (CurrentButton == (long)HomeButtonEnum.DONE)
                        {
                            currentDatetime = DatetimeInput;
                            GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            isChange = true;
                        }
                        else if (CurrentButton == (long)HomeButtonEnum.BOOKING)
                        {
                            currentDatetime = DatetimeInput;
                            GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            isChange = true;
                        }
                    }
                  
                });
                BtnDone = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    //CurrentButton = (long)HomeButtonEnum.DONE;                   
                    if (CurrentButton != (long)HomeButtonEnum.DONE)
                    {
                        TextBoxSearch = Visibility.Visible;
                        DateTimeVisibility = Visibility.Visible;
                        //DateTimeVisibility = Visibility.Visible;
                        RefreshVisibility = Visibility.Collapsed;
                        _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                        OrderDoneUserControl order = new OrderDoneUserControl();
                        _ContentOrder.Content = order;
                        isChange = false;
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));

                        OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));

                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done-active.png")));
                        BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        DatetimeInput = DateTime.Now;
                        CurrentPage = 1;
                        GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                        PageDoubleLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            CurrentPage = 1;
                            GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);

                        });

                        PageLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            if (CurrentPage > 1)
                            {
                                CurrentPage = CurrentPage - 1;
                                GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            }

                        });

                        PageRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            if (CurrentPage < TotalPage)
                            {
                                CurrentPage = CurrentPage + 1;
                                GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            }

                        });
                        PageDoubleRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            CurrentPage = TotalPage;
                            GetOrderListDone(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);

                        });
                        //GetOrderListDone(Utils.Utils.getDateFormatVN(DatetimeInput));
                    }
                    CurrentButton = (long)HomeButtonEnum.DONE;
                });
                BtnOnline = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    //CurrentButton = (long)HomeButtonEnum.ONLINE;

                    if (CurrentButton != (long)HomeButtonEnum.ONLINE)
                    {
                        DateTimeVisibility = Visibility.Collapsed;
                        RefreshVisibility = Visibility.Visible;
                        _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                        OrderOnlineUserControl order = new OrderOnlineUserControl();
                        _ContentOrder.Content = order;
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));

                        OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-online.png")));

                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                        BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        GetOrderListOnline();
                    }
                    CurrentButton = (long)HomeButtonEnum.ONLINE;
                    NotificationMessage.Infomation("Chức năng này đang được phát triển thêm");

                });
                ChangeOrderOnlineCommand = new RelayCommand<Order>((p) => { return true; }, p =>
                {
                    if (p != null)
                    {
                        if (p.Id != currentOrderOnlineId)
                        {
                            GetOrderDetailOnline(p.Id);
                            OrderOnlineItem = p;
                            currentOrderOnlineId = p.Id;
                        }
                    }
                });
                AddCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (OrderOnlineItem != null)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CONFIRM_ORDER_ONLINE_CONTENT;
                        string Title = MessageValue.MESSAGE_CONFIRM_ORDER_ONLINE;
                        string YesContent = MessageValue.MESSAGE_CO_CONTENT;
                        string NoContent = MessageValue.MESSAGE_KHONG_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            BaseResponse baseResponse = ordersClient.ConfirmOrders(OrderOnlineItem.Id);
                            if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                            {
                                OrderOnlineList.Remove(OrderOnlineItem);
                                if (OrderOnlineList != null && OrderOnlineList.Count > 0)
                                {
                                    OrderOnlineItem = OrderOnlineList[0];
                                    GetOrderDetailOnline(OrderOnlineItem.Id);
                                    CustomerVisibility = Visibility.Visible;
                                }
                                else
                                {
                                    CustomerVisibility = Visibility.Collapsed;
                                }
                            }

                        }

                    }

                });
                CloseCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    if (OrderOnlineItem != null)
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CANCEL_ORDER_ONLINE_CONTENT;
                        string Title = MessageValue.MESSAGE_CANCEL_ORDER_ONLINE;
                        string YesContent = MessageValue.MESSAGE_CO_CONTENT;
                        string NoContent = MessageValue.MESSAGE_KHONG_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            BaseResponse baseResponse = ordersClient.CancelOrders(OrderOnlineItem.Id);
                            if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                            {
                                OrderOnlineList.Remove(OrderOnlineItem);
                                if (OrderOnlineList != null && OrderOnlineList.Count > 0)
                                {
                                    OrderOnlineItem = OrderOnlineList[0];
                                    GetOrderDetailOnline(OrderOnlineItem.Id);
                                    CustomerVisibility = Visibility.Visible;
                                }
                                else
                                {
                                    CustomerVisibility = Visibility.Collapsed;
                                }
                            }

                        }

                    }

                });
                CheckedVATCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                Application.Current.Dispatcher.Invoke((Action)async delegate
                {
                    if (OrderOnlineItem != null)
                    {
                        currentOrderOnlineId = OrderOnlineItem.Id;
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = await Task.Run(() => ordersClient.ApplyVAT(OrderOnlineItem.Id, IsVat ? Constants.STATUS : Constants.NOT_STATUS));
                    }
                });
            });
                BtnOpening = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    TextBoxSearch = Visibility.Visible;
                    DateTimeVisibility = Visibility.Collapsed;
                    //CurrentButton = (long)HomeButtonEnum.OPENING;

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) && CurrentButton != (long)HomeButtonEnum.OPENING)
                    {
                        DateTimeVisibility = Visibility.Collapsed;
                        RefreshVisibility = Visibility.Visible;
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));

                        OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));

                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                        BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                        _ContentOrder.Content = new CardOrderUserControl();

                        GetOrders();
                        TotalAmountTmp();
                    }
                    CurrentButton = (long)HomeButtonEnum.OPENING;
                });
                BtnBooking = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    //CurrentButton = (long)HomeButtonEnum.OPENING;
                    isChange = false;
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions) && CurrentButton != (long)HomeButtonEnum.BOOKING)
                    {
                        TextBoxSearch = Visibility.Hidden; 
                        DateTimeVisibility = Visibility.Visible;
                        RefreshVisibility = Visibility.Collapsed;
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                        OnlineBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                        BookingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/complete.png")));
                        _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                        _ContentOrder.Content = new ConfirmDepositBookingUserControl();
                        DatetimeInput = DateTime.Now;
                        CurrentPage = 1;
                        GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                        
                        PageDoubleLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            CurrentPage = 1;
                            GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                        });

                        PageLeft = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            if (CurrentPage > 1)
                            {
                                CurrentPage = CurrentPage - 1;
                                GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            }

                        });

                        PageRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            if (CurrentPage < TotalPage)
                            {
                                CurrentPage = CurrentPage + 1;
                                GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                            }

                        });
                        PageDoubleRight = new RelayCommand<UserControl>((u) => { return true; }, u =>
                        {
                            CurrentPage = TotalPage;
                            GetOrderListBooking(Utils.Utils.GetDateFormatVN(DatetimeInput), CurrentPage);
                        });
                    }
                    CurrentButton = (long)HomeButtonEnum.BOOKING;
                });
                MouseEnterCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (_MainContentControl != null)
                    {
                        if (currentSetting != null && t != null)
                        {
                            if ((int)BranchTypeEnum.LARGE == currentSetting.BranchType || currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                            {
                                #region toan
                                OrderDetailUserControl tmp = new OrderDetailUserControl();
                                    tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                    _MainContentControl.Content = tmp;
                                #endregion
                            }
                            else if ((int)BranchTypeEnum.MEDIUM == currentSetting.BranchType)
                            {
                                if ((int)BranchTypeOption.OPTIONTWO == currentSetting.BranchTypeOption)
                                {
                                    #region Dat
                                    if (t.TableId == 0)
                                    {
                                        if (t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                                        {
                                            #region toan
                                            OrderDetailUserControl tmp = new OrderDetailUserControl();
                                            tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                            _MainContentControl.Content = tmp;
                                            #endregion
                                        }
                                        else
                                        {
                                            CreateOrderTakeAwayUserControl createOrderUserControl = new CreateOrderTakeAwayUserControl();
                                            createOrderUserControl.DataContext = new CreateOrderTakeAwayViewModel(t.OrderId);
                                            _MainContentControl.Content = createOrderUserControl;
                                        }
                                        #endregion
                                        //CreateOrderTakeAwayUserControl createOrderUserControl = new CreateOrderTakeAwayUserControl();
                                        //createOrderUserControl.DataContext = new CreateOrderTakeAwayViewModel(t.OrderId);
                                        //_MainContentControl.Content = createOrderUserControl;
                                    }
                                    else
                                    {
                                        #region Dat
                                        if (t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                                        {
                                            #region toan
                                            OrderDetailUserControl tmp = new OrderDetailUserControl();
                                            tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                            _MainContentControl.Content = tmp;
                                            #endregion
                                        }
                                        else
                                        {
                                            OrderDetailUserControl tmp = new OrderDetailUserControl();
                                            tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                            _MainContentControl.Content = tmp;
                                        }
                                        #endregion
                                        //OrderDetailUserControl tmp = new OrderDetailUserControl();
                                        //tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                        //_MainContentControl.Content = tmp;
                                    }
                                }
                                else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ORDER_FOOD), currentUser.Permissions))
                                {
                                    if (t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                                    {
                                        #region toan
                                        OrderDetailUserControl tmp = new OrderDetailUserControl();
                                        tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                        _MainContentControl.Content = tmp;
                                        #endregion
                                    }
                                    else
                                    {
                                        if (t.IsOnline || t.TableId == 0)
                                        {
                                            CreateOrderTakeAwayUserControl createOrderUserControl = new CreateOrderTakeAwayUserControl();
                                            createOrderUserControl.DataContext = new CreateOrderTakeAwayViewModel(t.OrderId);
                                            _MainContentControl.Content = createOrderUserControl;
                                            //CreateOrderTakeAwayViewModel model = new CreateOrderTakeAwayViewModel(t.OrderId);  
                                            //MainSecondViewModel.ShowOrderFood(model.FoodOrderList);

                                        }
                                        else
                                        {
                                            CreateOrderUserControl createOrderUserControl = new CreateOrderUserControl();
                                            createOrderUserControl.DataContext = new CreateOrderViewModel(t.OrderId);
                                            _MainContentControl.Content = createOrderUserControl;
                                        }


                                    }
                                }
                                else
                                {
                                    OrderDetailUserControl tmp = new OrderDetailUserControl();
                                    tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                    _MainContentControl.Content = tmp;
                                }
                            }
                            else if ((int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                            {
                                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                                {
                                    if (t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                                    {
                                        #region toan
                                        OrderDetailUserControl tmp = new OrderDetailUserControl();
                                        tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                        _MainContentControl.Content = tmp;
                                        #endregion
                                    }
                                    else
                                    {
                                        if (t.IsOnline || t.IsTakeAway)
                                        {
                                            CreateOrderTakeAwayUserControl createOrderUserControl = new CreateOrderTakeAwayUserControl();
                                            createOrderUserControl.DataContext = new CreateOrderTakeAwayViewModel(t.OrderId);
                                            _MainContentControl.Content = createOrderUserControl;

                                        }
                                        else
                                        {
                                            CreateOrderUserControl createOrderUserControl = new CreateOrderUserControl();
                                            createOrderUserControl.DataContext = new CreateOrderViewModel(t.OrderId);
                                            _MainContentControl.Content = createOrderUserControl;
                                        }

                                    }
                                }
                                else
                                {
                                    OrderDetailUserControl tmp = new OrderDetailUserControl();
                                    tmp.DataContext = new OrderDetailViewModel(t.OrderId);
                                    _MainContentControl.Content = tmp;
                                }
                            }
                        }
                    }
                });
                EditBookingCommand = new RelayCommand<Models.Booking>((p) => { return true; }, p =>
                {
                    ReceviceDepositBookingWindow window = new ReceviceDepositBookingWindow();
                    window.DataContext = new ReceviceDepositBookingViewModel(p);
                    window.ShowDialog();
                    var model = window.DataContext as ReceviceDepositBookingViewModel;
                    if (model.isSuccess)
                    {
                        #region dat
                        model.CurrentBooking.EmployeeCreate = p.EmployeeCreate;
                        model.CurrentBooking.DepositEmployeeName = p.DepositEmployeeName;
                        #endregion
                        int index =  OrderBookingList.IndexOf(p);
                        OrderBookingList.Remove(p);
                        OrderBookingList.Insert(index, model.CurrentBooking);
                    }
                });
                BtnPrintBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    PrintBooking(p);
                });
                BtnViewBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    ViewBookingWindow view = new ViewBookingWindow();
                    view.DataContext = new ViewBookingViewModel(p);
                    view.ShowDialog();
                });
                BtnConfirmBooking = new RelayCommand<TechresStandaloneSale.Models.Booking>((p) => { return true; }, p =>
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_CONFIRM_RECEVICE_BOOKING;
                    string Title = MessageValue.MESSAGE_CONFIRM_RECEVICE_BOOKING_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        BookingClient bookingClient = new BookingClient(this, this, this);
                        BookingResponse response = bookingClient.ConfirmDepositBooking(p.Id);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            #region dat
                            response.Data.EmployeeCreate = p.EmployeeCreate;
                            response.Data.DepositEmployeeName = p.DepositEmployeeName;
                            #endregion
                            int index = OrderBookingList.IndexOf(p);
                            OrderBookingList.Remove(p);
                            OrderBookingList.Insert(index, response.Data);
                        }
                    }
                });
                MouseSaveClick = new RelayCommand<CardOrderItem>((t) => { return true; }, t => 
                {
                    if (currentSetting != null && t != null)
                    {
                        if (!currentSetting.IsAllowPrintTemporaryBill)
                        {
                            Views.Remove(t);
                        }
                        cardOrderItem = new CardOrderItem();
                        cardOrderItem.OrderId = t.OrderId;
                        cardOrderItem.CustomerSlot = t.CustomerSlot;
                        cardOrderItem.TableName = t.TableName;
                        cardOrderItem.TableId = t.TableId;
                        cardOrderItem.MegerTableObject = t.MegerTableObject;
                        cardOrderItem.CashAmount = t.CashAmount;
                        cardOrderItem.UsingTime = t.UsingTime;
                        cardOrderItem.IsOnline = t.IsOnline;
                        cardOrderItem.Amount = t.Amount;
                        cardOrderItem.Vat = t.Vat;
                        cardOrderItem.VatAmount = t.VatAmount;
                        cardOrderItem.DiscountPercent = t.DiscountPercent;
                        cardOrderItem.DiscountAmount = t.DiscountAmount;
                        cardOrderItem.PointOrder = t.PointOrder;
                        cardOrderItem.ShippingFee = t.ShippingFee;
                        cardOrderItem.BookingDepositAmount = t.BookingDepositAmount;
                        cardOrderItem.IsTakeAway = t.IsTakeAway;
                        if (t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT && !currentSetting.IsAllowPrintTemporaryBill)
                        {
                            if (!t.IsOnline )
                            {
                                cardOrderItem.OrderStatus = (int)OrderStatusEnum.WAITING_COMPLETE;
                                cardOrderItem.BtnDoneIsEnabled = false;
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                                Views.Insert(0, cardOrderItem);
                            }
                            else
                            {
                                cardOrderItem.OrderStatus = (int)OrderStatusEnum.DELIVERING;
                                cardOrderItem.BtnDoneIsEnabled = false;
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                                Views.Insert(0, cardOrderItem);
                            }

                        }
                        else if (t.OrderStatus == (int)OrderStatusEnum.OPENING && !currentSetting.IsAllowPrintTemporaryBill)
                        {
                            if (!t.IsOnline)
                            {
                                cardOrderItem.OrderStatus = (int)OrderStatusEnum.WAITING_COMPLETE;
                                cardOrderItem.BtnDoneIsEnabled = false;
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                                Views.Insert(0, cardOrderItem);
                            }
                            else
                            {
                                cardOrderItem.OrderStatus = (int)OrderStatusEnum.DELIVERING;
                                cardOrderItem.BtnDoneIsEnabled = false;
                                cardOrderItem.BtnPrintIsEnabled = false;
                                cardOrderItem.BtnSaveIsEnabled = false;
                                Views.Insert(0, cardOrderItem);
                            }

                        }
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            BaseResponse response = ordersClient.MakePaymentWaiting(t.OrderId);
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {

                                string Title = "HÓA ĐƠN THANH TOÁN";
                                PrintBill(Title, t.OrderId);
                            }
                            else
                            {
                                GetOrders();
                            }
                        }
                        else
                        if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                        {
                            t.BtnSaveIsEnabled = true;
                            if (currentSetting.IsAllowPrintTemporaryBill)
                            {
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                OrderItemResponse response = ordersClient.GetOrderById(t.OrderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.STATUS);
                                if (response != null)
                                {
                                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                                    if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                                    {
                                        pt.PrintBillTMP(response, device.CasherPrinter, device.CasherSize);
                                    }
                                    //GetOrders(); 
                                }
                                else
                                {
                                    GetOrders();
                                }
                            }
                            else
                            {
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                BaseResponse response =  ordersClient.MakePaymentWaitingCompleteNoRP(t.OrderId);
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    string Title = "HÓA ĐƠN THANH TOÁN";
                                    PrintBill(Title, t.OrderId);
                                    //GetOrders(); 
                                }
                                else
                                {
                                    GetOrders();
                                }
                            }

                        }
                    }
                });
                MousePrintClick = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE && t != null)
                    {
                        if (t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        {

                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            CardOrderItem c = Views.Where(x => x.OrderId == t.OrderId).FirstOrDefault();
                            if (c != null)
                            {
                                Views.Remove(c);
                            }

                            BaseResponse response =  ordersClient.MakePayment(t.OrderId, new CompleteOrderWrapper(0, t.CashAmount, 0, 0));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                string Title = "HÓA ĐƠN THANH TOÁN";
                                PrintBill(Title, t.OrderId);
                            }
                            else
                            {
                                GetOrders();
                            }
                        }
                        else if (t.OrderStatus == (int)OrderStatusEnum.DONE)
                        {
                            string Title = "HÓA ĐƠN THANH TOÁN";
                            PrintBill(Title, t.OrderId);
                        }
                        else if (t.OrderStatus == (int)OrderStatusEnum.OPENING)
                        {
                            NotificationMessage.Error(MessageValue.MESSAGE_ORDER_NOT_DONE);
                        }

                    }
                    if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                    {
                        // t.btnprint.IsEnabled = false;
                        //long orderId = long.Parse(t.lbOrderCode.Content.ToString().TrimStart('#'));
                        //int OrderStatus = Int32.Parse(t.OrderStatus.Content.ToString());
                        if (t.OrderStatus != (int)OrderStatusEnum.DONE)
                        {
                            CardOrderItem c = Views.Where(x => x.OrderId == t.OrderId).FirstOrDefault();
                            if (c != null)
                            {
                                Views.Remove(c);
                            }
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            BaseResponse response = ordersClient.CompleteOrder(t.OrderId, new CompleteOrderWrapper( 0, t.CashAmount, 0, 0));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                string Title = "HÓA ĐƠN THANH TOÁN";
                                PrintBill(Title, t.OrderId);

                            }
                            else
                            {
                                GetOrders();
                            }
                        }

                    }
                });
                MouseDoneClick = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (t != null && (t.CashAmount > 0 || t.PointOrder > 0))
                    {
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            if (t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                                paymentWindow.DataContext = new PaymentOrderViewModel(t.OrderId, t.CashAmount, t.Amount, t.Vat, t.VatAmount, t.DiscountAmount, t.DiscountPercent, t.PointOrder, t.ShippingFee, t.IsReturnDeposit ? t.BookingDepositAmount : 0, t.OrderStatus, !t.IsReturnDeposit  ? 0 : t.BookingDepositPaymentMethod);
                                paymentWindow.ShowDialog();
                            }
                            else
                            {
                                NotificationMessage.Error(MessageValue.MESSAGE_ORDER_NOT_DONE);
                            }
                        }
                        else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                        {
                            PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                            paymentWindow.DataContext = new PaymentOrderViewModel(t.OrderId, t.CashAmount, t.Amount, t.Vat, t.VatAmount, t.DiscountAmount, t.DiscountPercent, t.PointOrder, t.ShippingFee, t.IsReturnDeposit ? t.BookingDepositAmount : 0, t.OrderStatus, !t.IsReturnDeposit ? 0 : t.BookingDepositPaymentMethod);
                            paymentWindow.ShowDialog();
                        }

                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke((Action)async delegate
                        {
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            await Task.Run(() => ordersClient.MakePayment(t.OrderId, new CompleteOrderWrapper( 0, t.CashAmount, 0, 0)));
                        });

                    }
                });
                MoveTableCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (t != null)
                    {
                        if (t.TableId > 0)
                        {
                            TableOrderWindow tableWindow = new TableOrderWindow();
                            tableWindow.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_MOVE_TABLE_TITLE, t.TableName);//toan
                            tableWindow.ShowDialog();
                            var tableViewModel = tableWindow.DataContext as TableViewModel;
                            if (tableViewModel != null && tableViewModel.table != null && tableViewModel.table.Id > 0)
                            {
                                TablesClient tablesClient = new TablesClient(this, this, this);
                                tablesClient.MoveTable(t.TableId, tableViewModel.table.Id);

                            }
                        }
                        else
                        {
                            ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                            confirmDeleteWindow.DataContext = new ConfirmViewModel(MessageValue.MESSAGE_NOT_CURRENT_TABLE_MOVE, MessageValue.MESSAGE_NOTIFICATION, MessageValue.MESSAGE_FROM_TABLE_CANCLE, MessageValue.MESSAGE_FROM_TABLE_AGREE);
                            confirmDeleteWindow.ShowDialog();
                            var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                            if (confirm.isConfirm)
                            {
                                TableOrderWindow table = new TableOrderWindow();
                                table.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_CHOOSE_CURRENT_TABLE_TITLE, false);//toan
                                table.ShowDialog();
                                var tableViewModel = table.DataContext as TableViewModel;
                                if (tableViewModel != null && tableViewModel.table != null)
                                {
                                    TablesClient tablesClient = new TablesClient(this, this, this);
                                    BaseResponse baseResponse = tablesClient.SetTableByOrder(t.OrderId, tableViewModel.table.Id);
                                    if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                    {
                                        TableOrderWindow TableOrderWindow = new TableOrderWindow();
                                        TableOrderWindow.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_MOVE_TABLE_TITLE, tableViewModel.table.Name);//toan
                                        TableOrderWindow.ShowDialog();
                                        var tableviewModel = TableOrderWindow.DataContext as TableViewModel;
                                        if (tableviewModel != null && tableviewModel.table != null)
                                        {
                                            tablesClient.MoveTable(tableViewModel.table.Id, tableviewModel.table.Id);

                                        }
                                    }
                                }
                            }
                        }
                    }
                });
                MoveFoodCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (t != null)
                    {
                        if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                        {
                            if (t.TableId > 0)
                            {
                                TablesClient client = new TablesClient(this, this, this);
                                TableOrderWindow TableOrderWindow = new TableOrderWindow();
                                TableOrderWindow.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_MOVE_FOOD_TABLE_TITLE, true);//toan
                                TableOrderWindow.ShowDialog();
                                var tableViewModel = TableOrderWindow.DataContext as TableViewModel;

                                if (tableViewModel != null && tableViewModel.table != null)
                                {
                                    MoveFoodWindow window = new MoveFoodWindow();
                                    window.DataContext = new MoveFoodViewModel(t.OrderId, t.TableId, t.TableName, tableViewModel.table);
                                    window.ShowDialog();
                                    var moveFood = window.DataContext as MoveFoodViewModel;
                                    if (moveFood.isCheck)
                                    {
                                        NotificationMessage.Infomation(MessageValue.MESSAGE_FORM_MOVE_FOOD);
                                    }
                                }
                            }
                            else if (t.TableId == 0)
                            {
                                //ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                                //confirmDeleteWindow.DataContext = new ConfirmViewModel(MessageValue.MESSAGE_NOT_CURRENT_TABLE_MOVE, MessageValue.MESSAGE_NOTIFICATION, MessageValue.MESSAGE_FROM_TABLE_CANCLE, MessageValue.MESSAGE_FROM_TABLE_AGREE);
                                //confirmDeleteWindow.ShowDialog();
                                //var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                                //if (confirm.isConfirm)
                                //{
                                TableOrderWindow table = new TableOrderWindow();
                                table.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_CHOOSE_CURRENT_TABLE_TITLE, true);//toan
                                table.ShowDialog();
                                var tableViewModel = table.DataContext as TableViewModel;
                                if (tableViewModel != null && tableViewModel.table != null)
                                {
                                    //TablesClient tablesClient = new TablesClient(this, this, this);
                                    //BaseResponse baseResponse = tablesClient.SetTableByOrder(t.OrderId, tableViewModel.table.Id);
                                    //if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                    //{
                                    //    TableOrderWindow TableOrderWindow = new TableOrderWindow();
                                    //    TableOrderWindow.DataContext = new TableViewModel(MessageValue.MESSAGE_FROM_ORDER_MOVE_FOOD_TABLE_TITLE, true);
                                    //    TableOrderWindow.ShowDialog();
                                    //    var tableviewModel = TableOrderWindow.DataContext as TableViewModel;
                                    //    if (tableviewModel != null && tableviewModel.table != null)
                                    //    {
                                    MoveFoodWindow window = new MoveFoodWindow();
                                    window.DataContext = new MoveFoodViewModel(t.OrderId, t.TableId, t.TableName = "MV", tableViewModel.table);
                                    window.ShowDialog();
                                    var moveFood = window.DataContext as MoveFoodViewModel;
                                    if (moveFood.isCheck)
                                    {
                                        NotificationMessage.Infomation(MessageValue.MESSAGE_FORM_MOVE_FOOD);
                                        //var content = new NotificationContent
                                        //{
                                        //    Title = "Tách món",
                                        //    Message = " Chuyển món từ bàn " + t.lbTableName.Content.ToString() + " qua bàn " + tableViewModel.table.Name + " thành công",
                                        //    Type = NotificationType.Success,
                                        //};

                                        //_manager.Show(content, expirationTime: TimeSpan.FromSeconds(5));
                                        _MainContentControl.Content = new HomeUserControl();
                                        _MainContentControl.DataContext = new HomeViewModel();
                                    }
                                    // }
                                    // }
                                }
                                //}
                            }

                        }
                    }
                });
                MergeTableCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        if (t != null)
                        {
                            if (t.TableId > 0)
                            {
                                MergeTableWindow mergeTableWindow = new MergeTableWindow();
                                mergeTableWindow.DataContext = new MergeTableViewModel((int)t.TableId);
                                mergeTableWindow.ShowDialog();
                                var merge = mergeTableWindow.DataContext as MergeTableViewModel;
                                if (merge != null && merge.mergeTables != null && merge.mergeTables.Count > 0)
                                {
                                    string mergeTable = merge.mergeTables[0].Name;
                                    for (int i = 1; i < merge.mergeTables.Count; i++)
                                    {
                                        mergeTable = mergeTable + "," + merge.mergeTables[i].Name;
                                    }
                                    List<int> TableIds = merge.mergeTables.Select(x => x.Id).ToList();
                                    if (TableIds != null && TableIds.Count > 0)
                                    {
                                        //if( )
                                        //{
                                           
                                        //}    
                                        TablesClient tablesClient = new TablesClient(this, this, this);
                                        BaseResponse baseResponse = await Task.Run(() => tablesClient.MergeTable(t.TableId, TableIds));

                                        //if(baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                        //{
                                        //    NotificationMessage.Infomation("Gộp bàn thành công");
                                        //    GetOrders();
                                        //}
                                        //else
                                        //{
                                        //    NotificationMessage.Warning("Bàn vừa chọn đang được gộp hoặc đang được sử dụng");
                                        //}

                                    }
                                }

                            }
                            else
                            {
                                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                                confirmDeleteWindow.DataContext = new ConfirmViewModel(MessageValue.MESSAGE_NOT_CURRENT_TABLE_MOVE, MessageValue.MESSAGE_NOTIFICATION, MessageValue.MESSAGE_FROM_TABLE_CANCLE, MessageValue.MESSAGE_FROM_TABLE_AGREE);
                                confirmDeleteWindow.ShowDialog();
                                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                                if (confirm.isConfirm)
                                {
                                    TableOrderWindow table = new TableOrderWindow();
                                    table.DataContext = new TableViewModel((int)t.TableId,MessageValue.MESSAGE_FROM_ORDER_CHOOSE_CURRENT_TABLE_TITLE, true);//toan
                                    table.ShowDialog();
                                    var tableViewModel = table.DataContext as TableViewModel;
                                    if (tableViewModel != null && tableViewModel.table != null)
                                    {
                                        TablesClient tablesClient = new TablesClient(this, this, this);
                                        BaseResponse baseResponse = tablesClient.SetTableByOrder(t.OrderId, tableViewModel.table.Id);
                                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                        {
                                            MergeTableWindow mergeTableWindow = new MergeTableWindow();
                                            mergeTableWindow.DataContext = new MergeTableViewModel((int)t.TableId);
                                            mergeTableWindow.ShowDialog();
                                            var merge = mergeTableWindow.DataContext as MergeTableViewModel;
                                            if (merge != null && merge.mergeTables != null)
                                            {
                                                string mergeTable = merge.mergeTables[0].Name;
                                                for (int i = 1; i < merge.mergeTables.Count; i++)
                                                {
                                                    mergeTable = mergeTable + "," + merge.mergeTables[i].Name;
                                                }
                                                List<int> TableIds = merge.mergeTables.Select(x => x.Id).ToList();
                                                if (TableIds != null && TableIds.Count > 0)
                                                {
                                                    await Task.Run(() => tablesClient.MergeTable(tableViewModel.table.Id, TableIds));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                });
                CancelTableCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                    {
                        if (t.TableId > 0)
                        {
                            TablesClient tablesClient = new TablesClient(this, this, this);
                            tablesClient.CancelTable(t.TableId);

                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_NOT_TABLE_MOVE);
                        }
                    }
                });
                CustomerSlotCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                    {  
                            {
                                //  long orderId = long.Parse(t.lbOrderCode.Content.ToString().TrimStart('#'));
                                CreateSlotCustomerTableWindow window = new CreateSlotCustomerTableWindow();
                                window.DataContext = new CreateSlotCustomerTableViewModel((int)t.CustomerSlot);
                                window.ShowDialog();

                                var createSlotCustomer = window.DataContext as CreateSlotCustomerTableViewModel;
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                BaseResponse response = ordersClient.UpdateCustomerSlotNumber(t.OrderId, createSlotCustomer.Slot);
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    //   t.lbCustomerSlot.Content = createSlotCustomer.Slot;
                                }
                            var load = window.DataContext as CreateSlotCustomerTableViewModel;
                            if (load.IsDone)
                            {
                                currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                                if (currentSetting.BranchType == (long)BranchTypeEnum.SMALL && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                                {
                                    RealTime();
                                    GetOrders();
                                    Views.Remove(t);
                                    TotalAmountTmp();
                                    GetRestaurantKitchen(currentUser.RestaurantBrandId, currentUser.BranchId);
                                }
                                else
                                {
                                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CASHIER_ACCESS), currentUser.Permissions))
                                    {
                                        RealTime();
                                        GetOrders();
                                        TotalAmountTmp();
                                        if (currentSetting.BranchType != (long)BranchTypeEnum.LARGE && currentSetting.BranchType < (long)BranchTypeEnum.LARGE)
                                        {
                                            GetRestaurantKitchen(currentUser.RestaurantBrandId, currentUser.BranchId);
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                });
                HistoryActivityCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    ActivityWindow activityWindow = new ActivityWindow();
                    activityWindow.DataContext = new ActivityOrderViewModel(t.OrderId);
                    activityWindow.ShowDialog();
                });
                DebitCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (t != null)
                    {
                        if (t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        {
                            DebitOrderEmployeeWindow window = new DebitOrderEmployeeWindow();
                            window.DataContext = new DebitOrderEmployeeViewModel(t);
                            window.ShowDialog();
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.FORBIDDEN);
                        }
                        //if (currentSetting.BranchType <= (int)BranchTypeEnum.LARGE)
                        //{
                        //    if (t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                        //    {
                        //        DebitOrderEmployeeWindow window = new DebitOrderEmployeeWindow();
                        //        window.DataContext = new DebitOrderEmployeeViewModel(t);
                        //        window.ShowDialog();
                        //    }
                        //    else
                        //    {
                        //        NotificationMessage.Warning(MessageValue.FORBIDDEN);
                        //    }
                        //}
                        //else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                        //{
                        //    DebitOrderEmployeeWindow window = new DebitOrderEmployeeWindow();
                        //    window.DataContext = new DebitOrderEmployeeViewModel(t);
                        //    window.ShowDialog();
                        //}
                    }


                });
                CustomerDebitCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (t != null)
                    {
                        if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
                        {
                            if (t.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT || t.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || t.OrderStatus == (int)OrderStatusEnum.DELIVERING)
                            {
                                CustomerDebitOrderWindow window = new CustomerDebitOrderWindow();
                                window.DataContext = new CustomerDebitOrderViewModel(t);
                                window.ShowDialog();
                            }
                            else
                            {
                                NotificationMessage.Warning(MessageValue.FORBIDDEN);
                            }
                        }
                        else if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                        {
                            CustomerDebitOrderWindow window = new CustomerDebitOrderWindow();
                            window.DataContext = new CustomerDebitOrderViewModel(t);
                            window.ShowDialog();
                        }
                    }


                });
                SendFoodCookCommand = new RelayCommand<CardOrderItem>((t) => { return true; }, t =>
                {
                    if (currentSetting.BranchType == (int)BranchTypeEnum.MEDIUM || (int)BranchTypeEnum.SMALL == currentSetting.BranchType)
                    {
                        SendFoodCookVisibility = Visibility.Visible;
                        // long orderId = long.Parse(t.lbOrderCode.Content.ToString().TrimStart('#'));
                        PrintCook(t.OrderId);
                    }
                    else
                    {       
                        SendFoodCookVisibility = Visibility.Collapsed;
                    }

                });
                BtnViewOrder = new RelayCommand<Order>((p) => { return true; }, p =>
                {
                    OrderViewWindow popup = new OrderViewWindow();
                    popup.DataContext = new OrderViewViewModel(p.Id, ((Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH)).Id);
                    popup.ShowDialog();
                });
                BtnHistoryActivityOrder = new RelayCommand<Order>((p) => { return true; }, p =>
                {
                    ActivityWindow activityWindow = new ActivityWindow();
                    activityWindow.DataContext = new ActivityOrderViewModel(p.Id);
                    //activityWindow.Show();
                    activityWindow.ShowDialog();
                });
                BtnPrintOrder = new RelayCommand<Order>((p) => { return true; }, p =>
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

                        PrintBill(TitleBILL, p.Id);
                    }
                });
                BtnActiveVatCommand = new RelayCommand<Order>((p) => { return true; }, p =>
                {
                    ConfirmActiveVATWindow confirmDeleteWindow = new ConfirmActiveVATWindow();
                    string Title = MessageValue.MESSAGE_ACTIVE_VAT_TITLE;
                    string contentConfirm = MessageValue.MESSAGE_ACTIVE_VAT;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmActiveVATViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmActiveVATViewModel;
                    if (confirm.isConfirm)
                    {

                        OrdersClient client = new OrdersClient(this, this, this);
                        OrderItemResponse response = client.GetOrderAddionalVAT(p.Id, confirm.PaymentMethord);
                        OrderViewViewModel orderView = new OrderViewViewModel(p.Id, ((Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH)).Id);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        //if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
                        {
                            int index = ItemsOrderDone.IndexOf(p);
                            ItemsOrderDone.Remove(p);
                            p.VatAmount = Utils.Utils.FormatMoneyDecimal(orderView.VAT);
                            p.TotalAmount = Utils.Utils.FormatMoneyDecimal(orderView.TotalAmount);
                            //p.VatAmount = (decimal)response.Data.Foods.Sum(t =>t.VatAmout);  //toan
                            //p.TotalAmount = response.Data.TotalAmount;
                            ItemsOrderDone.Insert(index, p);
                        }
                        #region toan
                        if (Utils.Utils.FormatMoneyDecimal(p.AmountVatString) > 0)
                        {
                            NotificationMessage.Infomation(MessageValue.MESSAGE_CONFIRM_ACTIVE_VAT);
                            return;
                        }
                        #endregion
                    }
                });
                //BtnList = new RelayCommand<UserControl>((p) => { return true; }, p =>
                //{
                //    _ContentOrder = p.FindName("ContentOrder") as ContentControl;
                //    _ContentOrder.Content = new OrderListUserControl();
                //    _ContentOrder.DataContext = new OrderListViewModel();
                //});
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        public void PrintBooking(Models.Booking booking)
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
                    //Console.WriteLine(jsonResponse.message);
                    //string a = jsonResponse.message;
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
        public void LogError(Exception ex, string infoMessage)
        {
            //
        }
    }
}

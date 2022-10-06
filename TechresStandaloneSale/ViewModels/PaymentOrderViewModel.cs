using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
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
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class PaymentOrderViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public string _ContentPaymentOrder { get; set; }
        public string ContentPaymentOrder { get => _ContentPaymentOrder; set { _ContentPaymentOrder = value; OnPropertyChanged("ContentPaymentOrder"); } }
        public string _TotalAmountPayment { get; set; }
        public string TotalAmountPayment { get => _TotalAmountPayment; set { _TotalAmountPayment = value; OnPropertyChanged("TotalAmountPayment"); } }
        public string _InputMoney { get; set; }
        public string InputMoney { get => _InputMoney; set { _InputMoney = value; OnPropertyChanged("InputMoney"); } }
        public string _MoneyCash { get; set; }
        public string MoneyCash { get => _MoneyCash; set { _MoneyCash = value; OnPropertyChanged("MoneyCash"); } }
        public string _MoneyBank { get; set; }
        public string MoneyBank { get => _MoneyBank; set { _MoneyBank = value; OnPropertyChanged("MoneyBank"); } }
        public string _MoneyPoint { get; set; }
        public string MoneyPoint { get => _MoneyPoint; set { _MoneyPoint = value; OnPropertyChanged("MoneyPoint"); } }
        public string _MoneyTrasfer { get; set; }
        public string MoneyTrasfer { get => _MoneyTrasfer; set { _MoneyTrasfer = value; OnPropertyChanged("MoneyTrasfer"); } }

        public string _MoneyReturnOfCustomer { get; set; }
        public string MoneyReturnOfCustomer { get => _MoneyReturnOfCustomer; set { _MoneyReturnOfCustomer = value; OnPropertyChanged("MoneyReturnOfCustomer"); } }
        public string _CashSuggest1 { get; set; }
        public string _CashSuggest2 { get; set; }
        public string _CashSuggest3 { get; set; }
        public string _CashSuggest4 { get; set; }
        public string _CashSuggest5 { get; set; }
        public string _CashSuggest6 { get; set; }

        private string _TipAmount { get; set; }
        public string TipAmount { get => _TipAmount; set { _TipAmount = value; OnPropertyChanged("TipAmount"); } }
        private string _TotalAmount { get; set; }
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }
        private bool _IsTipAmount { get; set; }
        public bool IsTipAmount { get => _IsTipAmount; set { _IsTipAmount = value; OnPropertyChanged("IsTipAmount"); } }
        private string _Amount { get; set; }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }
        private decimal _Vat { get; set; }
        public decimal Vat { get => _Vat; set { _Vat = value; OnPropertyChanged("Vat"); } }
        private string _VatContent { get; set; }
        public string VatContent { get => _VatContent; set { _VatContent = value; OnPropertyChanged("VatContent"); } }
        private string _VatAmount { get; set; }
        public string VatAmount { get => _VatAmount; set { _VatAmount = value; OnPropertyChanged("VatAmount"); } }
        private string _ShippingAmount { get; set; }
        public string ShippingAmount { get => _ShippingAmount; set { _ShippingAmount = value; OnPropertyChanged("ShippingAmount"); } }
        private string _CustomerGiveAmount { get; set; }
        public string CustomerGiveAmount { get => _CustomerGiveAmount; set { _CustomerGiveAmount = value; OnPropertyChanged("CustomerGiveAmount"); } }
        private Visibility _ShippingVisibility { get; set; }
        public Visibility ShippingVisibility { get => _ShippingVisibility; set { _ShippingVisibility = value; OnPropertyChanged("ShippingVisibility"); } }
        private string _DepositAmount { get; set; }
        public string DepositAmount { get => _DepositAmount; set { _DepositAmount = value; OnPropertyChanged("DepositAmount"); } }
        private Visibility _DepositVisibility { get; set; }
        public Visibility DepositVisibility { get => _DepositVisibility; set { _DepositVisibility = value; OnPropertyChanged("DepositVisibility"); } }
        private string _DiscountAmount { get; set; }
        public string DiscountAmount { get => _DiscountAmount; set { _DiscountAmount = value; OnPropertyChanged("DiscountAmount"); } }
        private string _DiscountContent { get; set; }
        public string DiscountContent { get => _DiscountContent; set { _DiscountContent = value; OnPropertyChanged("DiscountContent"); } }
        private decimal _DiscountPercent;
        public decimal DiscountPercent { get => _DiscountPercent; set { _DiscountPercent = value; OnPropertyChanged("DiscountPercent"); } }
        private bool _MakePaymentEnable { get; set; }
        public bool MakePaymentEnable { get => _MakePaymentEnable; set { _MakePaymentEnable = value; OnPropertyChanged("MakePaymentEnable"); } }
        private bool _MakePaymentPrintEnable { get; set; }
        public bool MakePaymentPrintEnable { get => _MakePaymentPrintEnable; set { _MakePaymentPrintEnable = value; OnPropertyChanged("MakePaymentPrintEnable"); } }
        private Visibility _MethodCashVisibility { get; set; }
        public Visibility MethodCashVisibility { get => _MethodCashVisibility; set { _MethodCashVisibility = value; OnPropertyChanged("MethodCashVisibility"); } }
        private Visibility _MethodTranferVisibility { get; set; }
        public Visibility MethodTranferVisibility { get => _MethodTranferVisibility; set { _MethodTranferVisibility = value; OnPropertyChanged("MethodTranferVisibility"); } }

        private Visibility _MethodBankVisibility { get; set; }
        public Visibility MethodBankVisibility { get => _MethodBankVisibility; set { _MethodBankVisibility = value; OnPropertyChanged("MethodBankVisibility"); } }
        private Visibility _TipAmountVisibility { get; set; }
        public Visibility TipAmountVisibility { get => _TipAmountVisibility; set { _TipAmountVisibility = value; OnPropertyChanged("TipAmountVisibility"); } }
        private Visibility _MethodPointVisibility { get; set; }
        public Visibility MethodPointVisibility { get => _MethodPointVisibility; set { _MethodPointVisibility = value; OnPropertyChanged("MethodPointVisibility"); } }
        private Visibility _TipVisibility { get; set; }
        public Visibility TipVisibility { get => _TipVisibility; set { _TipVisibility = value; OnPropertyChanged("TipVisibility"); } }
        
        private bool _InputMoneyEnabled { get; set; }
        public bool InputMoneyEnabled { get => _InputMoneyEnabled; set { _InputMoneyEnabled = value; OnPropertyChanged("InputMoneyEnabled"); } }

        private bool _MoneyEnabled { get; set; }
        public bool MoneyEnabled { get => _MoneyEnabled; set { _MoneyEnabled = value; OnPropertyChanged("MoneyEnabled"); } }
        // private int MethodCashMoneyBanKing;

        private bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }

        public ICommand InputMoney_500K { get; set; }
        public ICommand InputMoney_200K { get; set; }
        public ICommand InputMoney_100K { get; set; }
        public ICommand InputMoney_50K { get; set; }
        public ICommand InputMoney_20K { get; set; }
        public ICommand InputMoney_10K { get; set; }
        public ICommand InputMoney_5K { get; set; }
        public ICommand InputMoney_2K { get; set; }
        public ICommand InputMoney_1K { get; set; }
        public ICommand ReturnMoney_500K { get; set; }
        public ICommand ReturnMoney_200K { get; set; }
        public ICommand ReturnMoney_100K { get; set; }
        public ICommand ReturnMoney_50K { get; set; }
        public ICommand ReturnMoney_20K { get; set; }
        public ICommand ReturnMoney_10K { get; set; }
        public ICommand ReturnMoney_5K { get; set; }
        public ICommand ReturnMoney_2K { get; set; }
        public ICommand ReturnMoney_1K { get; set; }
        public ICommand CashCommand { get; set; }
        public ICommand BankCommand { get; set; }
        public ICommand TransferCommand { get; set; }
        public ICommand ButtonCashSuggest1 { get; set; }
        public ICommand ButtonCashSuggest2 { get; set; }
        public ICommand ButtonCashSuggest3 { get; set; }
        public ICommand ButtonCashSuggest4 { get; set; }
        public ICommand ButtonCashSuggest5 { get; set; }
        public ICommand ButtonCashSuggest6 { get; set; }
        public ICommand Close { get; set; }
        public ICommand CancelMoneyCashCommand { get; set; }
        public ICommand CancelMoneyBankCommand { get; set; }
        public ICommand CancelMoneyTransferCommand { get; set; }
        public ICommand PrintAndDoneOrderCommand { get; set; }
        public ICommand MakePaymentCommand { get; set; }
        public ICommand IsTipAmountCommand { get; set; }
        public ICommand TipAmountChangedCommand { get; set; }
        public bool isCash = true;
        public bool isTranfer = false; 
        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);

        public bool isSUCCESS;

        public void Total()
        {
            if (CompareEq0String(MoneyBank) || CompareEq0String(MoneyTrasfer))
            { 
                TipVisibility = Visibility.Visible;
            }
            else
            {
                TipVisibility = Visibility.Collapsed;
            }
            CustomerGiveAmount = AddThreeString(MoneyCash, MoneyBank, MoneyTrasfer);
            if (!CompareTwoString(CustomerGiveAmount, TotalAmountPayment))
            {
                TipAmount = "0";
            }
            #region toan
            decimal depositAmount = Utils.Utils.FormatMoneyDecimal(DepositAmount);
            decimal amount = Utils.Utils.FormatMoneyDecimal(Amount);
            decimal returnAmout;
            if (depositAmount > amount)
            {
                returnAmout = depositAmount - amount;
                MoneyReturnOfCustomer = Utils.Utils.FormatMoney(returnAmout);
            }
            else
            {
                //if (isTranfer == true)
                //{
                    MoneyReturnOfCustomer = SubTwoString(CustomerGiveAmount, AddTwoString(TotalAmount, TipAmount));
                    //decimal Tranfer = Utils.Utils.FormatMoneyDecimal(MoneyTrasfer);
                    //decimal total = Utils.Utils.FormatMoneyDecimal(TotalAmountPayment);// Ha Sua TotalAmout -> TotalAmountPayment
                    //if (Tranfer < total)
                    //{
                    //    MoneyReturnOfCustomer = SubTwoString(CustomerGiveAmount, AddTwoString(TotalAmount, TipAmount));
                        
                    //}
                    //else
                    //{
                    //    TipAmount = SubTwoString(MoneyTrasfer, TotalAmountPayment);
                    //    MoneyReturnOfCustomer = "0";
                    //}
                    //decimal tip = Utils.Utils.FormatMoneyDecimal(TipAmount);
                   
                }
                //else
                //{
                //    MoneyReturnOfCustomer = SubTwoString(CustomerGiveAmount, AddTwoString(TotalAmount, TipAmount));
                //}
            //}
            #endregion
            //MoneyReturnOfCustomer = SubTwoStringDeposit(DepositAmount,TotalAmountPayment);

        }


        public PaymentOrderViewModel(
            long OrderId,
            decimal AmountPayment,
            decimal amount,
            float vat,
            decimal vatAmount,
            decimal discountAmount,
            float discountPercent,
            long pointOrder,
            decimal shippingFee,
            decimal depositAmount,
            long orderStatus,
            int bookingDepositPaymentMethod)
        {          
            MakePaymentEnable = true;
            GetDetail(OrderId, AmountPayment, amount, vat, vatAmount, discountAmount, discountPercent, pointOrder, shippingFee, depositAmount, orderStatus);
            InputMoney_500K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_200K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_100K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_50K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_20K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_10K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_5K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_2K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            InputMoney_1K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = AddTwoString(p.Content.ToString(), InputMoney);
            });
            ReturnMoney_500K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {

                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_200K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_100K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_50K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_20K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_10K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_5K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_2K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            ReturnMoney_1K = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = SubTwoString(InputMoney, p.Content.ToString());
            });
            Close = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                
                p.Close();
            });
            ButtonCashSuggest1 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            ButtonCashSuggest2 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            ButtonCashSuggest3 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            ButtonCashSuggest4 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            ButtonCashSuggest5 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            ButtonCashSuggest6 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                InputMoney = p.Content.ToString();
            });
            CancelMoneyCashCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                MethodCashVisibility = Visibility.Collapsed;
                MoneyCash = "0";
                Total();
            });
            CancelMoneyBankCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                MethodBankVisibility = Visibility.Collapsed;
                MoneyBank = "0";
                Total();
            });
            CancelMoneyTransferCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                MethodTranferVisibility = Visibility.Collapsed;
                MoneyTrasfer = "0";
                Total();
            });
            CashCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
            decimal input = Utils.Utils.FormatMoneyDecimal(InputMoney.Replace(",", ""));
            if (input > 0)
            {

                if (!CompareTwoString(CustomerGiveAmount, TotalAmountPayment))
                    {
                        MethodCashVisibility = Visibility.Visible;
                        MoneyCash = InputMoney;
                        InputMoney = "0";
                        Total();
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_COMPARE_AMOUNT_PAYMENT);
                    }
                }
            else
            {
               NotificationMessage.Warning(MessageValue.MESSAGE_CUSTOMER_PAY);
            }
           

            });
            BankCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                decimal input = Utils.Utils.FormatMoneyDecimal(InputMoney.Replace(",", ""));
                if (input > 0)
                {
                    if (!CompareTwoString(CustomerGiveAmount, TotalAmountPayment))
                    {

                        MethodBankVisibility = Visibility.Visible;
                        MoneyBank = InputMoney;
                        InputMoney = "0";
                        Total();
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_COMPARE_AMOUNT_PAYMENT);
                    }
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_CUSTOMER_PAY);

                }

            });
            TransferCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTranfer = true; 
                decimal input = Utils.Utils.FormatMoneyDecimal(InputMoney.Replace(",", ""));
                decimal totalBill = Utils.Utils.FormatMoneyDecimal(TotalAmountPayment.Replace(",", "")); 
                if (input > 0 )
                {
                    if (!CompareTwoString(CustomerGiveAmount, TotalAmountPayment))
                    {
                        MethodTranferVisibility = Visibility.Visible;
                        MoneyTrasfer = InputMoney;
                        InputMoney = "0";
                        Total();
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_COMPARE_AMOUNT_PAYMENT);
                    }
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_CUSTOMER_PAY);

                }
            });
            PrintAndDoneOrderCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                #region toan
                decimal moneyReturn = Utils.Utils.FormatMoneyDecimal(MoneyReturnOfCustomer);
                decimal bankAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyBank) ? "0" : MoneyBank);
                decimal tranferAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyTrasfer) ? "0" : MoneyTrasfer);
                decimal cashAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyCash) ? "0" : MoneyCash);
                if (moneyReturn < 0) //  Ha sua 
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_COMPARE_AMOUNT_PAYMENT);
                    return;
                }
                #endregion
                //if (cashAmount > 0 && bankAmount > 0 && moneyReturn > 0 || cashAmount > 0 && tranferAmount > 0 && moneyReturn > 0 || bankAmount > 0 && tranferAmount > 0 && moneyReturn > 0 || cashAmount > 0 && bankAmount > 0 && tranferAmount > 0 && moneyReturn > 0)
                //{
                //    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_PAYMENT_METHODS);
                //    return;
                //}
                if (MakePaymentPrintEnable)
                {
                    MakePaymentPrintEnable = false;
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        if (AmountPayment == 0 && pointOrder > 0)
                        {
                            CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(0, 0, 0, 0);
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            DialogHostOpen = true;
                            BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                isSUCCESS = true;
                                p.Close();
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        }
                        else
                        {
                            decimal tipAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(TipAmount) ? "0" : TipAmount);
                            decimal returnAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyReturnOfCustomer) ? "0" : MoneyReturnOfCustomer);

                            //if (AmountPayment + shippingFee - depositAmount == cashAmount + tranferAmount + bankAmount - tipAmount - returnAmount)

                            //if (tranferAmount > 0 && tranferAmount + bankAmount > AmountPayment + shippingFee - depositAmount)
                            //{
                            //    MakePaymentPrintEnable = true;
                            //    NotificationMessage.Warning(MessageValue.MESSAGE_CREDIT_CARD_NOT_AVALID);
                            //}
                            //else
                            //if (tipAmount < cashAmount || tipAmount < tranferAmount || tipAmount < bankAmount)
                            //{

                            if (depositAmount < AmountPayment)
                            {
                                if (AmountPayment + shippingFee == cashAmount + tranferAmount + bankAmount - returnAmount + depositAmount)  // Ha sua 
                                {
                                    if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.CASH)
                                    {
                                        if (cashAmount == 0)
                                        {
                                            //cashAmount = cashAmount - shippingFee;
                                            if (tranferAmount > 0)
                                            {
                                                //tranferAmount = tranferAmount ;
                                                //cashAmount = depositAmount - returnAmount;
                                                if (tranferAmount > AmountPayment)
                                                {
                                                    returnAmount = tranferAmount - AmountPayment;
                                                }
                                                else if (tranferAmount < AmountPayment)
                                                {
                                                    cashAmount = depositAmount - returnAmount;
                                                    returnAmount = 0;
                                                }
                                            }
                                            else
                                            {
                                                cashAmount = cashAmount + depositAmount;
                                            }
                                        }
                                        else if (cashAmount > 0)
                                        {
                                            cashAmount = cashAmount - shippingFee - returnAmount + depositAmount;

                                        }

                                    }
                                    else if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.BANK)
                                    {
                                        bankAmount = bankAmount + depositAmount;
                                        //bankAmount = bankAmount - returnAmount - tipAmount - shippingFee + depositAmount;
                                        cashAmount = cashAmount - returnAmount - tipAmount - shippingFee;
                                    }
                                    else if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.TRANSFER)
                                    {
                                        tranferAmount = tranferAmount + depositAmount;
                                        //tranferAmount = tranferAmount - returnAmount - tipAmount - shippingFee + depositAmount;
                                        cashAmount = cashAmount - returnAmount - tipAmount - shippingFee;

                                    }
                                    else
                                    {
                                        if (cashAmount > 0)
                                        {
                                            cashAmount = cashAmount - returnAmount - shippingFee - depositAmount;
                                            returnAmount = 0; 
                                        }
                                    }

                                    if (tranferAmount > 0 || bankAmount > 0)
                                    {
                                        CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, returnAmount, tranferAmount);
                                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                                        DialogHostOpen = true;
                                        BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                        if (response != null && response.Status == (int)ResponseEnum.OK)
                                        {
                                            p.Close();
                                            isSUCCESS = true;
                                            string Title = "HÓA ĐƠN THANH TOÁN";
                                            PrintBill(Title, OrderId);
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;
                                        }
                                        else
                                        {
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;

                                        }
                                    }
                                    else
                                    {
                                        CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, tipAmount, tranferAmount);
                                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                                        DialogHostOpen = true;
                                        BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                        if (response != null && response.Status == (int)ResponseEnum.OK)
                                        {
                                            p.Close();
                                            isSUCCESS = true;
                                            string Title = "HÓA ĐƠN THANH TOÁN";
                                            PrintBill(Title, OrderId);
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;
                                        }
                                        else
                                        {
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;

                                        }
                                    }

                                }
                                else
                                {
                                    MakePaymentEnable = true;
                                    NotificationMessage.Warning(MessageValue.MESSAGE_CONFIRM_AMOUNT_PAYMENT);
                                }
                            }
                            else if (depositAmount >= AmountPayment)
                            {
                                cashAmount = AmountPayment;
                                CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, tipAmount, tranferAmount);
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                DialogHostOpen = true;
                                BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    p.Close();
                                    isSUCCESS = true;
                                    string Title = "HÓA ĐƠN THANH TOÁN";
                                    PrintBill(Title, OrderId);
                                    MakePaymentEnable = true;
                                    DialogHostOpen = false;
                                }
                                else
                                {
                                    MakePaymentEnable = true;
                                    DialogHostOpen = false;

                                }
                            }
                            //}
                            // else
                            //{
                            //    MakePaymentEnable = true;
                            //    NotificationMessage.Warning(MessageValue.MESSAGE_CONFIRM_AMOUNT_PAYMENT);
                            //}
                        }

                    });
                }
            });
            MakePaymentCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                #region toan
                decimal moneyReturn = Utils.Utils.FormatMoneyDecimal(MoneyReturnOfCustomer);
                decimal bankAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyBank) ? "0" : MoneyBank);
                decimal tranferAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyTrasfer) ? "0" : MoneyTrasfer);
                decimal cashAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyCash) ? "0" : MoneyCash);
                if (moneyReturn < 0) //  Ha sua 
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_COMPARE_AMOUNT_PAYMENT);
                    return;
                }
                #endregion
                if (cashAmount > 0 && bankAmount > 0 && moneyReturn > 0 || cashAmount > 0 && tranferAmount > 0 && moneyReturn > 0 || bankAmount> 0 && tranferAmount > 0 && moneyReturn > 0 || cashAmount > 0 && bankAmount > 0 && tranferAmount > 0 && moneyReturn > 0)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_PAYMENT_METHODS);
                    return;
                }
                if (MakePaymentEnable)
                {
                    MakePaymentEnable = false;
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        if (AmountPayment == 0 && pointOrder > 0)
                        {
                            CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(0, 0, 0, 0);
                            OrdersClient ordersClient = new OrdersClient(this, this, this);
                            DialogHostOpen = true;
                            BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                isSUCCESS = true;
                                p.Close();
                                DialogHostOpen = false;
                            }
                            else
                                DialogHostOpen = false;
                        }
                        else
                        {
                            decimal tipAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(TipAmount) ? "0" : TipAmount);
                            decimal returnAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyReturnOfCustomer) ? "0" : MoneyReturnOfCustomer);

                            //if (AmountPayment + shippingFee - depositAmount == cashAmount + tranferAmount + bankAmount - tipAmount - returnAmount)

                            //if (tranferAmount > 0 && tranferAmount + bankAmount > AmountPayment + shippingFee - depositAmount)
                            //{
                            //    MakePaymentPrintEnable = true;
                            //    NotificationMessage.Warning(MessageValue.MESSAGE_CREDIT_CARD_NOT_AVALID);
                            //}
                            //else
                            //if (tipAmount < cashAmount || tipAmount < tranferAmount || tipAmount < bankAmount)
                            //{

                            if (depositAmount < AmountPayment)
                            {
                                if (AmountPayment + shippingFee == cashAmount + tranferAmount + bankAmount - returnAmount + depositAmount)  // Ha sua 
                                {
                                    if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.CASH)
                                    {
                                        if (cashAmount == 0)
                                        {
                                            //cashAmount = cashAmount - shippingFee;
                                            if(tranferAmount > 0)
                                            {
                                                //tranferAmount = tranferAmount ;
                                                //cashAmount = depositAmount - returnAmount;
                                                if(tranferAmount > AmountPayment)
                                                {
                                                    returnAmount = tranferAmount - AmountPayment; 
                                                } 
                                                else if(tranferAmount < AmountPayment)
                                                {
                                                    cashAmount = depositAmount - returnAmount;
                                                    returnAmount = 0; 
                                                }
                                            }
                                            else
                                            {
                                                cashAmount = cashAmount + depositAmount; 
                                            }
                                        }
                                        else if (cashAmount > 0)
                                        {
                                            cashAmount = cashAmount - shippingFee - returnAmount + depositAmount;
                                            
                                        }

                                    }
                                    else if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.BANK)
                                    {
                                        bankAmount = bankAmount + depositAmount;
                                        //bankAmount = bankAmount - returnAmount - tipAmount - shippingFee + depositAmount;
                                        cashAmount = cashAmount - returnAmount - tipAmount - shippingFee;
                                    }
                                    else if (bookingDepositPaymentMethod == (int)PaymentMethodEnum.TRANSFER)
                                    {
                                        tranferAmount = tranferAmount + depositAmount;
                                        //tranferAmount = tranferAmount - returnAmount - tipAmount - shippingFee + depositAmount;
                                        cashAmount = cashAmount - returnAmount - tipAmount - shippingFee;

                                    }
                                    else
                                    {
                                        if (cashAmount > 0)
                                        {
                                            cashAmount = cashAmount - returnAmount - shippingFee - depositAmount;
                                        }
                                    }

                                    if (tranferAmount > 0 || bankAmount > 0)
                                    {
                                        CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, returnAmount , tranferAmount);
                                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                                        DialogHostOpen = true;
                                        BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                        if (response != null && response.Status == (int)ResponseEnum.OK)
                                        {
                                            p.Close();
                                            isSUCCESS = true;
                                            string Title = "HÓA ĐƠN THANH TOÁN";
                                            //PrintBill(Title, OrderId);
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;
                                        }
                                        else
                                        {
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;

                                        }
                                    }
                                    else
                                    {
                                        CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, tipAmount, tranferAmount);
                                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                                        DialogHostOpen = true;
                                        BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                        if (response != null && response.Status == (int)ResponseEnum.OK)
                                        {
                                            p.Close();
                                            isSUCCESS = true;
                                            string Title = "HÓA ĐƠN THANH TOÁN";
                                            //PrintBill(Title, OrderId);
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;
                                        }
                                        else
                                        {
                                            MakePaymentEnable = true;
                                            DialogHostOpen = false;

                                        }
                                    }

                                }
                                else
                                {
                                    MakePaymentEnable = true;
                                    NotificationMessage.Warning(MessageValue.MESSAGE_CONFIRM_AMOUNT_PAYMENT);
                                }
                            }
                            else if (depositAmount >= AmountPayment)
                            {
                                cashAmount = AmountPayment;
                                CompleteOrderWrapper completeOrderWrapper = new CompleteOrderWrapper(bankAmount, cashAmount, tipAmount, tranferAmount);
                                OrdersClient ordersClient = new OrdersClient(this, this, this);
                                DialogHostOpen = true;
                                BaseResponse response = await Task.Run(() => ordersClient.MakePayment(OrderId, completeOrderWrapper));
                                if (response != null && response.Status == (int)ResponseEnum.OK)
                                {
                                    p.Close();
                                    isSUCCESS = true;
                                    string Title = "HÓA ĐƠN THANH TOÁN";
                                    //PrintBill(Title, OrderId);
                                    MakePaymentEnable = true;
                                    DialogHostOpen = false;
                                }
                                else
                                {
                                    MakePaymentEnable = true;
                                    DialogHostOpen = false;

                                }
                            }
                            //}
                           // else
                            //{
                            //    MakePaymentEnable = true;
                            //    NotificationMessage.Warning(MessageValue.MESSAGE_CONFIRM_AMOUNT_PAYMENT);
                            //}
                        }

                    });
                }
            });
            IsTipAmountCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (IsTipAmount)
                {
                    TipAmountVisibility = Visibility.Visible;
                    TipAmount = SubTwoString(CustomerGiveAmount, TotalAmount);
                }
                else
                {
                    TipAmountVisibility = Visibility.Collapsed;
                    TipAmount = "0";
                }
                Total();
            });
            TipAmountChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                TipAmount = SubTwoString(CustomerGiveAmount, TotalAmount);
                Total();
            });
        }


        public void GetDetail(long orderId = 0, decimal PaymentAmount = 0, decimal amount = 0, float vat = 0, decimal vatAmount = 0, decimal discountAmount = 0, float discountPercent = 0, long pointOrder = 0, decimal shippingFee = 0, decimal depositAmount = 0, long orderStatus = 0)
        {
            if (pointOrder > 0)
            {
                MethodPointVisibility = Visibility.Visible;
            }
            else
            {
                MethodPointVisibility = Visibility.Collapsed;
            }
            MakePaymentPrintEnable = true;
            MakePaymentEnable = false;
            // Nếu loại hình nhà hàng level 3 trở lên thì check status order được thanh toán ko ? 
            if (currentSetting.BranchType >= (int)BranchTypeEnum.LARGE)
            {
                if (orderStatus == (int)OrderStatusEnum.DELIVERING || orderStatus == (int)OrderStatusEnum.WAITING_COMPLETE)
                {
                    MakePaymentEnable = true;
                }
                else
                {
                    MakePaymentEnable = false;
                }
            }
            // Level nhà hàng 2 thì cho thanh  toán mà không cần yêu cầu thanh toán
            else
            {
                MakePaymentEnable = true;
            }
            decimal TotalMoneyL = 0;
            if (PaymentAmount <= depositAmount)
            {
                TotalMoneyL = 0; 
            }
            else
            {
                TotalMoneyL = PaymentAmount + shippingFee - depositAmount; 
            }
           
            //decimal TotalMoneyL = PaymentAmount  + shippingFee;
            decimal Suggest1 = TotalMoneyL;

            decimal Cal5K = TotalMoneyL % (decimal)MoneyEnum.MONEY_5K;
            decimal Suggest2 = TotalMoneyL + ((decimal)MoneyEnum.MONEY_5K - Cal5K);

            decimal Cal10K = TotalMoneyL % (decimal)MoneyEnum.MONEY_10K;
            decimal Suggest3 = TotalMoneyL + ((decimal)MoneyEnum.MONEY_10K - Cal10K);

            decimal Cal50K = TotalMoneyL % (decimal)MoneyEnum.MONEY_50K;
            decimal Suggest4 = TotalMoneyL + ((decimal)MoneyEnum.MONEY_50K - Cal50K);

            decimal Cal100K = TotalMoneyL % (decimal)MoneyEnum.MONEY_100K;
            decimal Suggest5 = TotalMoneyL + ((decimal)MoneyEnum.MONEY_100K - Cal100K);

            decimal Cal500K = TotalMoneyL % (decimal)MoneyEnum.MONEY_500K;
            decimal Suggest6 = TotalMoneyL + ((decimal)MoneyEnum.MONEY_500K - Cal500K);

            if (Suggest1 == Suggest2)
            {
                Suggest2 = Suggest2 + (decimal)MoneyEnum.MONEY_5K;
            }
            if (Suggest1 == Suggest3)
            {
                Suggest3 = Suggest3 + (decimal)MoneyEnum.MONEY_10K;
            }

            if (Suggest1 == Suggest4)
            {
                Suggest4 = Suggest4 + (decimal)MoneyEnum.MONEY_50K;
            }
            if (Suggest1 == Suggest5)
            {
                Suggest5 = Suggest5 + (decimal)MoneyEnum.MONEY_100K;
            }
            if (Suggest1 == Suggest6)
            {
                Suggest6 = Suggest6 + (decimal)MoneyEnum.MONEY_500K;
            }
            if (Suggest2 == Suggest3)
            {
                Suggest3 = Suggest3 + (decimal)MoneyEnum.MONEY_10K;
            }
            if (Suggest2 == Suggest4)
            {
                Suggest4 = Suggest4 + (decimal)MoneyEnum.MONEY_50K;
            }
            if (Suggest2 == Suggest5)
            {
                Suggest5 = Suggest5 + (decimal)MoneyEnum.MONEY_100K;
            }
            if (Suggest2 == Suggest6)
            {
                Suggest6 = Suggest6 + (decimal)MoneyEnum.MONEY_500K;
            }
            if (Suggest3 == Suggest4)
            {
                Suggest4 = Suggest4 + (decimal)MoneyEnum.MONEY_50K;
            }
            if (Suggest3 == Suggest5)
            {
                Suggest5 = Suggest5 + (decimal)MoneyEnum.MONEY_100K;
            }
            if (Suggest3 == Suggest6)
            {
                Suggest6 = Suggest6 + (decimal)MoneyEnum.MONEY_500K;
            }
            if (Suggest4 == Suggest5)
            {
                Suggest5 = Suggest5 + (decimal)MoneyEnum.MONEY_100K;
            }
            if (Suggest4 == Suggest6)
            {
                Suggest6 = Suggest6 + (decimal)MoneyEnum.MONEY_500K;
            }
            if (Suggest5 == Suggest6)
            {
                Suggest6 = Suggest6 + (decimal)MoneyEnum.MONEY_500K;
            }
            _CashSuggest1 = Utils.Utils.FormatMoney(Suggest1);
            _CashSuggest2 = Utils.Utils.FormatMoney(Suggest2);
            _CashSuggest3 = Utils.Utils.FormatMoney(Suggest3);
            _CashSuggest4 = Utils.Utils.FormatMoney(Suggest4);
            _CashSuggest5 = Utils.Utils.FormatMoney(Suggest5);
            _CashSuggest6 = Utils.Utils.FormatMoney(Suggest6);

            ContentPaymentOrder = "THANH TOÁN HÓA ĐƠN";
            // TotalAmountPayment so tien can thu
            if (PaymentAmount > depositAmount)
            {
                TotalAmountPayment = Utils.Utils.FormatMoney(PaymentAmount + shippingFee - depositAmount); // Ha sua 
            }
            else
            {
                TotalAmountPayment = "0"; 
            }
            //TotalAmountPayment = Utils.Utils.FormatMoney(PaymentAmount + shippingFee - depositAmount);
            InputMoney = "0";
            MethodCashVisibility = Visibility.Collapsed;
            MethodBankVisibility = Visibility.Collapsed;
            MethodTranferVisibility = Visibility.Collapsed;
            TipVisibility = Visibility.Visible;
            MoneyCash = "0";
            MoneyBank = "0";
            MoneyTrasfer = "0";
            MoneyPoint = Utils.Utils.FormatMoney(pointOrder);
            TipAmount = "0";
            IsTipAmount = true;
            TipAmountVisibility = Visibility.Visible;
            Amount = Utils.Utils.FormatMoney(amount);
            DiscountAmount = Utils.Utils.FormatMoney(discountAmount);
            VatAmount = Utils.Utils.FormatMoney(vatAmount);
            VatContent = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_VAT, vat);
            DiscountContent = string.Format(MessageValue.MESSAGE_FROM_VIEW_DETAIL_ORDER_DISCOUNT, discountPercent);
            decimal returnAmount = Utils.Utils.FormatMoneyDecimal(string.IsNullOrEmpty(MoneyReturnOfCustomer) ? "0" : MoneyReturnOfCustomer);
            #region toan
            if (depositAmount > PaymentAmount)
            {
                MoneyEnabled = true;
                TotalAmount = "0";
            }
            else //PaymentAmount > depositAmount
            {
                MoneyEnabled = true;
                TotalAmount = Utils.Utils.FormatMoney(PaymentAmount + shippingFee - depositAmount);
            }
            #endregion
            //TotalAmount = Utils.Utils.FormatMoney(PaymentAmount + shippingFee);
            if (shippingFee > 0)
            {
                ShippingVisibility = Visibility.Visible;
                ShippingAmount = Utils.Utils.FormatMoney(shippingFee);
            }
            else
            {
                ShippingVisibility = Visibility.Collapsed;
            }
            if (depositAmount > 0)
            {
                DepositVisibility = Visibility.Visible;
                DepositAmount = Utils.Utils.FormatMoney(depositAmount);
            }
            else
            {
                DepositVisibility = Visibility.Collapsed;
            }
            Total();
        }
        public string SubTwoString(string DividendString, string DivisorString)
        {
            decimal tmp1 = string.IsNullOrEmpty(DividendString) ? 0 : decimal.Parse(DividendString.Replace(".", ""));// tien tra lai khach
            decimal tmp2 = string.IsNullOrEmpty(DivisorString) ? 0 : decimal.Parse(DivisorString.Replace(".", ""));// tien thanh toan + tien tip
            if (tmp1 > 0)
            {
                if (tmp1 - tmp2 > 0)
                {
                    return Utils.Utils.FormatMoney(tmp1 - tmp2);
                }
                else
                {
                    return Utils.Utils.FormatMoney(tmp1 - tmp2);
                }
            }
            else
                return "0"; 

        }
        public string SubTwoStringDeposit(string input, string deposit)
        {
            decimal tmp1 = string.IsNullOrEmpty(input) ? 0 : decimal.Parse(input.Replace(".", ""));
            decimal tmp2 = string.IsNullOrEmpty(deposit) ? 0 : decimal.Parse(deposit.Replace(".", ""));

            if (tmp1 > tmp2 )
            {
                return Utils.Utils.FormatMoney(tmp1 - tmp2);
            }
            else
            {
                return Utils.Utils.FormatMoney(tmp2 - tmp1);
            }
        }
        public string AddThreeString(string DividendString, string DivisorString, string ThreeString)
        {
            decimal tmp1 = string.IsNullOrEmpty(DividendString) ? 0 : decimal.Parse(DividendString.Replace(".", ""));
            decimal tmp2 = string.IsNullOrEmpty(DivisorString) ? 0 : decimal.Parse(DivisorString.Replace(".", ""));
            decimal tmp3 = string.IsNullOrEmpty(ThreeString) ? 0 : decimal.Parse(ThreeString.Replace(".", ""));
            if (tmp1 + tmp2 + tmp3 > 0)
            {
                return Utils.Utils.FormatMoney(tmp1 + tmp2 + tmp3);
            }
            else
            {
                return "0";
            }
        }

        public string AddTwoString(string DividendString, string DivisorString)
        {
            decimal tmp1 = string.IsNullOrEmpty(DividendString) ? 0 : decimal.Parse(DividendString.Replace(".", ""));
            //decimal tmp2 = string.IsNullOrEmpty(DivisorString) ? 0 : decimal.Parse(DivisorString.Replace(".", ""));
            #region Dat
            decimal tmp2;
            if (string.IsNullOrEmpty(DivisorString))
            {
                DivisorString = "0";
                tmp2 = decimal.Parse(DivisorString);
            }
            else
            {
                string convert = DivisorString.Replace(",", "");
                if (string.IsNullOrEmpty(convert))
                {
                    convert = "0";
                }
                tmp2 = decimal.Parse(convert);
            }
            #endregion
            if (tmp1 + tmp2 > 0)
            {
                return Utils.Utils.FormatMoney(tmp1 + tmp2);
            }
            else
            {
                return "0";
            }
        }
        public bool CompareEq0String(string DividendString)
        {
            if (!string.IsNullOrEmpty(DividendString))
            {

                decimal tmp1 = decimal.Parse(DividendString.Replace(".", ""));
                if (tmp1 > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool CompareTwoString(string DividendString, string DivisorString)
        {
            decimal tmp1 = string.IsNullOrEmpty(DividendString) ? 0 : decimal.Parse(DividendString.Replace(".", ""));
            decimal tmp2 = string.IsNullOrEmpty(DivisorString) ? 0 : decimal.Parse(DivisorString.Replace(".", ""));
            if (tmp1 >= tmp2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DeviceClient deviceClient = new DeviceClient();
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

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

        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }

    }
}

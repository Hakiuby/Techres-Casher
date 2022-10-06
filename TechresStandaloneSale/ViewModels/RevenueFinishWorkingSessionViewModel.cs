using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
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
    public class RevenueFinishWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        #region By Phan Viet Ha 

        #region Dat Coc 
        private string _DepositAmount;
        public string DepositAmount
        {
            get => _DepositAmount;
            set
            {
                _DepositAmount = value;
                OnPropertyChanged("DepositAmount");
            }
        }
        private string _DepositCashAmount;
        public string DepositCashAmount
        {
            get => _DepositCashAmount;
            set
            {
                _DepositCashAmount = value;
                OnPropertyChanged("DepositCashAmount");
            }
        }
        private string _DepositBankAmount;
        public string DepositBankAmount
        {
            get => _DepositBankAmount;
            set
            {
                _DepositBankAmount = value;
                OnPropertyChanged("DepositBankAmount");
            }
        }
        private string _DepositTransferAmount;
        public string DepositTransferAmount
        {
            get => _DepositTransferAmount; set
            {
                _DepositTransferAmount = value;
                OnPropertyChanged("DepositTransferAmount");
            }
        }

        #endregion
        #region Tra Coc 
        private string _ReturnDepositAmount;
        public string ReturnDepositAmount
        {
            get => _ReturnDepositAmount;
            set
            {
                _ReturnDepositAmount = value;
                OnPropertyChanged("ReturnDepositAmount");
            }
        }

        private string _ReturnDepositCashAmount;
        public string ReturnDepositCashAmount
        {
            get => _ReturnDepositCashAmount;
            set
            {
                _ReturnDepositCashAmount = value;
                OnPropertyChanged("ReturnDepositCashAmount");
            }
        }

        private string _ReturnDepositTransferAmount;
        public string ReturnDepositTransferAmount
        {
            get => _ReturnDepositTransferAmount;
            set
            {
                _ReturnDepositTransferAmount = value;
                OnPropertyChanged("ReturnDepositTransferAmount");
            }
        }
        #endregion
        #region Phieu Thu
        private string _InTotalAmountByAdditionFee;
        public string InTotalAmountByAdditionFee
        {
            get => _InTotalAmountByAdditionFee;
            set
            {
                _InTotalAmountByAdditionFee = value;
                OnPropertyChanged("InTotalAmountByAdditionFee");
            }
        }
        private string _InCashAmountByAdditionFee;
        public string InCashAmountByAdditionFee
        {
            get => _InCashAmountByAdditionFee;
            set
            {
                _InCashAmountByAdditionFee = value;
                OnPropertyChanged("InCashAmountByAdditionFee");
            }
        }
        private string _InBankAmountByAdditionFee;
        public string InBankkAmountByAdditionFee
        {
            get => _InBankAmountByAdditionFee;
            set
            {
                _InBankAmountByAdditionFee = value;
                OnPropertyChanged("InBankkAmountByAdditionFee");
            }
        }
        private string _InTransferAmountByAdditionFee;
        public string InTransferAmountByAdditionFee
        {
            get => _InTransferAmountByAdditionFee;
            set
            {
                _InTransferAmountByAdditionFee = value;
                OnPropertyChanged("InTransferAmountByAdditionFee");
            }
        }
        #endregion
        #region Phieu Chi 
        private string _OutTotalAmountByAdditionFee;
        public string OutTotalAmountByAdditionFee
        {
            get => _OutTotalAmountByAdditionFee;
            set
            {
                _OutTotalAmountByAdditionFee = value;
                OnPropertyChanged("OutTotalAmountByAdditionFee");
            }

        }
        private string _OutCashAmountByAdditionFee;
        public string OutCashAmountByAdditionFee
        {
            get => _OutCashAmountByAdditionFee; set
            {
                _OutCashAmountByAdditionFee = value;
                OnPropertyChanged("OutCashAmountByAdditionFee");
            }
        }
        private string _OutTransferAmountByAdditionFee;
        public string OutTransferAmountByAdditionFee
        {
            get => _OutTransferAmountByAdditionFee;
            set
            {
                _OutTransferAmountByAdditionFee = value;
                OnPropertyChanged("OutTransferAmountByAdditionFee");
            }
        }
        private string _TipAmount; 
        public string TipAmount { get => _TipAmount; set { _TipAmount = value; OnPropertyChanged("TipAmount"); } }
        #endregion
        #region Tong Hop
        private string _TotalAmountFinal;
        public string TotalAmountFinal
        {
            get => _TotalAmountFinal;
            set
            {
                _TotalAmountFinal = value;
                OnPropertyChanged("TotalAmountFinal");
            }
        }

        private string _TotalReceiptAmountFinal;
        public string TotalReceiptAmountFinal
        {
            get => _TotalReceiptAmountFinal;
            set
            {
                _TotalReceiptAmountFinal = value;
                OnPropertyChanged("TotalReceiptAmountFinal");
            }

        }
        #endregion
        #endregion




        private string _WorkingSessionTitle;
        public string WorkingSessionTitle
        {
            get
            {
                return _WorkingSessionTitle;
            }
            set
            {
                _WorkingSessionTitle = value;

                OnPropertyChanged("WorkingSessionTitle");
            }
        }
        private string _CheckInTitle;
        public string CheckInTitle
        {
            get
            {
                return _CheckInTitle;
            }
            set
            {
                _CheckInTitle = value;

                OnPropertyChanged("CheckInTitle");
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
        private string _OpenTime;
        public string OpenTime
        {
            get { return _OpenTime; }
            set { _OpenTime = value; OnPropertyChanged("OpenTime"); }
        }
        private string _DifferenceAmount;
        public string DifferenceAmount
        {
            get
            {
                return _DifferenceAmount;
            }
            set
            {
                _DifferenceAmount = value;

                OnPropertyChanged("DifferenceAmount");
            }
        }
        private string _TotalAmount;
        public string TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;

                OnPropertyChanged("TotalAmount");
            }
        }
        private string _CashAmount;
        public string CashAmount
        {
            get
            {
                return _CashAmount;
            }
            set
            {
                _CashAmount = value;

                OnPropertyChanged("CashAmount");
            }
        }
        private string _BankAmount;
        public string BankAmount
        {
            get
            {
                return _BankAmount;
            }
            set
            {
                _BankAmount = value;

                OnPropertyChanged("BankAmount");
            }
        }
        private string _TransferAmount;
        public string TransferAmount
        {
            get
            { return _TransferAmount; }
            set
            { _TransferAmount = value; OnPropertyChanged("TransferAmount"); }
        }
        private string _DebtAmount;
        public string DebtAmount
        {
            get
            {
                return _DebtAmount;
            }
            set
            {
                _DebtAmount = value;

                OnPropertyChanged("DebtAmount");
            }
        }
        private string _TotalDeposit;
        public string TotalDeposit
        {
            get
            {
                return _TotalDeposit;
            }
            set
            {
                _TotalDeposit = value;

                OnPropertyChanged("TotalDeposit");
            }
        }

        private string _CashDeposit;
        public string CashDeposit
        {
            get
            {
                return _CashDeposit;
            }
            set
            {
                _CashDeposit = value;

                OnPropertyChanged("CashDeposit");
            }
        }
        private string _TransferDeposit;
        public string TransferDeposit
        {
            get
            {
                return _TransferDeposit;
            }
            set
            {
                _TransferDeposit = value;

                OnPropertyChanged("TransferDeposit");
            }
        }
        private string _BankDeposit;
        public string BankDeposit
        {
            get
            {
                return _BankDeposit;
            }
            set
            {
                _BankDeposit = value;

                OnPropertyChanged("BankDeposit");
            }
        }
        private string _TotalReturnDeposit;
        public string TotalReturnDeposit
        {
            get
            {
                return _TotalReturnDeposit;
            }
            set
            {
                _TotalReturnDeposit = value;

                OnPropertyChanged("TotalReturnDeposit");
            }
        }
        private string _CashReturnDeposit;
        public string CashReturnDeposit
        {
            get
            {
                return _CashReturnDeposit;
            }
            set
            {
                _CashReturnDeposit = value;

                OnPropertyChanged("CashReturnDeposit");
            }
        }

        private string _BankReturnDeposit;
        public string BankReturnDeposit
        {
            get
            {
                return _BankReturnDeposit;
            }
            set
            {
                _BankReturnDeposit = value;

                OnPropertyChanged("BankReturnDeposit");
            }
        }
        private string _TransferReturnDeposit;
        public string TransferReturnDeposit
        {
            get
            {
                return _TransferReturnDeposit;
            }
            set
            {
                _TransferReturnDeposit = value;

                OnPropertyChanged("TransferReturnDeposit");
            }
        }
        private string _ReceiptAmount;
        public string ReceiptAmount
        {
            get
            {
                return _ReceiptAmount;
            }
            set
            {
                _ReceiptAmount = value;

                OnPropertyChanged("ReceiptAmount");
            }
        }

        private string _CashReceiptAmount;
        public string CashReceiptAmount
        {
            get
            {
                return _CashReceiptAmount;
            }
            set
            {
                _CashReceiptAmount = value;

                OnPropertyChanged("CashReceiptAmount");
            }
        }
        private string _TransferReceiptAmount;
        public string TransferReceiptAmount
        {
            get
            {
                return _TransferReceiptAmount;
            }
            set
            {
                _TransferReceiptAmount = value;

                OnPropertyChanged("TransferReceiptAmount");
            }
        }
        private string _BankReceiptAmount;
        public string BankReceiptAmount
        {
            get
            {
                return _BankReceiptAmount;
            }
            set
            {
                _BankReceiptAmount = value;

                OnPropertyChanged("BankReceiptAmount");
            }
        }
        private string _PaymentShipAmount;
        public string PaymentShipAmount
        {
            get
            {
                return _PaymentShipAmount;
            }
            set
            {
                _PaymentShipAmount = value;

                OnPropertyChanged("PaymentShipAmount");
            }
        }
        private string _CashPaymentShipAmount;
        public string CashPaymentShipAmount
        {
            get
            {
                return _CashPaymentShipAmount;
            }
            set
            {
                _CashPaymentShipAmount = value;

                OnPropertyChanged("CashPaymentShipAmount");
            }
        }

        private string _BankPaymentShipAmount;
        public string BankPaymentShipAmount
        {
            get
            {
                return _BankPaymentShipAmount;
            }
            set
            {
                _BankPaymentShipAmount = value;

                OnPropertyChanged("BankPaymentShipAmount");
            }
        }
        private string _TransferPaymentShipAmount;
        public string TransferPaymentShipAmount
        {
            get
            {
                return _TransferPaymentShipAmount;
            }
            set
            {
                _TransferPaymentShipAmount = value;

                OnPropertyChanged("TransferPaymentShipAmount");
            }
        }
        private string _TotalReceiptAmount;
        public string TotalReceiptAmount
        {
            get
            {
                return _TotalReceiptAmount;
            }
            set
            {
                _TotalReceiptAmount = value;

                OnPropertyChanged("TotalReceiptAmount");
            }
        }

        private string _CashTotalReceiptAmount;
        public string CashTotalReceiptAmount
        {
            get
            {
                return _CashTotalReceiptAmount;
            }
            set
            {
                _CashTotalReceiptAmount = value;

                OnPropertyChanged("CashTotalReceiptAmount");
            }
        }
        private string _BankTotalReceiptAmount;
        public string BankTotalReceiptAmount
        {
            get
            {
                return _BankTotalReceiptAmount;
            }
            set
            {
                _BankTotalReceiptAmount = value;

                OnPropertyChanged("BankTotalReceiptAmount");
            }
        }
        private string _TransferTotalReceiptAmount;
        public string TransferTotalReceiptAmount
        {
            get
            {
                return _TransferTotalReceiptAmount;
            }
            set
            {
                _TransferTotalReceiptAmount = value;

                OnPropertyChanged("TransferTotalReceiptAmount");
            }
        }
        private string _TransferTotalPaymentShipAmount;
        public string TransferTotalPaymentShipAmount
        {
            get
            {
                return _TransferTotalPaymentShipAmount;
            }
            set
            {
                _TransferTotalPaymentShipAmount = value;

                OnPropertyChanged("TransferTotalPaymentShipAmount");
            }
        }
        private string _TotalPaymentShipAmount;
        public string TotalPaymentShipAmount
        {
            get
            {
                return _TotalPaymentShipAmount;
            }
            set
            {
                _TotalPaymentShipAmount = value;

                OnPropertyChanged("TotalPaymentShipAmount");
            }
        }
        private string _CashTotalPaymentShipAmount;
        public string CashTotalPaymentShipAmount
        {
            get
            {
                return _CashTotalPaymentShipAmount;
            }
            set
            {
                _CashTotalPaymentShipAmount = value;

                OnPropertyChanged("CashTotalPaymentShipAmount");
            }
        }
        private string _BankTotalPaymentShipAmount;
        public string BankTotalPaymentShipAmount
        {
            get
            {
                return _BankTotalPaymentShipAmount;
            }
            set
            {
                _BankTotalPaymentShipAmount = value;

                OnPropertyChanged("BankTotalPaymentShipAmount");
            }
        }
        private string _BeforeAmount;
        public string BeforeAmount
        {
            get
            {
                return _BeforeAmount;
            }
            set
            {
                _BeforeAmount = value;

                OnPropertyChanged("BeforeAmount");
            }
        }
        private string _AfterAmount;
        public string AfterAmount
        {
            get
            {
                return _AfterAmount;
            }
            set
            {
                _AfterAmount = value;

                OnPropertyChanged("AfterAmount");
            }
        }
        private string _SystemReceiveAmount;
        public string SystemReceiveAmount
        {
            get
            {
                return _SystemReceiveAmount;
            }
            set
            {
                _SystemReceiveAmount = value;

                OnPropertyChanged("SystemReceiveAmount");
            }
        }
        private string _CurrentAmount;
        public string CurrentAmount
        {
            get
            {
                return _CurrentAmount;
            }
            set
            {
                _CurrentAmount = value;

                OnPropertyChanged("CurrentAmount");
            }
        }
        private string _TotalSessionAmount;
        public string TotalSessionAmount
        {
            get
            {
                return _TotalSessionAmount;
            }
            set
            {
                _TotalSessionAmount = value;

                OnPropertyChanged("TotalSessionAmount");
            }
        }
        private string _TakeAwayOrder;
        public string TakeAwayOrder
        {
            get
            {
                return _TakeAwayOrder;
            }
            set
            {
                _TakeAwayOrder = value;

                OnPropertyChanged("TakeAwayOrder");
            }
        }
        private string _MembershipTotalAddPointUsedAmount;
        public string MembershipTotalAddPointUsedAmount
        {
            get
            {
                return _MembershipTotalAddPointUsedAmount;
            }
            set
            {
                _MembershipTotalAddPointUsedAmount = value;

                OnPropertyChanged("MembershipTotalAddPointUsedAmount");
            }
        }

        private string _MembershipTotalAccumulatePointUsedAmount;
        public string MembershipTotalAccumulatePointUsedAmount
        {
            get
            {
                return _MembershipTotalAccumulatePointUsedAmount;
            }
            set
            {
                _MembershipTotalAccumulatePointUsedAmount = value;

                OnPropertyChanged("MembershipTotalAccumulatePointUsedAmount");
            }
        }
        private string _MembershipTotalAloPointUsedAmount;
        public string MembershipTotalAloPointUsedAmount
        {
            get
            {
                return _MembershipTotalAloPointUsedAmount;
            }
            set
            {
                _MembershipTotalAloPointUsedAmount = value;

                OnPropertyChanged("MembershipTotalAloPointUsedAmount");
            }
        }
        private string _MembershipTotalPromotionPointUsedAmount;
        public string MembershipTotalPromotionPointUsedAmount
        {
            get
            {
                return _MembershipTotalPromotionPointUsedAmount;
            }
            set
            {
                _MembershipTotalPromotionPointUsedAmount = value;

                OnPropertyChanged("MembershipTotalPromotionPointUsedAmount");
            }
        }
        private string _MembershipPromotionPointUsedAmount;
        public string MembershipPromotionPointUsedAmount
        {
            get
            {
                return _MembershipPromotionPointUsedAmount;
            }
            set
            {
                _MembershipPromotionPointUsedAmount = value;

                OnPropertyChanged("MembershipPromotionPointUsedAmount");
            }
        }
        private string _MembershipAloPointUsedAmount;
        public string MembershipAloPointUsedAmount
        {
            get
            {
                return _MembershipAloPointUsedAmount;
            }
            set
            {
                _MembershipAloPointUsedAmount = value;

                OnPropertyChanged("MembershipAloPointUsedAmount");
            }
        }
        private string _MembershipAccumulatePointUsedAmount;
        public string MembershipAccumulatePointUsedAmount
        {
            get
            {
                return _MembershipAccumulatePointUsedAmount;
            }
            set
            {
                _MembershipAccumulatePointUsedAmount = value;

                OnPropertyChanged("MembershipAccumulatePointUsedAmount");
            }
        }
        private string _MembershipAddPointUsedAmount;
        public string MembershipAddPointUsedAmount
        {
            get
            {
                return _MembershipAddPointUsedAmount;
            }
            set
            {
                _MembershipAddPointUsedAmount = value;

                OnPropertyChanged("MembershipAddPointUsedAmount");
            }
        }

        public ICommand RevenueOrderCommand { get; set; }

        public ICommand OrderViewCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand BookingDepositViewCommand { get; set; }

        public ICommand BookingReturnDepositViewCommand { get; set; }

        public ICommand ReceiptViewCommand { get; set; }
        public ICommand PaymentShipViewCommand { get; set; }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        private ObservableCollection<Money> _AmountList = new ObservableCollection<Money>();
        public ObservableCollection<Money> AmountList
        {
            get
            {
                return _AmountList;
            }
            set
            {
                _AmountList = value;
                OnPropertyChanged("AmountList");
            }
        }
        public void GetDenominations()
        {
            if (AmountList != null)
            {
                AmountList.Clear();
            }
            else
            {
                AmountList = new ObservableCollection<Money>();
            }
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_500K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_200K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_100K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_50K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_20K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_10K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_5K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_2K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_1K, 0));
            AmountList.Add(new Money((decimal)MoneyEnum.MONEY_500, 0));
            // AmountList.Add(new Money((decimal)MoneyEnum.MONEY_200, 0));
        }
        public async void getRevenueFinishWorkingSession()
        {
            DialogHostOpen = true;
            WorkingSessionClient client = new WorkingSessionClient(this, this, this);
            RevenueFinishWorkingSessionResponse revenueFinishWorkingSession = await System.Threading.Tasks.Task.Run(() => client.GetRevenueFinishWorkingSessionToDay());
            if (revenueFinishWorkingSession != null && revenueFinishWorkingSession.Status == (int)ResponseEnum.OK && revenueFinishWorkingSession.Data != null)
            {
                WorkingSessionTitle = string.Format(MessageValue.MESSAGE_FROM_REVENUE_FINISH_WORKING_SESION_TITLE, currentUser.Name);

                CheckInTitle = string.Format(MessageValue.MESSAGE_FROM_REVENUE_FINISH_CHECKIN_TITLE, revenueFinishWorkingSession.Data.OpenTime, "Chưa đóng ca");

                OpenTime = revenueFinishWorkingSession.Data.OpenTime;

                //TotalAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalAmount);
                CashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.CashAmount);
                BankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BankAmount);
                TransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TransferAmount);
                DebtAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DebtAmount);
                TakeAwayOrder = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TakeAwayOrders);
                MembershipAccumulatePointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount);
                MembershipAddPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipPromotionPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);
                MembershipAloPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount);

                TotalDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositAmount);
                BankDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositBankAmount);
                CashDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositCashAmount);
                TransferDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositTransferAmount);

                TotalReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositAmount);
                TransferReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositTransferAmount);
                CashReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositCashAmount);


                PaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTotalAmountByAdditionFee);
                CashPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutCashAmountByAdditionFee);
                TransferPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTransferAmountByAdditionFee);
             //   MembershipPointUsedPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutMembershiPointUsedAmountByAdditionFee);

                ReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTotalAmountByAdditionFee);
                CashReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InCashAmountByAdditionFee);
                BankReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InBankAmountByAdditionFee);
                TransferReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTransferAmountByAdditionFee);

                BeforeAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BeforeCash);
                TotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInAmount);
                CashTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInCashAmount);
                BankTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInBankAmount);
                TransferTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInTransferAmount);
                TotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutAmount);
                CashTotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutCashAmount);
                TransferTotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutTransferAmount);
                AfterAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.AfterCash);
                TotalSessionAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalCash);
                CurrentAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.RealAmount);
                DifferenceAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalCash - revenueFinishWorkingSession.Data.RealAmount);
                AdditionFeeIds = string.Join(",", revenueFinishWorkingSession.Data.AdditionFeeIds);
                MembershipTotalAccumulatePointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount);
                MembershipTotalAddPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipTotalPromotionPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);
                MembershipTotalAloPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount);

                // Đặt Cọc 
                DepositAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositAmount);
                DepositCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositCashAmount);
                DepositBankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositBankAmount);
                DepositTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositTransferAmount);
                // Trả Cọc 
                ReturnDepositAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositAmount);
                ReturnDepositCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositCashAmount);
                ReturnDepositTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositTransferAmount);

                // Phiếu thu 
                InTotalAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTotalAmountByAdditionFee);
                InCashAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InCashAmountByAdditionFee);
                InBankkAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InBankAmountByAdditionFee);
                InTransferAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTransferAmountByAdditionFee);


                // Phiếu Chi 
                OutTotalAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTotalAmountByAdditionFee);
                OutCashAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutCashAmountByAdditionFee);
                OutTransferAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTransferAmountByAdditionFee);
                TipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TipAmount);

                // Bán Hàng 
                CashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.CashAmount);
                TotalAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalAmount);
                BankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BankAmount);
                TransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TransferAmount);
                DebtAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DebtAmount);


                // Tổng Hợp 
                TotalAmountFinal = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalAmountFinal);
                TotalReceiptAmountFinal = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalReceiptAmountFinal);


                if (AmountList != null)
                {
                    AmountList.Clear();
                }
                else
                {
                    AmountList = new ObservableCollection<Money>();
                }
                if (revenueFinishWorkingSession.Data.CashValue != null && revenueFinishWorkingSession.Data.CashValue.Count > 0)
                {
                    revenueFinishWorkingSession.Data.CashValue.ForEach(AmountList.Add);
                }
                else
                {
                    GetDenominations();
                }

                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        private long employeeId;
        private string AdditionFeeIds = string.Empty;
        public async void getRevenueFinishWorkingSession(long Id, long BranchId, string EmployeeName)
        {
            DialogHostOpen = true;
            WorkingSessionClient client = new WorkingSessionClient(this, this, this);
            HistoryEndWorkingSessionItemResponse revenueFinishWorkingSession = await System.Threading.Tasks.Task.Run(() => client.HistoryEndWorkingSessionDetail(Id, BranchId));
            if (revenueFinishWorkingSession != null && revenueFinishWorkingSession.Status == (int)ResponseEnum.OK && revenueFinishWorkingSession != null)
            {
                WorkingSessionTitle = string.Format(MessageValue.MESSAGE_FROM_REVENUE_FINISH_WORKING_SESION_TITLE, EmployeeName);

                CheckInTitle = string.Format(MessageValue.MESSAGE_FROM_REVENUE_FINISH_CHECKIN_TITLE, revenueFinishWorkingSession.Data.OpenTime, revenueFinishWorkingSession.Data.CloseTime);
                OpenTime = string.IsNullOrEmpty(revenueFinishWorkingSession.Data.CloseTime) ? revenueFinishWorkingSession.Data.OpenTime : string.Format("{0} - {1}", revenueFinishWorkingSession.Data.OpenTime, revenueFinishWorkingSession.Data.CloseTime);
                employeeId = revenueFinishWorkingSession.Data.OpenEmployeeId;


                TotalAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInAmount);
                CashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.CashAmount);
                BankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BankAmount);
                TransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TransferAmount);
                DebtAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DebtAmount);
                TakeAwayOrder = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TakeAwayOrders);
                //    MembershipPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipAccumulatePointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount);
                MembershipAddPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipPromotionPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);
                MembershipAloPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount);

                TotalDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositAmount);
                BankDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositBankAmount);
                CashDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositCashAmount);
                TransferDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositTransferAmount);

                TotalReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositAmount);
                TransferReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositTransferAmount);
                CashReturnDeposit = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositCashAmount);


                PaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTotalAmountByAdditionFee);
                CashPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutCashAmountByAdditionFee);
                TransferPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTransferAmountByAdditionFee);
           //     MembershipPointUsedPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutMembershiPointUsedAmountByAdditionFee);
                
                ReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTotalAmountByAdditionFee);
                CashReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InCashAmountByAdditionFee);
                BankReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InBankAmountByAdditionFee);
                TransferReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTransferAmountByAdditionFee);

                BeforeAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BeforeCash);
                TotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInAmount);
                CashTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInCashAmount);
                BankTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInBankAmount);
                TransferTotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInTransferAmount);
                TotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutAmount);
                CashTotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutCashAmount);
                TransferTotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutTransferAmount);
                AfterAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.AfterCash);
                TotalSessionAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalCash);
                CurrentAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.RealAmount);
                DifferenceAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalReceiptAmountFinal - revenueFinishWorkingSession.Data.RealAmount);
                //   MembershipTotalPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                //  MembershipPointUsedTotalPaymentShipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipTotalAccumulatePointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount);
                MembershipTotalAddPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipTotalPromotionPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);
                MembershipTotalAloPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount);

                // Đặt Cọc 
                DepositAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositAmount);
                DepositCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositCashAmount);
                DepositBankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositBankAmount);
                DepositTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositTransferAmount);
                // Trả Cọc 
                ReturnDepositAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositAmount);
                ReturnDepositCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositCashAmount);
                ReturnDepositTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.ReturnDepositTransferAmount);

                // Phiếu thu 
                InTotalAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTotalAmountByAdditionFee);
                InCashAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InCashAmountByAdditionFee);
                InBankkAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InBankAmountByAdditionFee);
                InTransferAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InTransferAmountByAdditionFee);


                // Phiếu Chi 
                OutTotalAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTotalAmountByAdditionFee);
                OutCashAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutCashAmountByAdditionFee);
                OutTransferAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.OutTransferAmountByAdditionFee);
                TipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TipAmount);


                // Bán Hàng 
                CashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.CashAmount);
                TotalAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalAmount);
                BankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BankAmount);
                TransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TransferAmount);
                DebtAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DebtAmount);


                // Tổng Hợp 
                TotalAmountFinal = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalAmountFinal);
                TotalReceiptAmountFinal = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalReceiptAmountFinal);

                //AdditionFeeIds = string.Join(",", revenueFinishWorkingSession.Data.AdditionFeeIds);
                if (AmountList != null)
                {
                    AmountList.Clear();
                }
                else
                {
                    AmountList = new ObservableCollection<Money>();
                }
                if (revenueFinishWorkingSession.Data.CashValue != null && revenueFinishWorkingSession.Data.CashValue.Count > 0)
                {
                    revenueFinishWorkingSession.Data.CashValue.ForEach(AmountList.Add);
                }
                else
                {
                    GetDenominations();
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public RevenueFinishWorkingSessionViewModel()
        {
            if (currentUser != null)
            {
                getRevenueFinishWorkingSession();

                OrderViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    OrderListEndWorkingSessionWindow window = new OrderListEndWorkingSessionWindow();
                    window.DataContext = new OrderListEndWorkingSessionViewModel(Constants.CURRENT_SESSION, currentUser.BranchId);
                    window.ShowDialog();
                });
                BookingDepositViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    BookingListEndWorkingSessionWindow window = new BookingListEndWorkingSessionWindow();
                    window.DataContext = new BookingListEndWorkingSessionViewModel(Constants.CURRENT_SESSION);
                    window.ShowDialog();
                });
                BookingReturnDepositViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    ReturnBookingListEndWorkingSessionWindow window = new ReturnBookingListEndWorkingSessionWindow();
                    window.DataContext = new ReturnBookingListEndWorkingSessionViewModel(Constants.CURRENT_SESSION);
                    window.ShowDialog();
                });
                ReceiptViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Constants.CURRENT_SESSION, OpenTime, Constants.RECEIPT, AdditionFeeIds);
                    window.ShowDialog();
                });
                PaymentShipViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    Console.WriteLine("RevenFinishWorkSession"); 
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Constants.CURRENT_SESSION, OpenTime, Constants.PAYMENT, AdditionFeeIds);
                    window.ShowDialog();
                });
            }
        }
        public RevenueFinishWorkingSessionViewModel(long Id, long BranchId, string EmployeeName)
        {
            if (currentUser != null)
            {
                getRevenueFinishWorkingSession(Id, BranchId, EmployeeName);

                CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    p.Close();
                });
                OrderViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    OrderListEndWorkingSessionWindow window = new OrderListEndWorkingSessionWindow();
                    window.DataContext = new OrderListEndWorkingSessionViewModel(Id, BranchId);
                    window.ShowDialog();
                });
                BookingDepositViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    BookingListEndWorkingSessionWindow window = new BookingListEndWorkingSessionWindow();
                    window.DataContext = new BookingListEndWorkingSessionViewModel(Id);
                    window.ShowDialog();
                });
                BookingReturnDepositViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    ReturnBookingListEndWorkingSessionWindow window = new ReturnBookingListEndWorkingSessionWindow();
                    window.DataContext = new ReturnBookingListEndWorkingSessionViewModel(Id);
                    window.ShowDialog();
                });
                ReceiptViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Id, Constants.RECEIPT, BranchId, employeeId, AdditionFeeIds);
                    window.ShowDialog();
                });
                PaymentShipViewCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                {
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Id, Constants.PAYMENT, BranchId, employeeId, AdditionFeeIds);
                    window.ShowDialog();
                });
            }
        }

        #region Interface 
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
        public void LogError(Exception ex, string infoMessage)
        {
            //
        }
        #endregion

    }
}

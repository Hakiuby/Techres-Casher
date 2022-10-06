using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class EndWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        #region new by Phan Viet Ha 

        private long employeeId;
        private string AdditionFeeIds = string.Empty;
        private string _OpenTime;
        public string OpenTime
        {
            get { return _OpenTime; }
            set { _OpenTime = value; OnPropertyChanged("OpenTime"); }
        }

        private string _TotalCostFinal; 
        public string TotalCostFinal
        {
            get => _TotalCostFinal; 
            set
            {
                _TotalCostFinal = value;
                OnPropertyChanged("TotalCostFinal"); 
            }
        }

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
        public string InBankAmountByAdditionFee
        {
            get => _InBankAmountByAdditionFee; 
            set
            {
                _InBankAmountByAdditionFee = value;
                OnPropertyChanged("InBankAmountByAdditionFee"); 
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
        private string _TipAmount; 
        public string TipAmount
        {
            get => _TipAmount; 
            set
            {
                _TipAmount = value; 
                OnPropertyChanged("TipAmount"); 
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
        private string _TotalAmount; 
        public string TotalAmount
        {
            get => _TotalAmount; 
            set
            {
                _TotalAmount = value;
                OnPropertyChanged("TotalAmount"); 
            }
        }
        private string _BankAmount;
        public string BankAmount
        {
            get => _BankAmount; 
            set
            {
                _BankAmount = value;
                OnPropertyChanged("BankAmount"); 
            }
        }
        private string _TransferAmount; 
        public string TransferAmount
        {
            get => _TransferAmount; 
            set
            {
                _TransferAmount = value;
                OnPropertyChanged("TransferAmount"); 
            }
        }
        private string _DebtAmount; 
        public string DebtAmount
        {
            get => _DebtAmount; 
            set
            {
                _DebtAmount = value;
                OnPropertyChanged("DebtAmount"); 
            }
        }
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
        private string _CashAmount; 
        public string CashAmount
        {
            get => _CashAmount; 
            set
            {
                _CashAmount = value;
                OnPropertyChanged("CashAmount"); 
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
        public ICommand BookingDepositViewCommand { get; set; }
        public ICommand BookingReturnDepositViewCommand { get; set;  }
        public ICommand ReceiptViewCommand { get; set; }
        public ICommand PaymentShipViewCommand { get; set; }

        public ICommand OrderViewCommand { get; set; }







        #endregion



        private string _MembershipAddTotalPointUsedAmount;
        public string MembershipAddTotalPointUsedAmount
        {
            get
            {
                return _MembershipAddTotalPointUsedAmount;
            }
            set
            {
                _MembershipAddTotalPointUsedAmount = value;

                OnPropertyChanged("MembershipAddTotalPointUsedAmount");
            }
        }
        private string _AfterTotalAmount;
        public string AfterTotalAmount
        {
            get
            {
                return _AfterTotalAmount;
            }
            set
            {
                _AfterTotalAmount = value;

                OnPropertyChanged("AfterTotalAmount");
            }
        }

        private string _BeforeCash;
        public string BeforeCash
        {
            get
            {
                return _BeforeCash;
            }
            set
            {
                _BeforeCash = value;

                OnPropertyChanged("BeforeCash");
            }
        }
        private string _TotalCashAmount;
        public string TotalCashAmount
        {
            get
            {
                return _TotalCashAmount;
            }
            set
            {
                _TotalCashAmount = value;

                OnPropertyChanged("TotalCashAmount");
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


        private string _TotalTransferAmount;
        public string TotalTransferAmount
        {
            get
            {
                return _TotalTransferAmount;
            }
            set
            {
                _TotalTransferAmount = value;

                OnPropertyChanged("TotalTransferAmount");
            }
        }
        private string _CurrentAmountSystem;
        public string CurrentAmountSystem
        {
            get
            {
                return _CurrentAmountSystem;
            }
            set
            {
                _CurrentAmountSystem = value;

                OnPropertyChanged("CurrentAmountSystem");
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
        private string _TotalBankAmount;
        public string TotalBankAmount
        {
            get
            {
                return _TotalBankAmount;
            }
            set
            {
                _TotalBankAmount = value;

                OnPropertyChanged("TotalBankAmount");
            }
        }
        private string _TotalDebitBillAmount;
        public string TotalDebitBillAmount
        {
            get
            {
                return _TotalDebitBillAmount;
            }
            set
            {
                _TotalDebitBillAmount = value;

                OnPropertyChanged("TotalDebitBillAmount");
            }
        }
        private long _CountOrder;
        public long CountOrder
        {
            get
            {
                return _CountOrder;
            }
            set
            {
                _CountOrder = value;

                OnPropertyChanged("CountOrder");
            }
        }
        private string _MembershipAccumulateTotalPointUsedAmount;
        public string MembershipAccumulateTotalPointUsedAmount
        {
            get
            {
                return _MembershipAccumulateTotalPointUsedAmount;
            }
            set
            {
                _MembershipAccumulateTotalPointUsedAmount = value;

                OnPropertyChanged("MembershipAccumulateTotalPointUsedAmount");
            }
        }
        private string _MembershipAloTotalPointUsedAmount;
        public string MembershipAloTotalPointUsedAmount
        {
            get
            {
                return _MembershipAloTotalPointUsedAmount;
            }
            set
            {
                _MembershipAloTotalPointUsedAmount = value;

                OnPropertyChanged("MembershipAloTotalPointUsedAmount");
            }
        }
        private string _MembershipPromotionTotalPointUsedAmount;
        public string MembershipPromotionTotalPointUsedAmount
        {
            get
            {
                return _MembershipPromotionTotalPointUsedAmount;
            }
            set
            {
                _MembershipPromotionTotalPointUsedAmount = value;

                OnPropertyChanged("MembershipPromotionTotalPointUsedAmount");
            }
        }

        private string _TotalPaymentSlipAmount;
        public string TotalPaymentSlipAmount
        {
            get
            {
                return _TotalPaymentSlipAmount;
            }
            set
            {
                _TotalPaymentSlipAmount = value;

                OnPropertyChanged("TotalPaymentSlipAmount");
            }
        }
        private string _TotalCashPaymentSlipAmount;
        public string TotalCashPaymentSlipAmount
        {
            get
            {
                return _TotalCashPaymentSlipAmount;
            }
            set
            {
                _TotalCashPaymentSlipAmount = value;

                OnPropertyChanged("TotalCashPaymentSlipAmount");
            }
        }
        private string _TotalTransferPaymentSlipAmount;
        public string TotalTransferPaymentSlipAmount
        {
            get
            {
                return _TotalTransferPaymentSlipAmount;
            }
            set
            {
                _TotalTransferPaymentSlipAmount = value;

                OnPropertyChanged("TotalTransferPaymentSlipAmount");
            }
        }

        private string _TotalRevenue;
        public string TotalRevenue
        {
            get
            {
                return _TotalRevenue;
            }
            set
            {
                _TotalRevenue = value;

                OnPropertyChanged("TotalRevenue");
            }
        }
        private string _Note;
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;

                OnPropertyChanged("Note");
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
        private ObservableCollection<Money> _AmountList;
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

        public ICommand AddCommand { get; set; }
        public ICommand TextChangeQuantityCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand PrintCommand { get; set; }

       //public ICommand BookingReturnDepositViewCommand { get; set; }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);

        public RevenueFinishWorkingSessionResponse revenueFinishWorkingSession;
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
        
            WorkingSessionClient client = new WorkingSessionClient(this, this, this);
            revenueFinishWorkingSession = await System.Threading.Tasks.Task.Run(() => client.GetRevenueFinishWorkingSessionToDay());
            revenueFinishWorkingSession = client.GetRevenueFinishWorkingSessionToDay();
            if (revenueFinishWorkingSession != null)
            {
                // Helper 
                OpenTime = revenueFinishWorkingSession.Data.OpenTime;
                employeeId = revenueFinishWorkingSession.Data.OpenEmployeeId;

                TitleContent = string.Format(MessageValue.MESSAGE_FROM_END_WORKING_SESION_TITLE, currentUser.Name, revenueFinishWorkingSession.Data.BranchWorkingSessionName);
                TotalReceiptAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInAmount);
                TotalCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInCashAmount);
                TotalBankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInBankAmount);
                TotalTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalInTransferAmount);
                TotalDebitBillAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DebtAmount);
                MembershipAccumulateTotalPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount);
                MembershipAddTotalPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPointUsedAmount);
                MembershipPromotionTotalPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);
                MembershipAloTotalPointUsedAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount);

                TotalPaymentSlipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutAmount);
                TotalCashPaymentSlipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutCashAmount);
                TotalTransferPaymentSlipAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalOutTransferAmount);

                TotalRevenue = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.AfterCash);

                BeforeCash = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.BeforeCash);
                AfterTotalAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.AfterCash);
                CurrentAmount = Utils.Utils.FormatMoney(0);
                DifferenceAmount = Utils.Utils.FormatMoney(0);
                CountOrder = revenueFinishWorkingSession.Data.NumberOrders;
                CurrentAmountSystem = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.AfterCash - revenueFinishWorkingSession.Data.BeforeCash);

                #region new by PHAN VIET HA 

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
                InBankAmountByAdditionFee = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.InBankAmountByAdditionFee);
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
                TotalCostFinal = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.TotalCostFinal); 
                // Helper 
                AdditionFeeIds = string.Join(",", revenueFinishWorkingSession.Data.AdditionFeeIds);

                #endregion
            }
        }
        /*
        public async void getRevenueFinishWorkingSession(long Id, long BranchId, string EmployeeName)
        {
            WorkingSessionClient client = new WorkingSessionClient(this, this, this);
            HistoryEndWorkingSessionItemResponse revenueFinishWorkingSession = await System.Threading.Tasks.Task.Run(() => client.HistoryEndWorkingSessionDetail(Id, BranchId));
            if (revenueFinishWorkingSession != null && revenueFinishWorkingSession.Status == (int)ResponseEnum.OK && revenueFinishWorkingSession != null)
            {
                OpenTime = string.IsNullOrEmpty(revenueFinishWorkingSession.Data.CloseTime) ? revenueFinishWorkingSession.Data.OpenTime : string.Format("{0} - {1}", revenueFinishWorkingSession.Data.OpenTime, revenueFinishWorkingSession.Data.CloseTime);
                employeeId = revenueFinishWorkingSession.Data.OpenEmployeeId;

                DepositAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositAmount);
                DepositCashAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositCashAmount);
                DepositBankAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositBankAmount);
                DepositTransferAmount = Utils.Utils.FormatMoney(revenueFinishWorkingSession.Data.DepositTransferAmount);

                // Helper 
                OpenTime = string.IsNullOrEmpty(revenueFinishWorkingSession.Data.CloseTime) ? revenueFinishWorkingSession.Data.OpenTime : string.Format("{0} - {1}", revenueFinishWorkingSession.Data.OpenTime, revenueFinishWorkingSession.Data.CloseTime);
                AdditionFeeIds = string.Join(",", revenueFinishWorkingSession.Data.AdditionFeeIds);
            }
        }
        */
        public bool isSuccess;
        public EndWorkingSessionViewModel(bool isLogin)
        {
            if (currentUser != null)
            {
                getRevenueFinishWorkingSession();
                GetDenominations();

                OrderViewCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                    OrderListEndWorkingSessionWindow window = new OrderListEndWorkingSessionWindow();
                    window.DataContext = new OrderListEndWorkingSessionViewModel(Constants.CURRENT_SESSION, currentUser.BranchId);
                    window.ShowDialog();
                }); 

                BookingDepositViewCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                    Console.WriteLine("1235468789");
                    BookingListEndWorkingSessionWindow window = new BookingListEndWorkingSessionWindow();
                    window.DataContext = new BookingListEndWorkingSessionViewModel(Constants.CURRENT_SESSION);
                    window.ShowDialog();

                });

                ReceiptViewCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Constants.CURRENT_SESSION, OpenTime, Constants.RECEIPT, AdditionFeeIds);
                    window.ShowDialog();
                });

                BookingReturnDepositViewCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ReturnBookingListEndWorkingSessionWindow window = new ReturnBookingListEndWorkingSessionWindow();
                    window.DataContext = new ReturnBookingListEndWorkingSessionViewModel(Constants.CURRENT_SESSION);
                    window.ShowDialog();
                });

                PaymentShipViewCommand = new RelayCommand<Window>(p => { return true; }, p => {
                    Console.WriteLine("EndWorkingSesion"); 
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Constants.CURRENT_SESSION, OpenTime, Constants.PAYMENT, AdditionFeeIds);
                    window.ShowDialog();
                }); 

                TextChangeQuantityCommand = new RelayCommand<Money>((p) => { return true; }, p =>
                {
                    if (p != null)
                    {
                        int index = AmountList.IndexOf(p);
                        AmountList.RemoveAt(index);
                        AmountList.Insert(index, p);
                        CurrentAmount = Utils.Utils.FormatMoney(AmountList.Sum(x => x.Amount));
                        DifferenceAmount = Utils.Utils.FormatMoney(decimal.Parse(TotalReceiptAmountFinal) - decimal.Parse(CurrentAmount));
                    }

                });
                AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    if (revenueFinishWorkingSession != null)
                    {
                        List<Money> money = new List<Money>();
                        AmountList.ForEach(money.Add);
                        decimal realAmount = decimal.Parse(CurrentAmount.Trim(','));
                        RevenueFinishWorkingSessionWrapper revenueFinishWorkingSessionWrapper = new RevenueFinishWorkingSessionWrapper(revenueFinishWorkingSession.Data.BeforeCash,
                        revenueFinishWorkingSession.Data.AfterCash,
                        revenueFinishWorkingSession.Data.TotalAmount,
                        revenueFinishWorkingSession.Data.CashAmount,
                        revenueFinishWorkingSession.Data.BankAmount,
                        revenueFinishWorkingSession.Data.TransferAmount,
                        revenueFinishWorkingSession.Data.DebtAmount,
                        realAmount,
                        revenueFinishWorkingSession.Data.NumberOrders,
                        money,
                        revenueFinishWorkingSession.Data.AdditionFeeIds,
                        revenueFinishWorkingSession.Data.OutTotalAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.TipAmount,
                        revenueFinishWorkingSession.Data.InTotalAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.InCashAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.OutCashAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.DepositAmount,
                        revenueFinishWorkingSession.Data.ReturnDepositAmount,
                        revenueFinishWorkingSession.Data.OutBankAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.OutTransferAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.InBankAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.InTransferAmountByAdditionFee,
                        revenueFinishWorkingSession.Data.DepositBankAmount,
                        revenueFinishWorkingSession.Data.DepositTransferAmount,
                        revenueFinishWorkingSession.Data.DepositCashAmount,
                        revenueFinishWorkingSession.Data.ReturnDepositCashAmount,
                        revenueFinishWorkingSession.Data.ReturnDepositBankAmount,
                        revenueFinishWorkingSession.Data.ReturnDepositTransferAmount,
                        revenueFinishWorkingSession.Data.MembershipPointUsedAmount,
                        revenueFinishWorkingSession.Data.MembershipAloPointUsedAmount,
                        revenueFinishWorkingSession.Data.MembershipAccumulatePointUsedAmount,
                        revenueFinishWorkingSession.Data.MembershipPromotionPointUsedAmount);

                        WorkingSessionClient client = new WorkingSessionClient(this, this, this);
                        BaseResponse response = client.EndWorkingSession(revenueFinishWorkingSessionWrapper);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            if (isLogin)
                            {
                                isSuccess = true;
                                p.Close();
                                OpenWorkingSessionWindow openWorkingSessionWindow = new OpenWorkingSessionWindow();
                                openWorkingSessionWindow.DataContext = new OpenWorkingSessionViewModel(realAmount);
                                openWorkingSessionWindow.ShowDialog();
                                var open = openWorkingSessionWindow.DataContext as OpenWorkingSessionViewModel;
                                if (open.isSuccess)
                                {
                                    isSuccess = true;
                                }
                                else
                                {
                                    isSuccess = false; 
                                }
                            }
                            else
                            {
                                isSuccess = true;
                                p.Close();
                            }

                        }
                    }

                });
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    isSuccess = false;
                    p.Close();
                });
                PrintCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    //Console.WriteLine("1213212312312312312312312312313"); 
                    DeviceClient deviceClient = new DeviceClient();
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    DeviceConfigWrapper device = deviceClient.RealConfigs();
                    if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                    {
                        List<Money> amountList = new List<Money>();
                        if (AmountList.Count > 0)
                        {
                            AmountList.ForEach(amountList.Add);
                            pt.PrintViewTallying(amountList, TotalAmountFinal, currentUser.Name, device.CasherPrinter, currentBranch.Name, BeforeCash, TotalReceiptAmountFinal, device.CasherSize, DifferenceAmount, OutTotalAmountByAdditionFee,TipAmount);

                        }
                    }
                });

            }
        }

        /*b  
        public EndWorkingSessionViewModel(long Id, long BranchId, string EmployeeName)
        {

            if (currentUser != null)
            {

                //getRevenueFinishWorkingSession(Id, BranchId, EmployeeName); 

                OrderViewCommand = new RelayCommand<Window>(p => { return true; }, p => {
                    OrderListEndWorkingSessionWindow window = new OrderListEndWorkingSessionWindow();
                    window.DataContext = new OrderListEndWorkingSessionViewModel(Id, BranchId);
                    window.ShowDialog();
                });
                BookingDepositViewCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    BookingListEndWorkingSessionWindow window = new BookingListEndWorkingSessionWindow();
                    window.DataContext = new BookingListEndWorkingSessionViewModel(Id);
                    window.ShowDialog();
                });
                BookingReturnDepositViewCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                {
                    ReturnBookingListEndWorkingSessionWindow window = new ReturnBookingListEndWorkingSessionWindow();
                    window.DataContext = new ReturnBookingListEndWorkingSessionViewModel(Id);
                    window.ShowDialog();
                });

                PaymentShipViewCommand = new RelayCommand<Window>(p => { return true; }, p => {
                    PaymentShipEndWorkingSessionWindow window = new PaymentShipEndWorkingSessionWindow();
                    window.DataContext = new PaymentShipEndWorkingSessionViewModel(Id, Constants.PAYMENT, BranchId, employeeId, AdditionFeeIds);
                    window.ShowDialog();
                });

                //PrintCommand = new RelayCommand<Window>((p) => { return true; }, p =>
                //{
                //    Console.WriteLine("1213212312312312312312312312313");
                //    DeviceClient deviceClient = new DeviceClient();
                //    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                //    DeviceConfigWrapper device = deviceClient.RealConfigs();
                //    if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
                //    {
                //        List<Money> amountList = new List<Money>();
                //        if (AmountList.Count > 0)
                //        {
                //            AmountList.ForEach(amountList.Add);
                //            pt.PrintViewTallying(amountList, AfterTotalAmount, currentUser.Name, device.CasherPrinter, currentBranch.Name, BeforeCash, CurrentAmountSystem, device.CasherSize);

                //        }
                //    }
                //});
            }
        }
        */

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


    }
}

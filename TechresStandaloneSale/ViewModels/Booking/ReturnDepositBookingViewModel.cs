using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Booking
{
    public class ReturnDepositBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _BookingCode;
        public string BookingCode
        {
            get
            {
                return _BookingCode;
            }
            set
            {
                _BookingCode = value;
                OnPropertyChanged("BookingCode");
            }
        }

        private string _Tables;
        public string Tables
        {
            get
            {
                return _Tables;
            }
            set
            {
                _Tables = value;
                OnPropertyChanged("Tables");
            }
        }
        private string _CustomerName;
        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
                OnPropertyChanged("CustomerName");
            }
        }

        private string _CustomerPhone;
        public string CustomerPhone
        {
            get
            {
                return _CustomerPhone;
            }
            set
            {
                _CustomerPhone = value;
                OnPropertyChanged("CustomerPhone");
            }
        }

        private string _Deposit;
        public string Deposit
        {
            get
            {
                return _Deposit;
            }
            set
            {
                _Deposit = value;
                OnPropertyChanged("Deposit");
            }
        }

        private string _BookingTypeString;
        public string BookingTypeString
        {
            get
            {
                return _BookingTypeString;
            }
            set
            {
                _BookingTypeString = value;
                OnPropertyChanged("BookingTypeString");
            }
        }
        private string _EmployeeName;
        public string EmployeeName
        {
            get
            {
                return _EmployeeName;
            }
            set
            {
                _EmployeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }
        private string _Status;
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        private string _ReturnAmount;
        public string ReturnAmount
        {
            get
            {
                return _ReturnAmount;
            }
            set
            {
                _ReturnAmount = value;
                OnPropertyChanged("ReturnAmount");
            }
        }
        private Visibility _EmployeeVisibility;
        public Visibility EmployeeVisibility
        {
            get
            {
                return _EmployeeVisibility;
            }
            set
            {
                _EmployeeVisibility = value;
                OnPropertyChanged("EmployeeVisibility");
            }
        }
        private string _ReceviceDepositMoney;
        public string ReceviceDepositMoney
        {
            get
            {
                return _ReceviceDepositMoney;
            }
            set
            {
                _ReceviceDepositMoney = value;
                OnPropertyChanged("ReceviceDepositMoney");
            }
        }
        private string _ReturnDepositMoney;
        public string ReturnDepositMoney
        {
            get
            {
                return _ReturnDepositMoney;
            }
            set
            {
                _ReturnDepositMoney = value;
                OnPropertyChanged("ReturnDepositMoney");
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
        private ObservableCollection<BasicModel> _PaymentMethodList;
        public ObservableCollection<BasicModel> PaymentMethodList
        {
            get
            {
                return _PaymentMethodList;
            }
            set
            {
                _PaymentMethodList = value;
                OnPropertyChanged("PaymentMethodList");
            }
        }
        private BasicModel _PaymentMethod;
        public BasicModel PaymentMethod
        {
            get
            {
                return _PaymentMethod;
            }
            set
            {
                _PaymentMethod = value;
                OnPropertyChanged("PaymentMethod");
            }
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
            PaymentMethod = PaymentMethodList[0];
        }
        public bool isSuccess = false;
        public ICommand SaveCommand { get; set; }
        public ICommand SaveAndPrintCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Models.Booking CurrentBooking;
        public ReturnDepositBookingViewModel(Models.Booking booking)
        {
            GetDetailBooking(booking);
            GetPaymentMethodList();
            SaveCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (string.IsNullOrEmpty(ReturnAmount))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_EMPTY_AMOUNT);
                }
                else
                {
                    decimal depost = decimal.Parse(ReturnAmount.Trim(','));
                    BookingClient bookingClient = new BookingClient(this, this, this);
                    BookingResponse response = bookingClient.ReturnDeposit(new ReturnDepositWrapper(booking.Branch.Id,PaymentMethod.Value), booking.Id);
                    if (response != null)
                    {
                        if (response.Status == (int)ResponseEnum.OK)
                        {
                            isSuccess = true;
                            CurrentBooking = response.Data;
                            p.Close();
                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                        }
                    }
                }
            });
            SaveAndPrintCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (string.IsNullOrEmpty(ReturnAmount))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_EMPTY_AMOUNT);
                }
                else
                {
                    decimal depost = decimal.Parse(ReturnAmount.Trim(','));
                    BookingClient bookingClient = new BookingClient(this, this, this);
                    BaseResponse response = bookingClient.ReturnDeposit(new ReturnDepositWrapper(booking.Branch.Id, PaymentMethod.Value), booking.Id);
                    if (response != null)
                    {
                        if (response.Status == (int)ResponseEnum.OK)
                        {
                            booking.ReturnDepositAmount = depost;
                            PrintBill(booking);
                            isSuccess = true;
                            p.Close();
                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                        }
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public void PrintBill(Models.Booking booking)
        {
            DeviceClient deviceClient = new DeviceClient();
            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
            DeviceConfigWrapper device = deviceClient.RealConfigs();
            if (device != null && device.IsCasher && !string.IsNullOrEmpty(device.CasherPrinter))
            {
                Branch currentBranch = (Branch)Utils.Utils.GetCacheValue(Constants.CURRENT_BRANCH);
                pt.PrintBooking(MessageValue.MESSAGE_FROM_BOOKING_TITLE, booking, device.CasherPrinter, device.CasherSize, currentBranch,true);
            }
            else
            {
                NotificationMessage.Warning("Vui lòng thiết lập máy in trước khi in!");
            }
        }
        public void GetDetailBooking(Models.Booking booking)
        {
            BookingCode = booking.BookingCode;
            Tables = booking.TableString;
            CustomerName = booking.CustomerName;
            CustomerPhone = booking.CustomerPhone;
            Deposit = booking.DepositString;
            BookingTypeString = booking.BookingTypeName;
            if (booking.BookingType == (int)BookingTypeEnum.EMPLOYEE)
            {
                EmployeeName = booking.Employee.Name;
                EmployeeVisibility = Visibility.Visible;
            }
            else
            {
                EmployeeVisibility = Visibility.Collapsed;
            }
            ReceviceDepositMoney = Utils.Utils.FormatMoney(booking.DepositAmount);
            ReturnDepositMoney = Utils.Utils.FormatMoney(booking.ReturnDepositAmount);
            TotalAmount = Utils.Utils.FormatMoney(booking.TotalAmount);
            ReturnAmount = Utils.Utils.FormatMoney(booking.DepositAmount);
            Status = booking.BookingStatusName;
        }
        public void LogError(Exception ex, string infoMessage)
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

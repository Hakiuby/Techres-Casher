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
    public class ReceviceDepositBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
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
        public long AmountTmp
        {
            set
            {
                AmountTmp = long.Parse(Amount); 
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

        private string _Amount;
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
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
        public bool isSuccess = false;
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Models.Booking CurrentBooking;
        public ReceviceDepositBookingViewModel(Models.Booking booking)
        {
            GetDetailBooking(booking);
            GetPaymentMethodList();
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (string.IsNullOrEmpty(Amount))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_EMPTY_AMOUNT);
                }
                else if (decimal.Parse(Amount) >= 1000 && decimal.Parse(Amount) <= 1000000000)
                {
                    decimal depost = decimal.Parse(Amount.Trim(','));
                    BookingClient bookingClient = new BookingClient(this, this, this);
                    BookingResponse response = bookingClient.ReceiveDeposit(new ReceiveDepositWrapper(booking.Id, booking.Branch.Id, depost, PaymentMethod.Value));
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
                else
                {
                    {
                        NotificationMessage.Warning(MessageValue.MRSSAGE_WRONG_INPUT);
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

        }
        public void GetPaymentMethodList()
        {
            if(PaymentMethodList == null)
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
        public void GetDetailBooking(Models.Booking booking)
        {
            BookingCode = booking.BookingCode;
            Tables = booking.TableString;
            CustomerName = booking.CustomerName;
            CustomerPhone = booking.CustomerPhone;
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
            Amount ="0";
            Status = booking.BookingStatusName;
        }
        public void LogError(Exception ex, string infoMessage)
        {
            WriteLog.logs(ex.Message);
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

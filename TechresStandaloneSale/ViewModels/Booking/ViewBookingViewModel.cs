using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Booking
{
    public class ViewBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _ContentView;
        public string ContentView
        {
            get
            {
                return _ContentView;
            }
            set
            {
                _ContentView = value;
                OnPropertyChanged("ContentView");
            }
        }
        public ICommand CloseCommand { get; set; }

        private string _BookingTime;
        public string BookingTime
        {
            get
            {
                return _BookingTime;
            }
            set
            {
                _BookingTime = value;
                OnPropertyChanged("BookingTime");
            }
        }

        private long _SlotNumber;
        public long SlotNumber
        {
            get
            {
                return _SlotNumber;
            }
            set
            {
                _SlotNumber = value;
                OnPropertyChanged("SlotNumber");
            }
        }
        private string _CreatedAt;
        public string CreatedAt
        {
            get
            {
                return _CreatedAt;
            }
            set
            {
                _CreatedAt = value;
                OnPropertyChanged("CreatedAt");
            }
        }
        private string _PhoneCustomer;
        public string PhoneCustomer
        {
            get
            {
                return _PhoneCustomer;
            }
            set
            {
                _PhoneCustomer = value;
                OnPropertyChanged("PhoneCustomer");
            }
        }
        private string _Area;
        public string Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
                OnPropertyChanged("Area");
            }
        }
        private string _Table;
        public string Table
        {
            get
            {
                return _Table;
            }
            set
            {
                _Table = value;
                OnPropertyChanged("Table");
            }
        }
        private string _NameCustomer;
        public string NameCustomer
        {
            get
            {
                return _NameCustomer;
            }
            set
            {
                _NameCustomer = value;
                OnPropertyChanged("NameCustomer");
            }
        }

        private ObservableCollection<FoodRequest> _RequestFoodList = new ObservableCollection<FoodRequest>();
        public ObservableCollection<FoodRequest> RequestFoodList
        {
            get
            {
                return _RequestFoodList;
            }
            set
            {
                _RequestFoodList = value;
                OnPropertyChanged("RequestFoodList");
            }
        }
        private string _OtherRequest;
        public string OtherRequest
        {
            get
            {
                return _OtherRequest;
            }
            set
            {
                _OtherRequest = value;
                OnPropertyChanged("OtherRequest");
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
        private string _DepositString;
        public string DepositString
        {
            get
            {
                return _DepositString;
            }
            set
            {
                _DepositString = value;
                OnPropertyChanged("DepositString");
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

        private string _ReturnDepositString;
        public string ReturnDepositString
        {
            get
            {
                return _ReturnDepositString;
            }
            set
            {
                _ReturnDepositString = value;
                OnPropertyChanged("ReturnDepositString");
            }
        }
        private string _ReturnDepositTime;
        public string ReturnDepositTime
        {
            get
            {
                return _ReturnDepositTime;
            }
            set
            {
                _ReturnDepositTime = value;
                OnPropertyChanged("ReturnDepositTime");
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
        private Visibility _ReturnDepositVisibility;
        public Visibility ReturnDepositVisibility
        {
            get
            {
                return _ReturnDepositVisibility;
            }
            set
            {
                _ReturnDepositVisibility = value;
                OnPropertyChanged("ReturnDepositVisibility");
            }
        }
        private long _FoodQuantity;
        public long FoodQuantity { get => _FoodQuantity; set { _FoodQuantity = value; OnPropertyChanged("FoodQuantity"); } }

        private float _TotalQuantity;
        public float TotalQuantity { get => _TotalQuantity; set { _TotalQuantity = value; OnPropertyChanged("TotalQuantity"); } }
        public ViewBookingViewModel(Models.Booking booking)
        {
            ContentView = "XEM CHI TIẾT ĐƠN ĐẶT BÀN - " + booking.BookingCode;
            BookingTime = booking.BookingTime;
            SlotNumber = booking.NumberSlot;
            CreatedAt = booking.CreatedAt;
            BookingTypeString = booking.BookingTypeName;
            Status = booking.BookingStatusName;
            NameCustomer = booking.CustomerName;
            PhoneCustomer = booking.CustomerPhone;
            Table = booking.TableFormatString;
            Area = booking.Tables!= null && booking.Tables.Count > 0 ? booking.Tables[0].AreaNameBooking : "";
            booking.Foods.ForEach(RequestFoodList.Add);
            OtherRequest = booking.OrtherRequirements;
            Note = booking.Note;
            DepositString = booking.DepositString;
            TotalAmount = booking.TotalAmountString;
            FoodQuantity = booking.Foods.Count();
            TotalQuantity = booking.Foods.Sum(x => x.Quantity);
            if (booking.BookingType == (int)BookingTypeEnum.EMPLOYEE)
            {
                EmployeeVisibility = Visibility.Visible;
                EmployeeName = booking.Employee.Name;
            }
            else
            {
                EmployeeVisibility = Visibility.Hidden;
            }
            if (booking.ReturnDepositAmount > 0)
            {
                ReturnDepositVisibility = Visibility.Visible;
                ReturnDepositString = booking.ReturnDepositString;
                ReturnDepositTime = booking.ReturnDepositTime;
            }
            else
            {
                ReturnDepositVisibility = Visibility.Collapsed;
            }
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

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

using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class OrderDetailViewViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        public ICommand CloseCommand { get; set; }
        public string _OrderDetailStatusName; 
        public string OrderDetailStatusName
        {
            get => _OrderDetailStatusName; 
            set
            {
                _OrderDetailStatusName = value;
                OnPropertyChanged("OrderDetailStatusName"); 
            }
        }
        public string _OrderDetailCode;
        public string OrderDetailCode
        {
            get
            {
                return _OrderDetailCode;
            }
            set
            {
                _OrderDetailCode = value;
                OnPropertyChanged("OrderDetailCode");
            }
        }
        public string _OrderCode;
        public string OrderCode
        {
            get
            {
                return _OrderCode;
            }
            set
            {
                _OrderCode = value;
                OnPropertyChanged("OrderCode");
            }
        }
        public string _TableName;
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
                OnPropertyChanged("TableName");
            }
        }
        public string _FoodName;
        public string FoodName
        {
            get
            {
                return _FoodName;
            }
            set
            {
                _FoodName = value;
                OnPropertyChanged("FoodName");
            }
        }
        public string _Quantity;
        public string Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public string _UnitPriceString;
        public string UnitPriceString
        {
            get
            {
                return _UnitPriceString;
            }
            set
            {
                _UnitPriceString = value;
                OnPropertyChanged("UnitPriceString");
            }
        }
        public string _TotalQuantity;
        public string TotalQuantity
        {
            get
            {
                return _TotalQuantity;
            }
            set
            {
                _TotalQuantity = value;
                OnPropertyChanged("TotalQuantity");
            }
        }
        public string _ReturnQuantity;
        public string ReturnQuantity
        {
            get
            {
                return _ReturnQuantity;
            }
            set
            {
                _ReturnQuantity = value;
                OnPropertyChanged("ReturnQuantity");
            }
        }
        public string _TotalPrice;
        public string TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
        public string _EmployeeName;
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
        public string _CreateAt;
        public string CreateAt
        {
            get
            {
                return _CreateAt;
            }
            set
            {
                _CreateAt = value;
                OnPropertyChanged("CreateAt");
            }
        }

        public string _Status;
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
        public string _CancelReason;
        public string CancelReason
        {
            get
            {
                return _CancelReason;
            }
            set
            {
                _CancelReason = value;
                OnPropertyChanged("CancelReason");
            }
        }
        public string _Note;
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
        public string _HistoryLog;
        public string HistoryLog
        {
            get
            {
                return _HistoryLog;
            }
            set
            {
                _HistoryLog = value;
                OnPropertyChanged("HistoryLog");
            }
        }

        public Visibility _TotalQuantityVisibility;
        public Visibility TotalQuantityVisibility
        {
            get
            {
                return _TotalQuantityVisibility;
            }
            set
            {
                _TotalQuantityVisibility = value;
                OnPropertyChanged("TotalQuantityVisibility");
            }
        }

        public Visibility _ReturnQuantityVisibility;
        public Visibility ReturnQuantityVisibility
        {
            get
            {
                return _ReturnQuantityVisibility;
            }
            set
            {
                _ReturnQuantityVisibility = value;
                OnPropertyChanged("ReturnQuantityVisibility");
            }
        }

        public Visibility _CancelReasonVisibility;
        public Visibility CancelReasonVisibility
        {
            get
            {
                return _CancelReasonVisibility;
            }
            set
            {
                _CancelReasonVisibility = value;
                OnPropertyChanged("CancelReasonVisibility");
            }
        }
        public OrderDetailViewViewModel(long orderId, int isExtraCharge)
        {
            
            OrderDetailsClient orderDetailsClient = new OrderDetailsClient(this, this, this);
            SingleOrderDetailResponse response;
            if (isExtraCharge== Constants.NOT_STATUS)
            {
                response = orderDetailsClient.GetOrderDetail(orderId);
            }
            else
            {
                response = orderDetailsClient.GetOrderDetailExtraCharge(orderId);
            }
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                OrderDetailCode = string.Format("#{0}", response.Data.Id);
                OrderCode = string.Format("#{0}", response.Data.OrderId);
                if(response.Data.TableName == "")
                {
                    TableName = "MV";
                }
                else
                {
                    TableName = response.Data.TableName;
                }
                FoodName = response.Data.FoodName;
                Quantity = response.Data.Quantity.ToString();
                UnitPriceString = response.Data.UnitPriceFormat;
                TotalQuantity = response.Data.TotalQuantityForDrink.ToString();
                ReturnQuantity = response.Data.RefundQuantityDrink;
                TotalPrice = response.Data.TotalPriceFormat;
                EmployeeName = response.Data.EmployeeName;
                CreateAt = response.Data.CreatedAt;
                Status = response.Data.OrderDetailStatusString;
                CancelReason = response.Data.CancelReason;
                Note = response.Data.Note;
                HistoryLog = response.Data.HistoryLog;
                //OrderDetailStatusName = response.Data.OrderDetailStatusName; 
                if (response.Data.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL)
                {
                    CancelReasonVisibility = Visibility.Visible;
                }
                else
                {
                    CancelReasonVisibility = Visibility.Collapsed;
                }
                if (response.Data.CategoryType == (long)CategoryTypeEnum.DRINK || response.Data.CategoryType == (long)CategoryTypeEnum.OTHER)
                {
                    TotalQuantityVisibility = Visibility.Visible;
                    ReturnQuantityVisibility = Visibility.Visible;
                }
                else
                {
                    TotalQuantityVisibility = Visibility.Collapsed;
                    ReturnQuantityVisibility = Visibility.Collapsed;
                }

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

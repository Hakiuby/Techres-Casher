using DevExpress.Mvvm.Native;
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
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class OrderListEndWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _TotalAmount;
        public string TotalAmountString
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;

                OnPropertyChanged("TotalAmountString");
            }
        }
        private bool _DialogHostOpen;
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
        public ICommand CloseCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand GetOrderCommand { get; set; }
        private ObservableCollection<Order> itemsOrder = new ObservableCollection<Order>();
        public ObservableCollection<Order> ItemsOrder
        {
            get
            {
                return itemsOrder;
            }
            set
            {
                itemsOrder = value;
                OnPropertyChanged("ItemsOrder");
            }
        }
        private List<Order> OrderAll = new List<Order>();
        private ObservableCollection<BasicModel> _PaymentMethodList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> PaymentMethodList { get => _PaymentMethodList; set { _PaymentMethodList = value; OnPropertyChanged("PaymentMethodList"); } }
        private BasicModel _PaymentMethodItem;
        public BasicModel PaymentMethodItem { get => _PaymentMethodItem; set { _PaymentMethodItem = value; OnPropertyChanged("PaymentMethodItem"); } }

        public void GetPaymentMethod()
        {
            if (PaymentMethodList == null)
            {
                PaymentMethodList = new ObservableCollection<BasicModel>();
            }
            else
            {
                PaymentMethodList.Clear();
            }
            PaymentMethodList.Add(new BasicModel(-1, MessageValue.MESSAGE_ALL));
            PaymentMethodList.Add(new BasicModel((int)PaymentMethodEnum.CASH, MessageValue.MESSAGE_FROM_END_WORKING_SESION_CASH_MONEY));
            PaymentMethodList.Add(new BasicModel((int)PaymentMethodEnum.BANK, MessageValue.MESSAGE_FROM_END_WORKING_SESION_BANK_MONEY));
            PaymentMethodList.Add(new BasicModel((int)PaymentMethodEnum.TRANSFER, MessageValue.MESSAGE_FROM_END_WORKING_SESION_TRANSFER_ORDER));
            PaymentMethodList.Add(new BasicModel((int)PaymentMethodEnum.MEMBERSHIP_CARD, MessageValue.MESSAGE_FROM_END_WORKING_SESION_MEMBER_POINT));
            PaymentMethodList.Add(new BasicModel((int)PaymentMethodEnum.DEBIT, MessageValue.MESSAGE_FROM_END_WORKING_SESION_DEBIT_MONEY));
        }
        public async void GetDetail(long orderSessionId)
        {

            ReportClient reportClient = new ReportClient(this, this, this);
            OrderResponses orders = await System.Threading.Tasks.Task.Run(() => reportClient.OrderHistory(orderSessionId));
            if (orders != null && orders.Status == (int)ResponseEnum.OK && orders.Data != null)
            {
                if (ItemsOrder != null)
                {
                    ItemsOrder.Clear();
                }
                else
                {
                    ItemsOrder = new ObservableCollection<Order>();
                }
                OrderAll = orders.Data;
                orders.Data.ForEach(ItemsOrder.Add);

                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        private long paymentMethodId = -1;
        public OrderListEndWorkingSessionViewModel(long OrderSessionId, long branchId)
        {
            DialogHostOpen = true;
            GetPaymentMethod();
            PaymentMethodItem = PaymentMethodList.Where(x => x.Value == -1).FirstOrDefault();
            paymentMethodId = PaymentMethodItem == null ? -1 : PaymentMethodItem.Value;
            GetDetail(OrderSessionId);
            SelectionChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
         {
             if (PaymentMethodItem != null)
             {
                 if (paymentMethodId != PaymentMethodItem.Value)
                 {
                     if (ItemsOrder != null)
                     {
                         ItemsOrder.Clear();
                     }
                     else
                     {
                         ItemsOrder = new ObservableCollection<Order>();
                     }
                     paymentMethodId = PaymentMethodItem.Value;
                     if (PaymentMethodItem.Value == -1)
                     {
                         OrderAll.ForEach(ItemsOrder.Add);
                     }
                     else if (PaymentMethodItem.Value == (int)PaymentMethodEnum.CASH)
                     {
                         OrderAll.Where(x => x.CashAmount > 0).ForEach(ItemsOrder.Add);
                     }
                     else if (PaymentMethodItem.Value == (int)PaymentMethodEnum.BANK)
                     {
                         OrderAll.Where(x => x.BankAmount > 0).ForEach(ItemsOrder.Add);
                     }
                     else if (PaymentMethodItem.Value == (int)PaymentMethodEnum.TRANSFER)
                     {
                         OrderAll.Where(x => x.TransferAmount > 0).ForEach(ItemsOrder.Add);
                     }
                     else if (PaymentMethodItem.Value == (int)PaymentMethodEnum.DEBIT)
                     {
                         OrderAll.Where(x => x.OrderStatus == (int)OrderStatusEnum.DEBIT).ForEach(ItemsOrder.Add);
                     }
                     else if (PaymentMethodItem.Value == (int)PaymentMethodEnum.MEMBERSHIP_CARD)
                     {
                         OrderAll.Where(x => x.MembershipPointUsedAmount > 0).ForEach(ItemsOrder.Add);
                     }
                 }
             }
         });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            GetOrderCommand = new RelayCommand<Order>((p) => { return true; }, (p) =>
            {
                OrderViewWindow popup = new OrderViewWindow();
                popup.DataContext = new OrderViewViewModel(p.Id, branchId);
                popup.ShowDialog();
            });
        }
        public void LogError(Exception ex, string infoMessage)
        {
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
    }
}

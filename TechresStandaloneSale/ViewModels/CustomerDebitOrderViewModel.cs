using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public class CustomerDebitOrderViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand AddCustomerCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ByCusByPhone { get; set; }
        public ICommand EnterTextBoxCustomerCommand { get; set; }
        private ObservableCollection<Customer> _CustomerList = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged("CustomerList"); } }

        private string _CodeOrder;
        public string CodeOrder { get => _CodeOrder; set { _CodeOrder = value; OnPropertyChanged("CodeOrder"); } }


        private string _TotalAmount;
        public string TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }


        private Customer _CustomerItem;
        public Customer CustomerItem { get => _CustomerItem; set { _CustomerItem = value; OnPropertyChanged("CustomerItem"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private int _CustomerIndex;
        public int CustomerIndex { get => _CustomerIndex; set { _CustomerIndex = value; OnPropertyChanged("CustomerIndex"); } }
        private bool _IsOpenPopupCustomer;
        public bool IsOpenPopupCustomer { get => _IsOpenPopupCustomer; set { _IsOpenPopupCustomer = value; OnPropertyChanged("IsOpenPopupCustomer"); } }

        private string _CustomerText;
        public string CustomerText { get => _CustomerText; set { _CustomerText = value; OnPropertyChanged("CustomerText"); } }
        private string _CustomerInfor;
        public string CustomerInfor { get => _CustomerInfor; set { _CustomerInfor = value; OnPropertyChanged("CustomerInfor"); } }
        public Customer Customer;
        public CustomerDebitOrderViewModel(CardOrderItem cardOrderItem)
        {
            if (currentUser != null)
            {
                
                CodeOrder = string.Format("{0} - {1}", cardOrderItem.OrderCode, cardOrderItem.TableName);
                TotalAmount = string.Format("{0}VND", cardOrderItem.CashAmountString);
                if (cardOrderItem.CustomerId > 0)
                {
                    GetCustomerById(cardOrderItem.CustomerId);

                }

                AddCustomerCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
                {
                    ManageCustomerWindow createCustomer = new ManageCustomerWindow();
                    createCustomer.DataContext = new ManageCustomerViewModel();
                    createCustomer.ShowDialog();
                    var CustomerViewModel = createCustomer.DataContext as ManageCustomerViewModel;
                    if (CustomerViewModel.IsCreate == true && CustomerViewModel.CustomerUpdate != null)
                    {
                        CustomerList.Insert(0, CustomerViewModel.CustomerUpdate);
                        CustomerText = CustomerViewModel.CustomerUpdate.Name;
                        IsOpenPopupCustomer = false;
                        CustomerItem = CustomerViewModel.CustomerUpdate;
                        Customer = CustomerItem;
                    }
                });
                EnterTextBoxCustomerCommand = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    FindCustomerByPhone();
                });
                ByCusByPhone = new RelayCommand<TextBox>((p) => { return true; }, p =>
                {
                    FindCustomerByPhone();
                });
                CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    p.Close();
                });

                AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    if (Customer == null)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_NOT_SELECT_CUSTOMER);
                    }
                    else
                    {
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        CustomerDebitWrapper wrapper = new CustomerDebitWrapper(currentUser.BranchId, Customer.Id, cardOrderItem.OrderId, string.IsNullOrEmpty(Note) ? "" : Note);
                        BaseResponse response = ordersClient.UpdateDebitOrderCustomer(cardOrderItem.OrderId, wrapper);
                        if (response != null && response.Status == (int)ResponseEnum.OK)
                        {
                            p.Close();
                            NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                        }

                    }


                });
            }
        }
        public void GetCustomerById(long id)
        {
         
            CustomerClient client = new CustomerClient(this, this, this);
            CustomerRegisterResponse response = client.GetCustomerDetail(id);
            if (response != null && response.Status==(int)ResponseEnum.OK)
            {
                Customer = response.Data;
                CustomerInfor = String.Format("{0} - {1}", response.Data.Name, response.Data.Phone);
                CustomerText = response.Data.Phone;
            }
        }

        public void FindCustomerByPhone()
        {
            CustomerClient client = new CustomerClient(this, this, this);
            FindCustomerWrapper wrapper = new FindCustomerWrapper(string.IsNullOrEmpty(CustomerInfor) ? "" : CustomerInfor, string.IsNullOrEmpty(CustomerText) ? "" : CustomerText);
            CustomerRegisterResponse response = client.FindCustomerByPhone(wrapper);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                Customer = response.Data;
                CustomerInfor = String.Format("{0} - {1}", response.Data.Name, response.Data.Phone);
                CustomerText = response.Data.Phone;
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
        }

    }
}

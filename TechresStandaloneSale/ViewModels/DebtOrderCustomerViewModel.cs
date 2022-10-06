using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;

namespace TechresStandaloneSale.ViewModels
{
    public class DebtOrderCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand EditCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand CheckAllCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        private List<CustomerDebtData> OrderDebtListAll;
        private ObservableCollection<CustomerDebtData> _OrderDebtList = new ObservableCollection<CustomerDebtData>();
        public ObservableCollection<CustomerDebtData> OrderDebtList { get => _OrderDebtList; set { _OrderDebtList = value; OnPropertyChanged("OrderDebtList"); } }
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
        private long BranchId;    
        private List<long> ListOrderIds = new List<long>();
        public bool isCreate;
        public DebtOrderCustomerViewModel(long CustomerId)
        {
            Application.Current.Dispatcher.Invoke(async delegate
            {
                DialogHostOpen = true;
                CustomerClient client = new CustomerClient(this, this, this);
                CustomerDebtResponse CustomerResponse = await System.Threading.Tasks.Task.Run(() => client.CustomerDebtData(CustomerId));
                if (CustomerResponse != null && CustomerResponse.Status == (int)ResponseEnum.OK)
                {
                    OrderDebtListAll = new List<CustomerDebtData>();
                    CustomerResponse.Data.ForEach(OrderDebtListAll.Add);
                    CustomerResponse.Data.ForEach(OrderDebtList.Add);
                    DialogHostOpen = false;
                }
                else
                    DialogHostOpen = false;
            });
            CheckCommand = new RelayCommand<CustomerDebtData>((p) => { return true; }, p =>
            {                
                ListOrderIds.Add(p.OrderId);
                BranchId = p.BranchId;
            });
            CheckAllCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                ListOrderIds.Clear();
                if (p.IsChecked.Value)
                {
                    this.OrderDebtList.Clear();
                    foreach (CustomerDebtData order in OrderDebtListAll)
                    {
                        ListOrderIds.Add(order.OrderId);
                        order.IsSelected = true;
                        OrderDebtList.Add(order);
                    }
                }
                else
                {                   
                    this.OrderDebtList.Clear();
                    foreach (CustomerDebtData order in OrderDebtListAll)
                    {
                        order.IsSelected = false;
                        OrderDebtList.Add(order);
                    }
                }
                //ListOrderIds.Add(p.OrderId);
                BranchId = OrderDebtList[0].BranchId;
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if(ListOrderIds.Count > 0)
                {
                    DebtCustomerConfirm debt = new DebtCustomerConfirm();
                    debt.BranchId = BranchId;
                    debt.OrderIds = ListOrderIds;
                    CustomerClient client = new CustomerClient(this, this, this);
                    BaseResponse CustomerResponse = client.CustomerConfirmDebt(debt);
                    if (CustomerResponse != null && CustomerResponse.Status == (int)ResponseEnum.OK)
                    {
                        isCreate = true;
                        p.Close();
                    }
                    else
                    {
                        NotificationMessage.Error(CustomerResponse.Message);
                    }
                }                
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    isCreate = false;
                    p.Close();
                }
            });
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

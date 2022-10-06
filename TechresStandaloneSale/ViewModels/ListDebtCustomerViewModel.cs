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
using TechresStandaloneSale.Views;

namespace TechresStandaloneSale.ViewModels
{
    public class ListDebtCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand EditCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        private string _ContentTitle { get; set; }
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private ObservableCollection<CustomerDebtData> _CustomerList = new ObservableCollection<CustomerDebtData>();
        public ObservableCollection<CustomerDebtData> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged("CustomerList"); } }
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
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private int BrandId;
        private long BranchId;
        public async void GetDetail(int brandId, long branchId)
        {
            CustomerClient client = new CustomerClient(this, this, this);
            CustomerDebtResponse CustomerResponse = await System.Threading.Tasks.Task.Run(() => client.GetAllCustomerDebt(brandId, branchId, Constants.ALL, Constants.ALL, 0));
            if (CustomerResponse != null && CustomerResponse.Status == (int)ResponseEnum.OK)
            {
                if(CustomerList != null)
                {
                    CustomerList.Clear();
                }
                else
                {
                    CustomerList = new ObservableCollection<CustomerDebtData>();
                }
                CustomerResponse.Data.ForEach(CustomerList.Add);
                ContentTitle = string.Format(MessageValue.MESSAGE_FROM_CUSTOMER_DEBIT_HISTORY, CustomerResponse.Data.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public ListDebtCustomerViewModel()
        {
            DialogHostOpen = true;
            if (currentUser.UserManagerId == (int)UserManagerEnum.RESTAURANT)
            {

                BrandVisibility = Visibility.Visible;
                if (BrandList == null)
                {
                    BrandList = new ObservableCollection<Brand>();
                }
                else
                {
                    BrandList.Clear();
                }
                BrandList = Utils.Utils.GetBrands(true);
                BrandItem = BrandList.Where(x => x.Id == currentUser.RestaurantBrandId).FirstOrDefault();
                BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                if (BranchList == null)
                {
                    BranchList = new ObservableCollection<Branch>();
                }
                else
                {
                    BranchList.Clear();
                }
                BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, true);
                BranchItem = BranchList.Where(x => x.Id == currentUser.BranchId).FirstOrDefault();
                BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
            }
            else if (currentUser.UserManagerId == (int)UserManagerEnum.BRAND)
            {
                BranchVisibility = Visibility.Visible;
                if (BranchList == null)
                {
                    BranchList = new ObservableCollection<Branch>();
                }
                else
                {
                    BranchList.Clear();
                }
                BranchList = Utils.Utils.GetBranchs(currentUser.RestaurantBrandId, true);
                BranchItem = BranchList.Where(x => x.Id == currentUser.BranchId).FirstOrDefault();
                BrandId = currentUser.RestaurantBrandId;
                BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
            }
            else
            {
                BrandVisibility = Visibility.Collapsed;
                BranchVisibility = Visibility.Collapsed;
                BrandId = currentUser.RestaurantBrandId;
                BranchId = currentUser.BranchId;
            }
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_CUSTOMER_DEBIT_HISTORY, 0);
            GetDetail(BrandId, BranchId);
            SelectionChangedBrandCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BrandItem != null && BrandId != (BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id))
                {
                    if (BranchList == null)
                    {
                        BranchList = new ObservableCollection<Branch>();
                    }
                    else
                    {
                        BranchList.Clear();
                    }
                    BrandId = BrandItem == null ? currentUser.RestaurantBrandId : BrandItem.Id;
                    BranchList = Utils.Utils.GetBranchs(BrandId, true);
                    if (BranchList.Count > 0) BranchItem = BranchList[0];
                    if (CustomerList == null)
                    {
                        CustomerList = new ObservableCollection<CustomerDebtData>();
                    }
                    else
                    {
                        CustomerList.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchItem != null && BranchId != (BranchItem == null ? currentUser.BranchId : BranchItem.Id))
                {
                    BranchId = BranchItem == null ? currentUser.BranchId : BranchItem.Id;
                    GetDetail(BrandId, BranchId);
                }
            });
            EditCommand = new RelayCommand<CustomerDebtData>((p) => { return true; }, p =>
            {
                DebtCustomerWindow window = new DebtCustomerWindow();
                window.DataContext = new DebtOrderCustomerViewModel(p.CustomerId);
                window.ShowDialog();
                var debit = window.DataContext as DebtOrderCustomerViewModel;
                if (debit.isCreate)
                {
                    GetDetail(BrandId, BranchId);
                }
            });
        }


        public void LogError(Exception ex, string infoMessage)
        {
            WriteLog.logs(infoMessage);
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

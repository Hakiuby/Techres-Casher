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
    public class InputMoneyCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

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

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SearchCustomerPhoneCommand { get; set; }
        public ICommand ChooseMoneyCommand { get; set; }
        public ICommand UnChooseMoneyCommand { get; set; }

        private string _CustomerPhone;
        public string CustomerPhone { get => _CustomerPhone; set { _CustomerPhone = value; OnPropertyChanged("CustomerPhone"); } }
        private string _CustomerInfor;
        public string CustomerInfor { get => _CustomerInfor; set { _CustomerInfor = value; OnPropertyChanged("CustomerInfor"); } }
        private Visibility _CustomerInforVisibility;
        public Visibility CustomerInforVisibility { get => _CustomerInforVisibility; set { _CustomerInforVisibility = value; OnPropertyChanged("CustomerInforVisibility"); } }
        private ObservableCollection<TopUpCard> _TopUpCardList = new ObservableCollection<TopUpCard>();
        public ObservableCollection<TopUpCard> TopUpCardList { get => _TopUpCardList; set { _TopUpCardList = value; OnPropertyChanged("TopUpCardList"); } }

        public long CustomerId;
        public long TopUpCardId;
        public void GetDetail()
        {
            if (TopUpCardList != null)
            {
                TopUpCardList.Clear();
            }
            else
            {
                TopUpCardList = new ObservableCollection<TopUpCard>();
            }
            TopUpCardClient client = new TopUpCardClient(this, this, this);
            TopUpCardResponse response = client.GetListTopUpCardResponse(Constants.STATUS);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                response.Data.ForEach(TopUpCardList.Add);
              //  CustomerInforVisibility = Visibility.Collapsed;
            }
        }

        public InputMoneyCustomerViewModel()
        {
            CustomerInforVisibility = Visibility.Collapsed;
            GetDetail();
            SearchCustomerPhoneCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(CustomerPhone))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_FROM_NOTIFICATION_CUSTOMER_TAG);
                }
                else
                {
                    CustomerClient client = new CustomerClient(this, this, this);
                    CustomerRegisterResponse response = client.FindCustomerByPhone(new Models.Request.FindCustomerWrapper(string.Empty, CustomerPhone));
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        CustomerInfor = string.Format("{0}: {1}  -  {2}{3}",MessageValue.MESSAGE_FROM_MENU_CUSTOMER_CATEGORY, response.Data.Name,MessageValue.MESSAGE_FROM_RECEVICE_DEPOSIT_BOOKING_PHONE_NUMBER, response.Data.Phone);
                        CustomerId = response.Data.Id;
                        CustomerInforVisibility = Visibility.Visible;
                    }
                }

            });
            UnChooseMoneyCommand = new RelayCommand<TopUpCard>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    int index = TopUpCardList.IndexOf(p);
                    p.IsChoose = false;
                    TopUpCardList.Remove(p);
                    TopUpCardList.Insert(index, p);
                    TopUpCardId = 0;
                }
            });
            ChooseMoneyCommand = new RelayCommand<TopUpCard>((p) => { return true; }, (p) =>
             {
                 if (p != null)
                 {
                     int index = TopUpCardList.IndexOf(p);
                     TopUpCard topUpCard = TopUpCardList.Where(x => x.IsChoose).FirstOrDefault();
                     if (topUpCard != null)
                     {
                         int indexOld = TopUpCardList.IndexOf(topUpCard);
                         TopUpCardList.Remove(topUpCard);
                         topUpCard.IsChoose = false;
                         TopUpCardList.Insert(indexOld, topUpCard);
                     }
                     TopUpCardId = p.Id;
                     p.IsChoose = true;
                     TopUpCardList.Remove(p);
                     TopUpCardList.Insert(index, p);
                 }

             });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (CustomerId <= 0 || TopUpCardId <= 0)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_FROM_INPUT_MONEY_CUSTOMER_FAIL);
                }
                else
                {
                    TopUpCardClient client = new TopUpCardClient(this, this, this);
                    RestaurantTopUpCardResponse response = client.InputMoneyToCustomer(CustomerId, TopUpCardId);
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        p.Close();
                    }                    
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close(); 
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

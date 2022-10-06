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
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;

namespace TechresStandaloneSale.ViewModels
{
   public class CreateRestaurantExtraViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand RestaurantExtraChangeCommand { get; set; }
        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }
        private bool _AmountIsEnabled;
        public bool AmountIsEnabled { get => _AmountIsEnabled; set { _AmountIsEnabled = value; OnPropertyChanged("AmountIsEnabled"); } }

        private ObservableCollection<RestaurantExtra> _RestaurantExtraList = new ObservableCollection<RestaurantExtra>();
        public ObservableCollection<RestaurantExtra> RestaurantExtraList { get => _RestaurantExtraList; set { _RestaurantExtraList = value; OnPropertyChanged("RestaurantExtraList"); } }
        private RestaurantExtra _RestaurantExtraItem;
        public RestaurantExtra RestaurantExtraItem { get => _RestaurantExtraItem; set { _RestaurantExtraItem = value; OnPropertyChanged("RestaurantExtraItem"); } }
        public bool isCreate;

        public BillResponse ExtraChange;
        public void GetRestaurantExtra()
        {
            if (RestaurantExtraList != null)
            {
                RestaurantExtraList.Clear();
            }
            else
            {
                RestaurantExtraList = new ObservableCollection<RestaurantExtra>();
            }
           
            RestaurantExtraClient client = new RestaurantExtraClient(this, this, this);
            RestaurantExtraResponse response = client.GetRestaurantExtraResponse();
            if (response!= null && response.Status == (int)ResponseEnum.OK)
            {
                response.Data.ForEach(RestaurantExtraList.Add);
            }
            RestaurantExtra extra = new RestaurantExtra();
            extra.Id = 0;
            extra.Name = "KHÁC";
            RestaurantExtraList.Add(extra);
            RestaurantExtraItem = RestaurantExtraList[0];
            if (RestaurantExtraItem!= null && RestaurantExtraItem.Id > 0)
            {
                Amount = Utils.Utils.FormatMoney(RestaurantExtraItem.Price);
                Note = RestaurantExtraItem.Description;
                AmountIsEnabled = false;
            }
            else
            {
                Amount = "0";
                AmountIsEnabled = true;
            }
        }
        public CreateRestaurantExtraViewModel(long orderId)
        {
            GetRestaurantExtra();
            RestaurantExtraChangeCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (RestaurantExtraItem != null && RestaurantExtraItem.Id > 0)
                {
                    Amount = Utils.Utils.FormatMoney(RestaurantExtraItem.Price);
                    Note = RestaurantExtraItem.Description;
                    AmountIsEnabled = false;
                }
                else
                {
                    Amount = "0";
                    AmountIsEnabled = true;
                }
            });
            CloseCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                t.Close();
            });

            AddCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
              if (string.IsNullOrEmpty(Amount) )
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_EMPTY_AMOUNT);
                }
                else if (string.IsNullOrEmpty(Note))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_EMPTY_NOTE);
                }
                else
                {
                    decimal amount = Utils.Utils.FormatMoneyDecimal(Amount);
                    if (amount <= 0 || amount <= 1000)
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_FROM_NOTIFICATION_SHIPPINGFEE);
                    }
                    else
                    {
                        RestaurantExtraClient client = new RestaurantExtraClient(this, this, this);
                        AddExtraChargeResponse response = client.AddRestaurantExtra(new Models.Request.AddRestaurantExtraWrapper(RestaurantExtraItem.Id, RestaurantExtraItem.Name, amount, 1, Note), orderId);
                        if (response!= null && response.Status == (int)ResponseEnum.OK)
                        {
                            isCreate = true;
                            ExtraChange = response.Data;
                            ExtraChange.TotalPriceInlcudeAdditionFoods = response.Data.TotalPrice;                           
                            t.Close();
                        }
                    }
                }

            });

        }
        public void LogError(Exception ex, string infoMessage)
        {
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

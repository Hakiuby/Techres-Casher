using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class CreateFoodOtherViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _FoodName;
        public string FoodName { get => _FoodName; set { _FoodName = value; OnPropertyChanged("FoodName"); } }

        private int _Quantity;
        public int Quantity { get => _Quantity; set { _Quantity = value; OnPropertyChanged("Quantity"); } }

        private string _Price;
        public string Price { get => _Price; set { _Price = value; OnPropertyChanged("Price"); } }

        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        private ObservableCollection<Kitchen> _KitchenList = new ObservableCollection<Kitchen>();
        public ObservableCollection<Kitchen> KitchenList { get => _KitchenList; set { _KitchenList = value; OnPropertyChanged("KitchenList"); } }
        private Kitchen _KitchenItem;
        public Kitchen KitchenItem { get => _KitchenItem; set { _KitchenItem = value; OnPropertyChanged("KitchenItem"); } }

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddQuantityCommand { get; set; }
        public ICommand TextChangePriceCommand { get; set; }
        public ICommand TextChangeQuantityCommand { get; set; }
        public ICommand TextChangeAmountCommand { get; set; }
        public ICommand SubQuantityCommand { get; set; }
        public BillResponse FoodOther { get; set; }
        public bool isCreate = false;
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public void GetDetail()
        {
            if (KitchenList != null)
            {
                KitchenList.Clear();
            }
            else
            {
                KitchenList = new ObservableCollection<Kitchen>();
            }

            KitchenClient client = new KitchenClient(this, this, this);
            KitchenResponse response = client.GetKitchenResponses(currentUser.RestaurantBrandId, (int)currentUser.BranchId, 1, -1);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                response.Data.ForEach(KitchenList.Add);
            }
        }
        public CreateFoodOtherViewModel()
        {
            FoodOther = new BillResponse();
            Quantity = 1;
            FoodName = "";
            Price = "0";
            Amount = "0";
            Note = "";
            isCreate = false;
            GetDetail();
            AddQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (IsPhoneNumber(Quantity.ToString()) && Quantity != 999)
                {
                    Quantity = Quantity + 1;
                    decimal price = decimal.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(".", ""));
                    Amount = Utils.Utils.FormatMoney(price * Quantity);
                }
            });
            SubQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (IsPhoneNumber(Quantity.ToString()))
                {
                  //  Quantity = Quantity + 1;
                    if (Quantity > 1)
                    {
                        Quantity = Quantity - 1;
                        decimal price = decimal.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(".", ""));
                        Amount = Utils.Utils.FormatMoney(price * Quantity);
                    }
                }
            });
            TextChangePriceCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                #region Dat
                decimal price;
                if (string.IsNullOrEmpty(Price))
                {
                    Price = "0";
                    price = decimal.Parse(Price);
                }
                else
                {
                    string convert = Price.Replace(",", "");
                    if (string.IsNullOrEmpty(convert))
                    {
                        convert = "0";
                    }
                    price = decimal.Parse(convert);
                }
                #endregion
                ////decimal price = decimal.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(",", ""));
                Amount = Utils.Utils.FormatMoney(price * Quantity);
            });
            TextChangeQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                //decimal price = int.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(".", ""));
                decimal price = decimal.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(",", ""));
                Amount = Utils.Utils.FormatMoney(price * Quantity);
            });
            TextChangeAmountCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                decimal price = decimal.Parse(string.IsNullOrEmpty(Amount) ? "0" : Amount.Replace(".", ""));
                Price = Utils.Utils.FormatMoney((decimal)price / Quantity);
            });
            AddCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                decimal price = decimal.Parse(string.IsNullOrEmpty(Price) ? "0" : Price.Replace(",", ""));
                if (string.IsNullOrEmpty(FoodName))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_WRITE_FOOD_NAME);
                }
                else if (KitchenItem == null)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_WRITE_KITCHEN);
                }
                else if(price < 1)
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_FROM_FOOD_OTHER_EMPTY_PRICE);
                }
                else
                {
                    FoodOther.FoodName = FoodName;
                    FoodOther.Quantity = Quantity;
                    FoodOther.FoodUnit = "Phần";
                    FoodOther.Price = price;
                    FoodOther.UnitPrice = price;
                    FoodOther.TotalPriceInlcudeAdditionFoods = FoodOther.TotalAmount;
                    FoodOther.Note = string.IsNullOrEmpty(Note) ? "" : Note;
                    FoodOther.RestaurantKitchenPlaceId = KitchenItem.Id;
                    isCreate = true;
                    t.Close();
                }
            });
            CloseCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (!string.IsNullOrEmpty(FoodName))
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_CONFIRM_EDIT_EXIT;
                    string Title = MessageValue.MESSAGE_CONFIRM_EDIT_EXIT_TITLE;
                    string YesContent = MessageValue.MESSAGE_EXIT_WINDOW_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NOT_EXIT_WINDOW_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        isCreate = false;
                        t.Close();
                    }
                }
                else
                {
                    isCreate = false;
                    t.Close();
                }
            });
        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^[-+]?[0-9]*\.?[0-9]+$");
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

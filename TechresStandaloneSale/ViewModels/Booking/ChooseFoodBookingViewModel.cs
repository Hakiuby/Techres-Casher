using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView.CreateOrder;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Booking
{
    public class ChooseFoodBookingViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<Category> _CategoryList = new ObservableCollection<Category>();
        public ObservableCollection<Category> CategoryList
        {
            get
            {
                return _CategoryList;
            }
            set
            {
                _CategoryList = value;
                OnPropertyChanged("ItemsCategory");
            }
        }
        private Category _CategoryItem;
        public Category CategoryItem
        {
            get
            {
                return _CategoryItem;
            }
            set
            {
                _CategoryItem = value;
                OnPropertyChanged("CategoryItem");
            }
        }
        private ObservableCollection<Food> _FoodList = new ObservableCollection<Food>();
        public ObservableCollection<Food> FoodList
        {
            get
            {
                return _FoodList;
            }
            set
            {
                _FoodList = value;
                OnPropertyChanged("FoodList");
            }
        }
        private ObservableCollection<BillResponse> foodOrderList = new ObservableCollection<BillResponse>();
        public ObservableCollection<BillResponse> FoodOrderList
        {
            get
            {
                return foodOrderList;
            }
            set
            {
                foodOrderList = value;
                OnPropertyChanged("FoodOrderList");
            }
        }
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
        public string _TotalAmount;
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
        public string _ContentTilte;
        public string ContentTilte
        {
            get
            {
                return _ContentTilte;
            }
            set
            {
                _ContentTilte = value;
                OnPropertyChanged("ContentTilte");
            }
        }
        public string _FoodOrderListString;
        public string FoodOrderListString
        {
            get
            {
                return _FoodOrderListString;
            }
            set
            {
                _FoodOrderListString = value;
                OnPropertyChanged("FoodOrderListString");
            }
        }
        
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand CancelFood { get; set; }
        public ICommand AddQuantityCommand { get; set; }

        public ICommand TextChangeAmount { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand SaveOrderCommand { get; set; }
        public ICommand ReturnFood { get; set; }
        public ICommand NoteFood { get; set; }
        public ICommand BtnCategories { get; set; }

        public ICommand CategoryFoodCommand { get; set; }

        public ICommand CategoryDrinkCommand { get; set; }

        public ICommand CategoryOtherCommand { get; set; }

        public ICommand CategorySeaFoodCommand { get; set; }

        public ICommand SubQuantityCommand { get; set; }


        public ICommand CategoryBBQCommand { get; set; }

        public ICommand AddFoodCommand { get; set; }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);

        public bool IsBBq;

        public long CategoryType;

        public long CategoryId;
        public void GetAllFoodLisst(long branchId, long categoryId, long isSpecial)
        {
            if (FoodList != null)
            {
                FoodList.Clear();
            }
            else
            {
                FoodList = new ObservableCollection<Food>();
            }
            FoodsClient client = new FoodsClient(this, this, this);
            FoodResponses response = client.GetFoodsUSing(Constants.ALL, branchId, categoryId, Constants.ALL, Constants.STATUS, isSpecial, Constants.NOT_STATUS,1);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.ForEach(FoodList.Add);

            }
        }
        public void GetAllCategoryList(int brandId)
        {
            if (CategoryList != null)
            {
                CategoryList.Clear();
            }
            else
            {
                CategoryList = new ObservableCollection<Category>();
            }
            CategoriesClient client = new CategoriesClient(this, this, this);
            CategoryResponse categoryReponse = client.GetAllCategory(Constants.STATUS, "", brandId);
            if (categoryReponse != null && categoryReponse.Status == (int)ResponseEnum.OK)
            {
                Category category = new Category();
                category.Id = Constants.ALL;
                category.Name = MessageValue.MESSAGE_ALL;
                CategoryList.Add(category);
                categoryReponse.Data.ForEach(CategoryList.Add);
            }
        }
        public bool isConfirm = true;
        private void GetTotalAmount()
        {
            TotalAmount = Utils.Utils.FormatMoney(FoodOrderList.Sum(x => x.TotalAmount));
            FoodOrderListString =  String.Concat(FoodOrderList.Select(o => string.Format("{0} ({1} {2}), ", o.FoodName, o.Quantity, o.UnitType)));
        }
        public ChooseFoodBookingViewModel(int brandId,long branchId,List<BillResponse> bookingFoodList, bool isGift)
        {
            if (FoodOrderList == null)
            {
                FoodOrderList = new ObservableCollection<BillResponse>();
            }
            else
            {
                FoodOrderList.Clear();
            }
            if (bookingFoodList != null)
            {
                bookingFoodList.ForEach(FoodOrderList.Add);
            }
            GetAllCategoryList(brandId);
            CategoryItem = CategoryList[0];
            if (isGift)
            {
                ContentTilte = MessageValue.MESSAGE_FROM_GIFT_FOOD_BUTTON;
                GetAllFoodLisst(branchId, CategoryItem != null ? CategoryItem.Id : Constants.ALL, Constants.STATUS);

            }
            else
            {
                ContentTilte = MessageValue.MESSAGE_FROM_DESCRIPTION_MENU;
                GetAllFoodLisst(branchId, CategoryItem != null ? CategoryItem.Id : Constants.ALL, Constants.ALL);
            }
            GetTotalAmount();
            SelectionChangedCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (isGift)
                {
                    GetAllFoodLisst(branchId, CategoryItem != null ? CategoryItem.Id : Constants.ALL, Constants.STATUS);

                }
                else
                {
                    GetAllFoodLisst(branchId, CategoryItem != null ? CategoryItem.Id : Constants.ALL, Constants.ALL);
                }
            });
           
            AddQuantityCommand = new RelayCommand<Food>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.Id).FirstOrDefault();
                    if (orderFood != null)
                    {
                        orderFood.Quantity = orderFood.Quantity + 1;
                        orderFood.Price = t.Price;
                        orderFood.IsPrint = false;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(0, orderFood);
                    }
                    else
                    {
                        BillResponse of = new BillResponse();
                        of.FoodId = t.Id;
                        of.Quantity = 1;
                        of.Price = t.Price;
                        of.UnitPrice = t.Price;
                        of.FoodName = t.Name;
                        of.CategoryType =(int) t.CategoryTypeId;
                        of.UnitType = t.UnitType;
                        of.FoodUnit = t.UnitType;
                        of.IsPrint = false;
                        if (isGift)
                        {
                            of.IsGift = 1;
                        }
                        else
                        {
                            of.IsGift = 0;
                        }
                        FoodOrderList.Insert(0, of);
                    }
                }
                GetTotalAmount();
            });

            CancelFood = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                string contentConfirm = MessageValue.MESSAGE_CANCEL_FOOD;
                string Title = MessageValue.MESSAGE_CANCEL_FOOD_TITLE;
                string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                confirmDeleteWindow.ShowDialog();
                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                if (confirm.isConfirm)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == t.IsGift).FirstOrDefault();
                    if (orderFood != null)
                    {
                        FoodOrderList.Remove(orderFood);
                    }
                    GetTotalAmount();
                }
            });
            CloseCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {

                if (FoodOrderList != null && FoodOrderList.Count > 0)
                {
                    
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_FROM_FOOD_BOOKING_CANCEL_TITLE;
                        string Title = MessageValue.MESSAGE_NOTIFICATION;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            t.Close();
                            isConfirm = false;
                        }
                }
                else
                    t.Close();
            }); 
            SaveOrderCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    if (FoodOrderList!= null && FoodOrderList.Count > 0)
                    {
                        t.Close();
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_FROM_FOOD_BOOKING);
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

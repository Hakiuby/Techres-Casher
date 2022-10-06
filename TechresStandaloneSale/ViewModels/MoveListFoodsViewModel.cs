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
    public class MoveListFoodsViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SelectionBranchChangedCommand { get; set; }
        public ICommand ChooseFoodsCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
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
        private ObservableCollection<Kitchen> _KitchenList;
        public ObservableCollection<Kitchen> KitchenList
        {
            get
            {
                return _KitchenList;
            }
            set
            {
                _KitchenList = value;
                OnPropertyChanged("KitchenList");
            }
        }
        private Kitchen _KitchenInItem;
        public Kitchen KitchenInItem
        {
            get
            {
                return _KitchenInItem;
            }
            set
            {
                _KitchenInItem = value;
                OnPropertyChanged("KitchenInItem");
            }
        }
        private Kitchen _KitchenOutItem;
        public Kitchen KitchenOutItem
        {
            get
            {
                return _KitchenOutItem;
            }
            set
            {
                _KitchenOutItem = value;
                OnPropertyChanged("KitchenOutItem");
            }
        }
        private ObservableCollection<Food> _FoodList;
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

        private Branch _BranchItem { get; set; }
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private Visibility _BranchVisibility { get; set; }
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }
        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private long BranchId;
        public void GetAllBranches()
        {
            if (BranchList == null)
            {
                BranchList = new ObservableCollection<Branch>();
            }
            else
            {
                BranchList.Clear();
            }
            List<Branch> branches = (List<Branch>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_BRANCH);
            if (branches != null)
            {
                branches.Where(x=>x.Id != Constants.ALL).ForEach(BranchList.Add);
            }
        }
        public void GetDetail(int brandId, long branchId)
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
            KitchenResponse response = client.GetKitchenResponses(brandId,(int) branchId, 1);
            if (response != null && response.Status == (int)ResponseEnum.OK)
            {
                foreach (Kitchen i in response.Data)
                {
                    KitchenList.Add(i);
                }
            }
            KitchenInItem = KitchenList != null && KitchenList.Count > 0 ? KitchenList[0] : null;
            if (KitchenList.Count > 0)
            {
                KitchenInItem = KitchenList[0];
                if (KitchenList.Count>1)
                KitchenOutItem = KitchenList[1];
                else
                {
                    KitchenOutItem = KitchenList[0];
                }
            }
            else
            {
                KitchenInItem = null;
                KitchenOutItem = null;
            }
        }
        public void GetAllBrands()
        {
            if (BrandList == null)
            {
                BrandList = new ObservableCollection<Brand>();
            }
            else
            {
                BrandList.Clear();
            }
            List<Brand> branches = (List<Brand>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_BRAND);
            if (branches != null)
            {
                branches.ForEach(BrandList.Add);
            }
        }
        public void init()
        {
            if (currentUser != null)
            {
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
            }
            GetDetail(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : currentUser.BranchId);
            if (KitchenInItem != null)
            {
                GetAllFoodList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : currentUser.BranchId, KitchenInItem.Id);
            }
        }
        public void GetAllFoodList(int RestaurantBrandId,long branchId, int KitchenId)
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
            FoodResponses response = client.GetAllFoods(RestaurantBrandId,branchId, Constants.STATUS, Constants.ALL, Constants.ALL, Constants.ALL, Constants.NOT_STATUS, Constants.ALL, Constants.ALL, Constants.ALL,KitchenId, Constants.ALL, Constants.ALL);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.ForEach(FoodList.Add);
            }
        }
        public FrameworkElement GetWindowParent(System.Windows.Controls.UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }
            return parent;
        }
      private ContentControl _MainContentControl;
        public void CloseOrder(UserControl t)
        {
            FrameworkElement window = GetWindowParent(t);
            var w = window as Window;
           Utils.Utils.GoHome(w,currentUser, _MainContentControl);
        }
        
        public User currentUser;
        public MoveListFoodsViewModel()
        {
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            init();
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                //if (KitchenInItem != null)
                //{
                //    GetAllFoodList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, KitchenInItem.Id);
                //}
                if (BrandItem != null && BrandItem.Id != BranchId)
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
                        //if (BranchList.Count > 0) BranchItem = BranchList[0];
                        if (FoodList == null)
                        {
                            FoodList = new ObservableCollection<Food>();
                        }
                        else
                        {
                            FoodList.Clear();
                        }
                    }
                    DialogHostOpen = false;
                }
            });
            SaveCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                string contentConfirm = string.Format(MessageValue.MESSAGE_FROM_MOVE_FOOD_KITCHEN_PLACE_URGENTLY, KitchenInItem.Name, KitchenOutItem.Name);
                string Title = MessageValue.MESSAGE_FROM_MOVE_FOOD_KITCHEN_PLACE_URGENTLY_TITLE;
                string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                string NoContent = MessageValue.MESSAGE_FROM_UNIT_NO_CONFIRM_DELETED;
                confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                confirmDeleteWindow.ShowDialog();
                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                if (confirm.isConfirm)
                {
                    FoodsClient client = new FoodsClient(this, this, this);
                    UpdateFoodKitchenWrapper update = new UpdateFoodKitchenWrapper();
                    update.RestaurantBrandId = BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId;
                    update.BranchId = BranchItem != null ? BranchItem.Id: currentUser.BranchId;
                    update.IsMoveUrgently = 1;
                    update.KitchenPlaceId = KitchenOutItem.Id;
                    update.List = FoodList.Select(x => x.Id).ToList();
                    BaseResponse response = client.UpdateKitchen(update);
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                    }
                }
                else
                {
                    FoodsClient client = new FoodsClient(this, this, this);
                    UpdateFoodKitchenWrapper update = new UpdateFoodKitchenWrapper();
                    update.BranchId = BranchItem != null ? BranchItem.Id : currentUser.BranchId;
                    update.IsMoveUrgently = 0;
                    update.KitchenPlaceId = KitchenOutItem.Id;
                    update.List = FoodList.Select(x => x.Id).ToList();
                    BaseResponse response = client.UpdateKitchen(update);
                    if (response != null && response.Status == (int)ResponseEnum.OK)
                    {
                        NotificationMessage.Infomation(MessageValue.MESSAGE_RESPONSE_SUCCESS);
                    }
                }
                CloseOrder(p);
            });
            CancelCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                CloseOrder(p);
            });
            SelectionBranchChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                GetDetail(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId,BranchItem != null ? BranchItem.Id : currentUser.BranchId);
                if (KitchenInItem != null)
                {
                    GetAllFoodList(BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, KitchenInItem.Id);
                }
            });

            ChooseFoodsCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
             {
                 string ToKitchen = String.Format(MessageValue.MESSAGE_FROM_MOVE_FOOD_CHOOSE_FOOD_KITCHEN, KitchenOutItem.Name);
                 string Note = string.Format(MessageValue.MESSAGE_FROM_MOVE_FOOD_CHOOSE_FOOD_NOTE, KitchenInItem.Name, KitchenOutItem.Name);
                 ChooseFoodsMoveKitchenWindow chooseFoods = new ChooseFoodsMoveKitchenWindow();
                 chooseFoods.DataContext = new ChooseFoodsMoveKitchenViewModel(ToKitchen, Note, BrandItem != null ? BrandItem.Id : currentUser.RestaurantBrandId, BranchItem != null ? BranchItem.Id : currentUser.BranchId, FoodList);
                 chooseFoods.ShowDialog();
                 var confirm = chooseFoods.DataContext as ChooseFoodsMoveKitchenViewModel;
                 if (confirm.IsComfirm == true)
                 {
                    //InitFoodList();
                    FoodList.Clear();
                    confirm.FoodList.Where(x => x.IsSelected == true).ToList().ForEach(FoodList.Add);
                    //CloseOrder(p);
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

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
    public class ChooseFoodsMoveKitchenViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand IsCheckAllSelectedCommand { get; set; }
        public ICommand IsCheckCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }
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
        private string _ToKitchen;
        public string ToKitchen
        {
            get
            {
                return _ToKitchen;
            }
            set
            {
                _ToKitchen = value;
                OnPropertyChanged("ToKitchen");
            }
        }
        private bool _IsCheckAllSelected;
        public bool IsCheckAllSelected
        {
            get
            {
                return _IsCheckAllSelected;
            }
            set
            {
                _IsCheckAllSelected = value;
                OnPropertyChanged("IsCheckAllSelected");
            }
        }
        private string _Note;
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
        public void InitFoodList(ObservableCollection<Food> Foods)
        {
            if (Foods.Count > 0)
            {
                List<long> foodIds = Foods.Select(x => x.Id).ToList();
                List<Food> foods = FoodList.Where(x => foodIds.Contains(x.Id)).ToList();
                //if (foods.Count == Foods.Count)
                //{
                //    IsCheckAllSelected = true;
                //}
                IsCheckAllSelected = true;
                foreach (Food f in foods)
                {
                    f.IsSelected = true;
                    int index = FoodList.IndexOf(f);
                    FoodList.Remove(f);
                    FoodList.Insert(index, f);
                }
            }
        }
        public void GetAllFoodList(int RestaurantBrandId,long BranchId)
        {
            if (FoodList == null)
            {
                FoodList = new ObservableCollection<Food>();
            }
            else
            {
                FoodList.Clear();
            }
            FoodsClient client = new FoodsClient(this, this, this);
            FoodResponses response = client.GetAllFoods(RestaurantBrandId,BranchId, Constants.STATUS, Constants.ALL, Constants.ALL, Constants.ALL, Constants.NOT_STATUS, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach (Food food in response.Data)
                {
                    food.IsSelected = true;
                    FoodList.Add(food);
                    AllFoods.Add(food);
                }
            }
        }
        public bool IsComfirm;
        private List<Food> AllFoods = new List<Food>();
        public ChooseFoodsMoveKitchenViewModel(string ToKitchen, string Note,int RestaurantBrandId, long BranchId, ObservableCollection<Food> List)
        {
            this.ToKitchen = ToKitchen;
            this.Note = Note;
            GetAllFoodList(RestaurantBrandId,BranchId);
            InitFoodList(List);
            IsCheckCommand = new RelayCommand<Food>((p) => { return true; }, p =>
            {
                p.IsSelected = true;
                //FoodList.Add(p);
            });
            IsCheckAllSelectedCommand = new RelayCommand<CheckBox>((p) => { return true; }, p =>
            {
                if (p.IsChecked.Value)
                {
                    this.FoodList.Clear();
                    foreach (Food food in AllFoods)
                    {
                        food.IsSelected = true;
                        FoodList.Add(food);
                    }
                }
                else
                {
                    this.FoodList.Clear();
                    foreach (Food food in AllFoods)
                    {
                        food.IsSelected = false;
                        FoodList.Add(food);
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                this.FoodList.Clear();
                p.Close();
            });
            SaveCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                IsComfirm = true;
                p.Close();
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

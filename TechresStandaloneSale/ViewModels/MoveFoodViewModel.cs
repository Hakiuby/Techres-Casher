using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class MoveFoodViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _CurrentTable;
        public string CurrentTable { get => _CurrentTable; set { _CurrentTable = value; OnPropertyChanged("CurrentTable"); } }

        private string _MoveTable;
        public string MoveTable { get => _MoveTable; set { _MoveTable = value; OnPropertyChanged("MoveTable"); } }

        private ObservableCollection<BillResponse> _FoodListMove = new ObservableCollection<BillResponse>();
        public ObservableCollection<BillResponse> FoodListMove
        {
            get
            {
                return _FoodListMove;
            }
            set
            {
                _FoodListMove = value;
                OnPropertyChanged("FoodListMove");
            }
        }

        public ICommand AddCommand { get; set; }

        public ICommand BtnMinusCommand { get; set; }

        public ICommand BtnAddCommand { get; set; }

        public ICommand CloseCommand { get; set; }
        public MoveFoodViewModel()
        {

        }
        public List<BillResponse> foods;

        public bool isCheck = false;
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public MoveFoodViewModel(long orderId, long tableId, string currentTable, Table moveTable)
        {
            CurrentTable = currentTable;
            MoveTable = moveTable.Name;
            OrdersClient client = new OrdersClient(this, this, this);
            OrderItemResponse response = client.GetOrderById(orderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.NOT_STATUS);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null && response.Data.Foods != null)
            {
                foreach(BillResponse o in response.Data.Foods)
                {
                    if (o.Status != 4)
                        FoodListMove.Add(o);
                }
            }

            BtnMinusCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if (p.MoveQuantity > 0)
                {

                    int index = FoodListMove.IndexOf(p);
                    p.MoveQuantity--;
                    FoodListMove.Remove(p);
                    FoodListMove.Insert(index, p);
                }
            });
            BtnAddCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if ( p.MoveQuantity >= p.Quantity)
                {
                    NotificationMessage.Warning("Vượt quá số lượng món trong danh sách");
                    return;
                }
                else
                {
                    int index = FoodListMove.IndexOf(p);
                    p.MoveQuantity++;
                    FoodListMove.Remove(p);
                    FoodListMove.Insert(index, p);
                }


            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                Application.Current.Dispatcher.Invoke((Action)async delegate
                {
                    List<FoodData> foodDatas = new List<FoodData>();
                    TablesClient tablesClient = new TablesClient(this, this, this);
                    if(FoodListMove.Count == 0)
                    {

                        NotificationMessage.Warning("Không còn món trong danh sách");
                        return;
                    }
                    foreach (BillResponse f in FoodListMove)
                    {
                        if (f.MoveQuantity > 0)
                        {
                            FoodData data = new FoodData();
                            data.OrderDetailId = f.Id;
                            data.Quantity = f.MoveQuantity;
                            foodDatas.Add(data);
                        }
                    }
                    MoveFoodWrapper wrapper = new MoveFoodWrapper();
                    wrapper.TableId = moveTable.Id;
                    wrapper.ListFood = foodDatas;
                    wrapper.OrderId = orderId;
                    BaseResponse baseResponse = await Task.Run(() => tablesClient.MoveFoodTable(tableId, wrapper));
                    if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                    {
                        isCheck = true;
                        p.Close();
                    }
                });

            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {

                p.Close();
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
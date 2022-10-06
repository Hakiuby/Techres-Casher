using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.Views.Dialogs;
using static TechresStandaloneSale.Helpers.PrintBill;

namespace TechresStandaloneSale.ViewModels
{
    public class CookViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        #region By Phan Viet Ha

        //public string FoodWorking = "FoodWorking";
        public string filterSearch { get; set; }
        public List<string> allFoodStock;
        private List<OutOfFood> _OutOfFoodsUpdate;
        public List<OutOfFood> OutOfFoodsUpdate { get => _OutOfFoodsUpdate; set { _OutOfFoodsUpdate = value ; OnPropertyChanged("OutOfFoodsUpdate"); } }
        OutOfFood food = new OutOfFood();
        private string _BranchId;
        public string BranchId
        {
            get => _BranchId;
            set
            {
                _BranchId = value;
                OnPropertyChanged("BranchId");
            }
        }
        private long _FoodIds;
        public long FoodIds
        {
            get => _FoodIds;
            set
            {
                _FoodIds = value;
                OnPropertyChanged("FoodIds");
            }
        }
        public bool IsRightStock;
        public long IsOutStock = 0;
        public long KeySearch = 0;

        private ObservableCollection<OutOfFood> _ListData = new ObservableCollection<OutOfFood>();
        public ObservableCollection<OutOfFood> ListData { get => _ListData; set { _ListData = value; OnPropertyChanged("ListData"); } } // Wrorking
        private ObservableCollection<OutOfFood> _DataOutStock;
        public ObservableCollection<OutOfFood> DataOutStock { get => _DataOutStock; set { _DataOutStock = value; OnPropertyChanged("DataOutStock"); } } // Not Working
        private ObservableCollection<OutOfFood> AllFood = new ObservableCollection<OutOfFood>();
        private ObservableCollection<OutOfFood> FoodOutStock = new ObservableCollection<OutOfFood>();
        private ObservableCollection<OutOfFood> _ListDataNotWorking = new ObservableCollection<OutOfFood>();
        public ObservableCollection<OutOfFood> ListDataNotWorking { get => _ListDataNotWorking; set { _ListDataNotWorking = value; OnPropertyChanged("ListDataNotWorking"); } } // Wrorking
        private List<OutOfFood> ListDicticncWorking = new List<OutOfFood>();
        #endregion
        public ICommand TextChangeQuantityCommand { get; set; }
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand SaveInventoryWarehouse { get; set; }
        public ICommand RefreshRealtimeCommand { get; set; }
        public ICommand BtnAllFood { get; set; }
        public ICommand BtWaitingFood { get; set; }
        public ICommand BtnHistoryFood { get; set; }
        public ICommand BtnInventoryWarehouse { get; set; }
        public ICommand SelectionDateChangedCommand { get; set; }
        public ICommand WaitingChangeStatus { get; set; }
        public ICommand WaitingOutService { get; set; }
        public ICommand CookingFinish { get; set; }
        public ICommand CookingOutService { get; set; }
        public ICommand BtnCategories { get; set; }
        public ICommand BtnTakeAwayFood { get; set; }

        public ICommand BtnOutOfFood { get; set; }
        public ICommand PostOutOfFood { get; set; }

        public ICommand UpdateOutOfFood { get; set; }

        public ICommand CheckAllFoodingCommand { get; set; }
        public ICommand CheckAllFoodCommand { get; set; }


        public int currentButton;
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

        public Visibility _BtnSaveVisibility;
        public Visibility BtnSaveVisibility
        {
            get
            {
                return _BtnSaveVisibility;
            }
            set
            {
                _BtnSaveVisibility = value;

                OnPropertyChanged("BtnSaveVisibility");
            }
        }
        public Visibility _DatePickerVisibility;
        public Visibility DatePickerVisibility
        {
            get
            {
                return _DatePickerVisibility;
            }
            set
            {
                _DatePickerVisibility = value;

                OnPropertyChanged("DatePickerVisibility");
            }
        }
        private string _OrderDetailDoneContent;
        public string OrderDetailDoneContent
        {
            get
            {
                return _OrderDetailDoneContent;
            }
            set
            {
                _OrderDetailDoneContent = value;
                OnPropertyChanged("OrderDetailDoneContent");
            }
        }
        #region By Phan Viet Ha
        private long _fontsize;
        public long fontsize
        {
            get =>  _fontsize;
            set
            {
                _fontsize = value;
                OnPropertyChanged("fontsize");
            }
        }

        #endregion

        private string _OrderDetailCancelContent;
        public string OrderDetailCancelContent
        {
            get
            {
                return _OrderDetailCancelContent;
            }
            set
            {
                _OrderDetailCancelContent = value;
                OnPropertyChanged("OrderDetailCancelContent");
            }
        }
        public Visibility _RefreshVisibility;
        public Visibility RefreshVisibility
        {
            get
            {
                return _RefreshVisibility;
            }
            set
            {
                _RefreshVisibility = value;

                OnPropertyChanged("RefreshVisibility");
            }
        }

        private string warehouseOut;
        public string WarehouseOut
        {
            get
            {
                return warehouseOut;
            }
            set
            {
                warehouseOut = value;
                OnPropertyChanged("WarehouseOut");
            }
        }
        private string warehouseConfirms;
        public string WarehouseConfirms
        {
            get
            {
                return warehouseConfirms;
            }
            set
            {
                warehouseConfirms = value;
                OnPropertyChanged("WarehouseConfirms");
            }
        }
        private ObservableCollection<Category> itemsCategory;
        public ObservableCollection<Category> ItemsCategory
        {
            get
            {
                return itemsCategory;
            }
            set
            {
                itemsCategory = value;
                OnPropertyChanged("ItemsCategory");
            }
        }

        private ObservableCollection<OrderDetail> items_pending;
        public ObservableCollection<OrderDetail> ItemsPendings
        {
            get
            {
                return items_pending;
            }
            set
            {
                items_pending = value;
                OnPropertyChanged("ItemsPendings");
            }
        }

        private ObservableCollection<OrderDetail> _OrderDetailDoneList = new ObservableCollection<OrderDetail>();
        public ObservableCollection<OrderDetail> OrderDetailDoneList
        {
            get
            {

                return _OrderDetailDoneList;
            }
            set
            {
                _OrderDetailDoneList = value;
                OnPropertyChanged("OrderDetailDoneList");
            }
        }
        private ObservableCollection<OrderDetail> _OrderDetailCancelList = new ObservableCollection<OrderDetail>();
        public ObservableCollection<OrderDetail> OrderDetailCancelList
        {
            get
            {

                return _OrderDetailCancelList;
            }
            set
            {
                _OrderDetailCancelList = value;
                OnPropertyChanged("OrderDetailCancelList");
            }
        }
        private Visibility _SearchVisibility;
        public Visibility SearchVisibility { get => _SearchVisibility; set { _SearchVisibility = value; OnPropertyChanged("SearchVisibility"); } }
        //private ObservableCollection<MaterialDailyInventor> _DailyList = new ObservableCollection<MaterialDailyInventor>();
        //public ObservableCollection<MaterialDailyInventor> DailyList { get => _DailyList; set { _DailyList = value; OnPropertyChanged("DailyList"); } }

        private Visibility _SearchOutOfFoodVisibility;
        public Visibility SearchOutOfFoodVisibility { get => _SearchOutOfFoodVisibility; set { _SearchOutOfFoodVisibility = value; OnPropertyChanged("SearchOutOfFoodVisibility");   } }
        private ObservableCollection<OrderDetail> items;
        public ObservableCollection<OrderDetail> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        //public OutOfFood food = new OutOfFood();
        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
        private Kitchen currentKitchen;
        private UpdateFoodKitchenWrapper currentBranch;

        private static ContentControl _MainContentControl;
        public bool IsAddHistory = false;
        public long inventoryId = 0;
        public long CurrentCategoryId;
        private DateTime _DatetimeInput;
        public DateTime DatetimeInput { get => _DatetimeInput; set { _DatetimeInput = value; OnPropertyChanged("DatetimeInput"); } }
        private DateTime _DateTimeEnd;
        public DateTime DateTimeEnd { get => _DateTimeEnd; set { _DateTimeEnd = value; OnPropertyChanged("DateTimeEnd"); } }
        private ImageBrush _PendingBackground { get; set; }
        public ImageBrush PendingBackground { get => _PendingBackground; set { _PendingBackground = value; OnPropertyChanged("PendingBackground"); } }
        private ImageBrush _AllFoodBackground { get; set; }
        public ImageBrush AllFoodBackground { get => _AllFoodBackground; set { _AllFoodBackground = value; OnPropertyChanged("AllFoodBackground"); } }
        private ImageBrush _HistoryBackground { get; set; }
        public ImageBrush HistoryBackground { get => _HistoryBackground; set { _HistoryBackground = value; OnPropertyChanged("HistoryBackground"); } }
        private ImageBrush _TakeAwayBackground { get; set; }
        public ImageBrush TakeAwayBackground { get => _TakeAwayBackground; set { _TakeAwayBackground = value; OnPropertyChanged("TakeAwayBackground"); } }
        private ImageBrush _OutOfStockBackground;
        public ImageBrush OutOfStockBackground { get => _OutOfStockBackground; set { _OutOfStockBackground = value; OnPropertyChanged("OutOfStockBackground"); } }
        //private ImageBrush _CookingBackground { get; set; }
        //public ImageBrush CookingBackground { get => _CookingBackground; set { _CookingBackground = value; OnPropertyChanged("CookingBackground"); } }
        //private ImageBrush _ReturnFoodBackground { get; set; }
        //public ImageBrush ReturnFoodBackground { get => _ReturnFoodBackground; set { _ReturnFoodBackground = value; OnPropertyChanged("ReturnFoodBackground"); } }


        public List<OrderDetail> OrderDetailPending = new List<OrderDetail>();
        public List<OrderDetail> OrderDetailConfirm = new List<OrderDetail>();
        //    public List<OrderDetailRealTimeLocation> OrderDetailRealTimeLocationList = new List<OrderDetailRealTimeLocation>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        #region QBy Phan Viet Ha
        public async void UpdateStockFood(int brandid,List<int> foodids)
        {

            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            BaseResponse response = await Task.Run(() => client.UpdateFoodOutOfStock(brandid,foodids));
            UpdateFoodIsOutStockWrapper wrapper = new UpdateFoodIsOutStockWrapper(brandid, foodids);
            foreach (OutOfFood food in AllFood)
            {
                food.RestaurantBrandId = Convert.ToInt32(brandid);
                foodids.Add(food.FoodId);
            }
            DialogHostOpen = false;



        }
        public async void  GetOutStockFood()
        {
            if(ListData != null)
            {
                ListData.Clear();
            }
            else
            {
                ListData = new ObservableCollection<OutOfFood>();
            }
            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            OutOfFoodResponse response = await Task.Run(() => client.GetOutOfFoodWorking(currentUser.BranchId, 0, "", currentKitchen.Id));
            if(response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                    foreach (OutOfFood food in response.Data.List)
                    {
                        AllFood.Add(food);
                        AllFood= ListData;
                    }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, AllFood, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                DialogHostOpen = false;

            }
            else
            {
                DialogHostOpen = false;
            }
        }
        public async void GetOutStockFood1()
        {
            if (ListData != null)
            {
                ListData.Clear();
            }
            else
            {
                ListData = new ObservableCollection<OutOfFood>();
            }
            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            OutOfFoodResponse response = await Task.Run(() => client.GetOutOfFoodWorking(currentUser.BranchId, 0, "", currentKitchen.Id));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach (OutOfFood food in response.Data.List)
                {
                    AllFood.Add(food);
                    AllFood = ListData;
                }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, AllFood, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                DialogHostOpen = false;

            }
            else
            {
                DialogHostOpen = false;
            }
        }
        public async void GetOutOfFoodNotWorking()
        {
            if(ListDataNotWorking != null)
            {
                ListDataNotWorking.Clear();
            }
            else
            {
                ListDataNotWorking = new ObservableCollection<OutOfFood>();
            }

            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            OutOfFoodResponse response = await Task.Run(() => client.GetOutOfFoodNotWorking(currentUser.BranchId,1, "", currentKitchen.Id));

            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach(OutOfFood foodOutStock in response.Data.List)
                {
                    FoodOutStock.Add(foodOutStock);
                    //FoodOutStock = ListDataNotWorking;
                    ListDataNotWorking = FoodOutStock; // dat
                }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, FoodOutStock, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                DialogHostOpen = false;
            }

            else
            {
                DialogHostOpen = false;
            }
        }
        #endregion
        public void GetAllCategoryList()
        {
            if (ItemsCategory != null)
            {
                ItemsCategory.Clear();
            }
            else
            {
                ItemsCategory = new ObservableCollection<Category>();
            }
            DialogHostOpen = true;
            CategoriesClient client = new CategoriesClient(this, this, this);
            CategoryResponse categoryReponse = client.GetCategoryByBranch(currentUser.BranchId, Constants.STATUS_CATEGORY_TYPE_COOK);
            if (categoryReponse != null && categoryReponse.Status == (int)ResponseEnum.OK)
            {
                Category category = new Category();
                category.Id = (long)CategoryTypeEnum.ALL;
                category.Name = MessageValue.MESSAGE_ALL;
                category.IsChoose = true;
                categoryReponse.Data.Insert(0, category);
                CurrentCategoryId = category.Id;
                Category categoryOther = new Category();
                categoryOther.Id = (long)CategoryTypeEnum.OTHER_MENU;
                categoryOther.Name = MessageValue.MESSAGE_CATEGORY_OTHER_MENU;
                categoryOther.IsChoose = false;
                categoryReponse.Data.Add(categoryOther);
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_CATEGORY, categoryReponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));

                categoryReponse.Data.ForEach(ItemsCategory.Add);

                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RefreshVisibility = Visibility.Visible;
            dispatcherTimer.Stop();
        }
        protected async void LoadListAllFood()
        {
            if (ItemsPendings != null)
            {
                ItemsPendings.Clear();
            }
            else
            {
                ItemsPendings = new ObservableCollection<OrderDetail>();
            }
            if (Items != null)
            {
                Items.Clear();
            }
            else
            {
                Items = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailPending != null)
            {
                OrderDetailPending.Clear();
            }
            else
            {
                OrderDetailPending = new List<OrderDetail>();
            }
            if (OrderDetailConfirm != null)
            {
                OrderDetailConfirm.Clear();
            }
            else
            {
                OrderDetailConfirm = new List<OrderDetail>();
            }
            DialogHostOpen = true;
            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrderDetailResponse waitingResponse = await Task.Run(() => client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.COOKING), Constants.STATUS_IS_NOT_APPROVED, "", "", string.Format("{0}", string.Format("{0},{1},{2}", (int)OrderMethodEnum.EAT_IN, (int)OrderMethodEnum.TAKE_AWAY, (int)OrderMethodEnum.ONLINE_DELIVERY)), currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_COOK));
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {
                int indexPending = 0;
                int indexCooking = 0;
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                            indexPending++;
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.COOKING)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                            indexCooking++;
                        }
                    }
                }
                WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        protected async void LoadListWaitingFood()
        {
            if (ItemsPendings != null)
            {
                ItemsPendings.Clear();
            }
            else
            {
                ItemsPendings = new ObservableCollection<OrderDetail>();
            }
            if (Items != null)
            {
                Items.Clear();
            }
            else
            {
                Items = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailPending != null)
            {
                OrderDetailPending.Clear();
            }
            else
            {
                OrderDetailPending = new List<OrderDetail>();
            }
            if (OrderDetailConfirm != null)
            {
                OrderDetailConfirm.Clear();
            }
            else
            {
                OrderDetailConfirm = new List<OrderDetail>();
            }
            DialogHostOpen = true;
            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrderDetailResponse waitingResponse = await Task.Run(() => client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.COOKING), Constants.STATUS_IS_NOT_APPROVED, "", "", string.Format("{0}", (int)OrderMethodEnum.EAT_IN), currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_COOK));
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {

                int indexPending = 0;
                int indexCooking = 0;
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                            indexPending++;
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.COOKING)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                            indexCooking++;
                        }
                    }
                }
                WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        protected async void GetOrderDetailTakAway()
        {
            if (ItemsPendings != null)
            {
                ItemsPendings.Clear();
            }
            else
            {
                ItemsPendings = new ObservableCollection<OrderDetail>();
            }
            if (Items != null)
            {
                Items.Clear();
            }
            else
            {
                Items = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailPending != null)
            {
                OrderDetailPending.Clear();
            }
            else
            {
                OrderDetailPending = new List<OrderDetail>();
            }
            if (OrderDetailConfirm != null)
            {
                OrderDetailConfirm.Clear();
            }
            else
            {
                OrderDetailConfirm = new List<OrderDetail>();
            }
            DialogHostOpen = true;
            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrderDetailResponse waitingResponse = await Task.Run(() => client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.COOKING), Constants.STATUS_IS_NOT_APPROVED,  "", "", string.Format("{0},{1}", (int)OrderMethodEnum.ONLINE_DELIVERY, (int)OrderMethodEnum.TAKE_AWAY), currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_COOK));
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {

                int indexPending = 0;
                int indexCooking = 0;
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                            indexPending++;
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.COOKING)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                            indexCooking++;
                        }
                    }
                }
                WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }

        private void RealTime()
        {
            if (Constants.IS_NETWORK_ONLINE)
            {
                RealTimeOnline();
            }
            else
            {
                RealTimeOffline();
            }
        }

        public async void RealTimeOffline()
        {
            try
            {
                //if (Constants.CLIENT_WEB_SOCKET == null || Constants.CLIENT_WEB_SOCKET.State == WebSocketState.Closed || Constants.CLIENT_WEB_SOCKET.State == WebSocketState.Aborted)
                if (Constants.CLIENT_WEB_SOCKET == null || Constants.CLIENT_WEB_SOCKET.State != WebSocketState.Open)
                {
                    Constants.CLIENT_WEB_SOCKET = new ClientWebSocket();
                    TimeSpan timeSpan = new TimeSpan(12, 0, 0);
                    var timeOut = new CancellationTokenSource(timeSpan).Token;
                    await Constants.CLIENT_WEB_SOCKET.ConnectAsync(new Uri(Constants.WEBSOCKET_DOMAIN), timeOut);
                    await Task.WhenAll(Receive(Constants.CLIENT_WEB_SOCKET));
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

        }
        private async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer;
            while (webSocket.State == WebSocketState.Open)
            {
                buffer = new byte[2048];
                TimeSpan timeSpan = new TimeSpan(12, 0, 0);
                 var timeOut = new CancellationTokenSource(timeSpan).Token;
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), timeOut);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    try
                    {
                        // WriteLog.logs(buffer.Length);
                        string data = Encoding.UTF8.GetString(buffer, 0, buffer.Length).TrimEnd('\0');
                        if (data == null) return;
                        WriteLog.logs(data);
                        string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
                        WriteLog.logs(LinkSocketWaiting);
                        //string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                        //string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
                        if (data.Contains(LinkSocketWaiting))
                        {
                            if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY || currentButton == (int)OrderDetailButtonEnum.All)
                            {
                                BaseRealtimeModel baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModel>(data);
                                if (baseRealtimeModelOrder != null)
                                {
                                    OrderDetailReatime orderDetail = baseRealtimeModelOrder.Value;
                                    if (orderDetail != null)
                                    {
                                        if (orderDetail.ObjectData != null)
                                        {
                                            dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
                                            OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
                                            if (od != null && !od.isBBQ)
                                            {
                                                AddOrderDetailToRealTime(od);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        //else if (data.Contains(LinkSocketOrder))
                        //{
                        //    BaseRealtimeModelOrder baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModelOrder>(data);
                        //    if (baseRealtimeModelOrder != null)
                        //    {
                        //        if (currentButton == (int)OrderDetailButtonEnum.All)
                        //        {
                        //            OrderRealTime order = baseRealtimeModelOrder.Value;
                        //            if (order != null)
                        //            {
                        //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    LoadListAllFood();
                        //                }
                        //            }
                        //        }
                        //        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        //        {
                        //            //     string JsonString = data.ToString();
                        //            OrderRealTime order = baseRealtimeModelOrder.Value;
                        //            if (order != null)
                        //            {
                        //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    LoadListWaitingFood();
                        //                }
                        //            }
                        //        }
                        //        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        //        {
                        //            OrderRealTime order = baseRealtimeModelOrder.Value;
                        //            if (order != null)
                        //            {
                        //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    GetOrderDetailTakAway();
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (data.Contains(LinkSocketMoveFood))
                        //{
                        //    BaseRealtimeModel baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModel>(data);
                        //    if (baseRealtimeModelOrder != null)
                        //    {
                        //        if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.All || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        //        {
                        //            OrderDetailReatime orderDetail = baseRealtimeModelOrder.Value;
                        //            if (orderDetail != null)
                        //            {
                        //                if (orderDetail.ObjectData != null)
                        //                {
                        //                    dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
                        //                    OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
                        //                    if (od != null && !od.isBBQ && (od.CategoryType == (int)CategoryTypeEnum.FOOD || od.CategoryType == (int)CategoryTypeEnum.SEA_FOOD))
                        //                    {
                        //                        AddOrderDetailToRealTime(od);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //string LinkSocketNotifications = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId,currentKitchen.Id);
                        //if (data.Contains(LinkSocketNotifications))
                        //{
                        //    BaseRealtimeModelNotifications baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModelNotifications>(data);
                        //    if (baseRealtimeModelOrder != null)
                        //    {
                        //        NotificationRealTime notification = baseRealtimeModelOrder.Value;
                        //        NotificationMessage.ShowInfomation(notification.Title, notification.Content, notification.CreatedAt);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.Message);
                    }
                }
            }
        }

        private void RealTimeOnline()
        {
            string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);

            SocketIOManager.Instance.socket.On(LinkSocketWaiting, (data) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY || currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        //string JsonString = data.ToString();
                        WriteLog.logs(data.ToString());
                        //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
                        //OrderDetailReatime orderDetail = jsonResponse.ToObject<OrderDetailReatime>();
                        SocketIOManager.Instance.socket.JsonSerializer = new NewtonsoftJsonSerializer();
                        OrderDetailReatime orderDetail = data.GetValue<OrderDetailReatime>(0);
                        if (orderDetail != null)
                        {
                            if (orderDetail.ActionType == 1 || orderDetail.ActionType == 2 || orderDetail.ActionType == 3 || orderDetail.ActionType == 4 ||
                                orderDetail.ActionType == 5 || orderDetail.ActionType == 6 || orderDetail.ActionType == 7)
                            {
                                /*
                             ADD_FOOD(1),  -- Thêm món, tặng món, thêm món ngoài menu
                            UPDATE_FOOD(2), --Update ghi chú, số lượng
                            CANCEL_FOOD(3),  -- Hủy món theo số lượng hoặc hủy hết
                            MOVE_FOOD(5), -- Chuyển món/chia món
                            MERGE_ORDER(6), -- Gộp bàn
                            RETURN_DRINK(7), -- Trả bia
                            APPROVE_DRINK(8), -- Xác nhận bia
                            CHANGE_ORDER_DETAIL_STATUS(9) -- Đổi trạng thái khác (chuyển nấu, chuyển hoàn tất, ra bia, hết món)
                             */
                                if (currentButton == (int)OrderDetailButtonEnum.All || currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                                {
                                    LoadListWaitingFood();
                                }
                            }
                        }
                    }
                });
            });
            #region Command Socket
            //string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
            ////SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketOrder);
            //SocketIOManager.Instance.socket.On(LinkSocketOrder, (data) =>
            //{
            //    Application.Current.Dispatcher.Invoke((Action)delegate
            //    {
            //        if (currentButton == (int)OrderDetailButtonEnum.PENDING)
            //        {
            //            //string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //            if (order != null)
            //            {
            //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                {
            //                    LoadListWaitingFood();
            //                }
            //            }
            //        }
            //        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
            //        {
            //            //     string JsonString = data.ToString();
            //            //WriteLog.logs(data.ToString());
            //            Console.WriteLine(data);
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //            if (order != null)
            //            {
            //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                {
            //                    GetOrderDetailTakAway();
            //                }
            //            }
            //        }
            //        else if (currentButton == (int)OrderDetailButtonEnum.All)
            //        {
            //            //     string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //Console.WriteLine(data.GetValue<OrderRealTime>());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //            if (order != null)
            //            {
            //                if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                {
            //                    LoadListAllFood();
            //                }
            //            }
            //        }
            //    });
            //});
            //string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
            ////SocketIOManager.Instance.socket.EmitAsync("join_room", LinkSocketMoveFood);
            //SocketIOManager.Instance.socket.On(LinkSocketMoveFood, (data) =>
            //{
            //    Application.Current.Dispatcher.Invoke((Action)delegate
            //    {
            //        if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY || currentButton == (int)OrderDetailButtonEnum.All)
            //        {
            //            //string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderDetailReatime orderDetail = jsonResponse.ToObject<OrderDetailReatime>();
            //            OrderDetailReatime orderDetail = data.GetValue<OrderDetailReatime>(0);
            //            if (orderDetail != null)
            //            {
            //                if (orderDetail.ObjectData != null)
            //                {
            //                    dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
            //                    OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
            //                    if (od != null && !od.isBBQ && (od.CategoryType == (int)CategoryTypeEnum.FOOD || od.CategoryType == (int)CategoryTypeEnum.SEA_FOOD))
            //                    {
            //                        AddOrderDetailToRealTime(od);
            //                    }
            //                }
            //            }
            //        }
            //    });
            //});
            #endregion

        }
        public void AddOrderDetailHistory(OrderDetail orderDetail)
        {
            if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE)
            {
                OrderDetail od = OrderDetailDoneList.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                if (od == null)
                {
                    OrderDetailDoneList.Insert(0, orderDetail);

                }
                else
                {
                    int index = OrderDetailDoneList.IndexOf(od);
                    OrderDetailDoneList.Remove(od);
                    OrderDetailDoneList.Insert(index, orderDetail);
                }
                OrderDetailDoneContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM, OrderDetailDoneList.Count());
            }
            else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL || orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK)
            {
                OrderDetail od = OrderDetailCancelList.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                if (od == null)
                {
                    OrderDetailCancelList.Insert(0, orderDetail);
                }
                else
                {
                    int index = OrderDetailCancelList.IndexOf(od);
                    OrderDetailCancelList.Remove(od);
                    OrderDetailCancelList.Insert(index, orderDetail);
                }
                OrderDetailCancelContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CANCEL, OrderDetailCancelList.Count());
            }
        }
        public void AddOrderDetailPending(OrderDetail o)
        {
            if (o.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
            {
                OrderDetail orderDetailItem = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                if (orderDetailItem == null)
                {
                    OrderDetail od = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                    if (od == null)
                    {
                        ItemsPendings.Add(o);
                    }
                    else
                    {
                        int index = ItemsPendings.IndexOf(od);
                        ItemsPendings.Remove(od);
                        ItemsPendings.Insert(index, o);
                    }

                }
                else
                {
                    int index = Items.IndexOf(orderDetailItem);
                    Items.Remove(orderDetailItem);
                    o.OrderDetailStatus = orderDetailItem.OrderDetailStatus;
                    o.OrderDetailStatusName = orderDetailItem.OrderDetailStatusName;
                    Items.Insert(index, o);
                }
            }
            OrderDetail orderDetail = OrderDetailConfirm.Where(x => x.Id == o.Id).FirstOrDefault();
            if (orderDetail == null)
            {
                OrderDetail odt = OrderDetailPending.Where(x => x.Id == o.Id).FirstOrDefault();
                if (odt == null)
                {
                    OrderDetailPending.Add(o);
                }
                else
                {
                    int index = OrderDetailPending.IndexOf(odt);
                    OrderDetailPending.Remove(odt);
                    OrderDetailPending.Insert(index, o);
                }
            }
            else
            {
                int index = OrderDetailConfirm.IndexOf(orderDetail);
                OrderDetailConfirm.Remove(orderDetail);
                o.OrderDetailStatus = orderDetail.OrderDetailStatus;
                o.OrderDetailStatusName = orderDetail.OrderDetailStatusName;
                OrderDetailConfirm.Insert(index, o);
            }
            WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
        }
        public void AddOrderDetailToRealTime(OrderDetail orderDetail)
        {
            if (ItemsPendings == null)
            {
                ItemsPendings = new ObservableCollection<OrderDetail>();
            }
            if (Items == null)
            {
                Items = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailDoneList == null)
            {
                OrderDetailDoneList = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailCancelList == null)
            {
                OrderDetailCancelList = new ObservableCollection<OrderDetail>();
            }
            if (OrderDetailPending == null)
            {
                OrderDetailPending = new List<OrderDetail>();
            }
            if (OrderDetailConfirm == null)
            {
                OrderDetailConfirm = new List<OrderDetail>();
            }
            //if (OrderDetailRealTimeLocationList == null)
            //{
            //    OrderDetailRealTimeLocationList = new List<OrderDetailRealTimeLocation>();
            //}
            if (orderDetail != null)
            {
                if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                {
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        AddOrderDetailPending(orderDetail);
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        if (orderDetail.IsOnline == (int)StatusEnum.NOT && orderDetail.IsTakeAway == (int)StatusEnum.NOT)
                        {
                            AddOrderDetailPending(orderDetail);
                        }
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        if (orderDetail.IsOnline != (int)StatusEnum.NOT || orderDetail.IsTakeAway != (int)StatusEnum.NOT)
                        {
                            AddOrderDetailPending(orderDetail);
                        }
                    }
                }
                else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.COOKING)
                {
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        if (orderDetail.IsOnline == (int)StatusEnum.NOT)
                        {
                            if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                            {
                                OrderDetail od = Items.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                                if (od == null)
                                {
                                    Items.Add(orderDetail);
                                }
                                else
                                {
                                    int index = Items.IndexOf(od);
                                    Items.Remove(od);
                                    Items.Insert(index, orderDetail);
                                }
                            }
                            OrderDetail o = OrderDetailConfirm.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (o == null)
                            {
                                OrderDetailConfirm.Add(orderDetail);
                            }
                            else
                            {
                                int index = OrderDetailConfirm.IndexOf(o);
                                OrderDetailConfirm.Remove(o);
                                OrderDetailConfirm.Insert(index, orderDetail);
                            }
                            WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                        }
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        if (orderDetail.IsOnline == (int)StatusEnum.NOT && orderDetail.IsTakeAway == (int)StatusEnum.NOT)
                        {
                            if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                            {
                                OrderDetail od = Items.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                                if (od == null)
                                {
                                    Items.Add(orderDetail);
                                }
                                else
                                {
                                    int index = Items.IndexOf(od);
                                    Items.Remove(od);
                                    Items.Insert(index, orderDetail);
                                }
                            }
                            OrderDetail o = OrderDetailConfirm.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (o == null)
                            {
                                OrderDetailConfirm.Add(orderDetail);
                            }
                            else
                            {
                                int index = OrderDetailConfirm.IndexOf(o);
                                OrderDetailConfirm.Remove(o);
                                OrderDetailConfirm.Insert(index, orderDetail);
                            }
                            WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                        }
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        if (orderDetail.IsOnline != (int)StatusEnum.NOT || orderDetail.IsTakeAway != (int)StatusEnum.NOT)
                        {
                            if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                            {
                                OrderDetail od = Items.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                                if (od == null)
                                {
                                    Items.Add(orderDetail);
                                }
                                else
                                {
                                    int index = Items.IndexOf(od);
                                    Items.Remove(od);
                                    Items.Insert(index, orderDetail);
                                }
                            }
                            OrderDetail o = OrderDetailConfirm.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (o == null)
                            {
                                OrderDetailConfirm.Add(orderDetail);
                            }
                            else
                            {
                                int index = OrderDetailConfirm.IndexOf(o);
                                OrderDetailConfirm.Remove(o);
                                OrderDetailConfirm.Insert(index, orderDetail);
                            }
                            WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                        }
                    }
                }
                else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE)
                {
                    AddOrderDetailHistory(orderDetail);
                }
                else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK || orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL)
                {
                    OrderDetail od = Items.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                    if (od != null)
                    {
                        Items.Remove(od);
                    }
                    OrderDetail o = ItemsPendings.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                    if (o != null)
                    {
                        ItemsPendings.Remove(o);
                    }
                    OrderDetail orderDetail1 = OrderDetailPending.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                    if (orderDetail1 != null)
                    {
                        OrderDetailPending.Remove(od);
                    }
                    OrderDetail orderDetail2 = OrderDetailConfirm.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                    if (OrderDetailConfirm != null)
                    {
                        OrderDetailConfirm.Remove(o);
                    }
                    WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                    WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                    AddOrderDetailHistory(orderDetail);
                }
            }
        }
        private async void ChangeStatusPendingToConfirm(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                if (Items == null)
                {
                    Items = new ObservableCollection<OrderDetail>();
                }
                ItemsPendings.Remove(orderDetail);
                OrderDetailPending.Remove(orderDetail);
                WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.COOKING,0);
                BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                if (baseResponse == null || baseResponse.Status != (int)ResponseEnum.OK)
                {

                    IsAddHistory = true;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }

                }
                else
                {
                    Items.Add(orderDetail);
                    OrderDetailConfirm.Add(orderDetail);
                    WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);

                }
            }
        }
        private async void GetWaitingOut(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                if (orderDetail.FoodType != Constants.STATUS_FOOD_TYPE_BBQ)
                {
                    //Remove object orderdetail out list
                    ItemsPendings.Remove(orderDetail);
                    OrderDetailPending.Remove(orderDetail);
                    WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.OUTSTOCK,0);
                    BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                    if (baseResponse == null || baseResponse.Status != (int)ResponseEnum.OK)
                    {
                        IsAddHistory = true;
                        //call api refresh list waiting food in case call api update status error
                        if (currentButton == (int)OrderDetailButtonEnum.All)
                        {
                            LoadListAllFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        {
                            LoadListWaitingFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            GetOrderDetailTakAway();
                        }
                    }

                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CANCEL_BBQ);
                }
            }
        }
        private async void GetWaitingOutOfStock(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                if (orderDetail.FoodType != Constants.STATUS_FOOD_TYPE_BBQ)
                {
                    //Remove object orderdetail out list
                    ItemsPendings.Remove(orderDetail);
                    OrderDetailPending.Remove(orderDetail);
                    WarehouseOut = string.Format(MessageValue.MESSAGE_COOKING_WAIT, ItemsPendings.Count);
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.OUTSTOCK, 1);
                    BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                    if (baseResponse == null || baseResponse.Status != (int)ResponseEnum.OK)
                    {
                        IsAddHistory = true;
                        //call api refresh list waiting food in case call api update status error
                        if (currentButton == (int)OrderDetailButtonEnum.All)
                        {
                            LoadListAllFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        {
                            LoadListWaitingFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            GetOrderDetailTakAway();
                        }
                    }

                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CANCEL_BBQ);
                }
            }
        }
        private async void GetCookFinish(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                // remove item before call api update to server
                Items.Remove(orderDetail);
                OrderDetailConfirm.Remove(orderDetail);
                WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.DONE,0);
                BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                if (baseResponse != null)
                {
                    if (baseResponse.Status == (int)ResponseEnum.OK)
                    {
                     //   DeviceClient deviceClient = new DeviceClient();
                       // DeviceConfigWrapper device = deviceClient.RealConfigs();
                        if (currentKitchen != null && currentKitchen.IsHavePrinter && !string.IsNullOrEmpty(currentKitchen.PrinterName))
                        {
                            PrintText printFood = new PrintText();
                            printFood.PrintCook(orderDetail, currentKitchen.PrinterName, currentKitchen.PrinterPaperSize);
                        }

                    }
                    else
                    {
                        IsAddHistory = true;
                        if (currentButton == (int)OrderDetailButtonEnum.All)
                        {
                            LoadListAllFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        {
                            LoadListWaitingFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            GetOrderDetailTakAway();
                        }
                    }
                }
                else
                {
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                }
            }
        }
        private async void GetCookOut(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {

                if (orderDetail.FoodType != Constants.STATUS_FOOD_TYPE_BBQ)
                {
                    Items.Remove(orderDetail);
                    OrderDetailConfirm.Remove(orderDetail);
                    WarehouseConfirms = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM_WAIT, Items.Count);
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.OUTSTOCK,0);
                    BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                    if (baseResponse == null || baseResponse.Status != (int)ResponseEnum.OK)
                    {
                        IsAddHistory = true;
                        //Call api refresh list cooking in case api update status order detail error
                        if (currentButton == (int)OrderDetailButtonEnum.All)
                        {
                            LoadListAllFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        {
                            LoadListWaitingFood();
                        }
                        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            GetOrderDetailTakAway();
                        }
                    }
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_CANCEL_BBQ);
                }

            }
        }
        protected async void GetFoodList(DateTime DayTime)
        {
            if (OrderDetailCancelList == null)
            {
                OrderDetailCancelList = new ObservableCollection<OrderDetail>();
            }
            else
            {
                OrderDetailCancelList.Clear();
            }
            if (OrderDetailDoneList == null)
            {
                OrderDetailDoneList = new ObservableCollection<OrderDetail>();
            }
            else
            {
                OrderDetailDoneList.Clear();
            }
            OrderDetailDoneContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM, OrderDetailDoneList.Count());
            OrderDetailCancelContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CANCEL, OrderDetailCancelList.Count());
            DialogHostOpen = true;
            string fromDate = Utils.Utils.GetDateFormatVN(DayTime != null ? DayTime : DateTime.Now);
            string toDate = Utils.Utils.GetDateFormatVN(DayTime != null ? DayTime : DateTime.Now);
            OrderDetailsClient orderDetailsClient = new OrderDetailsClient(this, this, this);
            OrderDetailResponse response = await Task.Run(() => orderDetailsClient.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1},{2}", (int)OrderDetailStatusEnum.CANCEL, (int)OrderDetailStatusEnum.DONE, (int)OrderDetailStatusEnum.OUTSTOCK), Constants.STATUS_IS_NOT_APPROVED, fromDate, toDate, string.Format("{0},{1},{2}", (int)OrderMethodEnum.EAT_IN, (int)OrderMethodEnum.ONLINE_DELIVERY, (int)OrderMethodEnum.TAKE_AWAY), currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_COOK));
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                response.Data.List.Where(x => x.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE).ForEach(OrderDetailDoneList.Add);
                OrderDetailDoneContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRM, OrderDetailDoneList.Count());
                response.Data.List.Where(x => x.OrderDetailStatus == (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus == (int)OrderDetailStatusEnum.OUTSTOCK).ForEach(OrderDetailCancelList.Add);
                OrderDetailCancelContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CANCEL, OrderDetailCancelList.Count());
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {

            if (e.IsAvailable)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    DialogHostOpen = false;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingFood();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.HISTORY)
                    {
                        GetFoodList(DatetimeInput != null ? DatetimeInput : DateTime.Now);

                    }
                });
            }
            else
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    DialogHostOpen = true;
                });
            }
        }
        public void ChangeNetWork()
        {
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }
        public DateTime currentDateTime;
        public void GetOrderDetailByCategory(long categoryId)
        {
            if (ItemsPendings != null)
            {
                ItemsPendings.Clear();
            }
            else
            {
                ItemsPendings = new ObservableCollection<OrderDetail>();
            }
            if (Items != null)
            {
                Items.Clear();
            }
            else
            {
                Items = new ObservableCollection<OrderDetail>();
            }
            if (categoryId == Constants.ALL)
            {
                OrderDetailPending.ForEach(ItemsPendings.Add);
                OrderDetailConfirm.ForEach(Items.Add);
            }
            else
            {
                OrderDetailPending.Where(x => x.CategoryId == categoryId).ForEach(ItemsPendings.Add);
                OrderDetailConfirm.Where(x => x.CategoryId == categoryId).ForEach(Items.Add);
            }
            if (CurrentCategoryId != categoryId)
            {
                Category category = ItemsCategory.Where(x => x.Id == CurrentCategoryId).FirstOrDefault();
                if (category != null)
                {
                    int index = ItemsCategory.IndexOf(category);
                    ItemsCategory.Remove(category);
                    category.IsChoose = false;
                    ItemsCategory.Insert(index, category);
                }
                Category c = ItemsCategory.Where(x => x.Id == categoryId).FirstOrDefault();
                if (c != null)
                {
                    int index = ItemsCategory.IndexOf(c);
                    ItemsCategory.Remove(c);
                    c.IsChoose = true;
                    ItemsCategory.Insert(index, c);
                }
                CurrentCategoryId = categoryId;
            }
        }
        public CookViewModel()
        {
            //if(filterSearch != null)
            //{
            //    filterSearch = "";
            //}
            if(SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
            {
                fontsize = 14;
            }
            else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366)
            {
                fontsize = 12;
            }
            else
            {
                fontsize = 11;
            }

            try
            {

                currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
                currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);
                currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
                CurrentCategoryId = Constants.ALL;
                if (currentUser != null)
                {
                    RealTime();
                    LoadedUserControlCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        IsAddHistory = true;
                        currentButton = (int)OrderDetailButtonEnum.All;
                        SearchVisibility = Visibility.Visible;
                        RefreshVisibility = Visibility.Visible;
                        BtnSaveVisibility = Visibility.Collapsed;
                        DatePickerVisibility = Visibility.Collapsed;
                        DatetimeInput = DateTime.Now;
                        currentDateTime = DateTime.Now;
                        DateTimeEnd = DateTime.Now;
                        currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
                        SearchOutOfFoodVisibility = Visibility.Collapsed;

                        AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                        TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                        _MainContentControl = p.FindName("ContentCook") as ContentControl;
                        _MainContentControl.Content = new FoodingUserControl();
                        LoadListAllFood();
                        ChangeNetWork();
                    });
                    BtnAllFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (currentButton != (int)OrderDetailButtonEnum.All)
                        {
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchOutOfFoodVisibility = Visibility.Collapsed;

                            _MainContentControl.Content = new FoodingUserControl();

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            //OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                          LoadListAllFood();
                        }
                        currentButton = (int)OrderDetailButtonEnum.All;
                    });
                    BtnTakeAwayFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (currentButton != (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchOutOfFoodVisibility = Visibility.Collapsed;

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            _MainContentControl.Content = new FoodingUserControl();
                            GetOrderDetailTakAway();
                        }
                        currentButton = (int)OrderDetailButtonEnum.TAKE_AWAY;
                        NotificationMessage.Infomation("Chức năng này đang được phát triển thêm");
                    });
                    BtnCategories = new RelayCommand<Category>((p) => { return true; }, p =>
                    {
                        if (p != null)
                        {
                            GetOrderDetailByCategory(p.Id);
                        }
                    });
                    RefreshRealtimeCommand = new RelayCommand<Button>((p) => { return true; }, p =>
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            currentButton = (int)OrderDetailButtonEnum.All;

                            SearchVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            _MainContentControl.Content = new FoodingUserControl();
                            SearchOutOfFoodVisibility = Visibility.Collapsed;

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));
                            LoadListAllFood();

                            if (!Constants.IS_NETWORK_ONLINE)
                            {
                                RealTimeOffline();
                            }
                            else
                            {
                                RefreshVisibility = Visibility.Collapsed;
                                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                                dispatcherTimer.Interval = new TimeSpan(0, 3, 0);
                                dispatcherTimer.Start();
                            }
                        });
                    });
                    BtnHistoryFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {

                        SearchVisibility = Visibility.Visible;
                        RefreshVisibility = Visibility.Collapsed;
                        BtnSaveVisibility = Visibility.Collapsed;
                        DatePickerVisibility = Visibility.Visible;
                        SearchOutOfFoodVisibility = Visibility.Collapsed;

                        _MainContentControl.Content = new HistoryOrderDetailUserControl();

                        AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                        PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                        HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done-active.png")));
                        TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                        OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                        if (currentButton != (int)OrderDetailButtonEnum.HISTORY)
                        {
                            GetFoodList(DatetimeInput);
                        }
                        currentButton = (int)OrderDetailButtonEnum.HISTORY;

                    });
                    SelectionDateChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (DatetimeInput != currentDateTime)
                        {
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Visible;
                            currentButton = (int)OrderDetailButtonEnum.HISTORY;
                            SearchOutOfFoodVisibility = Visibility.Collapsed;

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done-active.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            _MainContentControl.Content = new HistoryOrderDetailUserControl();
                            GetFoodList(DatetimeInput != null ? DatetimeInput : DateTime.Now);
                            currentDateTime = DatetimeInput != null ? DatetimeInput : DateTime.Now;
                        }
                    });
                    BtWaitingFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        //currentButton = (int)OrderDetailButtonEnum.PENDING;
                        if (currentButton != (int)OrderDetailButtonEnum.PENDING)
                        {
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            _MainContentControl.Content = new FoodingUserControl();
                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));
                            SearchOutOfFoodVisibility = Visibility.Collapsed;
                            LoadListWaitingFood();
                        }
                        currentButton = (int)OrderDetailButtonEnum.PENDING;
                    });
                    BtnOutOfFood = new RelayCommand<UserControl>((p) => { return true; }, p => {
                        //if(currentButton != (int)OrderDetailButtonEnum.OUT_STOCK)
                        //{
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchOutOfFoodVisibility = Visibility.Collapsed;
                            currentButton = (int)OrderDetailButtonEnum.OUT_STOCK;
                            _MainContentControl.Content = new FoodOutStockUC();

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            //OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock.png")));

                            IsOutStock = 0;
                            if (AllFood.Count != 0)
                            {
                                AllFood.Clear();

                            }
                            GetOutStockFood();
                            GetOutOfFoodNotWorking();
                        //}
                        //currentButton = (int)OrderDetailButtonEnum.OUT_STOCK;

                    });
                    PostOutOfFood = new RelayCommand<OutOfFood>((p) => { return true; }, (p) => {

                        List<int> lstFoodIds = new List<int>();
                        lstFoodIds.Add(p.FoodId);
                       // var intlist = p.FoodId.ToString().Select(x => Convert.ToInt32(x.ToString())).ToList();
                        UpdateFoodIsOutStockWrapper wrapper = new UpdateFoodIsOutStockWrapper(p.BranchId, lstFoodIds);
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = string.Format("{0} {1}!", MessageValue.MESSAGE_CHANGE_STATUS_FOOD_DRINK, p.Name);
                        string Title = MessageValue.MESSAGE_NOTIFICATION;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            if(p != null)
                            {
                                UpdateStockFood(p.BranchId, lstFoodIds);
                                //GetOutStockFood();
                                //GetOutOfFoodNotWorking();
                                ListData.Remove(p);
                                ListDataNotWorking.Add(p);
                            }
                            else
                            {
                                WriteLog.logs("Lỗi tryền API Null");
                            }


                        }

                    });
                    UpdateOutOfFood = new RelayCommand<OutOfFood>((p) => { return true; }, (p) => {
                        List<int> lstFoodIds = new List<int>();
                        lstFoodIds.Add(p.FoodId);
                        // var intlist = p.FoodId.ToString().Select(x => Convert.ToInt32(x.ToString())).ToList();
                        UpdateFoodIsOutStockWrapper wrapper = new UpdateFoodIsOutStockWrapper(p.BranchId, lstFoodIds);
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = string.Format("{0} {1} không ?", MessageValue.MESSAAGE_CONFORM_FOOD_TITLE, p.Name);
                        string Title = MessageValue.MESSAGE_NOTIFICATION;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            if (p != null)
                            {
                                UpdateStockFood(p.BranchId, lstFoodIds);
                                ListDataNotWorking.Remove(p);
                                ListData.Add(p);
                            }
                            else
                            {
                                WriteLog.logs("Lỗi tryền API Null");
                            }


                        }
                    });
                    CheckAllFoodingCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                   {
                       foreach(Models.OrderDetail o in ItemsPendings.ToList())
                       {
                           ChangeStatusPendingToConfirm(o);
                       }
                   });
                    CheckAllFoodCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        foreach (Models.OrderDetail o in Items.ToList())
                        {
                            GetCookFinish(o);
                        }
                    });
                    WaitingChangeStatus = new RelayCommand<Models.OrderDetail>((p) => { return true; }, p =>
                    {
                        ChangeStatusPendingToConfirm(p);
                    });
                    //when press button Huy call api change status
                    WaitingOutService = new RelayCommand<Models.OrderDetail>((p) => { return true; }, p =>
                    {
                        List<int> lstFoodIds = new List<int>();
                        int tmp = unchecked((int)p.FoodId);
                        lstFoodIds.Add(tmp);
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = string.Format("{0} {1}!",MessageValue.MESSAGE_CHANGE_STATUS_FOOD_DRINK,p.FoodName);
                        string Title = MessageValue.MESSAGE_NOTIFICATION;
                        string YesContent = MessageValue.MESSAGE_FROM_NOTIFICATION_CANCEL;
                        string NoContent = MessageValue.MESSAGE_FROM_NOTIFICATION_OUT_OF_STOC;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            //Models.OrderDetail orderDetail = p;
                            GetWaitingOut(p);
                        }
                        if(confirm.isStock)
                        {
                            //int tmp2 = unchecked((int)currentUser.BranchId);
                            //UpdateStockFood(tmp2, lstFoodIds);
                            //GetOutStockFood();
                            //GetOutOfFoodNotWorking();
                            GetWaitingOutOfStock(p);
                        }


                    });
                    //when press button hoan ta call api change status
                    CookingFinish = new RelayCommand<Models.OrderDetail>((p) => { return true; }, p =>
                    {
                        //Models.OrderDetail orderDetail = p;
                        GetCookFinish(p);

                    });
                    //when press button Het mon call api change status
                    CookingOutService = new RelayCommand<Models.OrderDetail>((p) => { return true; }, p =>
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CANCEL_FOOD;
                        string Title = MessageValue.MESSAGE_CANCEL_FOOD;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            //Models.OrderDetail orderDetail = p;
                            GetCookOut(p);
                        }
                    });
                }
                //First refresh



            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

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

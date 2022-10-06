using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Nest;
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

namespace TechresStandaloneSale.ViewModels
{
    public class BatenderViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private Kitchen currentKitchen;
       
        #region By Phan Viet Ha 
        private ImageBrush _OutOfStockBackground;
        public ImageBrush OutOfStockBackground { get => _OutOfStockBackground; set { _OutOfStockBackground = value; OnPropertyChanged("OutOfStockBackground"); } }

        public List<string> allFoodStock;
        private List<OutOfFood> _OutOfFoodsUpdate;
        public List<OutOfFood> OutOfFoodsUpdate { get => _OutOfFoodsUpdate; set { _OutOfFoodsUpdate = value; OnPropertyChanged("OutOfFoodsUpdate"); } }
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

        public ICommand BtnOutOfFood { get; set; }
        public ICommand PostOutOfFood { get; set; }

        public ICommand UpdateOutOfFood { get; set; }


        // Responseive 

        private long _fontsize; 
        public long fontsize
        {
            get => _fontsize; 
            set
            {
                _fontsize = value;
                OnPropertyChanged("fontsize");  
            }
        }

        #endregion
        public ICommand WaitingChangeStatus { get; set; }
        public ICommand WaitingOutService { get; set; }
        public ICommand CookingFinish { get; private set; }
        public ICommand SaveInventoryWarehouse { get; set; }
        public ICommand TextChangeQuantityCommand { get; set; }
        public ICommand BtnInventoryWarehouse { get; set; }
        public ICommand SelectionDateChangedCommand { get; set; }
        public ICommand BtWaitingFood { get; set; }
        public ICommand LoadedUCCommand { get; set; }
        public ICommand BtnHistoryFood { get; set; }
        public ICommand BtnCategories { get; set; }
        public ICommand RefreshRealtimeCommand { get; set; }
        public ICommand BtnTakeAwayFood { get; set; }
        public ICommand BtnAllFood { get; set; }
        private User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

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
        public Visibility _RefreshVisibility { get; set; }
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
        public Visibility _SearchFilterVisibility;
        public Visibility SearchFilterVisibility
        {
            get
            {
                return _SearchFilterVisibility;
            }
            set
            {
                _SearchFilterVisibility = value;

                OnPropertyChanged("SearchFilterVisibility");
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

        private DateTime _DateTimeInput;
        public DateTime DateTimeInput { get => _DateTimeInput; set { _DateTimeInput = value; OnPropertyChanged("DateTimeInput"); } }
        private DateTime _DateTimeDisplayDateEnd;
        public DateTime DateTimeDisplayDateEnd { get => _DateTimeDisplayDateEnd; set { _DateTimeDisplayDateEnd = value; OnPropertyChanged("DateTimeDisplayDateEnd"); } }

        private Visibility _SearchVisibility;
        public Visibility SearchVisibility { get => _SearchVisibility; set { _SearchVisibility = value; OnPropertyChanged("SearchVisibility"); } }
        //private ObservableCollection<MaterialDailyInventor> _DailyList = new ObservableCollection<MaterialDailyInventor>();
        //public ObservableCollection<MaterialDailyInventor> DailyList { get => _DailyList; set { _DailyList = value; OnPropertyChanged("DailyList"); } }

        private string waitingContent;
        public string WaitingContent
        {
            get
            {
                return waitingContent;
            }
            set
            {
                waitingContent = value;
                OnPropertyChanged("WaitingContent");
            }
        }

        private string waitingConfirmContent;
        public string WaitingConfirmContent
        {
            get
            {
                return waitingConfirmContent;
            }
            set
            {
                waitingConfirmContent = value;
                OnPropertyChanged("WaitingConfirmContent");
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
        private ImageBrush _AllFoodBackground { get; set; }
        public ImageBrush AllFoodBackground { get => _AllFoodBackground; set { _AllFoodBackground = value; OnPropertyChanged("AllFoodBackground"); } }
        private ImageBrush _PendingBackground { get; set; }
        public ImageBrush PendingBackground { get => _PendingBackground; set { _PendingBackground = value; OnPropertyChanged("PendingBackground"); } }
        private ImageBrush _HistoryBackground { get; set; }
        public ImageBrush HistoryBackground { get => _HistoryBackground; set { _HistoryBackground = value; OnPropertyChanged("HistoryBackground"); } }
        private ImageBrush _TakeAwayBackground { get; set; }
        public ImageBrush TakeAwayBackground { get => _TakeAwayBackground; set { _TakeAwayBackground = value; OnPropertyChanged("TakeAwayBackground"); } }
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
            CategoryResponse categoryReponse = client.GetCategoryByBranch(currentUser.BranchId, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER);
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
            }
            DialogHostOpen = false;
        }
        public List<OrderDetail> OrderDetailPending = new List<OrderDetail>();
        public List<OrderDetail> OrderDetailConfirm = new List<OrderDetail>();
        protected async void LoadListAllDrinkAndOther()
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
            OrderDetailResponse waitingResponse =await Task.Run(() =>  client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.DONE), Constants.STATUS_IS_NOT_APPROVED,  "", "", string.Format("{0},{1},{2}", 
                (int)OrderMethodEnum.EAT_IN, (int)OrderMethodEnum.TAKE_AWAY, (int)OrderMethodEnum.ONLINE_DELIVERY),currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER));
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                        }
                    }
                }
                WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        protected  void LoadListWaitingDrinkAndOther()
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
            OrderDetailResponse waitingResponse =  client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.DONE), Constants.STATUS_IS_NOT_APPROVED,  "", "", string.Format("{0}", (int)OrderMethodEnum.EAT_IN),currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER);
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                        }
                    }
                }
                WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
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
            OrderDetailResponse waitingResponse = await Task.Run(() =>client.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1}", (int)OrderDetailStatusEnum.PENDING, (int)OrderDetailStatusEnum.DONE), Constants.STATUS_IS_NOT_APPROVED, "", "", string.Format("{0},{1}", (int)OrderMethodEnum.ONLINE_DELIVERY, (int)OrderMethodEnum.TAKE_AWAY),currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER));
            if (waitingResponse != null && waitingResponse.Status == (int)ResponseEnum.OK && waitingResponse.Data != null)
            {
                foreach (OrderDetail o in waitingResponse.Data.List)
                {
                    if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                    {
                        OrderDetail _item = ItemsPendings.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            ItemsPendings.Add(o);
                            OrderDetailPending.Add(o);
                        }
                    }
                    else if (o.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE)
                    {
                        OrderDetail _item = Items.Where(x => x.Id == o.Id).FirstOrDefault();
                        if (_item == null)
                        {
                            Items.Add(o);
                            OrderDetailConfirm.Add(o);
                        }
                    }
                }
                WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        public void AddOrderDetailHistory(OrderDetail orderDetail)
        {
            if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && orderDetail.IsApprovedDrink == Constants.STATUS)
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

        #region By  Phan Viet HA 



        #endregion
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
            if (OrderDetailPending == null)
            {
                OrderDetailPending = new List<OrderDetail>();
            }
            if (OrderDetailConfirm == null)
            {
                OrderDetailConfirm = new List<OrderDetail>();
            }
            if (orderDetail != null)
            {
                if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.PENDING)
                {
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        //if (orderDetail.IsOnline == (int)StatusEnum.NOT && orderDetail.IsTakeAway == (int)StatusEnum.NOT)
                        //{
                        if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                        {
                            OrderDetail od = ItemsPendings.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (od == null)
                            {
                                ItemsPendings.Add(orderDetail);
                            }
                            else
                            {
                                int index = ItemsPendings.IndexOf(od);
                                ItemsPendings.Remove(od);
                                ItemsPendings.Insert(index, orderDetail);
                            }
                        }

                        OrderDetail o = OrderDetailPending.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                        if (o == null)
                        {
                            OrderDetailPending.Add(orderDetail);
                        }
                        else
                        {
                            int index = OrderDetailPending.IndexOf(o);
                            OrderDetailPending.Remove(o);
                            OrderDetailPending.Insert(index, orderDetail);
                        }
                        WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                        //}

                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        if (orderDetail.IsOnline == (int)StatusEnum.NOT && orderDetail.IsTakeAway == (int)StatusEnum.NOT)
                        {
                            if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                            {
                                OrderDetail od = ItemsPendings.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                                if (od == null)
                                {
                                    ItemsPendings.Add(orderDetail);
                                }
                                else
                                {
                                    int index = ItemsPendings.IndexOf(od);
                                    ItemsPendings.Remove(od);
                                    ItemsPendings.Insert(index, orderDetail);
                                }
                            }

                            OrderDetail o = OrderDetailPending.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (o == null)
                            {
                                OrderDetailPending.Add(orderDetail);
                            }
                            else
                            {
                                int index = OrderDetailPending.IndexOf(o);
                                OrderDetailPending.Remove(o);
                                OrderDetailPending.Insert(index, orderDetail);
                            }
                            WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                        }

                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        if (orderDetail.IsOnline == (int)StatusEnum.YES || orderDetail.IsTakeAway == (int)StatusEnum.YES)
                        {
                            if (orderDetail.CategoryId == CurrentCategoryId || CurrentCategoryId == (long)CategoryTypeEnum.ALL)
                            {
                                OrderDetail od = ItemsPendings.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                                if (od == null)
                                {
                                    ItemsPendings.Add(orderDetail);
                                }
                                else
                                {
                                    int index = ItemsPendings.IndexOf(od);
                                    ItemsPendings.Remove(od);
                                    ItemsPendings.Insert(index, orderDetail);
                                }
                            }

                            OrderDetail o = OrderDetailPending.Where(x => x.Id == orderDetail.Id).FirstOrDefault();
                            if (o == null)
                            {
                                OrderDetailPending.Add(orderDetail);
                            }
                            else
                            {
                                int index = OrderDetailPending.IndexOf(o);
                                OrderDetailPending.Remove(o);
                                OrderDetailPending.Insert(index, orderDetail);
                            }
                            WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                        }
                    }
                }
                else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && orderDetail.IsApprovedDrink == Constants.NOT_STATUS)
                {
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        //if (orderDetail.IsTakeAway == (int)StatusEnum.NOT && orderDetail.IsOnline == (int)StatusEnum.NOT)
                        //{
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
                        WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                        //}
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        if (orderDetail.IsTakeAway == (int)StatusEnum.NOT && orderDetail.IsOnline == (int)StatusEnum.NOT)
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
                            WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                        }

                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        if (orderDetail.IsTakeAway == (int)StatusEnum.YES || orderDetail.IsOnline == (int)StatusEnum.YES)
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
                            WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                        }
                    }
                }

                else if (orderDetail.OrderDetailStatus == (int)OrderDetailStatusEnum.DONE && orderDetail.IsApprovedDrink == Constants.STATUS)
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
                    WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
                    WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                    AddOrderDetailHistory(orderDetail);

                }

            }

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
                    TimeSpan timeSpan = new TimeSpan(12, 0, 0);
                    var timeOut = new CancellationTokenSource(timeSpan).Token;
                    Constants.CLIENT_WEB_SOCKET = new ClientWebSocket();
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
                        string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId,currentKitchen.Id);
                        //string LinkSocketMoveFood = string.Format("restaurants/{0}/branches/{1}/order_detail_move_food_only", currentUser.RestaurantId, currentUser.BranchId);
                        //string LinkSocketReturn = string.Format("restaurants/{0}/branches/{1}/order_detail_drink_and_other_only_for_return", currentUser.RestaurantId, currentUser.BranchId);
                        //string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/orders", currentUser.RestaurantId, currentUser.BranchId);
                        if (data.Contains(LinkSocketWaiting))
                        {
                            if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY || currentButton == (int)OrderDetailButtonEnum.All)
                            {
                                BaseRealtimeModel baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModel>(data);
                                if (baseRealtimeModelOrder != null)
                                {
                                    if (currentButton == (int)OrderDetailButtonEnum.All || currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                                    {
                                        OrderDetailReatime orderDetail = baseRealtimeModelOrder.Value;
                                        if (orderDetail != null && orderDetail.ObjectData != null)
                                        {
                                            dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
                                            OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
                                            if (od != null)
                                            {
                                                AddOrderDetailToRealTime(od);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        #region Command socket
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
                        //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                //{
                        //                //    LoadListWaitingDrinkAndOther();
                        //                //}
                        //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    LoadListWaitingDrinkAndOther();
                        //                }

                        //            }
                        //        }
                        //        else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                        //        {
                        //            OrderRealTime order = baseRealtimeModelOrder.Value;
                        //            if (order != null)
                        //            {
                        //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                //{
                        //                //    LoadListWaitingDrinkAndOther();
                        //                //}
                        //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    LoadListWaitingDrinkAndOther();
                        //                }

                        //            }
                        //        }
                        //        else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        //        {
                        //            OrderRealTime order = baseRealtimeModelOrder.Value;
                        //            if (order != null)
                        //            {
                        //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                //{
                        //                //    GetOrderDetailTakAway();
                        //                //}
                        //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
                        //                {
                        //                    GetOrderDetailTakAway();
                        //                }

                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                        #region Socket command 
                        //else if (data.Contains(LinkSocketMoveFood))
                        //{
                        //    BaseRealtimeModel baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModel>(data);
                        //    if (baseRealtimeModelOrder != null)
                        //    {
                        //        OrderDetailReatime orderDetail = baseRealtimeModelOrder.Value;
                        //        if (currentButton == (int)OrderDetailButtonEnum.All || currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                        //        {
                        //            if (orderDetail != null && orderDetail.ObjectData != null)
                        //            {
                        //                dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
                        //                OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
                        //                if (od != null && (od.CategoryType == (int)CategoryTypeEnum.DRINK || od.CategoryType == (int)CategoryTypeEnum.OTHER))
                        //                {
                        //                    AddOrderDetailToRealTime(od);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (data.Contains(LinkSocketReturn))
                        //{
                        //    BaseRealtimeModel baseRealtimeModelOrder = JsonConvert.DeserializeObject<BaseRealtimeModel>(data);
                        //    if (baseRealtimeModelOrder != null)
                        //    {
                        //        OrderDetailReatime orderDetail = baseRealtimeModelOrder.Value;

                        //        if (currentButton == (int)OrderDetailButtonEnum.PENDING || currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY || currentButton == (int)OrderDetailButtonEnum.All)
                        //        {
                        //            if (orderDetail != null && orderDetail.ObjectData != null)
                        //            {
                        //                dynamic jsonResponseObject = JsonConvert.DeserializeObject(orderDetail.ObjectData);
                        //                OrderDetail od = jsonResponseObject.ToObject<OrderDetail>();
                        //                if (od != null)
                        //                {
                        //                    AddOrderDetailToRealTime(od);
                        //                }

                        //            }

                        //        }
                        //    }
                        //}
                        #endregion
                        //string LinkSocketNotifications = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId,0);
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
            string LinkSocketWaiting = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId,currentKitchen.Id);
            WriteLog.logs(LinkSocketWaiting);
            SocketIOManager.Instance.socket.On(LinkSocketWaiting, (data) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    WriteLog.logs("Conected" + SocketIOManager.Instance.socket.ServerUri);
                    //string JsonString = data.ToString();
                    WriteLog.logs(data.ToString());

                    //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
                    //OrderDetailReatime orderDetail = jsonResponse.ToObject<OrderDetailReatime>();
                    SocketIOManager.Instance.socket.JsonSerializer = new NewtonsoftJsonSerializer();
                    OrderDetailReatime orderDetail = data.GetValue<OrderDetailReatime>(0);
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        if (orderDetail != null)
                        {
                            if (orderDetail.ActionType == 1 || orderDetail.ActionType == 2 || orderDetail.ActionType == 3 || orderDetail.ActionType == 4 ||
                            orderDetail.ActionType == 5 ||
                            orderDetail.ActionType == 6
                            || orderDetail.ActionType == 7)
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
                                LoadListWaitingDrinkAndOther();
                            }
                        }
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        if (orderDetail != null)
                        {
                            if (orderDetail.ActionType == 1 || orderDetail.ActionType == 2 || orderDetail.ActionType == 3 || orderDetail.ActionType == 4 ||
                            orderDetail.ActionType == 5 ||
                            orderDetail.ActionType == 6
                            || orderDetail.ActionType == 7)
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
                                LoadListWaitingDrinkAndOther();
                            }
                        }
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                         GetOrderDetailTakAway();
                    }

                });
            });

            #region Command Socket 
            //string LinkSocketOrder = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/order_details", currentUser.RestaurantId, currentUser.BranchId, currentKitchen.Id);
            //SocketIOManager.Instance.socket.On(LinkSocketOrder, (data) =>
            // {
            //     Application.Current.Dispatcher.Invoke((Action)delegate
            //     {
            //         if (currentButton == (int)OrderDetailButtonEnum.All)
            //         {
            //            //     string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //             if (order != null)
            //             {
            //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                //{
            //                //    LoadListWaitingDrinkAndOther();
            //                //}
            //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                 {
            //                     LoadListWaitingDrinkAndOther();
            //                 }
            //             }
            //         }
            //         else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
            //         {
            //            //string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //             if (order != null)
            //             {
            //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                //{
            //                //    LoadListWaitingDrinkAndOther();
            //                //}
            //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                 {
            //                     LoadListWaitingDrinkAndOther();
            //                 }

            //             }
            //         }
            //         else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
            //         {
            //            //string JsonString = data.ToString();
            //            WriteLog.logs(data.ToString());
            //            //dynamic jsonResponse = JsonConvert.DeserializeObject(data.ToString());
            //            //OrderRealTime order = jsonResponse.ToObject<OrderRealTime>();
            //            OrderRealTime order = data.GetValue<OrderRealTime>(0);
            //             if (order != null)
            //             {
            //                //if (order.IsMoveTable && (order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                //{
            //                //    GetOrderDetailTakAway();
            //                //}
            //                if ((order.OrderStatus == (int)OrderStatusEnum.OPENING || order.OrderStatus == (int)OrderStatusEnum.WAITING_COMPLETE || order.OrderStatus == (int)OrderStatusEnum.WAITING_PAYMENT))
            //                 {
            //                     GetOrderDetailTakAway();
            //                 }
            //             }
            //         }
            //     });
            // });
            #endregion

            #region Command Socket



            //string LinkSocketNotification = string.Format("restaurants/{0}/branches/{1}/kitchens/{2}/notify_winapp_kitchens", currentUser.RestaurantId, currentUser.BranchId, 0);
            //WriteLog.logs(LinkSocketNotification);
            //SocketIOManager.Instance.socket.On(LinkSocketNotification, (data) =>
            //{
            //    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            //    {
            //        if (data == null) return;
            //        string JsonString = data.ToString();
            //        WriteLog.logs(JsonString);
            //        //dynamic jsonResponse = JsonConvert.DeserializeObject(JsonString);
            //        //NotificationRealTime notification = jsonResponse.ToObject<Models.NotificationRealTime>();
            //        NotificationRealTime notification = data.GetValue<Models.NotificationRealTime>(0);
            //        NotificationMessage.ShowInfomation(notification.Title, notification.Content, notification.CreatedAt);
            //    }), DispatcherPriority.ContextIdle);
            //});
            #endregion
        }
        public bool IsAddHistory = false;
       
        private async void ChangeStatusOrderDetailPendingToApproved(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                // Remove item from list order detail befored call api update to server 
                removeOrderDetail(orderDetail);
                //Begin call API to Server 
                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.DONE, 0);
                BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                // BaseResponse baseResponse =  await client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper);
                if (baseResponse == null || baseResponse.Status != (int)ResponseEnum.OK)
                {
                    IsAddHistory = true;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                }
                else
                {
                    AddOrderDetailHistory(orderDetail);
                }
            }
        }
        private void removeOrderDetail(OrderDetail orderDetail)
        {
            // Remove item from list order detail befored call api update to server 
            ItemsPendings.Remove(orderDetail);
            OrderDetailPending.Remove(orderDetail);
            // update quantity befored call api 
            WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);
            Items.Add(orderDetail);
            //OrderDetailConfirm.Add(orderDetail);
            WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
        }
        private async void ChangeStatusOrderDetailToCancel(OrderDetail orderDetail)
        {

            if (orderDetail != null)
            {

                // Remove item from list order detail befored call api update to server 
                ItemsPendings.Remove(orderDetail);
                OrderDetailPending.Remove(orderDetail);
                orderDetail.OrderDetailStatus = (int)OrderDetailStatusEnum.OUTSTOCK;
                orderDetail.OrderDetailStatusName = MessageValue.MESSAGE_FORM_ORDER_DETAIL_OUTSTOCK;
                AddOrderDetailHistory(orderDetail);
                // update quantity befored call api 
                WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);

                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.OUTSTOCK,0);
                BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                if (baseResponse == null || baseResponse.Status != (long)ResponseEnum.OK)
                {
                    IsAddHistory = true;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                }
            }
        }
        private async void ChangeStatusOrderDetailToOutStock(OrderDetail orderDetail)
        {

            if (orderDetail != null)
            {

                // Remove item from list order detail befored call api update to server 
                ItemsPendings.Remove(orderDetail);
                OrderDetailPending.Remove(orderDetail);
                orderDetail.OrderDetailStatus = (int)OrderDetailStatusEnum.OUTSTOCK;
                orderDetail.OrderDetailStatusName = MessageValue.MESSAGE_FORM_ORDER_DETAIL_OUTSTOCK;
                AddOrderDetailHistory(orderDetail);
                // update quantity befored call api 
                WaitingContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_OUT_WAIT, ItemsPendings.Count);

                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeSatusOrderDetailWrapper wrapper = new ChangeSatusOrderDetailWrapper(orderDetail.Id.ToString(), (int)OrderDetailStatusEnum.OUTSTOCK, 1);
                BaseResponse baseResponse = await Task.Run(() => client.ChangeStatus(orderDetail.OrderId.ToString(), wrapper));
                if (baseResponse == null || baseResponse.Status != (long)ResponseEnum.OK)
                {
                    IsAddHistory = true;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                }
            }
        }
        private async void ChangeStatusOrderDetailToApproved(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {

                // Remove item from list order detail befored call api update to server 
                Items.Remove(orderDetail);
                OrderDetailConfirm.Remove(orderDetail);
                AddOrderDetailHistory(orderDetail);
                WaitingConfirmContent = string.Format(MessageValue.MESSAGE_WAREHOUSE_CONFIRMMARION_WAIT, Items.Count);
                OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                ChangeApprovedDrinkWrapper wrapper = new ChangeApprovedDrinkWrapper(orderDetail.Id.ToString());
                BaseResponse baseResponse = await Task.Run(() => client.ApprovedDrinkOther(orderDetail.OrderId.ToString(), wrapper));
                if (baseResponse == null || baseResponse.Status != (long)ResponseEnum.OK)

                {
                    IsAddHistory = true;
                    if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                    // call api update list 
                }
            }
        }
        private ObservableCollection<Table> _ListTableName = new ObservableCollection<Table>();
        public ObservableCollection<Table> ListTableName { get => _ListTableName; set { _ListTableName = value; OnPropertyChanged("ListTableName"); } }

        public long inventoryId = 0;

        //public void InventoryWarehouse()
        //{
        //    if (DailyList != null)
        //    {
        //        DailyList.Clear();
        //    }
        //    else
        //    {
        //        DailyList = new ObservableCollection<MaterialDailyInventor>();
        //    }

        //    ReportClient client = new ReportClient(this, this, this);
        //    DialogHostOpen = true;
        //    CheckExistsDailyInventoryResponse checkExistsDailyInventoryResponse = client.CheckExistsDailyInventory(currentUser.BranchId, (int)MaterialEnum.DRINK);
        //    if (checkExistsDailyInventoryResponse != null)
        //    {
        //        if (checkExistsDailyInventoryResponse.Data == null)
        //        {
        //            MaterialDailyInventoryResponse response = client.GetMaterialDailyInventorybyCook((int)MaterialEnum.DRINK);
        //            if (response != null && response.Status == (long)ResponseEnum.OK && response.Data != null)
        //            {
        //                response.Data.ForEach(DailyList.Add);
        //            }
        //        }
        //        else
        //        {
        //            inventoryId = checkExistsDailyInventoryResponse.Data.Id;
        //            checkExistsDailyInventoryResponse.Data.List.ForEach(DailyList.Add);
        //        }
        //        DialogHostOpen = false;
        //    }
        //    else
        //        DialogHostOpen = false;
        //}

        private Button button;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            button.IsEnabled = true;
            dispatcherTimer.Stop();
        }
        public void GetListTableName()
        {
            if (ListTableName.Count > 0)
            {
                ListTableName.Clear();
            }
            else
            {
                ListTableName = new ObservableCollection<Table>();
            }
            DialogHostOpen = true;
            TablesClient tablesClient = new TablesClient(this, this, this);
            TableResponse tableResponse = tablesClient.GetListTable(Constants.ALL.ToString());
            if (tableResponse != null)
            {
                //Table b = new Table();
                //b.Id = Constants.ALL;
                //b.Name =MessageValue.MESSAGE_ALL;
                //ListTableName.Insert(0, b);
                tableResponse.Data.ForEach(ListTableName.Add);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        protected async void GetHistoryFoodList(DateTime DayTime)
        {
            if(OrderDetailCancelList == null)
            {
                OrderDetailCancelList = new ObservableCollection<OrderDetail>(); 
            }
            else
            {
                OrderDetailCancelList.Clear(); 
            }
            if(OrderDetailDoneList == null)
            {
                OrderDetailDoneList = new ObservableCollection<OrderDetail>(); 
            }
            else
            {
                OrderDetailDoneList.Clear(); 
            }
            string fromDate = Utils.Utils.GetDateFormatVN(DayTime != null ? DayTime : DateTime.Now); 
            //string toDare 
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
            string fromDate = Utils.Utils.GetDateFormatVN(DayTime != null ? DayTime : DateTime.Now);
            string toDate = Utils.Utils.GetDateFormatVN(DayTime != null ? DayTime : DateTime.Now);
            DialogHostOpen = true;
            OrderDetailsClient orderDetailsClient = new OrderDetailsClient(this, this, this);
            OrderDetailResponse response = await Task.Run(() => orderDetailsClient.GetOrderDetails(currentUser.BranchId, string.Format("{0},{1},{2}", (int)OrderDetailStatusEnum.CANCEL, (int)OrderDetailStatusEnum.DONE, (int)OrderDetailStatusEnum.OUTSTOCK), Constants.STATUS_IS_APPROVED,  fromDate, toDate, string.Format("{0},{1},{2}", (int)OrderMethodEnum.EAT_IN, (int)OrderMethodEnum.ONLINE_DELIVERY, (int)OrderMethodEnum.TAKE_AWAY),currentKitchen.Id, Constants.STATUS_CATEGORY_TYPE_DRINK_OTHER));
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
        public ContentControl _MainContentControl;
        public DateTime currentDateTime;
        public int currentButton;

        public long CurrentCategoryId;
        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {

            if (e.IsAvailable)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    DialogHostOpen = false;
                    if (currentButton == (int)OrderDetailButtonEnum.HISTORY)
                    {
                        GetFoodList(DateTimeInput);
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.PENDING)
                    {
                        LoadListWaitingDrinkAndOther();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.TAKE_AWAY)
                    {
                        GetOrderDetailTakAway();
                    }
                    else if (currentButton == (int)OrderDetailButtonEnum.All)
                    {
                        LoadListAllDrinkAndOther();
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

        #region By Phan Viet HA Set Up Function 
        public async void GetOutStockFood()
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
        public async void UpdateStockFood(int brandid, List<int> foodids)
        {

            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            BaseResponse response = await Task.Run(() => client.UpdateFoodOutOfStock(brandid, foodids));
            UpdateFoodIsOutStockWrapper wrapper = new UpdateFoodIsOutStockWrapper(brandid, foodids);
            foreach (OutOfFood food in AllFood)
            {
                food.RestaurantBrandId = Convert.ToInt32(brandid);
                foodids.Add(food.FoodId);
            }
            DialogHostOpen = false;



        }
        public async void GetOutOfFoodNotWorking()
        {
            if (ListDataNotWorking != null)
            {
                ListDataNotWorking.Clear();
            }
            else
            {
                ListDataNotWorking = new ObservableCollection<OutOfFood>();
            }

            OutOfFoodClient client = new OutOfFoodClient(this, this, this);
            DialogHostOpen = true;
            OutOfFoodResponse response = await Task.Run(() => client.GetOutOfFoodNotWorking(currentUser.BranchId, 1, "", currentKitchen.Id));
           // GetOutStockFood();
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach (OutOfFood foodOutStock in response.Data.List)
                {
                    FoodOutStock.Add(foodOutStock);
                    FoodOutStock = ListDataNotWorking;
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

        public BatenderViewModel()
        {
           
            #region By Phan Viet Ha Responsive 
            if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
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

            #endregion
            try
            {
                currentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
                if (currentUser != null)
                {
                    RealTime();
                    LoadedUCCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
                        {
                            
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                               
                                currentButton = (int)OrderDetailButtonEnum.All;
                                IsAddHistory = true;
                                BtnSaveVisibility = Visibility.Collapsed;
                                DatePickerVisibility = Visibility.Collapsed;
                                SearchVisibility = Visibility.Visible;
                                RefreshVisibility = Visibility.Visible;
                                SearchFilterVisibility = Visibility.Collapsed;
                                DateTimeInput = DateTime.Now;
                                currentDateTime = DateTimeInput;
                                DateTimeDisplayDateEnd = DateTime.Now;

                                AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                                PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                                HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                                TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                                OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));


                                _MainContentControl = p.FindName("listData") as ContentControl;
                                _MainContentControl.Content = new BatenderListUC();
                                if (Constants.IS_FIRST_COOK_CATEGORY)
                                {
                                    GetAllCategoryList();
                                    Constants.IS_FIRST_COOK_CATEGORY = false;
                                }
                                else
                                {
                                    if (ItemsCategory != null)
                                    {
                                        ItemsCategory.Clear();
                                    }
                                    else
                                    {
                                        ItemsCategory = new ObservableCollection<Category>();
                                    }
                                    List<Category> categories = (List<Category>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_CATEGORY);
                                    if (categories != null)
                                    {
                                        categories.ForEach(ItemsCategory.Add);
                                        Category c = categories.Where(x => x.IsChoose).FirstOrDefault();
                                        CurrentCategoryId = c != null ? c.Id : (long)CategoryTypeEnum.ALL;
                                    }
                                }

                                //GetListTableName();
                                LoadListAllDrinkAndOther();
                               
                                ChangeNetWork();
                            });
                        }
                    });
                    BtnAllFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (currentButton != (int)OrderDetailButtonEnum.All)
                        {
                            RefreshVisibility = Visibility.Visible;
                            DatePickerVisibility = Visibility.Collapsed;
                            _MainContentControl.Content = new BatenderListUC();

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            LoadListAllDrinkAndOther();
                        }
                        currentButton = (int)OrderDetailButtonEnum.All;
                    });
                    BtnTakeAwayFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (currentButton != (int)OrderDetailButtonEnum.TAKE_AWAY)
                        {
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            SearchFilterVisibility = Visibility.Collapsed;

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            _MainContentControl.Content = new BatenderListUC();

                            GetOrderDetailTakAway();
                        }
                        currentButton = (int)OrderDetailButtonEnum.TAKE_AWAY;
                        RealTime();
                        NotificationMessage.Infomation("Chức năng này đang được phát triển thêm");
                    });
                    BtnCategories = new RelayCommand<Category>((p) => { return true; }, p =>
                    {
                        if (p != null)
                        {
                            GetOrderDetailByCategory(p.Id);
                        }
                    });
                    SelectionDateChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>

                    {
                        if (DateTimeInput != currentDateTime)
                        {
                            currentButton = (int)OrderDetailButtonEnum.HISTORY;
                            currentDateTime = DateTimeInput;

                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Visible;
                            SearchVisibility = Visibility.Collapsed;
                            SearchFilterVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Collapsed;

                            _MainContentControl.Content = new HistoryBatenderDetailUserControl();
                            GetFoodList(DateTimeInput);
                        }
                    });

                    RefreshRealtimeCommand = new RelayCommand<Button>((p) => { return true; }, p =>
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            p.IsEnabled = false;
                            button = p;

                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchVisibility = Visibility.Visible;
                            
                            currentButton = (int)OrderDetailButtonEnum.All;

                            _MainContentControl.Content = new BatenderListUC();

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));
                            LoadListAllDrinkAndOther();
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
                        if (currentButton != (int)OrderDetailButtonEnum.HISTORY)
                        {
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Visible;
                            SearchVisibility = Visibility.Collapsed;
                            SearchFilterVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Collapsed;

                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bgroup-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done-active.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            _MainContentControl = p.FindName("listData") as ContentControl;
                            _MainContentControl.Content = new HistoryBatenderDetailUserControl();
                            GetFoodList(DateTimeInput);
                        }
                        currentButton = (int)OrderDetailButtonEnum.HISTORY;
                    });
                    BtWaitingFood = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    {
                        if (currentButton != (int)OrderDetailButtonEnum.PENDING)
                        {
                            BtnSaveVisibility = Visibility.Collapsed;
                            DatePickerVisibility = Visibility.Collapsed;
                            SearchVisibility = Visibility.Visible;
                            RefreshVisibility = Visibility.Visible;
                            SearchFilterVisibility = Visibility.Collapsed;
                            AllFoodBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-pending.png")));
                            PendingBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-online.png")));
                            HistoryBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/done.png")));
                            TakeAwayBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/bg-complete.png")));
                            OutOfStockBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/out_of_stock_bg.png")));

                            _MainContentControl.Content = new BatenderListUC();

                            LoadListWaitingDrinkAndOther();
                        }
                        currentButton = (int)OrderDetailButtonEnum.PENDING;
                    });
                    //BtnInventoryWarehouse = new RelayCommand<UserControl>((p) => { return true; }, p =>
                    //{
                    //    currentButton = (int)OrderDetailButtonEnum.HISTORY;
                    //    BtnSaveVisibility = Visibility.Visible;
                    //    DatePickerVisibility = Visibility.Collapsed;
                    //    SearchVisibility = Visibility.Collapsed;
                    //    RefreshVisibility = Visibility.Collapsed;
                    //    InventoryWarehouse();
                    //    _MainContentControl.Content = new InventoryWarehouseDailyListUserControl();
                    //});
                    WaitingChangeStatus = new RelayCommand<OrderDetail>((p) => { return true; }, p =>
                    {
                        //OrderDetail orderDetail = p;
                        WriteLog.logs("WaitingChangeStatus"); 
                        ChangeStatusOrderDetailPendingToApproved(p);
                    });
                    WaitingOutService = new RelayCommand<OrderDetail>((p) => { return true; }, p =>
                    {
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CANCEL_FOOD;
                        string Title = MessageValue.MESSAGE_CANCEL_FOOD;
                        string YesContent = MessageValue.MESSAGE_FROM_NOTIFICATION_CANCEL;
                        string NoContent = MessageValue.MESSAGE_FROM_NOTIFICATION_OUT_OF_STOC;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            OrderDetail orderDetail = p;
                            ChangeStatusOrderDetailToCancel(p);
                        }
                        else if (confirm.isStock)
                        {
                            ChangeStatusOrderDetailToOutStock(p); 
                        }
                    });
                    CookingFinish = new RelayCommand<OrderDetail>((p) => { return true; }, p =>
                    {
                        OrderDetail orderDetail = p;
                        ChangeStatusOrderDetailToApproved(p);
                    });

                    #region Phan Viet Ha

                    BtnOutOfFood = new RelayCommand<UserControl>((p) => { return true; }, p => {
                        //if(currentButton != (int)OrderDetailButtonEnum.OUT_STOCK)
                        //{
                        SearchVisibility = Visibility.Visible;
                        RefreshVisibility = Visibility.Visible;
                        BtnSaveVisibility = Visibility.Collapsed;
                        DatePickerVisibility = Visibility.Collapsed;
                        currentButton = (int)OrderDetailButtonEnum.OUT_STOCK;
                        _MainContentControl.Content = new DrinkOutStockUC();

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
                        lstFoodIds.Add(p.FoodId);
                        // var intlist = p.FoodId.ToString().Select(x => Convert.ToInt32(x.ToString())).ToList();
                        UpdateFoodIsOutStockWrapper wrapper = new UpdateFoodIsOutStockWrapper(p.BranchId, lstFoodIds);
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_CANCEL_DRINK;
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
                        string contentConfirm = MessageValue.MESSAAGE_CONFORM_FOOD_TITLE;
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

                                //GetOutStockFood();
                                //GetOutOfFoodNotWorking();

                                ListDataNotWorking.Remove(p);
                                ListData.Add(p);
                            }
                            else
                            {
                                WriteLog.logs("Lỗi tryền API Null");
                            }


                        }
                    });

                    #endregion

                }
            }
            catch (Exception ex)
            {
                WriteLog.logs(ex.Message);
            }

        }
        public void LogError(Exception ex, string infoMessage)
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

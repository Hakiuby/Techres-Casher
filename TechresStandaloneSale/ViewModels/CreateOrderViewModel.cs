using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.UserControlView;
using TechresStandaloneSale.UserControlView.CreateOrder;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace TechresStandaloneSale.ViewModels
{
    public class CreateOrderViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {

        #region By Phan Viet Ha
        private long _widthSendBar;
        public long widthSendBar
        {
            get => _widthSendBar;
            set
            {
                _widthSendBar = value;
                OnPropertyChanged("widthSendBar");
            }
        }
        private long _FontSizeTitleMain;
        public long FontSizeTitleMain { get => _FontSizeTitleMain; set { _FontSizeTitleMain = value; OnPropertyChanged("FontSizeTitleMain"); } }
        private long _fontsizeLable;
        public long fontsizeLable
        {
            get => _fontsizeLable;
            set
            {
                _fontsizeLable = value;
                OnPropertyChanged("fontsizeLable");
            }
        }
        private long _heightLableTb;
        public long heightLableTb
        {
            get => _heightLableTb;
            set { _heightLableTb = value; OnPropertyChanged("heightLableTb"); }
        }
        private long _heighTbAddress;
        public long heighTbAddress
        {
            get => _heighTbAddress;
            set
            {
                _heighTbAddress = value;
                OnPropertyChanged("heighTbAddress");
            }
        }
        private long _sizeWidthFood;
        public long sizeWidthFood
        {
            get => _sizeWidthFood;
            set
            {
                _sizeWidthFood = value;
                OnPropertyChanged("sizeWidthFood");
            }
        }
        private long _sizeHeightFood;
        public long sizeHeightFood
        {
            get => _sizeHeightFood;
            set
            {
                _sizeHeightFood = value;
                OnPropertyChanged("sizeHeightFood");
            }
        }


        private long _buttonHeightImage;
        public long buttonHeightImage
        {
            get => _buttonHeightImage;
            set
            {
                _buttonHeightImage = value;
                OnPropertyChanged("buttonHeightImage");
            }
        }
        private long _fontImage;
        public long fontImage
        {
            get => _fontImage;
            set
            {
                _fontImage = value;
                OnPropertyChanged("fontImage");
            }
        }
        private long _sizeButtonImage;
        public long sizeButtonImage
        {
            get => _sizeButtonImage;
            set
            {
                _sizeButtonImage = value;
                OnPropertyChanged("sizeButtonImage");
            }
        }
        private long _fontsizeTitle;
        public long fontsizeTitle
        {
            get => _fontsizeTitle;
            set
            {
                _fontsizeTitle = value;
                OnPropertyChanged("fontsizeTitle");
            }
        }
        private long _widthButton;
        public long widthButton
        {
            get => _widthButton;
            set
            {
                _widthButton = value;
                OnPropertyChanged("widthButton");
            }
        }
        private long _heightButton;
        public long heightButton
        {
            get => _heightButton;
            set
            {
                _heightButton = value;
                OnPropertyChanged("heightButton");
            }
        }
        private long _fontButton;
        public long fontButton
        {
            get => _fontButton;
            set
            {
                _fontButton = value;
                OnPropertyChanged("fontButton");
            }
        }
        private long _sizeWidthImage;
        public long sizeWidthImage { get => _sizeWidthImage; set { _sizeWidthImage = value; OnPropertyChanged("sizeWidthImage"); } }
        private long _sizeHeightImage;
        public long sizeHeightImage
        {
            get => _sizeHeightImage;
            set
            {
                _sizeHeightImage = value;
                OnPropertyChanged("sizeHeightImage");
            }
        }

        #region toan
        private long _heightFuntion;
        public long heightFuntion
        {
            get => _heightFuntion;
            set
            {
                _heightFuntion = value;
                OnPropertyChanged("heightFuntion");
            }
        }

        private long _heightImage;
        public long heightImage
        {
            get => _heightImage;
            set
            {
                _heightImage = value;
                OnPropertyChanged("heightImage");
            }
        }

        private string _widthfoodFrame;
        public string widthfoodFrame
        {
            get => _widthfoodFrame;
            set
            {
                _widthfoodFrame = value;
                OnPropertyChanged("widthfoodFrame");
            }
        }
        #endregion
        #endregion

        private ObservableCollection<Employee> _EmployeeList = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged("EmployeeList"); } }
        private Visibility _CategorySeaFoodVisibility;
        public Visibility CategorySeaFoodVisibility { get { return _CategorySeaFoodVisibility; } set { _CategorySeaFoodVisibility = value; OnPropertyChanged("CategorySeaFoodVisibility"); } }

        private Visibility _AddExtraChargeVisibility;
        public Visibility AddExtraChargeVisibility { get { return _AddExtraChargeVisibility; } set { _AddExtraChargeVisibility = value; OnPropertyChanged("AddExtraChargeVisibility"); } }
        private Visibility _CategoryOtherVisibility;
        public Visibility CategoryOtherVisibility { get { return _CategoryOtherVisibility; } set { _CategoryOtherVisibility = value; OnPropertyChanged("CategoryOtherVisibility"); } }
        private Visibility _CategoryDrinkVisibility;
        public Visibility CategoryDrinkVisibility { get { return _CategoryDrinkVisibility; } set { _CategoryDrinkVisibility = value; OnPropertyChanged("CategoryDrinkVisibility"); } }
        private Visibility _CategoryBbqVisibility;
        public Visibility CategoryBbqVisibility { get { return _CategoryBbqVisibility; } set { _CategoryBbqVisibility = value; OnPropertyChanged("CategoryBbqVisibility"); } }
        private Visibility _CategoryFoodVisibility;
        public Visibility CategoryFoodVisibility { get { return _CategoryFoodVisibility; } set { _CategoryFoodVisibility = value; OnPropertyChanged("CategoryFoodVisibility"); } }
        private bool _IsRightOrder;
        public bool IsRightOrder { get { return _IsRightOrder; } set { _IsRightOrder = value; OnPropertyChanged("IsRightOrder"); } }
        //  private bool _key = true;
        //  private bool _payment = false;
        private long _fontsize;
        public long fontsize { get { return _fontsize; } set { _fontsize = value; OnPropertyChanged("fontsize"); } }
        private long _width;
        public long width { get { return _width; } set { _width = value; OnPropertyChanged("width"); } }
        private double _ImageWidth;
        public double ImageWidth { get { return _ImageWidth; } set { _ImageWidth = value; OnPropertyChanged("ImageWidth"); } }
        private string _VisibleWidth;
        public string VisibleWidth { get { return _VisibleWidth; } set { _VisibleWidth = value; OnPropertyChanged("VisibleWidth"); } }
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
        private ObservableCollection<FoodMenuItem> itemsOrderDetail;
        public ObservableCollection<FoodMenuItem> ItemsOrderDetail
        {
            get
            {
                return itemsOrderDetail;
            }
            set
            {
                itemsOrderDetail = value;
                OnPropertyChanged("ItemsOrderDetail");
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
        public string tableContent;
        public string TableContent
        {
            get
            {
                return tableContent;
            }
            set
            {
                tableContent = value;
                OnPropertyChanged("TableContent");
            }
        }
        public bool sendCookEnabled;
        public bool SendCookEnabled
        {
            get
            {
                return sendCookEnabled;
            }
            set
            {
                sendCookEnabled = value;
                OnPropertyChanged("SendCookEnabled");
            }
        }

        public string totalAmount;
        public string TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public string _DiscountAmount;
        public string DiscountAmount
        {
            get
            {
                return _DiscountAmount;
            }
            set
            {
                _DiscountAmount = value;
                OnPropertyChanged("DiscountAmount");
            }
        }
        public string _Vat;
        public string Vat
        {
            get
            {
                return _Vat;
            }
            set
            {
                _Vat = value;
                OnPropertyChanged("Vat");
            }
        }
        public string _Discount;
        public string Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
                OnPropertyChanged("Discount");
            }
        }

        public string _VatAmount;
        public string VatAmount
        {
            get
            {
                return _VatAmount;
            }
            set
            {
                _VatAmount = value;
                OnPropertyChanged("VatAmount");
            }
        }
        public string _DepositAmount;
        public string DepositAmount
        {
            get
            {
                return _DepositAmount;
            }
            set
            {
                _DepositAmount = value;
                OnPropertyChanged("DepositAmount");
            }
        }

        public int slotCustomerContent;
        public int SlotCustomerContent
        {
            get
            {
                return slotCustomerContent;
            }
            set
            {
                slotCustomerContent = value;
                OnPropertyChanged("SlotCustomerContent");
            }
        }
        #region Dat
        private bool _isCheckSave;
        public bool isCheckSave { get => _isCheckSave; set { _isCheckSave = value; OnPropertyChanged("isCheckSave"); } }
        #endregion
        private Brush _tableForeground { get; set; }
        public Brush TableForeground { get => _tableForeground; set { _tableForeground = value; OnPropertyChanged("TableForeground"); } }
        private Brush _tableBackGround { get; set; }
        public Brush TableBackGround { get => _tableBackGround; set { _tableBackGround = value; OnPropertyChanged("TableBackGround"); } }
        private Brush _bringOutForeground { get; set; }
        public Brush BringOutForeground { get => _bringOutForeground; set { _bringOutForeground = value; OnPropertyChanged("BringOutForeground"); } }
        private Brush _bringOutBackGround { get; set; }
        public Brush BringOutBackGround { get => _bringOutBackGround; set { _bringOutBackGround = value; OnPropertyChanged("BringOutBackGround"); } }
        private Brush _CategoryFoodForeground { get; set; }
        public Brush CategoryFoodForeground { get => _CategoryFoodForeground; set { _CategoryFoodForeground = value; OnPropertyChanged("CategoryFoodForeground"); } }
        private Brush _CategoryFoodFground { get; set; }
        public Brush CategoryFoodFground { get => _CategoryFoodFground; set { _CategoryFoodFground = value; OnPropertyChanged("CategoryFoodFground"); } }
        private Brush _CategoryFoodBackground { get; set; }
        public Brush CategoryFoodBackground { get => _CategoryFoodBackground; set { _CategoryFoodBackground = value; OnPropertyChanged("CategoryFoodBackground"); } }

        private Brush _CategoryFoodBorderBrush { get; set; }
        public Brush CategoryFoodBorderBrush { get => _CategoryFoodBorderBrush; set { _CategoryFoodBorderBrush = value; OnPropertyChanged("CategoryFoodBorderBrush"); } }

        private Brush _CategoryBBQForeground { get; set; }
        public Brush CategoryBBQForeground { get => _CategoryBBQForeground; set { _CategoryBBQForeground = value; OnPropertyChanged("CategoryBBQForeground"); } }
        private Brush _CategoryBBQFground { get; set; }
        public Brush CategoryBBQFground { get => _CategoryBBQFground; set { _CategoryBBQFground = value; OnPropertyChanged("CategoryBBQFground"); } }
        private Brush _CategoryBBQBackground { get; set; }
        public Brush CategoryBBQBackground { get => _CategoryBBQBackground; set { _CategoryBBQBackground = value; OnPropertyChanged("CategoryBBQBackground"); } }
        private Brush _CategoryBBQBorderBrush { get; set; }
        public Brush CategoryBBQBorderBrush { get => _CategoryBBQBorderBrush; set { _CategoryBBQBorderBrush = value; OnPropertyChanged("CategoryBBQBorderBrush"); } }

        private Brush _CategoryDrinkForeground { get; set; }
        public Brush CategoryDrinkForeground { get => _CategoryDrinkForeground; set { _CategoryDrinkForeground = value; OnPropertyChanged("CategoryDrinkForeground"); } }
        private Brush _CategoryDrinkFground { get; set; }
        public Brush CategoryDrinkFground { get => _CategoryDrinkFground; set { _CategoryDrinkFground = value; OnPropertyChanged("CategoryDrinkFground"); } }
        private Brush _CategoryDrinkBackground { get; set; }
        public Brush CategoryDrinkBackground { get => _CategoryDrinkBackground; set { _CategoryDrinkBackground = value; OnPropertyChanged("CategoryDrinkBackground"); } }
        private Brush _CategoryDrinkBorderBrush { get; set; }
        public Brush CategoryDrinkBorderBrush { get => _CategoryDrinkBorderBrush; set { _CategoryDrinkBorderBrush = value; OnPropertyChanged("CategoryDrinkBorderBrush"); } }
        private Brush _CategoryOtherForeground { get; set; }
        public Brush CategoryOtherForeground { get => _CategoryOtherForeground; set { _CategoryOtherForeground = value; OnPropertyChanged("CategoryOtherForeground"); } }
        private Brush _CategoryOtherFground { get; set; }
        public Brush CategoryOtherFground { get => _CategoryOtherFground; set { _CategoryOtherFground = value; OnPropertyChanged("CategoryOtherFground"); } }
        private Brush _CategoryOtherBackground { get; set; }
        public Brush CategoryOtherBackground { get => _CategoryOtherBackground; set { _CategoryOtherBackground = value; OnPropertyChanged("CategoryOtherBackground"); } }
        private Brush _CategoryOtherBorderBrush { get; set; }
        public Brush CategoryOtherBorderBrush { get => _CategoryOtherBorderBrush; set { _CategoryOtherBorderBrush = value; OnPropertyChanged("CategoryOtherBorderBrush"); } }
        private Brush _CategorySeaFoodForeground { get; set; }
        public Brush CategorySeaFoodForeground { get => _CategorySeaFoodForeground; set { _CategorySeaFoodForeground = value; OnPropertyChanged("CategorySeaFoodForeground"); } }
        private Brush _CategorySeaFoodFground { get; set; }
        public Brush CategorySeaFoodFground { get => _CategorySeaFoodFground; set { _CategorySeaFoodFground = value; OnPropertyChanged("CategorySeaFoodFground"); } }
        private Brush _CategorySeaFoodBackground { get; set; }
        public Brush CategorySeaFoodBackground { get => _CategorySeaFoodBackground; set { _CategorySeaFoodBackground = value; OnPropertyChanged("CategorySeaFoodBackground"); } }

        private Brush _CategorySeaFoodBorderBrush { get; set; }
        public Brush CategorySeaFoodBorderBrush { get => _CategorySeaFoodBorderBrush; set { _CategorySeaFoodBorderBrush = value; OnPropertyChanged("CategorySeaFoodBorderBrush"); } }

        private Brush _DiscountForeground { get; set; }
        public Brush DiscountForeground { get => _DiscountForeground; set { _DiscountForeground = value; OnPropertyChanged("DiscountForeground"); } }
        private Brush _DiscountBackground { get; set; }
        public Brush DiscountBackground { get => _DiscountBackground; set { _DiscountBackground = value; OnPropertyChanged("DiscountBackground"); } }
        private Brush _DiscountBorderBrush { get; set; }
        public Brush DiscountBorderBrush { get => _DiscountBorderBrush; set { _DiscountBorderBrush = value; OnPropertyChanged("DiscountBorderBrush"); } }

        private Brush _VATForeground { get; set; }
        public Brush VATForeground { get => _VATForeground; set { _VATForeground = value; OnPropertyChanged("VATForeground"); } }
        private Brush _VATBackground { get; set; }
        public Brush VATBackground { get => _VATBackground; set { _VATBackground = value; OnPropertyChanged("VATBackground"); } }
        private Brush _VATBorderBrush { get; set; }
        public Brush VATBorderBrush { get => _VATBorderBrush; set { _VATBorderBrush = value; OnPropertyChanged("VATBorderBrush"); } }

        private Visibility _DiscountVisibility { get; set; }
        public Visibility DiscountVisibility { get => _DiscountVisibility; set { _DiscountVisibility = value; OnPropertyChanged("DiscountVisibility"); } }

        private Visibility _AddFoodOtherVisibility { get; set; }
        public Visibility AddFoodOtherVisibility { get => _AddFoodOtherVisibility; set { _AddFoodOtherVisibility = value; OnPropertyChanged("AddFoodOtherVisibility"); } }

        private Visibility _CancelFoodVisibility { get; set; }
        public Visibility CancelFoodVisibility { get => _CancelFoodVisibility; set { _CancelFoodVisibility = value; OnPropertyChanged("CancelFoodVisibility"); } }

        #region toan
        private Visibility _VatVisibility { get; set; }
        public Visibility VatVisibility { get => _VatVisibility; set { _VatVisibility = value; OnPropertyChanged("VatVisibility"); } }

        private Visibility _VatAmountVisibility { get; set; }
        public Visibility VatAmountVisibility { get => _VatAmountVisibility; set { _VatAmountVisibility = value; OnPropertyChanged("VatAmountVisibility"); } }

        #endregion
        private string _AddTaleVisibility { get; set; }
        public string AddTaleVisibility { get => _AddTaleVisibility; set { _AddTaleVisibility = value; OnPropertyChanged("AddTaleVisibility"); } }
        private BitmapImage _SeaFoodIcon { get; set; }
        public BitmapImage SeaFoodIcon { get => _SeaFoodIcon; set { _SeaFoodIcon = value; OnPropertyChanged("SeaFoodIcon"); } }
        private BitmapImage _OtherIcon { get; set; }
        public BitmapImage OtherIcon { get => _OtherIcon; set { _OtherIcon = value; OnPropertyChanged("OtherIcon"); } }

        private BitmapImage _DrinkIcon { get; set; }
        public BitmapImage DrinkIcon { get => _DrinkIcon; set { _DrinkIcon = value; OnPropertyChanged("DrinkIcon"); } }
        private BitmapImage _FoodIcon { get; set; }
        public BitmapImage FoodIcon { get => _FoodIcon; set { _FoodIcon = value; OnPropertyChanged("FoodIcon"); } }


        private bool _IsCheckVat { get; set; }
        public bool IsCheckVat { get => _IsCheckVat; set { _IsCheckVat = value; OnPropertyChanged("IsCheckVat"); } }

        private bool _IsCheck { get; set; }
        public bool IsCheck { get => _IsCheck; set { _IsCheck = value; OnPropertyChanged("IsCheck"); } }
        public ICommand AddExtraChargeCommand { get; set; }
        public ICommand FilterFoodCommand { get; set; }
        public ICommand CancelFood { get; set; }
        public ICommand AddFood { get; set; }
        public ICommand IsCheckCommand { get; set; }
        public ICommand WrittionChangeCommand { get; set; }
        public ICommand AddFoodOther { get; set; }

        public ICommand GiftFoodCommand { get; set; }

        public ICommand TextChangeAmount { get; set; }
        public ICommand AddSlotCustomer { get; set; }

        public ICommand CancelOrderCommand { get; set; }

        public ICommand SaveOrderCommand { get; set; }

        public ICommand SaveAndAddOrderCommand { get; set; }

        public ICommand PaymentOrderCommand { get; set; }

        public ICommand DiscountCommand { get; set; }

        public ICommand VATCommand { get; set; }

        public ICommand SendCookOrderCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }

        public ICommand ReturnFood { get; set; }
        public ICommand AddTableCommand { get; set; }

        public ICommand NoteFood { get; set; }
        public ICommand BtnCategories { get; set; }

        public ICommand CategoryFoodCommand { get; set; }

        public ICommand CategoryDrinkCommand { get; set; }

        public ICommand CategoryOtherCommand { get; set; }

        public ICommand CategorySeaFoodCommand { get; set; }

        public ICommand SubQuantityCommand { get; set; }

        public ICommand AddQuantityCommand { get; set; }
        public ICommand AddFoodCommand { get; set; }
        public ICommand ReturnBeer { get; set; }

        private static ContentControl _MainContentControl;

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        public SettingData currentSetting = (SettingData)Utils.Utils.GetCacheValue(Constants.CURRENT_SETTING);

        public DeviceClient deviceClient = new DeviceClient();

        public List<BillResponse> OrderSendServerNotIsGift;

        public List<BillResponse> OrderSendServerIsGift;

        public string TableName = "";

        public bool IsBBq;

        public bool SetTablePermission = true;

        public long CategoryType;

        public long CategoryId;

        public DiscountWrapper DiscountCount = new DiscountWrapper();

        public long CurrentTableId = 0;

        public long CurrentOrderId = 0;

        public long OldOrderId = 0;
        public bool SendCook = true;
        public List<FoodMenuItem> AllFood = new List<FoodMenuItem>();
        public List<Category> AllCategory = new List<Category>();
        private List<Kitchen> CurrentKitchens = Utils.Utils.AsObjectList<Kitchen>(Properties.Settings.Default.RestaurantKitchenPlacePrint);
        #region toan sua
        //public async void GetAllFoodLisst()
        //{
        //    FoodsClient client = new FoodsClient(this, this, this);
        //    DialogHostOpen = true;
        //    FoodResponses response = await Task.Run(() => client.GetFoodsUSing(Constants.ALL, currentUser.BranchId, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL,1));
        //    if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
        //    {
        //        foreach (Food food in response.Data)
        //        {
        //            if (AllFood == null)
        //            {
        //                AllFood = new List<FoodMenuItem>();
        //                AllFood.Add(Utils.Utils.AddFoodMenu(food));
        //            }
        //            else
        //            {
        //                AllFood.Add(Utils.Utils.AddFoodMenu(food));
        //            }
        //            //AllFood.Add(Utils.Utils.AddFoodMenu(food));
        //        }
        //        Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, AllFood, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
        //        if (CurrentOrderId == 0)
        //            GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
        //        DialogHostOpen = false;
        //    }
        //    else
        //        DialogHostOpen = false;
        //}
        public void GetAllFoodLisst()
        {
            FoodsClient client = new FoodsClient(this, this, this);
            DialogHostOpen = true;
            FoodResponses response = client.GetFoodsUSing(Constants.ALL, currentUser.BranchId, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, Constants.ALL, 1);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                foreach (Food food in response.Data)
                {
                    if (AllFood == null)
                    {
                        AllFood = new List<FoodMenuItem>();
                        AllFood.Add(Utils.Utils.AddFoodMenu(food));
                    }
                    else
                    {
                        AllFood.Add(Utils.Utils.AddFoodMenu(food));
                    }
                    //AllFood.Add(Utils.Utils.AddFoodMenu(food));
                }
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_FOOD, AllFood, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                if (CurrentOrderId == 0)
                    GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
        }
        #endregion
        //public async void GetAllCategoryList()
        //{
        //    CategoriesClient client = new CategoriesClient(this, this, this);
        //    DialogHostOpen = true;
        //    CategoryResponse categoryReponse = await Task.Run(() => client.GetAllCategory(Constants.STATUS, "", currentUser.RestaurantBrandId));
        //    if (categoryReponse != null && categoryReponse.Status == (int)ResponseEnum.OK)
        //    {
        //        Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_CATEGORY, categoryReponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
        //        categoryReponse.Data.ForEach(AllCategory.Add);
        //        if (CurrentOrderId == 0)
        //            GetCategoryMenu((int)CategoryTypeEnum.FOOD);
        //        Constants.IS_FIRST_CATEGORY = false;
        //        DialogHostOpen = false;
        //    }
        //}
        public void GetAllCategoryList()
        {
            CategoriesClient client = new CategoriesClient(this, this, this);
            DialogHostOpen = true;
            CategoryResponse categoryReponse = client.GetAllCategory(Constants.STATUS, "", currentUser.RestaurantBrandId);
            if (categoryReponse != null && categoryReponse.Status == (int)ResponseEnum.OK)
            {
                Utils.Utils.AddCacheValue(Constants.CURRENT_LIST_CATEGORY, categoryReponse.Data, DateTimeOffset.Now.AddDays(Constants.MAX_EXPIRE_CACHE));
                categoryReponse.Data.ForEach(AllCategory.Add);
                if (CurrentOrderId == 0)
                    GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                Constants.IS_FIRST_CATEGORY = false;
                DialogHostOpen = false;
            }
        }
        public void GetCategoryMenu(long CategoryType)
        {
            if (ItemsCategory == null)
            {
                ItemsCategory = new ObservableCollection<Category>();
            }
            else
            {
                ItemsCategory.Clear();
            }
            Category category = new Category();
            category.Id = Constants.ALL;
            category.Name = MessageValue.MESSAGE_ALL;
            category.IsChoose = true;
            ItemsCategory.Add(category);
            if (AllCategory != null)
            {
                AllCategory.Where(x => x.CategoryType == CategoryType).ForEach(ItemsCategory.Add);

                foreach (Category c in ItemsCategory.ToList())
                {
                    if (c.Id != Constants.ALL && c.IsChoose)
                    {
                        int index = ItemsCategory.IndexOf(c);
                        ItemsCategory.Remove(c);
                        c.IsChoose = false;
                        ItemsCategory.Insert(index, c);
                    }
                }
            }
        }
        public FoodMenuItem AddFoodMenu(Food f)
        {
            string priceFood = "";
            if (f.Price % 1000 == 0)
            {
                priceFood = f.Price / 1000 + "K";

            }
            else
            {
                if (f.Price > 1000)
                {
                    priceFood = f.Price / 1000 + "K" + (f.Price / 1000) / 100;
                }
                else
                {
                    priceFood = f.Price.ToString();
                }
            }
            return new FoodMenuItem(f.Id, new BitmapImage(new Uri(f.Avatar, UriKind.RelativeOrAbsolute)), priceFood, f.Price, false, f.Prefix, f.NormalizeName, (int)f.CategoryTypeId, f.IsBbq == Constants.STATUS ? true : false, f.Name, f.CategoryId, f.UnitType, f.AdditionFoods, f.IsAllowPrint == 1 ? true : false, f.Code, f.FoodInCombo, f.IsSellByWeight, f.FoodInPromotion,f.IsAllowEmployeeGift);
        }
        public void GetFoodMenu(long CategoryType, long CategoryId, bool isChooseCategoryType)
        {
            if (ItemsOrderDetail == null)
            {
                ItemsOrderDetail = new ObservableCollection<FoodMenuItem>();
            }
            else
            {
                ItemsOrderDetail.Clear();
            }
            if (!isChooseCategoryType)
            {
                if (AllFood != null)
                {
                    // IsBBq = BBQ;
                    this.CategoryId = CategoryId;
                    this.CategoryType = CategoryType;
                    if (CategoryId != Constants.ALL)
                        AllFood.Where(x => x.CategoryTypeId == CategoryType && x.CategoryId == CategoryId).ToList().ForEach(ItemsOrderDetail.Add);
                    else
                        AllFood.Where(x => x.CategoryTypeId == CategoryType).ToList().ForEach(ItemsOrderDetail.Add);
                }
                Category category = ItemsCategory.Where(x => x.IsChoose).FirstOrDefault();
                if (CategoryId != category.Id)
                {
                    if (category != null)
                    {
                        int index = ItemsCategory.IndexOf(category);
                        ItemsCategory.Remove(category);
                        category.IsChoose = false;
                        ItemsCategory.Insert(index, category);
                    }
                    Category c = ItemsCategory.Where(x => x.Id == CategoryId).FirstOrDefault();
                    if (c != null)
                    {
                        int index = ItemsCategory.IndexOf(c);
                        ItemsCategory.Remove(c);
                        c.IsChoose = true;
                        ItemsCategory.Insert(index, c);
                    }
                }
            }
            else
            {
                if (AllFood != null)
                {
                    // IsBBq = BBQ;
                    this.CategoryId = CategoryId;
                    this.CategoryType = CategoryType;
                    if (CategoryId != Constants.ALL)
                        AllFood.Where(x => x.CategoryTypeId == CategoryType && x.CategoryId == CategoryId).ToList().ForEach(ItemsOrderDetail.Add);
                    else
                        AllFood.Where(x => x.CategoryTypeId == CategoryType).ToList().ForEach(ItemsOrderDetail.Add);
                }
            }
        }
        public void FilterFood(string filter)
        {
            if (AllFood != null)
            {
                if (ItemsOrderDetail == null)
                {
                    ItemsOrderDetail = new ObservableCollection<FoodMenuItem>();
                }
                else
                {
                    ItemsOrderDetail.Clear();
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    AllFood.Where(x => x.ContentFoodName.ToString().ToLower().IndexOf(filter.ToLower()) >= 0
                      || x.NormalizeName.ToString().ToLower().IndexOf(filter.Trim().ToLower()) >= 0
                      || x.Code.ToString().ToLower().IndexOf(filter.ToLower()) >= 0
                      || x.Code.ToString().ToLower().IndexOf(filter.Trim().ToLower()) >= 0
                      || x.Prefix.ToString().ToLower().IndexOf(filter.ToLower()) >= 0
                      || x.NormalizeName.ToString().ToLower().IndexOf(filter.Trim().ToLower()) >= 0
                      || x.ContentFoodName.ToString().ToLower().IndexOf(filter.Trim().ToLower()) >= 0
                      || x.Prefix.ToString().ToLower().IndexOf(filter.Trim().ToLower()) >= 0).ToList().ForEach(ItemsOrderDetail.Add);
                }
                else
                {
                    GetCategoryMenu(CategoryType);
                    GetFoodMenu(CategoryType, CategoryId, false);
                }
            }
        }
        public void GetTotalAmount()
        {
            if (FoodOrderList != null && FoodOrderList.Count > 0)
            {
                decimal amount = (decimal)FoodOrderList.Where(x => x.IsGift == 0).Sum(x => x.TotalPrice);
                if (DiscountCount != null && DiscountCount.DiscountPercent > 0)
                {
                    decimal discountAmount = 0;
                    if (DiscountCount.DiscountType == (int)DiscountEnum.ALL_BILL)
                    {
                        decimal discount = Math.Round((decimal)DiscountCount.DiscountPercent / 100, Constants.ROUND_TWO);
                        discountAmount = amount * discount;
                        amount = amount - discountAmount;
                        //thuc
                        MainSecondViewModel.Discount = discount;
                    }
                    else if (DiscountCount.DiscountType == (int)DiscountEnum.FOOD_BILL)
                    {
                        decimal discount = Math.Round((decimal)DiscountCount.DiscountPercent / 100, Constants.ROUND_TWO);
                        discountAmount = (decimal)FoodOrderList.Where(x => x.IsGift == 0 && (x.CategoryType == (int)CategoryTypeEnum.FOOD || x.CategoryType == (int)CategoryTypeEnum.SEA_FOOD)).Sum(x => x.TotalAmount) * discount;
                        amount = amount - discountAmount;
                        MainSecondViewModel.Discount = discount;
                    }
                    Discount = string.Format("{0}%", DiscountCount.DiscountPercent);
                    DiscountAmount = Utils.Utils.FormatMoney(discountAmount);
                }
                if (IsCheckVat)
                {
                    decimal vat = Math.Round((decimal)currentSetting.Vat / 100, Constants.ROUND_TWO);
                    amount = amount + amount * vat;
                    Vat = string.Format("{0}%", currentSetting.Vat);
                    VatAmount = Utils.Utils.FormatMoney(amount * vat);
                    //thuc
                    MainSecondViewModel.Vat = vat;
                }
                else
                {
                    decimal vat = 0;
                    amount = amount + amount * vat;
                    Vat = string.Format("{0}%", 0);
                    VatAmount = Utils.Utils.FormatMoney(amount * vat);
                    //thuc
                    MainSecondViewModel.Vat = vat;
                }
                TotalAmount = Utils.Utils.FormatMoney(amount);
            }
            else
            {
                TotalAmount = "0";
            }
            MainSecondViewModel.TotalAmount = TotalAmount;
        }
        public void UpdateResponse(OrderBillData Data)
        {
            DialogHostOpen = true;
            CurrentOrderId = Data.Id;
            if (Data.IsTakeAway == Constants.STATUS)
            {
                TableContent = MessageValue.MESSAGE_NOT_TABLE_DEFAULT;
            }
            else
            {
                TableContent = Data.TableId == 0 ? MessageValue.MESSAGE_NOT_TABLE_DEFAULT : Data.TableName;
            }
            CurrentTableId = Data.TableId;
            SlotCustomerContent = (int)Data.CustomerSlotNumber;
            TotalAmount = Utils.Utils.FormatMoney(Data.TotalAmount);

            DiscountCount = new DiscountWrapper();
            DiscountCount.DiscountType = (int)Data.DiscountType;
            DiscountCount.DiscountPercent = Data.DiscountPercent;
            if (Data.Foods != null && Data.Foods.Count() > 0)
            {
                if (FoodOrderList != null)
                    FoodOrderList.Clear();
                else
                    FoodOrderList = new ObservableCollection<BillResponse>();
                foreach (BillResponse o in Data.Foods)
                {
                    if (o.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL && o.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)
                    {
                        o.IsPrint = true;
                        o.Price = o.UnitPrice;
                        o.OldQuantity = o.Quantity;
                        if (o.OrderAdditions == null)
                        {
                            o.OrderAdditions = new List<BillResponse>();
                        }
                        if (o.OrderDetailAdditions == null)
                        {
                            o.OrderDetailAdditions = new List<BillResponse>();

                        }
                        else
                        {
                            foreach (BillResponse additionfood in o.OrderDetailAdditions)
                            {
                                additionfood.IsPrint = true;
                                additionfood.OnlyViewQuantity = additionfood.Quantity;
                            }
                        }
                        FoodOrderList.Insert(0, o);


                    }
                }
                DialogHostOpen = false;
            }
        }
        public async void PayMent(UserControl t)
        {
            if (FoodOrderList != null && FoodOrderList.Count > 0)
            {
                // _key = true;
                //  _payment = true;
                SendCookOrder();
                if (IsCheck == true)
                {
                    OrdersClient client = new OrdersClient(this, this, this);
                    OrderItemResponse orderBillResponse = client.GetOrderById(CurrentOrderId, currentUser.BranchId, Constants.STATUS, Constants.STATUS);
                    if (orderBillResponse != null && orderBillResponse.Status == (int)ResponseEnum.OK && orderBillResponse.Data != null)
                    {
                        DialogHostOpen = false;
                        if (CurrentOrderId != 0)
                        {
                            PaymentOrderWindow paymentWindow = new PaymentOrderWindow();
                            paymentWindow.DataContext = new PaymentOrderViewModel(orderBillResponse.Data.Id, orderBillResponse.Data.TotalAmount, orderBillResponse.Data.Amount, orderBillResponse.Data.Vat,
                                orderBillResponse.Data.VatAmount, orderBillResponse.Data.DiscountAmount, orderBillResponse.Data.DiscountPercent, orderBillResponse.Data.MembershipTotalPointUsed,
                                orderBillResponse.Data.ShippingFee, orderBillResponse.Data.BookingDepositAmount, orderBillResponse.Data.OrderStatus, orderBillResponse.Data.IsReturnDeposit == 0 ? 0 : orderBillResponse.Data.BookingDepositPaymentMethod);
                            paymentWindow.ShowDialog();
                            var baseComplete = paymentWindow.DataContext as PaymentOrderViewModel;
                            if (baseComplete.isSUCCESS == true)
                            {
                                FrameworkElement window = GetWindowParent(t);
                                var w = window as Window;
                                _MainContentControl = w.FindName("ContentCt") as ContentControl;
                                _MainContentControl.Content = new HomeUserControl();
                            }
                            else
                            {
                                await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(FoodOrderList.Select(x => x.Id).ToList()),CurrentOrderId));
                                //await Task.Run(() => client.PostOrderDetailPrint(CurrentOrderId)); // Ha sua
                            }
                        }
                    }
                    else
                        DialogHostOpen = false;
                }
                else
                {
                    return;


                }
            }
        }
            public void CreateTable()
            {
                OrdersClient client = new OrdersClient(this, this, this);
                CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                CreateOrderResponse response = client.CreateOrder(wrapper);
                OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
            if (openTableById != null && openTableById.Status == (int)ResponseEnum.OK)
            {
                if (response != null && response.Data != null)
                {
                    AddExtraChargeVisibility = Visibility.Visible;
                    CurrentOrderId = response.Data.OrderId;
                    if (FoodOrderList != null && FoodOrderList.Count > 0)
                    {
                        AddFoodOrderWrapper addFoodOrderWrapper = new AddFoodOrderWrapper();
                        addFoodOrderWrapper.Foods = new List<BillResponse>();
                        foreach (BillResponse o in FoodOrderList.Where(x => x.IsExtraCharge == 0).ToList())
                        {
                            addFoodOrderWrapper.Foods.Add(o);
                            o.IsPrint = false;
                        }
                        OrderItemResponse addFood = client.AddFoodOrder(CurrentOrderId, addFoodOrderWrapper);
                    }
                }
            }
            }

        public async void SendCookOrder()
        {
            Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
            OrdersClient client = new OrdersClient(this, this, this);
            if (CurrentOrderId == 0)
            {
                BillResponse bill = foodOrderList.Where(x => x.IsGift == 0).FirstOrDefault();
                if (bill != null)
                {
                    IsCheck = true;
                    DialogHostOpen = true;
                    if (CurrentTableId == 0) // Check chọn bàn hay chưa ?
                    {
                        DialogHostOpen = false;
                        ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                        string contentConfirm = MessageValue.MESSAGE_NOT_TABLE_MOVE;
                        string Title = MessageValue.MESSAGE_NOTIFICATION;
                        string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                        string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                        confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                        confirmDeleteWindow.ShowDialog();
                        var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                        if (confirm.isConfirm)
                        {
                            SetTableByOrder();
                            IsCheck = false;
                            #region tạm đóng
                            //TableOrderWindow table = new TableOrderWindow();
                            //table.DataContext = new TableViewModel((int)CurrentTableId, MessageValue.MESSAGE_FROM_ORDER_CHOOSE_TABLE_TITLE, false);//toan
                            //table.ShowDialog();
                            //var tableViewModel = table.DataContext as TableViewModel;
                            //if (tableViewModel != null && tableViewModel.table != null)
                            //{
                            //CurrentTableId = tableViewModel.table.Id;
                            //TableContent = tableViewModel.table.Name;
                            //CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                            //OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
                            //if (tableViewModel.table.TableStatus != 2)
                            //{
                            //    if (openTableById != null && openTableById.Status == (int)ResponseEnum.OK)
                            //    {
                            //        CreateOrderResponse response = client.CreateOrder(wrapper);
                            //        if (response != null && response.Data != null)
                            //        {
                            //            AddExtraChargeVisibility = Visibility.Visible;
                            //            CurrentOrderId = response.Data.OrderId;
                            //            if (FoodOrderList != null && FoodOrderList.Count > 0)
                            //            {
                            //                AddFoodOrderWrapper addFoodOrderWrapper = new AddFoodOrderWrapper();
                            //                addFoodOrderWrapper.Foods = new List<BillResponse>();
                            //                AddFoodOrderWrapper giftFoodOrderWrapper = new AddFoodOrderWrapper();
                            //                giftFoodOrderWrapper.Foods = new List<BillResponse>();
                            //                foreach (BillResponse o in FoodOrderList.Where(x => x.IsExtraCharge == 0).ToList())
                            //                {
                            //                    o.Id = o.FoodId;
                            //                    if (o.IsGift == 1)
                            //                    {
                            //                        giftFoodOrderWrapper.Foods.Add(o);
                            //                    }
                            //                    else
                            //                    {
                            //                        #region Dat
                            //                        if (o.OrderAdditions.Count > 0)
                            //                        {
                            //                            foreach (BillResponse item in o.OrderAdditions.ToList())
                            //                            {
                            //                                if (item.Quantity == 0)
                            //                                {
                            //                                    o.OrderAdditions.Remove(item);
                            //                                }
                            //                            }
                            //                            Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                            //                            addFoodOrderWrapper.Foods.Add(o);
                            //                        }
                            //                        else if (o.FoodInPrmotion.Count > 0) // mua 1 tặng 1
                            //                        {
                            //                            foreach (BillResponse item in o.FoodInPrmotion.ToList())
                            //                            {
                            //                                if (item.Quantity == 0)
                            //                                {
                            //                                    o.FoodInPrmotion.Remove(item);
                            //                                }
                            //                            }
                            //                            Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                            //                            addFoodOrderWrapper.Foods.Add(o);
                            //                        }
                            //                        else
                            //                        {
                            //                            addFoodOrderWrapper.Foods.Add(o);
                            //                        }
                            //                        #endregion
                            //                        //addFoodOrderWrapper.Foods.Add(o);
                            //                    }
                            //                }
                            //                OrderItemResponse addFood = client.AddFoodOrder(CurrentOrderId, addFoodOrderWrapper); // Goi api (orderId, List Food)

                            //                if (giftFoodOrderWrapper.Foods.Count != 0)
                            //                {
                            //                    OrderItemResponse giftFood = client.GiftFoodOrder(CurrentOrderId, giftFoodOrderWrapper);
                            //                }

                            //                if (addFood != null && addFood.Status == (int)ResponseEnum.OK && addFood.Data != null)
                            //                {

                            //                    OrderDetailPrintResponse orderDetailPrintResponse = client.GetOrderDetailPrint(CurrentOrderId);//toan
                            //                    if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                            //                    {
                            //                        // List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                            //                        foreach (Kitchen k in CurrentKitchens)
                            //                        {
                            //                            List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                            //                            if (result != null && result.Count > 0)
                            //                            {
                            //                                //pt.PrintFoodCook(k.PrinterName, result, addFood.Data.TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                            //                                pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                            //                        string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                            //                            }

                            //                            List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity < x.OldQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                            //                            if (result != null && result.Count > 0)
                            //                            {
                            //                                foreach (OrderDetailPrint od in resultUpdate)
                            //                                {
                            //                                    pt.PrintFoodMove(od, k.PrinterName, addFood.Data.EmployeeName, addFood.Data.TableName, (int)k.PrinterPaperSize);
                            //                                }
                            //                            }
                            //                        }
                            //                        IsPrintSendFoodCookWrapper wrapperIsPrint = new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList());
                            //                        //await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList()),CurrentOrderId));
                            //                        await Task.Run(() => client.UpdateOrderIsPrintSendCook(wrapperIsPrint, CurrentOrderId));
                            //                        //await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(FoodOrderList.Select(x => x.Id).ToList(), CurrentOrderId)));
                            //                        //await Task.Run(() => client.PostOrderDetailPrint(CurrentOrderId));  // ha sua
                            //                        //UpdateResponse(addFood.Data);
                            //                        #region Dat
                            //                        if (isCheckSave == false)
                            //                        {
                            //                            GetDetail(CurrentOrderId);
                            //                        }
                            //                        #endregion

                            //                    }

                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    IsCheck = false;
                            //    DialogHostOpen = false;
                            //    NotificationMessage.Warning(MessageValue.MESSAGE_TABLE_NOT_EMPTY);
                            //    return;
                            //}                              
                            //}
                            #endregion
                        }
                    }
                    else
                    {
                        DialogHostOpen = true;
                        CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                        OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
                        if (openTableById != null && openTableById.Status == (int)ResponseEnum.OK)
                        {
                            CreateOrderResponse response = client.CreateOrder(wrapper);
                            if (response != null && response.Data != null)
                            {
                                AddExtraChargeVisibility = Visibility.Visible;
                                CurrentOrderId = response.Data.OrderId;
                                if (FoodOrderList != null && FoodOrderList.Count > 0)
                                {
                                    AddFoodOrderWrapper addFoodOrderWrapper = new AddFoodOrderWrapper();
                                    addFoodOrderWrapper.Foods = new List<BillResponse>();
                                    AddFoodOrderWrapper giftFoodOrderWrapper = new AddFoodOrderWrapper();
                                    giftFoodOrderWrapper.Foods = new List<BillResponse>();
                                    foreach (BillResponse o in FoodOrderList.Where(x => x.IsExtraCharge == 0).ToList())
                                    {
                                        //decimal quantity = o.Quantity - o.OldQuantity;
                                        //if (quantity != 0)
                                        //{
                                        //    o.Id = o.FoodId;
                                        //    o.Quantity = o.Quantity - o.OldQuantity;
                                        //    addFoodOrderWrapper.Foods.Add(o);
                                        //}
                                        o.Id = o.FoodId;
                                        if (o.IsGift == 1)
                                        {
                                            giftFoodOrderWrapper.Foods.Add(o);
                                        }
                                        else
                                        {
                                            #region Dat
                                            if (o.OrderAdditions.Count > 0)
                                            {
                                                foreach (BillResponse item in o.OrderAdditions.ToList())
                                                {
                                                    if (item.Quantity == 0)
                                                    {
                                                        o.OrderAdditions.Remove(item);
                                                    }
                                                }
                                                Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                                                addFoodOrderWrapper.Foods.Add(o);
                                            }
                                            else if (o.FoodInPrmotion.Count > 0) // mua 1 tặng 1
                                            {
                                                foreach (BillResponse item in o.FoodInPrmotion.ToList())
                                                {
                                                    if (item.Quantity == 0)
                                                    {
                                                        o.FoodInPrmotion.Remove(item);
                                                    }
                                                }
                                                Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                                                addFoodOrderWrapper.Foods.Add(o);
                                            }
                                            else
                                            {
                                                addFoodOrderWrapper.Foods.Add(o);
                                            }
                                            #endregion
                                            //addFoodOrderWrapper.Foods.Add(o);
                                        }
                                    }

                                    OrderItemResponse addFood = client.AddFoodOrder(CurrentOrderId, addFoodOrderWrapper);

                                    if (giftFoodOrderWrapper.Foods.Count != 0)
                                    {
                                        OrderItemResponse giftFood = client.GiftFoodOrder(CurrentOrderId, giftFoodOrderWrapper);
                                    }
                                    if (addFood != null && addFood.Status == (int)ResponseEnum.OK && addFood.Data != null)
                                    {

                                        OrderDetailPrintResponse orderDetailPrintResponse = client.GetOrderDetailPrint(CurrentOrderId);
                                        //OrderDetailPrintResponse orderDetailPrintResponse = client.GetOrderDetailPrint(CurrentOrderId);

                                        if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                                        {
                                            List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                                            foreach (Kitchen k in CurrentKitchens)
                                            {
                                                WriteLog.logs(k.Id.ToString());
                                                List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                                if (result != null && result.Count > 0)
                                                { 
                                                    //pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                                                    pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                                    string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                                                }
                                                List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity < x.OldQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                                if (result != null && result.Count > 0)
                                                {
                                                    foreach (OrderDetailPrint od in resultUpdate)
                                                    {
                                                        pt.PrintFoodMove(od, k.PrinterName, addFood.Data.EmployeeName, addFood.Data.TableName, (int)k.PrinterPaperSize);
                                                    }
                                                }
                                            }
                                            //IsPrintSendFoodCookWrapper wrapperIsPrint = new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList());
                                            await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList()), CurrentOrderId));
                                            //client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList()), CurrentOrderId); 
                                            //await Task.Run(() => client.UpdateOrderIsPrintSendCook(wrapperIsPrint, CurrentOrderId));
                                            //await Task.Run(() => client.PostOrderDetailPrint(CurrentOrderId));

                                            //UpdateResponse(addFood.Data);
                                            #region Dat
                                            if (isCheckSave == false)
                                            {
                                                GetDetail(CurrentOrderId);
                                            }
                                            #endregion
                                        }

                                    }
                                }
                            }
                        }
                    }
                    DialogHostOpen = false;
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_FORM_TAKE_AWAY_GIFT_ERROR);
                    IsCheck = false;
                }
            }
            else
            {
                IsCheck = true;
                if (FoodOrderList != null && FoodOrderList.Count > 0)
                {
                    AddFoodOrderWrapper addFoodOrderWrapper = new AddFoodOrderWrapper();
                    addFoodOrderWrapper.Foods = new List<BillResponse>();
                    AddFoodOrderWrapper giftFoodOrderWrapper = new AddFoodOrderWrapper();
                    giftFoodOrderWrapper.Foods = new List<BillResponse>();
                    foreach (BillResponse o in FoodOrderList.Where(x => x.IsExtraCharge == 0).ToList())
                    {
                        decimal quantity = o.Quantity - o.OldQuantity;
                        if (quantity != 0)
                        {
                            o.Id = o.FoodId;
                            o.Quantity = o.Quantity - o.OldQuantity;
                            if (o.IsGift == 1)
                            {
                                giftFoodOrderWrapper.Foods.Add(o);
                            }
                            else
                            {
                                #region Dat
                                if (o.OrderAdditions.Count > 0)
                                {
                                    foreach (BillResponse item in o.OrderAdditions.ToList())
                                    {
                                        if (item.Quantity == 0)
                                        {
                                            o.OrderAdditions.Remove(item);
                                        }
                                    }
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                                    addFoodOrderWrapper.Foods.Add(o);
                                }
                                else if (o.FoodInPrmotion.Count > 0) // mua 1 tặng 1
                                {
                                    foreach (BillResponse item in o.FoodInPrmotion.ToList())
                                    {
                                        if (item.Quantity == 0)
                                        {
                                            o.FoodInPrmotion.Remove(item);
                                        }
                                    }
                                    Utils.Utils.DeleteCacheValue(Constants.CURRENT_LIST_FOOD);
                                    addFoodOrderWrapper.Foods.Add(o);
                                }
                                else
                                {
                                    addFoodOrderWrapper.Foods.Add(o);
                                }
                                #endregion
                                //addFoodOrderWrapper.Foods.Add(o);
                            }
                        }
                    }
                    if (addFoodOrderWrapper != null && addFoodOrderWrapper.Foods != null && addFoodOrderWrapper.Foods.Count > 0 || giftFoodOrderWrapper != null && giftFoodOrderWrapper.Foods != null && giftFoodOrderWrapper.Foods.Count > 0)
                    {
                        OrderItemResponse addFood = new OrderItemResponse();
                        OrderItemResponse giftFood = new OrderItemResponse();
                        if (addFoodOrderWrapper.Foods.Count > 0)
                        {
                            addFood = client.AddFoodOrder(CurrentOrderId, addFoodOrderWrapper);
                        }

                        if (giftFoodOrderWrapper.Foods.Count != 0)
                        {
                            giftFood = client.GiftFoodOrder(CurrentOrderId, giftFoodOrderWrapper);
                        }
                        if (addFood != null && addFood.Status == (int)ResponseEnum.OK && addFood.Data != null || giftFood != null && giftFood.Status == (int)ResponseEnum.OK && giftFood.Data != null)
                        {
                            //UpdateResponse(addFood.Data);
                            OrderDetailPrintResponse orderDetailPrintResponse = await Task.Run(() => client.GetOrderDetailPrint(CurrentOrderId));
                            if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                            {
                                List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                                foreach (Kitchen k in CurrentKitchens)
                                {
                                    List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                    if (result != null && result.Count > 0)
                                    {
                                        // pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                                        pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                                string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                                    }
                                    List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity < x.OldQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                    if (result != null && result.Count > 0)
                                    {
                                        foreach (OrderDetailPrint od in resultUpdate)
                                        {
                                            pt.PrintFoodMove(od, k.PrinterName, addFood.Data.EmployeeName, addFood.Data.TableName, (int)k.PrinterPaperSize);
                                        }
                                    }
                                }
                                //await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList()), CurrentOrderId));
                                client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList()), CurrentOrderId); 
                                //await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.Select(x => x.Id).ToList())));
                            }
                            //UpdateResponse(addFood.Data);
                            #region Dat
                            if (isCheckSave == false)
                            {
                                GetDetail(CurrentOrderId);
                            }
                            #endregion
                        }
                    }
                }

            }
        }
        public async void CancelFoodItem(BillResponse item)
        {
            if (CurrentOrderId == 0)
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
                    BillResponse orderFood = FoodOrderList.Where(x => x.Id == item.Id && x.IsGift == item.IsGift).FirstOrDefault();
                    if (orderFood != null)
                    {
                        FoodOrderList.Remove(orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                }
            }
            else
            {
                if (item.IsExtraCharge != Constants.STATUS)
                {
                    BillResponse oF = FoodOrderList.Where(x => x.FoodId == item.FoodId && x.IsGift == item.IsGift).FirstOrDefault();
                    if (oF != null)
                    {
                        if (oF.IsPrint)
                        {
                            CancelReasonFoodWindow window = new CancelReasonFoodWindow();
                            window.DataContext = new CancelReasonFoodViewModel();
                            window.ShowDialog();
                            var cancelReasonFoodViewModel = window.DataContext as CancelReasonFoodViewModel;
                            if (!string.IsNullOrEmpty(cancelReasonFoodViewModel.CancelReasonString))
                            {
                                BillResponse t = new BillResponse();
                                var prefix = AllFood.Where(y => y.FoodId == t.FoodId).Select(x => x.Prefix).FirstOrDefault();
                                CancelFoodWrapper wrapper = new CancelFoodWrapper();
                                CancelFood cancelFood = new CancelFood(oF.Id, oF.Quantity, cancelReasonFoodViewModel.CancelReasonString);
                                wrapper.CancelFoods = new List<CancelFood>();
                                wrapper.CancelFoods.Add(cancelFood);
                                OrdersClient client = new OrdersClient(this, this, this);
                                BaseResponse baseResponse = client.CancelFood_2(wrapper, prefix, CurrentOrderId, t.Id, currentUser.BranchId);
                                if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                {

                                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                                    SingleOrderDetailResponse response = new SingleOrderDetailResponse();
                                    response.Data = new SingleOrderDetailData();
                                    response.Data.Quantity = oF.Quantity;
                                    response.Data.Note = oF.Note;
                                    response.Data.TableName = TableContent;
                                    response.Data.EmployeeName = currentUser.Name;
                                    response.Data.FoodId = oF.FoodId;
                                    response.Data.FoodName = oF.Name;
                                    response.Data.CreatedAt = oF.CreatedAt;
                                    if (oF != null)
                                    {
                                        FoodOrderList.Remove(oF);
                                    }
                                    Kitchen kitchen = CurrentKitchens.Where(x => x.Id == oF.RestaurantKitchenPlaceId).FirstOrDefault();
                                    if (kitchen != null)
                                    {
                                        pt.PrintFoodCancel(response, kitchen.PrinterName, kitchen.PrinterPaperSize);
                                    }
                                    await Task.Run(() => client.UpdateOrderIsPrintSendCook(new IsPrintSendFoodCookWrapper(FoodOrderList.Select(x => x.Id).ToList()), CurrentOrderId));
                                    //await Task.Run(() => client.PostOrderDetailPrint(CurrentOrderId)); // Ha Sua
                                    GetTotalAmount();
                                }
                            }
                        }
                        else
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
                                BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == item.FoodId && x.IsGift == item.IsGift).FirstOrDefault();
                                if (orderFood != null)
                                {
                                    FoodOrderList.Remove(orderFood);
                                }
                                GetTotalAmount();
                                SendCook = true;
                                SendCookEnabled = true;
                            }
                        }
                    }
                }
                else
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.MESSAGE_CANCEL_EXTRA_CHARGE;
                    string Title = MessageValue.MESSAGE_CANCEL_EXTRA_CHARGE_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {
                        CancelReasonFoodWindow window = new CancelReasonFoodWindow();
                        window.DataContext = new CancelReasonFoodViewModel();
                        window.ShowDialog();
                        var cancelReasonFoodViewModel = window.DataContext as CancelReasonFoodViewModel;
                        if (!string.IsNullOrEmpty(cancelReasonFoodViewModel.CancelReasonString))
                        {
                            RestaurantExtraClient client = new RestaurantExtraClient(this, this, this);
                            BaseResponse response = client.CancelRestaurantExtra(new CancelRestaurantExtraWrapper(item.Id, 1, cancelReasonFoodViewModel.CancelReasonString), CurrentOrderId);
                            if (response != null && response.Status == (int)ResponseEnum.OK)
                            {
                                FoodOrderList.Remove(item);
                                GetTotalAmount();
                            }
                        }
                    }
                }
                GetDetail(CurrentOrderId);
            }
        }
            public void SaveOrder(UserControl t)
            {
                if (FoodOrderList != null && FoodOrderList.Count > 0)
                {
                    //_key = false;
                    //SavedOrder();
                    isCheckSave = true; // Dat
                    SendCookOrder();
                    //CreateTable();
                    if (IsCheck == true)
                    {
                        if (CurrentTableId > 0)
                        {
                            FrameworkElement window = GetWindowParent(t);
                            var w = window as Window;
                            _MainContentControl = w.FindName("ContentCt") as ContentControl;
                            _MainContentControl.Content = new HomeUserControl();
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_ORDER_NOT_EMPTY_TABLE);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            public void CloseOrder(UserControl t)
            {
                if (SendCook)
                {
                    ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                    string contentConfirm = MessageValue.CANCEL_ORDER_CONTENT;
                    string Title = MessageValue.CANCEL_ORDER_TITLE;
                    string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                    string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                    confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                    confirmDeleteWindow.ShowDialog();
                    var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                    if (confirm.isConfirm)
                    {

                        if (FoodOrderList != null)
                        {
                            FoodOrderList.Clear();
                        }
                        GetDetail();
                        FrameworkElement window = GetWindowParent(t);
                        var w = window as Window;
                        _MainContentControl = w.FindName("ContentCt") as ContentControl;
                        _MainContentControl.Content = new HomeUserControl();
                    }
                }
                else
                {
                    FrameworkElement window = GetWindowParent(t);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                    _MainContentControl.Content = new HomeUserControl();
                }
            }
            public void SaveAndAddOrder()
            {
                if (FoodOrderList != null && FoodOrderList.Count > 0)
                {
                    isCheckSave = true; // Dat
                                        //   _key = false;
                    SendCookOrder();
                    //CreateTable();
                    if (CurrentTableId > 0)
                    {
                      //  GetDetail();
                        DialogHostOpen = false;
                        OldOrderId = CurrentOrderId;
                        SendCook = true;
                        SendCookEnabled = true;
                    }
                    else
                    {
                        NotificationMessage.Warning(MessageValue.MESSAGE_ORDER_NOT_EMPTY_TABLE);
                    }
                }
            }
            public void SetTableByOrder()
            {
                TableOrderWindow table = new TableOrderWindow();
                table.DataContext = new TableViewModel((int)CurrentTableId,MessageValue.MESSAGE_FROM_ORDER_CHOOSE_TABLE_TITLE, false);//toan
                table.ShowDialog();
                if (CurrentOrderId == 0)
                {
                    var tableViewModel = table.DataContext as TableViewModel;
                    if (tableViewModel != null && tableViewModel.table != null)
                    {
                        if (tableViewModel.table.TableStatus == (int)TableStatusEnum.EMPTY)
                        {
                            CurrentTableId = tableViewModel.table.Id;
                            TableContent = tableViewModel.table.Name;
                            TableStatus = tableViewModel.table.TableStatus;
                        }
                        else
                        {
                            NotificationMessage.Warning(MessageValue.MESSAGE_TABLE_NOT_EMPTY);
                        }
                    }
                }
                else
                {
                    if (CurrentTableId > 0)
                    {
                        var tableViewModel = table.DataContext as TableViewModel;
                        if (tableViewModel != null && tableViewModel.table != null)
                        {
                            if (tableViewModel.table.TableStatus == (int)TableStatusEnum.EMPTY)
                            {
                                TablesClient tablesClient = new TablesClient(this, this, this);
                                BaseResponse listFood = tablesClient.MoveTable(CurrentTableId, tableViewModel.table.Id);
                                if (listFood != null && listFood.Status == (int)ResponseEnum.OK)
                                {
                                    CurrentTableId = tableViewModel.table.Id;
                                    TableContent = tableViewModel.table.Name;
                                    TableStatus = tableViewModel.table.TableStatus;
                                }
                            }
                            else
                            {
                                NotificationMessage.Warning(MessageValue.MESSAGE_TABLE_NOT_EMPTY);
                            }
                        }
                    }
                    else
                    {
                        var tableViewModel = table.DataContext as TableViewModel;
                        if (tableViewModel != null && tableViewModel.table != null)
                        {
                            if (tableViewModel.table.TableStatus == (int)TableStatusEnum.EMPTY)
                            {
                                TablesClient tablesClient = new TablesClient(this, this, this);
                                BaseResponse baseResponse = tablesClient.SetTableByOrder(CurrentOrderId, tableViewModel.table.Id);
                                if (baseResponse.Status == (int)ResponseEnum.OK)
                                {
                                    CurrentTableId = tableViewModel.table.Id;
                                    TableContent = tableViewModel.table.Name;
                                    TableStatus = tableViewModel.table.TableStatus;
                                }
                            }
                            else
                            {
                                NotificationMessage.Warning(MessageValue.MESSAGE_TABLE_NOT_EMPTY);
                            }
                        }
                    }
                }
            }


        public int TableStatus = 0;
        public void GetOrderByTableId(long tableId)
        {

            if (FoodOrderList.Count > 0)
            {
                FoodOrderList.Clear();
            }
            else
            {
                FoodOrderList = new ObservableCollection<BillResponse>();
            }

            OrdersClient ordersClient = new OrdersClient(this, this, this);
            OrderDetailsClient orderDetailsClient = new OrderDetailsClient(this, this, this);
            OrderItemResponse response = ordersClient.GetOrderByTableId(tableId, currentUser.BranchId, Constants.NOT_STATUS);
            if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
            {
                CurrentOrderId = response.Data.Id;
                CurrentTableId = response.Data.TableId;
                UpdateResponse(response.Data);
            }
        }
        public void SetCustomerSlotByOrder()
        {
            CreateSlotCustomerTableWindow window = new CreateSlotCustomerTableWindow();
            window.DataContext = new CreateSlotCustomerTableViewModel(SlotCustomerContent);
            window.ShowDialog();
            if (CurrentOrderId == 0)
            {
                var createSlotCustomer = window.DataContext as CreateSlotCustomerTableViewModel;
                if (window.DataContext == null) return;
                SlotCustomerContent = createSlotCustomer.Slot;
            }
            else
            {
                var createSlotCustomer = window.DataContext as CreateSlotCustomerTableViewModel;
                OrdersClient ordersClient = new OrdersClient(this, this, this);
                BaseResponse response = ordersClient.UpdateCustomerSlotNumber(CurrentOrderId, createSlotCustomer.Slot);
                if (response.Status == (int)ResponseEnum.OK)
                {
                    SlotCustomerContent = createSlotCustomer.Slot;
                }
            }
        }
        public void ApplyDiscount()
        {
            if (CurrentOrderId == 0)
            {
                CreateDiscountWindow window = new CreateDiscountWindow();
                window.DataContext = new CreateDiscountViewModel();
                window.ShowDialog();
                var discountViewModel = window.DataContext as CreateDiscountViewModel;
                if (discountViewModel.Wrapper != null && discountViewModel.Wrapper.DiscountPercent > 0)
                {
                    OrdersClient client = new OrdersClient(this, this, this);
                    CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                    OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
                    if (openTableById != null && openTableById.Status == (int)ResponseEnum.OK)
                    {
                        CreateOrderResponse response = client.CreateOrder(wrapper);
                        if (response != null && response.Data != null)
                        {
                            AddExtraChargeVisibility = Visibility.Visible;
                            CurrentOrderId = response.Data.OrderId;
                            BaseResponse baseResponse = client.ApplyDiscount(CurrentOrderId, discountViewModel.Wrapper);
                            if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                            {
                                DiscountCount = discountViewModel.Wrapper;
                                GetTotalAmount();
                            }
                        }
                    }
                }
            }
            else
            {
                CreateDiscountWindow window = new CreateDiscountWindow();
                window.DataContext = new CreateDiscountViewModel();
                window.ShowDialog();
                var discountViewModel = window.DataContext as CreateDiscountViewModel;
                if (discountViewModel.Wrapper != null && discountViewModel.Wrapper.DiscountPercent > 0)
                {
                    OrdersClient ordersClient = new OrdersClient(this, this, this);
                    BaseResponse baseResponse = ordersClient.ApplyDiscount(CurrentOrderId, discountViewModel.Wrapper);
                    if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                    {
                        DiscountCount = discountViewModel.Wrapper;
                        GetTotalAmount();
                    }
                }
            }
        }
        public void ApplyVat()
        {
            if (IsCheckVat)
            {
                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                string contentConfirm = MessageValue.MESSAGE_CONFIRM_VAT;
                string Title = MessageValue.MESSAGE_CONFIRM_VAT_TITLE;
                string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                confirmDeleteWindow.ShowDialog();
                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                if (confirm.isConfirm)
                {
                    if (CurrentOrderId == 0)
                    {
                        OrdersClient client = new OrdersClient(this, this, this);
                        CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                        OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
                        if (openTableById != null && openTableById.Status == (int)ResponseEnum.OK)
                        {
                            CreateOrderResponse response = client.CreateOrder(wrapper);
                            if (response != null && response.Data != null)
                            {
                                CurrentOrderId = response.Data.OrderId;
                                AddExtraChargeVisibility = Visibility.Visible;
                                BaseResponse baseResponse = client.ApplyVAT(CurrentOrderId, 1);
                                if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                {
                                    //   IsApplyVAT = true;
                                    // GetTotalAmount();
                                    GetDetail(CurrentOrderId);
                                    IsCheckVat = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        DialogHostOpen = true;
                        System.Threading.Thread.Sleep(1000);
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = ordersClient.ApplyVAT(CurrentOrderId, 1);
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            //IsApplyVAT = true;
                            // GetTotalAmount();
                            GetDetail(CurrentOrderId);
                            IsCheckVat = true;
                        }
                        DialogHostOpen = false;
                    }
                }
                else
                {
                    IsCheckVat = !IsCheckVat;
                }
            }
            else
            {
                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                string contentConfirm = MessageValue.MESSAGE_CONFIRM_NOT_VAT;
                string Title = MessageValue.MESSAGE_CONFIRM_NOT_VAT_TITLE;
                string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                confirmDeleteWindow.ShowDialog();
                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                if (confirm.isConfirm)
                {
                    if (CurrentOrderId == 0)
                    {
                        OrdersClient client = new OrdersClient(this, this, this);
                        CreateOrderWrapper wrapper = new CreateOrderWrapper("", CurrentTableId, 0, slotCustomerContent, (int)OrderMethodEnum.EAT_IN);
                        OpenTableByIdResponse openTableById = client.OpenTableById(wrapper.TableId);
                        if(openTableById != null && openTableById.Status == (int)ResponseEnum.OK){
                            CreateOrderResponse response = client.CreateOrder(wrapper);
                            if (response != null && response.Data != null)
                            {
                                AddExtraChargeVisibility = Visibility.Visible;
                                CurrentOrderId = response.Data.OrderId;
                                BaseResponse baseResponse = client.ApplyVAT(CurrentOrderId, 0);
                                if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                                {
                                    // GetTotalAmount();
                                    GetDetail(CurrentOrderId);
                                    IsCheckVat = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        DialogHostOpen = true;
                        System.Threading.Thread.Sleep(1000);
                        OrdersClient ordersClient = new OrdersClient(this, this, this);
                        BaseResponse baseResponse = ordersClient.ApplyVAT(CurrentOrderId, 0);
                        if (baseResponse != null && baseResponse.Status == (int)ResponseEnum.OK)
                        {
                            //GetTotalAmount();
                            GetDetail(CurrentOrderId);
                            IsCheckVat = false;
                        }
                        DialogHostOpen = false;
                    }
                }
                else
                {
                    IsCheckVat = !IsCheckVat;
                }
            }
        }

        public bool checkPromotion() // Đạt
        {
            BillResponse billPromotion = FoodOrderList.Where(x => x.OrderDetailPromotion.Count > 0).FirstOrDefault();
            if (billPromotion != null)
            {
                if (billPromotion.OrderDetailPromotion.Sum(x => x.Quantity) >= 0)
                {
                    if (billPromotion.OrderDetailPromotion.Sum(x => x.Quantity) > 0)
                    {
                        if (billPromotion.Quantity < billPromotion.OrderDetailPromotion.Sum(x => x.Quantity))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        public CreateOrderViewModel()
        {
            if (Constants.IS_FIRST_FOOD)
            {

                GetAllFoodLisst();
                Constants.IS_FIRST_FOOD = false;
            }
            else
            {

                AllFood = (List<FoodMenuItem>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_FOOD);
                GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                if (ItemsOrderDetail.Count == 0)
                {
                    GetAllFoodLisst();
                }
            }
            if (Constants.IS_FIRST_CATEGORY)
            {
                GetAllCategoryList();
            }
            else
            {
                AllCategory = (List<Category>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_CATEGORY);
                GetCategoryMenu((int)CategoryTypeEnum.FOOD);
            }
            if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
            {
                fontsize = 17;
                width = 160;

                widthSendBar = 140;
                widthButton = 140;
                heightButton = 45;
                fontButton = 13;
                sizeHeightImage = 25;
                sizeWidthImage = 25;
                fontsizeTitle = 18;
                sizeButtonImage = 160;
                fontImage = 13;
                heightImage = 124;
                widthfoodFrame = "1.45*";

                sizeHeightFood = 195;
                sizeWidthFood = 178;
                heightFuntion = 200;

                // Thoong Tin Khac Hang
                FontSizeTitleMain = 16;
                fontsizeLable = 17;
                heightLableTb = 35;
                heighTbAddress = 80;

            }
            else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366)
            {
                fontsize = 16;
                width = 140;

                widthSendBar = 90;
                widthButton = 90;
                heightButton = 35;
                fontButton = 9;
                sizeHeightImage = 20;
                sizeWidthImage = 20;
                fontsizeTitle = 14;
                sizeButtonImage = 120;
                fontImage = 10;
                heightImage = 114;
                widthfoodFrame = "1.1*";

                sizeHeightFood = 165;
                sizeWidthFood = 140;
                heightFuntion = 165;

                // Thông tin khách hàng
                FontSizeTitleMain = 14;
                fontsizeLable = 15;
                heightLableTb = 25;
                heighTbAddress = 50;


            }

            else
            {
                fontsize = 12;
                width = 80;

                widthSendBar = 80;
                widthButton = 70;
                heightButton = 32;
                fontButton = 8;
                sizeHeightImage = 15;
                sizeWidthImage = 15;
                fontsizeTitle = 10;
                sizeButtonImage = 75;
                fontImage = 11;
                buttonHeightImage = 70;
                heightImage = 70;
                widthfoodFrame = "1.2*";

                sizeHeightFood = 170;
                sizeWidthFood = 130;
                heightFuntion = 140;

                // tHONG tIN kHACHS hANG

                FontSizeTitleMain = 12;
                fontsizeLable = 13;
                heightLableTb = 25;
                heighTbAddress = 30;

            }
            BringOutForeground = new SolidColorBrush(Colors.Black);
            IsRightOrder = false;
            DeviceClient deviceClient = new DeviceClient();
            SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
            LoadedWindowCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                // _key = true;
                CurrentTableId = 0;
                TableContent = MessageValue.MESSAGE_NOT_TABLE_DEFAULT;
                if (FoodOrderList == null)
                {
                    FoodOrderList = new ObservableCollection<BillResponse>();
                }
                else
                {
                    FoodOrderList.Clear();
                }
                CurrentOrderId = 0;
                TotalAmount = "0";
                VatAmount = "0";
                DiscountAmount = "0";
                DepositAmount = "0";
                Discount = "0%";
                Vat = "0%";
                SendCook = false;
                SendCookEnabled = false;
                AddExtraChargeVisibility = Visibility.Visible;
                VatAmountVisibility = Visibility.Visible;
                VatVisibility = Visibility.Visible;
                TableContent = MessageValue.MESSAGE_NOT_TABLE_DEFAULT;
                var bc = new System.Windows.Media.BrushConverter();
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush(Colors.White);
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));

                DiscountVisibility = Visibility.Collapsed;
                AddFoodOtherVisibility = Visibility.Collapsed;
                AddExtraChargeVisibility = Visibility.Collapsed;

                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_FOOD), currentUser.Permissions)
                || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_ORDER), currentUser.Permissions)
                || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                {
                    DiscountVisibility = Visibility.Visible;
                }
                if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADD_CUSTOM_FOOD), currentUser.Permissions)
                || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions)
                || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADD_EXTRA_CHARGE_TO_ORDER), currentUser.Permissions))
                {
                    AddFoodOtherVisibility = Visibility.Visible;
                }
                if(Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADD_EXTRA_CHARGE_TO_ORDER), currentUser.Permissions))
                {
                    AddExtraChargeVisibility = Visibility.Visible;
                }
                CategoryBbqVisibility = Visibility.Visible;
                CategoryFoodVisibility = Visibility.Visible;
                CategoryDrinkVisibility = Visibility.Visible;
                CategoryOtherVisibility = Visibility.Visible;
                CategorySeaFoodVisibility = Visibility.Visible;

                if (currentSetting.IsHideCategoryTypeFood)
                {
                    CategoryBbqVisibility = Visibility.Collapsed;
                    CategoryFoodVisibility = Visibility.Collapsed;
                }
                if (currentSetting.IsHideCategoryTypeOther)
                {
                    CategoryOtherVisibility = Visibility.Collapsed;
                }
                if (currentSetting.IsHideCategoryTypeDrink)
                {
                    CategoryDrinkVisibility = Visibility.Collapsed;
                }
                //if (currentSetting.IsHideCategoryTypeSeaFood)
                //{
                //    CategorySeaFoodVisibility = Visibility.Collapsed;
                //}
            });
            BtnCategories = new RelayCommand<Category>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetFoodMenu(CategoryType, t.Id, false);
                DialogHostOpen = false;
            });
            CategoryFoodCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                DialogHostOpen = false;
                var bc = new System.Windows.Media.BrushConverter();
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush(Colors.White);
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategoryDrinkCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.DRINK);
                GetFoodMenu((int)CategoryTypeEnum.DRINK, Constants.ALL, true);
                DialogHostOpen = false;
                var bc = new System.Windows.Media.BrushConverter();

                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush(Colors.White);
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink-active.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategoryOtherCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.OTHER);
                GetFoodMenu((int)CategoryTypeEnum.OTHER, Constants.ALL, true);
                DialogHostOpen = false;
                var bc = new System.Windows.Media.BrushConverter();
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush(Colors.White);
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other-active.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategorySeaFoodCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.SEA_FOOD);
                GetFoodMenu((int)CategoryTypeEnum.SEA_FOOD, Constants.ALL, true);
                DialogHostOpen = false;
                var bc = new System.Windows.Media.BrushConverter();
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));
                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush(Colors.White);
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster-white.png", UriKind.RelativeOrAbsolute));
            });
            FilterFoodCommand = new RelayCommand<TextBox>((t) => { return true; }, t =>
            {
                FilterFood(t.Text);
            });
            IsCheckCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == p.OrderFoodId).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(food);
                    if (p.IsSelectedAdditionFood == 1)
                    {
                        //Dat
                        p.Quantity = 1;
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * p.Quantity;
                        food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = p.Quantity;
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * p.Quantity;
                    }
                    if (p.IsSelectedAdditionFood == 0)
                    {
                        //Dat
                        food.AmountAddition = food.AmountAddition - (p.UnitPrice * p.Quantity);
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = 0;
                        p.Quantity = 0;
                        //food.AmountAddition = food.AmountAddition - (p.UnitPrice * p.Quantity);
                    }
                    FoodOrderList.Remove(food);
                    food.TotalPrice = food.TotalAmount;
                    FoodOrderList.Insert(index, food);
                    GetTotalAmount();
                }
            });
            ReturnBeer = new RelayCommand<BillResponse>((t) => { return true; }, t => {
                if ((t.CategoryType == (int)CategoryTypeEnum.DRINK && t.IsPrint == true) || (t.CategoryType == (int)CategoryTypeEnum.OTHER && t.IsPrint == true))
                {
                    OrdersClient ordersClient = new OrdersClient(this, this, this);
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    var prefix = AllFood.Where(y => y.FoodId == t.FoodId).Select(x => x.Prefix).FirstOrDefault();
                    ReturnBeerWindow returnBeerWindow = new ReturnBeerWindow();
                    returnBeerWindow.DataContext = new ReturnBeerViewModel(CurrentOrderId, t.Id, prefix, t.Quantity, currentUser.BranchId);
                    returnBeerWindow.ShowDialog();
                    var data = returnBeerWindow.DataContext as ReturnBeerViewModel;
                    if (data.isCreate == true)
                    {
                        OrderDetailPrintResponse orderDetailPrintResponse = ordersClient.GetOrderDetailPrint(CurrentOrderId);
                        if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                        {
                            foreach(Kitchen k in CurrentKitchens)
                            {
                                List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id 
                                && x.Quantity < x.PrintedQuantity
                                && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL
                                && x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK))).ToList(); // Thay đổi giảm 
                                if (resultUpdate != null && resultUpdate.Count > 0)
                                {
                                    foreach (OrderDetailPrint od in resultUpdate)
                                    {
                                        od.Note = data.Note;//toan
                                        pt.PrintFoodMove(od, k.PrinterName, od.EmployeeName, od.TableName, (int)k.PrinterPaperSize);
                                    }
                                }
                            }
                        }
                        GetDetail(CurrentOrderId);
                    }
                }
            });

            WrittionChangeCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == p.OrderFoodId).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(food);
                    if (p.IsSelectedAdditionFood == 1)
                    {
                        food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = p.Quantity;
                    }
                    if (food.Quantity != food.OldQuantity)
                        food.IsPrint = false;
                    else
                        food.IsPrint = true;
                    FoodOrderList.Remove(food);
                    food.TotalPrice = food.TotalAmount;
                    FoodOrderList.Insert(index, food);
                    GetTotalAmount();
                }
            });
            AddFood = new RelayCommand<FoodMenuItem>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    IsRightOrder = true;
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == (t.IsGift ? 1 : 0)).FirstOrDefault();
                    if (food != null && food.Note == "" && (food.OrderAdditions != null || !food.IsPrint) && (food.FoodInCombo != null || !food.IsPrint) && (food.IsSellByWeight == 0 || !food.IsPrint) && (food.FoodInPrmotion != null || !food.IsPrint))
                    {
                        food.Quantity++;
                        food.IsAlowPrint = t.IsAlowPrint;
                        if (food.Quantity != food.OldQuantity)
                            food.IsPrint = false;
                        else
                            food.IsPrint = true;
                        foreach (BillResponse f in t.AdditionFood)
                        {
                            f.AmountAddition = f.Quantity * f.Price;
                            f.Price = f.Price;
                            f.OrderFoodId = food.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                        }
                        food.TotalPrice = food.TotalAmount;
                        food.OrderDetailAdditions = t.AdditionFood != null ? t.AdditionFood : new List<BillResponse>();
                        food.OrderAdditions = food.OrderDetailAdditions;
                        FoodOrderList.Remove(food);
                        FoodOrderList.Insert(0, food);
                    }
                    else
                    {
                        BillResponse orderDetail = new BillResponse();
                        orderDetail.FoodId = t.FoodId;
                        orderDetail.FoodName = t.ContentFoodName;
                        orderDetail.Name = t.ContentFoodName;
                        orderDetail.IsAllowEmployeeGift = t.IsAllowEmployeeGift;
                        orderDetail.Prefix = t.Prefix != null ? t.Prefix : "";
                        orderDetail.NormalizeName = t.NormalizeName != null ? t.Prefix : "";
                        orderDetail.IsGift = t.IsGift ? 1 : 0;
                        if (t.IsSellByWeight == 1)
                        {
                            orderDetail.IsSellByWeight = t.IsSellByWeight;
                            orderDetail.Quantity = 1.0M;
                        }
                        else
                        {
                            orderDetail.IsSellByWeight = 0;
                            orderDetail.Quantity = 1;
                        }
                        //orderDetail.Quantity = 1;
                        orderDetail.CategoryType = t.CategoryTypeId;
                        orderDetail.IsSelectedAdditionFood = 1;
                        orderDetail.Note = "";
                        orderDetail.UnitType = t.UnitType;
                        orderDetail.FoodUnit = t.UnitType;
                        orderDetail.Price = t.Price;
                        orderDetail.UnitPrice = t.Price;
                        orderDetail.IsAlowPrint = t.IsAlowPrint;
                        orderDetail.TotalPrice = t.Price;
                        foreach (BillResponse f in t.AdditionFood)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 0;
                            f.FoodId = f.Id;
                            f.FoodUnit = t.UnitType;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderDetail.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                            f.Note = "";
                        }
                        foreach (BillResponse f in t.FoodInPromotionBuyOneGetOne)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 0;
                            f.FoodId = f.Id;
                            f.FoodUnit = t.UnitType;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderDetail.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                        }
                        orderDetail.OrderDetailAdditions = t.AdditionFood != null ? t.AdditionFood : new List<BillResponse>();
                        orderDetail.OrderAdditions = orderDetail.OrderDetailAdditions;
                        orderDetail.OrderDetailCombo = t.FoodInCombo != null ? t.FoodInCombo : new List<BillResponse>();
                        orderDetail.FoodInCombo = orderDetail.OrderDetailCombo;
                        orderDetail.OrderDetailPromotion = t.FoodInPromotionBuyOneGetOne != null ? t.FoodInPromotionBuyOneGetOne : new List<BillResponse>();
                        orderDetail.FoodInPrmotion = orderDetail.OrderDetailPromotion;
                        FoodOrderList.Insert(0, orderDetail);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }

            });
            AddQuantityCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == t.IsGift).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity != 999)
                    {
                        orderFood.Quantity = orderFood.Quantity + 1;
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                    //thuc
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
            });
            SubQuantityCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == t.IsGift).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity > 1)
                    {
                        orderFood.Quantity = orderFood.Quantity - 1;
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                    //thuc
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
            });
            ReturnFood = new RelayCommand<FoodMenuItem>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == (t.IsGift ? 1 : 0)).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity > 1)
                    {
                        orderFood.Quantity = orderFood.Quantity - 1;
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                    //thuc
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }

            });
            NoteFood = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x == t).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(orderFood);
                    NoteFoodWindow noteFoodWindow = new NoteFoodWindow();
                    noteFoodWindow.DataContext = new NoteFoodViewModel(t, currentUser.RestaurantBrandId, currentUser.BranchId);
                    noteFoodWindow.ShowDialog();
                    var note = noteFoodWindow.DataContext as NoteFoodViewModel;
                    if (note.isCheck)
                    {
                        orderFood.Note = note.OrderFoodNote.Note;
                        FoodOrderList.RemoveAt(index);
                        FoodOrderList.Insert(index, orderFood);
                    }
                }

            });
            CancelFood = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                CancelFoodItem(t);
                //thuc
                MainSecondViewModel.ShowOrderFood(FoodOrderList);
            });
            TextChangeAmount = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x == t).FirstOrDefault();
                    if (orderFood != null)
                    {
                        orderFood.Quantity = t.Quantity;
                        orderFood.OnlyViewQuantity = t.Quantity;
                        if (orderFood.Quantity == 0)
                            orderFood.Quantity = 1;
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        int index = FoodOrderList.IndexOf(orderFood);
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    //thuc
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
            });
            AddFoodOther = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if(CurrentOrderId !=0 )
                {
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    CreateFoodOtherWindow createFoodOtherWindow = new CreateFoodOtherWindow();
                    createFoodOtherWindow.DataContext = new CreateFoodOtherViewModel();
                    createFoodOtherWindow.ShowDialog();
                    var createFoodOther = createFoodOtherWindow.DataContext as CreateFoodOtherViewModel;

                    if (createFoodOtherWindow.DataContext == null) return;
                    if (createFoodOther.isCreate)
                    {
                        if (createFoodOther.FoodOther != null)
                        {
                            BillResponse dataFood = createFoodOther.FoodOther;
                            CreateSpecialFoodWrapper foodWrapper = new CreateSpecialFoodWrapper(dataFood.FoodName, dataFood.IsAlowPrint, dataFood.Note, dataFood.Price, dataFood.Quantity, dataFood.RestaurantKitchenPlaceId, 0);
                            OrdersClient client = new OrdersClient(this, this, this);
                            CreateSpecialFoodResponse response = client.CreateSpecialFood(CurrentOrderId, currentUser.BranchId, foodWrapper);
                            if (response != null && response.Data != null)
                            {
                                FoodOrderList.Add(createFoodOther.FoodOther);
                                OrderDetailPrintResponse orderDetailPrintResponse = client.GetOrderDetailPrint(CurrentOrderId);
                                if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                                {
                                    List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                                    foreach (Kitchen k in CurrentKitchens)
                                    {
                                        List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                        if (result != null && result.Count > 0)
                                        {
                                            //  pt.PrintFoodCook(k.PrinterName, result, addFood.Data.TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                                            //pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                            //string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)k.PrinterPaperSize);
                                            pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                                string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                                        }
                                    }
                                    IsPrintSendFoodCookWrapper wrapperIsPrint = new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList());
                                    Task.Run(() => client.UpdateOrderIsPrintSendCook(wrapperIsPrint, CurrentOrderId));
                                }
                            }
                        }
                        GetTotalAmount();
                        SendCook = true;
                        SendCookEnabled = true;
                        GetDetail(CurrentOrderId);
                    }
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_FUNCTION);
                }

            });
            AddTableCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (SetTablePermission)
                {
                    TableForeground = new SolidColorBrush(Colors.White);
                    BringOutForeground = new SolidColorBrush(Colors.Black);
                    TableBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BringOutBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_COLOR));
                    SetTableByOrder();
                    if (TableStatus == 2)
                        GetOrderByTableId(CurrentTableId);
                }
            });
            AddSlotCustomer = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                SetCustomerSlotByOrder();
            });
            GiftFoodCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    int index = FoodOrderList.IndexOf(t);
                    t.IsGift = t.IsGift == 1 ? 0 : 1;
                    FoodOrderList.Remove(t);
                    FoodOrderList.Insert(index, t);
                    GetTotalAmount();
                    if (t.IsGift == 1)
                    {
                        MainSecondViewModel.GiftAmount = (decimal)t.Amount;
                    }
                    else
                    {
                        MainSecondViewModel.GiftAmount = -(decimal)t.Amount;
                    }
                    //thuc
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
            });
            CancelOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                CloseOrder(t);
            });
            SendCookOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                isCheckSave = false; // Dat
                if (SendCook)
                {
                    if (FoodOrderList != null && FoodOrderList.Count > 0)
                    {
                        if (checkPromotion() == true)
                        {
                            SendCookOrder();
                            AllFood = (List<FoodMenuItem>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_FOOD);
                            GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                            if (ItemsOrderDetail.Count == 0)
                            {
                                GetAllFoodLisst();
                            }
                            GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                            GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                        }
                        else
                        {
                            NotificationMessage.Error("Món tặng không được lớn hơn món chính !");
                        }
                        //SendCookOrder();
                    }
                }
            });
            SaveOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    SaveOrder(t);
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !");
                }
                //SaveOrder(t);
            });
            SaveAndAddOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    SaveAndAddOrder();
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !");
                }
                //SaveAndAddOrder();
                if(IsCheck == true)
                {
                    FrameworkElement window = GetWindowParent(t);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                    _MainContentControl.Content = new HomeUserControl();

                    CreateOrderUserControl create = new CreateOrderUserControl();
                    create.DataContext = new CreateOrderViewModel();
                    _MainContentControl.Content = create;
                }
                else
                {
                    return;
                }

            });
            PaymentOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    PayMent(t);
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !");
                }
            });
            DiscountCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (CurrentOrderId != 0)
                {
                    ApplyDiscount();
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_FUNCTION);
                }

            });
            VATCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if(CurrentOrderId != 0)
                {
                    ApplyVat();
                    MainSecondViewModel.ShowOrderFood(FoodOrderList);
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_FUNCTION);
                }
            });

            AddExtraChargeCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (CurrentOrderId != 0)
                {
                    CreateRestaurantExtraWindow window = new CreateRestaurantExtraWindow();
                    window.DataContext = new CreateRestaurantExtraViewModel(CurrentOrderId);
                    window.ShowDialog();
                    var extra = window.DataContext as CreateRestaurantExtraViewModel;
                    if (extra.isCreate)
                    {
                        extra.ExtraChange.IsPrint = true;
                        FoodOrderList.Add(extra.ExtraChange);
                        GetTotalAmount();
                        GetDetail(CurrentOrderId);
                    }
                }
                else
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_ERROR_FUNCTION);
                }
            });
        }
        public void GetDetail(long orderId)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
           {
               if (FoodOrderList.Count > 0)
               {
                   FoodOrderList.Clear();
               }
               else
               {
                   FoodOrderList = new ObservableCollection<BillResponse>();
               }
               OrdersClient ordersClient = new OrdersClient(this, this, this);
               OrderItemResponse response = ordersClient.GetOrderById(orderId, currentUser.BranchId, Constants.NOT_STATUS, Constants.NOT_STATUS);
               if (response != null && response.Status == (int)ResponseEnum.OK && response.Data != null)
               {
                   CurrentOrderId = response.Data.Id;

                   TableForeground = new SolidColorBrush(Colors.White);
                   BringOutForeground = new SolidColorBrush(Colors.Black);
                   TableBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                   BringOutBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_COLOR));
                   AddTaleVisibility = "0.2*";
                   TableContent = response.Data.Id == 0 ? MessageValue.MESSAGE_NOT_TABLE_DEFAULT : response.Data.TableName;

                   CurrentTableId = response.Data.TableId;
                   SlotCustomerContent = (int)response.Data.CustomerSlotNumber;
                   TotalAmount = Utils.Utils.FormatMoney(response.Data.TotalAmount);
                   DiscountCount = new DiscountWrapper();
                   DiscountCount.DiscountType = (int)response.Data.DiscountType;
                   DiscountCount.DiscountPercent = response.Data.DiscountPercent;
                   DiscountAmount = Utils.Utils.FormatMoney(response.Data.DiscountAmount);
                   Discount = string.Format("{0}%", response.Data.DiscountPercent);
                   IsCheckVat = response.Data.VatAmount > 0 ? true : false;
                   VatAmount = Utils.Utils.FormatMoney(response.Data.VatAmount);
                   Vat = string.Format("{0}%", response.Data.Vat);
                   DialogHostOpen = false;
                   foreach (BillResponse f in response.Data.Foods.Where(x => x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL && x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK))
                   {
                       f.Price = f.UnitPrice;
                       f.Price = f.UnitPrice;
                       f.Prefix = f.FoodPrefix != null ? f.FoodPrefix : "";
                       f.NormalizeName = f.FoodNormalizeName != null ? f.FoodNormalizeName : "";
                       f.IsPrint = true;
                       if (f.OrderDetailAdditions != null && f.OrderDetailAdditions.Count > 0 || f.OrderDetailPromotion != null && f.OrderDetailPromotion.Count > 0)
                       {
                           foreach (BillResponse orderDetail in f.OrderDetailAdditions)
                           {
                               orderDetail.IsSelectedAdditionFood = 1;
                               orderDetail.OnlyViewQuantity = orderDetail.Quantity;
                               f.AmountAddition = f.AmountAddition + orderDetail.UnitPrice * orderDetail.Quantity;
                               orderDetail.IsPrint = true;
                           }
                           foreach (BillResponse orderDetail in f.OrderDetailPromotion)
                           {
                               orderDetail.IsSelectedAdditionFood = 1;
                               orderDetail.OnlyViewQuantity = orderDetail.Quantity;
                               f.AmountAddition = f.AmountAddition + orderDetail.UnitPrice * orderDetail.Quantity;
                               orderDetail.IsPrint = true;
                           }
                       }
                       f.OldQuantity = f.Quantity;
                       f.RealPrint = f.Quantity;
                       FoodOrderList.Add(f);
                   }

                   CategoryBbqVisibility = Visibility.Visible;
                   CategoryFoodVisibility = Visibility.Visible;
                   CategoryDrinkVisibility = Visibility.Visible;
                   CategoryOtherVisibility = Visibility.Visible;
                   CategorySeaFoodVisibility = Visibility.Visible;

                   if (currentSetting.IsHideCategoryTypeFood)
                   {
                       CategoryBbqVisibility = Visibility.Collapsed;

                       CategoryFoodVisibility = Visibility.Collapsed;
                   }
                   if (currentSetting.IsHideCategoryTypeOther)
                   {
                       CategoryOtherVisibility = Visibility.Collapsed;
                   }
                   if (currentSetting.IsHideCategoryTypeDrink)
                   {
                       CategoryDrinkVisibility = Visibility.Collapsed;
                   }
                   //if (currentSetting.IsHideCategoryTypeSeaFood)
                   //{
                   //    CategorySeaFoodVisibility = Visibility.Collapsed;
                   //}
               }
           });
            //MainSecondViewModel.ShowOrderFood1(FoodOrderList);
        }
        public CreateOrderViewModel(long orderId)
        {
            if (Constants.IS_FIRST_FOOD)
            {

                GetAllFoodLisst();
                Constants.IS_FIRST_FOOD = false;
            }
            else
            {

                AllFood = (List<FoodMenuItem>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_FOOD);
                GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                if (ItemsOrderDetail.Count == 0)
                {
                    GetAllFoodLisst();
                }
            }
            if (Constants.IS_FIRST_CATEGORY)
            {
                GetAllCategoryList();
            }
            else
            {
                AllCategory = (List<Category>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_CATEGORY);
                GetCategoryMenu((int)CategoryTypeEnum.FOOD);
            }
            IsRightOrder = true;
            LoadedWindowCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (SystemParameters.MaximizedPrimaryScreenWidth >= 1920)
                {
                    fontsize = 17;
                    width = 160;

                    widthSendBar = 140;
                    widthButton = 140;
                    heightButton = 45;
                    fontButton = 13;
                    sizeHeightImage = 25;
                    sizeWidthImage = 25;
                    fontsizeTitle = 18;
                    sizeButtonImage = 160;
                    fontImage = 13;
                    heightImage = 124;
                    widthfoodFrame = "1.45*";

                    sizeHeightFood = 195;
                    sizeWidthFood = 178;
                    heightFuntion = 200;

                    // Thoong Tin Khac Hang
                    FontSizeTitleMain = 16;
                    fontsizeLable = 17;
                    heightLableTb = 35;
                    heighTbAddress = 80;

                }
                else if (SystemParameters.MaximizedPrimaryScreenWidth >= 1366)
                {
                    fontsize = 16;
                    width = 140;

                    widthSendBar = 90;
                    widthButton = 90;
                    heightButton = 35;
                    fontButton = 9;
                    sizeHeightImage = 20;
                    sizeWidthImage = 20;
                    fontsizeTitle = 14;
                    sizeButtonImage = 120;
                    fontImage = 10;
                    heightImage = 114;
                    widthfoodFrame = "1.1*";

                    sizeHeightFood = 165;
                    sizeWidthFood = 140;
                    heightFuntion = 165;

                    // Thông tin khách hàng
                    FontSizeTitleMain = 14;
                    fontsizeLable = 15;
                    heightLableTb = 25;
                    heighTbAddress = 50;


                }

                else
                {
                    fontsize = 12;
                    width = 80;

                    widthSendBar = 80;
                    widthButton = 70;
                    heightButton = 32;
                    fontButton = 8;
                    sizeHeightImage = 15;
                    sizeWidthImage = 15;
                    fontsizeTitle = 10;
                    sizeButtonImage = 75;
                    fontImage = 11;
                    buttonHeightImage = 70;
                    heightImage = 70;
                    widthfoodFrame = "1.2*";

                    sizeHeightFood = 170;
                    sizeWidthFood = 130;
                    heightFuntion = 150;

                    // tHONG tIN kHACHS hANG

                    FontSizeTitleMain = 12;
                    fontsizeLable = 13;
                    heightLableTb = 25;
                    heighTbAddress = 30;

                }
                Application.Current.Dispatcher.Invoke((Action)async delegate
                {
                    DialogHostOpen = true;
                    await Task.Run(() =>
                    {
                        GetDetail(orderId);
                    });
                    GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                    GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                    CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                    CategoryFoodForeground = new SolidColorBrush(Colors.White);
                    FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-food.png", UriKind.RelativeOrAbsolute));

                    CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                    CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                    DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                    CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                    CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                    OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                    CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                    CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                    SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));

                    DiscountVisibility = Visibility.Collapsed;
                    AddFoodOtherVisibility = Visibility.Collapsed;
                    AddExtraChargeVisibility = Visibility.Collapsed;
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_FOOD), currentUser.Permissions)
                        || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_ORDER), currentUser.Permissions)
                        || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    {
                        DiscountVisibility = Visibility.Visible;
                    }
                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADD_CUSTOM_FOOD), currentUser.Permissions)
                    || Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.OWNER), currentUser.Permissions))
                    {
                        AddFoodOtherVisibility = Visibility.Visible;
                    }

                    if(Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.ADD_EXTRA_CHARGE_TO_ORDER), currentUser.Permissions))
                    {
                        AddExtraChargeVisibility = Visibility.Visible;
                    }

                    VatAmountVisibility = Visibility.Visible;
                    VatVisibility = Visibility.Visible;
                    DeviceClient deviceClient = new DeviceClient();
                    SettingLayoutWrapper setting = deviceClient.ReadCurrentSettingLayout();
                    MainSecondViewModel.ShowCreateOrderSecond(FoodOrderList);
                });
            });
            BtnCategories = new RelayCommand<Category>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetFoodMenu(CategoryType, t.Id, false);
                DialogHostOpen = false;
            });
            CategoryFoodCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                DialogHostOpen = false;
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush(Colors.White);
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategoryDrinkCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.DRINK);
                GetFoodMenu((int)CategoryTypeEnum.DRINK, Constants.ALL, true);
                DialogHostOpen = false;

                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush(Colors.White);
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink-active.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategoryOtherCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.OTHER);
                GetFoodMenu((int)CategoryTypeEnum.OTHER, Constants.ALL, true);
                DialogHostOpen = false;
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));

                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush(Colors.White);
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other-active.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster.png", UriKind.RelativeOrAbsolute));
            });
            CategorySeaFoodCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                DialogHostOpen = true;
                GetCategoryMenu((int)CategoryTypeEnum.SEA_FOOD);
                GetFoodMenu((int)CategoryTypeEnum.SEA_FOOD, Constants.ALL, true);
                DialogHostOpen = false;
                CategoryFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryFoodForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                FoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-food.png", UriKind.RelativeOrAbsolute));

                CategoryDrinkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryDrinkForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                DrinkIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-drink.png", UriKind.RelativeOrAbsolute));
                CategoryOtherBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_LIGTH_BACKGROUND));
                CategoryOtherForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_DARK_BACKGROUND));
                OtherIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/icon-other.png", UriKind.RelativeOrAbsolute));

                CategorySeaFoodBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.BLUE_BACKGROUND));
                CategorySeaFoodForeground = new SolidColorBrush(Colors.White);
                SeaFoodIcon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/ic-lobster-white.png", UriKind.RelativeOrAbsolute));
            });
            FilterFoodCommand = new RelayCommand<TextBox>((t) => { return true; }, t =>
            {
                FilterFood(t.Text);
            });
            IsCheckCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == p.OrderFoodId).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(food);
                    FoodOrderList.Remove(food);
                    if (p.IsSelectedAdditionFood == 1)
                    {
                        //Dat
                        p.Quantity = 1;
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * p.Quantity;
                        food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = p.Quantity;
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * p.Quantity;
                    }
                    if (p.IsSelectedAdditionFood == 0)
                    {
                        //Dat
                        food.AmountAddition = food.AmountAddition - (p.UnitPrice * p.Quantity);
                        //food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = 0;
                        p.Quantity = 0;
                        //food.AmountAddition = food.AmountAddition - (p.UnitPrice * p.Quantity);
                    }
                    food.TotalPrice = food.TotalAmount;
                    FoodOrderList.Insert(index, food);
                    GetTotalAmount();
                }
            });
            ReturnBeer = new RelayCommand<BillResponse>((t) => { return true; }, t => {
                if ((t.CategoryType == (int)CategoryTypeEnum.DRINK && t.IsPrint == true) || (t.CategoryType == (int)CategoryTypeEnum.OTHER && t.IsPrint == true))
                {
                    OrdersClient ordersClient = new OrdersClient(this, this, this);
                    Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                    var prefix = AllFood.Where(y => y.FoodId == t.FoodId).Select(x => x.Prefix).FirstOrDefault();
                    ReturnBeerWindow returnBeerWindow = new ReturnBeerWindow();
                    returnBeerWindow.DataContext = new ReturnBeerViewModel(CurrentOrderId, t.Id, prefix, t.Quantity, currentUser.BranchId);
                    returnBeerWindow.ShowDialog();
                    var data = returnBeerWindow.DataContext as ReturnBeerViewModel;
                    if (data.isCreate == true)
                    {
                        OrderDetailPrintResponse orderDetailPrintResponse = new OrderDetailPrintResponse();
                        orderDetailPrintResponse = ordersClient.GetOrderDetailPrint(CurrentOrderId);
                        if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                        {
                            foreach (Kitchen k in CurrentKitchens)
                            {
                                List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id
                                && x.Quantity < x.PrintedQuantity
                                && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL
                                && x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK))).ToList(); // Thay đổi giảm 
                                if (resultUpdate != null && resultUpdate.Count > 0)
                                {
                                    foreach (OrderDetailPrint od in resultUpdate)
                                    {
                                        od.Note = data.Note;//toan
                                        pt.PrintFoodMove(od, k.PrinterName, od.EmployeeName, od.TableName, (int)k.PrinterPaperSize);
                                    }
                                }
                            }
                        }
                        GetDetail(CurrentOrderId);
                    }
                }
            });
            WrittionChangeCommand = new RelayCommand<BillResponse>((p) => { return true; }, p =>
            {
                if (p != null)
                {
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == p.OrderFoodId).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(food);
                    FoodOrderList.Remove(food);
                    if (p.IsSelectedAdditionFood == 1)
                    {
                        food.AmountAddition = food.AmountAddition + p.UnitPrice * (p.Quantity - p.OnlyViewQuantity);
                        p.OnlyViewQuantity = p.Quantity;
                    }
                    if (food.Quantity != food.OldQuantity)
                        food.IsPrint = false;
                    else
                        food.IsPrint = true;
                    food.TotalPrice = food.TotalAmount;
                    FoodOrderList.Insert(index, food);
                    GetTotalAmount();
                }
            });
            AddFood = new RelayCommand<FoodMenuItem>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse food = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == (t.IsGift ? 1 : 0)).FirstOrDefault();
                    if (food != null && food.Note == "" && (food.OrderAdditions != null || !food.IsPrint) && (food.FoodInCombo != null || !food.IsPrint) && (food.IsSellByWeight == 0 || !food.IsPrint))
                    {

                        food.Quantity++;
                        food.IsAlowPrint = t.IsAlowPrint;
                        if (food.Quantity != food.OldQuantity)
                            food.IsPrint = false;
                        else
                            food.IsPrint = true;
                        foreach (BillResponse f in t.AdditionFood)
                        {
                            f.AmountAddition = f.Quantity * f.Price;
                            f.Price = f.Price;
                            f.OrderFoodId = food.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                        }
                        food.OrderDetailAdditions = t.AdditionFood != null ? t.AdditionFood : new List<BillResponse>();
                        food.TotalPrice = food.TotalAmount;
                        food.OrderAdditions = food.OrderDetailAdditions;
                        FoodOrderList.Remove(food);
                        FoodOrderList.Insert(0, food);
                        //
                        MainSecondViewModel.ShowOrderFood(FoodOrderList);
                    }
                    else
                    {
                        BillResponse orderDetail = new BillResponse();
                        orderDetail.FoodId = t.FoodId;
                        orderDetail.FoodName = t.ContentFoodName;
                        orderDetail.Name = t.ContentFoodName;
                        orderDetail.IsAllowEmployeeGift = t.IsAllowEmployeeGift;
                        orderDetail.Prefix = t.Prefix != null ? t.Prefix : "";
                        orderDetail.NormalizeName = t.NormalizeName != null ? t.Prefix : "";
                        orderDetail.IsGift = t.IsGift ? 1 : 0;
                        if (t.IsSellByWeight == 1)
                        {
                            orderDetail.IsSellByWeight = t.IsSellByWeight;
                            orderDetail.Quantity = 1.0M;
                        }
                        else
                        {
                            orderDetail.IsSellByWeight = 0;
                            orderDetail.Quantity = 1;
                        }
                        //orderDetail.Quantity = 1;
                        orderDetail.CategoryType = t.CategoryTypeId;
                        orderDetail.IsSelectedAdditionFood = 1;
                        orderDetail.Note = "";
                        orderDetail.UnitType = t.UnitType;
                        orderDetail.FoodUnit = t.UnitType;
                        orderDetail.Price = t.Price;
                        orderDetail.UnitPrice = t.Price;
                        orderDetail.TotalPrice = t.Price;
                        foreach (BillResponse f in t.AdditionFood)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 0;
                            f.FoodId = f.Id;
                            f.FoodUnit = t.UnitType;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderDetail.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                        }
                        foreach (BillResponse f in t.FoodInPromotionBuyOneGetOne)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 0;
                            f.FoodId = f.Id;
                            f.FoodUnit = t.UnitType;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderDetail.FoodId;
                            f.OnlyViewQuantity = f.Quantity;
                            f.IsPrint = false;
                        }
                        orderDetail.OrderDetailAdditions = t.AdditionFood != null ? t.AdditionFood : new List<BillResponse>();
                        orderDetail.OrderAdditions = orderDetail.OrderDetailAdditions;
                        orderDetail.OrderDetailCombo = t.FoodInCombo != null ? t.FoodInCombo : new List<BillResponse>();
                        orderDetail.FoodInCombo = orderDetail.OrderDetailCombo;
                        orderDetail.OrderDetailPromotion = t.FoodInPromotionBuyOneGetOne != null ? t.FoodInPromotionBuyOneGetOne : new List<BillResponse>();
                        orderDetail.FoodInPrmotion = orderDetail.OrderDetailPromotion;
                        FoodOrderList.Insert(0, orderDetail);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                }
            });
            AddQuantityCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == t.IsGift && x.Quantity == t.Quantity && x.Note == t.Note).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity != 999)
                    {
                        orderFood.Quantity = orderFood.Quantity + 1;
                        orderFood.IsGift = t.IsGift;
                        orderFood.Price = t.Price;
                        if (t.OrderAdditions != null)
                        {
                            foreach (BillResponse f in t.OrderAdditions)
                            {
                                f.IsSelectedAdditionFood = 0;
                                f.Quantity = 1;
                                f.UnitPrice = f.Price;
                                f.OrderFoodId = orderFood.FoodId;
                                f.IsPrint = false;
                            }
                        }

                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                }
            });
            SubQuantityCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == t.IsGift && x.Quantity == t.Quantity && x.Note == t.Note).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity > 1)
                    {
                        orderFood.Quantity = orderFood.Quantity - 1;
                        orderFood.IsGift = t.IsGift;
                        orderFood.Price = t.Price;
                        foreach (BillResponse f in t.OrderAdditions)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 1;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderFood.FoodId;
                            f.IsPrint = false;
                        }
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                }
            });
            ReturnFood = new RelayCommand<FoodMenuItem>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId && x.IsGift == (t.IsGift ? 1 : 0)).FirstOrDefault();
                    if (orderFood != null && orderFood.Quantity > 1)
                    {
                        orderFood.Quantity = orderFood.Quantity - 1;
                        if (orderFood.Quantity != orderFood.OldQuantity)
                            orderFood.IsPrint = false;
                        else
                            orderFood.IsPrint = true;
                        foreach (BillResponse f in t.AdditionFood)
                        {
                            f.IsSelectedAdditionFood = 0;
                            f.Quantity = 1;
                            f.UnitPrice = f.Price;
                            f.OrderFoodId = orderFood.FoodId;
                            f.IsPrint = false;
                        }
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                }

            });
            NoteFood = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId).FirstOrDefault();
                    int index = FoodOrderList.IndexOf(orderFood);
                    NoteFoodWindow noteFoodWindow = new NoteFoodWindow();
                    noteFoodWindow.DataContext = new NoteFoodViewModel(t, currentUser.RestaurantBrandId, currentUser.BranchId);
                    noteFoodWindow.ShowDialog();
                    var note = noteFoodWindow.DataContext as NoteFoodViewModel;
                    if (note.isCheck)
                    {
                        orderFood.Note = note.OrderFoodNote.Note;
                        FoodOrderList.RemoveAt(index);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                }

            });
            CancelFood = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                CancelFoodItem(t);
            });
            TextChangeAmount = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    BillResponse orderFood = FoodOrderList.Where(x => x.FoodId == t.FoodId).FirstOrDefault();
                    if (orderFood != null)
                    {
                        orderFood.Quantity = t.Quantity;
                        orderFood.OnlyViewQuantity = t.Quantity;
                        if (IsCheck == true)
                        {
                            if (orderFood.TotalPrice == 0)
                                orderFood.Quantity = 0;
                        }
                        else
                        {
                            if (orderFood.Quantity == 0)
                                orderFood.Quantity = 1;
                        }
                        int index = FoodOrderList.IndexOf(orderFood);
                        orderFood.TotalPrice = orderFood.TotalAmount;
                        FoodOrderList.Remove(orderFood);
                        FoodOrderList.Insert(index, orderFood);
                    }
                    GetTotalAmount();
                }
            });
            AddFoodOther = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                Helpers.PrintBill.PrintText pt = new Helpers.PrintBill.PrintText();
                CreateFoodOtherWindow createFoodOtherWindow = new CreateFoodOtherWindow();
                createFoodOtherWindow.DataContext = new CreateFoodOtherViewModel();
                createFoodOtherWindow.ShowDialog();
                var createFoodOther = createFoodOtherWindow.DataContext as CreateFoodOtherViewModel;

                if (createFoodOtherWindow.DataContext == null) return;
                if (createFoodOther.isCreate)
                {
                    if(createFoodOther.FoodOther != null)
                    {
                        BillResponse dataFood = createFoodOther.FoodOther;
                        CreateSpecialFoodWrapper foodWrapper = new CreateSpecialFoodWrapper(dataFood.FoodName, dataFood.IsAlowPrint, dataFood.Note, dataFood.Price, dataFood.Quantity, dataFood.RestaurantKitchenPlaceId, 0);
                        OrdersClient client = new OrdersClient(this, this, this);
                        CreateSpecialFoodResponse response = client.CreateSpecialFood(CurrentOrderId, currentUser.BranchId, foodWrapper);
                        if (response != null && response.Data != null)
                        {
                            FoodOrderList.Add(createFoodOther.FoodOther);
                            OrderDetailPrintResponse orderDetailPrintResponse = client.GetOrderDetailPrint(CurrentOrderId);
                            if (orderDetailPrintResponse != null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
                            {
                                List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                                foreach (Kitchen k in CurrentKitchens)
                                {
                                    List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                                    if (result != null && result.Count > 0)
                                    {
                                        //  pt.PrintFoodCook(k.PrinterName, result, addFood.Data.TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                                        //pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                        //string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)k.PrinterPaperSize);
                                        pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                                            string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                                    }
                                }
                                IsPrintSendFoodCookWrapper wrapperIsPrint = new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList());
                                Task.Run(() => client.UpdateOrderIsPrintSendCook(wrapperIsPrint, CurrentOrderId));
                            }
                        }
                    }
                    GetTotalAmount();
                    SendCook = true;
                    SendCookEnabled = true;
                    GetDetail(CurrentOrderId);
                }

            });
            AddTableCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (SetTablePermission)
                {
                    TableForeground = new SolidColorBrush(Colors.White);
                    BringOutForeground = new SolidColorBrush(Colors.Black);
                    TableBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
                    BringOutBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_COLOR));
                    SetTableByOrder();
                    if (TableStatus == 2)
                        GetOrderByTableId(CurrentTableId);
                }
            });
            AddSlotCustomer = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                CreateSlotCustomerTableWindow window = new CreateSlotCustomerTableWindow();
                window.DataContext = new CreateSlotCustomerTableViewModel(SlotCustomerContent);
                window.ShowDialog();
                var createSlotCustomer = window.DataContext as CreateSlotCustomerTableViewModel;
                OrdersClient ordersClient = new OrdersClient(this, this, this);
                BaseResponse response = ordersClient.UpdateCustomerSlotNumber(orderId, createSlotCustomer.Slot);
                if (response.Status == (int)ResponseEnum.OK)
                {
                    SlotCustomerContent = createSlotCustomer.Slot;
                }
            });
            GiftFoodCommand = new RelayCommand<BillResponse>((t) => { return true; }, t =>
            {
                if (t != null)
                {
                    int index = FoodOrderList.IndexOf(t);
                    t.IsGift = t.IsGift == 1 ? 0 : 1;
                    FoodOrderList.Remove(t);
                    FoodOrderList.Insert(index, t);
                    GetTotalAmount();
                }
            });
            CancelOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                CloseOrder(t);
            });
            SendCookOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                isCheckSave = false; // Dat
                if (SendCook)
                {
                    if (FoodOrderList != null && FoodOrderList.Count > 0)
                    {
                        if (checkPromotion() == true)
                        {
                            SendCookOrder();
                            AllFood = (List<FoodMenuItem>)Utils.Utils.GetCacheValue(Constants.CURRENT_LIST_FOOD);
                            GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                            if (ItemsOrderDetail.Count == 0)
                            {
                                GetAllFoodLisst();
                            }
                            GetCategoryMenu((int)CategoryTypeEnum.FOOD);
                            GetFoodMenu((int)CategoryTypeEnum.FOOD, Constants.ALL, true);
                        }
                        else
                        {
                            NotificationMessage.Error("Món tặng không được lớn hơn món chính !!!");
                        }
                        //SendCookOrder();
                    }
                }
            });
            SaveOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    SaveOrder(t);
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !!!");
                }
                //SaveOrder(t);
            });
            SaveAndAddOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    SaveAndAddOrder();

                    FrameworkElement window = GetWindowParent(t);
                    var w = window as Window;
                    _MainContentControl = w.FindName("ContentCt") as ContentControl;
                    _MainContentControl.Content = new HomeUserControl();

                    CreateOrderUserControl create = new CreateOrderUserControl();
                    create.DataContext = new CreateOrderViewModel();
                    _MainContentControl.Content = create;
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !!!");
                }
                //SaveAndAddOrder();

                //FrameworkElement window = GetWindowParent(t);
                //var w = window as Window;
                //_MainContentControl = w.FindName("ContentCt") as ContentControl;
                //_MainContentControl.Content = new HomeUserControl();

                //CreateOrderUserControl create = new CreateOrderUserControl();
                //create.DataContext = new CreateOrderViewModel();
                //_MainContentControl.Content = create;
            });
            PaymentOrderCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                if (checkPromotion() == true)
                {
                    PayMent(t);
                }
                else
                {
                    NotificationMessage.Error("Món tặng không được lớn hơn món chính !!!");
                }
            });
            DiscountCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                ApplyDiscount();
            });
            VATCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                ApplyVat();
            });
            AddExtraChargeCommand = new RelayCommand<UserControl>((t) => { return true; }, t =>
            {
                CreateRestaurantExtraWindow window = new CreateRestaurantExtraWindow();
                window.DataContext = new CreateRestaurantExtraViewModel(CurrentOrderId);
                window.ShowDialog();
                var extra = window.DataContext as CreateRestaurantExtraViewModel;
                if (extra.isCreate)
                {
                    extra.ExtraChange.IsPrint = true;
                    FoodOrderList.Add(extra.ExtraChange);
                    GetTotalAmount();
                }
                GetDetail(CurrentOrderId);
            });
        }
        public void GetDetail()
        {
            CurrentTableId = 0;
            CurrentOrderId = 0;
            FoodOrderList.Clear();
            TableContent = MessageValue.MESSAGE_NOT_TABLE_DEFAULT;
            TableForeground = new SolidColorBrush(Colors.White);
            BringOutForeground = new SolidColorBrush(Colors.Black);
            TableBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
            BringOutBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.GRAY_COLOR));
            SlotCustomerContent = 0;
            TotalAmount = "0";
            DiscountCount = null;
            sendCookEnabled = false;
            SendCook = false;
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
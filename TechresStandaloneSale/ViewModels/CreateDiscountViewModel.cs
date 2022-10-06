using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class CreateDiscountViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _FoodName;
        public string FoodName { get => _FoodName; set { _FoodName = value; OnPropertyChanged("FoodName"); } }

        private int _Quantity;
        public int Quantity { get => _Quantity; set { _Quantity = value; OnPropertyChanged("Quantity"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        private bool _AllBill;
        public bool AllBill { get => _AllBill; set { _AllBill = value; } }

        private bool _FoodBill;
        public bool FoodBill { get => _FoodBill; set { _FoodBill = value; } }
        private bool _DrinkBill; 
        public bool DrinkBill { get => _DrinkBill; set {_DrinkBill = value ;OnPropertyChanged("DrinkBill"); } }

        private Visibility _TotalBillVisibility { get; set; }
        public Visibility TotalBillVisibility { get => _TotalBillVisibility; set { _TotalBillVisibility = value; OnPropertyChanged("TotalBillVisibility"); } }

        private Visibility _FoodVisibility { get; set; }
        public Visibility FoodVisibility { get => _FoodVisibility; set { _FoodVisibility = value; OnPropertyChanged("FoodVisibility"); } }
        private Visibility _DrinkVisibility;
        public Visibility DrinkVisibility { get => _DrinkVisibility; set { _DrinkVisibility = value; OnPropertyChanged("DrinkVisibility"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public DiscountWrapper Wrapper { get; set; }

        private ObservableCollection<BasicModel> _NoteDiscountList = new ObservableCollection<BasicModel>();
        public ObservableCollection<BasicModel> NoteDiscountList
        {
            get
            {
                return _NoteDiscountList;
            }
            set
            {
                _NoteDiscountList = value;
                OnPropertyChanged("NoteDiscountList");
            }
        }
        private BasicModel _NoteDiscountItem { get; set; }
        public BasicModel NoteDiscountItem { get => _NoteDiscountItem; set { _NoteDiscountItem = value; OnPropertyChanged("NoteDiscountItem"); } }
        
        public void GetNoteDiscountList()
        {
            if (NoteDiscountList.Count > 0)
            {
                NoteDiscountList.Clear();
            }
            else
            {
                NoteDiscountList = new ObservableCollection<BasicModel>();
            }
            NoteDiscountList.Add(new BasicModel(0, MessageValue.MESSAGE_FROM_CUSTOMER_FAMILIAR));
            NoteDiscountList.Add(new BasicModel(1, MessageValue.MESSAGE_FROM_CUSTOMER_VIP));
            NoteDiscountList.Add(new BasicModel(2, MessageValue.MESSAGE_FROM_PROMOTION_SHOP));
        }
        public CreateDiscountViewModel()
        {
            if (currentUser != null)
            {     
                    Quantity = 0;
                    TotalBillVisibility = Visibility.Collapsed;
                    FoodVisibility = Visibility.Collapsed;
                    DrinkVisibility = Visibility.Collapsed; 

                    if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_FOOD), currentUser.Permissions)
                          && Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_ORDER), currentUser.Permissions))
                    {
                        TotalBillVisibility = Visibility.Visible;
                        FoodVisibility = Visibility.Visible;
                        DrinkVisibility = Visibility.Visible; 
                        AllBill = true;
                        FoodBill = false;
                        DrinkBill = false; 
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_FOOD), currentUser.Permissions))
                    {
                        FoodVisibility = Visibility.Visible;
                        DrinkVisibility = Visibility.Visible; 
                        FoodBill = true;
                        DrinkBill = true; 
                    }
                    else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.DISCOUNT_ORDER), currentUser.Permissions))
                    {
                        TotalBillVisibility = Visibility.Visible;
                        DrinkVisibility = Visibility.Visible;
                        AllBill = true;
                    }
                GetNoteDiscountList();
            }


            AddCommand = new RelayCommand<CreateDiscountWindow>((t) => { return true; }, t =>
            {
                if (!AllBill&& !FoodBill && !DrinkBill)
                {
                    //MessageNotificationWindow window = new MessageNotificationWindow();
                    //window.ContentNotification.Text = MessageValue.MESSAGE_NOT_TYPE_DISCOUNT;
                    //window.ShowDialog();
                    //  MessageBox.Show("Vui lòng chọn loại khuyến mãi!");
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_TYPE_DISCOUNT);
                    return;
                }
                else if (Quantity == 0)
                {
                    //MessageNotificationWindow window = new MessageNotificationWindow();
                    //window.ContentNotification.Text = MessageValue.MESSAGE_NOT_QUANTITY_DISCOUNT;
                    //window.ShowDialog();
                    //MessageBox.Show("Vui lòng nhập số lượng khuyễn mãi!");
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_QUANTITY_DISCOUNT);
                    return;
                }
                else if (NoteDiscountItem== null)
                {
                    //MessageNotificationWindow window = new MessageNotificationWindow();
                    //window.ContentNotification.Text = MessageValue.MESSAGE_NOT_NOTE_DISCOUNT;
                    //window.ShowDialog();
                    // MessageBox.Show("Vui lòng chọn lý do khuyễn mãi!");
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_NOTE_DISCOUNT);
                    return;
                }
                else if (Quantity < 0)
                {
                    //MessageNotificationWindow window = new MessageNotificationWindow();
                    //window.ContentNotification.Text = MessageValue.MESSAGE_NOT_RIGHT_QUANTITY_DISCOUNT;
                    //window.ShowDialog();
                    Quantity = 0;
                    //MessageBox.Show("Vui lòng nhập chính xác số lượng khuyễn mãi!");
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_RIGHT_QUANTITY_DISCOUNT);
                    return ;
                }
                else if (Quantity > 100)
                {
                    //MessageNotificationWindow window = new MessageNotificationWindow();
                    //window.ContentNotification.Text = MessageValue.MESSAGE_NOT_RIGHT_QUANTITY_DISCOUNT;
                    //window.ShowDialog();
                    Quantity = 0;
                    //MessageBox.Show("Vui lòng nhập chính xác số lượng khuyễn mãi!");
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_RIGHT_QUANTITY_DISCOUNT);
                    return;
                }
                else
                {
                    int DiscountType = 0;
                    if (AllBill)
                    {
                        DiscountType = (int)DiscountEnum.ALL_BILL;

                    }
                    else if (FoodBill) DiscountType = (int)DiscountEnum.FOOD_BILL;
                    else if (DrinkBill) DiscountType = (int)DiscountEnum.DRINK_BILL; 
                    Wrapper = new DiscountWrapper();
                    Wrapper.DiscountType = DiscountType;
                    Wrapper.DiscountPercent = Quantity;
                    Wrapper.Note = NoteDiscountItem!= null? NoteDiscountItem.Content:"";
                    t.Close();
                }

            });
            CloseCommand = new RelayCommand<CreateDiscountWindow>((t) => { return true; }, t =>
            {
                if (!string.IsNullOrEmpty(Note))
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
                        t.Close();
                    }
                }
                else
                {
                    t.Close();
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
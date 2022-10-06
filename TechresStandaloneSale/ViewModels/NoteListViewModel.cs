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
    public class NoteListViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<OrderNoteDetailResponseData> _NoteOrderDetailList = new ObservableCollection<OrderNoteDetailResponseData>();
        public ObservableCollection<OrderNoteDetailResponseData> NoteOrderDetailList { get => _NoteOrderDetailList; set { _NoteOrderDetailList = value; OnPropertyChanged("NoteOrderDetailList"); } }
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
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand IsCheckCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        private string _ContentTitle { get; set; }
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private ObservableCollection<Branch> _BranchList = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged("BranchList"); } }

        private Visibility _BranchVisibility;
        public Visibility BranchVisibility { get => _BranchVisibility; set { _BranchVisibility = value; OnPropertyChanged("BranchVisibility"); } }

        private Branch _BranchItem;
        public Branch BranchItem { get => _BranchItem; set { _BranchItem = value; OnPropertyChanged("BranchItem"); } }
        private ObservableCollection<Brand> _BrandList = new ObservableCollection<Brand>();
        public ObservableCollection<Brand> BrandList { get => _BrandList; set { _BrandList = value; OnPropertyChanged("BrandList"); } }
        private Brand _BrandItem { get; set; }
        public Brand BrandItem { get => _BrandItem; set { _BrandItem = value; OnPropertyChanged("BrandItem"); } }
        private Visibility _BrandVisibility { get; set; }
        public Visibility BrandVisibility { get => _BrandVisibility; set { _BrandVisibility = value; OnPropertyChanged("BrandVisibility"); } }
        private int BrandId;
        private long BranchId;
        public NoteListViewModel()
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
            GetListNote(BrandId, BranchId);
            SelectionChangedCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                DialogHostOpen = true;
                if (BranchId != (BranchItem != null ? BranchItem.Id : currentUser.BranchId))
                {
                    BranchId = BranchItem != null ? BranchItem.Id : currentUser.BranchId;
                    GetListNote(BrandId, BranchId);
                }
            });
            SelectionChangedBrandCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
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
                    BranchId = 0;
                    if (NoteOrderDetailList == null)
                    {
                        NoteOrderDetailList = new ObservableCollection<OrderNoteDetailResponseData>();
                    }
                    else
                    {
                        NoteOrderDetailList.Clear();
                    }
                }
                DialogHostOpen = false;
            });
            AddCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                CreateNoteOrderDetailWindow window = new CreateNoteOrderDetailWindow();
                window.DataContext = new ManageNoteOrderDetailViewModel(BrandId, BranchId);
                window.ShowDialog();
                var create = window.DataContext as ManageNoteOrderDetailViewModel;
                if (create.IsCreated)
                {
                    GetListNote(BrandId, BranchId);
                }
            });
            EditCommand = new RelayCommand<OrderNoteDetailResponseData>((p) => { return true; }, (p) =>
            {
                CreateNoteOrderDetailWindow window = new CreateNoteOrderDetailWindow();
                window.DataContext = new ManageNoteOrderDetailViewModel(p, BrandId, BranchId);
                window.ShowDialog();
                var create = window.DataContext as ManageNoteOrderDetailViewModel;
                if (create.IsCreated)
                {
                    GetListNote(BrandId, BranchId);
                }
            });
            DeleteCommand = new RelayCommand<OrderNoteDetailResponseData>((t) => { return true; }, (t) =>
            {
                ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow();
                string contentConfirm = MessageValue.MESSAGE_NOTIFICATION_DELETE_NOTE_ORDER_DETAIL;
                string Title = MessageValue.MESSAGE_NOTIFICATION;
                string YesContent = MessageValue.MESSAGE_YES_CONTENT;
                string NoContent = MessageValue.MESSAGE_NO_CONTENT;
                confirmDeleteWindow.DataContext = new ConfirmViewModel(contentConfirm, Title, NoContent, YesContent);
                confirmDeleteWindow.ShowDialog();
                var confirm = confirmDeleteWindow.DataContext as ConfirmViewModel;
                if (confirm.isConfirm)
                {
                    OrderNoteDetailResponseData order = NoteOrderDetailList.Where(x => x.Id == t.Id).FirstOrDefault();
                    if (order != null)
                    {
                        CreateNoteOrderDetailWrapper create = new CreateNoteOrderDetailWrapper();
                        create.Id = t.Id;
                        create.Delete = 1;
                        create.BrandId =BrandId;
                        create.BranchId = BranchId;
                        OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                        BaseResponse response = client.CreateNoteOrderDetail(create, BranchId, BrandId);
                        if (response != null)
                        {
                            GetListNote(BrandId, BranchId);
                        }
                    }
                }
            });
        }
        public async void GetListNote(int brandId, long branchId)
        {
            if (NoteOrderDetailList != null)
                NoteOrderDetailList.Clear();
            else
                NoteOrderDetailList = new ObservableCollection<OrderNoteDetailResponseData>();
            DialogHostOpen = true;
            OrderDetailsClient client = new OrderDetailsClient(this, this, this);
            OrderNoteDetailResponse response = await System.Threading.Tasks.Task.Run(() => client.GetOrderDetailNote(brandId, branchId));
            if (response != null && response.Data != null && response.Data != null)
            {
                response.Data.ForEach(NoteOrderDetailList.Add);
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
            ContentTitle = string.Format(MessageValue.MESSAGE_FROM_LIST_NOTE_FOOD_TITLE, NoteOrderDetailList.Count);
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

using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;


namespace TechresStandaloneSale.ViewModels
{
    class ManageNoteOrderDetailViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SelectionChangedBrandCommand { get; set; }
        public ICommand TextSelectionChangedCommand { get; set; }

        public bool IsCreated = false;

        private string _ContentTitle;
        public string ContentTitle { get => _ContentTitle; set { _ContentTitle = value; OnPropertyChanged("ContentTitle"); } }

        private string _Content;
        public string Content { get => _Content; set { _Content = value; OnPropertyChanged("Content"); } }


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
        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);

        private System.Windows.Media.Brush _ColorBoder;
        public System.Windows.Media.Brush ColorBoder { get => _ColorBoder; set { _ColorBoder = value; OnPropertyChanged("ColorBoder"); } }
        public ManageNoteOrderDetailViewModel(int brandId, long branchId)
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
                BrandItem = BrandList.Where(x => x.Id == brandId).FirstOrDefault();
                BrandId = BrandItem == null ? brandId : BrandItem.Id;
                if (BranchList == null)
                {
                    BranchList = new ObservableCollection<Branch>();
                }
                else
                {
                    BranchList.Clear();
                }
                BranchList = Utils.Utils.GetBranchs(brandId, true);
                BranchItem = BranchList.Where(x => x.Id == branchId).FirstOrDefault();
                BranchId = BranchItem == null ? branchId : BranchItem.Id;
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
                BranchList = Utils.Utils.GetBranchs(brandId, true);
                BranchItem = BranchList.Where(x => x.Id == branchId).FirstOrDefault();
                BrandId = currentUser.RestaurantBrandId;
                BranchId = BranchItem == null ? brandId : BranchItem.Id;
            }
            else
            {
                BrandVisibility = Visibility.Collapsed;
                BranchVisibility = Visibility.Collapsed;
                BrandId = currentUser.RestaurantBrandId;
                BranchId = currentUser.BranchId;
            }
            ColorBoder = System.Windows.Media.Brushes.Transparent;
            ContentTitle = "TẠO GHI CHÚ";
            SelectionChangedBrandCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
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
                }
            });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(Content))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_WRITE_ORDER_DETAIL_NOTE);
                    ColorBoder = System.Windows.Media.Brushes.Red;
                    return;
                }
                else
                {
                    CreateNoteOrderDetailWrapper create = new CreateNoteOrderDetailWrapper();
                    create.Content = Content;
                    create.Delete = 0;  
                    create.BrandId = BrandItem != null? BrandItem.Id:brandId;
                    create.BranchId = BranchItem != null ? BranchItem.Id : branchId;
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    BaseResponse response = client.CreateNoteOrderDetail(create,create.BranchId, create.BrandId);
                    if (response != null)
                    {
                        IsCreated = true;
                        p.Close();
                    }
                }
            });
            TextSelectionChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (Content == null || Content.Length > 0)
                {
                    ColorBoder = System.Windows.Media.Brushes.Transparent;
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public ManageNoteOrderDetailViewModel(OrderNoteDetailResponseData data, int brandId, long branchId)
        {
            BrandVisibility = Visibility.Collapsed;
            BranchVisibility = Visibility.Collapsed;
            ContentTitle = "CHỈNH SỬA GHI CHÚ";
            Content = data.Content;
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(Content))
                {
                    NotificationMessage.Warning(MessageValue.MESSAGE_NOT_WRITE_ORDER_DETAIL_NOTE);
                    ColorBoder = ColorBoder = System.Windows.Media.Brushes.Red;
                    return;
                }
                else
                {
                    CreateNoteOrderDetailWrapper create = new CreateNoteOrderDetailWrapper();
                    create.Id = data.Id;
                    create.Content = Content;
                    create.Delete = 0;
                    create.BrandId = brandId;
                    create.BranchId = branchId;
                    OrderDetailsClient client = new OrderDetailsClient(this, this, this);
                    BaseResponse response = client.CreateNoteOrderDetail(create,branchId, brandId);
                    if (response != null)
                    {
                        IsCreated = true;
                        p.Close();
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            TextSelectionChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (Content == null || Content.Length > 0)
                {
                    ColorBoder = ColorBoder = System.Windows.Media.Brushes.Transparent;
                }
            });
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

        public void LogError(Exception ex, string infoMessage)
        {
        }
    }
}
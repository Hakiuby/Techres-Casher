using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;

namespace TechresStandaloneSale.ViewModels
{
    public class ChooseKitchenViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand AddCommand { get; set; }
        public ICommand IsChooseCommand { get; set; }
        public ICommand CloseCommand { get; set; }

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
        public bool IsConfrim;
        public void GetDetail()
        {
            if (KitchenList != null)
            {
                KitchenList.Clear();
            }
            else
            {
                KitchenList = new ObservableCollection<Kitchen>();
            }
            Kitchen CurrentKitchen = Utils.Utils.AsObject<Kitchen>(Properties.Settings.Default.CurrentKitchen);
            KitchenClient client = new KitchenClient(this, this, this);
            if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.CHEF_COOK_ACCESS), currentUser.Permissions))
            {
                KitchenResponse response = client.GetKitchenResponses(currentUser.RestaurantBrandId, (int)currentUser.BranchId, -1,1); // Enum = 1 : Bếp 
                if (response != null && response.Status == (int)ResponseEnum.OK)
                {
                    if (CurrentKitchen != null)
                    {
                        foreach (Kitchen i in response.Data)
                        {
                            if (i.Id == CurrentKitchen.Id)
                            {
                                i.IsChoose = true;
                                KitchenList.Add(i);
                                KitchenItem = i;
                            }
                            else
                            {
                                KitchenList.Add(i);
                            }
                        }
                    }
                    else
                    {
                        response.Data.ForEach(KitchenList.Add);
                    }

                }
            }
            else if (Utils.Utils.CheckPermissionsEmployee(Enum.GetName(typeof(TechresEnum), TechresEnum.BAR_ACCESS), currentUser.Permissions))
            {
                KitchenResponse response = client.GetKitchenResponses(currentUser.RestaurantBrandId, (int)currentUser.BranchId, -1,0); // Enum = 0 : Kho Bia 
                if (response != null && response.Status == (int)ResponseEnum.OK)
                {
                    if (CurrentKitchen != null)
                    {
                        foreach (Kitchen i in response.Data)
                        {
                            if (i.Id == CurrentKitchen.Id)
                            {
                                i.IsChoose = true;
                                KitchenList.Add(i);
                                KitchenItem = i;
                            }
                            else
                            {
                                KitchenList.Add(i);
                            }
                        }
                    }
                    else
                    {
                        response.Data.ForEach(KitchenList.Add);
                    }

                }
            }
        }

        public User currentUser;
        private Kitchen KitchenItem;
        public ChooseKitchenViewModel()
        {
            currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
            GetDetail();
            AddCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                if (KitchenItem != null)
                {
                    IsConfrim = true;
                    Properties.Settings.Default.CurrentKitchen = Utils.Utils.AsJson<Kitchen>(KitchenItem);
                    Properties.Settings.Default.Save();
                    p.Close();
                }

            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            IsChooseCommand = new RelayCommand<Kitchen>((p) => { return true; }, p =>
        {
            if (p != null)
            {
                if (!p.IsChoose)
                {

                    p.IsChoose = true;
                    KitchenItem = p;
                    int indexP = KitchenList.IndexOf(p);
                    KitchenList.Remove(p);
                    KitchenList.Insert(indexP, p);
                    List<Kitchen> kitchens = KitchenList.Where(x => x.IsChoose && x.Id != p.Id).ToList();
                    foreach (Kitchen k in kitchens)
                    {
                        int index = KitchenList.IndexOf(k);
                        KitchenList.Remove(k);
                        k.IsChoose = false;
                        KitchenList.Insert(index, k);
                    }
                }
                else
                {
                    KitchenItem = null;
                }
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

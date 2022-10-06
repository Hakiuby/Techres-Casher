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
using System.Windows.Media;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;

namespace TechresStandaloneSale.ViewModels
{
    public class HistoryPrintManagerViewModel: BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        #region toan
        private Brush _unsentBakcground { get; set; }
        public Brush unsentBakcground { get => _unsentBakcground; set { _unsentBakcground = value; OnPropertyChanged("unsentBakcground"); } }

        private Brush _unsentForeground { get; set; }
        public Brush unsentForeground { get => _unsentForeground; set { _unsentForeground = value; OnPropertyChanged("unsentForeground"); } }

        private Brush _sentBakcground { get; set; }
        public Brush sentBakcground { get => _sentBakcground; set { _sentBakcground = value; OnPropertyChanged("sentBakcground"); } }

        private Brush _sentForeground { get; set; }
        public Brush sentForeground { get => _sentForeground; set { _sentForeground = value; OnPropertyChanged("sentForeground"); } }

        #endregion

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public ICommand LoadedUCCommand { get; set; }
        public ICommand HistoryFoodSendCommand { get; set; }
        public ICommand HistoryFoodDontSend { get; set; }
        public ICommand CloseCommnad { get; set; }
        public ICommand PrintBillCommand { get; set; }
        public ICommand SendCookBarCommand { get; set; }
        public ICommand SendAllCookBarCommand { get; set; }
        public ICommand ExtendFoodNameCommand { get; set; }
        public ICommand ShortenFoodNameCommand { get; set; }

        #region Layout Properties 
        private Visibility _PrintBillAgainVisibility; 
        public Visibility PrintBillAgainVisibility
        {
            get => _PrintBillAgainVisibility; 
            set
            {
                _PrintBillAgainVisibility = value;
                OnPropertyChanged("PrintBillAgainVisibility"); 
            }
        }
        private Visibility _SendCookBarVisibility; 
        public Visibility SenCookBarVisibility
        {
            get => _SendCookBarVisibility; 
            set
            {
                _SendCookBarVisibility = value;
                OnPropertyChanged("SenCookBarVisibility");
            }
        }

        #endregion
        

        public bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }
        private long _TotalDish { get; set; }
        public long TotalDish { get => _TotalDish; set { _TotalDish = value; OnPropertyChanged("TotalDish"); } }

        public ObservableCollection<HistoryPrintData> _LstFood; 
        public ObservableCollection<HistoryPrintData> LstFood
        {
            get => _LstFood; 
            set
            {
                _LstFood = value;
                OnPropertyChanged("LstFood"); 
            }
        }
        private List<string> _LstNameFood; 
        public List<string> LstNameFood
        {   
            get => _LstNameFood; 
            set
            {
                _LstNameFood = value;
                OnPropertyChanged("LstNameFood");  
            }
        }
        private string _NameLstFood; 
        public string NameLstFood { get => _NameLstFood; set {_NameLstFood = value ;OnPropertyChanged("NameLstFood"); } }
        private HistoryPrintEnum CurrentButton;
        private long IsPrint;
        private List<Kitchen> CurrentKitchens = Utils.Utils.AsObjectList<Kitchen>(Properties.Settings.Default.RestaurantKitchenPlacePrint);
        public async void GetData(long isPrint, HistoryPrintEnum currentButton)
        {
           
            CurrentButton = currentButton;
            IsPrint = isPrint; 
            switch (currentButton)
            {
                case HistoryPrintEnum.Send:
                {
                        HistoryPrintClient client = new HistoryPrintClient(this, this, this);
                        HistoryPrintResponse response = await Task.Run(() => client.GetListFoodSendCook(-1, currentUser.BranchId));
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if(response != null && response.Data != null && response.Status == (int)ResponseEnum.OK)
                            {
                                TotalDish = response.Data.Count();
                                for(int i = 0; i < TotalDish; i++)
                                {
                                    //HistoryOrderDetails item = response.Data[i];
                                    //HistoryPrintData item = response.Data[i].OrderDetails;
                                    //if (item.CreateAt == null)
                                    //{
                                    //    item.CreateAt = "null";
                                    //}
                                    //LstFood.Add(item); 
                                }
                                DialogHostOpen = false; 
                            }
                            else
                            {
                                DialogHostOpen = false; 
                            }
                        });
                    break; 
                }
                case HistoryPrintEnum.NotSen:
                    {
                        if(LstFood != null)
                        {
                            LstFood.Clear(); 
                        }
                        else
                        {
                            LstFood = new ObservableCollection<HistoryPrintData>(); 
                        }
                        HistoryPrintClient client = new HistoryPrintClient(this, this, this);
                        HistoryPrintResponse response = await Task.Run(() => client.GetListFoodSendCook(-1, currentUser.BranchId));
                        WriteLog.logs("Chưa gửi");
                        //if (response != null && response.Data != null && response.Status == (int)ResponseEnum.OK)
                        //{
                        //    response.Data.ForEach(LstFood.Add);
                        //    foreach (HistoryPrintData data in LstFood)
                        //    {
                        //        LstNameFood = data.OrderDetails.Select(x => x.FoodName).ToList();
                        //        NameLstFood = LstNameFood.Aggregate("", (current, next) => current + ", " + next);
                        //    }
                        //    this.NameLstFood = NameLstFood;

                        //}
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (response != null && response.Data != null && response.Status == (int)ResponseEnum.OK)
                            {
                                var dataList = response.Data.Where(x => x.OrderDetails.Count != 0).ToList();
                                dataList.ForEach(LstFood.Add);
                                //response.Data.ForEach(LstFood.Add);
                                foreach (HistoryPrintData data in LstFood)
                                {
                                    //LstNameFood = data.OrderDetails.Select(x => x.FoodName).ToList();
                                    //LstNameFood = data.OrderDetails.Select(x => x.FoodName).Take(6).ToList();
                                    //NameLstFood = LstNameFood.Aggregate("", (current, next) => current + ", " + next);
                                    //data.NameFood = LstNameFood.Aggregate("", (current, next) => current + ", " + next);
                                    //NameLstFood = LstNameFood.Aggregate((current, next) => current + ", " + next);
                                    //data.NameFood = LstNameFood.Aggregate((current, next) => current + ", " + next);
                                    if (data.LstNameFood.Count > 6)
                                    {
                                        LstNameFood = data.OrderDetails.Select(x => x.FoodName).Take(6).ToList();
                                        NameLstFood = LstNameFood.Aggregate((current, next) => current + ", " + next);
                                        data.NameFood = LstNameFood.Aggregate((current, next) => current + ", " + next);
                                        data.ZoomInVisibilityy = Visibility.Visible;
                                        data.ZoomOutVisibility = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        LstNameFood = data.OrderDetails.Select(x => x.FoodName).ToList();
                                        NameLstFood = LstNameFood.Aggregate((current, next) => current + ", " + next);
                                        data.NameFood = LstNameFood.Aggregate((current, next) => current + ", " + next) + ".";
                                        data.ZoomInVisibilityy = Visibility.Collapsed;
                                        data.ZoomOutVisibility = Visibility.Collapsed;
                                    }
                                }
                                this.NameLstFood = NameLstFood;
                            }
                            DialogHostOpen = false;
                        });
                        break;
                    }
               
            }
        }
        public async void SendCookBar(long orderId)
        {
            Helpers.PrintBill.PrintText pt = new PrintBill.PrintText();
            OrdersClient client = new OrdersClient(this, this, this);
            OrderDetailPrintResponse orderDetailPrintResponse = await Task.Run(() => client.GetOrderDetailPrint(orderId));
            if(orderDetailPrintResponse !=  null && orderDetailPrintResponse.Status == (int)ResponseEnum.OK)
            {
                List<StoreProcedureListResult<OrderDetailPrint, long>> storeProcedureListResults = new List<StoreProcedureListResult<OrderDetailPrint, long>>();
                foreach (Kitchen k in CurrentKitchens)
                {
                    List<OrderDetailPrint> result = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity > x.PrintedQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                    if (result != null && result.Count > 0)
                    {
                        //pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName, string.Format("#{0}", addFood.Data.Id), addFood.Data.EmployeeName, (int)k.PrinterPaperSize);
                        pt.PrintFoodCook(k.PrinterName, result, orderDetailPrintResponse.Data[0].TableName,
                        string.Format("#{0}", orderDetailPrintResponse.Data[0].OrderId), orderDetailPrintResponse.Data[0].EmployeeName, (int)80);
                    }
                    //List<OrderDetailPrint> resultUpdate = new List<OrderDetailPrint>(orderDetailPrintResponse.Data.Where(x => x.RestaurantKitchenPlaceId == k.Id && x.Quantity < x.OldQuantity && (x.OrderDetailStatus != (int)OrderDetailStatusEnum.CANCEL || x.OrderDetailStatus != (int)OrderDetailStatusEnum.OUTSTOCK)).ToList());
                    //if (result != null && result.Count > 0)
                    //{
                    //    foreach (OrderDetailPrint od in resultUpdate)
                    //    {
                    //        pt.PrintFoodMove(od, k.PrinterName, addFood.Data.EmployeeName, addFood.Data.TableName, (int)k.PrinterPaperSize);
                    //    }
                    //}
                }
                IsPrintSendFoodCookWrapper wrapperIsPrint = new IsPrintSendFoodCookWrapper(orderDetailPrintResponse.Data.ToList().Select(x => x.Id).ToList());
                await Task.Run(() => client.UpdateOrderIsPrintSendCook(wrapperIsPrint, orderId));

            }
        }

        public HistoryPrintManagerViewModel()
        {
            LstFood = new ObservableCollection<HistoryPrintData>();

            var converter = new System.Windows.Media.BrushConverter();//toan

            unsentBakcground = (Brush)converter.ConvertFromString("#FFA233");
            sentBakcground = new SolidColorBrush(Colors.LightGray);

            unsentForeground = new SolidColorBrush(Colors.White);
            sentForeground = new SolidColorBrush(Colors.Black);
            SenCookBarVisibility = Visibility.Visible;
            LoadedUCCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                DialogHostOpen = true;
                PrintBillAgainVisibility = Visibility.Visible;
                SenCookBarVisibility = Visibility.Visible; 

                GetData(0, HistoryPrintEnum.NotSen);
            });
            HistoryFoodSendCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                #region toan
                unsentBakcground = new SolidColorBrush(Colors.LightGray);
                sentBakcground = (Brush)converter.ConvertFromString("#FFA233");

                unsentForeground = new SolidColorBrush(Colors.Black);
                sentForeground = new SolidColorBrush(Colors.White);
                #endregion

                DialogHostOpen = true;
                PrintBillAgainVisibility = Visibility.Visible;

                GetData(0, HistoryPrintEnum.Send); 
            });
            HistoryFoodDontSend = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                #region toan
                unsentBakcground = (Brush)converter.ConvertFromString("#FFA233");
                sentBakcground = new SolidColorBrush(Colors.LightGray);

                unsentForeground = new SolidColorBrush(Colors.White);
                sentForeground = new SolidColorBrush(Colors.Black);
                #endregion
                DialogHostOpen = true;

                PrintBillAgainVisibility = Visibility.Collapsed;
                GetData(0, HistoryPrintEnum.NotSen);
            }); 
            CloseCommnad = new RelayCommand<Window>((p)=> { return true; }, (p) => {
                p.Close(); 
            });
            PrintBillCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

            });
            SendCookBarCommand = new RelayCommand<HistoryPrintData>((p) => { return true; }, (p) => {
                SendCookBar(p.Id);
                LstFood.Remove(p);
            });
            SendAllCookBarCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (LstFood.Count > 0)
                {
                    for (int i = 0; i < LstFood.Count; i++)
                    {

                        SendCookBar(LstFood[i].Id);
                    }
                    LstFood.Clear();
                }
                else
                {
                    NotificationMessage.Warning("Không có danh sách món cần gửi bếp bar");
                }
                
            });
            ExtendFoodNameCommand = new RelayCommand<HistoryPrintData>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    var item = LstFood.Where(x => x.Id == p.Id).FirstOrDefault();
                    int index = LstFood.IndexOf(item);
                    item = p;
                    item.NameFood= p.LstNameFood.Aggregate((current, next) => current + ", " + next) + ".";
                    item.ZoomInVisibilityy = Visibility.Collapsed;
                    item.ZoomOutVisibility = Visibility.Visible;
                    LstFood.Remove(p);
                    LstFood.Insert(index, item);
                }
                else
                {
                    MessageBox.Show("null");
                }
            });
            ShortenFoodNameCommand = new RelayCommand<HistoryPrintData>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    var item = LstFood.Where(x => x.Id == p.Id).FirstOrDefault();
                    int index = LstFood.IndexOf(item);
                    item = p;
                    var list = p.LstNameFood.Take(6).ToList();
                    item.NameFood = list.Aggregate((current, next) => current + ", " + next);
                    item.ZoomInVisibilityy = Visibility.Visible;
                    item.ZoomOutVisibility = Visibility.Collapsed;
                    LstFood.Remove(p);
                    LstFood.Insert(index, item);
                }
                else
                {
                    MessageBox.Show("null");
                }
            });

        }

        public T Get<T>(string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object item, int minutes)
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
                    //Console.WriteLine(jsonResponse.message);
                    //string a = jsonResponse.message;
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
        public void LogError(Exception ex, string infoMessage)
        {
            //
        }
    }
}

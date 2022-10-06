using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class PaymentShipEndWorkingSessionViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private ObservableCollection<AdditionFee> _AdditionFeeList = new ObservableCollection<AdditionFee>();
        public ObservableCollection<AdditionFee> AdditionFeeList { get => _AdditionFeeList; set { _AdditionFeeList = value; OnPropertyChanged("AdditionFeeList"); } }

        private long _TotalAmount;
        public long TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged("TotalAmount"); } }
        private bool _DialogHostOpen;
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
        private string _Title { get; set; }
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged("Title"); } }
        private string _ObjectName { get; set; }
        public string ObjectName { get => _ObjectName; set { _ObjectName = value; OnPropertyChanged("ObjectName"); } }
      //  private string _ReasonName { get; set;  }
        //public string ReasonName { get => _ReasonName; set { _ReasonName = value; OnPropertyChanged("ReasonName"); } }

        public User currentUser = (User)Utils.Utils.GetCacheValue(Constants.CURRENT_USER);
        public async void GetListData(int page,int brandId, long branchId, string FromDate, string ToDate, int type, int isTake,int isCountToRevenue, long restaurantBudgetId, long employeeId, long orderSessionId, string ids)
        {
            if (AdditionFeeList == null)
            {
                AdditionFeeList = new ObservableCollection<AdditionFee>();
            }
            else
            {
                AdditionFeeList.Clear();
            }
            DialogHostOpen = true;
            string from = Utils.Utils.GetDateFormatVN(Utils.Utils.GetStringFormatDateTimeHour(FromDate));
            AdditionFeeClient warehouseClient = new AdditionFeeClient(this, this, this);
            AdditionFeeResponse response =await Task.Run(()=> warehouseClient.GetListAdditionFee(page, brandId,branchId, orderSessionId, isTake, FromDate, ToDate, type, isCountToRevenue, restaurantBudgetId, employeeId, ids));
            if (response != null && response.Data != null && response.Data.List != null && response.Data.List.Count > 0)
            {
                TotalAmount = 0;
                foreach (AdditionFee a in response.Data.List)
                {
                    if (a.IsAutomaticallyGenerated == 0 || a.AutomaticallyGeneratedType == 4)
                    {
                        AdditionFeeList.Add(a);
                        TotalAmount = TotalAmount + a.Amount;
                    }
                   
                }
                DialogHostOpen = false;
            }
            else
                DialogHostOpen = false;
            if (type == 0) { Title = MessageValue.MESSAGE_LIST_PAYMENT_SHIP; ObjectName = MessageValue.MESSAGE_LIST_PEOPLE_PAYMENT_SHIP; }
            else { Title = MessageValue.MESSAGE_LIST_RECEIPT_SHIP; ObjectName = MessageValue.MESSAGE_LIST_PEOPLE_RECEIPT_SHIP; }
        }
        public PaymentShipEndWorkingSessionViewModel(long sessionId,string OpenTime,int type, string addititonFeeIds )
        {
            GetListData(1, currentUser.RestaurantBrandId,currentUser.BranchId, OpenTime, Utils.Utils.GetDateFormatVN(DateTime.Now), type, Constants.ALL, Constants.ALL, Constants.ALL, currentUser.Id, sessionId, addititonFeeIds);

        }
        public PaymentShipEndWorkingSessionViewModel(long sessionId, int type, long branchId, long employeeId, string addititonFeeIds)
        {
            GetListData(1, currentUser.RestaurantBrandId, branchId, string.Empty, string.Empty, type, Constants.ALL, Constants.ALL, Constants.ALL, employeeId, sessionId, addititonFeeIds);

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

        public void LogError(Exception ex, string infoMessage)
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object item, int minutes)
        {
            throw new NotImplementedException();
        }
    }
}

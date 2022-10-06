using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ExpenseReceiptViewViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private string _Code { get; set; }
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged("Code"); } }
        private string _Type { get; set; }
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged("Type"); } }

        private string _ReasonName { get; set; }
        public string ReasonName { get => _ReasonName; set { _ReasonName = value; OnPropertyChanged("ReasonName"); } }
        private string _ObjectType { get; set; }
        public string ObjectType { get => _ObjectType; set { _ObjectType = value; OnPropertyChanged("ObjectType"); } }
        private string _ObjectName { get; set; }
        public string ObjectName { get => _ObjectName; set { _ObjectName = value; OnPropertyChanged("ObjectName"); } }
        private string _CreateAt { get; set; }
        public string CreateAt { get => _CreateAt; set { _CreateAt = value; OnPropertyChanged("CreateAt"); } }
        private string _Amount { get; set; }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged("Amount"); } }
        private string _PaymentType { get; set; }
        public string PaymentType { get => _PaymentType; set { _PaymentType = value; OnPropertyChanged("PaymentType"); } }
        private string _StatusText { get; set; }
        public string StatusText { get => _StatusText; set { _StatusText = value; OnPropertyChanged("StatusText"); } }

        private string _EmployeeCreate { get; set; }
        public string EmployeeCreate { get => _EmployeeCreate; set { _EmployeeCreate = value; OnPropertyChanged("EmployeeCreate"); } }
        private string _EmployeeConfirm { get; set; }
        public string EmployeeConfirm { get => _EmployeeConfirm; set { _EmployeeConfirm = value; OnPropertyChanged("EmployeeConfirm"); } }
        private string _Note { get; set; }
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }
        private Visibility _WarehouseVisibility { get; set; }
        public Visibility WarehouseVisibility { get => _WarehouseVisibility; set { _WarehouseVisibility = value; OnPropertyChanged("WarehouseVisibility"); } }
        private ObservableCollection<WarehouseSession> _WarehouseSessionList = new ObservableCollection<WarehouseSession>();
        public ObservableCollection<WarehouseSession> WarehouseSessionList
        {
            get
            {
                return _WarehouseSessionList;
            }
            set
            {
                _WarehouseSessionList = value;
                OnPropertyChanged("WarehouseSessionList");
            }
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }

        public AdditionFee detailAdditionFeeData = new AdditionFee();
        public void GetData(long branchId, long id)
        {
            if (WarehouseSessionList == null)
            {
                WarehouseSessionList = new ObservableCollection<WarehouseSession>();
            }
            else
            {
                WarehouseSessionList.Clear();
            }
            AdditionFeeClient client = new AdditionFeeClient(this, this, this);
            AdditionFeeOneResponse response = client.GetAdditionFeeDetail(id,branchId);
            if (response != null && response.Status == (long)ResponseEnum.OK && response.Data != null)
            {
                detailAdditionFeeData = response.Data;
                Code = detailAdditionFeeData.Code;
                Type = detailAdditionFeeData.TypeString;
                ReasonName = detailAdditionFeeData.AdditionFeeReasonName;
                ObjectType = detailAdditionFeeData.ObjectType;
                ObjectName = detailAdditionFeeData.ObjectName;
                CreateAt = detailAdditionFeeData.CreatedAt;
                Amount = detailAdditionFeeData.AmountString;
                PaymentType = detailAdditionFeeData.PaymentIdString;
                StatusText = detailAdditionFeeData.StatusAdditionString;
                EmployeeCreate = detailAdditionFeeData.Employee.Name;
                EmployeeConfirm = detailAdditionFeeData.EmployeeConfirm.Name;
                Note = detailAdditionFeeData.Note;
                detailAdditionFeeData.List.ForEach(WarehouseSessionList.Add);
                if (detailAdditionFeeData.Type == (long)ExpenseTypeEnum.SUPPLIER)
                {
                    WarehouseVisibility = Visibility.Visible;
                }
                else
                {
                    WarehouseVisibility = Visibility.Collapsed;
                }
            }

        }
        public ExpenseReceiptViewViewModel(long branchId, long id)
        {
            GetData(branchId, id);
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
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

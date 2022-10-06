using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels
{
    public class ManageCustomerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        private string _LastName;
        public string LastName { get => _LastName; set { _LastName = value; OnPropertyChanged("LastName"); } }
        private string _FirstName;
        public string FirstName { get => _FirstName; set { _FirstName = value; OnPropertyChanged("FirstName"); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged("Phone"); } }
        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged("Address"); } }
        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged("Birthday"); } }
        public bool IsCreate = false;
        public Customer CustomerUpdate;

        #region Đạt
        public ManageCustomerViewModel(string firstname = "", string lastname = "", string phone = "")
        {
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
            Address = string.Empty;
            Birthday = DateTime.Now.AddYears(-10);
            CustomerUpdate = new Customer();
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            AddCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            { // Validate 
                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_MANAGE_CUTOMER_FIRSTNAME);
                }
                else if (string.IsNullOrWhiteSpace(LastName))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_MANAGE_CUTOMER_LASTNAME);
                }
                else if(string.IsNullOrEmpty(Phone))
                {
                    NotificationMessage.Error(MessageValue.MESSAGE_MANAGE_CUTOMER_PHONE);
                }
                //else if(!IsValidVietNamPhoneNumber(Phone))
                //{ 
                //    NotificationMessage.Error(MessageValue.MESSAGE_PHONE_ERROR);
                //}    
                else
                {
                    CustomerUpdate.Phone = Phone;
                    CustomerUpdate.Name = string.Format("{0} {1}", LastName, FirstName);
                    CustomerUpdate.Address = string.IsNullOrEmpty(Address) ? "" : Address;
                    CustomerUpdate.Birthday = Utils.Utils.GetDateFormatVN(Birthday);
                    #region Create Customer 
                    //CustomerClient client = new CustomerClient(this, this, this);
                    //CustomerRegisterResponse response = client.Register(FirstName, LastName, CustomerUpdate.Address, Phone, CustomerUpdate.Birthday);
                    //if (response != null && response.Status == (int)ResponseEnum.OK)
                    //{
                    //    CustomerUpdate.Id = response.Data.Id;
                    //    IsCreate = true;
                    //    p.Close();
                    //}
                    IsCreate = true;
                    p.Close();
                    #endregion
                }

                //if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Phone))
                //{
                //    MessageBox.Show("001");
                //}

            });
        }
        #endregion

        public bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            string sMailPattern = @"^((0(\d){9}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
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

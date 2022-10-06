using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Views.Dialogs;

namespace TechresStandaloneSale.ViewModels.Dialogs
{
   public class ConfirmActiveVATViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand NoCommand { get; set; }
        public ICommand YesCommand { get; set; }
        public bool _CashIsCheck;
        public bool CashIsCheck
        {
            get
            {
                return _CashIsCheck;
            }
            set
            {
                _CashIsCheck = value;
                OnPropertyChanged("CashIsCheck");
            }
        }
        public bool _BankIsCheck;
        public bool BankIsCheck
        {
            get
            {
                return _BankIsCheck;
            }
            set
            {
                _BankIsCheck = value;
                OnPropertyChanged("BankIsCheck");
            }
        }
        public bool _TransferIsCheck;
        public bool TransferIsCheck
        {
            get
            {
                return _TransferIsCheck;
            }
            set
            {
                _TransferIsCheck = value;
                OnPropertyChanged("TransferIsCheck");
            }
        }
        public string _ContentConfirm;
        public string ContentConfirm
        {
            get
            {
                return _ContentConfirm;
            }
            set
            {
                _ContentConfirm = value;
                OnPropertyChanged("ContentConfirm");
            }
        }

        public string _TitleContent;
        public string TitleContent
        {
            get
            {
                return _TitleContent;
            }
            set
            {
                _TitleContent = value;
                OnPropertyChanged("TitleContent");
            }
        }

        public string _NoContent;
        public string NoContent
        {
            get
            {
                return _NoContent;
            }
            set
            {
                _NoContent = value;
                OnPropertyChanged("NoContent");
            }
        }

        public string _YesContent;
        public string YesContent
        {
            get
            {
                return _YesContent;
            }
            set
            {
                _YesContent = value;
                OnPropertyChanged("YesContent");
            }
        }
        public bool isConfirm;
        public int PaymentMethord = 0;
        public ConfirmActiveVATViewModel(string contentConfirm = "", string title = "", string noContent = "", string yesContent = "")
        {
            ContentConfirm = contentConfirm;
            TitleContent = title;
            NoContent = noContent;
            YesContent = yesContent;
            CashIsCheck = true;
            YesCommand = new RelayCommand<ConfirmActiveVATWindow>((t) => { return true; }, t =>
            {
                if (CashIsCheck)
                {
                    PaymentMethord = (int)PaymentMethodEnum.CASH;                      
                }
                else if (BankIsCheck)
                {
                    PaymentMethord = (int)PaymentMethodEnum.BANK;
                }else if (TransferIsCheck)
                {
                    PaymentMethord = (int)PaymentMethodEnum.TRANSFER;
                }
                else
                {
                    PaymentMethord = (int)PaymentMethodEnum.CASH;
                }
                isConfirm = true;
                t.Close();
            });
            NoCommand = new RelayCommand<ConfirmActiveVATWindow>((t) => { return true; }, t =>
            {
                isConfirm = false;
                t.Close();
            });
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

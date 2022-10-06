using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Helpers;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Services;
using TechresStandaloneSale.Models.Response;
using System.Text.RegularExpressions;
using static TechresStandaloneSale.Models.Request.ReturnBeerWrapper;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.ViewModels
{
    class ReturnBeerViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        private List<ReturnBeerOrderDetails> _data;
        public List<ReturnBeerOrderDetails> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }
        private long _QuantityReturn;
        public long QuantityReturn { get => _QuantityReturn; set { _QuantityReturn = value; OnPropertyChanged("QuantityReturn"); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged("Note"); } }

        public bool isCreate = false;

        public ICommand LoadCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddQuantityCommand { get; set; }
        public ICommand SubQuantityCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ReturnBeerViewModel()
        {

        }
        public ReturnBeerViewModel(long orderId, long id, string prefix, decimal quantityReturn,long branchId)
        {
            QuantityReturn = (long)quantityReturn;
            
            AddCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                Data = new List<ReturnBeerOrderDetails>();
                ReturnBeerOrderDetails returnBeer = new ReturnBeerOrderDetails(id, QuantityReturn);
                Data.Add(returnBeer);
                ReturnBeerWrapper returnBeerWrapper = new ReturnBeerWrapper(Data);
                OrdersClient returnBeerById = new OrdersClient(this, this, this);
                BaseResponse response = returnBeerById.ReturnBeerById(returnBeerWrapper, prefix, orderId, id, branchId);
                if (response != null && response.Status == 200)
                {
                    t.Close();
                    isCreate = true;
                }
                //if (string.IsNullOrWhiteSpace(Note))
                //{
                //    NotificationMessage.Error("Vui lòng nhập lí do !");
                //}
                //else
                //{
                //    Data = new List<ReturnBeerOrderDetails>();
                //    ReturnBeerOrderDetails returnBeer = new ReturnBeerOrderDetails(id, QuantityReturn);
                //    Data.Add(returnBeer);
                //    ReturnBeerWrapper returnBeerWrapper = new ReturnBeerWrapper(Data);
                //    OrdersClient returnBeerById = new OrdersClient(this,this,this);
                //    BaseResponse response = returnBeerById.ReturnBeerById(returnBeerWrapper, prefix, orderId, id, branchId);
                //    if (response != null && response.Status == 200)
                //    {
                //        t.Close();
                //        isCreate = true;
                //    }

                //}
            });

            AddQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if(QuantityReturn >= quantityReturn)
                {
                    NotificationMessage.Error("Vượt quá số lượng hiện tại !");
                    return;
                }
                else if (QuantityReturn >= 1)
                {
                    QuantityReturn = QuantityReturn + 1;
                }
            });
            SubQuantityCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                if(QuantityReturn > 1)
                {
                    QuantityReturn = QuantityReturn - 1;
                    if (QuantityReturn == 0)
                    {
                        QuantityReturn = 1;
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                p.Close();
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

using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.ViewModels
{
    public class DetailEmployeeRoleViewModel : BaseViewModel, ICacheService, IDeserializer, IErrorLogger
    {
        public ICommand CloseCommand { get; set; }
        private long _Id;
        public long Id { get => _Id; set { _Id = value; OnPropertyChanged("Id"); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged("Name"); } }
        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged("Status"); } }
        public DetailEmployeeRoleViewModel(EmployeeRole roleemployee)
        {
            Id = roleemployee.Id;
            Name = roleemployee.Name;
            if (roleemployee.Status == Constants.STATUS)
            {
                Status = MessageValue.MESSAGE_FROM_STATUS_MESSAGE;

            }
            else Status = MessageValue.MESSAGE_FROM_NOT_STATUS_MESSAGE;


            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }

        public T Deserialize<T>(IRestResponse response)
        {
            throw new NotImplementedException();
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

using System;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Views;
using TechresStandaloneSale.Views.NotificationView;

namespace TechresStandaloneSale.ViewModels
{
    public class CreateSlotCustomerTableViewModel : BaseViewModel
    {
        private bool _DialogHostOpen { get; set; }
        public bool DialogHostOpen { get => _DialogHostOpen; set { _DialogHostOpen = value; OnPropertyChanged("DialogHostOpen"); } }
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public bool ccc; 


        private int _SlotCustomer;
        public int SlotCustomer { get => _SlotCustomer; set { _SlotCustomer = value; OnPropertyChanged("SlotCustomer"); } }

        public int Slot { get; set; }
        public bool IsDone; 
        public CreateSlotCustomerTableViewModel(int customerSlot)
        {
            IsDone = false; 
            SlotCustomer = customerSlot;
            AddCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {   
                    if (SlotCustomer >= 0)
                    {
                        Slot = SlotCustomer;
                    IsDone = true; 
                        t.Close();
                    }
                    else
                    {
                        ErrorNotificationWindow errorNotification = new ErrorNotificationWindow(MessageValue.MESSAGE_NOTIFICATION_ERROR_SLOT_CUSTOMER);
                        errorNotification.Show();
                    }
            });

            CloseCommand = new RelayCommand<Window>((t) => { return true; }, t =>
            {
                t.Close();
            });

        }
    }
}

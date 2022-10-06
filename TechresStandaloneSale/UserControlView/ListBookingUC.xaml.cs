using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for ListBookingUC.xaml
    /// </summary>
    public partial class ListBookingUC : UserControl
    {
        public ListBookingUC()
        {
            InitializeComponent();
        }
      
        private bool BookingFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var booking = (Booking)item;
            return (booking.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.TableString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.CustomerName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.CustomerPhone.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.Employee.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || booking.Employee.Prefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                  || booking.Employee.NormalizeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.BookingTime.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || booking.FoodString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private void ListBookingUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (lvBooking.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvBooking.ItemsSource).Filter = BookingFilter;
            }
        }
        private void dateTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.dateTime.IsDropDownOpen = true;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (lvBooking.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvBooking.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvBooking.ItemsSource).Filter = BookingFilter;
            }
        }

        private void toDate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.toDate.IsDropDownOpen = true;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for OrderListUserControlDetailAdmin.xaml
    /// </summary>
    public partial class OrderListUserControlDetailAdmin : UserControl
    {
        public OrderListUserControlDetailAdmin()
        {
            InitializeComponent();
            this.dateTime.SelectedDate = DateTime.Now;
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lvOrder.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvOrder.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvOrder.ItemsSource).Filter = OrderFilter;
            }

        }
        private bool OrderFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var order = (Order)item;
            return (order.OrderCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || order.TableName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || order.AmountString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || order.Vat.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || order.DiscountPercent.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || order.TotalAmountString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                  || order.CreatedAt.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                  || order.UpdatedAt.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                   || order.OrderStatusString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                   || order.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void OrderListUserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (lvOrder.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvOrder.ItemsSource).Filter = OrderFilter;
            }
        }
        private void dateTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.dateTime.IsDropDownOpen = true;
        }

        private void toDate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.toDate.IsDropDownOpen = true;
        }
    }
}

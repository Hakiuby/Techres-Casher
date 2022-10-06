using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for HistoryInputMoneyCustomerUserControl.xaml
    /// </summary>
    public partial class HistoryInputMoneyCustomerUserControl : UserControl
    {
        public HistoryInputMoneyCustomerUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustomerTopUpHistoryList.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(CustomerTopUpHistoryList.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(CustomerTopUpHistoryList.ItemsSource).Filter = CustomerTopUpHistoryFilter;
            }

        }
        private bool CustomerTopUpHistoryFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var Customer = (CustomerTopUpHistory)item;
            return (Customer.CustomerName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void ToDate_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ToDate.IsDropDownOpen)
            {
                ToDate.IsDropDownOpen = false;
            }
            else
            {
                ToDate.IsDropDownOpen = true;
            }
        }

        private void FromDate_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (FromDate.IsDropDownOpen)
            {
                FromDate.IsDropDownOpen = false;
            }
            else
            {
                FromDate.IsDropDownOpen = true;
            }
        }
    }
}

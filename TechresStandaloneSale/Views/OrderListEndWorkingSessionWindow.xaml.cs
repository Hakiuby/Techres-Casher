using System;
using System.Windows;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for OrderListEndWorkingSessionWindow.xaml
    /// </summary>
    public partial class OrderListEndWorkingSessionWindow : Window
    {
        public OrderListEndWorkingSessionWindow()
        {
            InitializeComponent();

        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
            var obj = (Order)item;
            return (obj.TableName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                       || obj.OrderCode.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        public void SalaryRankListLoaded(object sender, RoutedEventArgs e)
        {
            if (lvOrder.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvOrder.ItemsSource).Filter = OrderFilter;
            }
        }
    }
}

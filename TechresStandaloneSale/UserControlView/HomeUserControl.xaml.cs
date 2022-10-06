using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        private int isCheckButton = 0;
        public HomeUserControl()
        {
            InitializeComponent();
            //this.DataContext = new HomeViewModel();
        }
        private CardOrderUserControl items;
        private OrderDoneUserControl itemDones;
        private OrderOnlineUserControl itemOrderOnline;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCheckButton == 0)
            {
                items = (CardOrderUserControl)this.ContentOrder.Content;
            }
            else if (isCheckButton == 1)
            {
                itemDones = (OrderDoneUserControl)this.ContentOrder.Content;
            }
            else if (isCheckButton ==3)
            {
                itemOrderOnline = (OrderOnlineUserControl)this.ContentOrder.Content;
            }
            if (items != null)
                {
                    if (items.TblItemsControl.ItemsSource != null)
                    {
                        CollectionViewSource.GetDefaultView(items.TblItemsControl.ItemsSource).Refresh();
                        CollectionViewSource.GetDefaultView(items.TblItemsControl.ItemsSource).Filter = TableFilter;
                    }
                }
                if (itemDones != null)
                {
                    if (itemDones.lvOrderDone.ItemsSource != null)
                    {
                        CollectionViewSource.GetDefaultView(itemDones.lvOrderDone.ItemsSource).Filter = OrderFilter;
                    }
                }
            if (itemOrderOnline != null)
            {
                if (itemOrderOnline.lvOrder.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(itemOrderOnline.lvOrder.ItemsSource).Filter = OrderOnlineFilter;
                }
            }
        }

        private bool TableFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchText.Text))
            {
                return true;
            }
            else
            {
                var order = (CardOrderItem)item;
                return (order.TableName.IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                    || order.OrderCode.IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                    || order.OrderId.ToString().IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }
        private bool OrderFilter(object itemDone)
        {
            if (String.IsNullOrEmpty(SearchText.Text))
            {
                return true;
            }
            else
            {
                var order = (Order)itemDone;

                return (order.Id.ToString().IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (order.TableName.ToString()).IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (order.TotalAmountString.IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)); // Dat
            }
        }
        private bool OrderOnlineFilter(object itemDone)
        {
            if (String.IsNullOrEmpty(SearchText.Text))
                return true;
            var order = (Order)itemDone;

            return (order.Customer.Name.ToString().IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                (order.Customer.Phone.ToString()).IndexOf(SearchText.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCheckButton = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            isCheckButton = 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            isCheckButton = 2;
        }

        private void SearchText_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (isCheckButton == 0)
            {
                items = (CardOrderUserControl)this.ContentOrder.Content;
            }
            else if (isCheckButton == 1)
            {
                itemDones = (OrderDoneUserControl)this.ContentOrder.Content;
            }
            else if (isCheckButton == 3)
            {
                itemOrderOnline = (OrderOnlineUserControl)this.ContentOrder.Content;
            }
            if (items != null)
            {
                if (items.TblItemsControl.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(items.TblItemsControl.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(items.TblItemsControl.ItemsSource).Filter = TableFilter;
                }
            }
            if (itemDones != null)
            {
                if (itemDones.lvOrderDone.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(itemDones.lvOrderDone.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(itemDones.lvOrderDone.ItemsSource).Filter = OrderFilter;
                }
            }
            if (itemOrderOnline != null)
            {
                if (itemOrderOnline.lvOrder.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(itemOrderOnline.lvOrder.ItemsSource).Refresh();
                    CollectionViewSource.GetDefaultView(itemOrderOnline.lvOrder.ItemsSource).Filter = OrderOnlineFilter;
                }
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isCheckButton = 3;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}

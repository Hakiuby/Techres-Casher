using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.UserControlView.CreateOrder;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for OrderFoodBookingWindow.xaml
    /// </summary>
    public partial class OrderFoodBookingWindow : Window
    {

        public OrderFoodBookingWindow()
        {
            InitializeComponent();

        }

        private void CreateOrder_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.lvFoodPending.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Filter = foodFilter;
            }
          
        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Filter = foodFilter;
        }


        private bool foodFilter(object item)
        {
            if (String.IsNullOrEmpty(CommentTextBox.Text))
                return true;
            var food = (Food)item;
            return (food.Name.IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                (food.Prefix.ToString()).IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                || (food.NormalizeName.ToString()).IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }


    }
}

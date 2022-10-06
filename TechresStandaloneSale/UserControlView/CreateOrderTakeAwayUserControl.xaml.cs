using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CreateOrderTakeAwayUserControl.xaml
    /// </summary>
    public partial class CreateOrderTakeAwayUserControl : UserControl
    {
        public CreateOrderTakeAwayUserControl()
        {
            InitializeComponent();
            this.Focus();
        }

        private void createOrderTakeAway_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.TblItemsControl.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(this.TblItemsControl.ItemsSource).Filter = foodFilter;
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //TextBox t = (TextBox)sender;
            //int index = t.Text.IndexOf(" ");
            //while (index != -1)
            //{
            //    t.Text = t.Text.Replace(" ", "");
            //    index = t.Text.IndexOf(" ");
            //}
            //t.SelectionStart = t.Text.Length;
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool foodFilter(object item)
        {
            if (String.IsNullOrEmpty(CommentTextBox.Text))
                return true;
            var food = (FoodMenuItem)item;

            return (food.ContentFoodName.IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    food.NormalizeName.IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    food.Prefix.IndexOf(CommentTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void ResearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Refresh();
        }

        private void shippingFee_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        //private void txtNameCustomer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex(@"^[0-9äüöÄÖÜß@!#'\-.:,; ^""§$%&/()=?\\}\][{³²°*+~'_<>|\n]+$");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        // Dat
        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int index = t.Text.IndexOf(" ");
            while (index != -1)
            {
                t.Text = t.Text.Replace(" ", "");
                index = t.Text.IndexOf(" ");
            }
            t.SelectionStart = t.Text.Length;
        }
        // Dat
        private void txtNameCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int index = t.Text.IndexOf("  ");
            while (index != -1)
            {
                t.Text = t.Text.Replace("  ", " ");
                index = t.Text.IndexOf("  ");
            }
            t.SelectionStart = t.Text.Length;
        }

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region ĐẠT
            TextBox t = (TextBox)sender;
            int index = t.Text.IndexOf(" ");
            while (index != -1)
            {
                t.Text = t.Text.Replace(" ", "");
                index = t.Text.IndexOf(" ");
            }
            t.SelectionStart = t.Text.Length;
            //if (t.Text == "0")
            //{
            //    t.Text = "1";
            //}
            #endregion
        }

        private void quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void quantitySellByWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var ue = e.Source as TextBox;
            Regex regex;
            if (ue.Text.Contains("."))
            {
                regex = new Regex("[^0-9]+");
            }
            else
            {
                regex = new Regex("[^0-9.]+");
            }

            e.Handled = regex.IsMatch(e.Text);
        }
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }


    }
}

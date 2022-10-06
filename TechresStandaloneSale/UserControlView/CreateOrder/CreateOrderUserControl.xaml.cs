using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.CreateOrder
{
    /// <summary>
    /// Interaction logic for CreateOrderUserControl.xaml
    /// </summary>
    public partial class CreateOrderUserControl : UserControl
    {
        public CreateOrderUserControl()
        {
            InitializeComponent();
            this.Focus();
        }

        private void CreateOrder_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.TblItemsControl.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(this.TblItemsControl.ItemsSource).Filter = foodFilter;
            }
            if (this.lvFoodPending.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Filter = FoodOrderFilter;
            }
        }
        private double ImageWidth
        {
            get
            {
                return this.TblItemsControl.ActualWidth / 4;
            }
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
        private bool FoodOrderFilter(object item)
        {
            if (String.IsNullOrEmpty(ResearchTB.Text))
                return true;
            var food = (BillResponse)item;

            return (food.Name.IndexOf(ResearchTB.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                food.Prefix.IndexOf(ResearchTB.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                food.NormalizeName.IndexOf(ResearchTB.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                food.Quantity.ToString().IndexOf(ResearchTB.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                convertToUnSign3(food.Name).ToString().IndexOf(ResearchTB.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void ResearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(this.lvFoodPending.ItemsSource).Filter = FoodOrderFilter;
        }

        private void txtQuantity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtQuantitySellByWeight_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
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
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var bc = new System.Windows.Media.BrushConverter();
        //    this.Foreground= new SolidColorBrush(Colors.White);
        //    this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Constants.MAIN_COLOR_SYSTEM));
        //}
        //private

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

        private void lvFoodPending_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ImageFood_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/Images/logo.png", UriKind.Relative));
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}

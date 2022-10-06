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
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView.ReportFood
{
    /// <summary>
    /// Interaction logic for ReportFoodUserControl.xaml
    /// </summary>
    public partial class ReportFoodUserControl : UserControl
    {
        public ReportFoodUserControl()
        {
            InitializeComponent();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lvListFood.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvListFood.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvListFood.ItemsSource).Filter = FoodFilter;
            }
        }

        private void reportFoodUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (lvListFood.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvListFood.ItemsSource).Filter = FoodFilter;
            }
        }
        private bool FoodFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var f = (ReportFoodItem)item;
            return (f.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || f.Number.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || f.Quantity.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}

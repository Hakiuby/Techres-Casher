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

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for ExpenseCashierUserControl.xaml
    /// </summary>
    public partial class ExpenseCashierUserControl : UserControl
    {
        public ExpenseCashierUserControl()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListAdditionFee.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ListAdditionFee.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(ListAdditionFee.ItemsSource).Filter = AdditionFeeFilter;
            }
        }
        private bool AdditionFeeFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var itemFilter = (AdditionFee)item;
            //if (itemFilter.ReasonName != null)
            //{
                return (itemFilter.Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                     || itemFilter.ObjectType.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                         || itemFilter.ObjectName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //}
            //return true; 
        }

        private void expenseCashierUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListAdditionFee.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ListAdditionFee.ItemsSource).Filter = AdditionFeeFilter;
            }
        }



        private void ToDate_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToDate.IsDropDownOpen = true;
        }

        private void dateTime_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dateTime.IsDropDownOpen = true;
        }


    }
}

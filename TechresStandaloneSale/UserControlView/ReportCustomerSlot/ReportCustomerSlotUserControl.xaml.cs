
using System;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.ReportCustomerSlot
{
    /// <summary>
    /// Interaction logic for ReportCustomerSlotUserControl.xaml
    /// </summary>
    public partial class ReportCustomerSlotUserControl : UserControl
    {
        public ReportCustomerSlotUserControl()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lvDiscount.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvDiscount.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvDiscount.ItemsSource).Filter = ReportOderFilter;
            }
        }
        private bool ReportOderFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var oder = (Chart)item;
            return (oder.CreatedAt.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || oder.ValueString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void reportCustomerSlot_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            if (lvDiscount.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvDiscount.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvDiscount.ItemsSource).Filter = ReportOderFilter;
            }
        }
    }
}

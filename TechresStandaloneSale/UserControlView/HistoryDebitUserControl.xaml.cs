using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for HistoryDebitUserControl.xaml
    /// </summary>
    public partial class HistoryDebitUserControl : UserControl
    {
        public HistoryDebitUserControl()
        {
            InitializeComponent();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (historyDebit.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(historyDebit.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(historyDebit.ItemsSource).Filter = HistoryEndWorkingSessionFilter;
            }

        }
        private bool HistoryEndWorkingSessionFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var debit = (Debit)item;
            return (debit.Employee.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || debit.OrderId.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || debit.Employee.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || debit.Employee.Prefix.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                  || debit.Employee.NormalizeName.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || debit.DebtTime.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (historyDebit.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(historyDebit.ItemsSource).Filter = HistoryEndWorkingSessionFilter;
            }
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

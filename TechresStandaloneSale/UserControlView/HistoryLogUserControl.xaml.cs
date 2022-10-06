using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for HistoryLogUserControl.xaml
    /// </summary>
    public partial class HistoryLogUserControl : UserControl
    {
        public HistoryLogUserControl()
        {
            InitializeComponent();
        }

        private void FoodWaitingConfirmFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ActivityLogListView.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ActivityLogListView.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(ActivityLogListView.ItemsSource).Filter = ActivityLogFilter;
            }

        }

        private void LogUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ActivityLogListView.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(this.ActivityLogListView.ItemsSource).Filter = ActivityLogFilter;
            }

        }
        private bool ActivityLogFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var ac = (ActivityLog)item;

            return ((ac.ActionDetail.ToString()).IndexOf(txtFilter.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                || (ac.ActionType.ToString()).IndexOf(txtFilter.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                || (ac.EmployeeId.ToString()).IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                ||(ac.ObjectId.ToString()).IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || (ac.CreatedAt.ToString()).IndexOf(txtFilter.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
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

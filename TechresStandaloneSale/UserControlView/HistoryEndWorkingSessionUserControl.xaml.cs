using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for HistoryEndWorkingSessionUserControl.xaml
    /// </summary>
    public partial class HistoryEndWorkingSessionUserControl : UserControl
    {
        public HistoryEndWorkingSessionUserControl()
        {
            InitializeComponent();
            // this.dateTime.SelectedDate = DateTime.Now;
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (historyEndWorkingSession.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(historyEndWorkingSession.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(historyEndWorkingSession.ItemsSource).Filter = HistoryEndWorkingSessionFilter;
            }

        }
        private bool HistoryEndWorkingSessionFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var history = (RevenueFinishWorkingSession)item;
            if (history.CloseEmployeeNormalizeName != null && history.OpenEmployeePrefix != null && history.CloseEmployeeName != null && history.OpenEmployeeName != null && history.CloseEmployeePrefix != null)
            {
                return (history.CloseEmployeeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || history.OpenEmployeeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                      || history.OpenEmployeeNormalizeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                        || history.OpenEmployeePrefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                          || history.CloseEmployeeNormalizeName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                            || history.CloseEmployeePrefix.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || history.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || history.Code.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return true; 
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (historyEndWorkingSession.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(historyEndWorkingSession.ItemsSource).Filter = HistoryEndWorkingSessionFilter;
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

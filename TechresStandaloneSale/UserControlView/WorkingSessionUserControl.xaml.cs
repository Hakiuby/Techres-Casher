using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for WorkingSessionUserControl.xaml
    /// </summary>
    public partial class WorkingSessionUserControl : UserControl
    {
        public WorkingSessionUserControl()
        {
            InitializeComponent();
        }
        private void WorkingSessionUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (WorkingSessionList.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(WorkingSessionList.ItemsSource).Filter = WorkingSessionFilter;
            }
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WorkingSessionList.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(WorkingSessionList.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(WorkingSessionList.ItemsSource).Filter = WorkingSessionFilter;
            }

        }
        private bool WorkingSessionFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var working = (WorkingSession)item;
            return (working.Interval.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || working.Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                  || working.FromHour.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || working.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                     || working.NameTime.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                      || working.ToHour.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
              );
        }
    }
}

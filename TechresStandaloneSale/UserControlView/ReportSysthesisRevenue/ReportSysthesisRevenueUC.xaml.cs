

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView.ReportSysthesisRevenue
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ReportSysthesisRevenueUC : UserControl
    {
        public ReportSysthesisRevenueUC()
        {
            InitializeComponent();
        }
        private void reportSysthensisRevenueUC_Loaded(object sender, RoutedEventArgs e)
        {
            if (lvRevenueSysthesisReport.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvRevenueSysthesisReport.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvRevenueSysthesisReport.ItemsSource).Filter = ReportRevenueSysthesisFilter;
            }
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lvRevenueSysthesisReport.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvRevenueSysthesisReport.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvRevenueSysthesisReport.ItemsSource).Filter = ReportRevenueSysthesisFilter;
            }
        }

        private bool ReportRevenueSysthesisFilter(object obj)
        {
            return true;
            //if (string.IsNullOrEmpty(txtFilter.Text))
            //    return true;
            //var revenue = (RevenueSysthesisReportData)obj;
            //return revenue.TotalRevenueFormart.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
            //    || revenue.CashAmountFormart.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
            //    || revenue.TotalOrder.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
            //       || revenue.TotalSaleRevenueFormart.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;

        }
    }
}

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
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView.ReportRevenue
{
    /// <summary>
    /// Interaction logic for ReportRevenueUserControl.xaml
    /// </summary>
    public partial class ReportRevenueUserControl : UserControl
    {
        private ReportRevenueButtonEnum ButtonReport;
        public ReportRevenueUserControl()
        {
            InitializeComponent();
            ButtonReport = ReportRevenueButtonEnum.REVENUE;
        }
       
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (ButtonReport)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                        reportSysthesisRevenueUC = (ReportSysthesisRevenue.ReportSysthesisRevenueUC)this.ReportControl.Content;
                        if (reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource).Filter = FilterREVENUE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        reportRevenuePaymentMethodUC = (ReportRevenuePaymentMethod.ReportRevenuePaymentMethodUC)this.ReportControl.Content;
                        if (reportRevenuePaymentMethodUC.lvRevenueRankEmployee.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenuePaymentMethodUC.lvRevenueRankEmployee.ItemsSource).Refresh();

                            CollectionViewSource.GetDefaultView(reportRevenuePaymentMethodUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterPAYMENT_METHOD;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        reportRevenueRankEmployeeUC = (ReportRevenueRankEmployee.ReportRevenueRankEmployeeUC)this.ReportControl.Content;
                        if (reportRevenueRankEmployeeUC.lvRevenueRankEmployee.ItemsSource != null)
                        {     
                            CollectionViewSource.GetDefaultView(reportRevenueRankEmployeeUC.lvRevenueRankEmployee.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(reportRevenueRankEmployeeUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterRANK_EMPLOYEE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        reportRevenueAreaUC = (ReportRevenueArea.ReportRevenueAreaUC)this.ReportControl.Content;
                        if (reportRevenueAreaUC.lvRevenueAreaReport.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenueAreaUC.lvRevenueAreaReport.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(reportRevenueAreaUC.lvRevenueAreaReport.ItemsSource).Filter = FilterAREA;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        reportVATUC = (ReportVAT.ReportVATUC)this.ReportControl.Content;
                        if (reportVATUC.lvRevenueReportCashier.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportVATUC.lvRevenueReportCashier.ItemsSource).Refresh();
                           CollectionViewSource.GetDefaultView(reportVATUC.lvRevenueReportCashier.ItemsSource).Filter = FilterREVENUE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        reportDiscountUC = (ReportDiscount.ReportDiscountUC)this.ReportControl.Content;
                        if (reportDiscountUC.lvDiscount.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportDiscountUC.lvDiscount.ItemsSource).Refresh();
                               CollectionViewSource.GetDefaultView(reportDiscountUC.lvDiscount.ItemsSource).Filter = FilterDISCOUNT;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        reportOrderUC = (ReportOrder.ReportOrderUC)this.ReportControl.Content;
                        if (reportOrderUC.lvListOrder.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportOrderUC.lvListOrder.ItemsSource).Refresh();
                           CollectionViewSource.GetDefaultView(reportOrderUC.lvListOrder.ItemsSource).Filter = FilterORDER;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        reportRevenueOrderMethodUC = (ReportRevenueOrderMethod.ReportRevenueOrderMethodUC)this.ReportControl.Content;
                        if (reportRevenueOrderMethodUC.lvRevenueRankEmployee.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenueOrderMethodUC.lvRevenueRankEmployee.ItemsSource).Refresh();
                           CollectionViewSource.GetDefaultView(reportRevenueOrderMethodUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterORDER_METHOD;
                        }
                        break;
                    }
            }
        }
        private bool FilterREVENUE(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (RevenueSysthesisReportData)item;
            return (employee.TotalRevenueFormart.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.CashAmountFormart.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                   || employee.TotalOrder.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || employee.TotalSaleRevenueFormart.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        private bool FilterVAT(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (Chart)item;
            return (employee.CreatedAt.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Value.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterDISCOUNT(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (Chart)item;
            return (employee.CreatedAt.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Value.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterAREA(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (ReportRevenueAreas)item;
            return (employee.Revenue.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.BranchName.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                   || employee.BranchPhone.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || employee.AreaName.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterORDER(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (Order)item;
            return (employee.TableName.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.OrderCode.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                   || employee.Employee.Name.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterORDER_METHOD(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (FormService)item;
            return (employee.NumberOrder.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.TotalAmount.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        private bool FilterPAYMENT_METHOD(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (PaymentMethodModel)item;
            return (employee.Unit.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Method.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        private bool FilterRANK_EMPLOYEE(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var employee = (EmployeeMainAdmin)item;
            return (employee.Revenue.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Employee.Name.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Employee.RoleName.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Employee.NormalizeName.ToString().ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Branch.Name.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        ReportSysthesisRevenue.ReportSysthesisRevenueUC reportSysthesisRevenueUC;
        ReportVAT.ReportVATUC reportVATUC;
        ReportDiscount.ReportDiscountUC reportDiscountUC;
        ReportRevenueArea.ReportRevenueAreaUC reportRevenueAreaUC;
        ReportOrder.ReportOrderUC reportOrderUC;
        ReportRevenueOrderMethod.ReportRevenueOrderMethodUC reportRevenueOrderMethodUC;
        ReportRevenuePaymentMethod.ReportRevenuePaymentMethodUC reportRevenuePaymentMethodUC;
        ReportRevenueRankEmployee.ReportRevenueRankEmployeeUC reportRevenueRankEmployeeUC;
        private void reportRevenueUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (ButtonReport)
            {
                case ReportRevenueButtonEnum.REVENUE:
                    {
                        reportSysthesisRevenueUC = (ReportSysthesisRevenue.ReportSysthesisRevenueUC)this.ReportControl.Content;
                       
                        if (reportSysthesisRevenueUC!= null && reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(reportSysthesisRevenueUC.lvRevenueSysthesisReport.ItemsSource).Filter = FilterREVENUE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.PAYMENT_METHOD:
                    {
                        reportRevenuePaymentMethodUC = (ReportRevenuePaymentMethod.ReportRevenuePaymentMethodUC)this.ReportControl.Content;
                        if (reportRevenuePaymentMethodUC != null && reportRevenuePaymentMethodUC.lvRevenueRankEmployee.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenuePaymentMethodUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterPAYMENT_METHOD;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.RANK_EMPLOYEE:
                    {
                        reportRevenueRankEmployeeUC = (ReportRevenueRankEmployee.ReportRevenueRankEmployeeUC)this.ReportControl.Content;
                        if (reportRevenueRankEmployeeUC != null && reportRevenueRankEmployeeUC.lvRevenueRankEmployee.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenueRankEmployeeUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterRANK_EMPLOYEE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.AREA:
                    {
                        reportRevenueAreaUC = (ReportRevenueArea.ReportRevenueAreaUC)this.ReportControl.Content;
                        if (reportRevenueAreaUC != null && reportRevenueAreaUC.lvRevenueAreaReport.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenueAreaUC.lvRevenueAreaReport.ItemsSource).Filter = FilterAREA;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.VAT:
                    {
                        reportVATUC = (ReportVAT.ReportVATUC)this.ReportControl.Content;
                        if (reportVATUC != null && reportVATUC.lvRevenueReportCashier.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportVATUC.lvRevenueReportCashier.ItemsSource).Filter = FilterREVENUE;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.DISCOUNT:
                    {
                        reportDiscountUC = (ReportDiscount.ReportDiscountUC)this.ReportControl.Content;
                        if (reportDiscountUC != null && reportDiscountUC.lvDiscount.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportDiscountUC.lvDiscount.ItemsSource).Filter = FilterDISCOUNT;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER:
                    {
                        reportOrderUC = (ReportOrder.ReportOrderUC)this.ReportControl.Content;
                        if (reportOrderUC != null && reportOrderUC.lvListOrder.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportOrderUC.lvListOrder.ItemsSource).Filter = FilterORDER;
                        }
                        break;
                    }
                case ReportRevenueButtonEnum.ORDER_METHOD:
                    {
                        reportRevenueOrderMethodUC = (ReportRevenueOrderMethod.ReportRevenueOrderMethodUC)this.ReportControl.Content;
                        if (reportRevenueOrderMethodUC != null && reportRevenueOrderMethodUC.lvRevenueRankEmployee.ItemsSource != null)
                        {
                            CollectionViewSource.GetDefaultView(reportRevenueOrderMethodUC.lvRevenueRankEmployee.ItemsSource).Filter = FilterORDER_METHOD;
                        }
                        break;
                    }
            }
        }

        private void btnRevenue_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.REVENUE;
        }

        private void btnPaymentMethod_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.PAYMENT_METHOD;
        }

        private void btnRankEmployee_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.RANK_EMPLOYEE;
        }

        private void btnRevenueArea_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.AREA;
        }

        private void btnVAT_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.VAT;
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.DISCOUNT;
        }

        private void btnOrderMethod_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.ORDER_METHOD;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            ButtonReport = ReportRevenueButtonEnum.ORDER;
        }
    }
}

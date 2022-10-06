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
    /// Interaction logic for ListDebtCustomerUserControl.xaml
    /// </summary>
    public partial class ListDebtCustomerUserControl : UserControl
    {
        public ListDebtCustomerUserControl()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListPaymentReason.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(ListPaymentReason.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(ListPaymentReason.ItemsSource).Filter = EmployeeFilter;
            }

        }
        private bool EmployeeFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            return true;
            //var employee = (PaymentReason)item;
            //return (employee.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
            //           || employee.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}

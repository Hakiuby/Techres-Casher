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
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for EmployeeAdvancedSalaryUserControl.xaml
    /// </summary>
    public partial class EmployeeAdvancedSalaryUserControl : UserControl
    {
        public EmployeeAdvancedSalaryUserControl()
        {
            InitializeComponent();
        }

        private void employeeAdcancedSalary_Loaded(object sender, RoutedEventArgs e)
        {
            if (lvEmployeeSalaryAddition.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvEmployeeSalaryAddition.ItemsSource).Filter = Filter;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lvEmployeeSalaryAddition.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvEmployeeSalaryAddition.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvEmployeeSalaryAddition.ItemsSource).Filter = Filter;
            }
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            var booking = (EmployeeAdvancedSalary)item;
            return (booking.Id.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || booking.Employee.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || booking.Reason.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                 || booking.Employee.RoleName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}

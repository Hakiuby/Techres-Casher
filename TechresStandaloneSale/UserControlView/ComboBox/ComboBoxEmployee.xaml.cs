using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.ComboBox
{
    /// <summary>
    /// Interaction logic for ComboBoxEmployee.xaml
    /// </summary>
    public partial class ComboBoxEmployee : UserControl
    {
        public ComboBoxEmployee()
        {
            InitializeComponent();
        }

        private void comboxEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            if (PART_ItemList_Employee.ItemsSource != null)
            {
                this.PART_Popup_Employee.IsOpen = false;
                CollectionViewSource.GetDefaultView(PART_ItemList_Employee.ItemsSource).Filter = EmployeeFilter;
                if (this.PART_ItemList_Employee.ItemsSource != null)
                {
                    this.PART_ItemList_Employee.SelectedIndex = -1;
                }
            }
        }
        private void PART_ContentHost_Employee_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.PART_Popup_Employee.IsOpen = true;
            if (PART_ItemList_Employee.ItemsSource != null)
            {
                this.PART_ItemList_Employee.SelectedIndex = -1;
            }
        }
        private void PART_ContentHost_Employee_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PART_Popup_Employee.IsOpen = true;
            if (PART_ItemList_Employee.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(PART_ItemList_Employee.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(PART_ItemList_Employee.ItemsSource).Filter = EmployeeFilter;
                this.PART_ItemList_Employee.SelectedIndex = 0;
            }
        }
        private bool EmployeeFilter(object item)
        {
            if (string.IsNullOrEmpty(PART_ContentHost_Employee.Text))
                return true;
            var employee = (Employee)item;
            if(employee.Name != null)
            {
                return (employee.Name.IndexOf(PART_ContentHost_Employee.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.NormalizeName.ToString().IndexOf(PART_ContentHost_Employee.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || employee.Prefix.ToString().IndexOf(PART_ContentHost_Employee.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return true; 
            
        }
    }
}

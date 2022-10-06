using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.UserControlView.ComboBox;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ManagePaymentSlipWindow.xaml
    /// </summary>
    public partial class ManagePaymentSlipWindow : Window
    {
        public ManagePaymentSlipWindow()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void datePicker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //datePicker.IsDropDownOpen = true;
        }

        private void Amount_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(amount.Text))
            {
                int start = amount.Text.Length - amount.SelectionStart;
                amount.Text = Utils.Utils.FormatMoneyString(amount.Text);
                int number = -start + amount.Text.Length;

                if (number < 0)
                {
                    amount.SelectionStart = 1;
                }
                else
                {
                    amount.SelectionStart = number;
                }
            }
            else
            {
                amount.Text = "0";
            }
            if (decimal.Parse(amount.Text) > 1000000000)
            {
                amount.Text = "1,000,000,000";
            }

        }
        private void ManagePaymentSlipWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                if(objectType.Visibility== Visibility.Visible) // Dat
                {
                    this.objectType.Focus();
                    objectType.IsDropDownOpen = true;
                }
            }
            else if(e.Key == Key.F2)
            {
                this.ContentPersion.Focus();
                if (((BasicModel)objectType.SelectedItem).Value == (int)ExpenseTypeEnum.CUSTOMER)
                {
                    ComboBoxCustomer customer = (ComboBoxCustomer)ContentPersion.Content;
                   if (customer!= null)
                    {
                        customer.PART_ContentHost_Customer.Focus();
                        customer.PART_ContentHost_Customer.SelectAll();
                        Keyboard.Focus(customer.PART_ContentHost_Customer);
                    }
                }
                else if (((BasicModel)objectType.SelectedItem).Value == (int)ExpenseTypeEnum.SUPPLIER)
                {
                    ComboBoxSupplier customer = (ComboBoxSupplier)ContentPersion.Content;
                    if (customer != null)
                    {
                        customer.PART_ContentHost_Supplier.Focus();
                        customer.PART_ContentHost_Supplier.SelectAll();
                        Keyboard.Focus(customer.PART_ContentHost_Supplier);
                        customer.PART_Popup_Supplier.IsOpen = false;
                    }
                }
                else if (((BasicModel)objectType.SelectedItem).Value == (int)ExpenseTypeEnum.EMPLOYEE)
                {
                    ComboBoxEmployee customer = (ComboBoxEmployee)ContentPersion.Content;
                    if (customer != null)
                    {
                        customer.PART_ContentHost_Employee.Focus();
                        customer.PART_ContentHost_Employee.SelectAll();
                        Keyboard.Focus(customer.PART_ContentHost_Employee);
                        customer.PART_Popup_Employee.IsOpen = false;
                    }
                }
                else if (((BasicModel)objectType.SelectedItem).Value == (int)ExpenseTypeEnum.ORTHER)
                {
                    TextboxOther customer = (TextboxOther)ContentPersion.Content;
                    if (customer != null)
                    {
                        customer.name.Focus();
                        customer.name.SelectAll();
                        Keyboard.Focus(customer.name);
                    }
                }
            }
            else if (e.Key == Key.F6)
            {
                if (DockBranchItem.Visibility == Visibility.Visible) // Dat
                {
                    branchItem.Focus();
                    branchItem.IsDropDownOpen = true;
                }
            }
            else if (e.Key == Key.F7)
            {
                amount.Focus();
                amount.SelectAll();
                Keyboard.Focus(amount);
            }
            else if (e.Key == Key.F12)
            {
                note.Focus();
                note.SelectAll();
                Keyboard.Focus(note);
            }
            else if (e.SystemKey== Key.F10)
            {
                category.Focus();
                category.IsDropDownOpen = true;
            }
            else if (e.Key == Key.F8)
            {
                payment.Focus();
                payment.IsDropDownOpen = true;
            }
            else if (e.Key == Key.F9)
            {
                check.Focus();
                if (check.IsChecked== true)
                {
                    check.IsChecked = false;
                }
                else
                {
                    check.IsChecked = true;
                }
            }
            else if (e.Key == Key.F1)
            {
                ////checkAll.Focus();
                ////if (checkAll.IsChecked == true)
                //{
                //    checkAll.IsChecked = false;
                //}
                //else
                //{
                //    checkAll.IsChecked = true;
                //}
            }
            else if (e.Key == Key.F5)
            {
            //    materialWarehouseList.col
                //if (checkAll.IsChecked == true)
                //{
                //    checkAll.IsChecked = false;
                //}
                //else
                //{
                //    checkAll.IsChecked = true;
                //}
            }
        }

        private void amount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            #region toan
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            #endregion
        }
    }
}

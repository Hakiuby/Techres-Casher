using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.ComboBox
{
    /// <summary>
    /// Interaction logic for ComboBoxSupplier.xaml
    /// </summary>
    public partial class ComboBoxSupplier : UserControl
    {
        public ComboBoxSupplier()
        {
            InitializeComponent();
            //PART_ContentHost_Supplier.Focus();
            //PART_ContentHost_Supplier.SelectAll();
            //Keyboard.Focus(PART_ContentHost_Supplier);
        }
        private void PART_ContentHost_Supplier_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.PART_Popup_Supplier.IsOpen = true;
            if (PART_ItemList_Supplier.ItemsSource != null)
            {
                this.PART_ItemList_Supplier.SelectedIndex = 0;
            }
        }
        private void PART_ContentHost_Supplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PART_Popup_Supplier.IsOpen = true;
            if (PART_ItemList_Supplier.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Filter = SupplierFilter;
                this.PART_ItemList_Supplier.SelectedIndex = 0;
            }
        }
        private bool SupplierFilter(object item)
        {
            if (string.IsNullOrEmpty(PART_ContentHost_Supplier.Text))
                return true;
            var supplier = (Supplier)item;
            return (supplier.Name.IndexOf(PART_ContentHost_Supplier.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || supplier.NormalizeName.ToString().IndexOf(PART_ContentHost_Supplier.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || supplier.Prefix.ToString().IndexOf(PART_ContentHost_Supplier.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void comboBoxSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            if (PART_ItemList_Supplier.ItemsSource != null)
            {
                this.PART_Popup_Supplier.IsOpen = false;
                CollectionViewSource.GetDefaultView(PART_ItemList_Supplier.ItemsSource).Filter = SupplierFilter;
                if (this.PART_ItemList_Supplier.ItemsSource != null)
                {
                    this.PART_ItemList_Supplier.SelectedIndex = 0;
                }
            }
        }
    }
}

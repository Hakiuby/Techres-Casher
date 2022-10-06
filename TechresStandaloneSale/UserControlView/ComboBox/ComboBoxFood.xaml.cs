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

namespace TechresStandaloneSale.UserControlView.ComboBox
{
    /// <summary>
    /// Interaction logic for ComboBoxFood.xaml
    /// </summary>
    public partial class ComboBoxFood : UserControl
    {
        public ComboBoxFood()
        {
            InitializeComponent();
        }
        private void PART_ContentHost_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.PART_Popup.IsOpen = true;
            if (PART_ItemList.ItemsSource != null)
            {
                this.PART_ItemList.SelectedIndex = 0;
            }
        }
        private void PART_ContentHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PART_Popup.IsOpen = true;
            if (PART_ItemList.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(PART_ItemList.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(PART_ItemList.ItemsSource).Filter = SupplierFilter;
                this.PART_ItemList.SelectedIndex = 0;
            }
        }
        private bool SupplierFilter(object item)
        {
            if (string.IsNullOrEmpty(PART_ContentHost.Text))
                return true;
            var supplier = (Food)item;
            return (supplier.Name.IndexOf(PART_ContentHost.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || supplier.NormalizeName.ToString().IndexOf(PART_ContentHost.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || supplier.Prefix.ToString().IndexOf(PART_ContentHost.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private void comboBoxFood_Loaded(object sender, RoutedEventArgs e)
        {

            if (PART_ItemList.ItemsSource != null)
            {
                this.PART_Popup.IsOpen = false;
                CollectionViewSource.GetDefaultView(PART_ItemList.ItemsSource).Filter = SupplierFilter;
                if (this.PART_ItemList.ItemsSource != null)
                {
                    this.PART_ItemList.SelectedIndex = 0;
                }
            }
        }

        private void PART_ContentHost_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                this.PART_Popup.IsOpen = false;
            }
        }
    }
}

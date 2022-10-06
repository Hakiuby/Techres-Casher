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
using System.Windows.Shapes;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for HistoryPrinManagerWindow.xaml
    /// </summary>
    public partial class HistoryPrinManagerWindow : Window
    {
        public HistoryPrinManagerWindow()
        {
            InitializeComponent();
        }

        private bool TableFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                var order = (HistoryPrintData)item;
                return (order.Id.ToString().IndexOf(txtFilter.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LstTableNeedSend.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(LstTableNeedSend.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(LstTableNeedSend.ItemsSource).Filter = TableFilter;
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

    }
}

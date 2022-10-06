using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.ReportGiftFood
{
    /// <summary>
    /// Interaction logic for ReportGiftFoodUC.xaml
    /// </summary>
    public partial class ReportGiftFoodUC : UserControl
    {
        public ReportGiftFoodUC()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged (object sender, TextChangedEventArgs e)
        {
            if(lvListGiftFood.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(lvListGiftFood.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(lvListGiftFood.ItemsSource).Filter = ReportGiftFoodFilter;
            }    
        }

        private bool ReportGiftFoodFilter(object obj)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            var giftfood = (GiftFood)obj;
            return giftfood.PaymentDate.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.OrderId.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.TableName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.Employee.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.Food.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.PriceString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.Quantity.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0
                || giftfood.TotalAmountString.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}

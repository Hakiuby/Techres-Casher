using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for CreateRestaurantExtraWindow.xaml
    /// </summary>
    public partial class CreateRestaurantExtraWindow : Window
    {
        public CreateRestaurantExtraWindow()
        {
            InitializeComponent();
        }

        private void tbAmount_KeyUp(object sender, KeyEventArgs e)
        {
            //string str = tbAmount.Text;
            //str = str.Replace(",", "");
            //if (IsPhoneNumber(str))
            //{
            //    int start = tbAmount.Text.Length - tbAmount.SelectionStart;
            //    tbAmount.Text = Utils.Utils.FormatMoney(str);
            //    tbAmount.SelectionStart = -start + tbAmount.Text.Length;
            //}
            //else
            //    tbAmount.Text = "0";
            #region Dat
            tbAmount.Text = tbAmount.Text.Replace(" ","");
            int start = tbAmount.Text.Length - tbAmount.SelectionStart;
            tbAmount.Text = Utils.Utils.FormatMoneyString(tbAmount.Text);
            int number = -start + tbAmount.Text.Length;
            if (number < 0)
            {
                tbAmount.SelectionStart = 1;
            }
            else
            {
                tbAmount.SelectionStart = number;
            }
            if (decimal.Parse(tbAmount.Text) > 500000000)
            {
                tbAmount.Text = "500,000,000";
            }
            //tbAmount.SelectionStart = -start + tbAmount.Text.Length;
            #endregion
        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^[-+]?[0-9]*\.?[0-9]+$");
        }

        private void tbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

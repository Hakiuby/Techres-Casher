using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for CreateFoodOtherWindow.xaml
    /// </summary>
    public partial class CreateFoodOtherWindow : Window
    {

        public CreateFoodOtherWindow()
        {
            InitializeComponent();
        }
        private void tbPrince_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //string str = tbPrince.Text;
            //str = str.Replace(",", "");
            //if (IsPhoneNumber(str))
            //{
            //    int start = tbPrince.Text.Length - tbPrince.SelectionStart;
            //    tbPrince.Text = Utils.Utils.FormatMoney(str);
            //    tbPrince.SelectionStart = -start + tbPrince.Text.Length;
            //}
            //else
            //    tbPrince.Text = "0";
            ////return;
            #region Dat
            int start = tbPrince.Text.Length - tbPrince.SelectionStart;
            tbPrince.Text = Utils.Utils.FormatMoneyString(tbPrince.Text);
            int number = -start + tbPrince.Text.Length;
            if(number < 0)
            {
                tbPrince.SelectionStart = 1;
            }
            else
            {
                tbPrince.SelectionStart = number;
            }
            if (decimal.Parse(tbPrince.Text) > 500000000)
            {
                tbPrince.Text = "500,000,000";
            }
            //tbPrince.SelectionStart = -start + tbPrince.Text.Length;
            #endregion
        }
        private void tbAmount_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string str = tbAmount.Text;
            str = str.Replace(",", "");
            if (IsPhoneNumber(str))
            {
                int start = tbAmount.Text.Length - tbAmount.SelectionStart;
                tbAmount.Text = Utils.Utils.FormatMoney(str);
                tbAmount.SelectionStart = -start + tbAmount.Text.Length;
            }
            else
                tbAmount.Text ="0";
        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^[-+]?[0-9]*\.?[0-9]+$");
        }

        private void tbPrince_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbPrince_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;
            }    
        }

        private void tbQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbQuantity.Text == "0")
            {
                tbQuantity.Text = "1";
            }    
        }
    }
}

using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ReceviceDepositBookingWindow.xaml
    /// </summary>
    public partial class ReceviceDepositBookingWindow : Window
    {
        public ReceviceDepositBookingWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TbPrince_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region ĐẠT
            TextBox t = (TextBox)sender;
            int index = t.Text.IndexOf(" ");
            while (index != -1)
            {
                t.Text = t.Text.Replace(" ", "");
                index = t.Text.IndexOf(" ");
            }
            t.SelectionStart = t.Text.Length;
            #endregion
            if (!string.IsNullOrEmpty(tbPrince.Text))
            {
                //if (Utils.Utils.CheckNumberFormat(tbPrince.Text))
                //{
                //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                //float valueBefore = float.Parse(tbPrince.Text, System.Globalization.NumberStyles.AllowThousands);
                //tbPrince.Text = String.Format(culture, "{0:N0}", valueBefore);
                //tbPrince.Select(tbPrince.Text.Length, 0);
                #region toan
                int start = tbPrince.Text.Length - tbPrince.SelectionStart;
                    tbPrince.Text = Utils.Utils.FormatMoneyString(tbPrince.Text);
                    int number = -start + tbPrince.Text.Length;

                    if (number < 0)
                    {
                        tbPrince.SelectionStart = 1;
                    }
                    else
                    {
                        tbPrince.SelectionStart = number;
                    }
                #endregion
                //}
            }
            else
            {
                tbPrince.Text = "0";
            }
            if (decimal.Parse(tbPrince.Text) > 1000000000)
            {
                tbPrince.Text = "1,000,000,000";
            }
        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^(\+[0-9])$");
        }

        private void tbPrince_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

      
    }
}

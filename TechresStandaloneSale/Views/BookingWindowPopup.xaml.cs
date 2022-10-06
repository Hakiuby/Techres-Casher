using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for BookingWindowPopup.xaml
    /// </summary>
    public partial class BookingWindowPopup : Window
    {
        public BookingWindowPopup()
        {
            InitializeComponent();
        }

        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^(\+[0-9])$");
        }
        private void TbPrince_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region Đạt
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
                //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                //    float valueBefore = float.Parse(tbPrince.Text, System.Globalization.NumberStyles.AllowThousands);
                //    tbPrince.Text = String.Format(culture, "{0:N0}", valueBefore);
                //    tbPrince.Select(tbPrince.Text.Length, 0);
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
                // }
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
        private void BookingTime_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (BookingTime.IsDropDownOpen)
            {
                BookingTime.IsDropDownOpen = false;

            }
            else
            {
                BookingTime.IsDropDownOpen = true;

            }
        }

        private void tbPrince_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void QuantityPerson_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void QuantityPerson_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

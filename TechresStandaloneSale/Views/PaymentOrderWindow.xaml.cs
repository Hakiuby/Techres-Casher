using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for PaymentOrderWindow.xaml
    /// </summary>
    public partial class PaymentOrderWindow : Window
    {
        public PaymentOrderWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox t = (TextBox)sender;
            //int index = t.Text.IndexOf(" ");
            //while (index != -1)
            //{
            //    t.Text = t.Text.Replace(" ", "");
            //    index = t.Text.IndexOf(" ");
            //}

            //string str = TxtInputMoney.Text;
            //if (!string.IsNullOrEmpty(str))
            //{
            //    int start = TxtInputMoney.Text.Length - TxtInputMoney.SelectionStart;
            //    str = str.Replace(",", "");
            //    TxtInputMoney.Text = Utils.Utils.FormatMoney(str);
            //    TxtInputMoney.SelectionStart = -start + TxtInputMoney.Text.Length;
            //}

            //t.SelectionStart = t.Text.Length;
            //if (!string.IsNullOrEmpty(TxtInputMoney.Text))
            //{
            //    if (Utils.Utils.CheckNumberFormat(TxtInputMoney.Text))
            //    {
            //        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            //        float valueBefore = float.Parse(TxtInputMoney.Text, System.Globalization.NumberStyles.AllowThousands);
            //        TxtInputMoney.Text = String.Format(culture, "{0:N0}", valueBefore);
            //        TxtInputMoney.Select(TxtInputMoney.Text.Length, 0);
            //    }
            //}
        }
        private void amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox t = (TextBox)sender;
            //int index = t.Text.IndexOf(" ");
            //while (index != -1)
            //{
            //    t.Text = t.Text.Replace(" ", "");
            //    index = t.Text.IndexOf(" ");
            //}
            //t.SelectionStart = t.Text.Length;
            //if (!string.IsNullOrEmpty(amount.Text))
            //{
            //    if (Utils.Utils.CheckNumberFormat(amount.Text))
            //    {
            //        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            //        float valueBefore = float.Parse(amount.Text, System.Globalization.NumberStyles.AllowThousands);
            //        amount.Text = String.Format(culture, "{0:N0}", valueBefore);
            //        amount.Select(amount.Text.Length, 0);
            //    }
            //}
        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^(\+[0-9])$");
        }
        private void Amount_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrEmpty(amount.Text))
            //{
            //    string str = amount.Text;
            //    int start = amount.Text.Length - amount.SelectionStart;
            //    str = str.Replace(",", "");
            //    amount.Text = Utils.Utils.FormatMoney(str);
            //    amount.SelectionStart = -start + amount.Text.Length;
            //}

        }

        private void TxtInputMoney_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtInputMoney.Text))
            {
                #region Đạt
                int start = TxtInputMoney.Text.Length - TxtInputMoney.SelectionStart;
                TxtInputMoney.Text = Utils.Utils.FormatMoneyString(TxtInputMoney.Text);
                int number = -start + TxtInputMoney.Text.Length;
                if (number < 0)
                {
                    TxtInputMoney.SelectionStart = 1;
                }
                else
                {
                    TxtInputMoney.SelectionStart = number;
                }
            }
            else
            {
                TxtInputMoney.Text = "0";
            }
            if (decimal.Parse(TxtInputMoney.Text) > 1000000000)
            {
                TxtInputMoney.Text = "1,000,000,000";
            }
            //TxtInputMoney.SelectionStart = -start + TxtInputMoney.Text.Length;
            #endregion
        }

        private void TxtInputMoney_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TxtInputMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void amount_PreviewKeyUp(object sender, KeyEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(amount.Text))
        //    {
        //        #region Đạt
        //        int start = amount.Text.Length - amount.SelectionStart;
        //        amount.Text = Utils.Utils.FormatMoneyString(amount.Text);
        //        int number = -start + amount.Text.Length;
        //        if (number < 0)
        //        {
        //            amount.SelectionStart = 1;
        //        }
        //        else
        //        {
        //            amount.SelectionStart = number;
        //        }
        //    }
        //    else
        //    {
        //        amount.Text = "0";
        //    }
        //    //amount.SelectionStart = -start + amount.Text.Length;
        //    #endregion
        //}

        private void amount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

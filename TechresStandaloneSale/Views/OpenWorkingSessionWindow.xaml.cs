using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for OpenWorkingSessionWindow.xaml
    /// </summary>
    public partial class OpenWorkingSessionWindow : Window
    {
        public OpenWorkingSessionWindow()
        {
            InitializeComponent();
            TxtInputMoney.Focus();
            //TxtInputMoney.SelectAll();
            //Keyboard.Focus(TxtInputMoney);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (IsPhoneNumber(TxtInputMoney.Text) == true)
            //{
            //    if (!string.IsNullOrEmpty(TxtInputMoney.Text))
            //    {
            //        if (Utils.Utils.CheckNumberFormat(TxtInputMoney.Text))
            //        {
            //            if (IsPhoneNumber(TxtInputMoney.Text) == true)
            //            {
            //                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            //                float valueBefore = float.Parse(TxtInputMoney.Text.Trim(','), System.Globalization.NumberStyles.AllowThousands);
            //                TxtInputMoney.Text = String.Format(culture, "{0:N0}", valueBefore);
            //                TxtInputMoney.Select(TxtInputMoney.Text.Length, 0);
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    TxtInputMoney.Text = "0";
            //}
            #region Đạt
            TextBox t = (TextBox)sender;
            int index = t.Text.IndexOf(" ");
            while (index != -1)
            {
                t.Text = t.Text.Replace(" ", "");
                index = t.Text.IndexOf(" ");
            }
            t.SelectionStart = t.Text.Length;
            if (!string.IsNullOrEmpty(TxtInputMoney.Text))
            {

                //if (Utils.Utils.CheckNumberFormat(TxtInputMoney.Text))
                //{
                //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                //    float valueBefore = float.Parse(TxtInputMoney.Text, System.Globalization.NumberStyles.AllowThousands);
                //    TxtInputMoney.Text = String.Format(culture, "{0:N0}", valueBefore);
                //    TxtInputMoney.Select(TxtInputMoney.Text.Length, 0);

                #region toan
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

                #endregion
            }
            else
            {
                TxtInputMoney.Text = "0";
            }
            #endregion

        }
        public bool IsPhoneNumber(string telNo)
        {
            return Regex.IsMatch(telNo, @"^(\+[0-9])$");
        }

        private void TxtInputMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void openWorkingSessionWindow_Closing(object sender, CancelEventArgs e)
        //{

        //}
    }
}

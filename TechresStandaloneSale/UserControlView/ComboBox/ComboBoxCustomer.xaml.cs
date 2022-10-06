using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TechresStandaloneSale.Models;

namespace TechresStandaloneSale.UserControlView.ComboBox
{
    /// <summary>
    /// Interaction logic for ComboBoxCustomer.xaml
    /// </summary>
    public partial class ComboBoxCustomer : UserControl
    {
        public ComboBoxCustomer()
        {
            InitializeComponent();
        }
        private void PART_ContentHost_Customer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PART_ContentHost_Customer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

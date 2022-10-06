using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;


namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for CreateDiscountWindow.xaml
    /// </summary>
    public partial class CreateDiscountWindow : Window
    {
        public CreateDiscountWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbPrince_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbPrince_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbPrince_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(tbPrince.Text, out box_int);
            if(box_int > 100)
            {
                tbPrince.Text = "100";
            }
            tbPrince.SelectionStart = tbPrince.Text.Length;
        }
    }
}

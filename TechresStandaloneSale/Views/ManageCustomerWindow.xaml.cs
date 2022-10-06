using GalaSoft.MvvmLight.Messaging;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for Add_customer.xaml
    /// </summary>
    public partial class ManageCustomerWindow : Window
    {
        public ManageCustomerWindow()
        {
            InitializeComponent();

        }

        private void phone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        // Dat
        private void phone_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for CreateSlotCustomerTableWindow.xaml
    /// </summary>
    public partial class CreateSlotCustomerTableWindow : Window
    {
        public CreateSlotCustomerTableWindow()
        {
            InitializeComponent();
        }
        int slot = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            slot = 2;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            slot = 3;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            slot = 4;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            slot = 5;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            slot = 6;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            slot = 7;
            this.TBSlotNumber.Text = slot.ToString();
        }

        private void TBSlotNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
           
        }

        private void TBSlotNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }
    }
}

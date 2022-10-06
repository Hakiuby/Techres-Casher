using System.Windows;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ReturnBookingListEndWorkingSessionWindow.xaml
    /// </summary>
    public partial class ReturnBookingListEndWorkingSessionWindow : Window
    {
        public ReturnBookingListEndWorkingSessionWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.Windows;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ViewBookingWindow.xaml
    /// </summary>
    public partial class ViewBookingWindow : Window
    {
        public ViewBookingWindow()
        {
            InitializeComponent();
            if (SystemParameters.MaximizedPrimaryScreenHeight > 768)
                this.Height = 800;
            else
                this.Height = 700;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

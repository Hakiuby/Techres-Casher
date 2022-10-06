using System.Windows;

namespace TechresStandaloneSale.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageNotificationWindow.xaml
    /// </summary>
    public partial class MessageNotificationWindow : Window
    {
        public MessageNotificationWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void notificationWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Close();
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}

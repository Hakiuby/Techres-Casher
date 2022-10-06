using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace TechresStandaloneSale.Views.NotificationView
{
    /// <summary>
    /// Interaction logic for ErrorNotificationWindow.xaml
    /// </summary>
    public partial class ErrorNotificationWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public ErrorNotificationWindow(string notify)
        {
            InitializeComponent();

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 5;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 50;
            this.Notification.Text = notify;           

            SystemSounds.Asterisk.Play();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dispatcherTimer.Start();
        }
    }
}

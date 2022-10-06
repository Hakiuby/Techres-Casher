using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TechresStandaloneSale.Views.NotificationView
{
    /// <summary>
    /// Interaction logic for WarningNotificationWindow.xaml
    /// </summary>
    public partial class WarningNotificationWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public WarningNotificationWindow(string notify)
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
    }
}

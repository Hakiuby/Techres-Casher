using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TechresStandaloneSale.Views.NotificationView
{
    /// <summary>
    /// Interaction logic for SuccessNotificationWindow.xaml
    /// </summary>
    public partial class SuccessNotificationWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public SuccessNotificationWindow(string notify)
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 5;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 50;

            this.Notification.Text = notify;            

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(string.Concat(System.Windows.Forms.Application.StartupPath, "\\notification_ring.mp3")));
            mediaPlayer.Play();
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dispatcherTimer.Start();
        }
    }
}

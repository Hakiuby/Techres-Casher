using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TechresStandaloneSale.Views.NotificationView
{
    /// <summary>
    /// Interaction logic for InfomationNotificationWindow.xaml
    /// </summary>
    public partial class InfomationNotificationWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public InfomationNotificationWindow(string Title, string Content, string Time)
        {
            InitializeComponent();

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 7;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 50;

            this.Title.Text = Title;
            this.Notification.Text = Content;
            this.Time.Text = Time;
           
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
    }
}

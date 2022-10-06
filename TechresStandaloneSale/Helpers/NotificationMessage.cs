using System;
using System.Windows;
using TechresStandaloneSale.Views.NotificationView;

namespace TechresStandaloneSale.Helpers
{
    public static class NotificationMessage
    {
        public static void Infomation(string notify)
        {
            //Success.Notification.Text = notify;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                SuccessNotificationWindow window = new SuccessNotificationWindow(notify);
                window.Show();
            });

        }
        public static void Infomation(string notify, string filePath)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                SuccessNotificationWindow window = new SuccessNotificationWindow(notify);
                window.Show();
            });
        }
        public static void ShowInfomation(string Title, string Content, string Time)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                InfomationNotificationWindow window = new InfomationNotificationWindow(Title, Content, Time);
                window.Show();
            });
        }
        public static void Warning(string notify)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                WarningNotificationWindow window = new WarningNotificationWindow(notify);
                window.Show();
            });
        }
        public static void Error(dynamic notify)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                string x = Convert.ToString(notify);
                ErrorNotificationWindow window = new ErrorNotificationWindow(x);
                window.Notification.Text = notify;
                window.Show();
            });
        }
    }
}

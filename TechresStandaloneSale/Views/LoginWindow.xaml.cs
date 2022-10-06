using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TechresStandaloneSale.Helpers;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for SaleLogin.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool isClose = false;
        public LoginWindow()
        {
            InitializeComponent();
            
            Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            TaskBarLocation taskBarLocation = Utils.Utils.GetTaskBarLocation();
            if ( taskBarLocation == TaskBarLocation.LEFT)
            {
                Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                Top = 0;
            }
            else if (taskBarLocation == TaskBarLocation.RIGHT)
            {
                Left = 0;
                Top = 0;
            }
            else if (taskBarLocation == TaskBarLocation.BOTTOM)
            {
                Left = 0;
                Top = 0;
            }
            else if (taskBarLocation == TaskBarLocation.TOP)
            {
                Left = 0;
                Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            }
            Maximize(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)e.Source;
            if (passwordBox != null)
            {
                var textBox = passwordBox.Template.FindName("RevealedPassword", passwordBox) as TextBox;
                if (textBox != null)
                {
                    textBox.Text = passwordBox.Password;
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var uri = link.NavigateUri.ToString();
            Process.Start(uri);
            e.Handled = true;
        }
        private void Maximize(Window win)
        {
            //old_size = new Size(win.Width, win.Height);
            //old_loc = new Point(win.Top, win.Left);

            double x = SystemParameters.WorkArea.Width;
            double y = SystemParameters.WorkArea.Height;
            win.WindowState = WindowState.Normal;
            win.Top = 0;
            win.Left = 0;
            win.Width = x;
            win.Height = y;
        }
    }
}

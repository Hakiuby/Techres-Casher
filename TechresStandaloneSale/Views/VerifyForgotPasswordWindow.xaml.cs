using System.Windows;
using System.Windows.Controls;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for VerifyForgotPasswordWindow.xaml
    /// </summary>
    public partial class VerifyForgotPasswordWindow : Window
    {
        public VerifyForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void PasswordNew_PasswordChanged(object sender, RoutedEventArgs e)
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

        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
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
    }
}

using System.Windows;
using System.Windows.Controls;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordPopup.xaml
    /// </summary>
    public partial class ChangePasswordPopup : Window
    {
        public ChangePasswordPopup()
        {
            InitializeComponent();
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

        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
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

        private void PasswordBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}

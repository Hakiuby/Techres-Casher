using System.Windows;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for MoveFoodWindow.xaml
    /// </summary>
    public partial class MoveFoodWindow : Window
    {
        public MoveFoodWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.Windows;

namespace TechresStandaloneSale.Views.CreateEmployeeOfManager
{
    /// <summary>
    /// Interaction logic for DetailEmployeeOfManagerWindow.xaml
    /// </summary>
    public partial class DetailEmployeeWindow : Window
    {
        public DetailEmployeeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.Windows;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for HistoryLogViewWindow.xaml
    /// </summary>
    public partial class HistoryLogViewWindow : Window
    {
        public HistoryLogViewWindow()
        {
            InitializeComponent();
        }

        private void historyLogViewWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

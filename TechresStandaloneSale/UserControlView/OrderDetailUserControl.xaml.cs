using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for OrderDetailUserControl.xaml
    /// </summary>
    /// 
    public partial class OrderDetailUserControl : UserControl
    {
        public OrderDetailUserControl()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TechresStandaloneSale.Views.NotificationView
{
    /// <summary>
    /// Interaction logic for ListNotificationWindow.xaml
    /// </summary>
    public partial class ListNotificationWindow : Window
    {
        public ListNotificationWindow()
        {
            InitializeComponent();
            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial"});
            items.Add(new TodoItem() { Title = "Learn C#" });
            items.Add(new TodoItem() { Title = "Wash the car"});
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public class TodoItem
    {
        public string Title { get; set; }
    }
}

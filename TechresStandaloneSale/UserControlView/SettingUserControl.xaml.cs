
using TechresStandaloneSale.UserControlView;
using System.Windows;
using System.Windows.Controls;
using TechresStandaloneSale.ViewModels;
using System;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for SettingUserControl.xaml
    /// </summary>
    public partial class SettingUserControl : UserControl
    {
        public SettingUserControl()
        {
            InitializeComponent();
        }
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            PrintSettingUserControl print = new PrintSettingUserControl();
            print.DataContext = new PrintConfigViewModel();
            this.ContentSetting.Content = print;
        }
    }
}

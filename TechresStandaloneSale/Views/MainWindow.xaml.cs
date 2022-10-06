using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();
            Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            TaskBarLocation taskBarLocation = Utils.Utils.GetTaskBarLocation();
            if (taskBarLocation == TaskBarLocation.LEFT)
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmployeeList_Click(object sender, RoutedEventArgs e)
        {

        }
        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);

        //    System.Windows.Application.Current.Shutdown();
        //}

        #region Đạt
        private void MainMenu_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            btnMenuMain.Background = (Brush)converter.ConvertFromString("#bee6fd");
        }

        private void MainMenu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            btnMenuMain.Background = (Brush)converter.ConvertFromString("#ffa233");
        }

        private void MenuUser_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            btnMenuUser.Background = (Brush)converter.ConvertFromString("#bee6fd");
        }

        private void MenuUser_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            btnMenuUser.Background = (Brush)converter.ConvertFromString("#ffa233");
        }
        #endregion
    }
}

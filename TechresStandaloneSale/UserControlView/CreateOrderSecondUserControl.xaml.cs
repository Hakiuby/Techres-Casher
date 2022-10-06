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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for CreateOrderSecondUserControl.xaml
    /// </summary>
    public partial class CreateOrderSecondUserControl : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int i = 1;
        public CreateOrderSecondUserControl()
        {
            InitializeComponent();
            #region Test ads 

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            #endregion
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            i++;
            PicHolder.Source = new BitmapImage(new Uri(@"/ImageAdv/" + i + ".png", UriKind.Relative));

            if (i == 2)
            {
                i = 0;
            }
        }
    }
}

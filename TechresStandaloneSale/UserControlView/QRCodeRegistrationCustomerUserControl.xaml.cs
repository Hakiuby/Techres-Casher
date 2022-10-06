using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for QRCodeRegistrationCustomerUserControl.xaml
    /// </summary>
    public partial class QRCodeRegistrationCustomerUserControl : UserControl
    {
        public QRCodeRegistrationCustomerUserControl()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                FileName = string.Format("{0}.png", this.restaurantName.Content.ToString()),
                Filter = "Image Files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)qrCode.Source));
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    encoder.Save(stream);
            }
        }
    }
}

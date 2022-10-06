using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for QRCodeBranchCheckInUserControl.xaml
    /// </summary>
    public partial class QRCodeBranchCheckInUserControl : UserControl
    {
        public QRCodeBranchCheckInUserControl()
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
